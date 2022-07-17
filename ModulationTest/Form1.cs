using AI.DataStructs.Algebraic;
using AI.SignalLab.Modulation.Demodulation;
using AI.SignalLab.Modulation.Modulation;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ModulationTest
{
    public partial class Form1 : Form
    {
        double sr = 144.1e+3;// Частота дискретизации
        double f0 = 3e+3;
        QudratureDemodulation qudrature;


        public Form1()
        {
            InitializeComponent();
            qudrature = new QudratureDemodulation(f0, sr);
        }



        private void button1_Click(object sender, EventArgs e)
        {
            Vector signal = ModulationBitsTools.Object2Signal(textBox1.Text, bitDuration:0.0009, sr:sr);
            Vector ask = new Vector(signal.Count);
            for (int i = 0; i < signal.Count; i++)
            {
                double arg = 2 * Math.PI * f0 * i / sr;
                ask[i] = signal[i] *(Math.Cos(arg)+ 2*Math.Sin(arg));
            }

            var iq = qudrature.GetIQComponents(ask);

            chartVisual1.PlotBlack(ask);
            chartVisual2.ScatterComplexPlane(iq);

            Vector x = Vector.SeqBeginsWithZero(1,ask.Count);
            chartVisual3.Clear();
            chartVisual3.AddPlot(x, iq.RealVector, "Синфазная составляющая");
            chartVisual3.AddPlot(x, iq.ImaginaryVector, "Квадратурная составляющая");

        }
    }
}
