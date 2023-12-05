using AI.ML.Clustering;
using System;
using System.Collections.Generic;
using System.Text;

namespace AI.SignalLab.Modulation.ModulationTypeDetector
{
    [Serializable]
    public class DigitalModulationDetector
    {
        int _maxBits;

        public DigitalModulationDetector(int maxBits) 
        {
            _maxBits= maxBits;
        }
    }
}
