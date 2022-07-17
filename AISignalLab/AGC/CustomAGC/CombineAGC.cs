﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AI.SignalLab.AGC.CustomAGC
{
    /// <summary>
    /// Комбинированная АРУ, с целю сгладить переходной процесс
    /// </summary>
    public class CombineAGC : IAGC
    {
        public double TresholdAGC 
        {
            get 
            {
                return AGC1.TresholdAGC;
            }
            set 
            {
                AGC1.TresholdAGC = value;
                AGC2.TresholdAGC = value;
            }
        }
        public double TresholdFilter
        {
            get
            {
                return AGC1.TresholdFilter;
            }
            set
            {
                AGC1.TresholdFilter = value;
                AGC2.TresholdFilter = value;
            }
        }

        public IAGC AGC1{ get; set; }
        public IAGC AGC2 { get; set; }

        public CombineAGC()
        {
            AGC1 = new DirectAGC();
            AGC2 = new LogAGC();
            TresholdAGC = 4;
        }

        public CombineAGC(IAGC agc1, IAGC agc2, double tresholdAGC = 4)
        {
            AGC1 = agc1;
            AGC2 = agc2;
            TresholdAGC = tresholdAGC;
        }

        public double Calculate(double value)
        {
            double out1 = AGC1.Calculate(value);
            double out2 = AGC2.Calculate(value);
            return Math.Abs(out1)> Math.Abs(out2)? out2:out1;
        }
    }
}
