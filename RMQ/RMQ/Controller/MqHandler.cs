using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//project
using RMQ.Send;
using RMQ.Database;

namespace RMQ.Controller
{
    class MqHandler
    {
        private static readonly log4net.ILog logger = log4net.LogManager.GetLogger(typeof(MqHandler));
        private Producer producer;
        private SqlStatment sqlStatem;

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
        public string getMqInformation()
        {
            producer.generateMqInformationHttp();
            string rv = producer.ApiInformation;
            return rv;
            
        }

        public string publishToDeafult(string msg)
        {
            //add reconnect, check con
            sqlStatem = new SqlStatment();
            sqlStatem.insertLogStatus();
            string rv = producer.publishMsgDefault(msg + "make json auth for postgresql");
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
