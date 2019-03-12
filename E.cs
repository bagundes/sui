using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SUI
{
    public class E
    {
        public enum Events
        {
            None = 0,
            Items = 1,
            Forms = 2,
            Menus = 3,
            RightClick = 4,
            App = 5,
        }

        public static class Menus
        {
            public static UID GetMenu(string menuIUD)
            {
                var uid = klib.ValuesEx.To(menuIUD).ToInt();
                try
                {
                    return (UID)uid;
                } catch
                {
                    return UID.None;
                }
            }

            public enum UID
            {
                Data_AddRow = 1292,
                Data_DelRow = 1293,

                None = 0,
            }
        }
            
    }
}
