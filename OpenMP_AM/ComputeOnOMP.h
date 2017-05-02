#pragma once
#include  <iostream>
#include <omp.h>

class ComputeOnOMP {
public:
	//методы для вычисления времени
	class Time{
		double Tn, Tk;
		time_t start, end;
	public:
		double time;

		void tn(){
			Tn = omp_get_wtime();
		}
		double tk(){
			Tk = omp_get_wtime();
			time = Tk - Tn;
			return time;
		}

	};

	class PU {
	private:
	
		int X, Y;
		double *Ux,*Uy, *Uxn, *Uyn;
		double tau;
		double nuM;
		double *divU;
		double *P;
		double ro;
		double h;
		int x0, len;
		const double b = 100;
		
		
	public:
		FILE *f;
		enum PressureCalcMethod {
			Poisson,//ур-е Пуассона
			WeakСompressibility //метод слабой сжимаемости
		};

		enum NavierStokesCalcMethod{
			ExplicitScheme,//явная схема
			ImplicitScheme //неявная схема
		};

		double Calculation(PressureCalcMethod pressureMethod, NavierStokesCalcMethod navierStokesMethod, double *Ux, double *Uy, double tmax);
		void _Poisson();
		void _WeakСompressibility();
		void Speeds();

		PU(double tau, double ro, double nuM,int x0, int len, double h, int X, int Y);
		~PU();
			
	};


};
