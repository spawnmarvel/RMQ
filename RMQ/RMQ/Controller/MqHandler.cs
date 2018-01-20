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
        private Producer producer;
        public MqHandler()
        {

            producer = new Producer();
        }

        public bool setUpProducer()
        {
            
            bool status = producer.createMqConnection("default");
            return status;            

        }
        public string producerConnectionProperties()
        {
            return producer.getMqConnctionProperties();
        }
       

        
    }
}
