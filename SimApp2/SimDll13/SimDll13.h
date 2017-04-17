#pragma once

//-- Generic SYSTEM MODEL FOR A FISSILE SOLUTION SYSTEM (SUPO EXAMPLE)
//----------------------------------------------------------------------------------------------------
//--    
//-- Documentation in LA-UR-13-22033
//-- A Generic System Model for a Fissile Solution System
//-- Kimpland, Robert H. & Klein Steven K. 
//-- Los Alamos National Laboratory, 2013
//--    
//------------------------------------------------------------------------------

#include <iostream>     // std::cout
#include <fstream>

#ifdef SIMDLL_EXPORTS
#define SIMDLL_EXPORTS __declspec(dllimport) 
#else 
#define SIMDLL_EXPORTS __declspec(dllexport) 
#endif 


#define SIM_DATA double
#define SIM_DATA_SIZE 8
#define SIM_SIZE m_iSimSize
#define SIM_SIZE2 m_iSimSize2

namespace Sim
{
	enum switches { None = 0, AccOn, AccOff, CoolOn, CoolOff, Cool1Mult, Cool2Mult, Cool3Mult, HCore, A, TWC, AccSS, BETA, AccOscOn, AccOscOff, TwcSpinUp, TwcSpinDown, RMax, RRTE };
	enum TwcLims { eLowLim = 0, eHighLim = 20 }; 
	typedef void(__stdcall *PLOTCB)(int, float, float);

	class SIMDLL_EXPORTS Simulation
	{
	public:
		enum SIM_INT_RULES { RNONE = 0, R1, R2, R3, R4, R5, R6, R7, R8, R9, R10, R11, R12, R13, R14, R15 };
		Simulation();
		virtual ~Simulation();
		void Run(PLOTCB fp, float sdTS, int iNS, float sdMT, bool bC, int iFQ, int iMult, int iNumPlot, int* iPCs, float* fda, int* iIIs, double* dIVs);
		void ICs(double* dda);
		void Init();
		bool Integrate();
		void CloseFile(){ m_ofs.close(); }
		void Continue(bool bCont){ bContinue = bCont;}
		void SetNumIC(int iNIC){ m_iNumIC = iNIC;}
		virtual void InitializeVars(int* iIIs, double* dIVs) = 0;
		virtual void SetParameters(int* iIIs, double* dIVs) = 0;
		virtual bool Derivatives(int iCol) = 0;
		virtual SIM_DATA YScale() = 0;
		virtual int NumCustIcTabs() = 0;
		virtual bool ReCalcConst(bool bFlag) = 0;
		int swtch(SIM_DATA sdData) { return (sdData > 0) ? 1 : 0; }
		int RecordSize(){ return m_j + 1; }
		double DataItem(int j){ return *(m_dataPointers[j]); }
		int Simulator(char* szpath);
		//void SimFlag(bool bFlag){ m_bSimFlag = bFlag; }
		void DelT(double dt){ m_sdDelT = dt; }
		void AffectModel(switches eVal, double dVal);
		int GetModelState(switches eVal);
		double GetModelStateD(switches eVal);
		void ResetSpin(){ m_iUp = 0; }
	protected:
		SIM_DATA* m_sdData;// [SIM_SIZE2][5];
		int m_iIndex;
		SIM_INT_RULES m_eIntegralRule;
		double m_sdT;
		SIM_DATA m_sdDelT;
		SIM_DATA m_sdTMax;
		__int64 m_i64NN;
		int m_iNumReps;
		int m_j;
		SIM_DATA** m_dataPointers;
		SIM_DATA** m_constPointers;
		SIM_DATA m_sdEps;
		int m_iSimSize;
		int m_iSimSize2;
		bool m_bIC;
		int m_iNumIC;
		double* m_dConstVals;
		int* m_iCI;

		inline void S(SIM_DATA value, int iRow, int iCol){ m_sdData[iRow+iCol*m_iSimSize2] = value; }
		inline void S2(SIM_DATA*& p, SIM_DATA value) { if (!m_bIC /*|| m_bSimFlag*/) *p = value; }
		inline SIM_DATA V(SIM_DATA* p, int iCol);
		inline SIM_DATA V(int iRow, int iCol){ return m_sdData[iRow + iCol*m_iSimSize2]; }
		inline void D(SIM_DATA* p, SIM_DATA val, int iCol);
//		inline SIM_DATA DV(SIM_DATA* p, int iCol){ if (p){ return m_sdData[(int)(p - &(m_sdData[0][0])) % 5 + SIM_SIZE][iCol]; } else{/*log error*/ return 0.0; } }
		inline SIM_DATA DV(int iRow, int iCol){ return m_sdData[iRow + m_iSimSize + iCol*m_iSimSize2]; }
		std::ofstream m_ofs;
		//std::ofstream m_otfs;
		bool m_bSimFlag;
		char m_szPath[FILENAME_MAX];

