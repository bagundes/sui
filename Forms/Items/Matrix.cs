using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SAPbouiCOM;

namespace SUI.Forms.Items
{
    public static class Matrix
    {
        private static string LOG => "MATRIX";

        #region Add/Del rows
        public static void AddRowWithoutDSource(ref SAPbouiCOM.Matrix matrix, int qtty = 1)
        {
            try
            {
                matrix.AddRow(qtty);
                //matrix.FlushToDataSource();

                //matrix.AddRow(qtty);
                //matrix.LoadFromDataSource();

            }catch(Exception ex)
            {
                klib.Shell.WriteLine(R.Project.ID, LOG, ex);
                SUI.Forms.Notification.SendToBarMessage(true, $"Occur a internal error to add a new line. Matrix {matrix.Item.Description}");
            }

        }

        public static void DelRowWithoutDSource(ref SAPbouiCOM.Matrix matrix)
        {
            try
            {
                var lineSelected = matrix.GetCellFocus().rowIndex;
                matrix.DeleteRow(lineSelected);
            }catch(Exception ex)
            {
                klib.Shell.WriteLine(R.Project.ID, LOG, ex);
                SUI.Forms.Notification.SendToBarMessage(true, $"Occur a internal error to delete the line. Matrix {matrix.Item.Description}");
            }
        }
        #endregion

        #region Export to
        public static FileInfo ExportToCsv(ref SAPbouiCOM.Matrix matrix)
        {
            throw new NotImplementedException();
        }
        #endregion

        public static void ReIndexLine(ref SAPbouiCOM.Form oForm, string dbdsource, string field = "LineId")
        {
            var size = oForm.DataSources.DBDataSources.Item(dbdsource).Size;
            for (int i = 0; i < size; i++)
                oForm.DataSources.DBDataSources.Item(dbdsource).SetValue(field, i, i.ToString());
        }

        public static klib.Dynamic GetValue(ref SAPbouiCOM.Matrix matrix, int row, dynamic col, string msgnull = null, string invalidValue = null)
        {
            var oItem = matrix.Columns.Item(col).Cells.Item(row);
            var value = String.Empty;

            switch (oItem.Type)
            {
                case SAPbouiCOM.BoFormItemTypes.it_EDIT:
                case SAPbouiCOM.BoFormItemTypes.it_EXTEDIT:
                    value = ((SAPbouiCOM.EditText)oItem.Specific).Value; break;
                case SAPbouiCOM.BoFormItemTypes.it_COMBO_BOX:
                    value = ((SAPbouiCOM.ComboBox)oItem.Specific).Selected.Value; break;
                case SAPbouiCOM.BoFormItemTypes.it_CHECK_BOX:
                    value = ((SAPbouiCOM.CheckBox)oItem.Specific).Checked.ToString(); break;
                default:
                    throw new SUIException(1, $"The item {oItem.UniqueID} not exist.");
            }

            if (!String.IsNullOrEmpty(invalidValue) && value.Trim().Equals(invalidValue, StringComparison.InvariantCultureIgnoreCase))
                throw new SUIException(11, oItem.UniqueID, oItem.Description);

            if (!String.IsNullOrEmpty(msgnull) && String.IsNullOrEmpty(value))
                throw new SUIException(10, oItem.UniqueID, oItem.Description);

            return klib.ValuesEx.To(value);
        }

        public static void SetValue(ref SAPbouiCOM.Matrix matrix, dynamic col, int row, object val)
        {
            var editable = matrix.Columns.Item(col).Editable;
            matrix.Columns.Item(col).Editable = true;

            try
            {
                val = klib.ValuesEx.To(val).ToString();
                matrix.SetCellWithoutValidation(row, col.ToString(), val.ToString());
            }
            finally
            {
                matrix.Columns.Item(col).Editable = editable;
            }
        }
    }
}
