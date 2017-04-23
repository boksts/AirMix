#pragma once


class ComputeOnOMP {
public:
	class PU {
	public:
	
		int X, Y;
		double *Ux,*Uy, *Uxn, *Uyn;
		double tau;
		double nuM;
		double *divU;
		double *P;
		double ro;
		double t = 0, tmax;
		double h;
		int x0, len;
		const double nu = 1;
		const double b = 100;


		enum PressureCalcMethod {
			Poisson,//ур-е Пуассона
			WeakСompressibility //метод слабой сжимаемости
		};

		enum NavierStokesCalcMethod{
			ExplicitScheme,//явная схема
			ImplicitScheme //неявная схема
		};

		void Calculation(PressureCalcMethod pressureMethod, NavierStokesCalcMethod navierStokesMethod, double *Ux, double *Uy);
		void _Poisson();
		void _WeakСompressibility();
		void Speeds();

		PU(double tau, double ro, double nuM,int x0, int len, double h, int X, int Y, double tmax = 0.0);

	};


};
