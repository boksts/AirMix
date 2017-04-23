// Главный DLL-файл.

#include "stdafx.h"

#include "AirMixParallel.h"

AirMixParallel::PU::PU(double tau, double ro, double nuM, int  x0, int len, double h, int X, int Y, double tmax) {

	this->X = X;
	this->Y = Y;

	computeOnOMP = new ComputeOnOMP::PU(tau, ro, nuM, x0, len, h, X, Y, tmax);
	computeOnCUDA = new ComputeOnCUDA::PU(tau, ro, nuM, x0, len, h, X, Y, tmax);

}


void AirMixParallel::PU::CalcOpenMP(PressureCalcMethod pcm, NavierStokesCalcMethod nscm, array<double> ^Ux, array<double> ^Uy) {

	this->Ux = Ux;
	this->Uy = Uy;

	_Ux = new double[X*Y];
	_Uy = new double[X*Y];

	//копирование начальных данных из управляемого кода в неуправляемый
	System::Runtime::InteropServices::Marshal::Copy(Ux, 0, (System::IntPtr)_Ux, X*Y);
	System::Runtime::InteropServices::Marshal::Copy(Uy, 0, (System::IntPtr)_Uy, X*Y);

	computeOnOMP->Calculation(static_cast<ComputeOnOMP::PU::PressureCalcMethod>(pcm), static_cast<ComputeOnOMP::PU::NavierStokesCalcMethod>(nscm), _Ux, _Uy);

	//копирование результата из неуправляемого кода в управляемый
	System::Runtime::InteropServices::Marshal::Copy((System::IntPtr)_Ux, Ux, 0, X*Y);
	System::Runtime::InteropServices::Marshal::Copy((System::IntPtr)_Uy, Uy, 0, X*Y);

}

void AirMixParallel::PU::CalcCUDA(PressureCalcMethod pcm, NavierStokesCalcMethod nscm, array<double> ^Ux, array<double> ^Uy) {
	this->Ux = Ux;
	this->Uy = Uy;

	_Ux = new double[X*Y];
	_Uy = new double[X*Y];

	//копирование начальных данных из управляемого кода в неуправляемый
	System::Runtime::InteropServices::Marshal::Copy(Ux, 0, (System::IntPtr)_Ux, X*Y);
	System::Runtime::InteropServices::Marshal::Copy(Uy, 0, (System::IntPtr)_Uy, X*Y);

	computeOnCUDA->Calculation(static_cast<ComputeOnCUDA::PU::PressureCalcMethod>(pcm), static_cast<ComputeOnCUDA::PU::NavierStokesCalcMethod>(nscm), _Ux, _Uy);

	//копирование результата из неуправляемого кода в управляемый
	System::Runtime::InteropServices::Marshal::Copy((System::IntPtr)_Ux, Ux, 0, X*Y);
	System::Runtime::InteropServices::Marshal::Copy((System::IntPtr)_Uy, Uy, 0, X*Y);

}


