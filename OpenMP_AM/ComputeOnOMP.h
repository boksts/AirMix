#pragma once
#include  <iostream>
#include <omp.h>

class ComputeOnOMP {
public:

	enum TurbulenceModel {
		Secundova = 1,
		KE = 2
	};

	//методы для вычисления времени
	class Time {
		double Tn, Tk;
		time_t start, end;
	public:
		double time;
		void tn() {
			Tn = omp_get_wtime();
		}

		double tk() {
			Tk = omp_get_wtime();
			time = Tk - Tn;
			return time;
		}
	};

	class PU {
	private:
		int X, Y;
		int x0, len;
		double tau;
		double nuM;
		double ro;
		double h;
		double *Ux, *Uy, *Uxn, *Uyn;
		double *divU;
		double *P;
		double *Temp;
		FILE* f;
		
		//коэффициент в методе слабой сжимаемости
		const double b = 100;
		//ускорение свободного падения
		const double g = 9.8;
		//коэффициент объемного расширения воздуха
		double betta = 3.665 * pow(10.0, -3.0);
		
	public:
		enum PressureCalcMethod {
			Poisson,//ур-е Пуассона
			WeakСompressibility //метод слабой сжимаемости
		};

		enum NavierStokesCalcMethod {
			ExplicitScheme,//явная схема
			ImplicitScheme //неявная схема
		};

		PU(double tau, double ro, double nuM, int x0, int len, double h, int X, int Y);
		
		double Calculation(PressureCalcMethod pressureMethod, NavierStokesCalcMethod navierStokesMethod, double* Ux, double* Uy, double* Temp, double tmax);
		void _Poisson();
		void _WeakСompressibility();
		void Speeds();
		
		~PU();
	};

	class WPsi {
	private:
		int X, Y;
		int x0, len;
		double tau;
		double nuM;
		double h;
		double *Ux, *Uy, *Uxn, *Uyn;
		double *psi, *psin, *w, *wn;
		double *Temp;		
		FILE* f;

		//допустимая погрешность в методе Пуассона
		const double epsPsi = 0.001;
		//для метода верхней релаксации
		const double tetta = 1.85;
		//ускорение свободного падения
		const double g = 9.8;
		//коэффициент объемного расширения воздуха
		double betta = 3.665 * pow(10.0, -3.0);

	public:
		enum HelmholtzCalcMethod {
			ExplicitScheme,
			ImplicitScheme
		};

		WPsi(double tau, double nuM, int x0, int len, double h, int X, int Y, double* Ux, double* Uy);
		
		double Calculation(HelmholtzCalcMethod hcm, TurbulenceModel tm, double* Ux, double* Uy, double* Temp, double tmax);
		void Init();
		void CurrentFunction();
		void Vortex();
		void Speeds();
		
		~WPsi();
	};

private:
	class Temperature {
	private:
		int X, Y;
		int x0, len;
		double tau;
		double nuM;
		double h;
		double *Tempn;
			
		//коэффициент температуропроводности
		const double c = 1.0;
		//коэффициент температуропроводности стен
		const double a = 0.1;

	public:
		Temperature(double tau, double nuM, int x0, int len, double h, int X, int Y);	

		double *CalcTemp(double* Ux, double* Uy, double*Temp);

		~Temperature();
	};

};
