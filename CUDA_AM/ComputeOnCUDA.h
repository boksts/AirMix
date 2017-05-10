#pragma once

#include <stdexcept>

using namespace std;

class ComputeOnCUDA {
public:
	enum TurbulenceModel {
		Secundova = 1,
		KE = 2
	};

	class PU {
	public:

		enum PressureCalcMethod {
			Poisson,//
			WeakŃompressibility //
		};

		enum NavierStokesCalcMethod{
			ExplicitScheme,//
			ImplicitScheme //
		};

		double Calculation(PressureCalcMethod pressureCalcMethod, NavierStokesCalcMethod navierStokesCalcMethod, double* Ux, double* Uy, double tmax);

		PU(double tau, double ro, double nuM, int x0, int len, double h, int X, int Y);
		~PU();
	};

	
	class WPsi {
	private:
	
	public:
		enum HelmholtzCalcMethod {
			ExplicitScheme,
			ImplicitScheme
		};
		
		WPsi(double tau, double nuM, int x0, int len, double h, int X, int Y, double *Ux, double *Uy);

		double Calculation(HelmholtzCalcMethod hcm, TurbulenceModel tm, double* Ux, double* Uy, double tmax);

		~WPsi();

	};

};
	
	






