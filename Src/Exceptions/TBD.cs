using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CP_Dev_Tools.Src.Exceptions
{
    public class TBD : Exception
    {
        public TBD() : base("TBD") { }
        public TBD(string msg) : base (msg) { }
        public TBD(string msg, Exception inner) : base(msg, inner) { }
    }
}
