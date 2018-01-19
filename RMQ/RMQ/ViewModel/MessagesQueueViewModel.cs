using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;
using RMQ.Model;

namespace RMQ.ViewModel
{
    class MessagesQueueViewModel
    {
        public ArrayList queues;

        public MessagesQueueViewModel()
        {

        }
        public ArrayList getQueue()
        {
            return queues;
        }

        public void addMessageQueue(MessagesQueue mq)
        {
            queues.Add(mq);
        }

        public void dummyData()
        {
            MessagesQueue a = new MessagesQueue("queue a");
            MessagesQueue b = new MessagesQueue("queue b");
            addMessageQueue(a);
            addMessageQueue(b);

        }


    }
}
