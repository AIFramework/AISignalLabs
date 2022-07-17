using AI.DataStructs.Algebraic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace AI.SignalLab.Modulation.Modulation
{
    public class ModulationBitsTools
    {
        /// <summary>
        /// Перевод битов в модулирующий сигнал
        /// </summary>
        /// <param name="bools">Биты</param>
        /// <param name="bitDuration">Длительность бита</param>
        /// <param name="lowSignal">Нижний уровень</param>
        /// <param name="hSignal">Верхний уровень</param>
        /// <param name="sr">Частота дискретизации</param>
        public static Vector Bits2Signal(bool[] bools, double bitDuration = 3e-3, double lowSignal = 0, double hSignal = 1, double sr = 8e+3) 
        {
            int nSimb = (int)(bitDuration * sr); // Отсчетов на символ
            Vector bitsSignal = new Vector(nSimb*bools.Length);
            
            for (int i = 0; i < bools.Length; i++)
            {
                if (bools[i])
                {
                    // Установка высокого уровня
                    int end = (i + 1) * nSimb;
                    for (int j = i*nSimb; j < end; j++)
                        bitsSignal[j] = hSignal;
                }
                else 
                {
                    // Установка низкого уровня
                    int end = (i + 1) * nSimb;
                    for (int j = i * nSimb; j < end; j++)
                        bitsSignal[j] = lowSignal;
                }

            }

            return bitsSignal;
        }

        /// <summary>
        /// Перевод цифрового объекта в модулирующий сигнал
        /// </summary>
        /// <param name="bools">Биты</param>
        /// <param name="bitDuration">Длительность бита</param>
        /// <param name="lowSignal">Нижний уровень</param>
        /// <param name="hSignal">Верхний уровень</param>
        /// <param name="sr">Частота дискретизации</param>
        public static Vector Object2Signal(object obj, double bitDuration = 3e-3, double lowSignal = 0, double hSignal = 1, double sr = 8e+3)
        {
            BitArray bits = new BitArray(Obj2Bytes(obj));
            bool[] bools = new bool[bits.Length];
            
            for (int i = 0; i < bits.Length; i++)
                bools[i] = bits[i];

            return Bits2Signal(bools, bitDuration, lowSignal, hSignal, sr);
        }

        /// <summary>
        /// Конвертирование из obj в массив байт
        /// URL: https://stackoverflow.com/questions/4865104/convert-any-object-to-a-byte
        /// </summary>
        static byte[] Obj2Bytes(object obj)
        {
            if (obj == null)
                return null;
            BinaryFormatter bf = new BinaryFormatter();
            using (MemoryStream ms = new MemoryStream())
            {
                bf.Serialize(ms, obj);
                return ms.ToArray();
            }
        }
    }
}
