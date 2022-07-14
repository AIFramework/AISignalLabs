using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace AI.SignalLab.Generators
{
    /// <summary>
    /// Базовый генератор цифрового сигнала
    /// </summary>
    public class BaseGenerator : IDisposable
    {
        public int PeriodMS { get; set; } = 20;
        public Func<int, double> Signal = (i) => i / 1000.0;
        
        int _counter = 0;
        Thread _thread;

        public void SetPosition(int start) 
        {
            _counter = start;
        }

        /// <summary>
        /// Запуск генератора
        /// </summary>
        public void Start() 
        {
            SignalSemple += BaseGenerator_SignalSemple;
            _thread = new Thread(CalcSignal);
            _thread.Start();
        }


        public void Reset() 
        {
            SetPosition(0);
        }


        public void CalcSignal() 
        {
            while (true)
            {
                double s = Signal(_counter++);
                SignalSemple(s);
                Thread.Sleep(PeriodMS);
            }
        }

       

        public event Action<double> SignalSemple;


        private void BaseGenerator_SignalSemple(double obj) { }


        public void Dispose()
        {
            _thread.Abort();
        }
    }
}
