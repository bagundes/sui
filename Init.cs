using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SUI
{
    internal class Init : klib.implement.IInit
    {
        public void Configure()
        {
            throw new NotImplementedException();
        }

        public void Construct()
        {


            #region Menus
            Forms.Menu.Add("43520", klib.R.Company.AliasName, klib.R.Company.AliasName, SAPbouiCOM.BoMenuType.mt_POPUP, -1);
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
    }
}
