#pragma once

#include "ComputeOnCUDA.h"
#include "MiniWrapForCuda.h"

ComputeOnCUDA::PU::PU(double tau, double ro, double nuM, int x0, int len, double h, int X, int Y) {

	ConstructorPU(tau, ro, nuM, x0, len, h, X, Y);
}


ComputeOnCUDA::PU::~PU() {
	DestructorPU();
}
double ComputeOnCUDA::PU::Calculation(PressureCalcMethod pressureMethod, NavierStokesCalcMethod navierStokesMethod, double* Ux, double* Uy, double tmax) {
	return ComputePU(pressureMethod, navierStokesMethod, Ux, Uy, tmax);
}



ComputeOnCUDA::WPsi::WPsi(double tau, double nuM, int x0, int len, double h, int X, int Y, double *Ux, double *Uy) {
	ConstructorWPsi(tau, nuM, x0, len, h, X, Y,Ux,Uy);
}

double ComputeOnCUDA::WPsi::Calculation(HelmholtzCalcMethod hcm, TurbulenceModel tm, double* Ux, double* Uy, double tmax) {
	return ComputeWPsi(hcm, tm, Ux, Uy, tmax);

}




ComputeOnCUDA::WPsi::~WPsi() {
	DestructorWPsi();
}