		//mods to AD135 model
		/*AD135 model changes
		line 798:
		m_sdQS = m_sdQ0+m_sdAccSS*swtch((SIM_DATA)(m_sdT)-m_sdAccOnTime)*swtch(m_sdAccOffTime-(SIM_DATA)(m_sdT));
		changed to
		m_sdQS = m_sdQ0+m_sdAccSS*swtch((SIM_DATA)(m_sdT)-m_sdAccOnTime)*swtch(m_sdAccOffTime-(SIM_DATA)(m_sdT)) + QS135mod(m_sdAccSS,2*m_sdPI*m_sdT);*/
		int m_iAccOscOn;
		double m_dSinAmp;
		double m_dSinFreq;
		double QS135mod(double din, double dss){ return m_iAccOscOn*m_dSinAmp*dss*sin(m_dSinFreq * din); }

		//add, before WC .. WC3 split calcs:	m_sdTWC = TWC135mod(m_sdTWC,m_sdDelT/4);
		int m_iUp;
		double m_dSpinRate;
		inline double TWC135mod(double dtwc, double dts);

	private:
		int m_i;
		bool bContinue;
	};


	class SIMDLL_EXPORTS Model : public Simulation
	{
	public:
		Model();
		~Model();
		void SetParameters(int* iIIs, double* dIVs);
		void InitializeVars(int* iIIs, double* dIVs);
		//void SetParametersSim(int* iIIs, double* dIVs);
		bool Derivatives(int iCol);
		virtual void SetModelParameters() = 0;
		virtual void InitializeModelVars() = 0;
		virtual void ModelDerivatives(int iCol) = 0;
		virtual SIM_DATA* Base() = 0;
		virtual char* Name() = 0;
		virtual SIM_DATA* ConstBase() = 0;
		virtual int StateSize() = 0;
		virtual int NonStateSize() = 0;
		virtual int ConstSize() = 0;
		virtual bool ReCalcConst(bool bFlag);
		int GetArraySize(char* pBuf, int iSz);
		int GetICs(int*& iIIs, double*& dIVs);
	protected://model generator produces new and delete code, and value initialization
		int* m_iaNSAsizes;
		int* m_iaSAsizes;
		int* m_iaCAsizes;
		bool* m_baNSisG;
		bool* m_baSisG;
		bool* m_baCisIC;
		bool* m_baNSAisG;
		bool* m_baSAisG;
		bool* m_baCAisIC;
		/* goes in constructor
		"m_iaNSAsizes = new int[{0}];",listNonStateArrays.Count
		"m_iaSAsizes = new int[{0}];",listStateArrays.Count
		"m_iaCAsizes = new int[{0}];",listConstArrays.Count

		"m_baCisIC = new bool[ConstSize()];"

		"delete [] m_iaNSAsizes;"
		"delete [] m_iaSAsizes;"
		"delete [] m_iaCAsizes;"
		"delete [] m_baCisIC;"
		"delete [] m_baCAisIC;"

		"m_iaNSAsizes[{0}] = {1};",i,listNonStateArrays[i]
		"m_iaNSAsizes[{0}] = {1};",i,listStateArrays[i]
		"m_iaNSAsizes[{0}] = {1};",i,listConstArrays[i]

		"m_baCisIC[{0}] = {1};",i,listConstArrays[i].sType != "Constant"
		*/
	private:
		//define parameters here
		//define state variables here

	};
	
	inline SIM_DATA Simulation::V(SIM_DATA* p, int iCol)
	{
		return (m_bIC /*&& !m_bSimFlag*/) ? 1 :( m_sdData[(int)(p - &(m_sdData[0])) + iCol*m_iSimSize2]);
	}

	inline void Simulation::D(SIM_DATA* p, SIM_DATA val, int iCol)
	{
		if (p)
		{
			m_sdData[(int)(p - &(m_sdData[0])) + SIM_SIZE + iCol*m_iSimSize2] = val;
			if (m_sdT < 0.00000001)
				m_sdData[(int)(p - &(m_sdData[0])) + iCol*m_iSimSize2] += 1e-15;// val*m_sdDelT / 2;
		}
		else
		{/*log error*/
		}
	}

	inline double Simulation::TWC135mod(double dtwc, double dts)
	{
		double dRes;
		if (m_iUp>0)
		{ 
			if (dtwc + dts < TwcLims::eHighLim)
				dRes = dtwc + dts*m_dSpinRate;
			else
				dRes = TwcLims::eHighLim;
		}
		else if (m_iUp<0)
		{
			if (dtwc - dts > TwcLims::eLowLim)
				dRes = dtwc - dts*m_dSpinRate;
			else
				dRes = TwcLims::eLowLim;
		}
		else
			dRes = dtwc;
		return dRes;
	}

}
