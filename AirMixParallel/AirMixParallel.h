// Wrapper.h

#pragma once

#include "..\OpenMP_AM\ComputeOnOMP.h"
#include "..\OpenMP_AM\ComputeOnOMP.cpp"

#include "..\CUDA_AM\ComputeOnCUDA.h"
#include "..\CUDA_AM\ComputeOnCUDA.cpp"


using namespace System;
using namespace System::Runtime::InteropServices;

namespace AirMixParallel {

	public enum TurbulenceModel {
		Secundova = 1, //Секундова модель
		KE = 2 //К-Е модель
	};

	public ref class PU
	{

	private:
		array<double> ^Ux;
		array<double> ^Uy;
		int X;
		int Y;
		double *_Ux, *_Uy;

		ComputeOnOMP::PU *computeOnOMP;
		ComputeOnCUDA::PU *computeOnCUDA;

	public:

		/// <summary>
		/// Метод расчета поля давления
		/// </summary>
		enum class PressureCalcMethod {
			///<summary>ур-е Пуассона</summary>
			Poisson,
			///<summary>метод слабой сжимаемости</summary>
			WeakСompressibility
		};

		/// <summary>
		/// Схема расчета уравнения Навье-Стокса
		/// </summary>
		enum class NavierStokesCalcMethod{
			///<summary>явная схема</summary>
			ExplicitScheme,
			///<summary>неявная схема</summary>
			ImplicitScheme
		};

		PU(double tau, double ro, double nuM, int x0, int len, double h, int X, int Y, double tmax);

		/// <summary> Вычисления с применением OpenMP </summary>
		void CalcOpenMP(PressureCalcMethod pcm, NavierStokesCalcMethod nscm, array<double> ^Ux, array<double> ^Uy);

		/// <summary> Вычисления с применением CUDA </summary>
		void CalcCUDA(PressureCalcMethod pcm, NavierStokesCalcMethod nscm, array<double> ^Ux, array<double> ^Uy);


	};


}
