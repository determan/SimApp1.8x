#include "stdafx.h"
#include "AD135.h"

Sim::AD135::AD135()
{
	m_iaNSAsizes = NULL;
	m_iaSAsizes = NULL;
	m_iaCAsizes = NULL;
	m_baNSisG = NULL;
	m_baCisIC = NULL;
	m_baNSAisG = NULL;
	m_baSAisG = NULL;
	m_baCAisIC = NULL;
	m_baNSisG = new bool[NonStateSize()];
	int szs6[] = {0,0,0,0,0,0,0,0,0,1,0,0,0,0,0,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,1,1,1,1,1,1,1,1,1,0,0,0,0,1,0,0,0,0,0,0,0,0,0,0,1,0,0,0,0,0,0,0,0,0,1,0,0,0,0,0,1,1,1,1,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,0,0,0,0,0,0,0,0,0,0,0};
	for(int i = 0; i < NonStateSize(); i++)
		m_baNSisG[i] = szs6[i];

	m_baSisG = new bool[StateSize()];
	int szs7[] = {0,0,0,0,0,0,0,0,1,0,0,0,1,0,0,0,0,1,0,0,0,0,1,0,0,0,0,1,0,0,0,0,1,0,0,0,0,1,0,0,0,0,1,0,0,0,0,1,0,0,0,0,1,0,0,0,0,1,0,0,1,0,1,0,1,0,1,0,1,0,1,0,1,0,1,0,1,0,1,1,1,1,1,1,1,1,1,1,0,1,0,1,0,1,0,1,0,1,0,1,0,1,0,1,0,1,0,1,0,0,0,0,0,0,0,0,0,0,0,0,1,0,0,0,0,1,0,0,0,0,1,0,0,0,0,1,0,0,0,0,1,0,0,0,0,1,0,0,0,0,1,0,0,0,0,1,0,0,0,0,1,0,0,0,0,1,0,0,0,1,0,1,0,1,0,1,0,1,0,1,0,1,0,1,0,1,0,1,0,0,1,0,0,0,0,1,0,0,0,0,1,0,0,0,0,1,0,0,0,0,1,0,0,0,0,1,0,0,0,0,1,0,0,0,0,1,0,0,0,0,1,0,0,0,0,1,0,0,0,1,0,1,0,1,0,1,0,1,0,1,0,1,0,1,0,1,0,1,0,0,0,1,1,0,0,1,1,0,0,1,1,0,0,1,1,0,0,1,1,0,0,1,1,0,0,1,1,0,0,1,1,0,0,1,1,0,0,1,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0};
	for(int i = 0; i < StateSize(); i++)
		m_baSisG[i] = szs7[i];

	m_baCisIC = new bool[ConstSize()];
	int szs4[] = {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,0,0,0,1,1,1,1,1,0,1,0,0,0,0,0,1,0,1,1,0,0,1,0,0,1,0,1,1,0,1,0,0,0,0,0,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,0,0,0,0,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,0,1,1,1,1,1,1,1,1,0,1,0,1,1,1,1,1,1,1,1,1,1,1,0,0,0,0,0,0,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0};
	for(int i = 0; i < ConstSize(); i++)
		m_baCisIC[i] = szs4[i];
}

Sim::AD135::~AD135()
{
	delete [] m_iaNSAsizes;
	delete [] m_iaSAsizes;
	delete [] m_iaCAsizes;
	delete [] m_baNSisG;
	delete [] m_baSisG;
	delete [] m_baCisIC;
	delete [] m_baNSAisG;
	delete [] m_baSAisG;
	delete [] m_baCAisIC;
}

void Sim::AD135::SetModelParameters()
{
	//--    SYSTEM MODEL FOR GENERIC ACCELERATOR-DRIVEN FISSILE SOLUTION SYSTEM
	//----------------------------
	//--
	//--    ROBERT KIMPLAND & STEVEN KLEIN
	//--    LOS ALAMOS NATIONAL LABORATORY
	//--    ADVANCED NUCLEAR TECHNOLOGY GROUP
	//--    FEBRUARY, 2013
	//--
	//------------------------
	//-- General Physical Parameters
	m_sdRG = 8.31446;
	//RG: Gas constant (m^3*Pa/K/mol)
	m_sdHM = 0.002;
	//HM: H2 molar mass (kg)
	m_sdOM = 0.032;
	//OM: O2 molar mass (kg)
	m_sdNM = 0.028;
	//NM: N2 molar mass (kg)
	m_sdPI = 3.14159;
	m_sdUM = 235.0;
	//UM: Molar mass of U-235 (g)
	m_sdAN = 6.022e+23;
	//AN: Avogadro's Number
	m_sdkB = 1.38e-23;
	//kB: Boltzman constant (J/K)
	m_sdnmass = 1.66e-27;
	//nmass: Neutron mass (kg)
	//----- ALi | Fi: Delayed neutron parameters, Thermal
	m_sdAL1 = 0.0124;m_sdF1 = 0.033;
	m_sdAL2 = 0.0305;m_sdF2 = 0.219;
	m_sdAL3 = 0.1110;m_sdF3 = 0.196;
	m_sdAL4 = 0.3010;m_sdF4 = 0.395;
	m_sdAL5 = 1.1400;m_sdF5 = 0.115;
	m_sdAL6 = 3.0100;m_sdF6 = 0.042;
	if (m_bIC) m_sdHfg = (SIM_DATA)(2256.4)/1.0e+03;
	//(MJ/kg)
	m_sdsvg = 1.67;
	//(m^3/kg)
	m_sdHg = 2675.6;
	m_sdXT = 1.0e-04;
	//XT=H2 Threshold Quality factor
	//-- Core Configurtion Parameters
	if (m_bIC) m_sdV0 = 0.319;
	//Initial Core Volume
	if (m_bIC) m_sdHCORE = 0.99;
	//Initial core height
	if (m_bIC) m_sdVP0 = 0.20;
	//VP0=Initial volume of plenum (m^3)
	//---- inner channel dimensions
	if (m_bIC) m_sdCC1R = 17.6;
	//inner channel inside radius
	if (m_bIC) m_sdCC1WT = 0.693;
	//inner channel wall thickness
	m_sdCC2R = m_sdCC1R+m_sdCC1WT;
	//inner channel inner wall
	if (m_bIC) m_sdCC1T = 0.55;
	//inner channel thickness
	m_sdCC3R = m_sdCC2R+m_sdCC1T;
	//inner channel io wall
	m_sdCC4R = m_sdCC3R+m_sdCC1WT;
	//inner channel outside radius
	m_sdXSA1 = m_sdPI*((pow(m_sdCC3R,2))-(pow(m_sdCC2R,2)))*0.0001;
	//inner channel cross sectional area
	m_sdCC1V = m_sdXSA1*m_sdHCORE;
	//inner channel volume
	m_sdCC1WV = m_sdHCORE*m_sdPI*(pow(m_sdCC4R,2)-pow(m_sdCC3R,2))*0.0001;
	//Inner channel wall volume
	if (m_bIC) m_sdCC1D = 6570;
	//Inner channel wall density in kg/m^3
	m_sdCC1M = m_sdCC1D*m_sdCC1WV;
	//inner channel wall mass
	m_sdMW = (SIM_DATA)(m_sdCC1M)/10;
	//MW=mass of WALL (kg)
	m_sdCPW = 2.85e-04;
	//CPW=specific heat of WALL (MJ/C/kg)
	if (m_bIC) m_sdTID = 2.750;
	if (m_bIC) m_sdTWT = 0.5830;
    m_sdTOD = m_sdTID + 4 * m_sdTWT - m_sdCC1WT;
	m_sdXSAT = 0.0003*(SIM_DATA)(pow(m_sdTID,2.2))/2;
	if (m_bIC) m_sdTNUM = 16;
	m_sdCCTV = 20*m_sdXSAT*m_sdHCORE/1.57;
	m_sdCCTWV = 10*m_sdHCORE*3*(pow(((SIM_DATA)(m_sdTOD)/2),1.5))*0.001;
	if (m_bIC) m_sdCCTD = 7227;
	m_sdCCTM = m_sdCCTWV*m_sdCCTD;
	m_sdMW2 = (SIM_DATA)(m_sdCCTM)/10;
	m_sdCPW2 = 2.85e-04;
	//CPW=specific heat of WALL (MJ/C/kg)
	//-- outer channel dimensions
	if (m_bIC) m_sdCC5R = 38.50;
	//outer channel outer radius
	if (m_bIC) m_sdCC2WT = 0.6930;
	//outer channel wall thickness
	m_sdCC6R = m_sdCC5R-m_sdCC2WT;
	//outer channel inner radius
	if (m_bIC) m_sdCC2T = 1.1;
	//outer channel thickness
	m_sdCC7R = m_sdCC6R-m_sdCC2T;
	//outer channel io wall
	m_sdCC8R = m_sdCC7R-m_sdCC2WT;
	//outer channel inner wall
	m_sdXSA2 = m_sdPI*((pow(m_sdCC6R,2))-(pow(m_sdCC7R,2)))*0.0001;
	//outer channel cross sectional area
	m_sdCC2V = m_sdXSA2*m_sdHCORE;
	//outer channel volume
	m_sdCC2WV = m_sdHCORE*m_sdPI*(pow(m_sdCC7R,2)-pow(m_sdCC8R,2))*0.0001;
	//outer channel wall volume
	if (m_bIC) m_sdCC2D = 6570;
	//outer channel wall density
	m_sdCC2M = m_sdCC2WV*m_sdCC2D;
	//outer channel wall mass
	m_sdMW3 = (SIM_DATA)(m_sdCC2M)/10;
	//MW=mass of WALL (kg)
	m_sdCPW3 = 2.85e-04;
	//CPW=specific heat of WALL (MJ/C/kg)
	//-- coolent mass computations
	m_sdTCCV = m_sdCC1V+m_sdCCTV+m_sdCC2V;
	//total coolent volume
	m_sdTOTCM = m_sdTCCV*998.3;
	//Total coolent mass
	m_sdCC1MF = (SIM_DATA)(m_sdCC1V)/m_sdTCCV;
	//inner channel coolent volume fraction
	m_sdCCTMF = (SIM_DATA)(m_sdCCTV)/m_sdTCCV;
	//tubes volume fraction
	m_sdCC2MF = (SIM_DATA)(m_sdCC2V)/m_sdTCCV;
	//outer channel volume mass fraction
	m_sdMCLUMP = m_sdTOTCM*(SIM_DATA)(m_sdCC1MF)/20;
	m_sdMCLUMP2 = m_sdTOTCM*(SIM_DATA)(m_sdCCTMF)/20;
	m_sdMCLUMP3 = m_sdTOTCM*(SIM_DATA)(m_sdCC2MF)/20;
	//-- hydraulic diameter; wall thickness fractions; surface areas
	m_sdHDia1 = 2*m_sdCC1T*0.01;
	m_sdHDia2 = m_sdTID*0.01;
	m_sdHDia3 = 2*m_sdCC2T*0.01;
	m_sdWTR1 = (SIM_DATA)(m_sdCC4R)/m_sdCC3R;
	m_sdWTR2 = (SIM_DATA)(m_sdTOD)/m_sdTID;
	m_sdWTR3 = (SIM_DATA)(m_sdCC5R)/m_sdCC6R;
	m_sdSAL1F = m_sdPI*2*m_sdCC4R*m_sdHCORE*1e-02;
	m_sdSAL1C = m_sdPI*2*m_sdCC3R*m_sdHCORE*1e-02;
	m_sdSAL2F = m_sdTNUM*m_sdPI*m_sdTOD*m_sdHCORE*1e-02;
	m_sdSAL2C = m_sdTNUM*m_sdPI*m_sdTID*m_sdHCORE*1e-02;
	m_sdSAL3F = m_sdPI*2*m_sdCC8R*m_sdHCORE*1e-02;
	m_sdSAL3C = m_sdPI*2*m_sdCC7R*m_sdHCORE*1e-02;
	//-- coolent flow fractions
	if (m_bIC) m_sdTWC = 4.6687;
	//kg/sec
	m_sdTXSA = m_sdXSA1+12*m_sdXSAT+m_sdXSA2;
	m_sdXSA1f = (SIM_DATA)(m_sdXSA1)/m_sdTXSA;
	m_sdXSATf = m_sdTNUM*(SIM_DATA)(m_sdXSAT)/m_sdTXSA;
	m_sdXSA2f = (SIM_DATA)(m_sdXSA2)/m_sdTXSA;
	//-- Reactivity Parameters
	if (m_bIC) m_sdALF = 0.0417;
	//ALF: Core averaged temperature coefficient of reactivity
	if (m_bIC) m_sdPHI = 39.9871;
	//PHI: Void coefficient of reactivity
	//-- FRAi: Fission fractions for ith region
	if (m_bIC) m_sdFRA10 = 0.0343;
	if (m_bIC) m_sdFRA9 = 0.0784;
	if (m_bIC) m_sdFRA8 = 0.1137;
	if (m_bIC) m_sdFRA7 = 0.1351;
	if (m_bIC) m_sdFRA6 = 0.1452;
	if (m_bIC) m_sdFRA5 = 0.1486;
	if (m_bIC) m_sdFRA4 = 0.1334;
	if (m_bIC) m_sdFRA3 = 0.1072;
	if (m_bIC) m_sdFRA2 = 0.0736;
	if (m_bIC) m_sdFRA1 = 0.0305;
	//--
	if (m_bIC) m_sdI10 = 0.0106;
	if (m_bIC) m_sdI9 = 0.0526;
	if (m_bIC) m_sdI8 = 0.1089;
	if (m_bIC) m_sdI7 = 0.1547;
	if (m_bIC) m_sdI6 = 0.1808;
	if (m_bIC) m_sdI5 = 0.1869;
	if (m_bIC) m_sdI4 = 0.1525;
	if (m_bIC) m_sdI3 = 0.0980;
	if (m_bIC) m_sdI2 = 0.0465;
	if (m_bIC) m_sdI1 = 0.0085;
	if (m_bIC) m_sdTAU = 0.7835;
	//TAU: Bubble transit time in sec
	if (m_bIC) m_sdHfg = (SIM_DATA)(2256.4)/1.0e+03;
	//(MJ/kg)
	m_sdsvg = 1.67;
	//(m^3/kg)
	m_sdTAUS = (SIM_DATA)(1.0)/5.5;
	m_sdHg = 2675.6;
	//-- Operational Parameters
	if (m_bIC) m_sdT0 = 20.0;
	//T0: Initial fuel temperature in deg C
	//-- TC0=20.0 |-- TC0: Coolent inlet temperature in deg C
	if (m_bIC) m_sdTC0 = 20.0;
	//TC0: Coolent inlet temperature in deg C
}

void Sim::AD135::InitializeModelVars()
{
	S2(m_sdVNI, 510.0e-03);
	//VNI: Flow rate of cover gas in m^3/sec
	if (m_bIC) m_sdFE = 0.1975;
	//FE: Fuel enrichment
	if (m_bIC) m_sdFC = 148.50;
	//FC: Fuel Uranium concentration in g/l
	if (m_bIC) m_sdCV = (-4.3706*m_sdFC+4218)*1e-06;
	//CV=Specific heat of fuel (MJ/C/kg)
	if (m_bIC) m_sdA = -0.44;
	//A: Initial Power ($)
	//-- A=-0.36 |-- A: Initial Power ($)
	//-- Intrinsic Neutron Source Data
	if (m_bIC) m_sdMNT = 6.8104e-05;
	//MNT: Mean neutron generation time
	if (m_bIC) m_sdBETA = 0.00794;
	//BETA: Delayed neutron fraction
	//-- BOL=Effective BETA/Mean neutron generation time (1/s)
	m_sdBOL = (SIM_DATA)(m_sdBETA)/m_sdMNT;
	if (m_bIC) m_sdQ0 = 1.0e+03;
	//Q0=Initial intrinsic neutron source (#/s)
	m_sdEN0 = -(SIM_DATA)(m_sdQ0)/(SIM_DATA)(m_sdBOL)/m_sdA;
	//EN0=Inital power (MW)
	S2(m_sdEN, 1.0);S2(m_sdD1, 1.);S2(m_sdD2, 1.);S2(m_sdD3, 1.);S2(m_sdD4, 1.);S2(m_sdD5, 1.);S2(m_sdD6, 1.);
	//-- Off Normal Events
	if (m_bIC) m_sdAccSS = 4.4e+13;
	//-- AccSS=5.0e+13
	//-- AccOnTime=0.00
	//-- AccOffTime=7000.00
	if (m_bIC) m_sdAccOnTime = 0.00;
	if (m_bIC) m_sdAccOffTime = 7000.00;
	if (m_bIC) m_sdRRTE = 0.1;
	//RRTE: Reactivity insertion rate in $/sec
	if (m_bIC) m_sdFLAG = 0.0;
	//FLAG: Point-in-time-of reactivity insertion in sec
	if (m_bIC) m_sdRmax = 0.0;
	//Rmax: Total reactivity insertion in $
	//-- RRTE=0.1 |-- RRTE: Reactivity insertion rate in $/sec
	//-- FLAG=0.0 |-- FLAG: Point-in-time-of reactivity insertion in sec
	//-- Rmax=0.0 |-- Rmax: Total reactivity insertion in $
	if (m_bIC) m_sdCoolOffTime = 5000.00;
	if (m_bIC) m_sdCoolHigh1 = 1.0;
	//Coolant flow rate multiplier
	if (m_bIC) m_sdCoolHigh2 = 1.0;
	//Coolant flow rate multiplier
	if (m_bIC) m_sdCoolHigh3 = 1.0;
	//Coolant flow rate multiplier
	if (m_bIC) m_sdPP = 1.0;
	//Plenum pressure in atm
	m_sdP0 = m_sdPP*1.0133e+05;
	//P0: Plenum inlet pressure in pa
	m_sdTB = ((SIM_DATA)(1)/(log((SIM_DATA)(1)/m_sdPP)*2.045e-04+2.68e-03))-273.13;
	//-- Simulation Control
	m_sdTMAX = 2000.0;
	//TMAX: Maximum time of the simulation in seconds
	//-- NN: Sampling points
	m_sdNN = 200001;
	m_sdscale = 10;
	//scale: +/- limits of display variables
	//-- Display parameters
	//----- Black on white display set with thick lines
	//-- display C16
	//-- display N-8
	//-- display R
	//----- Color on black display
	//-- ----------------------------------------------------------------------------------------------------------------------------------------------------------
	//-- Boundary layer geometry
	//-- ----------------------------------------------------------------------------------------------------------------------------------------------------------
	m_sdLI = (SIM_DATA)(m_sdHCORE)/10;
	//-- BL=Boundary layer thickness (m)
	if (m_bIC) m_sdBL = (SIM_DATA)(1.00)/1000;
	m_sdCTR = (SIM_DATA)(1)/100;
	m_sdALI = m_sdPI*((m_sdBL+0.18)*(m_sdBL+0.18)-0.18*0.18);
	m_sdALO = m_sdPI*(0.34*0.34-(0.34-m_sdBL)*(0.34-m_sdBL));
	m_sdALOOP1 = (SIM_DATA)(m_sdSAL1F)/m_sdHCORE*m_sdBL;
	m_sdALOOP2 = (SIM_DATA)(m_sdSAL2F)/m_sdHCORE*m_sdBL;
	m_sdALOOP3 = (SIM_DATA)(m_sdSAL3F)/m_sdHCORE*m_sdBL;
	m_sdALOOPT = m_sdALOOP1+m_sdALOOP2+m_sdALOOP3;
	m_sdAL = (SIM_DATA)(3648)/(SIM_DATA)(1.0e+04)/m_sdHCORE*m_sdBL;
	m_sdAC = (SIM_DATA)(m_sdV0)/m_sdHCORE;
	m_sdVLAYER = m_sdAL*m_sdHCORE;
	//VLAYER=Volume of blz (m^3)
	//-- VL=VLAYER
	m_sdVL = m_sdSAL1F*m_sdBL;
	m_sdVL2 = m_sdSAL2F*m_sdBL;
	m_sdVL3 = m_sdSAL3F*m_sdBL;
	//-- EM=V0*FuelDen/10 | -- EM=Total mass of fuel in bfz divided by # of fuel regions (kg)
	//-- ELM=VL*FuelDen/20  | -- ELM=Total mass of fuel in blz divided by # of fuel regions/2 (kg)
	//-- ELM2=VL2*FuelDen/20
	//-- ELM3=VL3*FuelDen/20
	//-- ----------------------------------------------------------------------------------------------------------------------------------------------------
	//-- AK=Inverse heat capacity of fuel region (C/MJ)
	//-- AK=1/(EM*CV)
	//-- AK2=1/(ELM*CV)
	//--
	//-- Radiolytic gas generation data
	//-- At 0.5 kW/L, MH2,MO2=Mass production rate (kg/m^3/s)
	m_sdMH2 = 1.78e-04;
	m_sdMO2 = 1.42e-03;
	//-- TAU,TAU2=Transit time of H2,O2 bubble in each fuel region i (s)
	m_sdTAU2 = m_sdTAU;
	//-- PHI(i)=Reactivity coeff. of gas ($/m^3)
	m_sdPHI1 = m_sdPHI*m_sdI1;
	m_sdPHI2 = m_sdPHI*m_sdI2;
	m_sdPHI3 = m_sdPHI*m_sdI3;
	m_sdPHI4 = m_sdPHI*m_sdI4;
	m_sdPHI5 = m_sdPHI*m_sdI5;
	m_sdPHI6 = m_sdPHI*m_sdI6;
	m_sdPHI7 = m_sdPHI*m_sdI7;
	m_sdPHI8 = m_sdPHI*m_sdI8;
	m_sdPHI9 = m_sdPHI*m_sdI9;
	m_sdPHI10 = m_sdPHI*m_sdI10;
	//-- Initial Fuel Volumes
	S2(m_sdVFUEL1, (SIM_DATA)(m_sdV0)/10);
	S2(m_sdVFUEL2, (SIM_DATA)(m_sdV0)/10);
	S2(m_sdVFUEL3, (SIM_DATA)(m_sdV0)/10);
	S2(m_sdVFUEL4, (SIM_DATA)(m_sdV0)/10);
	S2(m_sdVFUEL5, (SIM_DATA)(m_sdV0)/10);
	S2(m_sdVFUEL6, (SIM_DATA)(m_sdV0)/10);
	S2(m_sdVFUEL7, (SIM_DATA)(m_sdV0)/10);
	S2(m_sdVFUEL8, (SIM_DATA)(m_sdV0)/10);
	S2(m_sdVFUEL9, (SIM_DATA)(m_sdV0)/10);
	S2(m_sdVFUEL10, (SIM_DATA)(m_sdV0)/10);
	//-- TEMP(i)=Initial Fuel Temperature in region i (C)
	//-- BR(i)=Fraction of total heat transfered in region i
	S2(m_sdTEMP1, m_sdT0);m_sdBR1 = 0.1;
	S2(m_sdTEMP2, m_sdT0);m_sdBR2 = 0.1;
	S2(m_sdTEMP3, m_sdT0);m_sdBR3 = 0.1;
	S2(m_sdTEMP4, m_sdT0);m_sdBR4 = 0.1;
	S2(m_sdTEMP5, m_sdT0);m_sdBR5 = 0.1;
	S2(m_sdTEMP6, m_sdT0);m_sdBR6 = 0.1;
	S2(m_sdTEMP7, m_sdT0);m_sdBR7 = 0.1;
	S2(m_sdTEMP8, m_sdT0);m_sdBR8 = 0.1;
	S2(m_sdTEMP9, m_sdT0);m_sdBR9 = 0.1;
	S2(m_sdTEMP10, m_sdT0);m_sdBR10 = 0.1;
	//-- Boundary layer Temperatures (C)
	S2(m_sdTL11, m_sdT0);
	S2(m_sdTL21, m_sdT0);
	S2(m_sdTL31, m_sdT0);
	S2(m_sdTL41, m_sdT0);
	S2(m_sdTL51, m_sdT0);
	S2(m_sdTL61, m_sdT0);
	S2(m_sdTL71, m_sdT0);
	S2(m_sdTL81, m_sdT0);
	S2(m_sdTL91, m_sdT0);
	S2(m_sdTL101, m_sdT0);
	//--
	S2(m_sdTL12, m_sdT0);
	S2(m_sdTL22, m_sdT0);
	S2(m_sdTL32, m_sdT0);
	S2(m_sdTL42, m_sdT0);
	S2(m_sdTL52, m_sdT0);
	S2(m_sdTL62, m_sdT0);
	S2(m_sdTL72, m_sdT0);
	S2(m_sdTL82, m_sdT0);
	S2(m_sdTL92, m_sdT0);
	S2(m_sdTL102, m_sdT0);
	//-- Wall Temperatures (C)
	S2(m_sdTW1, m_sdT0);
	S2(m_sdTW2, m_sdT0);
	S2(m_sdTW3, m_sdT0);
	S2(m_sdTW4, m_sdT0);
	S2(m_sdTW5, m_sdT0);
	S2(m_sdTW6, m_sdT0);
	S2(m_sdTW7, m_sdT0);
	S2(m_sdTW8, m_sdT0);
	S2(m_sdTW9, m_sdT0);
	S2(m_sdTW10, m_sdT0);
	//-- Boundary layer volumes (m^3)
	S2(m_sdVL11, (SIM_DATA)(m_sdVL)/20);
	S2(m_sdVL21, (SIM_DATA)(m_sdVL)/20);
	S2(m_sdVL31, (SIM_DATA)(m_sdVL)/20);
	S2(m_sdVL41, (SIM_DATA)(m_sdVL)/20);
	S2(m_sdVL51, (SIM_DATA)(m_sdVL)/20);
	S2(m_sdVL61, (SIM_DATA)(m_sdVL)/20);
	S2(m_sdVL71, (SIM_DATA)(m_sdVL)/20);
	S2(m_sdVL81, (SIM_DATA)(m_sdVL)/20);
	S2(m_sdVL91, (SIM_DATA)(m_sdVL)/20);
	S2(m_sdVL101, (SIM_DATA)(m_sdVL)/20);
	//--
	S2(m_sdVL12, (SIM_DATA)(m_sdVL)/20);
	S2(m_sdVL22, (SIM_DATA)(m_sdVL)/20);
	S2(m_sdVL32, (SIM_DATA)(m_sdVL)/20);
	S2(m_sdVL42, (SIM_DATA)(m_sdVL)/20);
	S2(m_sdVL52, (SIM_DATA)(m_sdVL)/20);
	S2(m_sdVL62, (SIM_DATA)(m_sdVL)/20);
	S2(m_sdVL72, (SIM_DATA)(m_sdVL)/20);
	S2(m_sdVL82, (SIM_DATA)(m_sdVL)/20);
	S2(m_sdVL92, (SIM_DATA)(m_sdVL)/20);
	S2(m_sdVL102, (SIM_DATA)(m_sdVL)/20);
	//-- Boundary layer 2 Temperatures (C)
	S2(m_sdTL211, m_sdT0);
	S2(m_sdTL221, m_sdT0);
	S2(m_sdTL231, m_sdT0);
	S2(m_sdTL241, m_sdT0);
	S2(m_sdTL251, m_sdT0);
	S2(m_sdTL261, m_sdT0);
	S2(m_sdTL271, m_sdT0);
	S2(m_sdTL281, m_sdT0);
	S2(m_sdTL291, m_sdT0);
	S2(m_sdTL2101, m_sdT0);
	//--
	S2(m_sdTL212, m_sdT0);
	S2(m_sdTL222, m_sdT0);
	S2(m_sdTL232, m_sdT0);
	S2(m_sdTL242, m_sdT0);
	S2(m_sdTL252, m_sdT0);
	S2(m_sdTL262, m_sdT0);
	S2(m_sdTL272, m_sdT0);
	S2(m_sdTL282, m_sdT0);
	S2(m_sdTL292, m_sdT0);
	S2(m_sdTL2102, m_sdT0);
	//-- Wall Temperatures (C)
	S2(m_sdTW21, m_sdT0);
	S2(m_sdTW22, m_sdT0);
	S2(m_sdTW23, m_sdT0);
	S2(m_sdTW24, m_sdT0);
	S2(m_sdTW25, m_sdT0);
	S2(m_sdTW26, m_sdT0);
	S2(m_sdTW27, m_sdT0);
	S2(m_sdTW28, m_sdT0);
	S2(m_sdTW29, m_sdT0);
	S2(m_sdTW210, m_sdT0);
	//-- Boundary layer volumes (m^3)
	S2(m_sdVL211, (SIM_DATA)(m_sdVL2)/20);
	S2(m_sdVL221, (SIM_DATA)(m_sdVL2)/20);
	S2(m_sdVL231, (SIM_DATA)(m_sdVL2)/20);
	S2(m_sdVL241, (SIM_DATA)(m_sdVL2)/20);
	S2(m_sdVL251, (SIM_DATA)(m_sdVL2)/20);
	S2(m_sdVL261, (SIM_DATA)(m_sdVL2)/20);
	S2(m_sdVL271, (SIM_DATA)(m_sdVL2)/20);
	S2(m_sdVL281, (SIM_DATA)(m_sdVL2)/20);
	S2(m_sdVL291, (SIM_DATA)(m_sdVL2)/20);
	S2(m_sdVL2101, (SIM_DATA)(m_sdVL2)/20);
	//--
	S2(m_sdVL212, (SIM_DATA)(m_sdVL2)/20);
	S2(m_sdVL222, (SIM_DATA)(m_sdVL2)/20);
	S2(m_sdVL232, (SIM_DATA)(m_sdVL2)/20);
	S2(m_sdVL242, (SIM_DATA)(m_sdVL2)/20);
	S2(m_sdVL252, (SIM_DATA)(m_sdVL2)/20);
	S2(m_sdVL262, (SIM_DATA)(m_sdVL2)/20);
	S2(m_sdVL272, (SIM_DATA)(m_sdVL2)/20);
	S2(m_sdVL282, (SIM_DATA)(m_sdVL2)/20);
	S2(m_sdVL292, (SIM_DATA)(m_sdVL2)/20);
	S2(m_sdVL2102, (SIM_DATA)(m_sdVL2)/20);
	//--
	//-- Boundary layer 3 Temperatures (C)
	S2(m_sdTL311, m_sdT0);
	S2(m_sdTL321, m_sdT0);
	S2(m_sdTL331, m_sdT0);
	S2(m_sdTL341, m_sdT0);
	S2(m_sdTL351, m_sdT0);
	S2(m_sdTL361, m_sdT0);
	S2(m_sdTL371, m_sdT0);
	S2(m_sdTL381, m_sdT0);
	S2(m_sdTL391, m_sdT0);
	S2(m_sdTL3101, m_sdT0);
	//--
	S2(m_sdTL312, m_sdT0);
	S2(m_sdTL322, m_sdT0);
	S2(m_sdTL332, m_sdT0);
	S2(m_sdTL342, m_sdT0);
	S2(m_sdTL352, m_sdT0);
	S2(m_sdTL362, m_sdT0);
	S2(m_sdTL372, m_sdT0);
	S2(m_sdTL382, m_sdT0);
	S2(m_sdTL392, m_sdT0);
	S2(m_sdTL3102, m_sdT0);
	//--
	//-- Wall Temperatures (C)
	S2(m_sdTW31, m_sdT0);
	S2(m_sdTW32, m_sdT0);
	S2(m_sdTW33, m_sdT0);
	S2(m_sdTW34, m_sdT0);
	S2(m_sdTW35, m_sdT0);
	S2(m_sdTW36, m_sdT0);
	S2(m_sdTW37, m_sdT0);
	S2(m_sdTW38, m_sdT0);
	S2(m_sdTW39, m_sdT0);
	S2(m_sdTW310, m_sdT0);
	//--
	//-- Boundary layer volumes (m^3)
	S2(m_sdVL311, (SIM_DATA)(m_sdVL3)/20);
	S2(m_sdVL321, (SIM_DATA)(m_sdVL3)/20);
	S2(m_sdVL331, (SIM_DATA)(m_sdVL3)/20);
	S2(m_sdVL341, (SIM_DATA)(m_sdVL3)/20);
	S2(m_sdVL351, (SIM_DATA)(m_sdVL3)/20);
	S2(m_sdVL361, (SIM_DATA)(m_sdVL3)/20);
	S2(m_sdVL371, (SIM_DATA)(m_sdVL3)/20);
	S2(m_sdVL381, (SIM_DATA)(m_sdVL3)/20);
	S2(m_sdVL391, (SIM_DATA)(m_sdVL3)/20);
	S2(m_sdVL3101, (SIM_DATA)(m_sdVL3)/20);
	//--
	S2(m_sdVL312, (SIM_DATA)(m_sdVL3)/20);
	S2(m_sdVL322, (SIM_DATA)(m_sdVL3)/20);
	S2(m_sdVL332, (SIM_DATA)(m_sdVL3)/20);
	S2(m_sdVL342, (SIM_DATA)(m_sdVL3)/20);
	S2(m_sdVL352, (SIM_DATA)(m_sdVL3)/20);
	S2(m_sdVL362, (SIM_DATA)(m_sdVL3)/20);
	S2(m_sdVL372, (SIM_DATA)(m_sdVL3)/20);
	S2(m_sdVL382, (SIM_DATA)(m_sdVL3)/20);
	S2(m_sdVL392, (SIM_DATA)(m_sdVL3)/20);
	S2(m_sdVL3102, (SIM_DATA)(m_sdVL3)/20);
	//-- Coolant loop initial temperatures
	//-- TC1IN=Temperature of coolant entering core (C)
	//-- TC1(i)=Temperature of Node i first lump (C)
	//-- TC2(i)=Temperature of Node i second lump (C)
	m_sdTC1IN = m_sdTC0;
	//--
	S2(m_sdTC11, m_sdTC0);S2(m_sdTC12, m_sdTC0);
	S2(m_sdTC21, m_sdTC0);S2(m_sdTC22, m_sdTC0);
	S2(m_sdTC31, m_sdTC0);S2(m_sdTC32, m_sdTC0);
	S2(m_sdTC41, m_sdTC0);S2(m_sdTC42, m_sdTC0);
	S2(m_sdTC51, m_sdTC0);S2(m_sdTC52, m_sdTC0);
	S2(m_sdTC61, m_sdTC0);S2(m_sdTC62, m_sdTC0);
	S2(m_sdTC71, m_sdTC0);S2(m_sdTC72, m_sdTC0);
	S2(m_sdTC81, m_sdTC0);S2(m_sdTC82, m_sdTC0);
	S2(m_sdTC91, m_sdTC0);S2(m_sdTC92, m_sdTC0);
	S2(m_sdTC101, m_sdTC0);S2(m_sdTC102, m_sdTC0);
	//-- Loop 2
	S2(m_sdTC211, m_sdTC0);S2(m_sdTC212, m_sdTC0);
	S2(m_sdTC221, m_sdTC0);S2(m_sdTC222, m_sdTC0);
	S2(m_sdTC231, m_sdTC0);S2(m_sdTC232, m_sdTC0);
	S2(m_sdTC241, m_sdTC0);S2(m_sdTC242, m_sdTC0);
	S2(m_sdTC251, m_sdTC0);S2(m_sdTC252, m_sdTC0);
	S2(m_sdTC261, m_sdTC0);S2(m_sdTC262, m_sdTC0);
	S2(m_sdTC271, m_sdTC0);S2(m_sdTC272, m_sdTC0);
	S2(m_sdTC281, m_sdTC0);S2(m_sdTC282, m_sdTC0);
	S2(m_sdTC291, m_sdTC0);S2(m_sdTC292, m_sdTC0);
	S2(m_sdTC2101, m_sdTC0);S2(m_sdTC2102, m_sdTC0);
	//-- Loop 3
	S2(m_sdTC311, m_sdTC0);S2(m_sdTC312, m_sdTC0);
	S2(m_sdTC321, m_sdTC0);S2(m_sdTC322, m_sdTC0);
	S2(m_sdTC331, m_sdTC0);S2(m_sdTC332, m_sdTC0);
	S2(m_sdTC341, m_sdTC0);S2(m_sdTC342, m_sdTC0);
	S2(m_sdTC351, m_sdTC0);S2(m_sdTC352, m_sdTC0);
	S2(m_sdTC361, m_sdTC0);S2(m_sdTC362, m_sdTC0);
	S2(m_sdTC371, m_sdTC0);S2(m_sdTC372, m_sdTC0);
	S2(m_sdTC381, m_sdTC0);S2(m_sdTC382, m_sdTC0);
	S2(m_sdTC391, m_sdTC0);S2(m_sdTC392, m_sdTC0);
	S2(m_sdTC3101, m_sdTC0);S2(m_sdTC3102, m_sdTC0);
	//--  Heat Exchanger initial temperatures
	//-- TP2=Inlet temperature to tube side (C)
	//-- TT1,TT2=Tube side temperatures, first and second lumps (C)
	//-- TS1,TS2=Shell side temperatures, first and second lumps (C)
	//-- TT=Tube temperature (C)
	//-- TS=Shell temperature (C)
	S2(m_sdTP2, m_sdT0);
	S2(m_sdTT1, m_sdT0);S2(m_sdTT2, m_sdT0);
	m_sdTS1IN = m_sdT0;
	//TS1IN=Shell side inlet temperature (C)
	S2(m_sdTS1, m_sdT0);S2(m_sdTS2, m_sdT0);
	S2(m_sdTT, m_sdT0);S2(m_sdTS, m_sdT0);
	//-- ---------------------------------------
	//-- Initial values for plenum
	//-- ----------------------------------------
	m_sdAIR = -1.0;
	//AIR=Cover gas Flag
	S2(m_sdPN, m_sdP0*(0.79+0.21*swtch(m_sdAIR)));
	//PN=Partial Pressure of N2 (Pa)
	S2(m_sdPO, m_sdP0*(0.21-0.21*swtch(m_sdAIR)));
	//PO=Partial Pressure of O2 (Pa)
	S2(m_sdTP, m_sdT0+273);
	//TP=Initial Temperature of plenum (K)
	//--
	m_sdPDOT = 0.0;
	S2(m_sdVP, m_sdVP0);
	//VP=Plenum volume (m^3)
	S2(m_sdMN, (SIM_DATA)(V(m_sdPN, 0))/(SIM_DATA)(((SIM_DATA)(m_sdRG)/m_sdNM))/V(m_sdTP, 0)*V(m_sdVP, 0));
	S2(m_sdMOX, (SIM_DATA)(V(m_sdPO, 0))/(SIM_DATA)(((SIM_DATA)(m_sdRG)/m_sdOM))/V(m_sdTP, 0)*V(m_sdVP, 0));
	m_sdRNI = (SIM_DATA)(V(m_sdPN, 0))/(SIM_DATA)(((SIM_DATA)(m_sdRG)/m_sdNM))/V(m_sdTP, 0);
	//RNI=Density of N2 cover gas at plenum inlet (kg/m^3)
	m_sdROI = (SIM_DATA)(V(m_sdPO, 0))/(SIM_DATA)(((SIM_DATA)(m_sdRG)/m_sdOM))/V(m_sdTP, 0);
	//ROI=Density of O2 cover gas at plenum inlet (kg/m^3)
	//-- VOGO=Volumetric flow rate of off-gas out of plenum  (m^3/s)
	S2(m_sdVOGO, V(m_sdVNI, 0));
	//--
	//----------------------
}

