using SAPbouiCOM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SUI
{
    public class Conn
    {
        private const string LOG = "CONN";

        public static Application UI;

        #region Connect
        public static void Connect(string commandLineArg = null, int appId = -1)
        {
            klib.Shell.WriteLine(R.Project.ID, LOG, $"SUI - Connecting ...");
            if (String.IsNullOrEmpty(commandLineArg))
                commandLineArg = R.CommandLineArg;

            var sga = new SAPbouiCOM.SboGuiApi();
            sga.Connect(commandLineArg);
            UI = sga.GetApplication(appId);

            klib.Shell.WriteLine(R.Project.ID, LOG, $"SUI - Connected");            
        }

        public static dynamic GetDI()
        {
            klib.Shell.WriteLine(R.Project.ID, LOG, "SUI - Getting DI");
            return UI.Company.GetDICompany();
        }
        #endregion
    }
}
