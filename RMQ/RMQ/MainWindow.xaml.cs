using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
//project
using RMQ.Util;

namespace RMQ
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        public MainWindow()
        {
            InitializeComponent();
        }

        private void btnSendPacket_Click(object sender, RoutedEventArgs e)
        {
            string rv = textBoxGetPacket.Text;
            if (rv.Length <= 0)
            {
                rv = "No input";
            }
            Helper.followTextBoxLog(richTextBoxLog, rv);
        }

        private void richTextBoxRec_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void btnRecievePacket_Click(object sender, RoutedEventArgs e)
        {
            Helper.followTextBoxLog(richTextBoxRec, "got some");
            Helper.followTextBoxLog(richTextBoxLog, "got some");
        }
    }
}
