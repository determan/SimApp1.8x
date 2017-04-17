#include "stdafx.h"
#include "SupoModel.h"

Sim::SupoModel::SupoModel()
{
}

Sim::SupoModel::~SupoModel()
{
}

void Sim::SupoModel::SetModelParameters()
{
	//-- Generic SYSTEM MODEL FOR A FISSILE SOLUTION SYSTEM (SUPO EXAMPLE)
	//----------------------------------------------------------------------------------------------------
	//--
	//-- Documentation in LA-UR-13-22033
	//-- A Generic System Model for a Fissile Solution System
	//-- Kimpland, Robert H. & Klein Steven K.
	//-- Los Alamos National Laboratory, 2013
	//--
	//----------------------------------------------------------------------------------------------------
	//-- Input Section
	//----------------------------------------------------------------------------------------------------
	//-- General Physical Parameters - Constants
	if (m_bIC) m_sdTKW = 6.500e-07;
	//TKW: Thermal conductivity of water
	m_sdTB = 93;
	//TB: Boiling point of water (C)
	m_sdRG = 8.31446;
	//RG: Gas constant (m^3*Pa/K/mol)
	m_sdHM = (SIM_DATA)(2)/1000;
	//HM: H2 molar mass (kg)
	m_sdOM = (SIM_DATA)(32)/1000;
	//OM: O2 molar mass (kg)
	m_sdNM = (SIM_DATA)(28)/1000;
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
	//-- General Physical Parameters - Core related
	if (m_bIC) m_sdalpha = 4.0e-04;
	//alpha: Isobaric compressibility of fuel (1/C)
	if (m_bIC) m_sdTKF = 6.500e-07;
	//TKF: Thermal conductivity of fuel
	if (m_bIC) m_sdPr = 6.950;
	//Pr: Prandtl number of fuel
	if (m_bIC) m_sdbta = 5.23e-04;
	//bta: Expansion coefficient of fuel
	if (m_bIC) m_sdkinvis = 7.9091e-07;
	//kinvis: Kinematic viscosity of fuel
	if (m_bIC) m_sdTHDIFF = 1.58e-07;
	//THDIFF: Thermal diffusivity of fuel
	if (m_bIC) m_sdCV = 3.4e-03;
	//CV=Specific heat of fuel (MJ/C/kg)
	if (m_bIC) m_sdPr2 = 3.00;
	//Pr2: Prandtl number of coolant
	//-- Core Configuration Parameters
	if (m_bIC) m_sdV0 = 0.011627;
	//V0: Total initial core volume in m^3
	if (m_bIC) m_sdHCORE = 0.21695;
	//HCORE: Core height in m
	if (m_bIC) m_sdCSA = 0.3648;
	//CSA: Cooling surface area in m^2
	if (m_bIC) m_sdVP0 = 0.0026;
	//VP0: Initial plenum volume in m^3
	if (m_bIC) m_sdCCR = 0.00248;
	//CCR: Cooling tube radius in m
	if (m_bIC) m_sdAREAHT = 0.3648;
	//AREAHT: Total cooling surface area in m^2
	if (m_bIC) m_sdMW = (SIM_DATA)(2.0)/10;
	//MW=mass of WALL (kg)
	if (m_bIC) m_sdCPW = 0.5e-03;
	//CPW=specific heat of WALL (MJ/C/kg)
	//-- Reactivity Parameters
	if (m_bIC) m_sdALF = 0.0344;
	//ALF: Core averaged temperature coefficient of reactivity
	if (m_bIC) m_sdPHI = 28.679;
	//PHI: Void coefficient of reactivity
	//-- Ii: Importance factor for ith region
	if (m_bIC) m_sdI10 = 0.0409;
	if (m_bIC) m_sdI9 = 0.0684;
	if (m_bIC) m_sdI8 = 0.0928;
	if (m_bIC) m_sdI7 = 0.1142;
	if (m_bIC) m_sdI6 = 0.1128;
	if (m_bIC) m_sdI5 = 0.1127;
	if (m_bIC) m_sdI4 = 0.1379;
	if (m_bIC) m_sdI3 = 0.1276;
	if (m_bIC) m_sdI2 = 0.1089;
	if (m_bIC) m_sdI1 = 0.0839;
	//-- FRAi: Fission fractions for ith region
	if (m_bIC) m_sdFRA10 = 0.0629;
	if (m_bIC) m_sdFRA9 = 0.0835;
	if (m_bIC) m_sdFRA8 = 0.0964;
	if (m_bIC) m_sdFRA7 = 0.1068;
	if (m_bIC) m_sdFRA6 = 0.1073;
	if (m_bIC) m_sdFRA5 = 0.1061;
	if (m_bIC) m_sdFRA4 = 0.1188;
	if (m_bIC) m_sdFRA3 = 0.1160;
	if (m_bIC) m_sdFRA2 = 0.1068;
	if (m_bIC) m_sdFRA1 = 0.0954;
	if (m_bIC) m_sdMNT = 1.70e-04;
	//MNT: Mean neutron generation time
	if (m_bIC) m_sdBETA = 0.0085;
	//BETA: Prompt neutron fraction
	if (m_bIC) m_sdTAU = 0.1818;
	//TAU: Bubble transit time in sec
	//-- Operational Parameters
	if (m_bIC) m_sdT0 = 25.0;
	//T0: Initial fuel temperature in deg C
	if (m_bIC) m_sdTC0 = 5.0;
	//TC0: Coolent inlet temperature in deg C
	if (m_bIC) m_sdP0 = 1.0e+05;
	//P0: Plenum inlet pressure in pa
}

void Sim::SupoModel::InitializeModelVars()
{
	S2(m_sdVNI, 1.8333e-01);
	//VNI: Flow rate of cover gas in m^3/sec
	if (m_bIC) m_sdRRTE = 0.001;
	//RRTE: Reactivity insertion rate in $/sec
	if (m_bIC) m_sdFLAG = 0.0;
	//FLAG: Point-in-time-of reactivity insertion in sec
	if (m_bIC) m_sdRmax = 1.90;
	//Rmax: Total reactivity insertion in $
	if (m_bIC) m_sdWCR = 0.2161;
	//WCR: Coolant mass flow rate in kg/sec
	m_sdMCLUMP = 0.0163;
	//MCLUMP: Mass of coolant in each lump in kg
	if (m_bIC) m_sdFE = 0.8879;
	//FE: Fuel enrichment
	if (m_bIC) m_sdFC = 84.2785;
	//FC: Fuel Uranium concentration in g/l
	if (m_bIC) m_sdA = 0.00;
	//A: Initial Power ($)
	//-- Intrinsic Neutron Source Data
	if (m_bIC) m_sdQ0 = 1.0e+03;
	//Q0=Initial intrinsic neutron source (#/s)
	m_sdEN0 = 1.0e-06;
	//EN0=Inital power (MW)
	S2(m_sdEN, 1.0);S2(m_sdD1, 1.);S2(m_sdD2, 1.);S2(m_sdD3, 1.);S2(m_sdD4, 1.);S2(m_sdD5, 1.);S2(m_sdD6, 1.);
	//-- Simulation Control
	m_sdTMAX = 4000.0;
	//TMAX: Maximum time of the simulation in seconds
	//-- NN: Sampling points
	m_sdNN = 400001;
	m_sdscale = 10;
	//scale: +/- limits of display variables
	//-- Display parameters
	//----- Black on white display set with thick lines
	//-- display C16
	//-- display N-8
	//-- display R
	//----- Color on black display
	//----------------------------------------------------------------------------------------------------
	//-- initialization
	//----------------------------------------------------------------------------------------------------
	//-- Boundary layer geometry
	m_sdAC = (SIM_DATA)(m_sdV0)/m_sdHCORE;
	//AC=Cross sectional area of bfz (m^2)
	m_sdBL = (SIM_DATA)(1.00)/1000;
	//BL=Boundary layer thickness (m)
	m_sdAL = (SIM_DATA)(m_sdCSA)/m_sdHCORE*m_sdBL;
	//AL=Cross sectional area of blz (m^2)
	m_sdVLAYER = m_sdAL*m_sdHCORE;
	//VLAYER=Volume of blz (m^3)
	m_sdEM = m_sdV0*(SIM_DATA)(1100.00)/10;
	//EM=Total mass of fuel in bfz divided by # of fuel regions (kg)
	m_sdELM = m_sdVLAYER*(SIM_DATA)(1100.00)/20;
	//ELM=Total mass of fuel in blz divided by # of fuel regions/2 (kg)
	//--
	m_sdLI = (SIM_DATA)(m_sdHCORE)/10;
	m_sdCTR = (SIM_DATA)(1)/100;
	m_sdALI = 3.14159*((m_sdBL+0.18)*(m_sdBL+0.18)-0.18*0.18);
	m_sdALO = 3.14159*(0.34*0.34-(0.34-m_sdBL)*(0.34-m_sdBL));
	m_sdVL = m_sdVLAYER;
	//----- AK=Inverse heat capacity of fuel region (C/MJ)
	m_sdAK = (SIM_DATA)(1)/(m_sdEM*m_sdCV);
	m_sdAK2 = (SIM_DATA)(1)/(m_sdELM*m_sdCV);
	S2(m_sdWF, 0.0);
	//-- Radiolytic gas generation data
	//----- At 0.5 kW/L, MH2,MO2=Mass production rate (kg/m^3/s)
	m_sdMH2 = 1.78e-04;
	m_sdMO2 = 1.42e-03;
	//----- ALF(i)=Temperature coeff. of reactivity in fuel region i
	//-- ALF1=ALF*I1
	//-- ALF2=ALF*I2
	//-- ALF3=ALF*I3
	//-- ALF4=ALF*I4
	//-- ALF5=ALF*I5
	//-- ALF6=ALF*I6
	//-- ALF7=ALF*I7
	//-- ALF8=ALF*I8
	//-- ALF9=ALF*I9
	//-- ALF10=ALF*I10
	m_sdTAU2 = m_sdTAU;
	//TAU,TAU2=Transit time of H2,O2 bubble in each fuel region i (s)
	//----- Initial Fuel Volumes
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
	//----- TEMP(i)=Initial Fuel Temperature in region i (C)
	//----- BR(i)=Fraction of total heat transfered in region i
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
	//----- Boundary layer Temperatures (C)
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
	//----- Wall Temperatures (C)
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
	//----- Boundary layer volumes (m^3)
	S2(m_sdVL11, (SIM_DATA)(m_sdVLAYER)/20);
	S2(m_sdVL21, (SIM_DATA)(m_sdVLAYER)/20);
	S2(m_sdVL31, (SIM_DATA)(m_sdVLAYER)/20);
	S2(m_sdVL41, (SIM_DATA)(m_sdVLAYER)/20);
	S2(m_sdVL51, (SIM_DATA)(m_sdVLAYER)/20);
	S2(m_sdVL61, (SIM_DATA)(m_sdVLAYER)/20);
	S2(m_sdVL71, (SIM_DATA)(m_sdVLAYER)/20);
	S2(m_sdVL81, (SIM_DATA)(m_sdVLAYER)/20);
	S2(m_sdVL91, (SIM_DATA)(m_sdVLAYER)/20);
	S2(m_sdVL101, (SIM_DATA)(m_sdVLAYER)/20);
	//--
	S2(m_sdVL12, (SIM_DATA)(m_sdVLAYER)/20);
	S2(m_sdVL22, (SIM_DATA)(m_sdVLAYER)/20);
	S2(m_sdVL32, (SIM_DATA)(m_sdVLAYER)/20);
	S2(m_sdVL42, (SIM_DATA)(m_sdVLAYER)/20);
	S2(m_sdVL52, (SIM_DATA)(m_sdVLAYER)/20);
	S2(m_sdVL62, (SIM_DATA)(m_sdVLAYER)/20);
	S2(m_sdVL72, (SIM_DATA)(m_sdVLAYER)/20);
	S2(m_sdVL82, (SIM_DATA)(m_sdVLAYER)/20);
	S2(m_sdVL92, (SIM_DATA)(m_sdVLAYER)/20);
	S2(m_sdVL102, (SIM_DATA)(m_sdVLAYER)/20);
	//----- Fuel Density
	//-- RFUEL=1200.0
	//-- RF1=RFUEL | RL1=RFUEL
	//-- RF2=RFUEL | RL2=RFUEL
	//-- RF3=RFUEL | RL3=RFUEL
	//-- RF4=RFUEL | RL4=RFUEL
	//-- RF5=RFUEL | RL5=RFUEL
	//-- RF6=RFUEL | RL6=RFUEL
	//-- RF7=RFUEL | RL7=RFUEL
	//-- RF8=RFUEL | RL8=RFUEL
	//-- RF9=RFUEL | RL9=RFUEL
	//-- RF10=RFUEL | RL10=RFUEL
	//----- Coolant loop initial temperatures
	m_sdTC1IN = m_sdTC0;
	//TC1IN=Temperature of coolant entering core (C)
	//-- TC1(i)=Temperature of Node i first lump (C)
	//-- TC2(i)=Temperature of Node i second lump (C)
	S2(m_sdTC11, m_sdTC0);S2(m_sdTC12, m_sdTC0);
	S2(m_sdTC21, m_sdTC0);S2(m_sdTC22, m_sdTC0);
	S2(m_sdTC31, m_sdTC0);S2(m_sdTC32, m_sdTC0);
	S2(m_sdTC41, m_sdTC0);S2(m_sdTC42, m_sdTC0);
	S2(m_sdTC51, m_sdTC0);S2(m_sdTC52, m_sdTC0);
	S2(m_sdTC61, m_sdTC0);S2(m_sdTC62, m_sdTC0);
	S2(m_sdTC71, m_sdTC0);S2(m_sdTC72, m_sdTC0);
	S2(m_sdTC81, m_sdTC0);S2(m_sdTC82, m_sdTC0);
	S2(m_sdTC91, m_sdTC0);S2(m_sdTC92, m_sdTC0);
	m_sdC101 = m_sdTC0;S2(m_sdTC102, m_sdTC0);
	//----- Heat Exchanger initial temperatures
	S2(m_sdTP2, m_sdT0);
	//TP2=Inlet temperature to tube side (C)
	S2(m_sdTT1, m_sdT0);S2(m_sdTT2, m_sdT0);
	//TT1,TT2=Tube side temperatures, first and second lumps (C)
	m_sdTS1IN = m_sdT0;
	//TS1IN=Shell side inlet temperature (C)
	S2(m_sdTS1, m_sdT0);S2(m_sdTS2, m_sdT0);
	//TS1,TS2=Shell side temperatures, first and second lumps (C)
	S2(m_sdTT, m_sdT0);S2(m_sdTS, m_sdT0);
	//TT=Tube temperature (C) | TS=Shell temperature (C)
	//----- Initial values for plenum
	m_sdAIR = -1.0;
	//AIR=Cover gas Flag
	S2(m_sdPN, m_sdP0*(0.79+0.21*swtch(m_sdAIR)));
	//PN=Partial Pressure of N2 (Pa)
	S2(m_sdPO, m_sdP0*(0.21-0.21*swtch(m_sdAIR)));
	//PO=Partial Pressure of O2 (Pa)
	S2(m_sdTP, m_sdT0+273);
	//TP=Initial Temperature of plenum (K)
	S2(m_sdVP, m_sdVP0);
	//VP=Plenum volume (m^3)
	m_sdPDOT = 0.0;
	S2(m_sdMN, (SIM_DATA)(V(m_sdPN, 0))/(SIM_DATA)(((SIM_DATA)(m_sdRG)/m_sdNM))/V(m_sdTP, 0)*V(m_sdVP, 0));
	S2(m_sdMOX, (SIM_DATA)(V(m_sdPO, 0))/(SIM_DATA)(((SIM_DATA)(m_sdRG)/m_sdOM))/V(m_sdTP, 0)*V(m_sdVP, 0));
	m_sdRNI = (SIM_DATA)(V(m_sdPN, 0))/(SIM_DATA)(((SIM_DATA)(m_sdRG)/m_sdNM))/V(m_sdTP, 0);
	//RNI=Density of N2 cover gas at plenum inlet (kg/m^3)
	m_sdROI = (SIM_DATA)(V(m_sdPO, 0))/(SIM_DATA)(((SIM_DATA)(m_sdRG)/m_sdOM))/V(m_sdTP, 0);
	//ROI=Density of O2 cover gas at plenum inlet (kg/m^3)
	S2(m_sdVOGO, V(m_sdVNI, 0));
	//ROI=Density of O2 cover gas at plenum inlet (kg/m^3)
	//--
	//----------------------------------------------------------------------------------------------------
}

