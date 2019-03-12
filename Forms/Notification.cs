using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SUI.Forms
{
    public static class Notification
    {
        private static string LOG => "NOTFIC";
        public static void MessageBox(string message, params object[] values)
        {
            klib.Shell.WriteLine(R.Project.ID, LOG, string.Format(message, values));
            Conn.UI.MessageBox(string.Format(message, values));
        }

        public static bool YesOrNo(bool defNo, string message, params object[] values)
        {
            var def = defNo ? 2 : 1;
            return Conn.UI.MessageBox(string.Format(message, values), def, "Yes", "No") == 1;
        }

        public static void SendToBarMessage(bool error, string message, params object[] values)
        {
            Conn.UI.SetStatusBarMessage(String.Format(message, values), SAPbouiCOM.BoMessageTime.bmt_Short, error);
        }



        public class Progress : IDisposable
        {
            private SAPbouiCOM.ProgressBar progress;
            public Progress(int max, string text)
            {
                progress = Conn.UI.StatusBar.CreateProgressBar(text, max, false);     
            }

            public bool Add(int value, string text)
            {
                if((progress.Value + value) < 0)
                    return Define(0, text);                   

                var full = progress.Value + value > progress.Maximum;

                if (full)
                    progress.Maximum = progress.Value + value + 1;

                progress.Value += value;
                progress.Text = Text(text);

                return !full;
            }

            public bool Define(int value, string text)
            {
                var full = value > progress.Maximum;

                if (full)
                    progress.Maximum = value + 1;

                progress.Value = value;
                progress.Text = Text(text);

                return !full;
            }
        
            public void Stop()
            {
                progress.Stop();
            }
            public void Dispose()
            {
                Stop();
                progress = null;
            }

            private string Text(string text)
            {
                return $"{progress.Value}/{progress.Maximum} {text}";
            }
        }

        public static void MessageBar(string message, bool error)
        {
            Conn.UI.SetStatusBarMessage(message, SAPbouiCOM.BoMessageTime.bmt_Short, error);
        }
    }
}
