using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SUI
{
    internal class LException : klib.implement.LException
    {
        public LException(string message) : base(message)
        {
        }

        public LException(Exception ex) : base(ex)
        {
        }

        public LException(int code, params object[] values) : base(code, values)
        {
        }
    }
}
