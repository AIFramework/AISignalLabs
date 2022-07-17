using AI.BackEnds.DSP.NWaves.Filters.Butterworth;
using AI.DataStructs.Algebraic;
using AI.DataStructs.WithComplexElements;
using AI.DSP.IIR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace AI.SignalLab.Modulation.Demodulation
{
    public class QudratureDemodulation
    {
        IIRFilter LPFButterworthI, LPFButterworthQ;
        double _sr, _f0;
        double dt;

        public QudratureDemodulation(double f0, double sr)
        {
            _f0 = f0;
            _sr = sr;
            dt = 1.0 / sr;
            double fCNorm = f0 / (sr);
            var filter = new LowPassFilter(fCNorm, 7);
            LPFButterworthI = new IIRFilter(filter._a, filter._b);
            LPFButterworthQ = new IIRFilter(filter._a, filter._b);
        }

        /// <summary>
        /// Получение синфазной и квадратурной компонент
        /// </summary>
        public ComplexVector GetIQComponents(Vector signal) 
        {
            int len = signal.Count;
            ComplexVector result = new ComplexVector(len);

            for (int i = 0; i < len; i++)
            {
                double arg = 2 * Math.PI * _f0 * i * dt;
                result[i] = new Complex(
                     LPFButterworthI.FilterOutp(Math.Cos(arg) * signal[i]),
                     LPFButterworthQ.FilterOutp(Math.Sin(arg) * signal[i])
                    );
            }

            return result;
        }


    }
}
