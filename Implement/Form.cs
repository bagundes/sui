using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SUI.implement
{

    public interface IForm
    {

        /// <summary>
        /// Initialize the items
        /// </summary>
        void InitComponents();
        /// <summary>
        /// Loading the parameters and triggers
        /// </summary>
        void Loading(SAPbouiCOM.Form form = null);

        /// <summary>
        /// Action when right click event is executed
        /// </summary>
        /// <param name="oForm"></param>
        /// <param name="menuInfo"></param>
        void UIRightClickEvent(SAPbouiCOM.Form oForm, SAPbouiCOM.ContextMenuInfo menuInfo);
        /// <summary>
        /// Action to get the click menu is executed.
        /// </summary>
        /// <param name="oForm"></param>
        /// <param name="uid"></param>
        /// <param name="before"></param>
        void UIMenuEvent(SAPbouiCOM.Form oForm, SUI.E.Menus.UID uid, bool before);
    }

    public abstract class Form
    {
        public static string LOG => "Form";
        protected SAPbouiCOM.Form oForm;

        /// <summary>
        /// Load de srf form and load the oForm properties.
        /// </summary>
        /// <param name="srf"></param>
        protected void Load(FileInfo srf)
        {
            if (oForm != null)
                return;

            srf.Refresh();
            if (!srf.Exists)
                throw new SUIException(3, srf.Name);

            oForm = SUI.Forms.Form.Load(srf);
            //using (var reader = System.IO.File.OpenText(srf.FullName))
            //    oForm = SUI.Forms.Form.Load(reader);

            klib.Shell.WriteLine(R.Project.ID, LOG, $"Loaded the {oForm.Title} form");
        }

        protected void Load(SAPbouiCOM.Form oForm)
        {
            this.oForm = oForm;
        }
    }
}
