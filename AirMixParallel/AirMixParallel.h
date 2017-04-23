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
		Secundova = 1, //��������� ������
		KE = 2 //�-� ������
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
		/// ����� ������� ���� ��������
		/// </summary>
		enum class PressureCalcMethod {
			///<summary>��-� ��������</summary>
			Poisson,
			///<summary>����� ������ �����������</summary>
			Weak�ompressibility
		};

		/// <summary>
		/// ����� ������� ��������� �����-������
		/// </summary>
		enum class NavierStokesCalcMethod{
			///<summary>����� �����</summary>
			ExplicitScheme,
			///<summary>������� �����</summary>
			ImplicitScheme
		};

		PU(double tau, double ro, double nuM, int x0, int len, double h, int X, int Y, double tmax);

		/// <summary> ���������� � ����������� OpenMP </summary>
		void CalcOpenMP(PressureCalcMethod pcm, NavierStokesCalcMethod nscm, array<double> ^Ux, array<double> ^Uy);

		/// <summary> ���������� � ����������� CUDA </summary>
		void CalcCUDA(PressureCalcMethod pcm, NavierStokesCalcMethod nscm, array<double> ^Ux, array<double> ^Uy);


	};


}
