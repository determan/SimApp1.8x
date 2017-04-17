// SimDll13.cpp : Defines the exported functions for the DLL application.
//

#include "stdafx.h"
#include "SimDll13.h"

Sim::Simulation::Simulation()
{
	m_iNumReps = 1;	//default, user sets in Model, if greater than 1 needed
	m_sdEps = 0.000001f;
	m_bSimFlag = false;
	m_iCI = NULL;
	m_dConstVals = NULL;
	m_iUp = 0;
    m_iAccOscOn = 0;
}

Sim::Simulation::~Simulation()
{
	if (m_dConstVals)
		delete[] m_dConstVals;
	if (m_iCI)
		delete[] m_iCI;
}

void Sim::Simulation::ICs(double* dda)
{
	SetParameters(NULL, NULL);
	InitializeVars(NULL, dda);
}

void Sim::Simulation::Init()
{
	long ll = sizeof(m_sdData);
	//memset((void*)m_sdData, 0, sizeof(m_sdData));
	for (int i = 0; i < m_iSimSize * 10; i++)
		m_sdData[i] = 0.0;
	m_iIndex = 0;
	m_sdT = 0;
}

void Sim::Simulation::Run(PLOTCB fp, float sdTS, int iNS, float sdMT, bool bC, int iFQ, int iMult, int iNumPlot, int* iPCs, float* fda, int* iIIs, double* dIVs)
{
	int iInd, iTmp;
	float fTmp;
	int iNumVals, iFreqMult, iBufSize, iBufSize2;
	bool bFirst = true;
	float* fBuf = NULL;
	try
	{
		m_ofs.open(".\\__SimResults__Default.bin", std::ios_base::out | std::ios_base::trunc | std::ios_base::binary);
		//m_otfs.open("c:\\simapp\\output.csv", std::ios_base::out | std::ios_base::trunc);
		SetParameters(iIIs, dIVs);
		iFreqMult = iFQ * iMult;
		bContinue = true;
		for (int jj = 0; jj < m_iNumReps; jj++)
		{
			m_sdDelT = (SIM_DATA)sdTS;
			m_sdTMax = (SIM_DATA)sdMT;
			m_i64NN = iNS;
			if (bC)
				fp(-1, 0.0, 0.0);
			InitializeVars(iIIs, dIVs);
			if (bFirst)
			{
				iBufSize = iNumPlot * iMult * SIM_DATA_SIZE;
				iBufSize2 = (m_j + 1);	//1 time step in float for the allocations
				fBuf = new float[iBufSize2];
				iBufSize2 *= 4;	//1 time step in bytes for the writes
				memset((void*)fBuf, 0, sizeof(fBuf));
				iTmp = m_j + 1;
				m_ofs.write((char*)&iNS, 4);	//# time steps - #data bytes = iNS*iTmp*4
				m_ofs.write((char*)&iTmp, 4);	//# variables per record (subtract 1 to get # of SF)
				m_ofs.write((char*)&m_iNumIC, 4);	//# of ICs, 4 byte for index, 8 bytes for data - 12 bytes per IC
				m_ofs.write((char*)&iFQ, 4);	//plot freq mult
				m_ofs.write((char*)&sdTS, 4);	//time step size, s
				m_ofs.write((char*)&sdMT, 4);	//max time, s
				for (int i = 0; i < m_iNumIC; i++)
				{
					m_ofs.write((char*)&iIIs[i], 4);
					m_ofs.write((char*)&dIVs[i], 8);
				}
				char memo[512];
				memset((void*)memo, 0, sizeof(memo));
				m_ofs.write(memo, sizeof(memo));	//set aside mem for memo field (no more SFs)
				//write out initial indices and values (ICs)
				bFirst = false;
			}
			Derivatives(0);	//initialize derivatives
			iNumVals = 0;
			for (m_i = 0; m_i < m_i64NN;)
			{
				if (bContinue)
				{
					fBuf[0] = (float)m_sdT;
					for (int j = 0; j < m_j; j++)
					{	//loop over all variables
						fTmp = (float)(*(m_dataPointers[j]));
						if (std::isfinite(fTmp))//valid number
							fBuf[j + 1] = fTmp;
					}
					m_ofs.write((char*)fBuf, iBufSize2);
					if ((m_i % iFQ) == 0)
					{
						fda[iNumVals++] = (float)m_sdT;
						for (int k = 0; k < iNumPlot; k++)
						{	//loop over selected plot vars
							for (int j = 0; j < m_j; j++)
							{	//loop over all variables
								iInd = iPCs[k];
								if (iInd == j)
								{
									fda[iNumVals++] = (float)*(m_dataPointers[iInd]);
									break;
								}
							}
						}
					}
					if ((m_i % iFreqMult) == 0)
					{
						fp(-3, 0, 0);
						m_ofs.flush();
						iNumVals = 0;
					}
					Integrate();
					m_i++;
				}//continue
			}//time
		}//repitition
		fp(-2, 0.0, 0.0);
		m_ofs.close();
		//m_otfs.close();
		delete[] fBuf;
	}
	catch (...)
	{
	}
}

