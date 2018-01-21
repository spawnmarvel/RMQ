using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMQ.Model
{
    class MessageQueue
    {
        private string name;
        //Durable (the queue will survive a broker restart)
        private bool durable;
        //Exclusive(used by only one connection and the queue will be deleted when that connection closes)
        private bool privateCon;
        //Auto-delete(queue that has had at least one consumer is deleted when last consumer unsubscribes)
        private bool delete;
        //Arguments (optional; used by plugins and broker-specific features such as message TTL, queue length limit, etc)
        //private string args;

        /// <summary>
        /// Make a queue, name, true, false, false, null
        /// </summary>
        /// <param name="queueName"></param>
        /// <param name="surviveRestart"></param>
        /// <param name="privateCon"></param>
        /// <param name="autoDelete"></param>
        public MessageQueue(string queueName, bool surviveRestart, bool privateCon, bool autoDelete)
        {
            this.name = queueName;
            this.durable = surviveRestart;
            this.privateCon = privateCon;
            this.delete = autoDelete;
        }


        public string toString()
        {
            return "Queue info; " + "name ; " +name + "; durable; " + durable + "; exclusive; " + privateCon + "; auto-delete; " + delete;
        }
        public string Name
        {
            get
            {
                return name;
            }

            set
            {
                name = value;
            }
        }

        public bool Durable
        {
            get
            {
                return durable;
            }

            set
            {
                durable = value;
            }
        }

        public bool PrivateCon
        {
            get
            {
                return privateCon;
            }

            set
            {
                privateCon = value;
            }
        }

        public bool Delete
        {
            get
            {
                return delete;
            }

            set
            {
                delete = value;
            }
        }

       

       

       
    }
}
