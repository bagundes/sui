using SAPbouiCOM;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SUI.Forms
{
    public class Form
    {
        static int delay = 0;

        public SAPbouiCOM.Form FormActive => Conn.UI.Forms.ActiveForm;

  
        #region getters
        public static klib.Values GetValue(ref SAPbouiCOM.Item oItem, string msgnull = null, string invalidValue = null)
        {
            var value = String.Empty;
            var enabled = oItem.Enabled;
            // @BFAGUNDES - This proper not work when is SAP field - Project audit
            // oItem.Enabled = true;

            try
            {
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
                        throw new LException(1, $"The item {oItem.UniqueID} not exist.");
                }

                if (!String.IsNullOrEmpty(invalidValue) && value.Trim().Equals(invalidValue, StringComparison.InvariantCultureIgnoreCase))
                    throw new LException(11, oItem.UniqueID, oItem.Description);

                if (!String.IsNullOrEmpty(msgnull) && String.IsNullOrEmpty(value))
                    throw new LException(10, oItem.UniqueID, oItem.Description);

                return klib.ValuesEx.To(value);
            }

            finally

            {
                // oItem.Enabled = enabled;
            }

        }

        public static klib.Values GetValue(ref SAPbouiCOM.Form oForm, string uid)
        {
            var oItem = oForm.Items.Item(uid);
            return GetValue(ref oItem);
        }

        public static klib.Values GetValue(SAPbouiCOM.Column oColumn, object line, string msgnull = null)
        {
            var value = String.Empty;
            var cell = oColumn.Cells.Item(line);

            try
            {
                switch (oColumn.Type)
                {
                    case SAPbouiCOM.BoFormItemTypes.it_EDIT:
                    case SAPbouiCOM.BoFormItemTypes.it_EXTEDIT:
                        value = ((SAPbouiCOM.EditText)cell.Specific).Value; break;
                    case SAPbouiCOM.BoFormItemTypes.it_COMBO_BOX:
                        value = ((SAPbouiCOM.ComboBox)cell.Specific).Selected.Value; break;
                    case SAPbouiCOM.BoFormItemTypes.it_CHECK_BOX:
                        value = ((SAPbouiCOM.CheckBox)cell.Specific).Checked.ToString(); break;
                    default:
                        throw new LException(1, $"The column {oColumn.UniqueID} not exist.");
                }

                if (!String.IsNullOrEmpty(msgnull) && String.IsNullOrEmpty(value))
                    throw new LException(10, oColumn.UniqueID, oColumn.Description);

                return klib.ValuesEx.To(value);
            }
            finally
            {
            }
        }

        public static klib.Values GetValue(ref SAPbouiCOM.Form oForm, string table, string column, int line = 0)
        {
            return klib.ValuesEx.To(oForm.DataSources.DBDataSources.Item(table).GetValue(column, line));
        }

        public static dynamic GetSpecific(ref SAPbouiCOM.Form oForm, object index)
        {
            return oForm.Items.Item(index).Specific;
        }
        /// <summary>
        /// Load UDSource data
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="Form"></param>
        /// <returns></returns>
        //public static DIHelper.Models.UDSources GetUDValues<T>(ref SAPbouiCOM.Form Form) where T : DIHelper.Models.UDSources, new()
        //{
        //    //var values = new DIHelper.Models.UDSources() as T;
        //    var values = new T();


        //    var propertiesInfo = typeof(T).GetProperties();

        //    foreach (var property in propertiesInfo)
        //    {
        //        try
        //        {
        //            var value = SF.UIHelper.Form.GetUDSValue(ref Form, property.Name);

        //            Type type = values.GetType();
        //            PropertyInfo pi = type.GetProperty(property.Name);
        //            pi.SetValue(values, value.Dynamic, null);
        //        }
        //        catch
        //        {

        //        }
        //    }

        //    return values;

        //}

        public static klib.Values GetUDSValue(ref SAPbouiCOM.Form Form, string uds)
        {
            return klib.ValuesEx.To(Form.DataSources.UserDataSources.Item(uds).Value);
        }

        //public static klib.Values GetKeyValue(ref SAPbouiCOM.Form Form)
        //{

        //    // TODO : Chenge to read in xml
        //    //var xmlstring = Form.BusinessObject.Key;
        //    //var xml = new klib.xml.Read(xmlstring);



        //    var repository = new Repositories.SAPTableRepository();
        //    var sapTables = repository.GetByForm(Form);

        //    if (sapTables._IsNull || String.IsNullOrEmpty(sapTables.LogTable))
        //        return Values.Null;
        //    else
        //        return GetValue(ref Form, sapTables.Table, sapTables.PrKey);//SF.UIHelper.Form.GetValue(ref Form, sapTables.FormFiKey);
        //}

        /// <summary>
        /// The method will try to get the key in the form.
        /// Case is not possible, it will return null;
        /// </summary>
        /// <param name="FormUID"></param>
        /// <returns></returns>
        //public static string TryGetKeyValue(string FormUID)
        //{
        //    try
        //    {
        //        var oForm = SF.Conn.UI.Forms.GetForm(FormUID, 0);
        //        return GetKeyValue(ref oForm).ToString();
        //    }
        //    catch
        //    {
        //        return null;
        //    }

        //}
        #endregion
        #region setters
        //public static DIHelper.Models.UDSources SetUDValues<T>(ref SAPbouiCOM.Form Form, T values) where T : DIHelper.Models.UDSources
        //{

        //    var propertiesInfo = typeof(T).GetProperties();

        //    foreach (var property in propertiesInfo)
        //    {
        //        try
        //        {
        //            var value = SF.UIHelper.Form.GetUDSValue(ref Form, property.Name);

        //            Type type = values.GetType();
        //            PropertyInfo pi = type.GetProperty(property.Name);
        //            SF.UIHelper.Form.SetDSValue(ref Form, property.Name, pi.GetValue(values));
        //        }
        //        catch
        //        {

        //        }
        //    }

        //    return values;

        //}

        public static void SetValue(ref SAPbouiCOM.Form Form, string uid, object value)
        {
            Form.Freeze(true);

            try
            {
                var oItem = Form.Items.Item(uid);

                bool enabled = Form.Items.Item(uid).Enabled;
                value = String.IsNullOrEmpty(value.ToString()) ? "" : value;

                oItem.Enabled = true;

                switch (oItem.Type)
                {
                    case SAPbouiCOM.BoFormItemTypes.it_EDIT:
                    case SAPbouiCOM.BoFormItemTypes.it_EXTEDIT:
                        ((SAPbouiCOM.EditText)oItem.Specific).Value = value.ToString(); break;
                    case SAPbouiCOM.BoFormItemTypes.it_COMBO_BOX:
                        ((SAPbouiCOM.ComboBox)oItem.Specific).Select(value.ToString(), SAPbouiCOM.BoSearchKey.psk_ByValue); break;
                    default:
                        throw new LException(1, $"The item {oItem.UniqueID} not exist."); break;

                }

                oItem.Enabled = enabled;

            }
            finally
            {

                Form.Freeze(false);
            }
        }

        public static void SetValue(ref SAPbouiCOM.Column oColumn, object line, dynamic value)
        {
            var cell = oColumn.Cells.Item(line);

            try
            {
                switch (oColumn.Type)
                {
                    case SAPbouiCOM.BoFormItemTypes.it_EDIT:
                    case SAPbouiCOM.BoFormItemTypes.it_EXTEDIT:
                        ((SAPbouiCOM.EditText)cell.Specific).Value = value; break;
                    case SAPbouiCOM.BoFormItemTypes.it_COMBO_BOX:
                        ((SAPbouiCOM.ComboBox)cell.Specific).Select(value.ToString(), SAPbouiCOM.BoSearchKey.psk_ByValue); break;
                    case SAPbouiCOM.BoFormItemTypes.it_CHECK_BOX:
                        ((SAPbouiCOM.CheckBox)cell.Specific).Checked = value; break;
                    default:
                        throw new LException(1, $"The column {oColumn.UniqueID} not exist.");
                }
            }
            finally
            {
            }
        }

        /// <summary>
        /// Set value in field
        /// </summary>
        /// <param name="oForm"></param>
        /// <param name="table">If the table is user table, do you need add @</param>
        /// <param name="column"></param>
        /// <param name="line"></param>
        /// <param name="val"></param>
        public static void SetValue(ref SAPbouiCOM.Form oForm, string table, string column, int line, Object val)
        {
            val = klib.ValuesEx.To(val).ToString();
            try
            {
                oForm.DataSources.DBDataSources.Item(table).SetValue(column, line, val.ToString());

                return;
            }
            catch (Exception ex)
            {
#if DEBUG
                System.Windows.Forms.MessageBox.Show(ex.Message);
#endif 
            }

            try
            {
                oForm.DataSources.DataTables.Item(table).SetValue(column, line, val.ToString());
                return;
            }
            catch (Exception ex)
            {
#if DEBUG
                System.Windows.Forms.MessageBox.Show(ex.Message);
#endif
                throw new LException(12, table, column, line);
            }
        }

        public static void SetDSValue(ref SAPbouiCOM.Form oForm, string userDS, Object value)
        {
            oForm.DataSources.UserDataSources.Item(userDS).ValueEx = value.ToString();
        }
        #endregion
        #region clicks
        public static void Click(ref SAPbouiCOM.Form Form, string item)
        {
            var Item = Form.Items.Item(item);

            switch (Item.Type)
            {
                case SAPbouiCOM.BoFormItemTypes.it_BUTTON:
                    Item.Click(); break;
                default:
                    throw new LException(1, $"The click item {Item.UniqueID} not implemented."); break;

            }
        }
        //public static void Click(G.Menus.MenuUID.Data data, BoCellClickType type)
        //{
        //    Conn.UI.ActivateMenuItem(((int)data).ToString());
        //}

        //public static void Click_RefreshRecord()
        //{
        //    System.Threading.Thread.Sleep(delay);
        //    Click(G.Menus.MenuUID.Data.RefreshRecord, BoCellClickType.ct_Regular);
        //    delay = 0;
        //}

        /// <summary>
        /// Test - Click with delay
        /// </summary>
        /// <param name="delay"></param>
        //public static void Click_RefreshRecord(int delay)
        //{
        //    Form.delay = delay;
        //    var t = new Thread(new ThreadStart(Click_RefreshRecord));
        //    t.Start();
        //}


        #endregion
        #region open



        public static SAPbouiCOM.Form Load(StreamReader strXml)
        {

            var xml = new System.Xml.XmlDocument();
            xml.Load(strXml);
            var strxml = (xml.DocumentElement).InnerXml.ToString();

            var oFormCreationParams = (SAPbouiCOM.FormCreationParams)Conn.UI.CreateObject(SAPbouiCOM.BoCreatableObjectType.cot_FormCreationParams);
            oFormCreationParams.XmlData = strxml;
            var oForm = Conn.UI.Forms.AddEx(oFormCreationParams);
            oForm.Visible = true;
            return oForm;
        }

        #endregion
        #region layout
        /// <summary>
        /// Add number of the instancie in the title.
        /// </summary>
        /// <param name="Form"></param>
        /// <returns>Number of instance</returns>
        //public static int AddNumberOfInstanceInTitle(ref SAPbouiCOM.Form Form)
        //{
        //    var title = Form.Title;
        //    var index = Form.Title.IndexOf(':');
        //    var instance = SF.DIHelper.DataInfo.GetLastInstanceNumber(ref Form);

        //    if (instance > 0)
        //    {
        //        if (index > 0)
        //            Form.Title = title.Substring(0, index);

        //        Form.Title = title + $" : Instance #{SF.DIHelper.DataInfo.GetLastInstanceNumber(ref Form)}";
        //    }
        //    return instance;
        //}
        #endregion
        #region properties


        /// <summary>
        /// Get datasource name in the xml attribute.
        /// </summary>
        /// <param name="srf"></param>
        /// <returns></returns>
        private static string GetDataSource(StreamReader srf)
        {
            throw new NotImplementedException("I will create to get Datasource by xml form");
            //var xml = new klib.xml.Read(srf.ToString());
            //var el = xml.GetAttributeValue
        }

        /// <summary>
        /// Returns the Object Type in the form.
        /// </summary>
        /// <param name="srf">file xml</param>
        /// <returns></returns>
        //public static string GetObjectType(StreamReader srf)
        //{
        //    var xml = new klib.xml.Read(srf.ToString());
        //    var foo = xml.GetAttributeValues("action", "ObjectType");
        //    if (foo != null && foo.Count > 0)
        //        return foo[0].ToString();
        //    else
        //        return null;
        //}
        #endregion
    }
}
