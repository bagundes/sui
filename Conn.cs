using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SUI
{
    public class Conn
    {
        internal static SAPbouiCOM.Application UI;
        public static void Connect(string commandLineArg = null, int appId = -1)
        {
            klib.Shell.WriteLine("Try to connect SAP UI");
            if (String.IsNullOrEmpty(commandLineArg))
                commandLineArg = R.CommandLineArg;

            var sga = new SAPbouiCOM.SboGuiApi();
            sga.Connect(commandLineArg);
            UI = sga.GetApplication(appId);

            klib.Shell.WriteLine("SAP UI connected");
            var init = new Init();
            init.Construct();
        }

        public static dynamic GetDI()
        {
            return UI.Company.GetDICompany();
        }
    }
}
