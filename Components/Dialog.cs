using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SUI.Components
{
    public static class Dialog
    {
        private static klib.Values value { get; set; }
        public static klib.Values Value => value;
        public static klib.Values OpenFile(string title, string filter)
        {
            value = klib.ValuesEx.Empty;

            Thread t = new Thread(() =>
            {
                var openFileDialog = new OpenFileDialog();
                openFileDialog.Title = title;
                openFileDialog.Filter = filter;
                var dr = openFileDialog.ShowDialog(new Form());

                if (dr == DialogResult.OK)
                {
                    string fileName = openFileDialog.FileName;
                    value = new klib.Values(fileName);
                }
            });          // Kick off a new thread
            t.IsBackground = true;
            t.SetApartmentState(ApartmentState.STA);
            t.Start();

            while (t.ThreadState == ThreadState.Background)
                Application.DoEvents();
            

            return value;
        }
    }
}
