using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMQ.Model
{
    class MessagesQueue
    {
        private string name;
        public MessagesQueue(string name)
        {
            this.name = name;
        }

        public string getName()
        {
            return name;
        }
        public void setName(string name)
        {
            this.name = name;
        }
    }
}
