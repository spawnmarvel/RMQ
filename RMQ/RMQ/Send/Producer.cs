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
using System.Net.Http;
using System.Net;
using Newtonsoft.Json.Linq;

namespace RMQ.Send
{
    class Producer
    {
        private static readonly log4net.ILog logger = log4net.LogManager.GetLogger(typeof(Producer));
        //Main entry point to the RabbitMQ.Net AMQP client API
        private ConnectionFactory conFac;
        //Common AMQP model, spanning the union of the func offred by v 0-8, 0-8qpid and 0-9-1 of AMQP
        private IModel model; // { get; set; }
        //Main interfac to AMQP connection
        private IConnection conn; // { get; set; 
        private bool connStatus;
        private static IConnection instance;
        //queue
        private MessageQueue queue;
        //get RMQ api info
        private string apiInformation { get; set; }

        /// <summary>
        /// constructor
        /// </summary>
        public Producer()
        {
            
            connStatus = false;
            
        }
        /// <summary>
        /// get connection
        /// </summary>
        /// <returns></returns>
        public bool getConnctionStatus()
        {
            return connStatus;
        }
        /// <summary>
        /// get api information
        /// </summary>
        public string ApiInformation
        {
            get
            {
                return apiInformation;
            }

            set
            {
                apiInformation = value;
            }
        }
        /// <summary>
        /// generate mq information
        /// </summary>
        public async void generateMqInformationHttp()
        {
            string msg = "HTTP error";
            string rv = "MQ:";
            logger.Info("HTTP Get in progress");
            using (HttpClient client = new HttpClient())
            {

                var authByteArray = Encoding.ASCII.GetBytes("guest:guest");
                string auth = Convert.ToBase64String(authByteArray);
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Basic", auth);
                //string url = "http://localhost:15672/api/overview";
                string url = "http://localhost:15672/api/queues";
                var response = await client.GetAsync(url);

                if(response.IsSuccessStatusCode)
                {
                    msg = "HTTP success";
                    logger.Info(msg);
                    var responseContent = response.Content;

                    rv = responseContent.ReadAsStringAsync().Result;

                    
                    //var data = JObject.Parse(rv);
                    var data = JArray.Parse(rv);
                    //logger.Debug("jarray " + data);
                    foreach(JObject content in data.Children<JObject>())
                    {
                        logger.Debug(content.SelectToken("data.name"));
                        foreach(JProperty pr in content.Properties())
                        {
                            //logger.Debug(pr.Value.ToString());
                           
                            //logger.Debug("\n");
                            
                        }
                        
                    }


                }
                else
                {
                    logger.Error(msg);

                }

            }
            apiInformation = rv + "\n Work on Json in JsonTester";
            logger.Info(rv);
           
        }
        /// <summary>
        /// get mq properties
        /// </summary>
        /// <returns></returns>
        public string getMqProperties()
        {
            string rv = "RMQ connection properties ";
            try
            {
                IDictionary<string, object> dic = conFac.ClientProperties;
                foreach (KeyValuePair<string, object> pair in dic)
                {
                    logger.Debug(pair.Key + " " + pair.Value);
                }
                logger.Debug(dic);
                rv += "conFac " + conFac.Endpoint.ToString();
                rv += " model " + model.ToString();
                logger.Info(rv);
            }
            catch (NullReferenceException msg)
            {
                logger.Error(msg);
               
            }
            return rv;
        }
       /// <summary>
       /// create mq connection
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
                setUpQueue(queueName);
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
        /// <summary>
        ///  set up queue
        /// </summary>
        /// <param name="queueName"></param>
        /// <returns></returns>
        public bool setUpQueue(string queueName)
        {
            bool status = false;
            logger.Info("Try setup / creating intial queue");
            try
            {

                queue = new MessageQueue(queueName, true, false, false);
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
        /// <summary>
        /// 
        /// </summary>
        /// <param name="messages"></param>
        /// <returns></returns>
        public string publishMsgDefault(string messages)
        {
            logger.Info("Publish");
            string info = "";
            int id = 0;
            try
            {
                IBasicProperties basicProp = model.CreateBasicProperties();
                //set persistent true, meaning the msg can be recoverd
                basicProp.Persistent = true;
                messages = messages + ";";
                byte[] load = Encoding.UTF8.GetBytes(messages);
                PublicationAddress adr = new PublicationAddress(ExchangeType.Direct, "exchange_for_default", "routingKey_for_default");
                model.BasicPublish(adr, basicProp, load);
                info = messages;
                string pub = adr.ToString();
                info += "ID: " + id + ": To " + pub;
                logger.Info("Message = " + info + "Published: " + pub);
                id++;

            }
            catch (NullReferenceException msg)
            {
                info = "" + msg;
                logger.Error(info);
            }
            //model.Dispose();
            conn.Close();
            logger.Info("Closing connection");

            return "Published msg:" + info;
        }



    }
}
