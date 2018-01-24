using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMQ.Model
{
    class PriorityQueue : MessageQueue
    {
        private int priority;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="queueName"></param>
        /// <param name="surviveRestart"></param>
        /// <param name="privateCon"></param>
        /// <param name="autoDelete"></param>
        /// <param name="pri"></param>
        public PriorityQueue(string queueName, bool surviveRestart, bool privateCon, bool autoDelete, int pri) 
            : base(queueName, surviveRestart, privateCon, autoDelete)
        {
            Priority = pri;
        }

        /// <summary>
        /// get / set priority, other fields are in base class
        /// </summary>
        public int Priority
        {
            get
            {
                return priority;
            }

            set
            {
                priority = value;
            }
        }
       
    }
}
