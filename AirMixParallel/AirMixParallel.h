// Wrapper.h

#pragma once

#include "..\OpenMP_AM\ComputeOnOMP.h"
#include "..\OpenMP_AM\OMP_PU.cpp"
#include "..\OpenMP_AM\OMP_WPsi.cpp"
#include "..\OpenMP_AM\Temperature.cpp"


#include "..\CUDA_AM\ComputeOnCUDA.h"
#include "..\CUDA_AM\ComputeOnCUDA.cpp"


using namespace System;
using namespace System::Runtime::InteropServices;

namespace AirMixParallel {

	/// <summary>
	/// Модель турбулентности
	/// </summary>
	 public enum class TurbulenceModel {
		///<summary>Секундова модель</summary>
		Secundova = 1,
		///<summary>К-Е модель</summary>
		KE = 2
	};

	/// <summary>
	/// Технология параллельного программирования
	/// </summary>
	 public enum class PPT{
		///<summary>OpenMP</summary>
		OpenMP,
		///<summary>CUDA</summary>
		CUDA
	};

	public ref class PU
	{

	private:
		array<double> ^Ux;
		array<double> ^Uy;
		array<double> ^Temp;
		int X;
		int Y;
		double *_Ux, *_Uy, *_Temp;

		ComputeOnOMP::PU *computeOnOMP;
		ComputeOnCUDA::PU *computeOnCUDA;
		PPT ppt;

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



		///<summary>Установка параметров расчета</summary>
		/// <param name="ppt">технология параллельного программирования</param>
		/// <param name="tau">шаг по времени</param>
		/// <param name="ro">плотность</param>
		///  <param name="nuM">молекулярная вязкость</param>
		///  <param name="x0">расположение отвестия снизу от точки х0...</param>
		///  <param name="len">...длиной len</param>
		/// <param name="h">шаг по сетке</param>
		/// <param name="X">число точек по оси Х</param>
		///  <param name="Y">число точек по оси У</param>
		PU(PPT ppt, double tau, double ro, double nuM, int x0, int len, double h, int X, int Y);

		

		/// <summary> Вычисления с применением парралельных технологий</summary>
		/// <param name="pcm">метод расчета поля давления</param>
		/// <param name="nscm">схема расчета уравнения Навье-Стокса</param>
		///  <param name="tm">модель турбулентности (turbulenceModel = 0 если турбулентность не расчитывается)</param>
		///  <param name="Ux">скорости Ux</param>
		///  <param name="Uy">скорости Uy</param>
		/// <param name="tmax">время расчета</param>
		double Calculation(PressureCalcMethod pcm, NavierStokesCalcMethod nscm, TurbulenceModel tm, array<double> ^Ux, array<double> ^Uy, array<double> ^Temp, double tmax);

		~PU();

	};
	
	public ref class WPsi
	{

	private:
		array<double> ^Ux;
		array<double> ^Uy;
		array<double> ^Temp;
		int X;
		int Y;
		double *_Ux, *_Uy, *_Temp;

		ComputeOnOMP::WPsi *computeOnOMP;
		ComputeOnCUDA::WPsi *computeOnCUDA;
		PPT ppt;

	public:

		/// <summary>
		/// Схема расчета уравнения Гельмгольца
		/// </summary>
		enum class HelmholtzCalcMethod {
			///<summary>явная схема</summary>
			ExplicitScheme,
			///<summary>неявная схема</summary>
			ImplicitScheme
		};
	

		///<summary>Установка параметров расчета</summary>
		/// <param name="tau">шаг по времени</param>
		///  <param name="nuM">молекулярная вязкость</param>
		///  <param name="x0">расположение отвестия снизу от точки х0...</param>
		///  <param name="len">...длиной len</param>
		/// <param name="h">шаг по сетке</param>
		/// <param name="X">число точек по оси Х</param>
		///  <param name="Y">число точек по оси У</param>
		WPsi(PPT ppt, double tau, double nuM, int x0, int len, double h, int X, int Y, array<double> ^Ux, array<double> ^Uy);

		///<summary>Расчет поля скоростей</summary>
		/// <param name="hcm">схема расчета уравнения Гельмгольца</param>
		///  <param name="tm">модель турбулентности (turbulenceModel = 0 если турбулентность не расчитывается)</param>
		/// <param name="tmax">время расчета</param>
		double Calculation(HelmholtzCalcMethod hcm, TurbulenceModel tm, array<double> ^Ux, array<double> ^Uy, array<double> ^Temp, double tmax);

		~WPsi();

	};
}
