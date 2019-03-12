using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SUI
{
    public class SUIException : klib.Implement.InternalException
    {
        public override string Message => MsgDecode(R.Project.LocationResx);
        public SUIException(string message) : base(message)
        {
        }

        public SUIException(Exception ex) : base(ex)
        {
        }

        public SUIException(int code, params object[] values) : base(code, values)
        {
            try
            {
                var a = R.Project.LocationResx;
                var b = a.GetString("L00004_3");
            }catch(Exception ex)
            {
                var c = ex.Message;
            }
        }
    }
}
