using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ZedGraph;

namespace AirMix.GraficsText {
    public partial class ChartForm : Form {
        private GraphPane pane;
        public ChartForm() {
            InitializeComponent();
            pane = new GraphPane();
        }
  
        //сохранение полученного графика в файл
        private void btnSaveFile_Click(object sender, EventArgs e) {
            zgcChart.SaveAsBitmap();
        }

        public void Graph(double[] data, int N, Color color, string[] dimension, string str = "") {
            // Создадим список точек
            PointPairList list = new PointPairList();
            pane = zgcChart.GraphPane;
            zgcChart.Invalidate();
           // pane.CurveList.Clear();

            //список точек
            for (int x = 0; x < N; x++) {
                list.Add(x, data[x]);
            }
            //масштаб по умолчанию для оси X
            pane.XAxis.Scale.MinAuto = true;
            pane.XAxis.Scale.MaxAuto = true;

            //масштаб по умолчанию для оси Y
            pane.YAxis.Scale.MinAuto = true;
            pane.YAxis.Scale.MaxAuto = true;

            pane.XAxis.Scale.FontSpec.Angle = 45;
            pane.XAxis.Type = AxisType.Text;
            pane.XAxis.Scale.TextLabels = dimension;

            pane.YAxis.Scale.Mag = 0;

            pane.XAxis.Title.Text = "Размер расчетной области (сетки)";
            pane.YAxis.Title.Text = "Время, сек";

            pane.Title.Text = "Зависимость времени выполнения от размера расчетной области";

            //отображение сетки напротив крупных рисок по оси Y
            pane.YAxis.MajorGrid.IsVisible = true;

            //длина штрихов равна 10 пикселям
            pane.YAxis.MajorGrid.DashOn = 10;
            //5 пикселей пропуск
            pane.YAxis.MajorGrid.DashOff = 5;

            //размеры шрифтов для подписей по осям
            pane.XAxis.Title.FontSpec.Size = 10;
            pane.YAxis.Title.FontSpec.Size = 10;

            //размеры шрифтов для меток вдоль осей
            pane.XAxis.Scale.FontSpec.Size = 12;
            pane.YAxis.Scale.FontSpec.Size = 12;

            LineItem myCurve = pane.AddCurve(str, list, color, SymbolType.None);
            myCurve.Line.Width = 2f;
            myCurve.Line.IsAntiAlias = true;//сглаживание

            zgcChart.AxisChange();

            zgcChart.Invalidate();
        }
    }
}
