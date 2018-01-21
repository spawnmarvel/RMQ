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
using RMQ.Controller;

namespace RMQ
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private static readonly log4net.ILog logger = log4net.LogManager.GetLogger(typeof(MainWindow));
        private MqHandler mqHandler;

        public MainWindow()
        {
            InitializeComponent();
            logger.Info("\n");
            logger.Info("*****************************************");
            logger.Info("************************************");
            logger.Info("*********************************");
            logger.Info("RMQ App started");
            logger.Info("Version 1.1");
        }

        private void btnSendPacket_Click(object sender, RoutedEventArgs e)
        {
            string res = textBoxGetPacket.Text;
            string mqRes = "";
            if (res.Length <= 0)
            {
                res = "No input";
            }
            else
            {
               mqRes =  mqHandler.publishToDeafult(res);
                Helper.followTextBoxLog(richTextBoxLog, res);
                logger.Info("Sent packet " + res);
            }
            Helper.followTextBoxLog(richTextBoxLog, mqRes);
        }



        private void btnRecievePacket_Click(object sender, RoutedEventArgs e)
        {
            Helper.followTextBoxLog(richTextBoxRec, "got some");
            Helper.followTextBoxLog(richTextBoxLog, "got some");
            logger.Info("Recieve packet ");
        }


        private void richTextBoxRec_TextChanged(object sender, TextChangedEventArgs e)
        {

        }



        private void buttonUseDefaultQueue_Click(object sender, RoutedEventArgs e)
        {
            mqHandler = new MqHandler();
            bool status = mqHandler.setUpProducerDefault();
            Helper.followTextBoxLog(richTextBoxLog, "Connect to queue: default " + status);

        }

        private void btnMakeProducer_Click(object sender, RoutedEventArgs e)
        {

        }

        private void buttonMakeNewQueue_Click(object sender, RoutedEventArgs e)
        {
            string queue = textBoxMakeQueue.Text;
            string rv = "New queue ";
            if (queue.Length <= 3)
            {
                rv = "Name must be > 2 chars";
            }
            else
            {
                mqHandler = new MqHandler();
                rv += mqHandler.setUpProducerNew(queue) + " ";
            }
            rv += queue;
            Helper.followTextBoxLog(richTextBoxLog, "Connect to queue: " + queue + " " + rv);
        }

        private void buttonRqmProperties_Click(object sender, RoutedEventArgs e)
        {
            try {
                Helper.followTextBoxLog(richTextBoxLog, mqHandler.getMqPropertiesHost());
            }
            catch (Exception msg)
            {
                logger.Error(msg);
                Helper.followTextBoxLog(richTextBoxLog, "No connection established");
            }
            



        }
    }
}
