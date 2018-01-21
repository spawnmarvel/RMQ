using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RMQ.Send;

namespace RMQ.Controller
{
    class MqHandler
    {
        private static readonly log4net.ILog logger = log4net.LogManager.GetLogger(typeof(MqHandler));
        private Producer producer;
        public MqHandler()
        {

            producer = new Producer();
        }

        public bool setUpProducerDefault()
        {
            
            bool status = producer.createMqConnection("default");
            return status;            

        }
        public bool setUpProducerNew(string queueName)
        {

            bool status = producer.createMqConnection(queueName);
            return status;

        }

        public string publishToDeafult(string msg)
        {
            string rv = producer.publishMsgDefault(msg);
            return rv;
        }
        public string getMqPropertiesHost()
        {
            string rv = "";
            try
            {
                rv = producer.getMqProperties();
            }
            catch(Exception msg)
            {
                logger.Error(msg);
            }
            return rv;
        }
       

        
    }
}
