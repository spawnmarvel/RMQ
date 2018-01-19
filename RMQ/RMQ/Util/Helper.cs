using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace RMQ.Util
{
    class Helper
    {

        public static void followTextBoxLog(RichTextBox loggerForm, string action)
        {
            string da = DateTime.Now.ToString();
            loggerForm.AppendText("Log:" + da + ": " + action);
            loggerForm.AppendText(Environment.NewLine);
            loggerForm.ScrollToEnd();

        }


    }
}