bool Sim::Simulation::Integrate()
{
	double sdhh = m_sdDelT*0.5;
	double sdh6 = m_sdDelT / 6.0;
//	Derivatives(0);	//k1 step, for all rules
	//save V0 to disk, overwrite V0
	switch (m_eIntegralRule)
	{
	case R1:
		Derivatives(0);	//k1 step
		for (int i = 0; i<SIM_SIZE; i++)	//state var index
		{
			S(V(i, 0) + DV(i, 0)*m_sdDelT, i, 1);	//create state 1 (k1)
		}
		Derivatives(1);	//k1 step, for all rules
		for (int i = 0; i<SIM_SIZE; i++)	//state var index
		{
			S(V(i, 0) + (DV(i, 0) + DV(i, 1))*m_sdDelT / 2, i, 0);	//average states 1 and 2, add to 0, put back in 0 for next iteration
		}
		m_sdT += m_sdDelT;
		break;
	case R2:
		Derivatives(0);	//k1 step
		for (int i = 0; i<SIM_SIZE; i++)	//state var index
		{
			S(DV(i, 0)*m_sdDelT, i, 1);	//create state 1 (k1)
		}
		for (int i = 0; i<SIM_SIZE; i++)	//state var index
		{
			S(V(i, 0) + V(i, 1), i, 0);	//add 0 and 1, put back in zero for next iteration
		}
		m_sdT += m_sdDelT;
		break;
	case R3:
		for (int i = 0; i < SIM_SIZE; i++)	//state var index
			S(V(i, 0) + DV(i, 0) *sdhh, i, 1);	//create state 1 (k1)
		//for (int i = 0; i < SIM_SIZE; i++)
		//	S(V(i, 1), i, 0);
		m_sdT += sdhh;
		Derivatives(1);	//k1 step, for all rules
		for (int i = 0; i<SIM_SIZE; i++)	//state var index
			S(V(i, 0) + DV(i, 1)*sdhh, i, 2);	//create state 2 (k2)
		//for (int i = 0; i < SIM_SIZE; i++)
		//	S(V(i, 2), i, 0);
		Derivatives(2);	//k2 step, for all rules
		for (int i = 0; i<SIM_SIZE; i++)	//state var index
		{
			int iOff_i = SIM_SIZE + i;
			S(V(i, 0) + DV(i, 2)*m_sdDelT, i, 3);	//create state 3 (k2)
			m_sdData[iOff_i + m_iSimSize2] += m_sdData[iOff_i + 2 * m_iSimSize2];
		}
		//for (int i = 0; i < SIM_SIZE; i++)
		//	S(V(i, 3), i, 0);
		//if (fabs(m_sdT - 0.515) < 0.0001)
		//	return true;
		m_sdT += sdhh;
		Derivatives(3);	//k1 step, for all rules
		for (int i = 0; i<SIM_SIZE; i++)	//state var index
			S(V(i, 0) + (DV(i, 0) + 2.0f * DV(i, 1) + DV(i, 3))*sdh6, i, 0);	//weighted average
		//for (int i = 0; i < SIM_SIZE; i++)
		//	S(V(i, 4), i, 0);
		Derivatives(0);
		break;
	case R4:
		break;
	case R5:
		break;
	case R6:
		break;
	case R7:
		break;
	case R8:
		break;
	case R9:
		break;
	case R10:
		break;
	case R11:
		break;
	case R12:
		break;
	case R13:
		break;
	case R14:
		break;
	case R15:
		break;
	default:
		//log error
		break;
	};
	return true;
}

