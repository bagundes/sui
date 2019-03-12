using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SUI.Forms.Items
{
    public static class ComboBox
    {
        public static string LOG => "COMBBX";
        public static void Load(ref SAPbouiCOM.ComboBox comboBox, Dictionary<string, string> valValues, bool clear = true)
        {
            if(clear)
                for (int i = comboBox.ValidValues.Count; i < 0; i--)
                    comboBox.ValidValues.Remove(i, SAPbouiCOM.BoSearchKey.psk_Index);

            foreach (var value in valValues)
                comboBox.ValidValues.Add(value.Key, value.Value);
        }

        /// <summary>
        /// Get the value selected
        /// </summary>
        /// <param name="comboBox">The Combobox item</param>
        /// <param name="val_invalid">Create message error if the value is same the selected</param>
        /// <param name="error_msg">Add a personal message in the exception</param>        
        /// <returns>Values object. The properties Name is a description</returns>
        public static klib.Dynamic GetSelected(ref SAPbouiCOM.ComboBox comboBox, string val_invalid = null, string error_msg = null)
        {
            try
            {

                var val = SUI.ValueEx.ValidValue(comboBox.Selected);

                if (!String.IsNullOrEmpty(val_invalid) && val.Value.ToString() == val_invalid)
                    throw new SUIException(4, comboBox.Item.Description, "ComboBox", error_msg);

                if (!String.IsNullOrEmpty(error_msg) && val.IsEmpty)
                    throw new SUIException(4, comboBox.Item.Description, "ComboBox", error_msg);

                return val;

            }
            catch(Exception ex)
            {
                klib.Shell.WriteLine(R.Project.ID, LOG, ex);
                throw new SUIException(4, comboBox.Item.Description, "ComboBox", error_msg);
            }
        }
    }
}
