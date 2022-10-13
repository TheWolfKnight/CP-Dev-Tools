using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CP_Dev_Tools.Src.Exceptions
{
    public class UnrechableCodeException : Exception
    {

        public UnrechableCodeException() : base( "Unrechable code" ) { }
        public UnrechableCodeException( string msg ) : base( msg ) { }
        public UnrechableCodeException( string msg, Exception inner ) : base( msg, inner ) { }

    }
}
