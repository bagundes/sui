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
        private static string LOG => "DIALOG";
        private static klib.Dynamic value { get; set; }
        public static klib.Dynamic Value => value;
        public static klib.Dynamic OpenFile(string title, string filter)
        {
            value = klib.Dynamic.Empty;

            Thread t = new Thread(() =>
            {
                var openFileDialog = new OpenFileDialog();
                openFileDialog.Title = title;
                openFileDialog.Filter = filter;
                var dr = openFileDialog.ShowDialog(new Form());

                if (dr == DialogResult.OK)
                {
                    string fileName = openFileDialog.FileName;
                    value = new klib.Dynamic(fileName);
                }
            });          // Kick off a new thread
            t.IsBackground = true;
            t.SetApartmentState(ApartmentState.STA);
            t.Start();

            while (t.ThreadState == ThreadState.Background)
                Application.DoEvents();

            klib.Shell.WriteLine(R.Project.ID, LOG, $"SUI - Selected the file: {value}");

            return value;
        }
    }
}
