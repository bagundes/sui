using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SUI.Forms
{
    public class Menu
    {

        public static void Add(string fatherUID, string menuUID, string name, SAPbouiCOM.BoMenuType oType, int position, System.IO.FileInfo image = null)
        {
            var oMenus = Conn.UI.Menus;
            var oCreationPackage = Conn.UI.CreateObject(SAPbouiCOM.BoCreatableObjectType.cot_MenuCreationParams) as SAPbouiCOM.MenuCreationParams;

            // Remove o menu atual
            if (oMenus.Exists(menuUID))
                oMenus.RemoveEx(menuUID);

            // Obtem o menu pai (caso exista)
            if (!String.IsNullOrEmpty(fatherUID))
                if (!oMenus.Exists(fatherUID))
                    throw new LException(1, "Menu father doesn't exists");
                else
                    oMenus = Conn.UI.Menus.Item(fatherUID).SubMenus;


            if (position < 0)
                position = oMenus.Count;

            oCreationPackage.Type = oType;
            oCreationPackage.UniqueID = menuUID;
            oCreationPackage.String = name;
            oCreationPackage.Enabled = true;
            oCreationPackage.Position = position;

            // TODO Validate
            // Supported image formats are BMP and JPG. Image size should be 16 - by - 16 pixels for the menu bar, and 26 - by - 18 pixels for the main menu.
            //oCreationPackage.Image = MenuImagePath;
            if (image != null)
                oCreationPackage.Image = image.FullName;

            oMenus.AddEx(oCreationPackage);
        }

        public static void Remove(string menuUID)
        {
            var oMenus = Conn.UI.Menus;
            var oCreationPackage = Conn.UI.CreateObject(SAPbouiCOM.BoCreatableObjectType.cot_MenuCreationParams) as SAPbouiCOM.MenuCreationParams;

            // Remove o menu atual
            if (oMenus.Exists(menuUID))
                oMenus.RemoveEx(menuUID);
        }

        public static void Click(string menuUID)
        {
            Conn.UI.ActivateMenuItem(menuUID);
        }

        public static void EnableAddRemoveRows(ref SAPbouiCOM.Form oForm)
        {
            oForm.EnableMenu("1292", true);
            oForm.EnableMenu("1293", true);
        }
    }

    //public class MenuEX
    //{
        /// <summary>
        /// Add Form in menu.
        /// </summary>
        /// <param name="fatherID"></param>
        /// <param name="tyForm"></param>
        /// <returns>Form MenuID</returns>
        //public static string AddForm(string fatherID, Type tyForm)
        //{
        //    var structForm = new SF.UIHelper.Struct.FormParams(tyForm);

        //    System.IO.FileInfo imageInfo;

        //    if (!System.IO.File.Exists(structForm.Icon))
        //        imageInfo = klib.tools.Content.CreateFile(Content.images.menu_icon_bmp, System.Reflection.Assembly.GetExecutingAssembly());
        //    else
        //        imageInfo = new System.IO.FileInfo(structForm.Icon);

        //    Menu.Add(fatherID, structForm.MenuId, structForm.Name, SAPbouiCOM.BoMenuType.mt_STRING, -1, imageInfo);

        //    return structForm.MenuId;
        //}
    //}
}
