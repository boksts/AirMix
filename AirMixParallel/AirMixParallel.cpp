// Главный DLL-файл.

#include "stdafx.h"

#include "AirMixParallel.h"

AirMixParallel::PU::PU(PPT _ppt, double tau, double ro, double nuM, int  x0, int len, double h, int X, int Y) {

	this->X = X;
	this->Y = Y;
	ppt = _ppt;

	_Ux = new double[X*Y];
	_Uy = new double[X*Y];
	_Temp = new double[X*Y];

	switch (ppt) {
	case PPT::OpenMP:
		computeOnOMP = new ComputeOnOMP::PU(tau, ro, nuM, x0, len, h, X, Y);
		break;
	case PPT::CUDA:
		computeOnCUDA = new ComputeOnCUDA::PU(tau, ro, nuM, x0, len, h, X, Y);
		break;
	}
}


double AirMixParallel::PU::Calculation(PressureCalcMethod pcm, NavierStokesCalcMethod nscm, TurbulenceModel tm, array<double> ^Ux, array<double> ^Uy, array<double> ^Temp, double tmax) {

	//копирование начальных данных из управляемого кода в неуправляемый
	System::Runtime::InteropServices::Marshal::Copy(Ux, 0, (System::IntPtr)_Ux, X*Y);
	System::Runtime::InteropServices::Marshal::Copy(Uy, 0, (System::IntPtr)_Uy, X*Y);
	System::Runtime::InteropServices::Marshal::Copy(Temp, 0, (System::IntPtr)_Temp, X*Y);

	switch (ppt) {
	case PPT::OpenMP:
		time = computeOnOMP->Calculation(static_cast<ComputeOnOMP::PU::PressureCalcMethod>(pcm), static_cast<ComputeOnOMP::PU::NavierStokesCalcMethod>(nscm), _Ux, _Uy, _Temp, tmax);
		break;
	case PPT::CUDA:
		time = computeOnCUDA->Calculation(static_cast<ComputeOnCUDA::PU::PressureCalcMethod>(pcm), static_cast<ComputeOnCUDA::PU::NavierStokesCalcMethod>(nscm), _Ux, _Uy, _Temp, tmax);
		break;
	}

	//копирование результата из неуправляемого кода в управляемый
	System::Runtime::InteropServices::Marshal::Copy((System::IntPtr)_Ux, Ux, 0, X*Y);
	System::Runtime::InteropServices::Marshal::Copy((System::IntPtr)_Uy, Uy, 0, X*Y);
	System::Runtime::InteropServices::Marshal::Copy((System::IntPtr)_Temp, Temp, 0, X*Y);

	return time;
}


AirMixParallel::PU::~PU() {

	delete[] _Ux;
	delete[] _Uy;
	delete[] _Temp;

	switch (ppt) {
		case PPT::OpenMP:
			computeOnOMP->~PU();
			delete computeOnOMP;
		break;
		case PPT::CUDA:
			computeOnCUDA->~PU();
			delete computeOnCUDA;
		break;
	}
}


/*==========================================================================*/

AirMixParallel::WPsi::WPsi(PPT _ppt, double tau, double nuM, int x0, int len, double h, int X, int Y, array<double> ^Ux, array<double> ^Uy) {
	
	ppt = _ppt;
	this->X = X;
	this->Y = Y;
	
	_Ux = new double[X*Y];
	_Uy = new double[X*Y];
	_Temp = new double[X*Y];

	//копирование начальных данных из управляемого кода в неуправляемый
	System::Runtime::InteropServices::Marshal::Copy(Ux, 0, (System::IntPtr)_Ux, X*Y);
	System::Runtime::InteropServices::Marshal::Copy(Uy, 0, (System::IntPtr)_Uy, X*Y);

	switch (ppt) {
	case PPT::OpenMP:
		computeOnOMP = new ComputeOnOMP::WPsi(tau, nuM, x0, len, h, X, Y, _Ux, _Uy);
		break;
	case PPT::CUDA:
		computeOnCUDA = new ComputeOnCUDA::WPsi(tau, nuM, x0, len, h, X, Y, _Ux, _Uy);
		break;
	}	
}


double AirMixParallel::WPsi::Calculation(HelmholtzCalcMethod hcm, TurbulenceModel tm, array<double> ^Ux, array<double> ^Uy, array<double> ^Temp, double tmax) {	

	//копирование начальных данных из управляемого кода в неуправляемый
	System::Runtime::InteropServices::Marshal::Copy(Ux, 0, (System::IntPtr)_Ux, X*Y);
	System::Runtime::InteropServices::Marshal::Copy(Uy, 0, (System::IntPtr)_Uy, X*Y);
	System::Runtime::InteropServices::Marshal::Copy(Temp, 0, (System::IntPtr)_Temp, X*Y);

	switch (ppt) {
	case PPT::OpenMP:
		time = computeOnOMP->Calculation(static_cast<ComputeOnOMP::WPsi::HelmholtzCalcMethod>(hcm), static_cast<ComputeOnOMP::TurbulenceModel>(tm), _Ux, _Uy, _Temp, tmax);
		break;
	case PPT::CUDA:
		time = computeOnCUDA->Calculation(static_cast<ComputeOnCUDA::WPsi::HelmholtzCalcMethod>(hcm), static_cast<ComputeOnCUDA::TurbulenceModel>(tm), _Ux, _Uy, _Temp, tmax);
		break;
	}
	
	//копирование результата из неуправляемого кода в управляемый
	System::Runtime::InteropServices::Marshal::Copy((System::IntPtr)_Ux, Ux, 0, X*Y);
	System::Runtime::InteropServices::Marshal::Copy((System::IntPtr)_Uy, Uy, 0, X*Y);
	System::Runtime::InteropServices::Marshal::Copy((System::IntPtr)_Temp, Temp, 0, X*Y);

	return time;
}


AirMixParallel::WPsi::~WPsi() {
	delete[] _Ux;
	delete[] _Uy;
	delete[] _Temp;

	switch (ppt) {
	case PPT::OpenMP:
		computeOnOMP->~WPsi();
		delete computeOnOMP;
		break;
	case PPT::CUDA:
		computeOnCUDA->~WPsi();
		delete computeOnCUDA;
		break;
	}
}