void Sim::SupoModel::ModelDerivatives(int iCol)
{
	//----------------------------------------------------------------------------------------------------
	//-- NEUTRON KINETICS MODEL
	//----- Reactivity insertion
	m_sdBOL = (SIM_DATA)(m_sdBETA)/m_sdMNT;
	m_sdCUT = (SIM_DATA)(m_sdRmax)/m_sdRRTE;
	m_sdRAMP = m_sdRRTE*((SIM_DATA)(m_sdT)-m_sdFLAG)*swtch((SIM_DATA)(m_sdT)-m_sdFLAG)-m_sdRRTE*((SIM_DATA)(m_sdT)-(m_sdFLAG+m_sdCUT))*swtch((SIM_DATA)(m_sdT)-(m_sdFLAG+m_sdCUT));
	m_sdTEMP = (SIM_DATA)((V(m_sdTEMP1, iCol)+V(m_sdTEMP2, iCol)+V(m_sdTEMP3, iCol)+V(m_sdTEMP4, iCol)+V(m_sdTEMP5, iCol)+V(m_sdTEMP6, iCol)+V(m_sdTEMP7, iCol)+V(m_sdTEMP8, iCol)+V(m_sdTEMP9, iCol)+V(m_sdTEMP10, iCol)))/10;
	//--
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
	//--
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
	//--
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
	//-- RFBV1=PHI1*(VGH1+VGO1)+PHI2*(VGH2+VGO2)+PHI3*(VGH3+VGO3)
	m_sdRFBV1 = m_sdPHI1*m_sdVF1+m_sdPHI2*m_sdVF2+m_sdPHI3*m_sdVF3+m_sdPHI4*m_sdVF4;
	//--  RFBV2=PHI4*(VGH4+VGO4)+PHI5*(VGH5+VGO5)+PHI6*(VGH6+VGO6)
	m_sdRFBV2 = m_sdPHI5*m_sdVF5+m_sdPHI6*m_sdVF6+m_sdPHI7*m_sdVF7+m_sdPHI8*m_sdVF8;
	//-- RFBV3=PHI7*(VGH7+VGO7)+PHI8*(VGH8+VGO8)+PHI9*(VGH9+VGO9)+PHI10*(VGH10+VGO10)
	m_sdRFBV3 = m_sdPHI9*m_sdVF9+m_sdPHI10*m_sdVF10;
	m_sdRFBV = m_sdRFBV1+m_sdRFBV2+m_sdRFBV3;
	//--
	//-- Equation 3 of LA-UR-13-22033
	//--
	m_sdR = m_sdA-m_sdRFBA-m_sdRFBV+m_sdRAMP;
	//R=Reactivity of assembly ($)
	//--
	//------------------------------------------------------
	//-- Point Reactor Kinetics Model
	//------------------------------------------------------
	//--
	//-- Equation 1 of LA-UR-13-22033
	//--
	m_sdENP = m_sdEN0*V(m_sdEN, iCol);
	m_sdSUM = m_sdF1*V(m_sdD1, iCol)+m_sdF2*V(m_sdD2, iCol)+m_sdF3*V(m_sdD3, iCol)+m_sdF4*V(m_sdD4, iCol)+m_sdF5*V(m_sdD5, iCol)+m_sdF6*V(m_sdD6, iCol);
	//--
	m_sdENDOT = m_sdBOL*((m_sdR-1.0)*V(m_sdEN, iCol)+m_sdSUM);
	//-- OMEGA=ENDOT/EN
	m_sdENLOG = 0.4342945*log(m_sdENP);
	//--
	D(m_sdEN, m_sdENDOT, iCol);
	//--
	//------ Delayed Neutron Precursors
	//--
	//-- Equation 2 of LA-UR-13-22033
	//--
	D(m_sdD1, m_sdAL1*(V(m_sdEN, iCol)-V(m_sdD1, iCol)), iCol);
	D(m_sdD2, m_sdAL2*(V(m_sdEN, iCol)-V(m_sdD2, iCol)), iCol);
	D(m_sdD3, m_sdAL3*(V(m_sdEN, iCol)-V(m_sdD3, iCol)), iCol);
	D(m_sdD4, m_sdAL4*(V(m_sdEN, iCol)-V(m_sdD4, iCol)), iCol);
	D(m_sdD5, m_sdAL5*(V(m_sdEN, iCol)-V(m_sdD5, iCol)), iCol);
	D(m_sdD6, m_sdAL6*(V(m_sdEN, iCol)-V(m_sdD6, iCol)), iCol);
	//-------------------------------------------------------------------------------------------------------------
	//--  Power conversion factor block
	//--  Convert neutron population to fission rate to power in MW
	//-------------------------------------------------------------------------------------------------------------
	m_sdmXS = 582.0e-24;
	//mXS=U-235 microscopic fission XS at 293 K (cm^2)
	//--
	//-- Equation 5 of LA-UR-13-22033
	//--
	m_sdVav = pow((2*m_sdkB*(SIM_DATA)((273+m_sdTEMP))/m_sdnmass),0.5)*(SIM_DATA)(2)/pow(m_sdPI,0.5);
	//Vav=Average speed of thermal neutrons (m/s)
	//--
	//-- Equation 6 of LA-UR-13-22033
	//--
	m_sdSav = m_sdmXS*(SIM_DATA)(pow(m_sdPI,0.5))/2*pow(((SIM_DATA)(293)/(273+m_sdTEMP)),0.5);
	//Sav=Average microscopic fission X-section of thermal neutrons (cm^2)
	m_sdFND = (SIM_DATA)(m_sdFC)/1.0e+03*(SIM_DATA)(m_sdFE)/m_sdUM*m_sdAN;
	//FND=U-235 number density (#/cm^3)
	//--
	//-- Equation 4 of LA-UR-13-22033
	//--
	m_sdPCF = 1.0;
	//PCF=Power Conversion Factor-3.1e+16 fissions/s=1 MW
	m_sdkw = (m_sdENP-m_sdEN0)*m_sdPCF*1.0e+03;
	//kw=Total assembly power (kw)
	D(m_sdETOT, m_sdPCF*(m_sdENP-m_sdEN0), iCol);
	D(m_sdE, m_sdPCF*(m_sdENP-m_sdEN0), iCol);
	m_sdENPMAX = m_sdENPMAX+(m_sdENP-m_sdENPMAX)*swtch(m_sdENP-m_sdENPMAX);
	m_sdkwmax = m_sdENPMAX*m_sdPCF*1.0e+03;
	//-------------------------------------------------------------------------------------------------------------
	//-- CORE THERMAL MODEL
	//-------------------------------------------------------------------------------------------------------------
	//-- Fuel Temp and Fuel Volume bfz
	//-- Regions 1-10,1=bottom
	//-- Bulk fuel zone
	//-- TEMP(i)=Temperature of fuel in region i (C)
	//-- VFUEL(i)=Volume of fuel in region i (m^3)
	m_sdWC = m_sdWCR+0.0*exp(-(SIM_DATA)(((SIM_DATA)(m_sdT)-2000))/60*swtch((SIM_DATA)(m_sdT)-2000));
	//-- Heat Transfer Coefficients
	m_sdRN1 = V(m_sdWF, iCol)*(SIM_DATA)(m_sdBL)/(SIM_DATA)(m_sdAL)/0.00087;
	m_sdACT = m_sdPI*m_sdCCR*m_sdCCR;
	m_sdRN2 = (SIM_DATA)(m_sdWC)/3*2*(SIM_DATA)(m_sdCCR)/(SIM_DATA)(m_sdACT)/0.001;
	m_sdTKZ = (SIM_DATA)(16.7)/1.0e+06;
	m_sdTHZ = (SIM_DATA)(2)/1000;
	m_sdTWA = (SIM_DATA)((V(m_sdTW1, iCol)+V(m_sdTW2, iCol)+V(m_sdTW3, iCol)+V(m_sdTW4, iCol)+V(m_sdTW5, iCol)+V(m_sdTW6, iCol)+V(m_sdTW7, iCol)+V(m_sdTW8, iCol)+V(m_sdTW9, iCol)+V(m_sdTW10, iCol)))/10;
	m_sdTHOT = m_sdTEMP;
	m_sdTCOLD = m_sdTWA;
	m_sdDIA = 2*m_sdCCR;
	m_sdRay = 9.8*m_sdbta*(m_sdTHOT-m_sdTCOLD)*(SIM_DATA)(pow(m_sdDIA,3))/(SIM_DATA)(m_sdkinvis)/m_sdTHDIFF;
	m_sdGr = 9.8*m_sdbta*(m_sdTHOT-m_sdTCOLD+0.001)*(SIM_DATA)(pow(m_sdHCORE,3))/(SIM_DATA)(m_sdkinvis)/m_sdkinvis;
	m_sddlta = m_sdHCORE*3.93*pow(m_sdPr2,(-0.5))*pow((0.95+m_sdPr2),0.25)*pow(m_sdGr,(-0.25));
	m_sdHTNC = (SIM_DATA)(m_sdTKF)/m_sdDIA*0.48*pow(m_sdRay,((SIM_DATA)(1)/4));
	m_sdHT1 = (SIM_DATA)(m_sdTKF)/m_sdBL*0.023*pow((m_sdRN1+0.01),0.8)*pow(m_sdPr,0.4);
	m_sdHT2 = (SIM_DATA)(m_sdTKW)/(SIM_DATA)(2)/m_sdCCR*0.023*pow((m_sdRN2+0.01),0.8)*pow(m_sdPr,0.4);
	m_sdOHM = (SIM_DATA)(1)/m_sdHTNC+(SIM_DATA)(1)/(m_sdHT2*swtch(m_sdHT2-m_sdHTNC)+m_sdHTNC*swtch(m_sdHTNC-m_sdHT2))+(SIM_DATA)(m_sdTHZ)/m_sdTKZ;
	m_sdAREAHT2 = m_sdPI*2*(0.34+0.18)*m_sdHCORE;
	m_sdGMMA = (m_sdAREAHT)*m_sdHTNC;
	m_sdGAMMA = m_sdGMMA;
	m_sdGAMMA9 = (m_sdAREAHT)*m_sdHT2*0.75;
	//--
	//-- 1
	//--
	m_sdST1 = swtch(m_sdTB-V(m_sdTEMP1, iCol));
	//-- EQUATION 38 of LA-UR-13-22033
	m_sdTEM1DOT = m_sdST1*m_sdAK*m_sdPCF*m_sdFRA1*(m_sdENP-m_sdEN0)+(SIM_DATA)(V(m_sdWF, iCol))/m_sdEM*(V(m_sdTL102, iCol)-V(m_sdTEMP1, iCol));
	D(m_sdTEMP1, m_sdTEM1DOT, iCol);
	//-- EQUATIONS 26,46 of LA-UR-13-22033
	m_sdVFT1 = (SIM_DATA)(m_sdV0)/10*m_sdalpha*m_sdTEM1DOT;
	D(m_sdVFUEL1, m_sdVFT1, iCol);
	//-- RF(i)=Fuel density bfz (kg/m^3)
	m_sdRF1 = (SIM_DATA)(m_sdEM)/V(m_sdVFUEL1, iCol)*(1-m_sdVF1);
	//-- TL(i)=Fuel Temperature blz (C)
	//-- Equation 39 of LA-UR-13-22033
	m_sdTL11DOT = -m_sdAK2*0.5*m_sdBR1*m_sdGAMMA*(V(m_sdTL11, iCol)-V(m_sdTW1, iCol))+(SIM_DATA)(V(m_sdWF, iCol))/m_sdELM*(V(m_sdTEMP10, iCol)-V(m_sdTL11, iCol));
	D(m_sdTL11, m_sdTL11DOT, iCol);
	m_sdVLT11 = (SIM_DATA)(m_sdVL)/20*m_sdalpha*m_sdTL11DOT;
	D(m_sdVL11, m_sdVLT11, iCol);
	//-- Equation 47 of LA-UR-13-22033
	m_sdRL11 = (SIM_DATA)(m_sdELM)/V(m_sdVL11, iCol);
	//-- Equation 40 of LA-UR-13-22033
	m_sdTL12DOT = -m_sdAK2*0.5*m_sdBR1*m_sdGAMMA*(V(m_sdTL11, iCol)-V(m_sdTW1, iCol))+(SIM_DATA)(V(m_sdWF, iCol))/m_sdELM*(V(m_sdTL11, iCol)-V(m_sdTL12, iCol));
	D(m_sdTL12, m_sdTL12DOT, iCol);
	m_sdVLT12 = (SIM_DATA)(m_sdVL)/20*m_sdalpha*m_sdTL12DOT;
	D(m_sdVL12, m_sdVLT12, iCol);
	m_sdRL12 = (SIM_DATA)(m_sdELM)/V(m_sdVL12, iCol);
	//-- TW(i)=Temperature of WALL
	//-- Equation 42 of LA-UR-13-22033
	D(m_sdTW1, (SIM_DATA)(1)/(SIM_DATA)(m_sdMW)/m_sdCPW*(m_sdBR1*m_sdGAMMA*(V(m_sdTL11, iCol)-V(m_sdTW1, iCol))-m_sdBR1*m_sdGAMMA9*(V(m_sdTW1, iCol)-V(m_sdTC101, iCol))), iCol);
	//-------------------------------------------------------------------------------------------------------------
	//-- Boundary Layer Zone
	//-------------------------------------------------------------------------------------------------------------
	//--  2
	//--
	m_sdTL21DOT = -m_sdAK2*0.5*m_sdBR1*m_sdGAMMA*(V(m_sdTL21, iCol)-V(m_sdTW2, iCol))+(SIM_DATA)(V(m_sdWF, iCol))/m_sdELM*(V(m_sdTL12, iCol)-V(m_sdTL21, iCol));
	D(m_sdTL21, m_sdTL21DOT, iCol);
	m_sdVLT21 = (SIM_DATA)(m_sdVL)/20*m_sdalpha*m_sdTL21DOT;
	D(m_sdVL21, m_sdVLT21, iCol);
	m_sdRL21 = (SIM_DATA)(m_sdELM)/V(m_sdVL21, iCol);
	m_sdTL22DOT = -m_sdAK2*0.5*m_sdBR1*m_sdGAMMA*(V(m_sdTL21, iCol)-V(m_sdTW2, iCol))+(SIM_DATA)(V(m_sdWF, iCol))/m_sdELM*(V(m_sdTL21, iCol)-V(m_sdTL22, iCol));
	D(m_sdTL22, m_sdTL22DOT, iCol);
	m_sdVLT22 = (SIM_DATA)(m_sdVL)/20*m_sdalpha*m_sdTL22DOT;
	D(m_sdVL22, m_sdVLT22, iCol);
	m_sdRL22 = (SIM_DATA)(m_sdELM)/V(m_sdVL22, iCol);
	D(m_sdTW2, (SIM_DATA)(1)/(SIM_DATA)(m_sdMW)/m_sdCPW*(m_sdBR1*m_sdGAMMA*(V(m_sdTL21, iCol)-V(m_sdTW2, iCol))-m_sdBR1*m_sdGAMMA9*(V(m_sdTW2, iCol)-V(m_sdTC91, iCol))), iCol);
	//--
	//-- 3
	//--
	m_sdTL31DOT = -m_sdAK2*0.5*m_sdBR1*m_sdGAMMA*(V(m_sdTL31, iCol)-V(m_sdTW3, iCol))+(SIM_DATA)(V(m_sdWF, iCol))/m_sdELM*(V(m_sdTL22, iCol)-V(m_sdTL31, iCol));
	D(m_sdTL31, m_sdTL31DOT, iCol);
	m_sdVLT31 = (SIM_DATA)(m_sdVL)/20*m_sdalpha*m_sdTL31DOT;
	D(m_sdVL31, m_sdVLT31, iCol);
	m_sdRL31 = (SIM_DATA)(m_sdELM)/V(m_sdVL31, iCol);
	m_sdTL32DOT = -m_sdAK2*0.5*m_sdBR1*m_sdGAMMA*(V(m_sdTL31, iCol)-V(m_sdTW3, iCol))+(SIM_DATA)(V(m_sdWF, iCol))/m_sdELM*(V(m_sdTL31, iCol)-V(m_sdTL32, iCol));
	D(m_sdTL32, m_sdTL32DOT, iCol);
	m_sdVLT32 = (SIM_DATA)(m_sdVL)/20*m_sdalpha*m_sdTL32DOT;
	D(m_sdVL32, m_sdVLT32, iCol);
	m_sdRL32 = (SIM_DATA)(m_sdELM)/V(m_sdVL32, iCol);
	D(m_sdTW3, (SIM_DATA)(1)/(SIM_DATA)(m_sdMW)/m_sdCPW*(m_sdBR1*m_sdGAMMA*(V(m_sdTL31, iCol)-V(m_sdTW3, iCol))-m_sdBR1*m_sdGAMMA9*(V(m_sdTW3, iCol)-V(m_sdTC81, iCol))), iCol);
	//--
	//-- 4
	//--
	m_sdTL41DOT = -m_sdAK2*0.5*m_sdBR1*m_sdGAMMA*(V(m_sdTL41, iCol)-V(m_sdTW4, iCol))+(SIM_DATA)(V(m_sdWF, iCol))/m_sdELM*(V(m_sdTL32, iCol)-V(m_sdTL41, iCol));
	D(m_sdTL41, m_sdTL41DOT, iCol);
	m_sdVLT41 = (SIM_DATA)(m_sdVL)/20*m_sdalpha*m_sdTL41DOT;
	D(m_sdVL41, m_sdVLT41, iCol);
	m_sdRL41 = (SIM_DATA)(m_sdELM)/V(m_sdVL41, iCol);
	m_sdTL42DOT = -m_sdAK2*0.5*m_sdBR1*m_sdGAMMA*(V(m_sdTL41, iCol)-V(m_sdTW4, iCol))+(SIM_DATA)(V(m_sdWF, iCol))/m_sdELM*(V(m_sdTL41, iCol)-V(m_sdTL42, iCol));
	D(m_sdTL42, m_sdTL42DOT, iCol);
	m_sdVLT42 = (SIM_DATA)(m_sdVL)/20*m_sdalpha*m_sdTL42DOT;
	D(m_sdVL42, m_sdVLT42, iCol);
	m_sdRL42 = (SIM_DATA)(m_sdELM)/V(m_sdVL42, iCol);
	D(m_sdTW4, (SIM_DATA)(1)/(SIM_DATA)(m_sdMW)/m_sdCPW*(m_sdBR1*m_sdGAMMA*(V(m_sdTL41, iCol)-V(m_sdTW4, iCol))-m_sdBR1*m_sdGAMMA9*(V(m_sdTW4, iCol)-V(m_sdTC71, iCol))), iCol);
	//--
	//-- 5
	//--
	m_sdTL51DOT = -m_sdAK2*0.5*m_sdBR1*m_sdGAMMA*(V(m_sdTL51, iCol)-V(m_sdTW5, iCol))+(SIM_DATA)(V(m_sdWF, iCol))/m_sdELM*(V(m_sdTL42, iCol)-V(m_sdTL51, iCol));
	D(m_sdTL51, m_sdTL51DOT, iCol);
	m_sdVLT51 = (SIM_DATA)(m_sdVL)/20*m_sdalpha*m_sdTL51DOT;
	D(m_sdVL51, m_sdVLT51, iCol);
	m_sdRL51 = (SIM_DATA)(m_sdELM)/V(m_sdVL51, iCol);
	m_sdTL52DOT = -m_sdAK2*0.5*m_sdBR1*m_sdGAMMA*(V(m_sdTL51, iCol)-V(m_sdTW5, iCol))+(SIM_DATA)(V(m_sdWF, iCol))/m_sdELM*(V(m_sdTL51, iCol)-V(m_sdTL52, iCol));
	D(m_sdTL52, m_sdTL52DOT, iCol);
	m_sdVLT52 = (SIM_DATA)(m_sdVL)/20*m_sdalpha*m_sdTL52DOT;
	D(m_sdVL52, m_sdVLT52, iCol);
	m_sdRL52 = (SIM_DATA)(m_sdELM)/V(m_sdVL52, iCol);
	D(m_sdTW5, (SIM_DATA)(1)/(SIM_DATA)(m_sdMW)/m_sdCPW*(m_sdBR1*m_sdGAMMA*(V(m_sdTL51, iCol)-V(m_sdTW5, iCol))-m_sdBR1*m_sdGAMMA9*(V(m_sdTW5, iCol)-V(m_sdTC61, iCol))), iCol);
	//--
	//-- 6
	//--
	m_sdTL61DOT = -m_sdAK2*0.5*m_sdBR1*m_sdGAMMA*(V(m_sdTL61, iCol)-V(m_sdTW6, iCol))+(SIM_DATA)(V(m_sdWF, iCol))/m_sdELM*(V(m_sdTL52, iCol)-V(m_sdTL61, iCol));
	D(m_sdTL61, m_sdTL61DOT, iCol);
	m_sdVLT61 = (SIM_DATA)(m_sdVL)/20*m_sdalpha*m_sdTL61DOT;
	D(m_sdVL61, m_sdVLT61, iCol);
	m_sdRL61 = (SIM_DATA)(m_sdELM)/V(m_sdVL61, iCol);
	m_sdTL62DOT = -m_sdAK2*0.5*m_sdBR1*m_sdGAMMA*(V(m_sdTL61, iCol)-V(m_sdTW6, iCol))+(SIM_DATA)(V(m_sdWF, iCol))/m_sdELM*(V(m_sdTL61, iCol)-V(m_sdTL62, iCol));
	D(m_sdTL62, m_sdTL62DOT, iCol);
	m_sdVLT62 = (SIM_DATA)(m_sdVL)/20*m_sdalpha*m_sdTL62DOT;
	D(m_sdVL62, m_sdVLT62, iCol);
	m_sdRL62 = (SIM_DATA)(m_sdELM)/V(m_sdVL62, iCol);
	D(m_sdTW6, (SIM_DATA)(1)/(SIM_DATA)(m_sdMW)/m_sdCPW*(m_sdBR1*m_sdGAMMA*(V(m_sdTL61, iCol)-V(m_sdTW6, iCol))-m_sdBR1*m_sdGAMMA9*(V(m_sdTW6, iCol)-V(m_sdTC51, iCol))), iCol);
	//--
	//-- 7
	//--
	m_sdTL71DOT = -m_sdAK2*0.5*m_sdBR1*m_sdGAMMA*(V(m_sdTL71, iCol)-V(m_sdTW7, iCol))+(SIM_DATA)(V(m_sdWF, iCol))/m_sdELM*(V(m_sdTL62, iCol)-V(m_sdTL71, iCol));
	D(m_sdTL71, m_sdTL71DOT, iCol);
	m_sdVLT71 = (SIM_DATA)(m_sdVL)/20*m_sdalpha*m_sdTL71DOT;
	D(m_sdVL71, m_sdVLT71, iCol);
	m_sdRL71 = (SIM_DATA)(m_sdELM)/V(m_sdVL71, iCol);
	m_sdTL72DOT = -m_sdAK2*0.5*m_sdBR1*m_sdGAMMA*(V(m_sdTL71, iCol)-V(m_sdTW7, iCol))+(SIM_DATA)(V(m_sdWF, iCol))/m_sdELM*(V(m_sdTL71, iCol)-V(m_sdTL72, iCol));
	D(m_sdTL72, m_sdTL72DOT, iCol);
	m_sdVLT72 = (SIM_DATA)(m_sdVL)/20*m_sdalpha*m_sdTL72DOT;
	D(m_sdVL72, m_sdVLT72, iCol);
	m_sdRL72 = (SIM_DATA)(m_sdELM)/V(m_sdVL72, iCol);
	D(m_sdTW7, (SIM_DATA)(1)/(SIM_DATA)(m_sdMW)/m_sdCPW*(m_sdBR1*m_sdGAMMA*(V(m_sdTL71, iCol)-V(m_sdTW7, iCol))-m_sdBR1*m_sdGAMMA9*(V(m_sdTW7, iCol)-V(m_sdTC41, iCol))), iCol);
	//--
	//-- 8
	//--
	m_sdTL81DOT = -m_sdAK2*0.5*m_sdBR1*m_sdGAMMA*(V(m_sdTL81, iCol)-V(m_sdTW8, iCol))+(SIM_DATA)(V(m_sdWF, iCol))/m_sdELM*(V(m_sdTL72, iCol)-V(m_sdTL81, iCol));
	D(m_sdTL81, m_sdTL81DOT, iCol);
	m_sdVLT81 = (SIM_DATA)(m_sdVL)/20*m_sdalpha*m_sdTL81DOT;
	D(m_sdVL81, m_sdVLT81, iCol);
	m_sdRL81 = (SIM_DATA)(m_sdELM)/V(m_sdVL81, iCol);
	m_sdTL82DOT = -m_sdAK2*0.5*m_sdBR1*m_sdGAMMA*(V(m_sdTL81, iCol)-V(m_sdTW8, iCol))+(SIM_DATA)(V(m_sdWF, iCol))/m_sdELM*(V(m_sdTL81, iCol)-V(m_sdTL82, iCol));
	D(m_sdTL82, m_sdTL82DOT, iCol);
	m_sdVLT82 = (SIM_DATA)(m_sdVL)/20*m_sdalpha*m_sdTL82DOT;
	D(m_sdVL82, m_sdVLT82, iCol);
	m_sdRL82 = (SIM_DATA)(m_sdELM)/V(m_sdVL82, iCol);
	D(m_sdTW8, (SIM_DATA)(1)/(SIM_DATA)(m_sdMW)/m_sdCPW*(m_sdBR1*m_sdGAMMA*(V(m_sdTL81, iCol)-V(m_sdTW8, iCol))-m_sdBR1*m_sdGAMMA9*(V(m_sdTW8, iCol)-V(m_sdTC31, iCol))), iCol);
	//--
	//-- 9
	//--
	m_sdTL91DOT = -m_sdAK2*0.5*m_sdBR1*m_sdGAMMA*(V(m_sdTL91, iCol)-V(m_sdTW9, iCol))+(SIM_DATA)(V(m_sdWF, iCol))/m_sdELM*(V(m_sdTL82, iCol)-V(m_sdTL91, iCol));
	D(m_sdTL91, m_sdTL91DOT, iCol);
	m_sdVLT91 = (SIM_DATA)(m_sdVL)/20*m_sdalpha*m_sdTL91DOT;
	D(m_sdVL91, m_sdVLT91, iCol);
	m_sdRL91 = (SIM_DATA)(m_sdELM)/V(m_sdVL91, iCol);
	m_sdTL92DOT = -m_sdAK2*0.5*m_sdBR1*m_sdGAMMA*(V(m_sdTL91, iCol)-V(m_sdTW9, iCol))+(SIM_DATA)(V(m_sdWF, iCol))/m_sdELM*(V(m_sdTL91, iCol)-V(m_sdTL92, iCol));
	D(m_sdTL92, m_sdTL92DOT, iCol);
	m_sdVLT92 = (SIM_DATA)(m_sdVL)/20*m_sdalpha*m_sdTL92DOT;
	D(m_sdVL92, m_sdVLT92, iCol);
	m_sdRL92 = (SIM_DATA)(m_sdELM)/V(m_sdVL92, iCol);
	D(m_sdTW9, (SIM_DATA)(1)/(SIM_DATA)(m_sdMW)/m_sdCPW*(m_sdBR1*m_sdGAMMA*(V(m_sdTL91, iCol)-V(m_sdTW9, iCol))-m_sdBR1*m_sdGAMMA9*(V(m_sdTW9, iCol)-V(m_sdTC21, iCol))), iCol);
	//--
	//-- 10
	//--
	m_sdTL101DOT = -m_sdAK2*0.5*m_sdBR1*m_sdGAMMA*(V(m_sdTL101, iCol)-V(m_sdTW10, iCol))+(SIM_DATA)(V(m_sdWF, iCol))/m_sdELM*(V(m_sdTL92, iCol)-V(m_sdTL101, iCol));
	D(m_sdTL101, m_sdTL101DOT, iCol);
	m_sdVLT101 = (SIM_DATA)(m_sdVL)/20*m_sdalpha*m_sdTL101DOT;
	D(m_sdVL101, m_sdVLT101, iCol);
	m_sdRL101 = (SIM_DATA)(m_sdELM)/V(m_sdVL101, iCol);
	m_sdTL102DOT = -m_sdAK2*0.5*m_sdBR1*m_sdGAMMA*(V(m_sdTL101, iCol)-V(m_sdTW10, iCol))+(SIM_DATA)(V(m_sdWF, iCol))/m_sdELM*(V(m_sdTL101, iCol)-V(m_sdTL102, iCol));
	D(m_sdTL102, m_sdTL102DOT, iCol);
	m_sdVLT102 = (SIM_DATA)(m_sdVL)/20*m_sdalpha*m_sdTL102DOT;
	D(m_sdVL102, m_sdVLT102, iCol);
	m_sdRL102 = (SIM_DATA)(m_sdELM)/V(m_sdVL102, iCol);
	D(m_sdTW10, (SIM_DATA)(1)/(SIM_DATA)(m_sdMW)/m_sdCPW*(m_sdBR1*m_sdGAMMA*(V(m_sdTL101, iCol)-V(m_sdTW10, iCol))-m_sdBR1*m_sdGAMMA9*(V(m_sdTW10, iCol)-V(m_sdTC11, iCol))), iCol);
	//-------------------------------------------------------------------------------------------------------------
	//--  Bulk Fuel Zone
	//-------------------------------------------------------------------------------------------------------------        --
	//-- 2
	//--
	m_sdST2 = swtch(m_sdTB-V(m_sdTEMP2, iCol));
	m_sdTEM2DOT = m_sdST2*m_sdAK*m_sdPCF*m_sdFRA2*(m_sdENP-m_sdEN0)+(SIM_DATA)(V(m_sdWF, iCol))/m_sdEM*(V(m_sdTEMP1, iCol)-V(m_sdTEMP2, iCol));
	D(m_sdTEMP2, m_sdTEM2DOT, iCol);
	m_sdVFT2 = (SIM_DATA)(m_sdV0)/10*m_sdalpha*m_sdTEM2DOT;
	D(m_sdVFUEL2, m_sdVFT2, iCol);
	m_sdRF2 = (SIM_DATA)(m_sdEM)/V(m_sdVFUEL2, iCol)*(1-m_sdVF2);
	//--
	//-- 3
	//--
	m_sdST3 = swtch(m_sdTB-V(m_sdTEMP3, iCol));
	m_sdTEM3DOT = m_sdST3*m_sdAK*m_sdPCF*m_sdFRA3*(m_sdENP-m_sdEN0)+(SIM_DATA)(V(m_sdWF, iCol))/m_sdEM*(V(m_sdTEMP2, iCol)-V(m_sdTEMP3, iCol));
	D(m_sdTEMP3, m_sdTEM3DOT, iCol);
	m_sdVFT3 = (SIM_DATA)(m_sdV0)/10*m_sdalpha*m_sdTEM3DOT;
	D(m_sdVFUEL3, m_sdVFT3, iCol);
	m_sdRF3 = (SIM_DATA)(m_sdEM)/V(m_sdVFUEL3, iCol)*(1-m_sdVF3);
	//--
	//-- 4
	//--
	m_sdST4 = swtch(m_sdTB-V(m_sdTEMP4, iCol));
	m_sdTEM4DOT = m_sdST4*m_sdAK*m_sdPCF*m_sdFRA4*(m_sdENP-m_sdEN0)+(SIM_DATA)(V(m_sdWF, iCol))/m_sdEM*(V(m_sdTEMP3, iCol)-V(m_sdTEMP4, iCol));
	D(m_sdTEMP4, m_sdTEM4DOT, iCol);
	m_sdVFT4 = (SIM_DATA)(m_sdV0)/10*m_sdalpha*m_sdTEM4DOT;
	D(m_sdVFUEL4, m_sdVFT4, iCol);
	m_sdRF4 = (SIM_DATA)(m_sdEM)/V(m_sdVFUEL4, iCol)*(1-m_sdVF4);
	//--
	//-- 5
	//--
	m_sdST5 = swtch(m_sdTB-V(m_sdTEMP5, iCol));
	m_sdTEM5DOT = m_sdST5*m_sdAK*m_sdPCF*m_sdFRA5*(m_sdENP-m_sdEN0)+(SIM_DATA)(V(m_sdWF, iCol))/m_sdEM*(V(m_sdTEMP4, iCol)-V(m_sdTEMP5, iCol));
	D(m_sdTEMP5, m_sdTEM5DOT, iCol);
	m_sdVFT5 = (SIM_DATA)(m_sdV0)/10*m_sdalpha*m_sdTEM5DOT;
	D(m_sdVFUEL5, m_sdVFT5, iCol);
	m_sdRF5 = (SIM_DATA)(m_sdEM)/V(m_sdVFUEL5, iCol)*(1-m_sdVF5);
	//--
	//-- 6
	//--
	m_sdST6 = swtch(m_sdTB-V(m_sdTEMP6, iCol));
	m_sdTEM6DOT = m_sdST6*m_sdAK*m_sdPCF*m_sdFRA6*(m_sdENP-m_sdEN0)+(SIM_DATA)(V(m_sdWF, iCol))/m_sdEM*(V(m_sdTEMP5, iCol)-V(m_sdTEMP6, iCol));
	D(m_sdTEMP6, m_sdTEM6DOT, iCol);
	m_sdVFT6 = (SIM_DATA)(m_sdV0)/10*m_sdalpha*m_sdTEM6DOT;
	D(m_sdVFUEL6, m_sdVFT6, iCol);
	m_sdRF6 = (SIM_DATA)(m_sdEM)/V(m_sdVFUEL6, iCol)*(1-m_sdVF6);
	//--
	//-- 7
	//--
	m_sdST7 = swtch(m_sdTB-V(m_sdTEMP7, iCol));
	m_sdTEM7DOT = m_sdST7*m_sdAK*m_sdPCF*m_sdFRA7*(m_sdENP-m_sdEN0)+(SIM_DATA)(V(m_sdWF, iCol))/m_sdEM*(V(m_sdTEMP6, iCol)-V(m_sdTEMP7, iCol));
	D(m_sdTEMP7, m_sdTEM7DOT, iCol);
	m_sdVFT7 = (SIM_DATA)(m_sdV0)/10*m_sdalpha*m_sdTEM7DOT;
	D(m_sdVFUEL7, m_sdVFT7, iCol);
	m_sdRF7 = (SIM_DATA)(m_sdEM)/V(m_sdVFUEL7, iCol)*(1-m_sdVF7);
	//--
	//-- 8
	//--
	m_sdST8 = swtch(m_sdTB-V(m_sdTEMP8, iCol));
	m_sdTEM8DOT = m_sdST8*m_sdAK*m_sdPCF*m_sdFRA8*(m_sdENP-m_sdEN0)+(SIM_DATA)(V(m_sdWF, iCol))/m_sdEM*(V(m_sdTEMP7, iCol)-V(m_sdTEMP8, iCol));
	D(m_sdTEMP8, m_sdTEM8DOT, iCol);
	m_sdVFT8 = (SIM_DATA)(m_sdV0)/10*m_sdalpha*m_sdTEM8DOT;
	D(m_sdVFUEL8, m_sdVFT8, iCol);
	m_sdRF8 = (SIM_DATA)(m_sdEM)/V(m_sdVFUEL8, iCol)*(1-m_sdVF8);
	//--
	//-- 9
	//--
	m_sdST9 = swtch(m_sdTB-V(m_sdTEMP9, iCol));
	m_sdTEM9DOT = m_sdST9*m_sdAK*m_sdPCF*m_sdFRA9*(m_sdENP-m_sdEN0)+(SIM_DATA)(V(m_sdWF, iCol))/m_sdEM*(V(m_sdTEMP8, iCol)-V(m_sdTEMP9, iCol));
	D(m_sdTEMP9, m_sdTEM9DOT, iCol);
	m_sdVFT9 = (SIM_DATA)(m_sdV0)/10*m_sdalpha*m_sdTEM9DOT;
	D(m_sdVFUEL9, m_sdVFT9, iCol);
	m_sdRF9 = (SIM_DATA)(m_sdEM)/V(m_sdVFUEL9, iCol)*(1-m_sdVF9);
	//--
	//-- 10
	//--
	m_sdST10 = swtch(m_sdTB-V(m_sdTEMP10, iCol));
	m_sdTEM10DT = m_sdST10*m_sdAK*m_sdPCF*m_sdFRA10*(m_sdENP-m_sdEN0)+(SIM_DATA)(V(m_sdWF, iCol))/m_sdEM*(V(m_sdTEMP9, iCol)-V(m_sdTEMP10, iCol));
	D(m_sdTEMP10, m_sdTEM10DT, iCol);
	m_sdVFT10 = (SIM_DATA)(m_sdV0)/10*m_sdalpha*m_sdTEM10DT;
	D(m_sdVFUEL10, m_sdVFT10, iCol);
	m_sdRF10 = (SIM_DATA)(m_sdEM)/V(m_sdVFUEL10, iCol)*(1-m_sdVF10);
	//---------------------------------------------------------------------------------------------------
	//-- Buoyancy
	//---------------------------------------------------------------------------------------------------
	//-- Equation 44 of LA-UR-13-22033
	m_sdSUML1 = 0.5*m_sdLI*(m_sdRL11+m_sdRL21+m_sdRL31+m_sdRL41+m_sdRL51+m_sdRL61+m_sdRL71+m_sdRL81+m_sdRL91+m_sdRL101)*9.8;
	m_sdSUML2 = 0.5*m_sdLI*(m_sdRL12+m_sdRL22+m_sdRL32+m_sdRL42+m_sdRL52+m_sdRL62+m_sdRL72+m_sdRL82+m_sdRL92+m_sdRL102)*9.8;
	m_sdSUMF = m_sdLI*(m_sdRF1+m_sdRF2+m_sdRF3+m_sdRF4+m_sdRF5+m_sdRF6+m_sdRF7+m_sdRF8+m_sdRF9+m_sdRF10)*9.8;
	m_sdFRIC = 1.0;
	m_sdDISP = m_sdFRIC*(SIM_DATA)(m_sdHCORE)/2*((SIM_DATA)(1)/(SIM_DATA)(0.16)/(SIM_DATA)(m_sdAC)/(SIM_DATA)(m_sdAC)/m_sdRF5+(SIM_DATA)(1)/(SIM_DATA)(m_sdBL)/(SIM_DATA)(m_sdAL)/(SIM_DATA)(m_sdAL)/m_sdRL51);
	D(m_sdWF, (SIM_DATA)(1)/((SIM_DATA)(m_sdHCORE)/m_sdAL+(SIM_DATA)(m_sdHCORE)/m_sdAC)*(m_sdSUML1+m_sdSUML2-m_sdSUMF-V(m_sdWF, iCol)*(SIM_DATA)(V(m_sdWF, iCol))/(SIM_DATA)(2)/1200*((SIM_DATA)(1)/(SIM_DATA)(m_sdAL)/m_sdAL-(SIM_DATA)(1)/(SIM_DATA)(m_sdAC)/m_sdAC)), iCol);
	//-- VFUEL=Total Fuel Volume (m^3)
	//-- VFT=Time rate of change of total fuel volume
	m_sdVFUEL = V(m_sdVFUEL1, iCol)+V(m_sdVFUEL2, iCol)+V(m_sdVFUEL3, iCol)+V(m_sdVFUEL4, iCol)+V(m_sdVFUEL5, iCol)+V(m_sdVFUEL6, iCol)+V(m_sdVFUEL7, iCol)+V(m_sdVFUEL8, iCol)+V(m_sdVFUEL9, iCol)+V(m_sdVFUEL10, iCol);
	m_sdVFT = m_sdVFT1+m_sdVFT2+m_sdVFT3+m_sdVFT4+m_sdVFT5+m_sdVFT6+m_sdVFT7+m_sdVFT8+m_sdVFT9+m_sdVFT10;
	//---------------------------------------------------------------------------------------------------------------
	//-- Steam model
	//---------------------------------------------------------------------------------------------------------------
	m_sdP = V(m_sdPN, iCol)+V(m_sdPH, iCol)+V(m_sdPO, iCol);
	//Plenum/Fuel Pressure (Pa)
	m_sdHfg = (SIM_DATA)(2256.4)/1.0e+03;
	//(MJ/kg)
	m_sdsvg = 1.67;
	//(m^3/kg)
	m_sdTAUS = (SIM_DATA)(1.0)/5.5;
	m_sdHg = 2675.6;
	//--
	//-- 10
	//--
	m_sdSS10 = swtch(V(m_sdTEMP10, iCol)-m_sdTB);
	m_sdVS10DOT = m_sdSS10*m_sdFRA10*(SIM_DATA)((m_sdENP-m_sdEN0))/m_sdHfg*m_sdsvg-(V(m_sdVS10, iCol)-V(m_sdVS9, iCol))*(SIM_DATA)(1)/m_sdTAUS-(SIM_DATA)(m_sdPDOT)/m_sdP*V(m_sdVS10, iCol);
	D(m_sdVS10, m_sdVS10DOT, iCol);
	//--
	//-- 9
	//--
	m_sdSS9 = swtch(V(m_sdTEMP9, iCol)-m_sdTB);
	m_sdVS9DOT = m_sdSS9*m_sdFRA9*(SIM_DATA)((m_sdENP-m_sdEN0))/m_sdHfg*m_sdsvg-(V(m_sdVS9, iCol)-V(m_sdVS8, iCol))*(SIM_DATA)(1)/m_sdTAUS-(SIM_DATA)(m_sdPDOT)/m_sdP*V(m_sdVS9, iCol);
	D(m_sdVS9, m_sdVS9DOT, iCol);
	//--
	//-- 8
	//--
	m_sdSS8 = swtch(V(m_sdTEMP8, iCol)-m_sdTB);
	m_sdVS8DOT = m_sdSS8*m_sdFRA8*(SIM_DATA)((m_sdENP-m_sdEN0))/m_sdHfg*m_sdsvg-(V(m_sdVS8, iCol)-V(m_sdVS7, iCol))*(SIM_DATA)(1)/m_sdTAUS-(SIM_DATA)(m_sdPDOT)/m_sdP*V(m_sdVS8, iCol);
	D(m_sdVS8, m_sdVS8DOT, iCol);
	//--
	//-- 7
	//--
	m_sdSS7 = swtch(V(m_sdTEMP7, iCol)-m_sdTB);
	m_sdVS7DOT = m_sdSS7*m_sdFRA7*(SIM_DATA)((m_sdENP-m_sdEN0))/m_sdHfg*m_sdsvg-(V(m_sdVS7, iCol)-V(m_sdVS6, iCol))*(SIM_DATA)(1)/m_sdTAUS-(SIM_DATA)(m_sdPDOT)/m_sdP*V(m_sdVS7, iCol);
	D(m_sdVS7, m_sdVS7DOT, iCol);
	//--
	//-- 6
	//--
	m_sdSS6 = swtch(V(m_sdTEMP6, iCol)-m_sdTB);
	m_sdVS6DOT = m_sdSS6*m_sdFRA6*(SIM_DATA)((m_sdENP-m_sdEN0))/m_sdHfg*m_sdsvg-(V(m_sdVS6, iCol)-V(m_sdVS5, iCol))*(SIM_DATA)(1)/m_sdTAUS-(SIM_DATA)(m_sdPDOT)/m_sdP*V(m_sdVS6, iCol);
	D(m_sdVS6, m_sdVS6DOT, iCol);
	//--
	//-- 5
	//--
	m_sdSS5 = swtch(V(m_sdTEMP5, iCol)-m_sdTB);
	m_sdVS5DOT = m_sdSS5*m_sdFRA5*(SIM_DATA)((m_sdENP-m_sdEN0))/m_sdHfg*m_sdsvg-(V(m_sdVS5, iCol)-V(m_sdVS4, iCol))*(SIM_DATA)(1)/m_sdTAUS-(SIM_DATA)(m_sdPDOT)/m_sdP*V(m_sdVS5, iCol);
	D(m_sdVS5, m_sdVS5DOT, iCol);
	//--
	//-- 4
	//--
	m_sdSS4 = swtch(V(m_sdTEMP4, iCol)-m_sdTB);
	m_sdVS4DOT = m_sdSS4*m_sdFRA4*(SIM_DATA)((m_sdENP-m_sdEN0))/m_sdHfg*m_sdsvg-(V(m_sdVS4, iCol)-V(m_sdVS3, iCol))*(SIM_DATA)(1)/m_sdTAUS-(SIM_DATA)(m_sdPDOT)/m_sdP*V(m_sdVS4, iCol);
	D(m_sdVS4, m_sdVS4DOT, iCol);
	//--
	//-- 3
	//--
	m_sdSS3 = swtch(V(m_sdTEMP3, iCol)-m_sdTB);
	m_sdVS3DOT = m_sdSS3*m_sdFRA3*(SIM_DATA)((m_sdENP-m_sdEN0))/m_sdHfg*m_sdsvg-(V(m_sdVS3, iCol)-V(m_sdVS2, iCol))*(SIM_DATA)(1)/m_sdTAUS-(SIM_DATA)(m_sdPDOT)/m_sdP*V(m_sdVS3, iCol);
	D(m_sdVS3, m_sdVS3DOT, iCol);
	//--
	//-- 2
	//--
	m_sdSS2 = swtch(V(m_sdTEMP2, iCol)-m_sdTB);
	m_sdVS2DOT = m_sdSS2*m_sdFRA2*(SIM_DATA)((m_sdENP-m_sdEN0))/m_sdHfg*m_sdsvg-(V(m_sdVS2, iCol)-V(m_sdVS1, iCol))*(SIM_DATA)(1)/m_sdTAUS-(SIM_DATA)(m_sdPDOT)/m_sdP*V(m_sdVS2, iCol);
	D(m_sdVS2, m_sdVS2DOT, iCol);
	//--
	//-- 1
	//--
	m_sdSS1 = swtch(V(m_sdTEMP1, iCol)-m_sdTB);
	m_sdVS1DOT = m_sdSS1*m_sdFRA1*(SIM_DATA)((m_sdENP-m_sdEN0))/m_sdHfg*m_sdsvg-(V(m_sdVS1, iCol))*(SIM_DATA)(1)/m_sdTAUS-(SIM_DATA)(m_sdPDOT)/m_sdP*V(m_sdVS1, iCol);
	D(m_sdVS1, m_sdVS1DOT, iCol);
	//---------------------------------------------------------------------------------------------------------------
	//-- Coolant Loop
	//---------------------------------------------------------------------------------------------------------------
	//-- Nodes 1-10, 1=bottom
	m_sdMC11 = m_sdMCLUMP;
	//MC11=Mass of Node 1 lump 1 (kg)
	m_sdMC12 = m_sdMCLUMP;
	//MC12=Mass of Node 1 lump 2 (kg)
	m_sdCPC = 4.18e-03;
	//CPC= specific heat of coolant Node 1 (MJ/C/kg)
	m_sdFR11 = 0.5;
	//FR11=Fraction of Region 1 power going into lump 1
	m_sdFR12 = 0.5;
	//FR12=Fraction of Region 1 power going into lump2
	//-- EQUATION 50 of LA-UR-13-22033
	m_sdTAUC11 = (SIM_DATA)(m_sdMC11)/m_sdWC;
	//TAUC11=Transit time Node 1 lump 1 (s)
	m_sdTAUC12 = (SIM_DATA)(m_sdMC12)/m_sdWC;
	//TAUC12=Transit time Node 1 lump 2 (s)
	//-- TC11=Temperature Node 1, lump 1
	//-- EQUATION 48 of LA-UR-13-22033
	D(m_sdTC11, (SIM_DATA)(1)/(SIM_DATA)(m_sdMC11)/m_sdCPC*m_sdFR11*m_sdBR1*m_sdGAMMA9*(V(m_sdTW10, iCol)-V(m_sdTC11, iCol))+(SIM_DATA)(1)/m_sdTAUC11*(m_sdTC1IN-V(m_sdTC11, iCol)), iCol);
	//-- TC12=Temperature Node 1, lump 2
	//-- EQUATION 49 of LA-UR-13-22033
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
	//---------------------------------------------------------------------------------------------------------------
	//-- Piping from core to HE
	//---------------------------------------------------------------------------------------------------------------
	//-- TP2=Temperature in to tube side (C)
	m_sdMP2 = 20.0;
	//MP2=Mass of coolant in pipe (kg)
	//-- EQUATION 51 of LA-UR-13-22033
	m_sdTAUP2 = (SIM_DATA)(m_sdMP2)/m_sdWC;
	//TAUP2=Transit time of piping (s)
	D(m_sdTP2, (SIM_DATA)(1)/m_sdTAUP2*(V(m_sdTC102, iCol)-V(m_sdTP2, iCol)), iCol);
	//---------------------------------------------------------------------------------------------------------------
	//-- Heat exchanger
	//---------------------------------------------------------------------------------------------------------------
	//-- Tube side
	//---------------------------------------------------------------------------------------------------------------
	m_sdMT1 = m_sdMC11*4*10;
	//MT1=Mass of lump (kg)
	m_sdTAUT1 = (SIM_DATA)(m_sdMT1)/m_sdWC;
	//TAUT1=Transit time of lump (s)
	m_sdGAM1 = m_sdHT2*m_sdAREAHT*4;
	//GAM1=Overall heat transfer coeff. - Tube side to tube (MW/C)
	//-- *EQUATION 52
	D(m_sdTT1, (SIM_DATA)(2)/m_sdTAUT1*(V(m_sdTP2, iCol)-V(m_sdTT1, iCol))-(SIM_DATA)(m_sdGAM1)/(SIM_DATA)(m_sdMT1)/m_sdCPC*(V(m_sdTT1, iCol)-V(m_sdTT, iCol)), iCol);
	//TT1=Temperature of first lump (C)
	//--  EQUATION 53 of LA-UR-13-22033
	D(m_sdTT2, (SIM_DATA)(2)/m_sdTAUT1*(V(m_sdTT1, iCol)-V(m_sdTT2, iCol))-(SIM_DATA)(m_sdGAM1)/(SIM_DATA)(m_sdMT1)/m_sdCPC*(V(m_sdTT1, iCol)-V(m_sdTT, iCol)), iCol);
	//TT2=Temperature of second lump (C)
	//---------------------------------------------------------------------------------------------------------------
	//-- Piping from HE to Core
	//---------------------------------------------------------------------------------------------------------------
	m_sdMP1 = 20.0;
	//Mass of coolant in pipe (kg)
	m_sdTAUP1 = (SIM_DATA)(m_sdMP1)/m_sdWC;
	//TAUP1=Transit time in piping (s)
	//---------------------------------------------------------------------------------------------------------------
	//-- Shell side
	//---------------------------------------------------------------------------------------------------------------
	m_sdWC2 = 3.0;
	//WC2=Mass flow rate (kg/s)
	m_sdMS1 = 2*m_sdMT1;
	//MS1=Mass of lump (kg)
	m_sdTAUS1 = (SIM_DATA)(m_sdMS1)/m_sdWC2;
	//TAUS1=Transit time of lump (s)
	m_sdGAM2 = m_sdGAM1;
	//GAM2=Overall heat transfer Coeff. - Tube to shell side (MW/C)
	m_sdGAM3 = m_sdGAM1;
	//GAM3=Overall heat transfer Coeff. - Shell side to shell (MW/C)
	//-- TS1=Temperature of first lump (C)
	//-- EQUATION 54 of LA-UR-13-22033
	D(m_sdTS1, (SIM_DATA)(2)/m_sdTAUS1*(m_sdTS1IN-V(m_sdTS1, iCol))+(SIM_DATA)(m_sdGAM2)/(SIM_DATA)(m_sdMS1)/m_sdCPC*(V(m_sdTT, iCol)-V(m_sdTS1, iCol))-(SIM_DATA)(m_sdGAM3)/(SIM_DATA)(m_sdMS1)/m_sdCPC*(V(m_sdTS1, iCol)-V(m_sdTS, iCol)), iCol);
	//-- TS2=Temperature of second lump (C)
	//--  EQUATION 55 of LA-UR-13-22033
	D(m_sdTS2, (SIM_DATA)(2)/m_sdTAUS1*(V(m_sdTS1, iCol)-V(m_sdTS2, iCol))+(SIM_DATA)(m_sdGAM2)/(SIM_DATA)(m_sdMS1)/m_sdCPC*(V(m_sdTT, iCol)-V(m_sdTS1, iCol))-(SIM_DATA)(m_sdGAM3)/(SIM_DATA)(m_sdMS1)/m_sdCPC*(V(m_sdTS1, iCol)-V(m_sdTS, iCol)), iCol);
	//---------------------------------------------------------------------------------------------------------------
	//-- Tube
	//---------------------------------------------------------------------------------------------------------------
	m_sdMT = 10.0;
	//MT=mass of tube (kg)
	m_sdCPT = 0.5e-03;
	//CPT=specific heat of tube (MJ/C/kg)
	//-- TT=Temperature of Tube
	//-- EQUATION 56 of LA-UR-13-22033
	D(m_sdTT, 2*(SIM_DATA)(m_sdGAM1)/(SIM_DATA)(m_sdMT)/m_sdCPT*(V(m_sdTT1, iCol)-V(m_sdTT, iCol))-2*(SIM_DATA)(m_sdGAM2)/(SIM_DATA)(m_sdMT)/m_sdCPT*(V(m_sdTT, iCol)-V(m_sdTS1, iCol)), iCol);
	//---------------------------------------------------------------------------------------------------------------
	//-- Shell
	//---------------------------------------------------------------------------------------------------------------
	m_sdMS = 20.0;
	//MS=Mass of shell (kg)
	m_sdCPS = 0.5e-03;
	//CPS=Specific heat of shell (MJ/C/kg)
	//-- TS=Shell Temperature (C)
	//-- EQUATION 57 of LA-UR-13-22033
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
	//-- At steady state PCF*(ENP-ENO)=HCDOT=HSDOT=-HTDOT
	//---------------------------------------------------------------------------------------------------------------
	//-- RADIOLYTIC GAS MODEL
	//---------------------------------------------------------------------------------------------------------------
	//-- EQUATION 14 of LA-UR-13-22033
	//-- P=PN+PH+PO |   -- Plenum/Fuel Pressure (Pa)
	//-- ROH2A,ROO2A=Average density of H2,O2 in core (kg/m^3)
	m_sdROH2A = (SIM_DATA)(m_sdP)/(SIM_DATA)(((SIM_DATA)(m_sdRG)/m_sdHM))/(m_sdTEMP+273);
	m_sdROO2A = (SIM_DATA)(m_sdP)/(SIM_DATA)(((SIM_DATA)(m_sdRG)/m_sdOM))/(m_sdTEMP+273);
	//---------------------------------------------------------------------------------------------------------------
	//--  REGIONS 1-10, 1=bottom
	//---------------------------------------------------------------------------------------------------------------
	//--
	//-- 1
	//--
	//-- ROH2(i)=Denisty of H2 (kg/m^3) in region i
	//-- ROO2(i)=Denisty of O2 (kg/m^3) in region i
	//-- EQUATION 29 of LA-UR-13-22033
	m_sdROH21 = (SIM_DATA)(m_sdP)/(SIM_DATA)(((SIM_DATA)(m_sdRG)/m_sdHM))/(V(m_sdTEMP1, iCol)+273);
	m_sdROO21 = (SIM_DATA)(m_sdP)/(SIM_DATA)(((SIM_DATA)(m_sdRG)/m_sdOM))/(V(m_sdTEMP1, iCol)+273);
	//-- XT=H2 Threshold Quality factor
	m_sdXT = 1.0e-04;
	//-- GH2,GO2=Dissolved gas production rates
	//-- EQUATIONS 10-13 of LA-UR-13-22033
	m_sdGH2 = (SIM_DATA)(m_sdMH2)/0.5;
	//(kg/MJ)  H2
	m_sdGO2 = (SIM_DATA)(m_sdMO2)/0.5;
	//(kg/MJ) O2
	//-- ED(i)=Energy deposited in region I (MJ)
	m_sdXH210 = (SIM_DATA)(V(m_sdMDH210, iCol))/(V(m_sdMDH210, iCol)+m_sdEM);
	m_sdXO210 = (SIM_DATA)(V(m_sdMDO210, iCol))/(V(m_sdMDO210, iCol)+m_sdEM);
	m_sdXH21 = (SIM_DATA)(V(m_sdMDH21, iCol))/(V(m_sdMDH21, iCol)+m_sdEM);
	m_sdXO21 = (SIM_DATA)(V(m_sdMDO21, iCol))/(V(m_sdMDO21, iCol)+m_sdEM);
	m_sdED1 = m_sdPCF*m_sdFRA1*(m_sdENP-m_sdEN0);
	//-- MDH2(i),MDO2(i)=Dissolved gas mass in region i
	D(m_sdMDH21, m_sdED1*m_sdGH2+V(m_sdWF, iCol)*(m_sdXH210-m_sdXH21), iCol);
	D(m_sdMDO21, m_sdED1*m_sdGO2+V(m_sdWF, iCol)*(m_sdXO210-m_sdXO21), iCol);
	//-- XH2(i),XO2(i)=Dissolved gas quality factor in region i
	//-- XTO=O2 Threshold quality factor
	m_sdXTO = 0.001;
	//-- G(i)H=H2 gas generation rate (m^3/MJ) in region i
	//-- EQUATIONS 8,9 of LA-UR-22033
	m_sdG1H = (SIM_DATA)(m_sdGH2)/m_sdROH21*swtch(m_sdXH21-m_sdXT);
	//-- VH(i)DOT=H2 gas rate of change in region i
	//-- EQUATIONS 7,30 of LA-Ur-13-22033
	m_sdVH1DOT = m_sdG1H*m_sdPCF*m_sdFRA1*(m_sdENP-m_sdEN0)-V(m_sdVGH1, iCol)*(SIM_DATA)(1)/m_sdTAU-(SIM_DATA)(m_sdPDOT)/m_sdP*V(m_sdVGH1, iCol);
	//-- VGH(i)=Volume of H2 gas in region i (m^3)
	D(m_sdVGH1, m_sdVH1DOT, iCol);
	//-- G(i)O=O2 gas generation rate (m^3/MJ) in region i
	//-- EQUATIONS 8,9 of LA-UR-13-22033
	m_sdG1O = (SIM_DATA)(m_sdGO2)/m_sdROO21*swtch(m_sdXO21-m_sdXTO);
	//-- VO(i)DOT=O2 gas rate of change in region i
	//-- VGO(i)=Volume of O2 gas in region i (m^3)
	//-- EQUATION 7,30 of LA-UR-22033
	m_sdVO1DOT = m_sdG1O*m_sdPCF*m_sdFRA1*(m_sdENP-m_sdEN0)-V(m_sdVGO1, iCol)*(SIM_DATA)(1)/m_sdTAU2-(SIM_DATA)(m_sdPDOT)/m_sdP*V(m_sdVGO1, iCol);
	D(m_sdVGO1, m_sdVO1DOT, iCol);
	//-- VF(i)=Volume fraction of gas in region i
	//-- VF1=(VGH1+VGO1)/(VGH1+VGO1+VFUEL1)
	//--
	//-- 2
	//--
	m_sdROH22 = (SIM_DATA)(m_sdP)/(SIM_DATA)(((SIM_DATA)(m_sdRG)/m_sdHM))/(V(m_sdTEMP2, iCol)+273);
	m_sdROO22 = (SIM_DATA)(m_sdP)/(SIM_DATA)(((SIM_DATA)(m_sdRG)/m_sdOM))/(V(m_sdTEMP2, iCol)+273);
	//-- XT=1.0e-04
	//-- GH2=MH2/0.5  | -- kg/MJ  H2
	//-- GO2=MO2/0.5 | -- kg/MJ O2
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
	//-- VF2=(VGH2+VGO2)/(VGH2+VGO2+VFUEL2)
	//--
	//-- 3
	//--
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
	//-- VF3=(VGH3+VGO3)/(VGH3+VGO3+VFUEL3)
	//--
	//-- 4
	//--
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
	//-- VF4=(VGH4+VGO4)/(VGH4+VGO4+VFUEL4)
	//--
	//-- 5
	//--
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
	//-- VF5=(VGH5+VGO5)/(VGH5+VGO5+VFUEL5)
	//--
	//-- 6
	//--
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
	//-- VF6=(VGH6+VGO6)/(VGH6+VGO6+VFUEL6)
	//--
	//-- 7
	//--
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
	//-- VF7=(VGH7+VGO7)/(VGH7+VGO7+VFUEL7)
	//--
	//-- 8
	//--
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
	//-- VF8=(VGH8+VGO8)/(VGH8+VGO8+VFUEL8)
	//--
	//-- 9
	//--
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
	//-- VF9=(VGH9+VGO9)/(VGH9+VGO9+VFUEL9)
	//--
	//-- 10
	//--
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
	//-- VF10=(VGH10+VGO10)/(VGH10+VGO10+VFUEL10)
	//-- Check core gas mass balance (WMUIN=0)
	//-- HPDOT+OPDOT=WMUDOT
	//-- HP+OP=-WMU+VHTOT*ROH2A+VOTOT*ROO2A
	m_sdHPDOT = m_sdGH2*m_sdPCF*(m_sdENP-m_sdEN0)*swtch(m_sdXH210-m_sdXT);
	m_sdOPDOT = m_sdGO2*m_sdPCF*(m_sdENP-m_sdEN0)*swtch(m_sdXO210-m_sdXTO);
	D(m_sdHP, m_sdHPDOT, iCol);
	D(m_sdOP, m_sdOPDOT, iCol);
	m_sdVHTOT = V(m_sdVGH1, iCol)+V(m_sdVGH2, iCol)+V(m_sdVGH3, iCol)+V(m_sdVGH4, iCol)+V(m_sdVGH5, iCol)+V(m_sdVGH6, iCol)+V(m_sdVGH7, iCol)+V(m_sdVGH8, iCol)+V(m_sdVGH9, iCol)+V(m_sdVGH10, iCol);
	m_sdVOTOT = V(m_sdVGO1, iCol)+V(m_sdVGO2, iCol)+V(m_sdVGO3, iCol)+V(m_sdVGO4, iCol)+V(m_sdVGO5, iCol)+V(m_sdVGO6, iCol)+V(m_sdVGO7, iCol)+V(m_sdVGO8, iCol)+V(m_sdVGO9, iCol)+V(m_sdVGO10, iCol);
	//-- VF=Void fraction of total core
	//-- VF=(VHTOT+VOTOT)/(VHTOT+VOTOT+VFUEL)
	m_sdVF = (SIM_DATA)((m_sdVF1+m_sdVF2+m_sdVF3+m_sdVF4+m_sdVF5+m_sdVF6+m_sdVF7+m_sdVF8+m_sdVF9+m_sdVF10))/10;
	//---------------------------------------------------------------------------------------------------------------
	//-- PLENUM MODEL
	//---------------------------------------------------------------------------------------------------------------
	//-- Control block for plenum pressure
	m_sdPTRIP = 1.5e+05;
	m_sdPSIG = swtch(m_sdP-m_sdPTRIP);
	D(m_sdVOGO, 1.0e-04*m_sdPSIG, iCol);
	D(m_sdVNI, 1.0e-04*m_sdPSIG, iCol);
	//-- Plenum Volume
	//-- Water make-up model
	m_sdWMUIN = 0.0;
	//WMUIN=water mass flow in (kg/s)
	//-- EQUATION 24 of LA-UR-13-22033
	m_sdWMUDOT = m_sdWMUIN-m_sdROH2A*(SIM_DATA)(V(m_sdVGH10, iCol))/m_sdTAU-m_sdROO2A*(SIM_DATA)(V(m_sdVGO10, iCol))/m_sdTAU2;
	//WMUDOT=time rate of change of makeup water mass
	D(m_sdWMU, m_sdWMUDOT, iCol);
	//WMU=Mass of makeup water (kg)
	m_sdROW = 1000.0;
	//ROW=Density of makeup water (kg/m^3)
	//-- EQUATION 25 of LA-UR-13-22033
	m_sdVFDOT = (SIM_DATA)(m_sdWMUDOT)/m_sdROW;
	//VFDOT=time rate of change of makeup water volume
	//---------------------------------------------------------------------------------------------------------------
	//-- Fuel Expansion
	//---------------------------------------------------------------------------------------------------------------
	//-- VHDT,VODT=Radiolytic gas volume derivatives in core
	m_sdVHDT1 = m_sdVH1DOT+m_sdVH2DOT+m_sdVH3DOT+m_sdVH4DOT+m_sdVH5DOT;
	m_sdVHDT2 = m_sdVH6DOT+m_sdVH7DOT+m_sdVH8DOT+m_sdVH9DOT+m_sdVH10DOT;
	m_sdVODT1 = m_sdVO1DOT+m_sdVO2DOT+m_sdVO3DOT+m_sdVO4DOT+m_sdVO5DOT;
	m_sdVODT2 = m_sdVO6DOT+m_sdVO7DOT+m_sdVO8DOT+m_sdVO9DOT+m_sdVO10DOT;
	//--  EQUATION 21 of LA-UR-13-22033
	m_sdVPDOT = -(m_sdVHDT1+m_sdVHDT2+m_sdVODT1+m_sdVODT2+m_sdVFT);
	//VPDOT=time rate of change of plenum volume
	D(m_sdVP, m_sdVPDOT, iCol);
	//VP=Volume of plenum (m^3)
	//--  Check Plenum Volume
	m_sdVCTOT = m_sdVFUEL+m_sdVHTOT+m_sdVOTOT;
	//VCTOT=Total Core Volume (m^3)
	//---------------------------------------------------------------------------------------------------------------
	//-- Plenum energy equation
	//---------------------------------------------------------------------------------------------------------------
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
	//-- EQUATION 16 of LA-UR-13-22033
	m_sdRHY = (SIM_DATA)(V(m_sdMHY, iCol))/V(m_sdVP, iCol);
	//RHY=H2 density in plenum (kg/m^3)
	m_sdROX = (SIM_DATA)(V(m_sdMOX, iCol))/V(m_sdVP, iCol);
	//ROX=O2 density in plenum (kg/m^3)
	m_sdRN = (SIM_DATA)(V(m_sdMN, iCol))/V(m_sdVP, iCol);
	//RN=N2 density in plenum (kg/m^3)
	//-- Energy flow in=HFH+HFO+HFN
	//-- EQUATION 34 of LA-UR-13-22033
	m_sdHFH = m_sdROH2A*(SIM_DATA)(V(m_sdVGH10, iCol))/m_sdTAU*m_sdENH1;
	m_sdHFO = m_sdROO2A*(SIM_DATA)(V(m_sdVGO10, iCol))/m_sdTAU2*m_sdENO1+m_sdROI*V(m_sdVNI, iCol)*m_sdENO2*(1-swtch(m_sdAIR));
	m_sdHFN = m_sdRNI*V(m_sdVNI, iCol)*m_sdENN2;
	//-- Energy flow out=HFOUT
	//-- EQUATION 35 of LA-UR-13-22033
	m_sdHFOUT = (m_sdRHY*m_sdENH5+m_sdROX*m_sdENO5+m_sdRN*m_sdENN5)*V(m_sdVOGO, iCol);
	//-- CVH,CVO,CVN=Specifc Heat (constant volume) (J/K/kg)
	//-- MCV=Heat capacity of plenum (J/K)
	//-- TP=Plenum Temperature (K)
	m_sdCVH = (SIM_DATA)(5)/2*(SIM_DATA)(m_sdRG)/m_sdHM;
	m_sdCVO = (SIM_DATA)(5)/2*(SIM_DATA)(m_sdRG)/m_sdOM;
	m_sdCVN = (SIM_DATA)(5)/2*(SIM_DATA)(m_sdRG)/m_sdNM;
	//-- EQUATIONS 32,33 of LA-UR-22033
	m_sdMCV = V(m_sdMHY, iCol)*m_sdCVH+V(m_sdMOX, iCol)*m_sdCVO+V(m_sdMN, iCol)*m_sdCVN;
	//-- EQUATION 36 of LA-UR-13-22033
	m_sdTPDOT = (SIM_DATA)(1)/m_sdMCV*(m_sdHFH+m_sdHFO+m_sdHFN-m_sdHFOUT-m_sdP*m_sdVPDOT);
	D(m_sdTP, m_sdTPDOT, iCol);
	//-- Check energy balance in plenum
	D(m_sdHEATIN, m_sdHFH+m_sdHFO+m_sdHFN, iCol);
	D(m_sdHEATOUT, m_sdHFOUT, iCol);
	D(m_sdUP, m_sdMCV*m_sdTPDOT, iCol);
	D(m_sdWORK, m_sdP*m_sdVPDOT, iCol);
	//-- UP=HEATIN-HEATOUT-WORK
	//---------------------------------------------------------------------------------------------------------------
	//-- Continuity equations
	//---------------------------------------------------------------------------------------------------------------
	//-- Hydrogen
	//-- EQUATION 19 of LA-UR-13-22033
	m_sdMHDOT = m_sdROH2A*(SIM_DATA)(V(m_sdVGH10, iCol))/m_sdTAU-m_sdRHY*V(m_sdVOGO, iCol);
	D(m_sdMHY, m_sdMHDOT, iCol);
	//MHY=H2 mass in plenum (kg)
	//-- EQUATION 17 of LA-UR-13-22033
	m_sdPHDOT = (SIM_DATA)(1)/V(m_sdVP, iCol)*(m_sdMHDOT*(SIM_DATA)(m_sdRG)/m_sdHM*V(m_sdTP, iCol)+V(m_sdMHY, iCol)*(SIM_DATA)(m_sdRG)/m_sdHM*m_sdTPDOT-V(m_sdPH, iCol)*m_sdVPDOT);
	D(m_sdPH, m_sdPHDOT, iCol);
	//PH=Partial pressure of H2 in plenum (Pa)
	//-- H2F=H2 mass fraction in plenum (kg)
	m_sdH2F = (SIM_DATA)(V(m_sdMHY, iCol))/(V(m_sdMN, iCol)+V(m_sdMOX, iCol)+V(m_sdMHY, iCol));
	//-- Oxygen
	//-- EQUATIONS 18,19 of LA-UR-13-22033
	m_sdMODOT = m_sdROO2A*(SIM_DATA)(V(m_sdVGO10, iCol))/m_sdTAU2-m_sdROX*V(m_sdVOGO, iCol)+m_sdROI*V(m_sdVNI, iCol)*(1-swtch(m_sdAIR));
	D(m_sdMOX, m_sdMODOT, iCol);
	//MOX=O2 mass in plenum (kg)
	//-- EQUATION 17 of LA-UR-13-22033
	m_sdPODOT = (SIM_DATA)(1)/V(m_sdVP, iCol)*(m_sdMODOT*(SIM_DATA)(m_sdRG)/m_sdOM*V(m_sdTP, iCol)+V(m_sdMOX, iCol)*(SIM_DATA)(m_sdRG)/m_sdOM*m_sdTPDOT-V(m_sdPO, iCol)*m_sdVPDOT);
	D(m_sdPO, m_sdPODOT, iCol);
	//PO=Partial pressure of O2 in plenum (Pa)
	//-- Nitrogen
	//-- EQUATION 18 of LA-UR-22033
	m_sdMNDOT = m_sdRNI*V(m_sdVNI, iCol)-m_sdRN*V(m_sdVOGO, iCol);
	D(m_sdMN, m_sdMNDOT, iCol);
	//MN=N2 mass in plenum (kg)
	//-- EQUATION 17 of LA-UR-22033
	m_sdPNDOT = (SIM_DATA)(1)/V(m_sdVP, iCol)*(m_sdMNDOT*(SIM_DATA)(m_sdRG)/m_sdNM*V(m_sdTP, iCol)+V(m_sdMN, iCol)*(SIM_DATA)(m_sdRG)/m_sdNM*m_sdTPDOT-V(m_sdPN, iCol)*m_sdVPDOT);
	D(m_sdPN, m_sdPNDOT, iCol);
	//PN=Partial pressure of N2 in plenum (Pa)
	m_sdPDOT = m_sdPHDOT+m_sdPODOT+m_sdPNDOT;
	//PDOT=time rate of change of plenum pressure
	//-- Check PN
	//-- PN=RN*RG/NM*TP=MN/VP*RG/NM*TP
	//---------------------------------------------------------------------------------------------------------------
	//-- graph display
	//---------------------------------------------------------------------------------------------------------------
	m_sdRR = m_sdR*20;m_sdTEM = m_sdTEMP*0.10;
	m_sdTEM2 = V(m_sdTC102, iCol)*0.1;m_sdTEM3 = m_sdTC1IN*0.1;
	m_sdTEM4 = V(m_sdTS2, iCol)*0.1;
	m_sdVOG = V(m_sdVOGO, iCol)*800;m_sdPS = -1*m_sdPSIG*7;
	m_sdELOG = (SIM_DATA)(m_sdENLOG)/2;m_sdEPOW = m_sdENP*m_sdPCF*(SIM_DATA)(1.0e+03)/5;
	m_sdVGAS = m_sdVF*100*3;
	//---------------------------------------------------------------------------------------------------------------
}

