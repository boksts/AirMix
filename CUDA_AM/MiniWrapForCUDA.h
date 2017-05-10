#pragma once
#include "ComputeOnCUDA.h"

double ComputePU(ComputeOnCUDA::PU::PressureCalcMethod pressureMethod, ComputeOnCUDA::PU::NavierStokesCalcMethod navierStokesMethod, double *Ux, double *Uy, double tmax);
void ConstructorPU(double tau, double ro, double nuM, int x0, int len, double h, int X, int Y);
void DestructorPU();


double ComputeWPsi(ComputeOnCUDA::WPsi::HelmholtzCalcMethod hcm, ComputeOnCUDA::TurbulenceModel tm, double *Ux, double *Uy, double tmax);
void ConstructorWPsi(double tau, double _nuM, int x0, int len, double h, int X, int Y, double *Ux, double *Uy);
void DestructorWPsi();