BOOL Sim::Simulation::GetModelState(switches eVal)
{
	BOOL bRetVal = FALSE;
	switch (eVal)
	{
	case AccOn:
		bRetVal = (m_sdT >= *(m_constPointers[120]) && m_sdT <= *(m_constPointers[121]))?TRUE:FALSE;
		break;
	case CoolOn:
		bRetVal = (m_sdT < *(m_constPointers[125])) ? TRUE : FALSE;
		break;
	}
	return bRetVal;
}

double Sim::Simulation::GetModelStateD(switches eVal)
{
	double fRetVal = 0.0;
	switch (eVal)
	{
	case Cool1Mult: fRetVal = *(m_constPointers[126]); break;
	case Cool2Mult: fRetVal = *(m_constPointers[127]); break;
	case Cool3Mult: fRetVal = *(m_constPointers[128]); break;
	case BETA:		fRetVal = *(m_constPointers[115]); break;
	}
	return fRetVal;
}

void Sim::Simulation::AffectModel(switches eVal, double dVal)
{
	if (dVal < 0.01)
		dVal = 0.01;	//don't let flow go to actual zero as this turns off heat transfer
	switch (eVal)
	{
	case AccOn:
		*(m_constPointers[120]) = 0.0;		//on time to 0
		*(m_constPointers[121]) = 1.0e15;	//off time to distant future - net effect is for acc to be on now and for conceivable future
		break;
	case AccOff:
		*(m_constPointers[120]) = 1.0e15;		//on time to distant future
		*(m_constPointers[121]) = 0.0;			//off time to past - net effect is for acc to be off now and for conceivable future
		break;
	case CoolOn:
		*(m_constPointers[125]) = 1.0e15;		//set cool off time high, flow will be on
		break;
	case CoolOff:
		*(m_constPointers[125]) = 0.0;			//set cool off time to 0, flow will be off
		break;
	case Cool1Mult:
		*(m_constPointers[126]) = dVal;
		break;
	case Cool2Mult:
		*(m_constPointers[127]) = dVal;
		break;
	case Cool3Mult:
		*(m_constPointers[128]) = dVal;
		break;
	case HCore:
		*(m_constPointers[26]) = dVal;
		break;
	case A:
		*(m_constPointers[113]) = dVal;
		break;
	case TWC:
		if (dVal < TwcLims::eLowLim)
			dVal = TwcLims::eLowLim;
		if (dVal > TwcLims::eHighLim)
			dVal = TwcLims::eHighLim;
		*(m_constPointers[79]) = dVal;
		break;
	case AccSS:
		*(m_constPointers[119]) = dVal;
		break;
	case AccOscOn:
		m_iAccOscOn = 1;
		m_dSinFreq = 10;
		m_dSinAmp = 0.05;
		break;
	case AccOscOff:
		m_iAccOscOn = 0;
		break;
	case TwcSpinUp:
		m_iUp = 1;
		m_dSpinRate = dVal;
		break;
	case TwcSpinDown:
		m_iUp = -1;
		m_dSpinRate = dVal;
		break;
	case RMax:
		*(m_constPointers[124]) = dVal;
		break;
	case RRTE:
		*(m_constPointers[122]) = dVal;
		break;
	}
}

int Sim::Model::GetICs(int*& iIIs, double*& dIVs)
{
	iIIs = new int[m_iNumIC];
	dIVs = new double[m_iNumIC];
	int j = 0;
	for (int i = 0; i < ConstSize(); i++)
	{
		if (m_iCI[i] >= 0)
		{
			iIIs[j] = m_iCI[i];
			dIVs[j++] = m_dConstVals[m_iCI[i]];
		}
	}
	return m_iNumIC;
}


Sim::Model::Model()
{
	m_constPointers = NULL;
	m_dataPointers = NULL;
	m_sdData = NULL;
	memset((void*)m_szPath, 0, sizeof(m_szPath));
	//sprintf_s(,260,"");
}

