using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMQ.Database
{
    interface IDataBaseStatments
    {
        void insertLogStatus();
        void insert();
        void update();
        void delete();
        void read();
    }
}
