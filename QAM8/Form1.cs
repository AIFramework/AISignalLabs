using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Numerics;
using AI.SignalLab.Modulation.Modulation;
using AI.DataStructs.WithComplexElements;
using AI.DataStructs.Algebraic;
using AI;

namespace QAM8
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            byte[] data = ModulationBitsTools.Obj2Bytes("1>------ Сборка начата: проект: QAM8, Конфигурация: Debug Any CPU ------\r\n1>  QAM8 -> C:\\Users\\ZZZ\\Documents\\GitHub\\AISignalLabs\\QAM8\\bin\\Debug\\QAM8.exe\r\n========== Сборка: успешно выполнено — 1 , со сбоем — 0, в актуальном состоянии — 0, пропущено — 0 ==========\r\n========== Прошло 00:00,446 ==========");// Пример данных
            Complex[] modulatedSignal = QAM8Modulator.Modulate(data);

            ComplexVector complexes = new ComplexVector(modulatedSignal);
            complexes *= new Complex(1,1)*Math.Sqrt(2)/2;
            chartVisual2.ScatterComplexPlane(complexes);

            var signal = Interp(complexes, nS);
            t = Vector.Seq(0, 1, signal.Count) / sr;

            Vector qud = signal.ImaginaryVector * t.Transform(x => -Math.Sin(2 * x * Math.PI * f0));
            Vector inf = signal.RealVector * t.Transform(x => Math.Cos(2 * x * Math.PI * f0));

            Vector mod = qud + inf;

            chartVisual1.Clear();
            chartVisual1.AddPlot(t, mod, "Mod QAM8");
            Sound sound = new Sound();
            sound.SaveVector("test.wav", mod, sr);
            //chartVisual1.AddPlot(t, inf, "I");
        }

        double f0 = 2e+3;
        int sr = (int)441e+2;
        int nS = 300;
        Vector t;


        static ComplexVector Interp(ComplexVector complexes, int n) 
        {
            ComplexVector ret = new ComplexVector(complexes.Count*n);

            for (int i = 0, k = 0; i < complexes.Count; i++)
                for (int j = 0; j < n; j++)
                    ret[k++] = complexes[i];
            return ret; 
        }
    }


    class QAM8Modulator
    {
        public static Complex[] Modulate(byte[] data)
        {
            List<Complex> symbols = new List<Complex>();

            for (int i = 0; i < data.Length; i += 3)
            {
                int bit1 = (i < data.Length) ? (data[i] & 0x01) : 0;
                int bit2 = (i + 1 < data.Length) ? (data[i + 1] & 0x01) : 0;
                int bit3 = (i + 2 < data.Length) ? (data[i + 2] & 0x01) : 0;

                double real = GetRealPart(bit1, bit2, bit3);
                double imag = GetImaginaryPart(bit1, bit2, bit3);

                symbols.Add(new Complex(real, imag));
            }

            return symbols.ToArray();
        }

        static double GetRealPart(int bit1, int bit2, int bit3)
        {
            // Простая схема: используем 4 различных уровня амплитуды
            if (bit1 == 0 && bit2 == 0 && bit3 == 0) return 1.0;
            if (bit1 == 0 && bit2 == 0 && bit3 == 1) return 2.0;
            if (bit1 == 1 && bit2 == 0 && bit3 == 0) return -1.0;
            if (bit1 == 1 && bit2 == 0 && bit3 == 1) return -2.0;

            return 0.0; 
        }

        static double GetImaginaryPart(int bit1, int bit2, int bit3)
        {
            if (bit1 == 0 && bit2 == 1 && bit3 == 0) return 1.0;
            if (bit1 == 0 && bit2 == 1 && bit3 == 1) return 2.0;
            if (bit1 == 1 && bit2 == 1 && bit3 == 0) return -1.0;
            if (bit1 == 1 && bit2 == 1 && bit3 == 1) return -2.0;

            return 0.0;
        }

    }

}
