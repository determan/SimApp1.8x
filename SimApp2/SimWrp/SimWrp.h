// SimWrp.h

#pragma once
#include "..\SimDll13\SupoModel.h"
#include "..\SimDll13\SupoModel2.h"
#include "..\SimDll13\AD135.h"


using namespace System;
using namespace System::Runtime::InteropServices;

namespace SimWrp
{
	public ref class SimModel
	{
	public:
//		SimModel(){ simModel = dynamic_cast<Sim::Model*> (new Sim::SupoModel()); }
		SimModel(){ simModel = dynamic_cast<Sim::Model*> (new Sim::AD135()); }
//		SimModel(){ simModel = dynamic_cast<Sim::Model*> (new Sim::SupoModel2()); }
		~SimModel(){ delete simModel; }
		void Run(IntPtr ip, float fTS, int iNS, float fMT, bool bC, int iFQ, int iMult, int iNumPlot, System::IntPtr ia, System::IntPtr fa, System::IntPtr ia2, System::IntPtr da2)
		{
			m_iPtr = ip.ToPointer();
			simModel->Run((Sim::PLOTCB)m_iPtr, fTS, iNS, fMT, bC, iFQ, iMult, iNumPlot, (int*)((int)ia), (float*)((int)fa), (int*)((int)ia2), (double*)((int)da2));
		}
		void ICs(System::IntPtr da)
		{
			simModel->ICs((double*)((int)da));
		}
		void Continue(bool bContinue)
		{ 
			simModel->Continue(bContinue); 
		}
		void SetNumIC(int iNIC){ simModel->SetNumIC(iNIC); }
		void GetName(System::String^% sName){ sName = (gcnew String(simModel->Name()))->ToString(); }
		int SSize(){ return simModel->StateSize(); }
		int NSSize(){ return simModel->NonStateSize(); }
		int CSize(){ return simModel->ConstSize(); }
		float YScale(){ return (float)simModel->YScale(); }
		int NumCustIcTabs(){ return simModel->NumCustIcTabs(); }
		void CloseFile(){ simModel->CloseFile(); }
	private:
		Sim::Model* simModel;
		IntPtr m_dlg;
		void* m_iPtr;
	};
};
//#pragma unmanaged

