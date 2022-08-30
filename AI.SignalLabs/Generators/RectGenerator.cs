using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AI.SignalLab.Generators
{
    public class RectGenerator : BaseGenerator
    {
        public double freq = 20;
        public double phi = 0;
        public double A = 1;

        public RectGenerator()
        {
            Signal = GetRect;
        }

        double GetRect(int i)
        {
            return A * Math.Sign(Math.Sin(i / 500.0 * freq * Math.PI + phi));
        }
    }
}