Sim::Model::~Model()
{
	if (m_constPointers != NULL)
		delete[] m_constPointers;
	if (m_dataPointers != NULL)
		delete[] m_dataPointers;
	if (m_sdData != NULL)
	{
		//for (int i = 0; i < m_iSimSize2; i++)
		//	delete[] m_sdData[i];
		delete[] m_sdData;
	}
}

int Sim::Model::GetArraySize(char* pBuf, int iSz)
{
	char* pOpBr;
	char* pClBr;
	char* pSm;
	char szbuf[10];
	int iArrSz;
	pSm = strstr(pBuf, ";");
	pOpBr = strstr(pBuf, "[");
	pClBr = strstr(pBuf, "]");
	if (pSm && pOpBr && pClBr && (pSm > pOpBr) && (pClBr > pOpBr))
	{
		int iLen = pClBr - pOpBr - 1;
		strncpy_s(szbuf, pOpBr + 1,  iLen);
		szbuf[iLen] = 0;
		iArrSz = atoi(szbuf);
	}
	else
		iArrSz = -1;
	return iArrSz;
}

int Sim::Simulation::Simulator(char* szpath)
{ 
	try
	{
		m_bSimFlag = true; 
		return sprintf_s((char*)m_szPath, sizeof(m_szPath), "%s", (const char*)szpath);
	}
	catch (...)
	{
		return -1;
	}
}

