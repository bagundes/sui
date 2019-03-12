using SAPbouiCOM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SUI
{
    public class Init : klib.Implement.IInit
    {
        private static string LOG => "INIT";
        public static void Start()
        {
            klib.Shell.WriteLine(R.Project.ID,LOG, $"SUI - Loading the module");
            var init = new Init();
            init.Construct();
            init.Configure();
        }

        public void Configure()
        {
            //P.CreateParameters();
            //SUI.Conn.UI.MenuEvent += new _IApplicationEvents_MenuEventEventHandler(UIMenuEvent1);
            SUI.Conn.UI.AppEvent += new SAPbouiCOM._IApplicationEvents_AppEventEventHandler(UIAppEvent);
        }


        public void Construct()
        {
            #region Menus
            Forms.Menu.Add("43520", R.Menus.CompanyMenu, klib.R.Company.AliasName, SAPbouiCOM.BoMenuType.mt_POPUP, -1, R.GetFile("teamsoft1.bmp"));
            #endregion
        }

        public void Destruct()
        {
            throw new NotImplementedException();
        }

        public void Populate()
        {
            throw new NotImplementedException();
        }

        #region Events
        private static void UIMenuEvent1(ref MenuEvent pVal, out bool BubbleEvent)
        {
            BubbleEvent = true;

        }

        private void UIAppEvent(BoAppEventTypes EventType)
        {
            try
            {
                switch (EventType)

                {
                    case SAPbouiCOM.BoAppEventTypes.aet_ShutDown:
                        System.Environment.Exit(0);
                        break;

                    case SAPbouiCOM.BoAppEventTypes.aet_CompanyChanged:
                        System.Environment.Exit(0);
                        break;

                    case SAPbouiCOM.BoAppEventTypes.aet_ServerTerminition:
                        System.Environment.Exit(0);
                        break;

                }
            }
            catch (Exception ex)
            {
                klib.Shell.WriteLine(R.Project.ID, LOG, ex);
            }
        }
        #endregion
    }
}
