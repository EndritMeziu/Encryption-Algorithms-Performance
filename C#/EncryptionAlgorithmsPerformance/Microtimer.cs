using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EncryptionAlgorithmsPerformance
{
    public class MicroTimer : System.Diagnostics.Stopwatch
    {
        readonly double _microSecPerTick =
            1000000D / System.Diagnostics.Stopwatch.Frequency;

        public MicroTimer()
        {
            if (!System.Diagnostics.Stopwatch.IsHighResolution)
            {
                throw new Exception("On this system the high-resolution " +
                                    "performance counter is not available");
            }
        }

        public long ElapsedMicroseconds
        {
            get
            {
                return (long)(ElapsedTicks * _microSecPerTick);
            }
        }
    }
}