void Sim::Model::SetParameters(int* iIIs, double* dIVs)
{
	SIM_DATA** pf;
	int iRoundUp;
	char strFileName[MAX_PATH], strLine[500];
	if ((!m_bSimFlag && iIIs) || (m_bSimFlag && !iIIs))
	{
		char szBuf[MAX_PATH];
		DWORD dw = ::GetCurrentDirectoryA(MAX_PATH, szBuf);
		if (strlen(m_szPath) == 0)
			sprintf_s(strFileName, MAX_PATH, ".\\%s.h", Name());
		else
			sprintf_s(strFileName, MAX_PATH, "%s\\%s.h", m_szPath, Name());
		std::ifstream ifs(strFileName, std::ios_base::in);
		if (ifs.is_open())
		{
			if (m_constPointers != NULL)
				delete[] m_constPointers;
			if (m_dataPointers != NULL)
				delete[] m_dataPointers;
			if (m_sdData != NULL)
				delete[] m_sdData;
			if (m_iCI != NULL)
				delete[] m_iCI;

			m_iCI = NULL;
			m_iSimSize = ((StateSize() + 100) / 100) * 100;
			m_iSimSize2 = m_iSimSize * 2;
			iRoundUp = ((NonStateSize() + StateSize() + 1000) / 1000) * 1000;
			m_dataPointers = new SIM_DATA*[iRoundUp];
			iRoundUp = ((ConstSize() + 100) / 100) * 100;
			m_constPointers = new SIM_DATA*[iRoundUp];
			m_sdData = new SIM_DATA[m_iSimSize2 * 5];
			if (m_bSimFlag)
			{
				m_iNumIC = 0;
				m_iCI = new int[ConstSize()];
			}
			memset(m_constPointers, 0, iRoundUp);
			Init();

			m_j = 0;
			int ii = 0, jj = 0, iNNS = 0;
			int iArrSz;
			while (!ifs.eof())
			{
				ifs.getline(strLine, 500, '\n');
				if (strstr(strLine, "//STATE_VAR") || strstr(strLine, "//NS_VAR ") || strstr(strLine, "//CONST"))
				{

					//for double
					if (strstr(strLine, "//NS_VAR "))
					{
						iArrSz = GetArraySize(strLine, 500);
						if (iArrSz < 0)
						{
							m_dataPointers[m_j] = Base() + (m_j + 1);
							*m_dataPointers[m_j] = (SIM_DATA)0.0;
							iNNS++;
							m_j++;
						}
						else
						{
							for (int i = 0; i < iArrSz; i++)
							{
								m_dataPointers[m_j] = Base() + (m_j + 1);
								*m_dataPointers[m_j] = (SIM_DATA)0.0;
								m_j++;
								iNNS++;
							}
						}
					}
					else if (strstr(strLine, "//STATE_VAR"))
					{
						iArrSz = GetArraySize(strLine, 500);
						if (iArrSz < 0)
						{
							pf = (SIM_DATA**)((unsigned char*)Base() + (iNNS + 1) * 8 + ((m_j - iNNS) * 4));	//address of pointers to state vars
							*pf = &(m_sdData[ii]);				//pointers to state vars point to array positions
							m_dataPointers[m_j] = &(m_sdData[ii]);	//datapointers also point to array postions
							m_sdData[ii++] = (SIM_DATA)0.0;		//init state vars to zero
							m_j++;
						}
						else
						{
							for (int i = 0; i < iArrSz; i++)
							{
								pf = (SIM_DATA**)((unsigned char*)Base() + (iNNS + 1) * 8 + ((m_j - iNNS) * 4));	//address of pointers to state vars
								*pf = &(m_sdData[ii]);				//pointers to state vars point to array positions
								m_dataPointers[m_j] = &(m_sdData[ii]);	//datapointers also point to array postions
								m_sdData[ii++] = (SIM_DATA)0.0;		//init state vars to zero
								m_j++;
							}
						}
					}
					else
					{//CONST
						iArrSz = GetArraySize(strLine, 500);
						char* pCh = strstr(strLine, "[Constant]");
						if (iArrSz < 0)
						{
							m_constPointers[jj] = ConstBase() + (jj + 1);
							*m_constPointers[jj] = (SIM_DATA)0.0;
							if (m_bSimFlag)
							{
								if (!pCh)
								{
									m_iCI[jj] = jj;
									m_iNumIC++;
								}
								else
									m_iCI[jj] = -1;
							}
							jj++;
						}
						else
						{
							if (m_bSimFlag)
							{
								if (!pCh)
									m_iNumIC += iArrSz;
							}
							for (int i = 0; i < iArrSz; i++)
							{
								m_constPointers[jj] = ConstBase() + (jj + i + 1);
								*m_constPointers[jj] = (SIM_DATA)0.0;
								if (m_bSimFlag)
								{
									if (!pCh)
										m_iCI[jj] = jj;
									else
										m_iCI[jj] = -1;
								}
								jj++;
							}
						}
					}
				}
			}//while
			ifs.close();
		}//if file open
		for (int i = 0; i < m_j; i++)
			*(m_dataPointers[i]) = (SIM_DATA)0.0;
		if (m_bSimFlag)
			m_bIC = true;
		else
			m_bIC = false;

		//overriding init of specific values from interface
		if (!m_bSimFlag)
		{
		for (int i = 0; i < ConstSize(); i++)
			*(m_constPointers[i]) = m_dConstVals[i];
		for (int i = 0; i < m_iNumIC; i++)
			*(m_constPointers[iIIs[i]]) = dIVs[i];
		}
	}
	else
	{
		if (m_bSimFlag)
			m_bIC = false;
		else
			m_bIC = true;
	}
	SetModelParameters();
}

void Sim::Model::InitializeVars(int* iIIs, double* dIVs)
{
	InitializeModelVars();
	int iCSize = ConstSize();
	m_sdT = (SIM_DATA)0;
	SIM_DATA* fp = ConstBase();
	m_eIntegralRule = R3;
	if (!iIIs)
	{
		double dVal;
		if (m_dConstVals != NULL)
			delete[] m_dConstVals;
		m_dConstVals = new double[iCSize];
		for (int i = 1; i <= iCSize; i++)
		{
			dVal = *((SIM_DATA*)(fp + i));
			if (dIVs)
				dIVs[i - 1] = dVal;
			m_dConstVals[i - 1] = dVal;
		}
		//also have to process arrays of constants here
	}

}

bool Sim::Model::Derivatives(int iCol)
{
	ModelDerivatives(iCol);
	return true;
}

bool Sim::Model::ReCalcConst(bool bFlag)
{
	try
	{
		m_bIC = bFlag;
		SetModelParameters();
		InitializeModelVars();
		return true;
	}
	catch (...)
	{
		return false;
	}
}