void Sim::AD135::ModelDerivatives(int iCol)
{
	//---------------------
	//--
	//-- **********************************
	//-- NEUTRON KINETICS MODEL
	//-- **********************************
	//--
	//--  --------------------------------------------------------
	//-- Reactivity Model
	//-- ---------------------------------------------------------
	//--
	//-- Reactivity insertion model   ----------------
	//--
	m_sdCUT = (SIM_DATA)(m_sdRmax)/m_sdRRTE;
	m_sdRAMP = m_sdRRTE*((SIM_DATA)(m_sdT)-m_sdFLAG)*swtch((SIM_DATA)(m_sdT)-m_sdFLAG)-m_sdRRTE*((SIM_DATA)(m_sdT)-(m_sdFLAG+m_sdCUT))*swtch((SIM_DATA)(m_sdT)-(m_sdFLAG+m_sdCUT));
	//-- --------------------------------------------------------------------------------------------------------------------------
	m_sdTEMP = (SIM_DATA)((V(m_sdTEMP1, iCol)+V(m_sdTEMP2, iCol)+V(m_sdTEMP3, iCol)+V(m_sdTEMP4, iCol)+V(m_sdTEMP5, iCol)+V(m_sdTEMP6, iCol)+V(m_sdTEMP7, iCol)+V(m_sdTEMP8, iCol)+V(m_sdTEMP9, iCol)+V(m_sdTEMP10, iCol)))/10;
	m_sdalpha = ((0.00019*pow((0.1*m_sdTEMP),3))-(0.00624*pow((0.1*m_sdTEMP),2))+0.12101*(0.1*m_sdTEMP)-0.01837)*0.001;
	m_sdkinvis = (1.7297*pow((0.1*m_sdTEMP),(-0.78195)))*1e-06;
	m_sdkinvis1 = m_sdkinvis;m_sdkinvis2 = m_sdkinvis;m_sdkinvis3 = m_sdkinvis;
	m_sdFuelDen = ((-2.97298*pow(10,(-6)))*(pow(m_sdTEMP,2))+(-1.47582*pow(10,(-4)))*m_sdTEMP+1.00273)*((1.16535*pow(10,(-3)))*m_sdFC+1)*1000;
	m_sdEM = m_sdV0*(SIM_DATA)(m_sdFuelDen)/10;
	//EM=Total mass of fuel in bfz divided by # of fuel regions (kg)
	m_sdELM = m_sdVL*(SIM_DATA)(m_sdFuelDen)/20;
	//ELM=Total mass of fuel in blz divided by # of fuel regions/2 (kg)
	m_sdELM2 = m_sdVL2*(SIM_DATA)(m_sdFuelDen)/20;
	m_sdELM3 = m_sdVL3*(SIM_DATA)(m_sdFuelDen)/20;
	//-- ----------------------------------------------------------------------------------------------------------------------------------------------------
	//-- AK=Inverse heat capacity of fuel region (C/MJ)
	m_sdAK = (SIM_DATA)(1)/(m_sdEM*m_sdCV);
	m_sdAK2 = (SIM_DATA)(1)/(m_sdELM*m_sdCV);
	m_sdTCA1 = (SIM_DATA)((V(m_sdTC11, iCol)+V(m_sdTC12, iCol)))/2;
	m_sdTCA2 = (SIM_DATA)((V(m_sdTC21, iCol)+V(m_sdTC22, iCol)))/2;
	m_sdTCA3 = (SIM_DATA)((V(m_sdTC31, iCol)+V(m_sdTC32, iCol)))/2;
	m_sdTCA4 = (SIM_DATA)((V(m_sdTC41, iCol)+V(m_sdTC42, iCol)))/2;
	m_sdTCA5 = (SIM_DATA)((V(m_sdTC51, iCol)+V(m_sdTC52, iCol)))/2;
	m_sdTCA6 = (SIM_DATA)((V(m_sdTC61, iCol)+V(m_sdTC62, iCol)))/2;
	m_sdTCA7 = (SIM_DATA)((V(m_sdTC71, iCol)+V(m_sdTC72, iCol)))/2;
	m_sdTCA8 = (SIM_DATA)((V(m_sdTC81, iCol)+V(m_sdTC82, iCol)))/2;
	m_sdTCA9 = (SIM_DATA)((V(m_sdTC91, iCol)+V(m_sdTC92, iCol)))/2;
	m_sdTCA10 = (SIM_DATA)((V(m_sdTC101, iCol)+V(m_sdTC102, iCol)))/2;
	m_sdTCAL1 = (SIM_DATA)((m_sdTCA1+m_sdTCA2+m_sdTCA3+m_sdTCA4+m_sdTCA5+m_sdTCA6+m_sdTCA7+m_sdTCA8+m_sdTCA9+m_sdTCA10))/10;
	m_sdTLA1 = (SIM_DATA)((V(m_sdTL11, iCol)+V(m_sdTL12, iCol)))/2;
	m_sdTLA2 = (SIM_DATA)((V(m_sdTL21, iCol)+V(m_sdTL22, iCol)))/2;
	m_sdTLA3 = (SIM_DATA)((V(m_sdTL31, iCol)+V(m_sdTL32, iCol)))/2;
	m_sdTLA4 = (SIM_DATA)((V(m_sdTL41, iCol)+V(m_sdTL42, iCol)))/2;
	m_sdTLA5 = (SIM_DATA)((V(m_sdTL51, iCol)+V(m_sdTL52, iCol)))/2;
	m_sdTLA6 = (SIM_DATA)((V(m_sdTL61, iCol)+V(m_sdTL62, iCol)))/2;
	m_sdTLA7 = (SIM_DATA)((V(m_sdTL71, iCol)+V(m_sdTL72, iCol)))/2;
	m_sdTLA8 = (SIM_DATA)((V(m_sdTL81, iCol)+V(m_sdTL82, iCol)))/2;
	m_sdTLA9 = (SIM_DATA)((V(m_sdTL91, iCol)+V(m_sdTL92, iCol)))/2;
	m_sdTLA10 = (SIM_DATA)((V(m_sdTL101, iCol)+V(m_sdTL102, iCol)))/2;
	m_sdTLAL1 = (SIM_DATA)((m_sdTLA1+m_sdTLA2+m_sdTLA3+m_sdTLA4+m_sdTLA5+m_sdTLA6+m_sdTLA7+m_sdTLA8+m_sdTLA9+m_sdTLA10))/10;
	m_sdTKW1 = (0.001256*m_sdTCAL1+0.55588)*1e-06;
	m_sdTKF01 = (0.001256*m_sdTLAL1+0.55588)*1e-06;
	//TKF: Thermal conductivity of water at BL temperature
	m_sdTKF1 = (-0.00017*m_sdFC+m_sdTKF01*1e+06)*1e-06;
	//TKF: Thermal conductivity of fuel
	m_sdTHDIFF1 = (SIM_DATA)(m_sdTKF1)/(m_sdFuelDen*m_sdCV);
	m_sdbta1 = ((0.00019*pow((0.1*m_sdTLAL1),3))-(0.00624*pow((0.1*m_sdTLAL1),2))+0.12101*(0.1*m_sdTLAL1)-0.01837)*0.001;
	m_sddynvis1 = ((2.149*pow(10,(-8)))*(pow(m_sdFC,2))+(4.236*pow(10,(-6)))*m_sdFC+1.338*pow(10,(-3)))*exp(0.0185*m_sdTLAL1);
	m_sdTCA21 = (SIM_DATA)((V(m_sdTC211, iCol)+V(m_sdTC212, iCol)))/2;
	m_sdTCA22 = (SIM_DATA)((V(m_sdTC221, iCol)+V(m_sdTC222, iCol)))/2;
	m_sdTCA23 = (SIM_DATA)((V(m_sdTC231, iCol)+V(m_sdTC232, iCol)))/2;
	m_sdTCA24 = (SIM_DATA)((V(m_sdTC241, iCol)+V(m_sdTC242, iCol)))/2;
	m_sdTCA25 = (SIM_DATA)((V(m_sdTC251, iCol)+V(m_sdTC252, iCol)))/2;
	m_sdTCA26 = (SIM_DATA)((V(m_sdTC261, iCol)+V(m_sdTC262, iCol)))/2;
	m_sdTCA27 = (SIM_DATA)((V(m_sdTC271, iCol)+V(m_sdTC272, iCol)))/2;
	m_sdTCA28 = (SIM_DATA)((V(m_sdTC281, iCol)+V(m_sdTC282, iCol)))/2;
	m_sdTCA29 = (SIM_DATA)((V(m_sdTC291, iCol)+V(m_sdTC292, iCol)))/2;
	m_sdTCA210 = (SIM_DATA)((V(m_sdTC2101, iCol)+V(m_sdTC2102, iCol)))/2;
	m_sdTCAL2 = (SIM_DATA)((m_sdTCA21+m_sdTCA22+m_sdTCA23+m_sdTCA24+m_sdTCA25+m_sdTCA26+m_sdTCA27+m_sdTCA28+m_sdTCA29+m_sdTCA210))/10;
	m_sdTLA21 = (SIM_DATA)((V(m_sdTL211, iCol)+V(m_sdTL212, iCol)))/2;
	m_sdTLA22 = (SIM_DATA)((V(m_sdTL221, iCol)+V(m_sdTL222, iCol)))/2;
	m_sdTLA23 = (SIM_DATA)((V(m_sdTL231, iCol)+V(m_sdTL232, iCol)))/2;
	m_sdTLA24 = (SIM_DATA)((V(m_sdTL241, iCol)+V(m_sdTL242, iCol)))/2;
	m_sdTLA25 = (SIM_DATA)((V(m_sdTL251, iCol)+V(m_sdTL252, iCol)))/2;
	m_sdTLA26 = (SIM_DATA)((V(m_sdTL261, iCol)+V(m_sdTL262, iCol)))/2;
	m_sdTLA27 = (SIM_DATA)((V(m_sdTL271, iCol)+V(m_sdTL272, iCol)))/2;
	m_sdTLA28 = (SIM_DATA)((V(m_sdTL281, iCol)+V(m_sdTL282, iCol)))/2;
	m_sdTLA29 = (SIM_DATA)((V(m_sdTL291, iCol)+V(m_sdTL292, iCol)))/2;
	m_sdTLA210 = (SIM_DATA)((V(m_sdTL2101, iCol)+V(m_sdTL2102, iCol)))/2;
	m_sdTLAL2 = (SIM_DATA)((m_sdTLA21+m_sdTLA22+m_sdTLA23+m_sdTLA24+m_sdTLA25+m_sdTLA26+m_sdTLA27+m_sdTLA28+m_sdTLA29+m_sdTLA210))/10;
	m_sdTKW2 = (0.001256*m_sdTCAL2+0.55588)*1e-06;
	m_sdTKF02 = (0.001256*m_sdTLAL2+0.55588)*1e-06;
	//TKF: Thermal conductivity of water at BL temperature
	m_sdTKF2 = (-0.00017*m_sdFC+m_sdTKF02*1e+06)*1e-06;
	//TKF: Thermal conductivity of fuel
	m_sdTHDIFF2 = (SIM_DATA)(m_sdTKF2)/(m_sdFuelDen*m_sdCV);
	m_sdbta2 = ((0.00019*pow((0.1*m_sdTLAL2),3))-(0.00624*pow((0.1*m_sdTLAL2),2))+0.12101*(0.1*m_sdTLAL2)-0.01837)*0.001;
	m_sddynvis2 = ((2.149*pow(10,(-8)))*(pow(m_sdFC,2))+(4.236*pow(10,(-6)))*m_sdFC+1.338*pow(10,(-3)))*exp(0.0185*m_sdTLAL2);
	m_sdTCA31 = (SIM_DATA)((V(m_sdTC311, iCol)+V(m_sdTC312, iCol)))/2;
	m_sdTCA32 = (SIM_DATA)((V(m_sdTC321, iCol)+V(m_sdTC322, iCol)))/2;
	m_sdTCA33 = (SIM_DATA)((V(m_sdTC331, iCol)+V(m_sdTC332, iCol)))/2;
	m_sdTCA34 = (SIM_DATA)((V(m_sdTC341, iCol)+V(m_sdTC342, iCol)))/2;
	m_sdTCA35 = (SIM_DATA)((V(m_sdTC351, iCol)+V(m_sdTC352, iCol)))/2;
	m_sdTCA36 = (SIM_DATA)((V(m_sdTC361, iCol)+V(m_sdTC362, iCol)))/2;
	m_sdTCA37 = (SIM_DATA)((V(m_sdTC371, iCol)+V(m_sdTC372, iCol)))/2;
	m_sdTCA38 = (SIM_DATA)((V(m_sdTC381, iCol)+V(m_sdTC382, iCol)))/2;
	m_sdTCA39 = (SIM_DATA)((V(m_sdTC391, iCol)+V(m_sdTC392, iCol)))/2;
	m_sdTCA310 = (SIM_DATA)((V(m_sdTC3101, iCol)+V(m_sdTC3102, iCol)))/2;
	m_sdTCAL3 = (SIM_DATA)((m_sdTCA31+m_sdTCA32+m_sdTCA33+m_sdTCA34+m_sdTCA35+m_sdTCA36+m_sdTCA37+m_sdTCA38+m_sdTCA39+m_sdTCA310))/10;
	m_sdTLA31 = (SIM_DATA)((V(m_sdTL311, iCol)+V(m_sdTL312, iCol)))/2;
	m_sdTLA32 = (SIM_DATA)((V(m_sdTL321, iCol)+V(m_sdTL322, iCol)))/2;
	m_sdTLA33 = (SIM_DATA)((V(m_sdTL331, iCol)+V(m_sdTL332, iCol)))/2;
	m_sdTLA34 = (SIM_DATA)((V(m_sdTL341, iCol)+V(m_sdTL342, iCol)))/2;
	m_sdTLA35 = (SIM_DATA)((V(m_sdTL351, iCol)+V(m_sdTL352, iCol)))/2;
	m_sdTLA36 = (SIM_DATA)((V(m_sdTL361, iCol)+V(m_sdTL362, iCol)))/2;
	m_sdTLA37 = (SIM_DATA)((V(m_sdTL371, iCol)+V(m_sdTL372, iCol)))/2;
	m_sdTLA38 = (SIM_DATA)((V(m_sdTL381, iCol)+V(m_sdTL382, iCol)))/2;
	m_sdTLA39 = (SIM_DATA)((V(m_sdTL391, iCol)+V(m_sdTL392, iCol)))/2;
	m_sdTLA310 = (SIM_DATA)((V(m_sdTL3101, iCol)+V(m_sdTL3102, iCol)))/2;
	m_sdTLAL3 = (SIM_DATA)((m_sdTLA31+m_sdTLA32+m_sdTLA33+m_sdTLA34+m_sdTLA35+m_sdTLA36+m_sdTLA37+m_sdTLA38+m_sdTLA39+m_sdTLA310))/10;
	m_sdTKW3 = (0.001256*m_sdTCAL3+0.55588)*1e-06;
	m_sdTKF03 = (0.001256*m_sdTLAL3+0.55588)*1e-06;
	//TKF: Thermal conductivity of water at BL temperature
	m_sdTKF3 = (-0.00017*m_sdFC+m_sdTKF03*1e+06)*1e-06;
	//TKF: Thermal conductivity of fuel
	m_sdTHDIFF3 = (SIM_DATA)(m_sdTKF3)/(m_sdFuelDen*m_sdCV);
	m_sdbta3 = ((0.00019*pow((0.1*m_sdTLAL3),3))-(0.00624*pow((0.1*m_sdTLAL3),2))+0.12101*(0.1*m_sdTLAL3)-0.01837)*0.001;
	m_sddynvis3 = ((2.149*pow(10,(-8)))*(pow(m_sdFC,2))+(4.236*pow(10,(-6)))*m_sdFC+1.338*pow(10,(-3)))*exp(0.0185*m_sdTLAL3);
	m_sdALF1 = m_sdALF*m_sdI1;
	m_sdALF2 = m_sdALF*m_sdI2;
	m_sdALF3 = m_sdALF*m_sdI3;
	m_sdALF4 = m_sdALF*m_sdI4;
	m_sdALF5 = m_sdALF*m_sdI5;
	m_sdALF6 = m_sdALF*m_sdI6;
	m_sdALF7 = m_sdALF*m_sdI7;
	m_sdALF8 = m_sdALF*m_sdI8;
	m_sdALF9 = m_sdALF*m_sdI9;
	m_sdALF10 = m_sdALF*m_sdI10;
	//-- RFBA=Reactivity feedback due to fuel temperature ($)
	m_sdRFBA1 = m_sdALF1*(V(m_sdTEMP1, iCol)-m_sdT0)+m_sdALF2*(V(m_sdTEMP2, iCol)-m_sdT0)+m_sdALF3*(V(m_sdTEMP3, iCol)-m_sdT0)+m_sdALF4*(V(m_sdTEMP4, iCol)-m_sdT0);
	m_sdRFBA2 = m_sdALF5*(V(m_sdTEMP5, iCol)-m_sdT0)+m_sdALF6*(V(m_sdTEMP6, iCol)-m_sdT0)+m_sdALF7*(V(m_sdTEMP7, iCol)-m_sdT0)+m_sdALF8*(V(m_sdTEMP8, iCol)-m_sdT0);
	m_sdRFBA3 = m_sdALF9*(V(m_sdTEMP9, iCol)-m_sdT0)+m_sdALF10*(V(m_sdTEMP10, iCol)-m_sdT0);
	m_sdRFBA = m_sdRFBA1+m_sdRFBA2+m_sdRFBA3;
	//--
	m_sdVF1 = (SIM_DATA)((V(m_sdVS1, iCol)+V(m_sdVGH1, iCol)+V(m_sdVGO1, iCol)))/(V(m_sdVS1, iCol)+V(m_sdVGH1, iCol)+V(m_sdVGO1, iCol)+V(m_sdVFUEL1, iCol));
	m_sdVF2 = (SIM_DATA)((V(m_sdVS2, iCol)+V(m_sdVGH2, iCol)+V(m_sdVGO2, iCol)))/(V(m_sdVS2, iCol)+V(m_sdVGH2, iCol)+V(m_sdVGO2, iCol)+V(m_sdVFUEL2, iCol));
	m_sdVF3 = (SIM_DATA)((V(m_sdVS3, iCol)+V(m_sdVGH3, iCol)+V(m_sdVGO3, iCol)))/(V(m_sdVS3, iCol)+V(m_sdVGH3, iCol)+V(m_sdVGO3, iCol)+V(m_sdVFUEL3, iCol));
	m_sdVF4 = (SIM_DATA)((V(m_sdVS4, iCol)+V(m_sdVGH4, iCol)+V(m_sdVGO4, iCol)))/(V(m_sdVS4, iCol)+V(m_sdVGH4, iCol)+V(m_sdVGO4, iCol)+V(m_sdVFUEL4, iCol));
	m_sdVF5 = (SIM_DATA)((V(m_sdVS5, iCol)+V(m_sdVGH5, iCol)+V(m_sdVGO5, iCol)))/(V(m_sdVS5, iCol)+V(m_sdVGH5, iCol)+V(m_sdVGO5, iCol)+V(m_sdVFUEL5, iCol));
	m_sdVF6 = (SIM_DATA)((V(m_sdVS6, iCol)+V(m_sdVGH6, iCol)+V(m_sdVGO6, iCol)))/(V(m_sdVS6, iCol)+V(m_sdVGH6, iCol)+V(m_sdVGO6, iCol)+V(m_sdVFUEL6, iCol));
	m_sdVF7 = (SIM_DATA)((V(m_sdVS7, iCol)+V(m_sdVGH7, iCol)+V(m_sdVGO7, iCol)))/(V(m_sdVS7, iCol)+V(m_sdVGH7, iCol)+V(m_sdVGO7, iCol)+V(m_sdVFUEL7, iCol));
	m_sdVF8 = (SIM_DATA)((V(m_sdVS8, iCol)+V(m_sdVGH8, iCol)+V(m_sdVGO8, iCol)))/(V(m_sdVS8, iCol)+V(m_sdVGH8, iCol)+V(m_sdVGO8, iCol)+V(m_sdVFUEL8, iCol));
	m_sdVF9 = (SIM_DATA)((V(m_sdVS9, iCol)+V(m_sdVGH9, iCol)+V(m_sdVGO9, iCol)))/(V(m_sdVS9, iCol)+V(m_sdVGH9, iCol)+V(m_sdVGO9, iCol)+V(m_sdVFUEL9, iCol));
	m_sdVF10 = (SIM_DATA)((V(m_sdVS10, iCol)+V(m_sdVGH10, iCol)+V(m_sdVGO10, iCol)))/(V(m_sdVS10, iCol)+V(m_sdVGH10, iCol)+V(m_sdVGO10, iCol)+V(m_sdVFUEL10, iCol));
	//-- RFBV=Reactivity feedback due to gas ($)
	m_sdRFBV1 = m_sdPHI1*m_sdVF1+m_sdPHI2*m_sdVF2+m_sdPHI3*m_sdVF3+m_sdPHI4*m_sdVF4;
	m_sdRFBV2 = m_sdPHI5*m_sdVF5+m_sdPHI6*m_sdVF6+m_sdPHI7*m_sdVF7+m_sdPHI8*m_sdVF8;
	m_sdRFBV3 = m_sdPHI9*m_sdVF9+m_sdPHI10*m_sdVF10;
	m_sdRFBV = m_sdRFBV1+m_sdRFBV2+m_sdRFBV3;
	//--
	//-- *EQUATION 3
	//--
	//-- R=Reactivity of assembly ($)
	m_sdR = m_sdA-m_sdRFBA-m_sdRFBV+m_sdRAMP;
	//-- -----------------------------------------------------
	//-- Point Reactor Kinetics Model
	//-- ------------------------------------------------------
	//--
	//-- *EQUATION 1
	//--
	m_sdENP = m_sdEN0*V(m_sdEN, iCol);
	m_sdSUM = m_sdF1*V(m_sdD1, iCol)+m_sdF2*V(m_sdD2, iCol)+m_sdF3*V(m_sdD3, iCol)+m_sdF4*V(m_sdD4, iCol)+m_sdF5*V(m_sdD5, iCol)+m_sdF6*V(m_sdD6, iCol);
	//-- QS=Neutron Source (#/s)
	m_sdQS = m_sdQ0 + m_sdAccSS*swtch((SIM_DATA)(m_sdT)-m_sdAccOnTime)*swtch(m_sdAccOffTime - (SIM_DATA)(m_sdT)) + QS135mod(2 * m_sdPI*m_sdT, m_sdAccSS);
	m_sdENDOT = m_sdBOL*((m_sdR-1.0)*V(m_sdEN, iCol)+m_sdSUM)+(SIM_DATA)(m_sdQS)/m_sdEN0;
	m_sdENLOG = 0.4342945*log(m_sdENP);
	D(m_sdEN, m_sdENDOT, iCol);
	//-- Delayed Neutron Precursors
	//--  *EQUATION 2
	D(m_sdD1, m_sdAL1*(V(m_sdEN, iCol)-V(m_sdD1, iCol)), iCol);
	D(m_sdD2, m_sdAL2*(V(m_sdEN, iCol)-V(m_sdD2, iCol)), iCol);
	D(m_sdD3, m_sdAL3*(V(m_sdEN, iCol)-V(m_sdD3, iCol)), iCol);
	D(m_sdD4, m_sdAL4*(V(m_sdEN, iCol)-V(m_sdD4, iCol)), iCol);
	D(m_sdD5, m_sdAL5*(V(m_sdEN, iCol)-V(m_sdD5, iCol)), iCol);
	D(m_sdD6, m_sdAL6*(V(m_sdEN, iCol)-V(m_sdD6, iCol)), iCol);
	//--
	//-- ccccccccccccccccccccccccccccccccccccccccccccccccccccccccccc
	//--- Check neutron balance at steady state
	//-- -------------------------------------------------------------------------------------------------------------
	//--  Power conversion factor block
	//--  Convert neutron population to fission rate to power in MW
	//-- -------------------------------------------------------------------------------------------------------------
	//-- TEMP=Average Fuel Temperature (C)
	//-- Vav=Average speed of thermal neutrons (m/s)
	//-- Sav=Average microscopic fission X-section of thermal neutrons (cm^2)
	m_sdmXS = 582.0e-24;
	//mXS=U-235 microscopic fission XS at 293 K (cm^2)
	//--  FC=Fuel concentration (gU/L)
	//--  FND=U-235 number density (#/cm^3)
	//-- PCF=Power Conversion Factor-3.1e+16 fissions/s=1 MW
	//-- *EQUATION 5
	m_sdVav = pow((2*m_sdkB*(SIM_DATA)((273+m_sdTEMP))/m_sdnmass),0.5)*(SIM_DATA)(2)/pow(m_sdPI,0.5);
	//-- *EQUATION 6
	m_sdSav = m_sdmXS*(SIM_DATA)(pow(m_sdPI,0.5))/2*pow(((SIM_DATA)(293)/(273+m_sdTEMP)),0.5);
	m_sdFND = (SIM_DATA)(m_sdFC)/1.0e+03*(SIM_DATA)(m_sdFE)/m_sdUM*m_sdAN;
	//-- *EQUATION 4
	m_sdPCF = 1.50*m_sdVav*100.0*m_sdSav*(SIM_DATA)(m_sdFND)/3.1e+16;
	m_sdkw = (m_sdENP-m_sdEN0)*m_sdPCF*1.0e+03;
	//kw=Total assembly power (kw)
	D(m_sdETOT, m_sdPCF*(m_sdENP-m_sdEN0), iCol);
	//-- ********************************************
	//-- CORE THERMAL MODEL
	//-- fueltool
	//-- ********************************************
	//-- Fuel Temp and Fuel Volume bfz
	//--  Regions 1-10,1=bottom
	//-- ---------------------------------------------------
	//--   Bulk fuel zone
	//--  TEMP(i)=Temperature of fuel in region i (C)
	//-- VFUEL(i)=Volume of fuel in region i (m^3)
	m_sdWF1 = V(m_sdWF, iCol)*(SIM_DATA)(m_sdALOOP1)/m_sdALOOPT;
	m_sdWF2 = V(m_sdWF, iCol)*(SIM_DATA)(m_sdALOOP2)/m_sdALOOPT;
	m_sdWF3 = V(m_sdWF, iCol)*(SIM_DATA)(m_sdALOOP3)/m_sdALOOPT;
	m_sdWFF1 = (SIM_DATA)(m_sdALOOP1)/m_sdALOOPT;
	m_sdWFF2 = (SIM_DATA)(m_sdALOOP2)/m_sdALOOPT;
	m_sdWFF3 = (SIM_DATA)(m_sdALOOP3)/m_sdALOOPT;
	//--
	m_sdTWC = TWC135mod(m_sdTWC, m_sdDelT/4);
	m_sdWC = m_sdCoolHigh1*m_sdTWC*m_sdXSA1f*(0.01+swtch(m_sdCoolOffTime-(SIM_DATA)(m_sdT)));
	//flow in inner channel
	m_sdWC2 = m_sdCoolHigh2*m_sdTWC*m_sdXSATf*(0.01+swtch(m_sdCoolOffTime-(SIM_DATA)(m_sdT)));
	//flow
	m_sdWC3 = m_sdCoolHigh3*m_sdTWC*m_sdXSA2f*(0.01+swtch(m_sdCoolOffTime-(SIM_DATA)(m_sdT)));
	//flow in outer channel
	//-- -----------------------------------------------------------------------------------------------------------------------------------------------
	//-- Heat Transfer Coefficients
	//-- -----------------------------------------------------------------------------------------------------------------------------------------------
	m_sdVF = (SIM_DATA)((m_sdVF1+m_sdVF2+m_sdVF3+m_sdVF4+m_sdVF5+m_sdVF6+m_sdVF7+m_sdVF8+m_sdVF9+m_sdVF10))/10;
	m_sdTWA1 = (SIM_DATA)((V(m_sdTW1, iCol)+V(m_sdTW2, iCol)+V(m_sdTW3, iCol)+V(m_sdTW4, iCol)+V(m_sdTW5, iCol)+V(m_sdTW6, iCol)+V(m_sdTW7, iCol)+V(m_sdTW8, iCol)+V(m_sdTW9, iCol)+V(m_sdTW10, iCol)))/10;
	m_sdTHOT1 = m_sdTEMP;
	m_sdTCOLD1 = m_sdTWA1;
	m_sdFLDT = 1.0;
	m_sdDT1 = (SIM_DATA)(m_sdVF)/m_sdbta1*swtch(m_sdFLDT);
	m_sdLPr1 = m_sddynvis1*(SIM_DATA)(m_sdCV)/m_sdTKF1;
	//Prandtl number for Loop 1
	m_sdLRa1 = 9.81*m_sdbta1*(m_sdTHOT1+m_sdDT1-m_sdTCOLD1)*(SIM_DATA)((pow(m_sdHCORE,3)))/(m_sdTHDIFF1*m_sdkinvis1);
	m_sdLNu1 = pow((0.825+(SIM_DATA)(((0.387*(pow(m_sdLRa1,((SIM_DATA)(1)/6))))))/pow((1+(pow(((SIM_DATA)(0.492)/m_sdLPr1),((SIM_DATA)(9)/16)))),((SIM_DATA)(8)/27))),2);
	//Nusselt number for Loop 1
	m_sdHTNC1 = m_sdLNu1*(SIM_DATA)(m_sdTKF1)/m_sdHCORE;
	//heat transfer coefficient for Loop 1
	m_sdLRe1 = m_sdWC*(SIM_DATA)(((SIM_DATA)(m_sdHDia1)/m_sdXSA1))/m_sddynvis1;
	m_sdLe1 = 0.06*m_sdLRe1;
	//entrance Length
	m_sdLf1 = pow((0.790*log(m_sdLRe1)-0.164),(-2));
	m_sdLNuFf1 = ((SIM_DATA)(m_sdLf1)/8)*(m_sdLRe1-1000)*m_sdLPr1;
	m_sdLNuFf2 = 1+12.7*(pow(((SIM_DATA)(m_sdLf1)/8),0.5))*((pow(m_sdLPr1,((SIM_DATA)(2)/3)))-1);
	m_sdLNuF1 = swtch(m_sdLRe1-2300)*((SIM_DATA)(m_sdLNuFf1)/m_sdLNuFf2)*(1-pow(((SIM_DATA)(m_sdHDia1)/m_sdHCORE),((SIM_DATA)(2)/3))) + swtch(2300-m_sdLRe1)*1.86*pow(((SIM_DATA)((m_sdLRe1*m_sdLPr1))/((SIM_DATA)(m_sdHCORE)/m_sdHDia1)),((SIM_DATA)(1)/3));
	m_sdHT1 = m_sdLNuF1*(SIM_DATA)(m_sdTKW1)/m_sdHDia1;
	m_sdGMMA = m_sdSAL1F*m_sdHTNC1;
	m_sdGAMMA = m_sdGMMA;
	m_sdGAMMA9 = m_sdSAL1C*m_sdHT1;
	//--   ----------------------------------------------------------------------------------------------------------------------
	//-- 1
	//--
	m_sdST1 = swtch(m_sdTB-V(m_sdTEMP1, iCol));
	//-- *EQUATION 38
	m_sdTEM1DOT = m_sdST1*m_sdAK*m_sdPCF*m_sdFRA1*(m_sdENP-m_sdEN0)+(SIM_DATA)(V(m_sdWF, iCol))/m_sdEM*(m_sdWFF1*V(m_sdTL102, iCol)+m_sdWFF2*V(m_sdTL2102, iCol)+m_sdWFF3*V(m_sdTL3102, iCol)-V(m_sdTEMP1, iCol));
	D(m_sdTEMP1, m_sdTEM1DOT, iCol);
	//-- *EQUATIONS 26,46
	m_sdVFT1 = (SIM_DATA)(m_sdV0)/10*m_sdalpha*m_sdTEM1DOT;
	D(m_sdVFUEL1, m_sdVFT1, iCol);
	m_sdRF1 = (SIM_DATA)(m_sdEM)/V(m_sdVFUEL1, iCol)*(1-m_sdVF1);
	//-- TL(i)=Fuel Temperature blz (C)
	//-- *Equation 39
	m_sdTL11DOT = -m_sdAK2*0.5*m_sdBR1*m_sdGAMMA*(V(m_sdTL11, iCol)-V(m_sdTW1, iCol))+(SIM_DATA)(m_sdWF1)/m_sdELM*(V(m_sdTEMP10, iCol)-V(m_sdTL11, iCol));
	D(m_sdTL11, m_sdTL11DOT, iCol);
	m_sdVLT11 = (SIM_DATA)(m_sdVL)/20*m_sdalpha*m_sdTL11DOT;
	D(m_sdVL11, m_sdVLT11, iCol);
	//-- RL(i)=Fuel density blz (kg/m^3)
	//-- *Equation 47
	m_sdRL11 = (SIM_DATA)(m_sdELM)/V(m_sdVL11, iCol);
	//-- *Equation 40
	m_sdTL12DOT = -m_sdAK2*0.5*m_sdBR1*m_sdGAMMA*(V(m_sdTL11, iCol)-V(m_sdTW1, iCol))+(SIM_DATA)(m_sdWF1)/m_sdELM*(V(m_sdTL11, iCol)-V(m_sdTL12, iCol));
	D(m_sdTL12, m_sdTL12DOT, iCol);
	m_sdVLT12 = (SIM_DATA)(m_sdVL)/20*m_sdalpha*m_sdTL12DOT;
	D(m_sdVL12, m_sdVLT12, iCol);
	m_sdRL12 = (SIM_DATA)(m_sdELM)/V(m_sdVL12, iCol);
	m_sdMW = (SIM_DATA)(m_sdCC1M)/10;
	//MW=mass of WALL (kg)
	m_sdCPW = 0.285e-03;
	//CPW=specific heat of WALL (MJ/C/kg)
	//-- TW1=Temperature of WALL
	//-- *Equation 42
	D(m_sdTW1, (SIM_DATA)(1)/(SIM_DATA)(m_sdMW)/m_sdCPW*(m_sdBR1*m_sdGAMMA*(V(m_sdTL11, iCol)-V(m_sdTW1, iCol))-m_sdBR1*m_sdGAMMA9*(V(m_sdTW1, iCol)-V(m_sdTC101, iCol))), iCol);
	//-- -------------------------------------------------------------------------------------------------------------------------------------
	//-- Boundary Layer Zone
	//-- -------------------------------------------------------------------------------------------------------------------------------------
	//--  2
	m_sdTL21DOT = -m_sdAK2*0.5*m_sdBR1*m_sdGAMMA*(V(m_sdTL21, iCol)-V(m_sdTW2, iCol))+(SIM_DATA)(m_sdWF1)/m_sdELM*(V(m_sdTL12, iCol)-V(m_sdTL21, iCol));
	D(m_sdTL21, m_sdTL21DOT, iCol);
	m_sdVLT21 = (SIM_DATA)(m_sdVL)/20*m_sdalpha*m_sdTL21DOT;
	D(m_sdVL21, m_sdVLT21, iCol);
	m_sdRL21 = (SIM_DATA)(m_sdELM)/V(m_sdVL21, iCol);
	m_sdTL22DOT = -m_sdAK2*0.5*m_sdBR1*m_sdGAMMA*(V(m_sdTL21, iCol)-V(m_sdTW2, iCol))+(SIM_DATA)(m_sdWF1)/m_sdELM*(V(m_sdTL21, iCol)-V(m_sdTL22, iCol));
	D(m_sdTL22, m_sdTL22DOT, iCol);
	m_sdVLT22 = (SIM_DATA)(m_sdVL)/20*m_sdalpha*m_sdTL22DOT;
	D(m_sdVL22, m_sdVLT22, iCol);
	m_sdRL22 = (SIM_DATA)(m_sdELM)/V(m_sdVL22, iCol);
	D(m_sdTW2, (SIM_DATA)(1)/(SIM_DATA)(m_sdMW)/m_sdCPW*(m_sdBR1*m_sdGAMMA*(V(m_sdTL21, iCol)-V(m_sdTW2, iCol))-m_sdBR1*m_sdGAMMA9*(V(m_sdTW2, iCol)-V(m_sdTC91, iCol))), iCol);
	//--  3
	m_sdTL31DOT = -m_sdAK2*0.5*m_sdBR1*m_sdGAMMA*(V(m_sdTL31, iCol)-V(m_sdTW3, iCol))+(SIM_DATA)(m_sdWF1)/m_sdELM*(V(m_sdTL22, iCol)-V(m_sdTL31, iCol));
	D(m_sdTL31, m_sdTL31DOT, iCol);
	m_sdVLT31 = (SIM_DATA)(m_sdVL)/20*m_sdalpha*m_sdTL31DOT;
	D(m_sdVL31, m_sdVLT31, iCol);
	m_sdRL31 = (SIM_DATA)(m_sdELM)/V(m_sdVL31, iCol);
	m_sdTL32DOT = -m_sdAK2*0.5*m_sdBR1*m_sdGAMMA*(V(m_sdTL31, iCol)-V(m_sdTW3, iCol))+(SIM_DATA)(m_sdWF1)/m_sdELM*(V(m_sdTL31, iCol)-V(m_sdTL32, iCol));
	D(m_sdTL32, m_sdTL32DOT, iCol);
	m_sdVLT32 = (SIM_DATA)(m_sdVL)/20*m_sdalpha*m_sdTL32DOT;
	D(m_sdVL32, m_sdVLT32, iCol);
	m_sdRL32 = (SIM_DATA)(m_sdELM)/V(m_sdVL32, iCol);
	D(m_sdTW3, (SIM_DATA)(1)/(SIM_DATA)(m_sdMW)/m_sdCPW*(m_sdBR1*m_sdGAMMA*(V(m_sdTL31, iCol)-V(m_sdTW3, iCol))-m_sdBR1*m_sdGAMMA9*(V(m_sdTW3, iCol)-V(m_sdTC81, iCol))), iCol);
	//--  4
	m_sdTL41DOT = -m_sdAK2*0.5*m_sdBR1*m_sdGAMMA*(V(m_sdTL41, iCol)-V(m_sdTW4, iCol))+(SIM_DATA)(m_sdWF1)/m_sdELM*(V(m_sdTL32, iCol)-V(m_sdTL41, iCol));
	D(m_sdTL41, m_sdTL41DOT, iCol);
	m_sdVLT41 = (SIM_DATA)(m_sdVL)/20*m_sdalpha*m_sdTL41DOT;
	D(m_sdVL41, m_sdVLT41, iCol);
	m_sdRL41 = (SIM_DATA)(m_sdELM)/V(m_sdVL41, iCol);
	m_sdTL42DOT = -m_sdAK2*0.5*m_sdBR1*m_sdGAMMA*(V(m_sdTL41, iCol)-V(m_sdTW4, iCol))+(SIM_DATA)(m_sdWF1)/m_sdELM*(V(m_sdTL41, iCol)-V(m_sdTL42, iCol));
	D(m_sdTL42, m_sdTL42DOT, iCol);
	m_sdVLT42 = (SIM_DATA)(m_sdVL)/20*m_sdalpha*m_sdTL42DOT;
	D(m_sdVL42, m_sdVLT42, iCol);
	m_sdRL42 = (SIM_DATA)(m_sdELM)/V(m_sdVL42, iCol);
	D(m_sdTW4, (SIM_DATA)(1)/(SIM_DATA)(m_sdMW)/m_sdCPW*(m_sdBR1*m_sdGAMMA*(V(m_sdTL41, iCol)-V(m_sdTW4, iCol))-m_sdBR1*m_sdGAMMA9*(V(m_sdTW4, iCol)-V(m_sdTC71, iCol))), iCol);
	//--  5
	m_sdTL51DOT = -m_sdAK2*0.5*m_sdBR1*m_sdGAMMA*(V(m_sdTL51, iCol)-V(m_sdTW5, iCol))+(SIM_DATA)(m_sdWF1)/m_sdELM*(V(m_sdTL42, iCol)-V(m_sdTL51, iCol));
	D(m_sdTL51, m_sdTL51DOT, iCol);
	m_sdVLT51 = (SIM_DATA)(m_sdVL)/20*m_sdalpha*m_sdTL51DOT;
	D(m_sdVL51, m_sdVLT51, iCol);
	m_sdRL51 = (SIM_DATA)(m_sdELM)/V(m_sdVL51, iCol);
	m_sdTL52DOT = -m_sdAK2*0.5*m_sdBR1*m_sdGAMMA*(V(m_sdTL51, iCol)-V(m_sdTW5, iCol))+(SIM_DATA)(m_sdWF1)/m_sdELM*(V(m_sdTL51, iCol)-V(m_sdTL52, iCol));
	D(m_sdTL52, m_sdTL52DOT, iCol);
	m_sdVLT52 = (SIM_DATA)(m_sdVL)/20*m_sdalpha*m_sdTL52DOT;
	D(m_sdVL52, m_sdVLT52, iCol);
	m_sdRL52 = (SIM_DATA)(m_sdELM)/V(m_sdVL52, iCol);
	D(m_sdTW5, (SIM_DATA)(1)/(SIM_DATA)(m_sdMW)/m_sdCPW*(m_sdBR1*m_sdGAMMA*(V(m_sdTL51, iCol)-V(m_sdTW5, iCol))-m_sdBR1*m_sdGAMMA9*(V(m_sdTW5, iCol)-V(m_sdTC61, iCol))), iCol);
	//--  6
	m_sdTL61DOT = -m_sdAK2*0.5*m_sdBR1*m_sdGAMMA*(V(m_sdTL61, iCol)-V(m_sdTW6, iCol))+(SIM_DATA)(m_sdWF1)/m_sdELM*(V(m_sdTL52, iCol)-V(m_sdTL61, iCol));
	D(m_sdTL61, m_sdTL61DOT, iCol);
	m_sdVLT61 = (SIM_DATA)(m_sdVL)/20*m_sdalpha*m_sdTL61DOT;
	D(m_sdVL61, m_sdVLT61, iCol);
	m_sdRL61 = (SIM_DATA)(m_sdELM)/V(m_sdVL61, iCol);
	m_sdTL62DOT = -m_sdAK2*0.5*m_sdBR1*m_sdGAMMA*(V(m_sdTL61, iCol)-V(m_sdTW6, iCol))+(SIM_DATA)(m_sdWF1)/m_sdELM*(V(m_sdTL61, iCol)-V(m_sdTL62, iCol));
	D(m_sdTL62, m_sdTL62DOT, iCol);
	m_sdVLT62 = (SIM_DATA)(m_sdVL)/20*m_sdalpha*m_sdTL62DOT;
	D(m_sdVL62, m_sdVLT62, iCol);
	m_sdRL62 = (SIM_DATA)(m_sdELM)/V(m_sdVL62, iCol);
	D(m_sdTW6, (SIM_DATA)(1)/(SIM_DATA)(m_sdMW)/m_sdCPW*(m_sdBR1*m_sdGAMMA*(V(m_sdTL61, iCol)-V(m_sdTW6, iCol))-m_sdBR1*m_sdGAMMA9*(V(m_sdTW6, iCol)-V(m_sdTC51, iCol))), iCol);
	//--  7
	m_sdTL71DOT = -m_sdAK2*0.5*m_sdBR1*m_sdGAMMA*(V(m_sdTL71, iCol)-V(m_sdTW7, iCol))+(SIM_DATA)(m_sdWF1)/m_sdELM*(V(m_sdTL62, iCol)-V(m_sdTL71, iCol));
	D(m_sdTL71, m_sdTL71DOT, iCol);
	m_sdVLT71 = (SIM_DATA)(m_sdVL)/20*m_sdalpha*m_sdTL71DOT;
	D(m_sdVL71, m_sdVLT71, iCol);
	m_sdRL71 = (SIM_DATA)(m_sdELM)/V(m_sdVL71, iCol);
	m_sdTL72DOT = -m_sdAK2*0.5*m_sdBR1*m_sdGAMMA*(V(m_sdTL71, iCol)-V(m_sdTW7, iCol))+(SIM_DATA)(m_sdWF1)/m_sdELM*(V(m_sdTL71, iCol)-V(m_sdTL72, iCol));
	D(m_sdTL72, m_sdTL72DOT, iCol);
	m_sdVLT72 = (SIM_DATA)(m_sdVL)/20*m_sdalpha*m_sdTL72DOT;
	D(m_sdVL72, m_sdVLT72, iCol);
	m_sdRL72 = (SIM_DATA)(m_sdELM)/V(m_sdVL72, iCol);
	//-- TW7=Temperature of WALL
	D(m_sdTW7, (SIM_DATA)(1)/(SIM_DATA)(m_sdMW)/m_sdCPW*(m_sdBR1*m_sdGAMMA*(V(m_sdTL71, iCol)-V(m_sdTW7, iCol))-m_sdBR1*m_sdGAMMA9*(V(m_sdTW7, iCol)-V(m_sdTC41, iCol))), iCol);
	//--  8
	m_sdTL81DOT = -m_sdAK2*0.5*m_sdBR1*m_sdGAMMA*(V(m_sdTL81, iCol)-V(m_sdTW8, iCol))+(SIM_DATA)(m_sdWF1)/m_sdELM*(V(m_sdTL72, iCol)-V(m_sdTL81, iCol));
	D(m_sdTL81, m_sdTL81DOT, iCol);
	m_sdVLT81 = (SIM_DATA)(m_sdVL)/20*m_sdalpha*m_sdTL81DOT;
	D(m_sdVL81, m_sdVLT81, iCol);
	m_sdRL81 = (SIM_DATA)(m_sdELM)/V(m_sdVL81, iCol);
	m_sdTL82DOT = -m_sdAK2*0.5*m_sdBR1*m_sdGAMMA*(V(m_sdTL81, iCol)-V(m_sdTW8, iCol))+(SIM_DATA)(m_sdWF1)/m_sdELM*(V(m_sdTL81, iCol)-V(m_sdTL82, iCol));
	D(m_sdTL82, m_sdTL82DOT, iCol);
	m_sdVLT82 = (SIM_DATA)(m_sdVL)/20*m_sdalpha*m_sdTL82DOT;
	D(m_sdVL82, m_sdVLT82, iCol);
	m_sdRL82 = (SIM_DATA)(m_sdELM)/V(m_sdVL82, iCol);
	D(m_sdTW8, (SIM_DATA)(1)/(SIM_DATA)(m_sdMW)/m_sdCPW*(m_sdBR1*m_sdGAMMA*(V(m_sdTL81, iCol)-V(m_sdTW8, iCol))-m_sdBR1*m_sdGAMMA9*(V(m_sdTW8, iCol)-V(m_sdTC31, iCol))), iCol);
	//--  9
	m_sdTL91DOT = -m_sdAK2*0.5*m_sdBR1*m_sdGAMMA*(V(m_sdTL91, iCol)-V(m_sdTW9, iCol))+(SIM_DATA)(m_sdWF1)/m_sdELM*(V(m_sdTL82, iCol)-V(m_sdTL91, iCol));
	D(m_sdTL91, m_sdTL91DOT, iCol);
	m_sdVLT91 = (SIM_DATA)(m_sdVL)/20*m_sdalpha*m_sdTL91DOT;
	D(m_sdVL91, m_sdVLT91, iCol);
	m_sdRL91 = (SIM_DATA)(m_sdELM)/V(m_sdVL91, iCol);
	m_sdTL92DOT = -m_sdAK2*0.5*m_sdBR1*m_sdGAMMA*(V(m_sdTL91, iCol)-V(m_sdTW9, iCol))+(SIM_DATA)(m_sdWF1)/m_sdELM*(V(m_sdTL91, iCol)-V(m_sdTL92, iCol));
	D(m_sdTL92, m_sdTL92DOT, iCol);
	m_sdVLT92 = (SIM_DATA)(m_sdVL)/20*m_sdalpha*m_sdTL92DOT;
	D(m_sdVL92, m_sdVLT92, iCol);
	m_sdRL92 = (SIM_DATA)(m_sdELM)/V(m_sdVL92, iCol);
	D(m_sdTW9, (SIM_DATA)(1)/(SIM_DATA)(m_sdMW)/m_sdCPW*(m_sdBR1*m_sdGAMMA*(V(m_sdTL91, iCol)-V(m_sdTW9, iCol))-m_sdBR1*m_sdGAMMA9*(V(m_sdTW9, iCol)-V(m_sdTC21, iCol))), iCol);
	//--  10
	m_sdTL101DOT = -m_sdAK2*0.5*m_sdBR1*m_sdGAMMA*(V(m_sdTL101, iCol)-V(m_sdTW10, iCol))+(SIM_DATA)(m_sdWF1)/m_sdELM*(V(m_sdTL92, iCol)-V(m_sdTL101, iCol));
	D(m_sdTL101, m_sdTL101DOT, iCol);
	m_sdVLT101 = (SIM_DATA)(m_sdVL)/20*m_sdalpha*m_sdTL101DOT;
	D(m_sdVL101, m_sdVLT101, iCol);
	m_sdRL101 = (SIM_DATA)(m_sdELM)/V(m_sdVL101, iCol);
	m_sdTL102DOT = -m_sdAK2*0.5*m_sdBR1*m_sdGAMMA*(V(m_sdTL101, iCol)-V(m_sdTW10, iCol))+(SIM_DATA)(m_sdWF1)/m_sdELM*(V(m_sdTL101, iCol)-V(m_sdTL102, iCol));
	D(m_sdTL102, m_sdTL102DOT, iCol);
	m_sdVLT102 = (SIM_DATA)(m_sdVL)/20*m_sdalpha*m_sdTL102DOT;
	D(m_sdVL102, m_sdVLT102, iCol);
	m_sdRL102 = (SIM_DATA)(m_sdELM)/V(m_sdVL102, iCol);
	D(m_sdTW10, (SIM_DATA)(1)/(SIM_DATA)(m_sdMW)/m_sdCPW*(m_sdBR1*m_sdGAMMA*(V(m_sdTL101, iCol)-V(m_sdTW10, iCol))-m_sdBR1*m_sdGAMMA9*(V(m_sdTW10, iCol)-V(m_sdTC11, iCol))), iCol);
	//-- --------------------------------------------------------------------------------------------------------------------------------------------------------------
	//--  Bulk Fuel Zone
	//-- ---------------------------------------------------------------------------------------------------------------------------------------------------------------
	//-- 2
	m_sdST2 = swtch(m_sdTB-V(m_sdTEMP2, iCol));
	m_sdTEM2DOT = m_sdST2*m_sdAK*m_sdPCF*m_sdFRA2*(m_sdENP-m_sdEN0)+(SIM_DATA)(V(m_sdWF, iCol))/m_sdEM*(V(m_sdTEMP1, iCol)-V(m_sdTEMP2, iCol));
	D(m_sdTEMP2, m_sdTEM2DOT, iCol);
	m_sdVFT2 = (SIM_DATA)(m_sdV0)/10*m_sdalpha*m_sdTEM2DOT;
	D(m_sdVFUEL2, m_sdVFT2, iCol);
	m_sdRF2 = (SIM_DATA)(m_sdEM)/V(m_sdVFUEL2, iCol)*(1-m_sdVF2);
	//-- 3
	m_sdST3 = swtch(m_sdTB-V(m_sdTEMP3, iCol));
	m_sdTEM3DOT = m_sdST3*m_sdAK*m_sdPCF*m_sdFRA3*(m_sdENP-m_sdEN0)+(SIM_DATA)(V(m_sdWF, iCol))/m_sdEM*(V(m_sdTEMP2, iCol)-V(m_sdTEMP3, iCol));
	D(m_sdTEMP3, m_sdTEM3DOT, iCol);
	m_sdVFT3 = (SIM_DATA)(m_sdV0)/10*m_sdalpha*m_sdTEM3DOT;
	D(m_sdVFUEL3, m_sdVFT3, iCol);
	m_sdRF3 = (SIM_DATA)(m_sdEM)/V(m_sdVFUEL3, iCol)*(1-m_sdVF3);
	//-- 4
	m_sdST4 = swtch(m_sdTB-V(m_sdTEMP4, iCol));
	m_sdTEM4DOT = m_sdST4*m_sdAK*m_sdPCF*m_sdFRA4*(m_sdENP-m_sdEN0)+(SIM_DATA)(V(m_sdWF, iCol))/m_sdEM*(V(m_sdTEMP3, iCol)-V(m_sdTEMP4, iCol));
	D(m_sdTEMP4, m_sdTEM4DOT, iCol);
	m_sdVFT4 = (SIM_DATA)(m_sdV0)/10*m_sdalpha*m_sdTEM4DOT;
	D(m_sdVFUEL4, m_sdVFT4, iCol);
	m_sdRF4 = (SIM_DATA)(m_sdEM)/V(m_sdVFUEL4, iCol)*(1-m_sdVF4);
	//-- 5
	m_sdST5 = swtch(m_sdTB-V(m_sdTEMP5, iCol));
	m_sdTEM5DOT = m_sdST5*m_sdAK*m_sdPCF*m_sdFRA5*(m_sdENP-m_sdEN0)+(SIM_DATA)(V(m_sdWF, iCol))/m_sdEM*(V(m_sdTEMP4, iCol)-V(m_sdTEMP5, iCol));
	D(m_sdTEMP5, m_sdTEM5DOT, iCol);
	m_sdVFT5 = (SIM_DATA)(m_sdV0)/10*m_sdalpha*m_sdTEM5DOT;
	D(m_sdVFUEL5, m_sdVFT5, iCol);
	m_sdRF5 = (SIM_DATA)(m_sdEM)/V(m_sdVFUEL5, iCol)*(1-m_sdVF5);
	//-- 6
	m_sdST6 = swtch(m_sdTB-V(m_sdTEMP6, iCol));
	m_sdTEM6DOT = m_sdST6*m_sdAK*m_sdPCF*m_sdFRA6*(m_sdENP-m_sdEN0)+(SIM_DATA)(V(m_sdWF, iCol))/m_sdEM*(V(m_sdTEMP5, iCol)-V(m_sdTEMP6, iCol));
	D(m_sdTEMP6, m_sdTEM6DOT, iCol);
	m_sdVFT6 = (SIM_DATA)(m_sdV0)/10*m_sdalpha*m_sdTEM6DOT;
	D(m_sdVFUEL6, m_sdVFT6, iCol);
	m_sdRF6 = (SIM_DATA)(m_sdEM)/V(m_sdVFUEL6, iCol)*(1-m_sdVF6);
	//-- 7
	m_sdST7 = swtch(m_sdTB-V(m_sdTEMP7, iCol));
	m_sdTEM7DOT = m_sdST7*m_sdAK*m_sdPCF*m_sdFRA7*(m_sdENP-m_sdEN0)+(SIM_DATA)(V(m_sdWF, iCol))/m_sdEM*(V(m_sdTEMP6, iCol)-V(m_sdTEMP7, iCol));
	D(m_sdTEMP7, m_sdTEM7DOT, iCol);
	m_sdVFT7 = (SIM_DATA)(m_sdV0)/10*m_sdalpha*m_sdTEM7DOT;
	D(m_sdVFUEL7, m_sdVFT7, iCol);
	m_sdRF7 = (SIM_DATA)(m_sdEM)/V(m_sdVFUEL7, iCol)*(1-m_sdVF7);
	//-- 8
	m_sdST8 = swtch(m_sdTB-V(m_sdTEMP8, iCol));
	m_sdTEM8DOT = m_sdST8*m_sdAK*m_sdPCF*m_sdFRA8*(m_sdENP-m_sdEN0)+(SIM_DATA)(V(m_sdWF, iCol))/m_sdEM*(V(m_sdTEMP7, iCol)-V(m_sdTEMP8, iCol));
	D(m_sdTEMP8, m_sdTEM8DOT, iCol);
	m_sdVFT8 = (SIM_DATA)(m_sdV0)/10*m_sdalpha*m_sdTEM8DOT;
	D(m_sdVFUEL8, m_sdVFT8, iCol);
	m_sdRF8 = (SIM_DATA)(m_sdEM)/V(m_sdVFUEL8, iCol)*(1-m_sdVF8);
	//-- 9
	m_sdST9 = swtch(m_sdTB-V(m_sdTEMP9, iCol));
	m_sdTEM9DOT = m_sdST9*m_sdAK*m_sdPCF*m_sdFRA9*(m_sdENP-m_sdEN0)+(SIM_DATA)(V(m_sdWF, iCol))/m_sdEM*(V(m_sdTEMP8, iCol)-V(m_sdTEMP9, iCol));
	D(m_sdTEMP9, m_sdTEM9DOT, iCol);
	m_sdVFT9 = (SIM_DATA)(m_sdV0)/10*m_sdalpha*m_sdTEM9DOT;
	D(m_sdVFUEL9, m_sdVFT9, iCol);
	m_sdRF9 = (SIM_DATA)(m_sdEM)/V(m_sdVFUEL9, iCol)*(1-m_sdVF9);
	//-- 10
	m_sdST10 = swtch(m_sdTB-V(m_sdTEMP10, iCol));
	m_sdTEM10DT = m_sdST10*m_sdAK*m_sdPCF*m_sdFRA10*(m_sdENP-m_sdEN0)+(SIM_DATA)(V(m_sdWF, iCol))/m_sdEM*(V(m_sdTEMP9, iCol)-V(m_sdTEMP10, iCol));
	D(m_sdTEMP10, m_sdTEM10DT, iCol);
	m_sdVFT10 = (SIM_DATA)(m_sdV0)/10*m_sdalpha*m_sdTEM10DT;
	D(m_sdVFUEL10, m_sdVFT10, iCol);
	m_sdRF10 = (SIM_DATA)(m_sdEM)/V(m_sdVFUEL10, iCol)*(1-m_sdVF10);
	//-- ====================================================
	//-- Buoyancy
	//-- ====================================================
	//-- *Equation 44
	//-- VFUEL=Total Fuel Volume (m^3)
	//--  VFT=Time rate of change of total fuel volume
	m_sdVFUEL = V(m_sdVFUEL1, iCol)+V(m_sdVFUEL2, iCol)+V(m_sdVFUEL3, iCol)+V(m_sdVFUEL4, iCol)+V(m_sdVFUEL5, iCol)+V(m_sdVFUEL6, iCol)+V(m_sdVFUEL7, iCol)+V(m_sdVFUEL8, iCol)+V(m_sdVFUEL9, iCol)+V(m_sdVFUEL10, iCol);
	m_sdVFT = m_sdVFT1+m_sdVFT2+m_sdVFT3+m_sdVFT4+m_sdVFT5+m_sdVFT6+m_sdVFT7+m_sdVFT8+m_sdVFT9+m_sdVFT10;
	//-- ----------------------------------------------------------------------------------------------------------------
	//-- Steam model
	//-- ---------------------------------------------------------------------------------------------------------------
	m_sdP = V(m_sdPN, iCol)+V(m_sdPH, iCol)+V(m_sdPO, iCol);
	//Plenum/Fuel Pressure (Pa)
	//-- 10
	m_sdSS10 = swtch(V(m_sdTEMP10, iCol)-m_sdTB);
	m_sdVS10DOT = m_sdSS10*m_sdFRA10*(SIM_DATA)((m_sdENP-m_sdEN0))/m_sdHfg*m_sdsvg-(V(m_sdVS10, iCol)-V(m_sdVS9, iCol))*(SIM_DATA)(1)/m_sdTAUS-(SIM_DATA)(m_sdPDOT)/m_sdP*V(m_sdVS10, iCol);
	D(m_sdVS10, m_sdVS10DOT, iCol);
	//-- 9
	m_sdSS9 = swtch(V(m_sdTEMP9, iCol)-m_sdTB);
	m_sdVS9DOT = m_sdSS9*m_sdFRA9*(SIM_DATA)((m_sdENP-m_sdEN0))/m_sdHfg*m_sdsvg-(V(m_sdVS9, iCol)-V(m_sdVS8, iCol))*(SIM_DATA)(1)/m_sdTAUS-(SIM_DATA)(m_sdPDOT)/m_sdP*V(m_sdVS9, iCol);
	D(m_sdVS9, m_sdVS9DOT, iCol);
	//-- 8
	m_sdSS8 = swtch(V(m_sdTEMP8, iCol)-m_sdTB);
	m_sdVS8DOT = m_sdSS8*m_sdFRA8*(SIM_DATA)((m_sdENP-m_sdEN0))/m_sdHfg*m_sdsvg-(V(m_sdVS8, iCol)-V(m_sdVS7, iCol))*(SIM_DATA)(1)/m_sdTAUS-(SIM_DATA)(m_sdPDOT)/m_sdP*V(m_sdVS8, iCol);
	D(m_sdVS8, m_sdVS8DOT, iCol);
	//-- 7
	m_sdSS7 = swtch(V(m_sdTEMP7, iCol)-m_sdTB);
	m_sdVS7DOT = m_sdSS7*m_sdFRA7*(SIM_DATA)((m_sdENP-m_sdEN0))/m_sdHfg*m_sdsvg-(V(m_sdVS7, iCol)-V(m_sdVS6, iCol))*(SIM_DATA)(1)/m_sdTAUS-(SIM_DATA)(m_sdPDOT)/m_sdP*V(m_sdVS7, iCol);
	D(m_sdVS7, m_sdVS7DOT, iCol);
	//-- 6
	m_sdSS6 = swtch(V(m_sdTEMP6, iCol)-m_sdTB);
	m_sdVS6DOT = m_sdSS6*m_sdFRA6*(SIM_DATA)((m_sdENP-m_sdEN0))/m_sdHfg*m_sdsvg-(V(m_sdVS6, iCol)-V(m_sdVS5, iCol))*(SIM_DATA)(1)/m_sdTAUS-(SIM_DATA)(m_sdPDOT)/m_sdP*V(m_sdVS6, iCol);
	D(m_sdVS6, m_sdVS6DOT, iCol);
	//-- 5
	m_sdSS5 = swtch(V(m_sdTEMP5, iCol)-m_sdTB);
	m_sdVS5DOT = m_sdSS5*m_sdFRA5*(SIM_DATA)((m_sdENP-m_sdEN0))/m_sdHfg*m_sdsvg-(V(m_sdVS5, iCol)-V(m_sdVS4, iCol))*(SIM_DATA)(1)/m_sdTAUS-(SIM_DATA)(m_sdPDOT)/m_sdP*V(m_sdVS5, iCol);
	D(m_sdVS5, m_sdVS5DOT, iCol);
	//-- 4
	m_sdSS4 = swtch(V(m_sdTEMP4, iCol)-m_sdTB);
	m_sdVS4DOT = m_sdSS4*m_sdFRA4*(SIM_DATA)((m_sdENP-m_sdEN0))/m_sdHfg*m_sdsvg-(V(m_sdVS4, iCol)-V(m_sdVS3, iCol))*(SIM_DATA)(1)/m_sdTAUS-(SIM_DATA)(m_sdPDOT)/m_sdP*V(m_sdVS4, iCol);
	D(m_sdVS4, m_sdVS4DOT, iCol);
	//-- 3
	m_sdSS3 = swtch(V(m_sdTEMP3, iCol)-m_sdTB);
	m_sdVS3DOT = m_sdSS3*m_sdFRA3*(SIM_DATA)((m_sdENP-m_sdEN0))/m_sdHfg*m_sdsvg-(V(m_sdVS3, iCol)-V(m_sdVS2, iCol))*(SIM_DATA)(1)/m_sdTAUS-(SIM_DATA)(m_sdPDOT)/m_sdP*V(m_sdVS3, iCol);
	D(m_sdVS3, m_sdVS3DOT, iCol);
	//-- 2
	m_sdSS2 = swtch(V(m_sdTEMP2, iCol)-m_sdTB);
	m_sdVS2DOT = m_sdSS2*m_sdFRA2*(SIM_DATA)((m_sdENP-m_sdEN0))/m_sdHfg*m_sdsvg-(V(m_sdVS2, iCol)-V(m_sdVS1, iCol))*(SIM_DATA)(1)/m_sdTAUS-(SIM_DATA)(m_sdPDOT)/m_sdP*V(m_sdVS2, iCol);
	D(m_sdVS2, m_sdVS2DOT, iCol);
	//-- 1
	m_sdSS1 = swtch(V(m_sdTEMP1, iCol)-m_sdTB);
	m_sdVS1DOT = m_sdSS1*m_sdFRA1*(SIM_DATA)((m_sdENP-m_sdEN0))/m_sdHfg*m_sdsvg-(V(m_sdVS1, iCol))*(SIM_DATA)(1)/m_sdTAUS-(SIM_DATA)(m_sdPDOT)/m_sdP*V(m_sdVS1, iCol);
	D(m_sdVS1, m_sdVS1DOT, iCol);
	//-- =====================
	//-- Coolant Loop
	//--
	//-- looptool
	//-- ---------------------------------------------------------------------------
	//-- Nodes 1-10, 1=bottom
	//-- ---------------------------------------------------------------------------
	m_sdMC11 = m_sdMCLUMP;
	//MC11=Mass of Node 1 lump 1 (kg)
	m_sdMC12 = m_sdMCLUMP;
	//MC12=Mass of Node 1 lump 2 (kg)
	m_sdCPC = 4.18e-03;
	//CPC=Specific heat of coolant Node 1 (MJ/C/kg)
	m_sdFR11 = 0.5;
	//FR11=Fraction of Region 1 power going into lump 1
	m_sdFR12 = 0.5;
	//FR12=Fraction of Region 1 power going into lump2
	//-- *EQUATION 50
	m_sdTAUC11 = (SIM_DATA)(m_sdMC11)/m_sdWC;
	//TAUC11=Transit time Node 1 lump 1 (s)
	m_sdTAUC12 = (SIM_DATA)(m_sdMC12)/m_sdWC;
	//TAUC12=Transit time Node 1 lump 2 (s)
	//-- TC11=Temperature Node 1, lump 1
	//-- *EQUATION 48
	D(m_sdTC11, (SIM_DATA)(1)/(SIM_DATA)(m_sdMC11)/m_sdCPC*m_sdFR11*m_sdBR1*m_sdGAMMA9*(V(m_sdTW10, iCol)-V(m_sdTC11, iCol))+(SIM_DATA)(1)/m_sdTAUC11*(m_sdTC1IN-V(m_sdTC11, iCol)), iCol);
	//-- TC12=Temperature Node 1, lump 2
	//-- *EQUATION 49
	D(m_sdTC12, (SIM_DATA)(1)/(SIM_DATA)(m_sdMC12)/m_sdCPC*m_sdFR12*m_sdBR1*m_sdGAMMA9*(V(m_sdTW10, iCol)-V(m_sdTC11, iCol))+(SIM_DATA)(1)/m_sdTAUC12*(V(m_sdTC11, iCol)-V(m_sdTC12, iCol)), iCol);
	m_sdMC21 = m_sdMCLUMP;
	m_sdMC22 = m_sdMCLUMP;
	m_sdFR21 = 0.5;
	m_sdFR22 = 0.5;
	m_sdTAUC21 = (SIM_DATA)(m_sdMC21)/m_sdWC;
	m_sdTAUC22 = (SIM_DATA)(m_sdMC22)/m_sdWC;
	D(m_sdTC21, (SIM_DATA)(1)/(SIM_DATA)(m_sdMC21)/m_sdCPC*m_sdFR21*m_sdBR2*m_sdGAMMA9*(V(m_sdTW9, iCol)-V(m_sdTC21, iCol))+(SIM_DATA)(1)/m_sdTAUC21*(V(m_sdTC12, iCol)-V(m_sdTC21, iCol)), iCol);
	D(m_sdTC22, (SIM_DATA)(1)/(SIM_DATA)(m_sdMC22)/m_sdCPC*m_sdFR22*m_sdBR2*m_sdGAMMA9*(V(m_sdTW9, iCol)-V(m_sdTC21, iCol))+(SIM_DATA)(1)/m_sdTAUC22*(V(m_sdTC21, iCol)-V(m_sdTC22, iCol)), iCol);
	m_sdMC31 = m_sdMCLUMP;
	m_sdMC32 = m_sdMCLUMP;
	m_sdFR31 = 0.5;
	m_sdFR32 = 0.5;
	m_sdTAUC31 = (SIM_DATA)(m_sdMC31)/m_sdWC;
	m_sdTAUC32 = (SIM_DATA)(m_sdMC32)/m_sdWC;
	D(m_sdTC31, (SIM_DATA)(1)/(SIM_DATA)(m_sdMC31)/m_sdCPC*m_sdFR31*m_sdBR3*m_sdGAMMA9*(V(m_sdTW8, iCol)-V(m_sdTC31, iCol))+(SIM_DATA)(1)/m_sdTAUC31*(V(m_sdTC22, iCol)-V(m_sdTC31, iCol)), iCol);
	D(m_sdTC32, (SIM_DATA)(1)/(SIM_DATA)(m_sdMC32)/m_sdCPC*m_sdFR32*m_sdBR3*m_sdGAMMA9*(V(m_sdTW8, iCol)-V(m_sdTC31, iCol))+(SIM_DATA)(1)/m_sdTAUC32*(V(m_sdTC31, iCol)-V(m_sdTC32, iCol)), iCol);
	m_sdMC41 = m_sdMCLUMP;
	m_sdMC42 = m_sdMCLUMP;
	m_sdFR41 = 0.5;
	m_sdFR42 = 0.5;
	m_sdTAUC41 = (SIM_DATA)(m_sdMC41)/m_sdWC;
	m_sdTAUC42 = (SIM_DATA)(m_sdMC42)/m_sdWC;
	D(m_sdTC41, (SIM_DATA)(1)/(SIM_DATA)(m_sdMC41)/m_sdCPC*m_sdFR41*m_sdBR4*m_sdGAMMA9*(V(m_sdTW7, iCol)-V(m_sdTC41, iCol))+(SIM_DATA)(1)/m_sdTAUC41*(V(m_sdTC32, iCol)-V(m_sdTC41, iCol)), iCol);
	D(m_sdTC42, (SIM_DATA)(1)/(SIM_DATA)(m_sdMC42)/m_sdCPC*m_sdFR42*m_sdBR4*m_sdGAMMA9*(V(m_sdTW7, iCol)-V(m_sdTC41, iCol))+(SIM_DATA)(1)/m_sdTAUC42*(V(m_sdTC41, iCol)-V(m_sdTC42, iCol)), iCol);
	m_sdMC51 = m_sdMCLUMP;
	m_sdMC52 = m_sdMCLUMP;
	m_sdFR51 = 0.5;
	m_sdFR52 = 0.5;
	m_sdTAUC51 = (SIM_DATA)(m_sdMC51)/m_sdWC;
	m_sdTAUC52 = (SIM_DATA)(m_sdMC52)/m_sdWC;
	D(m_sdTC51, (SIM_DATA)(1)/(SIM_DATA)(m_sdMC51)/m_sdCPC*m_sdFR51*m_sdBR5*m_sdGAMMA9*(V(m_sdTW6, iCol)-V(m_sdTC51, iCol))+(SIM_DATA)(1)/m_sdTAUC51*(V(m_sdTC42, iCol)-V(m_sdTC51, iCol)), iCol);
	D(m_sdTC52, (SIM_DATA)(1)/(SIM_DATA)(m_sdMC52)/m_sdCPC*m_sdFR52*m_sdBR5*m_sdGAMMA9*(V(m_sdTW6, iCol)-V(m_sdTC51, iCol))+(SIM_DATA)(1)/m_sdTAUC52*(V(m_sdTC51, iCol)-V(m_sdTC52, iCol)), iCol);
	m_sdMC61 = m_sdMCLUMP;
	m_sdMC62 = m_sdMCLUMP;
	m_sdFR61 = 0.5;
	m_sdFR62 = 0.5;
	m_sdTAUC61 = (SIM_DATA)(m_sdMC61)/m_sdWC;
	m_sdTAUC62 = (SIM_DATA)(m_sdMC62)/m_sdWC;
	D(m_sdTC61, (SIM_DATA)(1)/(SIM_DATA)(m_sdMC61)/m_sdCPC*m_sdFR61*m_sdBR6*m_sdGAMMA9*(V(m_sdTW5, iCol)-V(m_sdTC61, iCol))+(SIM_DATA)(1)/m_sdTAUC61*(V(m_sdTC52, iCol)-V(m_sdTC61, iCol)), iCol);
	D(m_sdTC62, (SIM_DATA)(1)/(SIM_DATA)(m_sdMC62)/m_sdCPC*m_sdFR62*m_sdBR6*m_sdGAMMA9*(V(m_sdTW5, iCol)-V(m_sdTC61, iCol))+(SIM_DATA)(1)/m_sdTAUC62*(V(m_sdTC61, iCol)-V(m_sdTC62, iCol)), iCol);
	m_sdMC71 = m_sdMCLUMP;
	m_sdMC72 = m_sdMCLUMP;
	m_sdFR71 = 0.5;
	m_sdFR72 = 0.5;
	m_sdTAUC71 = (SIM_DATA)(m_sdMC71)/m_sdWC;
	m_sdTAUC72 = (SIM_DATA)(m_sdMC72)/m_sdWC;
	D(m_sdTC71, (SIM_DATA)(1)/(SIM_DATA)(m_sdMC71)/m_sdCPC*m_sdFR71*m_sdBR7*m_sdGAMMA9*(V(m_sdTW4, iCol)-V(m_sdTC71, iCol))+(SIM_DATA)(1)/m_sdTAUC71*(V(m_sdTC62, iCol)-V(m_sdTC71, iCol)), iCol);
	D(m_sdTC72, (SIM_DATA)(1)/(SIM_DATA)(m_sdMC72)/m_sdCPC*m_sdFR72*m_sdBR7*m_sdGAMMA9*(V(m_sdTW4, iCol)-V(m_sdTC71, iCol))+(SIM_DATA)(1)/m_sdTAUC72*(V(m_sdTC71, iCol)-V(m_sdTC72, iCol)), iCol);
	m_sdMC81 = m_sdMCLUMP;
	m_sdMC82 = m_sdMCLUMP;
	m_sdFR81 = 0.5;
	m_sdFR82 = 0.5;
	m_sdTAUC81 = (SIM_DATA)(m_sdMC81)/m_sdWC;
	m_sdTAUC82 = (SIM_DATA)(m_sdMC82)/m_sdWC;
	D(m_sdTC81, (SIM_DATA)(1)/(SIM_DATA)(m_sdMC81)/m_sdCPC*m_sdFR81*m_sdBR8*m_sdGAMMA9*(V(m_sdTW3, iCol)-V(m_sdTC81, iCol))+(SIM_DATA)(1)/m_sdTAUC81*(V(m_sdTC72, iCol)-V(m_sdTC81, iCol)), iCol);
	D(m_sdTC82, (SIM_DATA)(1)/(SIM_DATA)(m_sdMC82)/m_sdCPC*m_sdFR82*m_sdBR8*m_sdGAMMA9*(V(m_sdTW3, iCol)-V(m_sdTC81, iCol))+(SIM_DATA)(1)/m_sdTAUC82*(V(m_sdTC81, iCol)-V(m_sdTC82, iCol)), iCol);
	m_sdMC91 = m_sdMCLUMP;
	m_sdMC92 = m_sdMCLUMP;
	m_sdFR91 = 0.5;
	m_sdFR92 = 0.5;
	m_sdTAUC91 = (SIM_DATA)(m_sdMC91)/m_sdWC;
	m_sdTAUC92 = (SIM_DATA)(m_sdMC92)/m_sdWC;
	D(m_sdTC91, (SIM_DATA)(1)/(SIM_DATA)(m_sdMC91)/m_sdCPC*m_sdFR91*m_sdBR9*m_sdGAMMA9*(V(m_sdTW2, iCol)-V(m_sdTC91, iCol))+(SIM_DATA)(1)/m_sdTAUC91*(V(m_sdTC82, iCol)-V(m_sdTC91, iCol)), iCol);
	D(m_sdTC92, (SIM_DATA)(1)/(SIM_DATA)(m_sdMC92)/m_sdCPC*m_sdFR92*m_sdBR9*m_sdGAMMA9*(V(m_sdTW2, iCol)-V(m_sdTC91, iCol))+(SIM_DATA)(1)/m_sdTAUC92*(V(m_sdTC91, iCol)-V(m_sdTC92, iCol)), iCol);
	m_sdMC101 = m_sdMCLUMP;
	m_sdMC102 = m_sdMCLUMP;
	m_sdFR101 = 0.5;
	m_sdFR102 = 0.5;
	m_sdTAUC101 = (SIM_DATA)(m_sdMC101)/m_sdWC;
	m_sdTAUC102 = (SIM_DATA)(m_sdMC102)/m_sdWC;
	D(m_sdTC101, (SIM_DATA)(1)/(SIM_DATA)(m_sdMC101)/m_sdCPC*m_sdFR101*m_sdBR10*m_sdGAMMA9*(V(m_sdTW1, iCol)-V(m_sdTC101, iCol))+(SIM_DATA)(1)/m_sdTAUC101*(V(m_sdTC92, iCol)-V(m_sdTC101, iCol)), iCol);
	D(m_sdTC102, (SIM_DATA)(1)/(SIM_DATA)(m_sdMC102)/m_sdCPC*m_sdFR102*m_sdBR10*m_sdGAMMA9*(V(m_sdTW1, iCol)-V(m_sdTC101, iCol))+(SIM_DATA)(1)/m_sdTAUC102*(V(m_sdTC101, iCol)-V(m_sdTC102, iCol)), iCol);
	//-- ---------------------------------------
	//-- Piping from core to HE
	//-- ---------------------------------------
	//-- TP2=Temperature in to tube side (C)
	m_sdMP2 = 20.0;
	//MP2=Mass of coolant in pipe (kg)
	//-- *EQUATION 51
	m_sdTAUP2 = (SIM_DATA)(m_sdMP2)/m_sdWC;
	//TAUP2=Transit time of piping (s)
	D(m_sdTP2, (SIM_DATA)(1)/m_sdTAUP2*(V(m_sdTC102, iCol)-V(m_sdTP2, iCol)), iCol);
	//-- ==================
	//-- Heat exchanger
	//-- ==================
	//-- Tube side
	m_sdMT1 = m_sdMC11*4*10;
	//MT1=Mass of lump (kg)
	m_sdTAUT1 = (SIM_DATA)(m_sdMT1)/m_sdWC;
	//TAUT1=Transit time of lump (s)
	m_sdGAM1 = m_sdHT1*m_sdSAL1C*4;
	//-- *EQUATION 52
	D(m_sdTT1, (SIM_DATA)(2)/m_sdTAUT1*(V(m_sdTP2, iCol)-V(m_sdTT1, iCol))-(SIM_DATA)(m_sdGAM1)/(SIM_DATA)(m_sdMT1)/m_sdCPC*(V(m_sdTT1, iCol)-V(m_sdTT, iCol)), iCol);
	//-- TT2=Temperature of second lump (C)
	//--  *EQUATION 53
	D(m_sdTT2, (SIM_DATA)(2)/m_sdTAUT1*(V(m_sdTT1, iCol)-V(m_sdTT2, iCol))-(SIM_DATA)(m_sdGAM1)/(SIM_DATA)(m_sdMT1)/m_sdCPC*(V(m_sdTT1, iCol)-V(m_sdTT, iCol)), iCol);
	//-- -----------------------------------------
	//-- Piping from HE to Core
	//-- -----------------------------------------
	m_sdMP1 = 20.0;
	//Mass of coolant in pipe (kg)
	m_sdTAUP1 = (SIM_DATA)(m_sdMP1)/m_sdWC;
	//TAUP1=Transit time in piping (s)
	//-- ----------------------------
	//-- Shell side
	//-- ----------------------------
	m_sdWCS2 = 3.0;
	//WC2=Mass flow rate (kg/s)
	m_sdMS1 = 2*m_sdMT1;
	//MS1=Mass of lump (kg)
	m_sdTAUS1 = (SIM_DATA)(m_sdMS1)/m_sdWCS2;
	//TAUS1=Transit time of lump (s)
	//-- GAM2=Overall heat transfer Coeff. - Tube to shell side (MW/C)
	//-- GAM3=Overall heat transfer Coeff. - Shell side to shell (MW/C)
	m_sdGAM2 = m_sdGAM1;
	m_sdGAM3 = m_sdGAM1;
	//-- TS1=Temperature of first lump (C)
	//-- *EQUATION 54
	D(m_sdTS1, (SIM_DATA)(2)/m_sdTAUS1*(m_sdTS1IN-V(m_sdTS1, iCol))+(SIM_DATA)(m_sdGAM2)/(SIM_DATA)(m_sdMS1)/m_sdCPC*(V(m_sdTT, iCol)-V(m_sdTS1, iCol))-(SIM_DATA)(m_sdGAM3)/(SIM_DATA)(m_sdMS1)/m_sdCPC*(V(m_sdTS1, iCol)-V(m_sdTS, iCol)), iCol);
	//-- TS2=Temperature of second lump (C)
	//--  *EQUATION 55
	D(m_sdTS2, (SIM_DATA)(2)/m_sdTAUS1*(V(m_sdTS1, iCol)-V(m_sdTS2, iCol))+(SIM_DATA)(m_sdGAM2)/(SIM_DATA)(m_sdMS1)/m_sdCPC*(V(m_sdTT, iCol)-V(m_sdTS1, iCol))-(SIM_DATA)(m_sdGAM3)/(SIM_DATA)(m_sdMS1)/m_sdCPC*(V(m_sdTS1, iCol)-V(m_sdTS, iCol)), iCol);
	//-- -----------------------------
	//-- Tube
	//-- -----------------------------
	m_sdMT = 10.0;
	//MT=mass of tube (kg)
	m_sdCPT = 0.5e-03;
	//CPT=specific heat of tube (MJ/C/kg)
	//-- TT=Temperature of Tube
	//-- *EQUATION 56
	D(m_sdTT, 2*(SIM_DATA)(m_sdGAM1)/(SIM_DATA)(m_sdMT)/m_sdCPT*(V(m_sdTT1, iCol)-V(m_sdTT, iCol))-2*(SIM_DATA)(m_sdGAM2)/(SIM_DATA)(m_sdMT)/m_sdCPT*(V(m_sdTT, iCol)-V(m_sdTS1, iCol)), iCol);
	//-- ---------------------
	//-- Shell
	m_sdMS = 20.0;
	//MS=Mass of shell (kg)
	m_sdCPS = 0.5e-03;
	//CPS=Specific heat of shell (MJ/C/kg)
	//-- TS=Shell Temperature (C)
	//-- *EQUATION 57
	D(m_sdTS, 2*(SIM_DATA)(m_sdGAM3)/(SIM_DATA)(m_sdMS)/m_sdCPS*(V(m_sdTS1, iCol)-V(m_sdTS, iCol)), iCol);
	//-- Check energy balance in core
	m_sdHCDOT = m_sdWC*m_sdCPC*(V(m_sdTC102, iCol)-m_sdTC1IN);
	m_sdHTDOT = m_sdWC*m_sdCPC*(V(m_sdTT2, iCol)-V(m_sdTP2, iCol));
	m_sdHSDOT = m_sdWC2*m_sdCPC*(V(m_sdTS2, iCol)-m_sdTS1IN);
	D(m_sdHSOUT, m_sdHSDOT, iCol);
	D(m_sdHTOUT, m_sdHTDOT, iCol);
	D(m_sdHCOUT, m_sdHCDOT, iCol);
	m_sdHC = (SIM_DATA)(1)/m_sdAK*(V(m_sdTEMP1, iCol)+V(m_sdTEMP2, iCol)+V(m_sdTEMP3, iCol)+V(m_sdTEMP4, iCol)+V(m_sdTEMP5, iCol)+V(m_sdTEMP6, iCol)+V(m_sdTEMP7, iCol)+V(m_sdTEMP8, iCol)+V(m_sdTEMP9, iCol)+V(m_sdTEMP10, iCol)-10*m_sdT0);
	m_sdHC1 = m_sdMC11*m_sdCPC*(V(m_sdTC11, iCol)-m_sdT0)+m_sdMC12*m_sdCPC*(V(m_sdTC12, iCol)-m_sdT0);
	m_sdHC2 = m_sdMC21*m_sdCPC*(V(m_sdTC21, iCol)-m_sdT0)+m_sdMC22*m_sdCPC*(V(m_sdTC22, iCol)-m_sdT0);
	m_sdHC3 = m_sdMC31*m_sdCPC*(V(m_sdTC31, iCol)-m_sdT0)+m_sdMC32*m_sdCPC*(V(m_sdTC32, iCol)-m_sdT0);
	m_sdHC4 = m_sdMC41*m_sdCPC*(V(m_sdTC41, iCol)-m_sdT0)+m_sdMC42*m_sdCPC*(V(m_sdTC42, iCol)-m_sdT0);
	m_sdHC5 = m_sdMC51*m_sdCPC*(V(m_sdTC51, iCol)-m_sdT0)+m_sdMC52*m_sdCPC*(V(m_sdTC52, iCol)-m_sdT0);
	m_sdHC6 = m_sdMC61*m_sdCPC*(V(m_sdTC61, iCol)-m_sdT0)+m_sdMC62*m_sdCPC*(V(m_sdTC62, iCol)-m_sdT0);
	m_sdHC7 = m_sdMC71*m_sdCPC*(V(m_sdTC71, iCol)-m_sdT0)+m_sdMC72*m_sdCPC*(V(m_sdTC72, iCol)-m_sdT0);
	m_sdHC8 = m_sdMC81*m_sdCPC*(V(m_sdTC81, iCol)-m_sdT0)+m_sdMC82*m_sdCPC*(V(m_sdTC82, iCol)-m_sdT0);
	m_sdHC9 = m_sdMC91*m_sdCPC*(V(m_sdTC91, iCol)-m_sdT0)+m_sdMC92*m_sdCPC*(V(m_sdTC92, iCol)-m_sdT0);
	m_sdHC10 = m_sdMC101*m_sdCPC*(V(m_sdTC101, iCol)-m_sdT0)+m_sdMC102*m_sdCPC*(V(m_sdTC102, iCol)-m_sdT0);
	m_sdHCL = m_sdHC1+m_sdHC2+m_sdHC3+m_sdHC4+m_sdHC5+m_sdHC6+m_sdHC7+m_sdHC8+m_sdHC9+m_sdHC10;
	m_sdHSHELLS = m_sdMS1*m_sdCPC*(V(m_sdTS1, iCol)+V(m_sdTS2, iCol)-2*m_sdTS1IN);
	m_sdHTUBES = m_sdMT1*m_sdCPC*(V(m_sdTT1, iCol)+V(m_sdTT2, iCol)-2*m_sdT0);
	m_sdHSHELL = m_sdMS*m_sdCPS*(V(m_sdTS, iCol)-m_sdT0);
	m_sdHTUBE = m_sdMT*m_sdCPT*(V(m_sdTT, iCol)-m_sdT0);
	m_sdHP1 = m_sdTAUP1*m_sdWC*m_sdCPC*(m_sdTC1IN-m_sdT0);
	m_sdHP2 = m_sdTAUP2*m_sdWC*m_sdCPC*(V(m_sdTP2, iCol)-m_sdT0);
	m_sdEB = V(m_sdETOT, iCol)-V(m_sdHSOUT, iCol)-m_sdHC-m_sdHCL-m_sdHSHELLS-m_sdHTUBES-m_sdHSHELL-m_sdHTUBE-m_sdHP1-m_sdHP2;
	//-- =================================================================================================
	//--  Second Loop
	//-- =================================================================================================
	m_sdTWA2 = (SIM_DATA)((V(m_sdTW21, iCol)+V(m_sdTW22, iCol)+V(m_sdTW23, iCol)+V(m_sdTW24, iCol)+V(m_sdTW25, iCol)+V(m_sdTW26, iCol)+V(m_sdTW27, iCol)+V(m_sdTW28, iCol)+V(m_sdTW29, iCol)+V(m_sdTW210, iCol)))/10;
	m_sdTHOT2 = m_sdTEMP;
	m_sdTCOLD2 = m_sdTWA2;
	m_sdDT2 = (SIM_DATA)(m_sdVF)/m_sdbta2*swtch(m_sdFLDT);
	m_sdLPr2 = m_sddynvis2*(SIM_DATA)(m_sdCV)/m_sdTKF2;
	//Prandt number for Loop 2
	m_sdLRa2 = 9.81*m_sdbta2*(m_sdTHOT2+m_sdDT2-m_sdTCOLD2)*(SIM_DATA)((pow(m_sdHCORE,3)))/(m_sdTHDIFF2*m_sdkinvis2);
	m_sdLNu2f1 = ((SIM_DATA)(4)/3)*pow(((SIM_DATA)((7*m_sdLRa2*m_sdLPr2))/(5*(20+21*m_sdLPr2))),0.25);
	m_sdLNu2f2 = (SIM_DATA)((4*(272+315*m_sdLPr2)*m_sdHCORE))/(35*(64+63*m_sdLPr2)*m_sdHDia2);
	m_sdLNu2 = m_sdLNu2f1+m_sdLNu2f2;
	//Nusselt number for Loop 2
	m_sdHTNC2 = m_sdLNu2*(SIM_DATA)(m_sdTKF2)/m_sdHCORE;
	//heat transfer coefficient for Loop 2
	m_sdLRe2 = ((SIM_DATA)(m_sdWC2)/12)*(SIM_DATA)(m_sdHDia2)/(SIM_DATA)(m_sdXSAT)/m_sddynvis2;
	m_sdLe2 = 0.06*m_sdLRe2;
	m_sdLNuF2f1 = 0.037*(pow(m_sdLRe2,0.8))*m_sdLPr2;
	m_sdLNuF2f2 = 1+2.443*(pow(m_sdLRe2,(-0.1)))*((pow(m_sdLPr2,((SIM_DATA)(2)/3)))-1);
	m_sdLNuF2 = swtch(m_sdLRe2-2300)*((SIM_DATA)(m_sdLNuF2f1)/m_sdLNuF2f2)*(1-pow(((SIM_DATA)(m_sdHDia2)/m_sdHCORE),((SIM_DATA)(2)/3))) + swtch(2300-m_sdLRe2)*1.86*pow(((SIM_DATA)((m_sdLRe2*m_sdLPr2))/((SIM_DATA)(m_sdHCORE)/m_sdHDia2)),((SIM_DATA)(1)/3));
	m_sdHT2 = m_sdLNuF2*(SIM_DATA)(m_sdTKW2)/m_sdHDia2;
	m_sdGMMA2 = m_sdSAL2F*m_sdHTNC2;
	m_sdGAMMA2 = m_sdGMMA2;
	m_sdGAMMA90 = m_sdSAL2C*m_sdHT2;
	m_sdAK3 = (SIM_DATA)(1)/(m_sdELM2*m_sdCV);
	m_sdalpha2 = m_sdalpha;
	//-- 1
	m_sdTL211DOT = -m_sdAK3*0.5*m_sdBR1*m_sdGAMMA2*(V(m_sdTL211, iCol)-V(m_sdTW21, iCol))+(SIM_DATA)(m_sdWF2)/m_sdELM2*(V(m_sdTEMP10, iCol)-V(m_sdTL211, iCol));
	D(m_sdTL211, m_sdTL211DOT, iCol);
	m_sdVLT211 = (SIM_DATA)(m_sdVL2)/20*m_sdalpha2*m_sdTL211DOT;
	D(m_sdVL211, m_sdVLT211, iCol);
	m_sdRL211 = (SIM_DATA)(m_sdELM2)/V(m_sdVL211, iCol);
	m_sdTL212DOT = -m_sdAK3*0.5*m_sdBR1*m_sdGAMMA2*(V(m_sdTL211, iCol)-V(m_sdTW21, iCol))+(SIM_DATA)(m_sdWF2)/m_sdELM2*(V(m_sdTL211, iCol)-V(m_sdTL212, iCol));
	D(m_sdTL212, m_sdTL212DOT, iCol);
	m_sdVLT212 = (SIM_DATA)(m_sdVL2)/20*m_sdalpha2*m_sdTL212DOT;
	D(m_sdVL212, m_sdVLT212, iCol);
	m_sdRL212 = (SIM_DATA)(m_sdELM2)/V(m_sdVL212, iCol);
	m_sdMW2 = (SIM_DATA)(m_sdCCTM)/10;
	//MW=mass of WALL (kg)
	m_sdCPW2 = m_sdCPW;
	//CPW=specific heat of WALL (MJ/C/kg)
	D(m_sdTW21, (SIM_DATA)(1)/(SIM_DATA)(m_sdMW2)/m_sdCPW2*(m_sdBR1*m_sdGAMMA2*(V(m_sdTL211, iCol)-V(m_sdTW21, iCol))-m_sdBR1*m_sdGAMMA90*(V(m_sdTW21, iCol)-V(m_sdTC2101, iCol))), iCol);
	//-- 2
	m_sdTL221DOT = -m_sdAK3*0.5*m_sdBR1*m_sdGAMMA2*(V(m_sdTL221, iCol)-V(m_sdTW22, iCol))+(SIM_DATA)(m_sdWF2)/m_sdELM2*(V(m_sdTL212, iCol)-V(m_sdTL221, iCol));
	D(m_sdTL221, m_sdTL221DOT, iCol);
	m_sdVLT221 = (SIM_DATA)(m_sdVL2)/20*m_sdalpha2*m_sdTL221DOT;
	D(m_sdVL221, m_sdVLT221, iCol);
	m_sdRL221 = (SIM_DATA)(m_sdELM2)/V(m_sdVL221, iCol);
	m_sdTL222DOT = -m_sdAK3*0.5*m_sdBR1*m_sdGAMMA2*(V(m_sdTL221, iCol)-V(m_sdTW22, iCol))+(SIM_DATA)(m_sdWF2)/m_sdELM2*(V(m_sdTL221, iCol)-V(m_sdTL222, iCol));
	D(m_sdTL222, m_sdTL222DOT, iCol);
	m_sdVLT222 = (SIM_DATA)(m_sdVL2)/20*m_sdalpha2*m_sdTL222DOT;
	D(m_sdVL222, m_sdVLT222, iCol);
	m_sdRL222 = (SIM_DATA)(m_sdELM2)/V(m_sdVL222, iCol);
	D(m_sdTW22, (SIM_DATA)(1)/(SIM_DATA)(m_sdMW2)/m_sdCPW2*(m_sdBR1*m_sdGAMMA2*(V(m_sdTL221, iCol)-V(m_sdTW22, iCol))-m_sdBR1*m_sdGAMMA90*(V(m_sdTW22, iCol)-V(m_sdTC291, iCol))), iCol);
	//-- 3
	m_sdTL231DOT = -m_sdAK3*0.5*m_sdBR1*m_sdGAMMA2*(V(m_sdTL231, iCol)-V(m_sdTW23, iCol))+(SIM_DATA)(m_sdWF2)/m_sdELM2*(V(m_sdTL222, iCol)-V(m_sdTL231, iCol));
	D(m_sdTL231, m_sdTL231DOT, iCol);
	m_sdVLT231 = (SIM_DATA)(m_sdVL2)/20*m_sdalpha2*m_sdTL231DOT;
	D(m_sdVL231, m_sdVLT231, iCol);
	m_sdRL231 = (SIM_DATA)(m_sdELM2)/V(m_sdVL231, iCol);
	m_sdTL232DOT = -m_sdAK3*0.5*m_sdBR1*m_sdGAMMA2*(V(m_sdTL231, iCol)-V(m_sdTW23, iCol))+(SIM_DATA)(m_sdWF2)/m_sdELM2*(V(m_sdTL231, iCol)-V(m_sdTL232, iCol));
	D(m_sdTL232, m_sdTL232DOT, iCol);
	m_sdVLT232 = (SIM_DATA)(m_sdVL2)/20*m_sdalpha2*m_sdTL232DOT;
	D(m_sdVL232, m_sdVLT232, iCol);
	m_sdRL232 = (SIM_DATA)(m_sdELM2)/V(m_sdVL232, iCol);
	D(m_sdTW23, (SIM_DATA)(1)/(SIM_DATA)(m_sdMW2)/m_sdCPW2*(m_sdBR1*m_sdGAMMA2*(V(m_sdTL231, iCol)-V(m_sdTW23, iCol))-m_sdBR1*m_sdGAMMA90*(V(m_sdTW23, iCol)-V(m_sdTC281, iCol))), iCol);
	//-- 4
	m_sdTL241DOT = -m_sdAK3*0.5*m_sdBR1*m_sdGAMMA2*(V(m_sdTL241, iCol)-V(m_sdTW24, iCol))+(SIM_DATA)(m_sdWF2)/m_sdELM2*(V(m_sdTL232, iCol)-V(m_sdTL241, iCol));
	D(m_sdTL241, m_sdTL241DOT, iCol);
	m_sdVLT241 = (SIM_DATA)(m_sdVL2)/20*m_sdalpha2*m_sdTL241DOT;
	D(m_sdVL241, m_sdVLT241, iCol);
	m_sdRL241 = (SIM_DATA)(m_sdELM2)/V(m_sdVL241, iCol);
	m_sdTL242DOT = -m_sdAK3*0.5*m_sdBR1*m_sdGAMMA2*(V(m_sdTL241, iCol)-V(m_sdTW24, iCol))+(SIM_DATA)(m_sdWF2)/m_sdELM2*(V(m_sdTL241, iCol)-V(m_sdTL242, iCol));
	D(m_sdTL242, m_sdTL242DOT, iCol);
	m_sdVLT242 = (SIM_DATA)(m_sdVL2)/20*m_sdalpha2*m_sdTL242DOT;
	D(m_sdVL242, m_sdVLT242, iCol);
	m_sdRL242 = (SIM_DATA)(m_sdELM2)/V(m_sdVL242, iCol);
	D(m_sdTW24, (SIM_DATA)(1)/(SIM_DATA)(m_sdMW2)/m_sdCPW2*(m_sdBR1*m_sdGAMMA2*(V(m_sdTL241, iCol)-V(m_sdTW24, iCol))-m_sdBR1*m_sdGAMMA90*(V(m_sdTW24, iCol)-V(m_sdTC271, iCol))), iCol);
	//-- 5
	m_sdTL251DOT = -m_sdAK3*0.5*m_sdBR1*m_sdGAMMA2*(V(m_sdTL251, iCol)-V(m_sdTW25, iCol))+(SIM_DATA)(m_sdWF2)/m_sdELM2*(V(m_sdTL242, iCol)-V(m_sdTL251, iCol));
	D(m_sdTL251, m_sdTL251DOT, iCol);
	m_sdVLT251 = (SIM_DATA)(m_sdVL2)/20*m_sdalpha2*m_sdTL251DOT;
	D(m_sdVL251, m_sdVLT251, iCol);
	m_sdRL251 = (SIM_DATA)(m_sdELM2)/V(m_sdVL251, iCol);
	m_sdTL252DOT = -m_sdAK3*0.5*m_sdBR1*m_sdGAMMA2*(V(m_sdTL251, iCol)-V(m_sdTW25, iCol))+(SIM_DATA)(m_sdWF2)/m_sdELM2*(V(m_sdTL251, iCol)-V(m_sdTL252, iCol));
	D(m_sdTL252, m_sdTL252DOT, iCol);
	m_sdVLT252 = (SIM_DATA)(m_sdVL2)/20*m_sdalpha2*m_sdTL252DOT;
	D(m_sdVL252, m_sdVLT252, iCol);
	m_sdRL252 = (SIM_DATA)(m_sdELM2)/V(m_sdVL252, iCol);
	D(m_sdTW25, (SIM_DATA)(1)/(SIM_DATA)(m_sdMW2)/m_sdCPW2*(m_sdBR1*m_sdGAMMA2*(V(m_sdTL251, iCol)-V(m_sdTW25, iCol))-m_sdBR1*m_sdGAMMA90*(V(m_sdTW25, iCol)-V(m_sdTC261, iCol))), iCol);
	//-- 6
	m_sdTL261DOT = -m_sdAK3*0.5*m_sdBR1*m_sdGAMMA2*(V(m_sdTL261, iCol)-V(m_sdTW26, iCol))+(SIM_DATA)(m_sdWF2)/m_sdELM2*(V(m_sdTL252, iCol)-V(m_sdTL261, iCol));
	D(m_sdTL261, m_sdTL261DOT, iCol);
	m_sdVLT261 = (SIM_DATA)(m_sdVL2)/20*m_sdalpha2*m_sdTL261DOT;
	D(m_sdVL261, m_sdVLT261, iCol);
	m_sdRL261 = (SIM_DATA)(m_sdELM2)/V(m_sdVL261, iCol);
	m_sdTL262DOT = -m_sdAK3*0.5*m_sdBR1*m_sdGAMMA2*(V(m_sdTL261, iCol)-V(m_sdTW26, iCol))+(SIM_DATA)(m_sdWF2)/m_sdELM2*(V(m_sdTL261, iCol)-V(m_sdTL262, iCol));
	D(m_sdTL262, m_sdTL262DOT, iCol);
	m_sdVLT262 = (SIM_DATA)(m_sdVL2)/20*m_sdalpha2*m_sdTL262DOT;
	D(m_sdVL262, m_sdVLT262, iCol);
	m_sdRL262 = (SIM_DATA)(m_sdELM2)/V(m_sdVL262, iCol);
	D(m_sdTW26, (SIM_DATA)(1)/(SIM_DATA)(m_sdMW2)/m_sdCPW2*(m_sdBR1*m_sdGAMMA2*(V(m_sdTL261, iCol)-V(m_sdTW26, iCol))-m_sdBR1*m_sdGAMMA90*(V(m_sdTW26, iCol)-V(m_sdTC251, iCol))), iCol);
	//-- 7
	m_sdTL271DOT = -m_sdAK3*0.5*m_sdBR1*m_sdGAMMA2*(V(m_sdTL271, iCol)-V(m_sdTW27, iCol))+(SIM_DATA)(m_sdWF2)/m_sdELM2*(V(m_sdTL262, iCol)-V(m_sdTL271, iCol));
	D(m_sdTL271, m_sdTL271DOT, iCol);
	m_sdVLT271 = (SIM_DATA)(m_sdVL2)/20*m_sdalpha2*m_sdTL271DOT;
	D(m_sdVL271, m_sdVLT271, iCol);
	m_sdRL271 = (SIM_DATA)(m_sdELM2)/V(m_sdVL271, iCol);
	m_sdTL272DOT = -m_sdAK3*0.5*m_sdBR1*m_sdGAMMA2*(V(m_sdTL271, iCol)-V(m_sdTW27, iCol))+(SIM_DATA)(m_sdWF2)/m_sdELM2*(V(m_sdTL271, iCol)-V(m_sdTL272, iCol));
	D(m_sdTL272, m_sdTL272DOT, iCol);
	m_sdVLT272 = (SIM_DATA)(m_sdVL2)/20*m_sdalpha2*m_sdTL272DOT;
	D(m_sdVL272, m_sdVLT272, iCol);
	m_sdRL272 = (SIM_DATA)(m_sdELM2)/V(m_sdVL272, iCol);
	D(m_sdTW27, (SIM_DATA)(1)/(SIM_DATA)(m_sdMW2)/m_sdCPW2*(m_sdBR1*m_sdGAMMA2*(V(m_sdTL271, iCol)-V(m_sdTW27, iCol))-m_sdBR1*m_sdGAMMA90*(V(m_sdTW27, iCol)-V(m_sdTC241, iCol))), iCol);
	//-- 8
	m_sdTL281DOT = -m_sdAK3*0.5*m_sdBR1*m_sdGAMMA2*(V(m_sdTL281, iCol)-V(m_sdTW28, iCol))+(SIM_DATA)(m_sdWF2)/m_sdELM2*(V(m_sdTL272, iCol)-V(m_sdTL281, iCol));
	D(m_sdTL281, m_sdTL281DOT, iCol);
	m_sdVLT281 = (SIM_DATA)(m_sdVL2)/20*m_sdalpha2*m_sdTL281DOT;
	D(m_sdVL281, m_sdVLT281, iCol);
	m_sdRL281 = (SIM_DATA)(m_sdELM2)/V(m_sdVL281, iCol);
	m_sdTL282DOT = -m_sdAK3*0.5*m_sdBR1*m_sdGAMMA2*(V(m_sdTL281, iCol)-V(m_sdTW28, iCol))+(SIM_DATA)(m_sdWF2)/m_sdELM2*(V(m_sdTL281, iCol)-V(m_sdTL282, iCol));
	D(m_sdTL282, m_sdTL282DOT, iCol);
	m_sdVLT282 = (SIM_DATA)(m_sdVL2)/20*m_sdalpha2*m_sdTL282DOT;
	D(m_sdVL282, m_sdVLT282, iCol);
	m_sdRL282 = (SIM_DATA)(m_sdELM2)/V(m_sdVL282, iCol);
	D(m_sdTW28, (SIM_DATA)(1)/(SIM_DATA)(m_sdMW2)/m_sdCPW2*(m_sdBR1*m_sdGAMMA2*(V(m_sdTL281, iCol)-V(m_sdTW28, iCol))-m_sdBR1*m_sdGAMMA90*(V(m_sdTW28, iCol)-V(m_sdTC231, iCol))), iCol);
	//-- 9
	m_sdTL291DOT = -m_sdAK3*0.5*m_sdBR1*m_sdGAMMA2*(V(m_sdTL291, iCol)-V(m_sdTW29, iCol))+(SIM_DATA)(m_sdWF2)/m_sdELM2*(V(m_sdTL282, iCol)-V(m_sdTL291, iCol));
	D(m_sdTL291, m_sdTL291DOT, iCol);
	m_sdVLT291 = (SIM_DATA)(m_sdVL2)/20*m_sdalpha2*m_sdTL291DOT;
	D(m_sdVL291, m_sdVLT291, iCol);
	m_sdRL291 = (SIM_DATA)(m_sdELM2)/V(m_sdVL291, iCol);
	m_sdTL292DOT = -m_sdAK3*0.5*m_sdBR1*m_sdGAMMA2*(V(m_sdTL291, iCol)-V(m_sdTW29, iCol))+(SIM_DATA)(m_sdWF2)/m_sdELM2*(V(m_sdTL291, iCol)-V(m_sdTL292, iCol));
	D(m_sdTL292, m_sdTL292DOT, iCol);
	m_sdVLT292 = (SIM_DATA)(m_sdVL2)/20*m_sdalpha2*m_sdTL292DOT;
	D(m_sdVL292, m_sdVLT292, iCol);
	m_sdRL292 = (SIM_DATA)(m_sdELM2)/V(m_sdVL292, iCol);
	D(m_sdTW29, (SIM_DATA)(1)/(SIM_DATA)(m_sdMW2)/m_sdCPW2*(m_sdBR1*m_sdGAMMA2*(V(m_sdTL291, iCol)-V(m_sdTW29, iCol))-m_sdBR1*m_sdGAMMA90*(V(m_sdTW29, iCol)-V(m_sdTC221, iCol))), iCol);
	//-- 10
	m_sdTL2101DOT = -m_sdAK3*0.5*m_sdBR1*m_sdGAMMA2*(V(m_sdTL2101, iCol)-V(m_sdTW210, iCol))+(SIM_DATA)(m_sdWF2)/m_sdELM2*(V(m_sdTL292, iCol)-V(m_sdTL2101, iCol));
	D(m_sdTL2101, m_sdTL2101DOT, iCol);
	m_sdVLT2101 = (SIM_DATA)(m_sdVL2)/20*m_sdalpha2*m_sdTL2101DOT;
	D(m_sdVL2101, m_sdVLT2101, iCol);
	m_sdRL2101 = (SIM_DATA)(m_sdELM2)/V(m_sdVL2101, iCol);
	m_sdTL2102DOT = -m_sdAK3*0.5*m_sdBR1*m_sdGAMMA2*(V(m_sdTL2101, iCol)-V(m_sdTW210, iCol))+(SIM_DATA)(m_sdWF2)/m_sdELM2*(V(m_sdTL2101, iCol)-V(m_sdTL2102, iCol));
	D(m_sdTL2102, m_sdTL2102DOT, iCol);
	m_sdVLT2102 = (SIM_DATA)(m_sdVL2)/20*m_sdalpha2*m_sdTL2102DOT;
	D(m_sdVL2102, m_sdVLT2102, iCol);
	m_sdRL2102 = (SIM_DATA)(m_sdELM2)/V(m_sdVL2102, iCol);
	D(m_sdTW210, (SIM_DATA)(1)/(SIM_DATA)(m_sdMW2)/m_sdCPW2*(m_sdBR1*m_sdGAMMA2*(V(m_sdTL2101, iCol)-V(m_sdTW210, iCol))-m_sdBR1*m_sdGAMMA90*(V(m_sdTW210, iCol)-V(m_sdTC211, iCol))), iCol);
	m_sdTC2IN = m_sdTC1IN;
	m_sdCPC2 = 4.18e-03;
	//CPC=Specific heat of coolant Node 1 (MJ/C/kg)
	//-- 1
	m_sdMC211 = m_sdMCLUMP2;
	//MC11=Mass of Node 1 lump 1 (kg)
	m_sdMC212 = m_sdMCLUMP2;
	//MC12=Mass of Node 1 lump 2 (kg)
	m_sdFR211 = 0.5;
	//FR11=Fraction of Region 1 power going into lump 1
	m_sdFR212 = 0.5;
	//FR12=Fraction of Region 1 power going into lump2
	m_sdTAUC211 = (SIM_DATA)(m_sdMC211)/m_sdWC2;
	//TAUC11=Transit time Node 1 lump 1 (s)
	m_sdTAUC212 = (SIM_DATA)(m_sdMC212)/m_sdWC2;
	//TAUC12=Transit time Node 1 lump 2 (s)
	D(m_sdTC211, (SIM_DATA)(1)/(SIM_DATA)(m_sdMC211)/m_sdCPC2*m_sdFR211*m_sdBR1*m_sdGAMMA90*(V(m_sdTW210, iCol)-V(m_sdTC211, iCol))+(SIM_DATA)(1)/m_sdTAUC211*(m_sdTC2IN-V(m_sdTC211, iCol)), iCol);
	D(m_sdTC212, (SIM_DATA)(1)/(SIM_DATA)(m_sdMC212)/m_sdCPC2*m_sdFR212*m_sdBR1*m_sdGAMMA90*(V(m_sdTW210, iCol)-V(m_sdTC211, iCol))+(SIM_DATA)(1)/m_sdTAUC212*(V(m_sdTC211, iCol)-V(m_sdTC212, iCol)), iCol);
	//-- 2
	m_sdMC221 = m_sdMCLUMP2;
	//MC11=Mass of Node 1 lump 1 (kg)
	m_sdMC222 = m_sdMCLUMP2;
	//MC12=Mass of Node 1 lump 2 (kg)
	m_sdFR221 = 0.5;
	//FR11=Fraction of Region 1 power going into lump 1
	m_sdFR222 = 0.5;
	//FR12=Fraction of Region 1 power going into lump2
	m_sdTAUC221 = (SIM_DATA)(m_sdMC221)/m_sdWC2;
	//TAUC11=Transit time Node 1 lump 1 (s)
	m_sdTAUC222 = (SIM_DATA)(m_sdMC222)/m_sdWC2;
	//TAUC12=Transit time Node 1 lump 2 (s)
	D(m_sdTC221, (SIM_DATA)(1)/(SIM_DATA)(m_sdMC221)/m_sdCPC2*m_sdFR221*m_sdBR1*m_sdGAMMA90*(V(m_sdTW29, iCol)-V(m_sdTC221, iCol))+(SIM_DATA)(1)/m_sdTAUC221*(V(m_sdTC212, iCol)-V(m_sdTC221, iCol)), iCol);
	D(m_sdTC222, (SIM_DATA)(1)/(SIM_DATA)(m_sdMC222)/m_sdCPC2*m_sdFR222*m_sdBR1*m_sdGAMMA90*(V(m_sdTW29, iCol)-V(m_sdTC221, iCol))+(SIM_DATA)(1)/m_sdTAUC222*(V(m_sdTC221, iCol)-V(m_sdTC222, iCol)), iCol);
	//-- 3
	m_sdMC231 = m_sdMCLUMP2;
	//MC11=Mass of Node 1 lump 1 (kg)
	m_sdMC232 = m_sdMCLUMP2;
	//MC12=Mass of Node 1 lump 2 (kg)
	m_sdFR231 = 0.5;
	//FR11=Fraction of Region 1 power going into lump 1
	m_sdFR232 = 0.5;
	//FR12=Fraction of Region 1 power going into lump2
	m_sdTAUC231 = (SIM_DATA)(m_sdMC231)/m_sdWC2;
	//TAUC11=Transit time Node 1 lump 1 (s)
	m_sdTAUC232 = (SIM_DATA)(m_sdMC232)/m_sdWC2;
	//TAUC12=Transit time Node 1 lump 2 (s)
	D(m_sdTC231, (SIM_DATA)(1)/(SIM_DATA)(m_sdMC231)/m_sdCPC2*m_sdFR231*m_sdBR1*m_sdGAMMA90*(V(m_sdTW28, iCol)-V(m_sdTC231, iCol))+(SIM_DATA)(1)/m_sdTAUC231*(V(m_sdTC222, iCol)-V(m_sdTC231, iCol)), iCol);
	D(m_sdTC232, (SIM_DATA)(1)/(SIM_DATA)(m_sdMC232)/m_sdCPC2*m_sdFR232*m_sdBR1*m_sdGAMMA90*(V(m_sdTW28, iCol)-V(m_sdTC231, iCol))+(SIM_DATA)(1)/m_sdTAUC232*(V(m_sdTC231, iCol)-V(m_sdTC232, iCol)), iCol);
	//-- 4
	m_sdMC241 = m_sdMCLUMP2;
	//MC11=Mass of Node 1 lump 1 (kg)
	m_sdMC242 = m_sdMCLUMP2;
	//MC12=Mass of Node 1 lump 2 (kg)
	m_sdFR241 = 0.5;
	//FR11=Fraction of Region 1 power going into lump 1
	m_sdFR242 = 0.5;
	//FR12=Fraction of Region 1 power going into lump2
	m_sdTAUC241 = (SIM_DATA)(m_sdMC241)/m_sdWC2;
	//TAUC11=Transit time Node 1 lump 1 (s)
	m_sdTAUC242 = (SIM_DATA)(m_sdMC242)/m_sdWC2;
	//TAUC12=Transit time Node 1 lump 2 (s)
	D(m_sdTC241, (SIM_DATA)(1)/(SIM_DATA)(m_sdMC241)/m_sdCPC2*m_sdFR241*m_sdBR1*m_sdGAMMA90*(V(m_sdTW27, iCol)-V(m_sdTC241, iCol))+(SIM_DATA)(1)/m_sdTAUC241*(V(m_sdTC232, iCol)-V(m_sdTC241, iCol)), iCol);
	D(m_sdTC242, (SIM_DATA)(1)/(SIM_DATA)(m_sdMC242)/m_sdCPC2*m_sdFR242*m_sdBR1*m_sdGAMMA90*(V(m_sdTW27, iCol)-V(m_sdTC241, iCol))+(SIM_DATA)(1)/m_sdTAUC242*(V(m_sdTC241, iCol)-V(m_sdTC242, iCol)), iCol);
	//-- 5
	m_sdMC251 = m_sdMCLUMP2;
	//MC11=Mass of Node 1 lump 1 (kg)
	m_sdMC252 = m_sdMCLUMP2;
	//MC12=Mass of Node 1 lump 2 (kg)
	m_sdFR251 = 0.5;
	//FR11=Fraction of Region 1 power going into lump 1
	m_sdFR252 = 0.5;
	//FR12=Fraction of Region 1 power going into lump2
	m_sdTAUC251 = (SIM_DATA)(m_sdMC251)/m_sdWC2;
	//TAUC11=Transit time Node 1 lump 1 (s)
	m_sdTAUC252 = (SIM_DATA)(m_sdMC252)/m_sdWC2;
	//TAUC12=Transit time Node 1 lump 2 (s)
	D(m_sdTC251, (SIM_DATA)(1)/(SIM_DATA)(m_sdMC251)/m_sdCPC2*m_sdFR251*m_sdBR1*m_sdGAMMA90*(V(m_sdTW26, iCol)-V(m_sdTC251, iCol))+(SIM_DATA)(1)/m_sdTAUC251*(V(m_sdTC242, iCol)-V(m_sdTC251, iCol)), iCol);
	D(m_sdTC252, (SIM_DATA)(1)/(SIM_DATA)(m_sdMC252)/m_sdCPC2*m_sdFR252*m_sdBR1*m_sdGAMMA90*(V(m_sdTW26, iCol)-V(m_sdTC251, iCol))+(SIM_DATA)(1)/m_sdTAUC252*(V(m_sdTC251, iCol)-V(m_sdTC252, iCol)), iCol);
	//-- 6
	m_sdMC261 = m_sdMCLUMP2;
	//MC11=Mass of Node 1 lump 1 (kg)
	m_sdMC262 = m_sdMCLUMP2;
	//MC12=Mass of Node 1 lump 2 (kg)
	m_sdFR261 = 0.5;
	//FR11=Fraction of Region 1 power going into lump 1
	m_sdFR262 = 0.5;
	//FR12=Fraction of Region 1 power going into lump2
	m_sdTAUC261 = (SIM_DATA)(m_sdMC261)/m_sdWC2;
	//TAUC11=Transit time Node 1 lump 1 (s)
	m_sdTAUC262 = (SIM_DATA)(m_sdMC262)/m_sdWC2;
	//TAUC12=Transit time Node 1 lump 2 (s)
	D(m_sdTC261, (SIM_DATA)(1)/(SIM_DATA)(m_sdMC261)/m_sdCPC2*m_sdFR261*m_sdBR1*m_sdGAMMA90*(V(m_sdTW25, iCol)-V(m_sdTC261, iCol))+(SIM_DATA)(1)/m_sdTAUC261*(V(m_sdTC252, iCol)-V(m_sdTC261, iCol)), iCol);
	D(m_sdTC262, (SIM_DATA)(1)/(SIM_DATA)(m_sdMC262)/m_sdCPC2*m_sdFR262*m_sdBR1*m_sdGAMMA90*(V(m_sdTW25, iCol)-V(m_sdTC261, iCol))+(SIM_DATA)(1)/m_sdTAUC262*(V(m_sdTC261, iCol)-V(m_sdTC262, iCol)), iCol);
	//-- 7
	m_sdMC271 = m_sdMCLUMP2;
	//MC11=Mass of Node 1 lump 1 (kg)
	m_sdMC272 = m_sdMCLUMP2;
	//MC12=Mass of Node 1 lump 2 (kg)
	m_sdFR271 = 0.5;
	//FR11=Fraction of Region 1 power going into lump 1
	m_sdFR272 = 0.5;
	//FR12=Fraction of Region 1 power going into lump2
	m_sdTAUC271 = (SIM_DATA)(m_sdMC271)/m_sdWC2;
	//TAUC11=Transit time Node 1 lump 1 (s)
	m_sdTAUC272 = (SIM_DATA)(m_sdMC272)/m_sdWC2;
	//TAUC12=Transit time Node 1 lump 2 (s)
	D(m_sdTC271, (SIM_DATA)(1)/(SIM_DATA)(m_sdMC271)/m_sdCPC2*m_sdFR271*m_sdBR1*m_sdGAMMA90*(V(m_sdTW24, iCol)-V(m_sdTC271, iCol))+(SIM_DATA)(1)/m_sdTAUC271*(V(m_sdTC262, iCol)-V(m_sdTC271, iCol)), iCol);
	D(m_sdTC272, (SIM_DATA)(1)/(SIM_DATA)(m_sdMC272)/m_sdCPC2*m_sdFR272*m_sdBR1*m_sdGAMMA90*(V(m_sdTW24, iCol)-V(m_sdTC271, iCol))+(SIM_DATA)(1)/m_sdTAUC272*(V(m_sdTC271, iCol)-V(m_sdTC272, iCol)), iCol);
	//-- 8
	m_sdMC281 = m_sdMCLUMP2;
	//MC11=Mass of Node 1 lump 1 (kg)
	m_sdMC282 = m_sdMCLUMP2;
	//MC12=Mass of Node 1 lump 2 (kg)
	m_sdFR281 = 0.5;
	//FR11=Fraction of Region 1 power going into lump 1
	m_sdFR282 = 0.5;
	//FR12=Fraction of Region 1 power going into lump2
	m_sdTAUC281 = (SIM_DATA)(m_sdMC281)/m_sdWC2;
	//TAUC11=Transit time Node 1 lump 1 (s)
	m_sdTAUC282 = (SIM_DATA)(m_sdMC282)/m_sdWC2;
	//TAUC12=Transit time Node 1 lump 2 (s)
	D(m_sdTC281, (SIM_DATA)(1)/(SIM_DATA)(m_sdMC281)/m_sdCPC2*m_sdFR281*m_sdBR1*m_sdGAMMA90*(V(m_sdTW23, iCol)-V(m_sdTC281, iCol))+(SIM_DATA)(1)/m_sdTAUC281*(V(m_sdTC272, iCol)-V(m_sdTC281, iCol)), iCol);
	D(m_sdTC282, (SIM_DATA)(1)/(SIM_DATA)(m_sdMC282)/m_sdCPC2*m_sdFR282*m_sdBR1*m_sdGAMMA90*(V(m_sdTW23, iCol)-V(m_sdTC281, iCol))+(SIM_DATA)(1)/m_sdTAUC282*(V(m_sdTC281, iCol)-V(m_sdTC282, iCol)), iCol);
	//-- 9
	m_sdMC291 = m_sdMCLUMP2;
	//MC11=Mass of Node 1 lump 1 (kg)
	m_sdMC292 = m_sdMCLUMP2;
	//MC12=Mass of Node 1 lump 2 (kg)
	m_sdFR291 = 0.5;
	//FR11=Fraction of Region 1 power going into lump 1
	m_sdFR292 = 0.5;
	//FR12=Fraction of Region 1 power going into lump2
	m_sdTAUC291 = (SIM_DATA)(m_sdMC291)/m_sdWC2;
	//TAUC11=Transit time Node 1 lump 1 (s)
	m_sdTAUC292 = (SIM_DATA)(m_sdMC292)/m_sdWC2;
	//TAUC12=Transit time Node 1 lump 2 (s)
	D(m_sdTC291, (SIM_DATA)(1)/(SIM_DATA)(m_sdMC291)/m_sdCPC2*m_sdFR291*m_sdBR1*m_sdGAMMA90*(V(m_sdTW22, iCol)-V(m_sdTC291, iCol))+(SIM_DATA)(1)/m_sdTAUC291*(V(m_sdTC282, iCol)-V(m_sdTC291, iCol)), iCol);
	D(m_sdTC292, (SIM_DATA)(1)/(SIM_DATA)(m_sdMC292)/m_sdCPC2*m_sdFR292*m_sdBR1*m_sdGAMMA90*(V(m_sdTW22, iCol)-V(m_sdTC291, iCol))+(SIM_DATA)(1)/m_sdTAUC292*(V(m_sdTC291, iCol)-V(m_sdTC292, iCol)), iCol);
	//-- 10
	m_sdMC2101 = m_sdMCLUMP2;
	//MC11=Mass of Node 1 lump 1 (kg)
	m_sdMC2102 = m_sdMCLUMP2;
	//MC12=Mass of Node 1 lump 2 (kg)
	m_sdFR2101 = 0.5;
	//FR11=Fraction of Region 1 power going into lump 1
	m_sdFR2102 = 0.5;
	//FR12=Fraction of Region 1 power going into lump2
	m_sdTAUC2101 = (SIM_DATA)(m_sdMC2101)/m_sdWC2;
	//TAUC11=Transit time Node 1 lump 1 (s)
	m_sdTAUC2102 = (SIM_DATA)(m_sdMC2102)/m_sdWC2;
	//TAUC12=Transit time Node 1 lump 2 (s)
	D(m_sdTC2101, (SIM_DATA)(1)/(SIM_DATA)(m_sdMC2101)/m_sdCPC2*m_sdFR2101*m_sdBR1*m_sdGAMMA90*(V(m_sdTW21, iCol)-V(m_sdTC2101, iCol))+(SIM_DATA)(1)/m_sdTAUC2101*(V(m_sdTC292, iCol)-V(m_sdTC2101, iCol)), iCol);
	D(m_sdTC2102, (SIM_DATA)(1)/(SIM_DATA)(m_sdMC2102)/m_sdCPC2*m_sdFR2102*m_sdBR1*m_sdGAMMA90*(V(m_sdTW21, iCol)-V(m_sdTC2101, iCol))+(SIM_DATA)(1)/m_sdTAUC2102*(V(m_sdTC2101, iCol)-V(m_sdTC2102, iCol)), iCol);
	//-- =========================================================================================
	//-- Loop 3
	//-- =========================================================================================
	m_sdTWA3 = (SIM_DATA)((V(m_sdTW31, iCol)+V(m_sdTW32, iCol)+V(m_sdTW33, iCol)+V(m_sdTW34, iCol)+V(m_sdTW35, iCol)+V(m_sdTW36, iCol)+V(m_sdTW37, iCol)+V(m_sdTW38, iCol)+V(m_sdTW39, iCol)+V(m_sdTW310, iCol)))/10;
	m_sdTHOT3 = m_sdTEMP;
	m_sdTCOLD3 = m_sdTWA3;
	m_sdDT3 = (SIM_DATA)(m_sdVF)/m_sdbta3*swtch(m_sdFLDT);
	m_sdLPr3 = m_sddynvis3*(SIM_DATA)(m_sdCV)/m_sdTKF3;
	//Prandt number for Loop 3
	m_sdLRa3 = 9.81*m_sdbta3*(m_sdTHOT3+m_sdDT3-m_sdTCOLD3)*(SIM_DATA)((pow(m_sdHCORE,3)))/(m_sdTHDIFF3*m_sdkinvis3);
	m_sdLNu3 = pow((0.825+(SIM_DATA)(((0.387*(pow(m_sdLRa3,((SIM_DATA)(1)/6))))))/pow((1+(pow(((SIM_DATA)(0.492)/m_sdLPr3),((SIM_DATA)(9)/16)))),((SIM_DATA)(8)/27))),2);
	//Nusselt number for Loop 3
	m_sdHTNC3 = m_sdLNu3*(SIM_DATA)(m_sdTKF3)/m_sdHCORE;
	//heat transfer coefficient for Loop 3
	m_sdLRe3 = m_sdWC3*(SIM_DATA)(m_sdHDia3)/(SIM_DATA)(m_sdXSA2)/m_sddynvis3;
	m_sdLe3 = 0.06*m_sdLRe3;
	m_sdLf3 = pow((0.790*log(m_sdLRe3)-0.164),(-2));
	m_sdLNuF3f1 = ((SIM_DATA)(m_sdLf3)/8)*(m_sdLRe3-1000)*m_sdLPr1;
	m_sdLNuF3f2 = 1+12.7*(pow(((SIM_DATA)(m_sdLf3)/8),0.5))*((pow(m_sdLPr3,((SIM_DATA)(2)/3)))-1);
	m_sdLNuF3 = swtch(m_sdLRe3-2300)*((SIM_DATA)(m_sdLNuF3f1)/m_sdLNuF3f2)*(1-pow(((SIM_DATA)(m_sdHDia3)/m_sdHCORE),((SIM_DATA)(2)/3))) + swtch(2300-m_sdLRe3)*1.86*pow(((SIM_DATA)((m_sdLRe3*m_sdLPr3))/((SIM_DATA)(m_sdHCORE)/m_sdHDia3)),((SIM_DATA)(1)/3));
	m_sdHT3 = m_sdLNuF3*(SIM_DATA)(m_sdTKW3)/m_sdHDia3;
	m_sdGMMA3 = m_sdSAL3F*m_sdHTNC3;
	m_sdGAMMA3 = m_sdGMMA3;
	m_sdGAMMA95 = m_sdSAL3C*m_sdHT3;
	m_sdAK4 = (SIM_DATA)(1)/(m_sdELM3*m_sdCV);
	m_sdalpha3 = m_sdalpha;
	//-- 1
	m_sdTL311DOT = -m_sdAK4*0.5*m_sdBR1*m_sdGAMMA3*(V(m_sdTL311, iCol)-V(m_sdTW31, iCol))+(SIM_DATA)(m_sdWF3)/m_sdELM3*(V(m_sdTEMP10, iCol)-V(m_sdTL311, iCol));
	D(m_sdTL311, m_sdTL311DOT, iCol);
	m_sdVLT311 = (SIM_DATA)(m_sdVL3)/20*m_sdalpha3*m_sdTL311DOT;
	D(m_sdVL311, m_sdVLT311, iCol);
	m_sdRL311 = (SIM_DATA)(m_sdELM3)/V(m_sdVL311, iCol);
	m_sdTL312DOT = -m_sdAK4*0.5*m_sdBR1*m_sdGAMMA3*(V(m_sdTL311, iCol)-V(m_sdTW31, iCol))+(SIM_DATA)(m_sdWF3)/m_sdELM3*(V(m_sdTL311, iCol)-V(m_sdTL312, iCol));
	D(m_sdTL312, m_sdTL312DOT, iCol);
	m_sdVLT312 = (SIM_DATA)(m_sdVL3)/20*m_sdalpha3*m_sdTL312DOT;
	D(m_sdVL312, m_sdVLT312, iCol);
	m_sdRL312 = (SIM_DATA)(m_sdELM3)/V(m_sdVL312, iCol);
	m_sdMW3 = (SIM_DATA)(m_sdCC2M)/10;
	//MW=mass of WALL (kg)
	m_sdCPW3 = m_sdCPW;
	//CPW=specific heat of WALL (MJ/C/kg)
	D(m_sdTW31, (SIM_DATA)(1)/(SIM_DATA)(m_sdMW3)/m_sdCPW3*(m_sdBR1*m_sdGAMMA3*(V(m_sdTL311, iCol)-V(m_sdTW31, iCol))-m_sdBR1*m_sdGAMMA95*(V(m_sdTW31, iCol)-V(m_sdTC3101, iCol))), iCol);
	//-- 2
	m_sdTL321DOT = -m_sdAK4*0.5*m_sdBR1*m_sdGAMMA3*(V(m_sdTL321, iCol)-V(m_sdTW32, iCol))+(SIM_DATA)(m_sdWF3)/m_sdELM3*(V(m_sdTL312, iCol)-V(m_sdTL321, iCol));
	D(m_sdTL321, m_sdTL321DOT, iCol);
	m_sdVLT321 = (SIM_DATA)(m_sdVL3)/20*m_sdalpha3*m_sdTL321DOT;
	D(m_sdVL321, m_sdVLT321, iCol);
	m_sdRL321 = (SIM_DATA)(m_sdELM3)/V(m_sdVL321, iCol);
	m_sdTL322DOT = -m_sdAK4*0.5*m_sdBR1*m_sdGAMMA3*(V(m_sdTL321, iCol)-V(m_sdTW32, iCol))+(SIM_DATA)(m_sdWF3)/m_sdELM3*(V(m_sdTL321, iCol)-V(m_sdTL322, iCol));
	D(m_sdTL322, m_sdTL322DOT, iCol);
	m_sdVLT322 = (SIM_DATA)(m_sdVL3)/20*m_sdalpha3*m_sdTL322DOT;
	D(m_sdVL322, m_sdVLT322, iCol);
	m_sdRL322 = (SIM_DATA)(m_sdELM3)/V(m_sdVL322, iCol);
	D(m_sdTW32, (SIM_DATA)(1)/(SIM_DATA)(m_sdMW3)/m_sdCPW3*(m_sdBR1*m_sdGAMMA3*(V(m_sdTL321, iCol)-V(m_sdTW32, iCol))-m_sdBR1*m_sdGAMMA95*(V(m_sdTW32, iCol)-V(m_sdTC391, iCol))), iCol);
	//-- 3
	m_sdTL331DOT = -m_sdAK4*0.5*m_sdBR1*m_sdGAMMA3*(V(m_sdTL331, iCol)-V(m_sdTW33, iCol))+(SIM_DATA)(m_sdWF3)/m_sdELM3*(V(m_sdTL322, iCol)-V(m_sdTL331, iCol));
	D(m_sdTL331, m_sdTL331DOT, iCol);
	m_sdVLT331 = (SIM_DATA)(m_sdVL3)/20*m_sdalpha3*m_sdTL331DOT;
	D(m_sdVL331, m_sdVLT331, iCol);
	m_sdRL331 = (SIM_DATA)(m_sdELM3)/V(m_sdVL331, iCol);
	m_sdTL332DOT = -m_sdAK4*0.5*m_sdBR1*m_sdGAMMA3*(V(m_sdTL331, iCol)-V(m_sdTW33, iCol))+(SIM_DATA)(m_sdWF3)/m_sdELM3*(V(m_sdTL331, iCol)-V(m_sdTL332, iCol));
	D(m_sdTL332, m_sdTL332DOT, iCol);
	m_sdVLT332 = (SIM_DATA)(m_sdVL3)/20*m_sdalpha3*m_sdTL332DOT;
	D(m_sdVL332, m_sdVLT332, iCol);
	m_sdRL332 = (SIM_DATA)(m_sdELM3)/V(m_sdVL332, iCol);
	D(m_sdTW33, (SIM_DATA)(1)/(SIM_DATA)(m_sdMW3)/m_sdCPW3*(m_sdBR1*m_sdGAMMA3*(V(m_sdTL331, iCol)-V(m_sdTW33, iCol))-m_sdBR1*m_sdGAMMA95*(V(m_sdTW33, iCol)-V(m_sdTC381, iCol))), iCol);
	//-- 4
	m_sdTL341DOT = -m_sdAK4*0.5*m_sdBR1*m_sdGAMMA3*(V(m_sdTL341, iCol)-V(m_sdTW34, iCol))+(SIM_DATA)(m_sdWF3)/m_sdELM3*(V(m_sdTL332, iCol)-V(m_sdTL341, iCol));
	D(m_sdTL341, m_sdTL341DOT, iCol);
	m_sdVLT341 = (SIM_DATA)(m_sdVL3)/20*m_sdalpha3*m_sdTL341DOT;
	D(m_sdVL341, m_sdVLT341, iCol);
	m_sdRL341 = (SIM_DATA)(m_sdELM3)/V(m_sdVL341, iCol);
	m_sdTL342DOT = -m_sdAK4*0.5*m_sdBR1*m_sdGAMMA3*(V(m_sdTL341, iCol)-V(m_sdTW34, iCol))+(SIM_DATA)(m_sdWF3)/m_sdELM3*(V(m_sdTL341, iCol)-V(m_sdTL342, iCol));
	D(m_sdTL342, m_sdTL342DOT, iCol);
	m_sdVLT342 = (SIM_DATA)(m_sdVL3)/20*m_sdalpha3*m_sdTL342DOT;
	D(m_sdVL342, m_sdVLT342, iCol);
	m_sdRL342 = (SIM_DATA)(m_sdELM3)/V(m_sdVL342, iCol);
	D(m_sdTW34, (SIM_DATA)(1)/(SIM_DATA)(m_sdMW3)/m_sdCPW3*(m_sdBR1*m_sdGAMMA3*(V(m_sdTL341, iCol)-V(m_sdTW34, iCol))-m_sdBR1*m_sdGAMMA95*(V(m_sdTW34, iCol)-V(m_sdTC371, iCol))), iCol);
	//-- 5
	m_sdTL351DOT = -m_sdAK4*0.5*m_sdBR1*m_sdGAMMA3*(V(m_sdTL351, iCol)-V(m_sdTW35, iCol))+(SIM_DATA)(m_sdWF3)/m_sdELM3*(V(m_sdTL342, iCol)-V(m_sdTL351, iCol));
	D(m_sdTL351, m_sdTL351DOT, iCol);
	m_sdVLT351 = (SIM_DATA)(m_sdVL3)/20*m_sdalpha3*m_sdTL351DOT;
	D(m_sdVL351, m_sdVLT351, iCol);
	m_sdRL351 = (SIM_DATA)(m_sdELM3)/V(m_sdVL351, iCol);
	m_sdTL352DOT = -m_sdAK4*0.5*m_sdBR1*m_sdGAMMA3*(V(m_sdTL351, iCol)-V(m_sdTW35, iCol))+(SIM_DATA)(m_sdWF3)/m_sdELM3*(V(m_sdTL351, iCol)-V(m_sdTL352, iCol));
	D(m_sdTL352, m_sdTL352DOT, iCol);
	m_sdVLT352 = (SIM_DATA)(m_sdVL3)/20*m_sdalpha3*m_sdTL352DOT;
	D(m_sdVL352, m_sdVLT352, iCol);
	m_sdRL352 = (SIM_DATA)(m_sdELM3)/V(m_sdVL352, iCol);
	D(m_sdTW35, (SIM_DATA)(1)/(SIM_DATA)(m_sdMW3)/m_sdCPW3*(m_sdBR1*m_sdGAMMA3*(V(m_sdTL351, iCol)-V(m_sdTW35, iCol))-m_sdBR1*m_sdGAMMA95*(V(m_sdTW35, iCol)-V(m_sdTC361, iCol))), iCol);
	//-- 6
	m_sdTL361DOT = -m_sdAK4*0.5*m_sdBR1*m_sdGAMMA3*(V(m_sdTL361, iCol)-V(m_sdTW36, iCol))+(SIM_DATA)(m_sdWF3)/m_sdELM3*(V(m_sdTL352, iCol)-V(m_sdTL361, iCol));
	D(m_sdTL361, m_sdTL361DOT, iCol);
	m_sdVLT361 = (SIM_DATA)(m_sdVL3)/20*m_sdalpha3*m_sdTL361DOT;
	D(m_sdVL361, m_sdVLT361, iCol);
	m_sdRL361 = (SIM_DATA)(m_sdELM3)/V(m_sdVL361, iCol);
	m_sdTL362DOT = -m_sdAK4*0.5*m_sdBR1*m_sdGAMMA3*(V(m_sdTL361, iCol)-V(m_sdTW36, iCol))+(SIM_DATA)(m_sdWF3)/m_sdELM3*(V(m_sdTL361, iCol)-V(m_sdTL362, iCol));
	D(m_sdTL362, m_sdTL362DOT, iCol);
	m_sdVLT362 = (SIM_DATA)(m_sdVL3)/20*m_sdalpha3*m_sdTL362DOT;
	D(m_sdVL362, m_sdVLT362, iCol);
	m_sdRL362 = (SIM_DATA)(m_sdELM3)/V(m_sdVL362, iCol);
	D(m_sdTW36, (SIM_DATA)(1)/(SIM_DATA)(m_sdMW3)/m_sdCPW3*(m_sdBR1*m_sdGAMMA3*(V(m_sdTL361, iCol)-V(m_sdTW36, iCol))-m_sdBR1*m_sdGAMMA95*(V(m_sdTW36, iCol)-V(m_sdTC351, iCol))), iCol);
	//-- 7
	m_sdTL371DOT = -m_sdAK4*0.5*m_sdBR1*m_sdGAMMA3*(V(m_sdTL371, iCol)-V(m_sdTW37, iCol))+(SIM_DATA)(m_sdWF3)/m_sdELM3*(V(m_sdTL362, iCol)-V(m_sdTL371, iCol));
	D(m_sdTL371, m_sdTL371DOT, iCol);
	m_sdVLT371 = (SIM_DATA)(m_sdVL3)/20*m_sdalpha3*m_sdTL371DOT;
	D(m_sdVL371, m_sdVLT371, iCol);
	m_sdRL371 = (SIM_DATA)(m_sdELM3)/V(m_sdVL371, iCol);
	m_sdTL372DOT = -m_sdAK4*0.5*m_sdBR1*m_sdGAMMA3*(V(m_sdTL371, iCol)-V(m_sdTW37, iCol))+(SIM_DATA)(m_sdWF3)/m_sdELM3*(V(m_sdTL371, iCol)-V(m_sdTL372, iCol));
	D(m_sdTL372, m_sdTL372DOT, iCol);
	m_sdVLT372 = (SIM_DATA)(m_sdVL3)/20*m_sdalpha3*m_sdTL372DOT;
	D(m_sdVL372, m_sdVLT372, iCol);
	m_sdRL372 = (SIM_DATA)(m_sdELM3)/V(m_sdVL372, iCol);
	D(m_sdTW37, (SIM_DATA)(1)/(SIM_DATA)(m_sdMW3)/m_sdCPW3*(m_sdBR1*m_sdGAMMA3*(V(m_sdTL371, iCol)-V(m_sdTW37, iCol))-m_sdBR1*m_sdGAMMA95*(V(m_sdTW37, iCol)-V(m_sdTC341, iCol))), iCol);
	//-- 8
	m_sdTL381DOT = -m_sdAK4*0.5*m_sdBR1*m_sdGAMMA3*(V(m_sdTL381, iCol)-V(m_sdTW38, iCol))+(SIM_DATA)(m_sdWF3)/m_sdELM3*(V(m_sdTL372, iCol)-V(m_sdTL381, iCol));
	D(m_sdTL381, m_sdTL381DOT, iCol);
	m_sdVLT381 = (SIM_DATA)(m_sdVL3)/20*m_sdalpha3*m_sdTL381DOT;
	D(m_sdVL381, m_sdVLT381, iCol);
	m_sdRL381 = (SIM_DATA)(m_sdELM3)/V(m_sdVL381, iCol);
	m_sdTL382DOT = -m_sdAK4*0.5*m_sdBR1*m_sdGAMMA3*(V(m_sdTL381, iCol)-V(m_sdTW38, iCol))+(SIM_DATA)(m_sdWF3)/m_sdELM3*(V(m_sdTL381, iCol)-V(m_sdTL382, iCol));
	D(m_sdTL382, m_sdTL382DOT, iCol);
	m_sdVLT382 = (SIM_DATA)(m_sdVL3)/20*m_sdalpha3*m_sdTL382DOT;
	D(m_sdVL382, m_sdVLT382, iCol);
	m_sdRL382 = (SIM_DATA)(m_sdELM3)/V(m_sdVL382, iCol);
	D(m_sdTW38, (SIM_DATA)(1)/(SIM_DATA)(m_sdMW3)/m_sdCPW3*(m_sdBR1*m_sdGAMMA3*(V(m_sdTL381, iCol)-V(m_sdTW38, iCol))-m_sdBR1*m_sdGAMMA95*(V(m_sdTW38, iCol)-V(m_sdTC331, iCol))), iCol);
	//-- 9
	m_sdTL391DOT = -m_sdAK4*0.5*m_sdBR1*m_sdGAMMA3*(V(m_sdTL391, iCol)-V(m_sdTW39, iCol))+(SIM_DATA)(m_sdWF3)/m_sdELM3*(V(m_sdTL382, iCol)-V(m_sdTL391, iCol));
	D(m_sdTL391, m_sdTL391DOT, iCol);
	m_sdVLT391 = (SIM_DATA)(m_sdVL3)/20*m_sdalpha3*m_sdTL391DOT;
	D(m_sdVL391, m_sdVLT391, iCol);
	m_sdRL391 = (SIM_DATA)(m_sdELM3)/V(m_sdVL391, iCol);
	m_sdTL392DOT = -m_sdAK4*0.5*m_sdBR1*m_sdGAMMA3*(V(m_sdTL391, iCol)-V(m_sdTW39, iCol))+(SIM_DATA)(m_sdWF3)/m_sdELM3*(V(m_sdTL391, iCol)-V(m_sdTL392, iCol));
	D(m_sdTL392, m_sdTL392DOT, iCol);
	m_sdVLT392 = (SIM_DATA)(m_sdVL3)/20*m_sdalpha3*m_sdTL392DOT;
	D(m_sdVL392, m_sdVLT392, iCol);
	m_sdRL392 = (SIM_DATA)(m_sdELM3)/V(m_sdVL392, iCol);
	D(m_sdTW39, (SIM_DATA)(1)/(SIM_DATA)(m_sdMW3)/m_sdCPW3*(m_sdBR1*m_sdGAMMA3*(V(m_sdTL391, iCol)-V(m_sdTW39, iCol))-m_sdBR1*m_sdGAMMA95*(V(m_sdTW39, iCol)-V(m_sdTC321, iCol))), iCol);
	//-- 10
	m_sdTL3101DOT = -m_sdAK4*0.5*m_sdBR1*m_sdGAMMA3*(V(m_sdTL3101, iCol)-V(m_sdTW310, iCol))+(SIM_DATA)(m_sdWF3)/m_sdELM3*(V(m_sdTL392, iCol)-V(m_sdTL3101, iCol));
	D(m_sdTL3101, m_sdTL3101DOT, iCol);
	m_sdVLT3101 = (SIM_DATA)(m_sdVL3)/20*m_sdalpha3*m_sdTL3101DOT;
	D(m_sdVL3101, m_sdVLT3101, iCol);
	m_sdRL3101 = (SIM_DATA)(m_sdELM3)/V(m_sdVL3101, iCol);
	m_sdTL3102DOT = -m_sdAK4*0.5*m_sdBR1*m_sdGAMMA3*(V(m_sdTL3101, iCol)-V(m_sdTW310, iCol))+(SIM_DATA)(m_sdWF3)/m_sdELM3*(V(m_sdTL3101, iCol)-V(m_sdTL3102, iCol));
	D(m_sdTL3102, m_sdTL3102DOT, iCol);
	m_sdVLT3102 = (SIM_DATA)(m_sdVL3)/20*m_sdalpha3*m_sdTL3102DOT;
	D(m_sdVL3102, m_sdVLT3102, iCol);
	m_sdRL3102 = (SIM_DATA)(m_sdELM3)/V(m_sdVL3102, iCol);
	D(m_sdTW310, (SIM_DATA)(1)/(SIM_DATA)(m_sdMW3)/m_sdCPW3*(m_sdBR1*m_sdGAMMA3*(V(m_sdTL3101, iCol)-V(m_sdTW310, iCol))-m_sdBR1*m_sdGAMMA95*(V(m_sdTW310, iCol)-V(m_sdTC311, iCol))), iCol);
	m_sdTC3IN = m_sdTC1IN;
	m_sdCPC3 = 4.18e-03;
	//CPC=Specific heat of coolant Node 1 (MJ/C/kg)
	//-- 1
	m_sdMC311 = m_sdMCLUMP3;
	//MC11=Mass of Node 1 lump 1 (kg)
	m_sdMC312 = m_sdMCLUMP3;
	//MC12=Mass of Node 1 lump 2 (kg)
	m_sdFR311 = 0.5;
	//FR11=Fraction of Region 1 power going into lump 1
	m_sdFR312 = 0.5;
	//FR12=Fraction of Region 1 power going into lump2
	m_sdTAUC311 = (SIM_DATA)(m_sdMC311)/m_sdWC3;
	//TAUC11=Transit time Node 1 lump 1 (s)
	m_sdTAUC312 = (SIM_DATA)(m_sdMC312)/m_sdWC3;
	//TAUC12=Transit time Node 1 lump 2 (s)
	D(m_sdTC311, (SIM_DATA)(1)/(SIM_DATA)(m_sdMC311)/m_sdCPC3*m_sdFR311*m_sdBR1*m_sdGAMMA95*(V(m_sdTW310, iCol)-V(m_sdTC311, iCol))+(SIM_DATA)(1)/m_sdTAUC311*(m_sdTC3IN-V(m_sdTC311, iCol)), iCol);
	D(m_sdTC312, (SIM_DATA)(1)/(SIM_DATA)(m_sdMC312)/m_sdCPC3*m_sdFR312*m_sdBR1*m_sdGAMMA95*(V(m_sdTW310, iCol)-V(m_sdTC311, iCol))+(SIM_DATA)(1)/m_sdTAUC312*(V(m_sdTC311, iCol)-V(m_sdTC312, iCol)), iCol);
	//-- 2
	m_sdMC321 = m_sdMCLUMP3;
	//MC11=Mass of Node 1 lump 1 (kg)
	m_sdMC322 = m_sdMCLUMP3;
	//MC12=Mass of Node 1 lump 2 (kg)
	m_sdFR321 = 0.5;
	//FR11=Fraction of Region 1 power going into lump 1
	m_sdFR322 = 0.5;
	//FR12=Fraction of Region 1 power going into lump2
	m_sdTAUC321 = (SIM_DATA)(m_sdMC321)/m_sdWC3;
	//TAUC11=Transit time Node 1 lump 1 (s)
	m_sdTAUC322 = (SIM_DATA)(m_sdMC322)/m_sdWC3;
	//TAUC12=Transit time Node 1 lump 2 (s)
	D(m_sdTC321, (SIM_DATA)(1)/(SIM_DATA)(m_sdMC321)/m_sdCPC3*m_sdFR321*m_sdBR1*m_sdGAMMA95*(V(m_sdTW39, iCol)-V(m_sdTC321, iCol))+(SIM_DATA)(1)/m_sdTAUC321*(V(m_sdTC312, iCol)-V(m_sdTC321, iCol)), iCol);
	D(m_sdTC322, (SIM_DATA)(1)/(SIM_DATA)(m_sdMC322)/m_sdCPC3*m_sdFR322*m_sdBR1*m_sdGAMMA95*(V(m_sdTW39, iCol)-V(m_sdTC321, iCol))+(SIM_DATA)(1)/m_sdTAUC322*(V(m_sdTC321, iCol)-V(m_sdTC322, iCol)), iCol);
	//-- 3
	m_sdMC331 = m_sdMCLUMP3;
	//MC11=Mass of Node 1 lump 1 (kg)
	m_sdMC332 = m_sdMCLUMP3;
	//MC12=Mass of Node 1 lump 2 (kg)
	m_sdFR331 = 0.5;
	//FR11=Fraction of Region 1 power going into lump 1
	m_sdFR332 = 0.5;
	//FR12=Fraction of Region 1 power going into lump2
	m_sdTAUC331 = (SIM_DATA)(m_sdMC331)/m_sdWC3;
	//TAUC11=Transit time Node 1 lump 1 (s)
	m_sdTAUC332 = (SIM_DATA)(m_sdMC332)/m_sdWC3;
	//TAUC12=Transit time Node 1 lump 2 (s)
	D(m_sdTC331, (SIM_DATA)(1)/(SIM_DATA)(m_sdMC331)/m_sdCPC3*m_sdFR331*m_sdBR1*m_sdGAMMA95*(V(m_sdTW38, iCol)-V(m_sdTC331, iCol))+(SIM_DATA)(1)/m_sdTAUC331*(V(m_sdTC322, iCol)-V(m_sdTC331, iCol)), iCol);
	D(m_sdTC332, (SIM_DATA)(1)/(SIM_DATA)(m_sdMC332)/m_sdCPC3*m_sdFR332*m_sdBR1*m_sdGAMMA95*(V(m_sdTW38, iCol)-V(m_sdTC331, iCol))+(SIM_DATA)(1)/m_sdTAUC332*(V(m_sdTC331, iCol)-V(m_sdTC332, iCol)), iCol);
	//-- 4
	m_sdMC341 = m_sdMCLUMP3;
	//MC11=Mass of Node 1 lump 1 (kg)
	m_sdMC342 = m_sdMCLUMP3;
	//MC12=Mass of Node 1 lump 2 (kg)
	m_sdFR341 = 0.5;
	//FR11=Fraction of Region 1 power going into lump 1
	m_sdFR342 = 0.5;
	//FR12=Fraction of Region 1 power going into lump2
	m_sdTAUC341 = (SIM_DATA)(m_sdMC341)/m_sdWC3;
	//TAUC11=Transit time Node 1 lump 1 (s)
	m_sdTAUC342 = (SIM_DATA)(m_sdMC342)/m_sdWC3;
	//TAUC12=Transit time Node 1 lump 2 (s)
	D(m_sdTC341, (SIM_DATA)(1)/(SIM_DATA)(m_sdMC341)/m_sdCPC3*m_sdFR341*m_sdBR1*m_sdGAMMA95*(V(m_sdTW37, iCol)-V(m_sdTC341, iCol))+(SIM_DATA)(1)/m_sdTAUC341*(V(m_sdTC332, iCol)-V(m_sdTC341, iCol)), iCol);
	D(m_sdTC342, (SIM_DATA)(1)/(SIM_DATA)(m_sdMC342)/m_sdCPC3*m_sdFR342*m_sdBR1*m_sdGAMMA95*(V(m_sdTW37, iCol)-V(m_sdTC341, iCol))+(SIM_DATA)(1)/m_sdTAUC342*(V(m_sdTC341, iCol)-V(m_sdTC342, iCol)), iCol);
	//-- 5
	m_sdMC351 = m_sdMCLUMP3;
	//MC11=Mass of Node 1 lump 1 (kg)
	m_sdMC352 = m_sdMCLUMP3;
	//MC12=Mass of Node 1 lump 2 (kg)
	m_sdFR351 = 0.5;
	//FR11=Fraction of Region 1 power going into lump 1
	m_sdFR352 = 0.5;
	//FR12=Fraction of Region 1 power going into lump2
	m_sdTAUC351 = (SIM_DATA)(m_sdMC351)/m_sdWC3;
	//TAUC11=Transit time Node 1 lump 1 (s)
	m_sdTAUC352 = (SIM_DATA)(m_sdMC352)/m_sdWC3;
	//TAUC12=Transit time Node 1 lump 2 (s)
	D(m_sdTC351, (SIM_DATA)(1)/(SIM_DATA)(m_sdMC351)/m_sdCPC3*m_sdFR351*m_sdBR1*m_sdGAMMA95*(V(m_sdTW36, iCol)-V(m_sdTC351, iCol))+(SIM_DATA)(1)/m_sdTAUC351*(V(m_sdTC342, iCol)-V(m_sdTC351, iCol)), iCol);
	D(m_sdTC352, (SIM_DATA)(1)/(SIM_DATA)(m_sdMC352)/m_sdCPC3*m_sdFR352*m_sdBR1*m_sdGAMMA95*(V(m_sdTW36, iCol)-V(m_sdTC351, iCol))+(SIM_DATA)(1)/m_sdTAUC352*(V(m_sdTC351, iCol)-V(m_sdTC352, iCol)), iCol);
	//-- 6
	m_sdMC361 = m_sdMCLUMP3;
	//MC11=Mass of Node 1 lump 1 (kg)
	m_sdMC362 = m_sdMCLUMP3;
	//MC12=Mass of Node 1 lump 2 (kg)
	m_sdFR361 = 0.5;
	//FR11=Fraction of Region 1 power going into lump 1
	m_sdFR362 = 0.5;
	//FR12=Fraction of Region 1 power going into lump2
	m_sdTAUC361 = (SIM_DATA)(m_sdMC361)/m_sdWC3;
	//TAUC11=Transit time Node 1 lump 1 (s)
	m_sdTAUC362 = (SIM_DATA)(m_sdMC362)/m_sdWC3;
	//TAUC12=Transit time Node 1 lump 2 (s)
	D(m_sdTC361, (SIM_DATA)(1)/(SIM_DATA)(m_sdMC361)/m_sdCPC3*m_sdFR361*m_sdBR1*m_sdGAMMA95*(V(m_sdTW35, iCol)-V(m_sdTC361, iCol))+(SIM_DATA)(1)/m_sdTAUC361*(V(m_sdTC352, iCol)-V(m_sdTC361, iCol)), iCol);
	D(m_sdTC362, (SIM_DATA)(1)/(SIM_DATA)(m_sdMC362)/m_sdCPC3*m_sdFR362*m_sdBR1*m_sdGAMMA95*(V(m_sdTW35, iCol)-V(m_sdTC361, iCol))+(SIM_DATA)(1)/m_sdTAUC362*(V(m_sdTC361, iCol)-V(m_sdTC362, iCol)), iCol);
	//-- 7
	m_sdMC371 = m_sdMCLUMP3;
	//MC11=Mass of Node 1 lump 1 (kg)
	m_sdMC372 = m_sdMCLUMP3;
	//MC12=Mass of Node 1 lump 2 (kg)
	m_sdFR371 = 0.5;
	//FR11=Fraction of Region 1 power going into lump 1
	m_sdFR372 = 0.5;
	//FR12=Fraction of Region 1 power going into lump2
	m_sdTAUC371 = (SIM_DATA)(m_sdMC371)/m_sdWC3;
	//TAUC11=Transit time Node 1 lump 1 (s)
	m_sdTAUC372 = (SIM_DATA)(m_sdMC372)/m_sdWC3;
	//TAUC12=Transit time Node 1 lump 2 (s)
	D(m_sdTC371, (SIM_DATA)(1)/(SIM_DATA)(m_sdMC371)/m_sdCPC3*m_sdFR371*m_sdBR1*m_sdGAMMA95*(V(m_sdTW34, iCol)-V(m_sdTC371, iCol))+(SIM_DATA)(1)/m_sdTAUC371*(V(m_sdTC362, iCol)-V(m_sdTC371, iCol)), iCol);
	D(m_sdTC372, (SIM_DATA)(1)/(SIM_DATA)(m_sdMC372)/m_sdCPC3*m_sdFR372*m_sdBR1*m_sdGAMMA95*(V(m_sdTW34, iCol)-V(m_sdTC371, iCol))+(SIM_DATA)(1)/m_sdTAUC372*(V(m_sdTC371, iCol)-V(m_sdTC372, iCol)), iCol);
	//-- 8
	m_sdMC381 = m_sdMCLUMP3;
	//MC11=Mass of Node 1 lump 1 (kg)
	m_sdMC382 = m_sdMCLUMP3;
	//MC12=Mass of Node 1 lump 2 (kg)
	m_sdFR381 = 0.5;
	//FR11=Fraction of Region 1 power going into lump 1
	m_sdFR382 = 0.5;
	//FR12=Fraction of Region 1 power going into lump2
	m_sdTAUC381 = (SIM_DATA)(m_sdMC381)/m_sdWC3;
	//TAUC11=Transit time Node 1 lump 1 (s)
	m_sdTAUC382 = (SIM_DATA)(m_sdMC382)/m_sdWC3;
	//TAUC12=Transit time Node 1 lump 2 (s)
	D(m_sdTC381, (SIM_DATA)(1)/(SIM_DATA)(m_sdMC381)/m_sdCPC3*m_sdFR381*m_sdBR1*m_sdGAMMA95*(V(m_sdTW33, iCol)-V(m_sdTC381, iCol))+(SIM_DATA)(1)/m_sdTAUC381*(V(m_sdTC372, iCol)-V(m_sdTC381, iCol)), iCol);
	D(m_sdTC382, (SIM_DATA)(1)/(SIM_DATA)(m_sdMC382)/m_sdCPC3*m_sdFR382*m_sdBR1*m_sdGAMMA95*(V(m_sdTW33, iCol)-V(m_sdTC381, iCol))+(SIM_DATA)(1)/m_sdTAUC382*(V(m_sdTC381, iCol)-V(m_sdTC382, iCol)), iCol);
	//-- 9
	m_sdMC391 = m_sdMCLUMP3;
	//MC11=Mass of Node 1 lump 1 (kg)
	m_sdMC392 = m_sdMCLUMP3;
	//MC12=Mass of Node 1 lump 2 (kg)
	m_sdFR391 = 0.5;
	//FR11=Fraction of Region 1 power going into lump 1
	m_sdFR392 = 0.5;
	//FR12=Fraction of Region 1 power going into lump2
	m_sdTAUC391 = (SIM_DATA)(m_sdMC391)/m_sdWC3;
	//TAUC11=Transit time Node 1 lump 1 (s)
	m_sdTAUC392 = (SIM_DATA)(m_sdMC392)/m_sdWC3;
	//TAUC12=Transit time Node 1 lump 2 (s)
	D(m_sdTC391, (SIM_DATA)(1)/(SIM_DATA)(m_sdMC391)/m_sdCPC3*m_sdFR391*m_sdBR1*m_sdGAMMA95*(V(m_sdTW32, iCol)-V(m_sdTC391, iCol))+(SIM_DATA)(1)/m_sdTAUC391*(V(m_sdTC382, iCol)-V(m_sdTC391, iCol)), iCol);
	D(m_sdTC392, (SIM_DATA)(1)/(SIM_DATA)(m_sdMC392)/m_sdCPC3*m_sdFR392*m_sdBR1*m_sdGAMMA95*(V(m_sdTW32, iCol)-V(m_sdTC391, iCol))+(SIM_DATA)(1)/m_sdTAUC392*(V(m_sdTC391, iCol)-V(m_sdTC392, iCol)), iCol);
	//-- 10
	m_sdMC3101 = m_sdMCLUMP3;
	//MC11=Mass of Node 1 lump 1 (kg)
	m_sdMC3102 = m_sdMCLUMP3;
	//MC12=Mass of Node 1 lump 2 (kg)
	m_sdFR3101 = 0.5;
	//FR11=Fraction of Region 1 power going into lump 1
	m_sdFR3102 = 0.5;
	//FR12=Fraction of Region 1 power going into lump2
	m_sdTAUC3101 = (SIM_DATA)(m_sdMC3101)/m_sdWC3;
	//TAUC11=Transit time Node 1 lump 1 (s)
	m_sdTAUC3102 = (SIM_DATA)(m_sdMC3102)/m_sdWC3;
	//TAUC12=Transit time Node 1 lump 2 (s)
	D(m_sdTC3101, (SIM_DATA)(1)/(SIM_DATA)(m_sdMC3101)/m_sdCPC3*m_sdFR3101*m_sdBR1*m_sdGAMMA95*(V(m_sdTW31, iCol)-V(m_sdTC3101, iCol))+(SIM_DATA)(1)/m_sdTAUC3101*(V(m_sdTC392, iCol)-V(m_sdTC3101, iCol)), iCol);
	D(m_sdTC3102, (SIM_DATA)(1)/(SIM_DATA)(m_sdMC3102)/m_sdCPC3*m_sdFR3102*m_sdBR1*m_sdGAMMA95*(V(m_sdTW31, iCol)-V(m_sdTC3101, iCol))+(SIM_DATA)(1)/m_sdTAUC3102*(V(m_sdTC3101, iCol)-V(m_sdTC3102, iCol)), iCol);
	//-- ====================================================
	//-- Buoyancy
	//-- ====================================================
	//-- *Equation 44
	m_sdSUML1 = 0.5*m_sdLI*(m_sdRL11+m_sdRL21+m_sdRL31+m_sdRL41+m_sdRL51+m_sdRL61+m_sdRL71+m_sdRL81+m_sdRL91+m_sdRL101)*9.8;
	m_sdSUML2 = 0.5*m_sdLI*(m_sdRL12+m_sdRL22+m_sdRL32+m_sdRL42+m_sdRL52+m_sdRL62+m_sdRL72+m_sdRL82+m_sdRL92+m_sdRL102)*9.8;
	m_sdSUML21 = 0.5*m_sdLI*(m_sdRL211+m_sdRL221+m_sdRL231+m_sdRL241+m_sdRL251+m_sdRL261+m_sdRL271+m_sdRL281+m_sdRL291+m_sdRL2101)*9.8;
	m_sdSUML22 = 0.5*m_sdLI*(m_sdRL212+m_sdRL222+m_sdRL232+m_sdRL242+m_sdRL252+m_sdRL262+m_sdRL272+m_sdRL282+m_sdRL292+m_sdRL2102)*9.8;
	m_sdSUML31 = 0.5*m_sdLI*(m_sdRL311+m_sdRL321+m_sdRL331+m_sdRL341+m_sdRL351+m_sdRL361+m_sdRL371+m_sdRL381+m_sdRL391+m_sdRL3101)*9.8;
	m_sdSUML32 = 0.5*m_sdLI*(m_sdRL312+m_sdRL322+m_sdRL332+m_sdRL342+m_sdRL352+m_sdRL362+m_sdRL372+m_sdRL382+m_sdRL392+m_sdRL3102)*9.8;
	m_sdSML1 = (m_sdSUML1+m_sdSUML2)*(SIM_DATA)(m_sdALOOP1)/m_sdALOOPT;
	m_sdSML2 = (m_sdSUML21+m_sdSUML22)*(SIM_DATA)(m_sdALOOP2)/m_sdALOOPT;
	m_sdSML3 = (m_sdSUML31+m_sdSUML32)*(SIM_DATA)(m_sdALOOP3)/m_sdALOOPT;
	m_sdSUMF = m_sdLI*(m_sdRF1+m_sdRF2+m_sdRF3+m_sdRF4+m_sdRF5+m_sdRF6+m_sdRF7+m_sdRF8+m_sdRF9+m_sdRF10)*9.8;
	m_sdFRIC = 1.0;
	m_sdDISP = m_sdFRIC*(SIM_DATA)(m_sdHCORE)/2*((SIM_DATA)(1)/(SIM_DATA)(0.16)/(SIM_DATA)(m_sdAC)/(SIM_DATA)(m_sdAC)/m_sdRF5+(SIM_DATA)(1)/(SIM_DATA)(m_sdBL)/(SIM_DATA)(m_sdAL)/(SIM_DATA)(m_sdAL)/m_sdRL51);
	D(m_sdWF, (SIM_DATA)(1)/((SIM_DATA)(m_sdHCORE)/m_sdALOOPT+(SIM_DATA)(m_sdHCORE)/m_sdAC)*(m_sdSML1+m_sdSML2+m_sdSML3-m_sdSUMF-V(m_sdWF, iCol)*(SIM_DATA)(V(m_sdWF, iCol))/(SIM_DATA)(2)/m_sdFuelDen*((SIM_DATA)(1)/(SIM_DATA)(m_sdALOOPT)/m_sdALOOPT-(SIM_DATA)(1)/(SIM_DATA)(m_sdAC)/m_sdAC)), iCol);
	//-- ===============================
	//-- Energy balance
	//-- ---------------------------
	m_sdT1AV = m_sdWFF1*V(m_sdTL102, iCol)+m_sdWFF2*V(m_sdTL2102, iCol)+m_sdWFF3*V(m_sdTL3102, iCol);
	m_sdPBF = V(m_sdWF, iCol)*m_sdCV*(V(m_sdTEMP10, iCol)-m_sdT1AV)*1000;
	m_sdPBL1 = m_sdWF1*m_sdCV*(V(m_sdTEMP10, iCol)-V(m_sdTL102, iCol))*1000;
	m_sdPBL2 = m_sdWF2*m_sdCV*(V(m_sdTEMP10, iCol)-V(m_sdTL2102, iCol))*1000;
	m_sdPBL3 = m_sdWF3*m_sdCV*(V(m_sdTEMP10, iCol)-V(m_sdTL3102, iCol))*1000;
	m_sdPBLT = m_sdPBL1+m_sdPBL2+m_sdPBL3;
	m_sdPCL1 = m_sdWC*m_sdCPC*(V(m_sdTC102, iCol)-m_sdTC1IN)*1000;
	m_sdPCL2 = m_sdWC2*m_sdCPC2*(V(m_sdTC2102, iCol)-m_sdTC2IN)*1000;
	m_sdPCL3 = m_sdWC3*m_sdCPC3*(V(m_sdTC3102, iCol)-m_sdTC3IN)*1000;
	m_sdPCLT = m_sdPCL1+m_sdPCL2+m_sdPCL3;
	//-- *************************
	//-- RADIOLYTIC GAS MODEL
	//--
	//-- gastool
	//-- *EQUATION 14
	//-- ROH2A,ROO2A=Average density of H2,O2 in core (kg/m^3)
	m_sdROH2A = (SIM_DATA)(m_sdP)/(SIM_DATA)(((SIM_DATA)(m_sdRG)/m_sdHM))/(m_sdTEMP+273);
	m_sdROO2A = (SIM_DATA)(m_sdP)/(SIM_DATA)(((SIM_DATA)(m_sdRG)/m_sdOM))/(m_sdTEMP+273);
	//-- --------------------------------------------
	//--  REGIONS 1-10, 1=bottom
	//-- ---------------------------------------------
	//-- 1
	//-- *EQUATION 29
	m_sdROH21 = (SIM_DATA)(m_sdP)/(SIM_DATA)(((SIM_DATA)(m_sdRG)/m_sdHM))/(V(m_sdTEMP1, iCol)+273);
	m_sdROO21 = (SIM_DATA)(m_sdP)/(SIM_DATA)(((SIM_DATA)(m_sdRG)/m_sdOM))/(V(m_sdTEMP1, iCol)+273);
	//-- GH2,GO2=Dissolved gas production rates
	//-- *EQUATIONS 10-13
	m_sdGB = (1+0.25*swtch((SIM_DATA)(m_sdT)-500));
	m_sdGH2 = (SIM_DATA)(m_sdMH2)/0.5;
	//(kg/MJ)  H2
	m_sdGO2 = (SIM_DATA)(m_sdMO2)/0.5;
	//(kg/MJ) O2
	//--  XH2(i),XO2(i)=Dissolved gas quality factor in region i
	m_sdXH210 = (SIM_DATA)(V(m_sdMDH210, iCol))/(V(m_sdMDH210, iCol)+m_sdEM);
	m_sdXO210 = (SIM_DATA)(V(m_sdMDO210, iCol))/(V(m_sdMDO210, iCol)+m_sdEM);
	m_sdXH21 = (SIM_DATA)(V(m_sdMDH21, iCol))/(V(m_sdMDH21, iCol)+m_sdEM);
	m_sdXO21 = (SIM_DATA)(V(m_sdMDO21, iCol))/(V(m_sdMDO21, iCol)+m_sdEM);
	//-- ED(i)=Energy deposited in region I (MJ)
	m_sdED1 = m_sdPCF*m_sdFRA1*(m_sdENP-m_sdEN0);
	//-- MDH2(i),MDO2(i)=Dissolved gas mass in region i
	D(m_sdMDH21, m_sdED1*m_sdGH2+V(m_sdWF, iCol)*(m_sdXH210-m_sdXH21), iCol);
	D(m_sdMDO21, m_sdED1*m_sdGO2+V(m_sdWF, iCol)*(m_sdXO210-m_sdXO21), iCol);
	//--  XTO=O2 Threshold quality factor
	m_sdXTO = 0.001;
	//-- G(i)H=H2 gas generation rate (m^3/MJ) in region i
	//-- *EQUATIONS 8,9
	m_sdG1H = (SIM_DATA)(m_sdGH2)/m_sdROH21*swtch(m_sdXH21-m_sdXT);
	//-- VH(i)DOT=H2 gas rate of change in region i
	//--  *EQUATIONS 7,30
	m_sdVH1DOT = m_sdG1H*m_sdPCF*m_sdFRA1*(m_sdENP-m_sdEN0)-V(m_sdVGH1, iCol)*(SIM_DATA)(1)/m_sdTAU-(SIM_DATA)(m_sdPDOT)/m_sdP*V(m_sdVGH1, iCol);
	//-- VGH(i)=Volume of H2 gas in region i (m^3)
	D(m_sdVGH1, m_sdVH1DOT, iCol);
	//-- G(i)O=O2 gas generation rate (m^3/MJ) in region i
	//-- *EQUATIONS 8,9
	m_sdG1O = (SIM_DATA)(m_sdGO2)/m_sdROO21*swtch(m_sdXO21-m_sdXTO);
	//-- VO(i)DOT=O2 gas rate of change in region i
	//-- VGO(i)=Volume of O2 gas in region i (m^3)
	//-- *EQUATION 7,30
	m_sdVO1DOT = m_sdG1O*m_sdPCF*m_sdFRA1*(m_sdENP-m_sdEN0)-V(m_sdVGO1, iCol)*(SIM_DATA)(1)/m_sdTAU2-(SIM_DATA)(m_sdPDOT)/m_sdP*V(m_sdVGO1, iCol);
	D(m_sdVGO1, m_sdVO1DOT, iCol);
	//-- 2
	m_sdROH22 = (SIM_DATA)(m_sdP)/(SIM_DATA)(((SIM_DATA)(m_sdRG)/m_sdHM))/(V(m_sdTEMP2, iCol)+273);
	m_sdROO22 = (SIM_DATA)(m_sdP)/(SIM_DATA)(((SIM_DATA)(m_sdRG)/m_sdOM))/(V(m_sdTEMP2, iCol)+273);
	m_sdXH22 = (SIM_DATA)(V(m_sdMDH22, iCol))/(V(m_sdMDH22, iCol)+m_sdEM);
	m_sdXO22 = (SIM_DATA)(V(m_sdMDO22, iCol))/(V(m_sdMDO22, iCol)+m_sdEM);
	m_sdED2 = m_sdPCF*m_sdFRA2*(m_sdENP-m_sdEN0);
	D(m_sdMDH22, m_sdED2*m_sdGH2+V(m_sdWF, iCol)*(m_sdXH21-m_sdXH22), iCol);
	D(m_sdMDO22, m_sdED2*m_sdGO2+V(m_sdWF, iCol)*(m_sdXO21-m_sdXO22), iCol);
	m_sdG2H = (SIM_DATA)(m_sdGH2)/m_sdROH22*swtch(m_sdXH22-m_sdXT);
	m_sdVH2DOT = m_sdG2H*m_sdPCF*m_sdFRA2*(m_sdENP-m_sdEN0)-(V(m_sdVGH2, iCol)-V(m_sdVGH1, iCol))*(SIM_DATA)(1)/m_sdTAU-(SIM_DATA)(m_sdPDOT)/m_sdP*V(m_sdVGH2, iCol);
	D(m_sdVGH2, m_sdVH2DOT, iCol);
	m_sdG2O = (SIM_DATA)(m_sdGO2)/m_sdROO22*swtch(m_sdXO22-m_sdXTO);
	m_sdVO2DOT = m_sdG2O*m_sdPCF*m_sdFRA2*(m_sdENP-m_sdEN0)-(V(m_sdVGO2, iCol)-V(m_sdVGO1, iCol))*(SIM_DATA)(1)/m_sdTAU2-(SIM_DATA)(m_sdPDOT)/m_sdP*V(m_sdVGO2, iCol);
	D(m_sdVGO2, m_sdVO2DOT, iCol);
	//-- 3
	m_sdROH23 = (SIM_DATA)(m_sdP)/(SIM_DATA)(((SIM_DATA)(m_sdRG)/m_sdHM))/(V(m_sdTEMP3, iCol)+273);
	m_sdROO23 = (SIM_DATA)(m_sdP)/(SIM_DATA)(((SIM_DATA)(m_sdRG)/m_sdOM))/(V(m_sdTEMP3, iCol)+273);
	m_sdXH23 = (SIM_DATA)(V(m_sdMDH23, iCol))/(V(m_sdMDH23, iCol)+m_sdEM);
	m_sdXO23 = (SIM_DATA)(V(m_sdMDO23, iCol))/(V(m_sdMDO23, iCol)+m_sdEM);
	m_sdED3 = m_sdPCF*m_sdFRA3*(m_sdENP-m_sdEN0);
	D(m_sdMDH23, m_sdED3*m_sdGH2+V(m_sdWF, iCol)*(m_sdXH22-m_sdXH23), iCol);
	D(m_sdMDO23, m_sdED3*m_sdGO2+V(m_sdWF, iCol)*(m_sdXO22-m_sdXO23), iCol);
	m_sdG3H = (SIM_DATA)(m_sdGH2)/m_sdROH23*swtch(m_sdXH23-m_sdXT);
	m_sdVH3DOT = m_sdG3H*m_sdPCF*m_sdFRA3*(m_sdENP-m_sdEN0)-(V(m_sdVGH3, iCol)-V(m_sdVGH2, iCol))*(SIM_DATA)(1)/m_sdTAU-(SIM_DATA)(m_sdPDOT)/m_sdP*V(m_sdVGH3, iCol);
	D(m_sdVGH3, m_sdVH3DOT, iCol);
	m_sdG3O = (SIM_DATA)(m_sdGO2)/m_sdROO23*swtch(m_sdXO23-m_sdXTO);
	m_sdVO3DOT = m_sdG3O*m_sdPCF*m_sdFRA3*(m_sdENP-m_sdEN0)-(V(m_sdVGO3, iCol)-V(m_sdVGO2, iCol))*(SIM_DATA)(1)/m_sdTAU2-(SIM_DATA)(m_sdPDOT)/m_sdP*V(m_sdVGO3, iCol);
	D(m_sdVGO3, m_sdVO3DOT, iCol);
	//-- 4
	m_sdROH24 = (SIM_DATA)(m_sdP)/(SIM_DATA)(((SIM_DATA)(m_sdRG)/m_sdHM))/(V(m_sdTEMP4, iCol)+273);
	m_sdROO24 = (SIM_DATA)(m_sdP)/(SIM_DATA)(((SIM_DATA)(m_sdRG)/m_sdOM))/(V(m_sdTEMP4, iCol)+273);
	m_sdXH24 = (SIM_DATA)(V(m_sdMDH24, iCol))/(V(m_sdMDH24, iCol)+m_sdEM);
	m_sdXO24 = (SIM_DATA)(V(m_sdMDO24, iCol))/(V(m_sdMDO24, iCol)+m_sdEM);
	m_sdED4 = m_sdPCF*m_sdFRA4*(m_sdENP-m_sdEN0);
	D(m_sdMDH24, m_sdED4*m_sdGH2+V(m_sdWF, iCol)*(m_sdXH23-m_sdXH24), iCol);
	D(m_sdMDO24, m_sdED4*m_sdGO2+V(m_sdWF, iCol)*(m_sdXO23-m_sdXO24), iCol);
	m_sdG4H = (SIM_DATA)(m_sdGH2)/m_sdROH24*swtch(m_sdXH24-m_sdXT);
	m_sdVH4DOT = m_sdG4H*m_sdPCF*m_sdFRA4*(m_sdENP-m_sdEN0)-(V(m_sdVGH4, iCol)-V(m_sdVGH3, iCol))*(SIM_DATA)(1)/m_sdTAU-(SIM_DATA)(m_sdPDOT)/m_sdP*V(m_sdVGH4, iCol);
	D(m_sdVGH4, m_sdVH4DOT, iCol);
	m_sdG4O = (SIM_DATA)(m_sdGO2)/m_sdROO24*swtch(m_sdXO24-m_sdXTO);
	m_sdVO4DOT = m_sdG4O*m_sdPCF*m_sdFRA4*(m_sdENP-m_sdEN0)-(V(m_sdVGO4, iCol)-V(m_sdVGO3, iCol))*(SIM_DATA)(1)/m_sdTAU2-(SIM_DATA)(m_sdPDOT)/m_sdP*V(m_sdVGO4, iCol);
	D(m_sdVGO4, m_sdVO4DOT, iCol);
	//-- 5
	m_sdROH25 = (SIM_DATA)(m_sdP)/(SIM_DATA)(((SIM_DATA)(m_sdRG)/m_sdHM))/(V(m_sdTEMP5, iCol)+273);
	m_sdROO25 = (SIM_DATA)(m_sdP)/(SIM_DATA)(((SIM_DATA)(m_sdRG)/m_sdOM))/(V(m_sdTEMP5, iCol)+273);
	m_sdXH25 = (SIM_DATA)(V(m_sdMDH25, iCol))/(V(m_sdMDH25, iCol)+m_sdEM);
	m_sdXO25 = (SIM_DATA)(V(m_sdMDO25, iCol))/(V(m_sdMDO25, iCol)+m_sdEM);
	m_sdED5 = m_sdPCF*m_sdFRA5*(m_sdENP-m_sdEN0);
	D(m_sdMDH25, m_sdED5*m_sdGH2+V(m_sdWF, iCol)*(m_sdXH24-m_sdXH25), iCol);
	D(m_sdMDO25, m_sdED5*m_sdGO2+V(m_sdWF, iCol)*(m_sdXO24-m_sdXO25), iCol);
	m_sdG5H = (SIM_DATA)(m_sdGH2)/m_sdROH25*swtch(m_sdXH25-m_sdXT);
	m_sdVH5DOT = m_sdG5H*m_sdPCF*m_sdFRA5*(m_sdENP-m_sdEN0)-(V(m_sdVGH5, iCol)-V(m_sdVGH4, iCol))*(SIM_DATA)(1)/m_sdTAU-(SIM_DATA)(m_sdPDOT)/m_sdP*V(m_sdVGH5, iCol);
	D(m_sdVGH5, m_sdVH5DOT, iCol);
	m_sdG5O = (SIM_DATA)(m_sdGO2)/m_sdROO25*swtch(m_sdXO25-m_sdXTO);
	m_sdVO5DOT = m_sdG5O*m_sdPCF*m_sdFRA5*(m_sdENP-m_sdEN0)-(V(m_sdVGO5, iCol)-V(m_sdVGO4, iCol))*(SIM_DATA)(1)/m_sdTAU2-(SIM_DATA)(m_sdPDOT)/m_sdP*V(m_sdVGO5, iCol);
	D(m_sdVGO5, m_sdVO5DOT, iCol);
	//-- 6
	m_sdROH26 = (SIM_DATA)(m_sdP)/(SIM_DATA)(((SIM_DATA)(m_sdRG)/m_sdHM))/(V(m_sdTEMP6, iCol)+273);
	m_sdROO26 = (SIM_DATA)(m_sdP)/(SIM_DATA)(((SIM_DATA)(m_sdRG)/m_sdOM))/(V(m_sdTEMP6, iCol)+273);
	m_sdXH26 = (SIM_DATA)(V(m_sdMDH26, iCol))/(V(m_sdMDH26, iCol)+m_sdEM);
	m_sdXO26 = (SIM_DATA)(V(m_sdMDO26, iCol))/(V(m_sdMDO26, iCol)+m_sdEM);
	m_sdED6 = m_sdPCF*m_sdFRA6*(m_sdENP-m_sdEN0);
	D(m_sdMDH26, m_sdED6*m_sdGH2+V(m_sdWF, iCol)*(m_sdXH25-m_sdXH26), iCol);
	D(m_sdMDO26, m_sdED6*m_sdGO2+V(m_sdWF, iCol)*(m_sdXO25-m_sdXO26), iCol);
	m_sdG6H = (SIM_DATA)(m_sdGH2)/m_sdROH26*swtch(m_sdXH26-m_sdXT);
	m_sdVH6DOT = m_sdG6H*m_sdPCF*m_sdFRA6*(m_sdENP-m_sdEN0)-(V(m_sdVGH6, iCol)-V(m_sdVGH5, iCol))*(SIM_DATA)(1)/m_sdTAU-(SIM_DATA)(m_sdPDOT)/m_sdP*V(m_sdVGH6, iCol);
	D(m_sdVGH6, m_sdVH6DOT, iCol);
	m_sdG6O = (SIM_DATA)(m_sdGO2)/m_sdROO26*swtch(m_sdXO26-m_sdXTO);
	m_sdVO6DOT = m_sdG6O*m_sdPCF*m_sdFRA6*(m_sdENP-m_sdEN0)-(V(m_sdVGO6, iCol)-V(m_sdVGO5, iCol))*(SIM_DATA)(1)/m_sdTAU2-(SIM_DATA)(m_sdPDOT)/m_sdP*V(m_sdVGO6, iCol);
	D(m_sdVGO6, m_sdVO6DOT, iCol);
	//-- 7
	m_sdROH27 = (SIM_DATA)(m_sdP)/(SIM_DATA)(((SIM_DATA)(m_sdRG)/m_sdHM))/(V(m_sdTEMP7, iCol)+273);
	m_sdROO27 = (SIM_DATA)(m_sdP)/(SIM_DATA)(((SIM_DATA)(m_sdRG)/m_sdOM))/(V(m_sdTEMP7, iCol)+273);
	m_sdXH27 = (SIM_DATA)(V(m_sdMDH27, iCol))/(V(m_sdMDH27, iCol)+m_sdEM);
	m_sdXO27 = (SIM_DATA)(V(m_sdMDO27, iCol))/(V(m_sdMDO27, iCol)+m_sdEM);
	m_sdED7 = m_sdPCF*m_sdFRA7*(m_sdENP-m_sdEN0);
	D(m_sdMDH27, m_sdED7*m_sdGH2+V(m_sdWF, iCol)*(m_sdXH26-m_sdXH27), iCol);
	D(m_sdMDO27, m_sdED7*m_sdGO2+V(m_sdWF, iCol)*(m_sdXO26-m_sdXO27), iCol);
	m_sdG7H = (SIM_DATA)(m_sdGH2)/m_sdROH27*swtch(m_sdXH27-m_sdXT);
	m_sdVH7DOT = m_sdG7H*m_sdPCF*m_sdFRA7*(m_sdENP-m_sdEN0)-(V(m_sdVGH7, iCol)-V(m_sdVGH6, iCol))*(SIM_DATA)(1)/m_sdTAU-(SIM_DATA)(m_sdPDOT)/m_sdP*V(m_sdVGH7, iCol);
	D(m_sdVGH7, m_sdVH7DOT, iCol);
	m_sdG7O = (SIM_DATA)(m_sdGO2)/m_sdROO27*swtch(m_sdXO27-m_sdXTO);
	m_sdVO7DOT = m_sdG7O*m_sdPCF*m_sdFRA7*(m_sdENP-m_sdEN0)-(V(m_sdVGO7, iCol)-V(m_sdVGO6, iCol))*(SIM_DATA)(1)/m_sdTAU2-(SIM_DATA)(m_sdPDOT)/m_sdP*V(m_sdVGO7, iCol);
	D(m_sdVGO7, m_sdVO7DOT, iCol);
	//-- 8
	m_sdROH28 = (SIM_DATA)(m_sdP)/(SIM_DATA)(((SIM_DATA)(m_sdRG)/m_sdHM))/(V(m_sdTEMP8, iCol)+273);
	m_sdROO28 = (SIM_DATA)(m_sdP)/(SIM_DATA)(((SIM_DATA)(m_sdRG)/m_sdOM))/(V(m_sdTEMP8, iCol)+273);
	m_sdXH28 = (SIM_DATA)(V(m_sdMDH28, iCol))/(V(m_sdMDH28, iCol)+m_sdEM);
	m_sdXO28 = (SIM_DATA)(V(m_sdMDO28, iCol))/(V(m_sdMDO28, iCol)+m_sdEM);
	m_sdED8 = m_sdPCF*m_sdFRA8*(m_sdENP-m_sdEN0);
	D(m_sdMDH28, m_sdED8*m_sdGH2+V(m_sdWF, iCol)*(m_sdXH27-m_sdXH28), iCol);
	D(m_sdMDO28, m_sdED8*m_sdGO2+V(m_sdWF, iCol)*(m_sdXO27-m_sdXO28), iCol);
	m_sdG8H = (SIM_DATA)(m_sdGH2)/m_sdROH28*swtch(m_sdXH28-m_sdXT);
	m_sdVH8DOT = m_sdG8H*m_sdPCF*m_sdFRA8*(m_sdENP-m_sdEN0)-(V(m_sdVGH8, iCol)-V(m_sdVGH7, iCol))*(SIM_DATA)(1)/m_sdTAU-(SIM_DATA)(m_sdPDOT)/m_sdP*V(m_sdVGH8, iCol);
	D(m_sdVGH8, m_sdVH8DOT, iCol);
	m_sdG8O = (SIM_DATA)(m_sdGO2)/m_sdROO28*swtch(m_sdXO28-m_sdXTO);
	m_sdVO8DOT = m_sdG8O*m_sdPCF*m_sdFRA8*(m_sdENP-m_sdEN0)-(V(m_sdVGO8, iCol)-V(m_sdVGO7, iCol))*(SIM_DATA)(1)/m_sdTAU2-(SIM_DATA)(m_sdPDOT)/m_sdP*V(m_sdVGO8, iCol);
	D(m_sdVGO8, m_sdVO8DOT, iCol);
	//-- 9
	m_sdROH29 = (SIM_DATA)(m_sdP)/(SIM_DATA)(((SIM_DATA)(m_sdRG)/m_sdHM))/(V(m_sdTEMP9, iCol)+273);
	m_sdROO29 = (SIM_DATA)(m_sdP)/(SIM_DATA)(((SIM_DATA)(m_sdRG)/m_sdOM))/(V(m_sdTEMP9, iCol)+273);
	m_sdXH29 = (SIM_DATA)(V(m_sdMDH29, iCol))/(V(m_sdMDH29, iCol)+m_sdEM);
	m_sdXO29 = (SIM_DATA)(V(m_sdMDO29, iCol))/(V(m_sdMDO29, iCol)+m_sdEM);
	m_sdED9 = m_sdPCF*m_sdFRA9*(m_sdENP-m_sdEN0);
	D(m_sdMDH29, m_sdED9*m_sdGH2+V(m_sdWF, iCol)*(m_sdXH28-m_sdXH29), iCol);
	D(m_sdMDO29, m_sdED9*m_sdGO2+V(m_sdWF, iCol)*(m_sdXO28-m_sdXO29), iCol);
	m_sdG9H = (SIM_DATA)(m_sdGH2)/m_sdROH29*swtch(m_sdXH29-m_sdXT);
	m_sdVH9DOT = m_sdG9H*m_sdPCF*m_sdFRA9*(m_sdENP-m_sdEN0)-(V(m_sdVGH9, iCol)-V(m_sdVGH8, iCol))*(SIM_DATA)(1)/m_sdTAU-(SIM_DATA)(m_sdPDOT)/m_sdP*V(m_sdVGH9, iCol);
	D(m_sdVGH9, m_sdVH9DOT, iCol);
	m_sdG9O = (SIM_DATA)(m_sdGO2)/m_sdROO29*swtch(m_sdXO29-m_sdXTO);
	m_sdVO9DOT = m_sdG9O*m_sdPCF*m_sdFRA9*(m_sdENP-m_sdEN0)-(V(m_sdVGO9, iCol)-V(m_sdVGO8, iCol))*(SIM_DATA)(1)/m_sdTAU2-(SIM_DATA)(m_sdPDOT)/m_sdP*V(m_sdVGO9, iCol);
	D(m_sdVGO9, m_sdVO9DOT, iCol);
	//-- 10
	m_sdROH210 = (SIM_DATA)(m_sdP)/(SIM_DATA)(((SIM_DATA)(m_sdRG)/m_sdHM))/(V(m_sdTEMP10, iCol)+273);
	m_sdROO210 = (SIM_DATA)(m_sdP)/(SIM_DATA)(((SIM_DATA)(m_sdRG)/m_sdOM))/(V(m_sdTEMP10, iCol)+273);
	m_sdED10 = m_sdPCF*m_sdFRA10*(m_sdENP-m_sdEN0);
	D(m_sdMDH210, m_sdED10*m_sdGH2+V(m_sdWF, iCol)*(m_sdXH29-m_sdXH210), iCol);
	D(m_sdMDO210, m_sdED10*m_sdGO2+V(m_sdWF, iCol)*(m_sdXO29-m_sdXO210), iCol);
	m_sdG10H = (SIM_DATA)(m_sdGH2)/m_sdROH210*swtch(m_sdXH210-m_sdXT);
	m_sdVH10DOT = m_sdG10H*m_sdPCF*m_sdFRA10*(m_sdENP-m_sdEN0)-(V(m_sdVGH10, iCol)-V(m_sdVGH9, iCol))*(SIM_DATA)(1)/m_sdTAU-(SIM_DATA)(m_sdPDOT)/m_sdP*V(m_sdVGH10, iCol);
	D(m_sdVGH10, m_sdVH10DOT, iCol);
	m_sdG10O = (SIM_DATA)(m_sdGO2)/m_sdROO210*swtch(m_sdXO210-m_sdXTO);
	m_sdVO10DOT = m_sdG10O*m_sdPCF*m_sdFRA10*(m_sdENP-m_sdEN0)-(V(m_sdVGO10, iCol)-V(m_sdVGO9, iCol))*(SIM_DATA)(1)/m_sdTAU2-(SIM_DATA)(m_sdPDOT)/m_sdP*V(m_sdVGO10, iCol);
	D(m_sdVGO10, m_sdVO10DOT, iCol);
	//--  cccccccccccccccccccccccccccccccccccccccccccccccccccccccccccccc
	//-- Check core gas mass balance (WMUIN=0)
	m_sdHPDOT = m_sdGH2*m_sdPCF*(m_sdENP-m_sdEN0)*swtch(m_sdXH210-m_sdXT);
	m_sdOPDOT = m_sdGO2*m_sdPCF*(m_sdENP-m_sdEN0)*swtch(m_sdXO210-m_sdXTO);
	D(m_sdHP, m_sdHPDOT, iCol);
	D(m_sdOP, m_sdOPDOT, iCol);
	//-- cccccccccccccccccccccccccccccccccccccccccccccccccccccccccccccccccccc
	m_sdVHTOT = V(m_sdVGH1, iCol)+V(m_sdVGH2, iCol)+V(m_sdVGH3, iCol)+V(m_sdVGH4, iCol)+V(m_sdVGH5, iCol)+V(m_sdVGH6, iCol)+V(m_sdVGH7, iCol)+V(m_sdVGH8, iCol)+V(m_sdVGH9, iCol)+V(m_sdVGH10, iCol);
	m_sdVOTOT = V(m_sdVGO1, iCol)+V(m_sdVGO2, iCol)+V(m_sdVGO3, iCol)+V(m_sdVGO4, iCol)+V(m_sdVGO5, iCol)+V(m_sdVGO6, iCol)+V(m_sdVGO7, iCol)+V(m_sdVGO8, iCol)+V(m_sdVGO9, iCol)+V(m_sdVGO10, iCol);
	//-- *******************
	//-- PLENUM MODEL
	//--
	//--  plntool
	//-- *******************
	//-- ---------- Control block for plenum pressure ---------------
	m_sdPTRIP = 1.5e+05;
	m_sdPSIG = swtch(m_sdP-m_sdPTRIP);
	D(m_sdVOGO, 1.0e-04*m_sdPSIG, iCol);
	D(m_sdVNI, 1.0e-04*m_sdPSIG, iCol);
	//-- ==================
	//-- Plenum Volume
	//-- ==================
	//-- Water make-up model
	//-- WMUIN=water mass flow in (kg/s)
	m_sdWMUIN = 0.0;
	//-- WMUDOT=time rate of change of makeup water mass
	//--  *EQUATION 24
	m_sdWMUDOT = m_sdWMUIN-m_sdROH2A*(SIM_DATA)(V(m_sdVGH10, iCol))/m_sdTAU-m_sdROO2A*(SIM_DATA)(V(m_sdVGO10, iCol))/m_sdTAU2;
	//--  WMU=Mass of makeup water (kg)
	D(m_sdWMU, m_sdWMUDOT, iCol);
	//-- ROW=Density of makeup water (kg/m^3)
	m_sdROW = 1000.0;
	//-- VFDOT=time rate of change of makeup water volume
	//-- *EQUATION 25
	m_sdVFDOT = (SIM_DATA)(m_sdWMUDOT)/m_sdROW;
	//-- ----------------------------
	//-- Fuel Expansion
	//-- -----------------------------
	//-- VHDT,VODT=Radiolytic gas volume derivatives in core
	m_sdVHDT1 = m_sdVH1DOT+m_sdVH2DOT+m_sdVH3DOT+m_sdVH4DOT+m_sdVH5DOT;
	m_sdVHDT2 = m_sdVH6DOT+m_sdVH7DOT+m_sdVH8DOT+m_sdVH9DOT+m_sdVH10DOT;
	m_sdVODT1 = m_sdVO1DOT+m_sdVO2DOT+m_sdVO3DOT+m_sdVO4DOT+m_sdVO5DOT;
	m_sdVODT2 = m_sdVO6DOT+m_sdVO7DOT+m_sdVO8DOT+m_sdVO9DOT+m_sdVO10DOT;
	//-- VPDOT=time rate of change of plenum volume
	//--  *EQUATION 21
	m_sdVPDOT = -(m_sdVHDT1+m_sdVHDT2+m_sdVODT1+m_sdVODT2+m_sdVFT);
	//-- VP=Volume of plenum (m^3)
	D(m_sdVP, m_sdVPDOT, iCol);
	//-- ccccccccccccccccccccccccccccccccccccccccccccccccccccccccccccccccccccccccc
	//--  Check Plenum Volume
	//--  VCTOT=Total Core Volume (m^3)
	m_sdVCTOT = m_sdVFUEL+m_sdVHTOT+m_sdVOTOT;
	//-- cccccccccccccccccccccccccccccccccccccccccccccccccccccccccccccccccccccccc
	//-- =======================
	//-- Plenum energy equation
	//-- ======================
	//--  CPH,CPO,CPN=Specific heat (constant pressure) (J/K/kg)
	m_sdCPH = (SIM_DATA)(7)/2*(SIM_DATA)(m_sdRG)/m_sdHM;
	m_sdCPO = (SIM_DATA)(7)/2*(SIM_DATA)(m_sdRG)/m_sdOM;
	m_sdCPN = (SIM_DATA)(7)/2*(SIM_DATA)(m_sdRG)/m_sdNM;
	m_sdCPS = 1900.0;
	//J/kg/C  specific heat of steam
	//--  Gas Enthalpy at 20 C (J/kg)
	m_sdT20 = 20.0;
	m_sdENH20 = (SIM_DATA)(8351)/m_sdHM;
	m_sdENO20 = (SIM_DATA)(8560)/m_sdOM;
	m_sdENN20 = (SIM_DATA)(8551)/m_sdNM;
	//-- Gas Enthalpy entering plenum
	m_sdENH1 = m_sdENH20+m_sdCPH*(m_sdTEMP-m_sdT20);
	m_sdENO1 = m_sdENO20+m_sdCPO*(m_sdTEMP-m_sdT20);
	m_sdENO2 = m_sdENO20+m_sdCPO*(m_sdT0-m_sdT20);
	m_sdENN2 = m_sdENN20+m_sdCPN*(m_sdT0-m_sdT20);
	//-- Gas Enthalpy exiting plenum
	m_sdENH5 = m_sdENH20+m_sdCPH*((V(m_sdTP, iCol)-273)-m_sdT20);
	m_sdENO5 = m_sdENO20+m_sdCPO*((V(m_sdTP, iCol)-273)-m_sdT20);
	m_sdENN5 = m_sdENN20+m_sdCPN*((V(m_sdTP, iCol)-273)-m_sdT20);
	//-- *EQUATION 16
	m_sdRHY = (SIM_DATA)(V(m_sdMHY, iCol))/V(m_sdVP, iCol);
	//RHY=H2 density in plenum (kg/m^3)
	m_sdROX = (SIM_DATA)(V(m_sdMOX, iCol))/V(m_sdVP, iCol);
	//ROX=O2 density in plenum (kg/m^3)
	m_sdRN = (SIM_DATA)(V(m_sdMN, iCol))/V(m_sdVP, iCol);
	//RN=N2 density in plenum (kg/m^3)
	//-- Energy flow in=HFH+HFO+HFN
	//-- *EQUATION 34
	m_sdHFH = m_sdROH2A*(SIM_DATA)(V(m_sdVGH10, iCol))/m_sdTAU*m_sdENH1;
	m_sdHFO = m_sdROO2A*(SIM_DATA)(V(m_sdVGO10, iCol))/m_sdTAU2*m_sdENO1+m_sdROI*V(m_sdVNI, iCol)*m_sdENO2*(1-swtch(m_sdAIR));
	m_sdHFN = m_sdRNI*V(m_sdVNI, iCol)*m_sdENN2;
	//-- Energy flow out=HFOUT
	//-- *EQUATION 35
	m_sdHFOUT = (m_sdRHY*m_sdENH5+m_sdROX*m_sdENO5+m_sdRN*m_sdENN5)*V(m_sdVOGO, iCol);
	//-- CVH,CVO,CVN=Specifc Heat (constant volume) (J/K/kg)
	//--  MCV=Heat capacity of plenum (J/K)
	//--  TP=Plenum Temperature (K)
	m_sdCVH = (SIM_DATA)(5)/2*(SIM_DATA)(m_sdRG)/m_sdHM;
	m_sdCVO = (SIM_DATA)(5)/2*(SIM_DATA)(m_sdRG)/m_sdOM;
	m_sdCVN = (SIM_DATA)(5)/2*(SIM_DATA)(m_sdRG)/m_sdNM;
	//-- *EQUATIONS 32,33
	m_sdMCV = V(m_sdMHY, iCol)*m_sdCVH+V(m_sdMOX, iCol)*m_sdCVO+V(m_sdMN, iCol)*m_sdCVN;
	//-- *EQUATION 36
	m_sdTPDOT = (SIM_DATA)(1)/m_sdMCV*(m_sdHFH+m_sdHFO+m_sdHFN-m_sdHFOUT-m_sdP*m_sdVPDOT);
	D(m_sdTP, m_sdTPDOT, iCol);
	//-- ccccccccccccccccccccccccccccccccccccccccccccccccccccccccccccccccccccccccc
	//-- Check energy balance in plenum
	D(m_sdHEATIN, m_sdHFH+m_sdHFO+m_sdHFN, iCol);
	D(m_sdHEATOUT, m_sdHFOUT, iCol);
	D(m_sdUP, m_sdMCV*m_sdTPDOT, iCol);
	D(m_sdWORK, m_sdP*m_sdVPDOT, iCol);
	//--  ccccccccccccccccccccccccccccccccccccccccccccccccccccccccccccccccccccccccccccccc
	//-- ====================
	//-- Continuity equations
	//-- ====================
	//-- Hydrogen
	//-- MHY=H2 mass in plenum (kg)
	//-- PH=Partial pressure of H2 in plenum (Pa)
	//--  *EQUATION 19
	m_sdMHDOT = m_sdROH2A*(SIM_DATA)(V(m_sdVGH10, iCol))/m_sdTAU-m_sdRHY*V(m_sdVOGO, iCol);
	D(m_sdMHY, m_sdMHDOT, iCol);
	//-- *EQUATION 17
	m_sdPHDOT = (SIM_DATA)(1)/V(m_sdVP, iCol)*(m_sdMHDOT*(SIM_DATA)(m_sdRG)/m_sdHM*V(m_sdTP, iCol)+V(m_sdMHY, iCol)*(SIM_DATA)(m_sdRG)/m_sdHM*m_sdTPDOT-V(m_sdPH, iCol)*m_sdVPDOT);
	D(m_sdPH, m_sdPHDOT, iCol);
	//-- H2F=H2 mass fraction in plenum (kg)
	m_sdH2F = (SIM_DATA)(V(m_sdMHY, iCol))/(V(m_sdMN, iCol)+V(m_sdMOX, iCol)+V(m_sdMHY, iCol));
	//-- Oxygen
	//-- MOX=O2 mass in plenum (kg)
	//-- PO=Partial pressure of O2 in plenum (Pa)
	//-- *EQUATIONS 18,19
	m_sdMODOT = m_sdROO2A*(SIM_DATA)(V(m_sdVGO10, iCol))/m_sdTAU2-m_sdROX*V(m_sdVOGO, iCol)+m_sdROI*V(m_sdVNI, iCol)*(1-swtch(m_sdAIR));
	D(m_sdMOX, m_sdMODOT, iCol);
	//-- *EQUATION 17
	m_sdPODOT = (SIM_DATA)(1)/V(m_sdVP, iCol)*(m_sdMODOT*(SIM_DATA)(m_sdRG)/m_sdOM*V(m_sdTP, iCol)+V(m_sdMOX, iCol)*(SIM_DATA)(m_sdRG)/m_sdOM*m_sdTPDOT-V(m_sdPO, iCol)*m_sdVPDOT);
	D(m_sdPO, m_sdPODOT, iCol);
	//-- Nitrogen
	//-- MN=N2 mass in plenum (kg)
	//-- PN=Partial pressure of N2 in plenum (Pa)
	//-- *EQUATION 18
	m_sdMNDOT = m_sdRNI*V(m_sdVNI, iCol)-m_sdRN*V(m_sdVOGO, iCol);
	D(m_sdMN, m_sdMNDOT, iCol);
	//-- *EQUATION 17
	m_sdPNDOT = (SIM_DATA)(1)/V(m_sdVP, iCol)*(m_sdMNDOT*(SIM_DATA)(m_sdRG)/m_sdNM*V(m_sdTP, iCol)+V(m_sdMN, iCol)*(SIM_DATA)(m_sdRG)/m_sdNM*m_sdTPDOT-V(m_sdPN, iCol)*m_sdVPDOT);
	D(m_sdPN, m_sdPNDOT, iCol);
	//-- PDOT=time rate of change of plenum pressure
	m_sdPDOT = m_sdPHDOT+m_sdPODOT+m_sdPNDOT;
	//-- ***************************************************
	//-- graph
	//-- ***************************************************
	D(m_sdE, m_sdPCF*(m_sdENP-m_sdEN0), iCol);
	m_sdENPMAX = m_sdENPMAX+(m_sdENP-m_sdENPMAX)*swtch(m_sdENP-m_sdENPMAX);
	m_sdkwmax = m_sdENPMAX*m_sdPCF*1.0e+03;
	m_sdRR = m_sdR;m_sdTEM = m_sdTEMP*0.10;
	m_sdTEM2 = V(m_sdTC102, iCol);m_sdTEM3 = V(m_sdTL101, iCol)*0.1;
	m_sdTEM4 = m_sdTEMP*0.1;
	m_sdVOG = V(m_sdVOGO, iCol)*800;m_sdPS = -1*m_sdPSIG*7;
	m_sdELOG = (SIM_DATA)(m_sdENLOG)/2;m_sdEPOW = (SIM_DATA)(m_sdkw)/20;
	m_sdVGAS = m_sdVF*100*3;
	m_sdWFG = V(m_sdWF, iCol)*10;
	m_sdTEM5 = V(m_sdTL51, iCol)*0.1;
	m_sdPDIS = m_sdP*1e-5;
	m_sdTLA = (SIM_DATA)((V(m_sdTL11, iCol)+V(m_sdTL21, iCol)+V(m_sdTL31, iCol)+V(m_sdTL41, iCol)+V(m_sdTL51, iCol)+V(m_sdTL61, iCol)+V(m_sdTL71, iCol)+V(m_sdTL81, iCol)+V(m_sdTL91, iCol)+V(m_sdTL101, iCol)))/100;
	//-------------------------------
}

