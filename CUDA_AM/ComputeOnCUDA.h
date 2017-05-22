#pragma once

#include <stdexcept>

using namespace std;

class ComputeOnCUDA {
public:
	
	enum TurbulenceModel {
		Secundova = 1,//модель Секундова
		KE = 2//К-Е модель
	};

	class PU {
	public:
		enum PressureCalcMethod {
			Poisson,//уравнение Пуассона
			WeakCompressibility //метод слабой сжимаемости
		};

		enum NavierStokesCalcMethod{
			ExplicitScheme,//явная схема
			ImplicitScheme //неявная схема
		};

		PU(double tau, double ro, double nuM, int x0, int len, double h, int X, int Y);
		
		double Calculation(PressureCalcMethod pressureCalcMethod, NavierStokesCalcMethod navierStokesCalcMethod, double* Ux, double* Uy, double *Temp, double tmax);
	
		~PU();
	};

	class WPsi {
	private:
	
	public:
		enum HelmholtzCalcMethod {
			ExplicitScheme,//явная схема
			ImplicitScheme //неявная схема
		};
		
		WPsi(double tau, double nuM, int x0, int len, double h, int X, int Y, double *Ux,  double *Uy);

		double Calculation(HelmholtzCalcMethod hcm, TurbulenceModel tm, double* Ux, double* Uy, double *Temp, double tmax);

		~WPsi();

	};
};
	
	






