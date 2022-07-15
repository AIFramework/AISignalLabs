using AI.DataStructs.Algebraic;
using AI.SignalLab.Modulation.Molation;
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
        double f0 = 9e+3;


        public Form1()
        {
            InitializeComponent();
        }



        private void button1_Click(object sender, EventArgs e)
        {
            Vector signal = ModulationBitsTools.Object2Signal(textBox1.Text, bitDuration:0.0003, sr:sr);
            Vector ask = new Vector(signal.Count);
            for (int i = 0; i < signal.Count; i++)
                ask[i] = signal[i] * Math.Cos((2 * Math.PI * f0 * i) / sr);

            chartVisual1.PlotBlack(ask);
        }
    }
}
