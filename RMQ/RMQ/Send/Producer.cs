using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//project
using RabbitMQ.Client;
using RabbitMQ.Client.Exceptions;
using System.Collections;
using RMQ.Model;

namespace RMQ.Send
{
    class Producer
    {
        private static readonly log4net.ILog logger = log4net.LogManager.GetLogger(typeof(Producer));
        //Main entry point to the RabbitMQ.Net AMQP client API
        private ConnectionFactory conFac;
        //Common AMQP model, spanning the union of the func offred by v 0-8, 0-8qpid and 0-9-1 of AMQP
        private IModel model { get; set; }
        //Main interfac to AMQP connection
        private IConnection conn { get; set; }
        private bool connStatus;
        private static IConnection instance;
        //queue
        private MessagesQueue queue;


        public Producer()
        {
            connStatus = false;
        }

        public bool getConnctionStatus()
        {
            return connStatus;
        }
        public string getMqConnctionProperties()
        {
            string rv = "RMQ connection properties ";
            IDictionary<string, object> dic = conFac.ClientProperties;
            foreach(KeyValuePair<string, object> pair in dic)
            {
                logger.Debug(pair.Key + " " + pair.Value);
            }
            logger.Debug(dic);
            rv += conFac.Endpoint.ToString();
            logger.Info(rv);
            return rv;
        }
       /// <summary>
       /// 
       /// </summary>
       /// <param name="queueName"></param>
       /// <returns></returns>
       public bool createMqConnection(string queueName)
        {
            logger.Debug("Try create MQ connection for queue " + queueName);
            
            try
            {

                //connection
                conFac = new ConnectionFactory();
                conFac.HostName = "localhost";
                conFac.UserName = "guest";
                conFac.Password = "guest";
                conFac.RequestedHeartbeat = 30;
                //automatic reconnect
                //maybe not use this one, could cerate many channels
                conFac.AutomaticRecoveryEnabled = true;
                conn = conFac.CreateConnection();
                connStatus = true;
                setUpIntialQueue(queueName);
                logger.Info("Create MQ connection for queue " + queueName + " is " + connStatus);
            }
            catch(BrokerUnreachableException msg)
            {
                logger.Error(msg);
            }
            catch(Exception msg)
            {
                logger.Error(msg);
            }
            return connStatus;
            
        }

        public bool setUpIntialQueue(string queueName)
        {
            bool status = false;
            logger.Info("Try setup / creating intial queue");
            try
            {

                queue = new MessagesQueue(queueName, true, false, false);
                logger.Info(queue.toString());
                model = conn.CreateModel();
                //create queue

                model.QueueDeclare(queue.Name, queue.Durable, queue.PrivateCon, queue.Delete, null);
                string exchange = "exchange_for_" + queue.Name;
                string routingKey = "routingKey_for_" + queue.Name;
                //A direct exchange delivers messages to queues based on the message routing key
                //Direct exchange: a queue binds to the exchange with a routing key K
                //When a new message with routing key R arrives at the direct exchange, the exchange routes it to the queue if K = R
                model.ExchangeDeclare(exchange, ExchangeType.Direct, true);
                //bind with routing key
                model.QueueBind(queue.Name, exchange, routingKey);
                status = true;
                logger.Info("Setup / creating " + queueName +  " is " + status);
            }
            catch(NullReferenceException msg)
            {
                logger.Error(msg);
            }
            catch (Exception msg)
            {
                logger.Error(msg);
            }
            return status;
        }


    }
}
