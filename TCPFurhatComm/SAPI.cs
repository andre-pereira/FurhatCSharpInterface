using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TCPFurhatComm
{
    public static class SAPI
    {
        public enum BreakValues
        {
            none, xweak, weak, medium, strong, xstrong
        }

        public enum RateValues
        {
            xslow, slow, medium, fast, xfast, default_
        }

        public enum PitchValues
        {
            xlow, low, medium, high, xhigh, default_
        }

        public enum VolumeValues
        {
            silent, xsoft, soft, medium, loud, xloud, default_
        }

        public enum EmphasisValues
        {
            reduced, moderate, strong, none
        }
    }
}
