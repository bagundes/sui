using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SUI
{
    public static class ValueEx
    {
        public static klib.Dynamic ValidValue(SAPbouiCOM.ValidValue validValue)
        {
            return new klib.Dynamic(validValue.Value, validValue.Description);
        }
    }
}
