using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SUI.Forms.Items
{
    public static class EditText
    {
        public static string LOG => "EDITEX";
        /// <summary>
        /// Get the value selected
        /// </summary>
        /// <param name="editText">The EditText item</param>
        /// <param name="error_msg">Add a personal message in the exception</param>        
        /// <returns>Values object</returns>
        public static klib.Dynamic GetValue(ref SAPbouiCOM.EditText editText, string error_msg = null)
        {

            var val = new klib.Dynamic(editText.Value);

            if (val.IsEmpty)
                throw new SUIException(4, editText.Item.Description, "EditText", error_msg);

            return val;
        }
    }
}
