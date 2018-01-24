using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//project
using Npgsql;
using Newtonsoft;
using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace RMQ.Database
{
    class DataBaseConnection
    {
        private static readonly log4net.ILog logger = log4net.LogManager.GetLogger(typeof(DataBaseConnection));
        private static NpgsqlConnection conn;
        //private static NpgsqlDataAdapter da;
        //private static NpgsqlCommand cmd;
        //PostgeSQL-style connection string
        private static string conString;

        /// <summary>
        /// constructor
        /// </summary>
        public DataBaseConnection()
        {
            setUpConnection(readJsonMakeConnectionString());
        }
        /// <summary>
        /// set up connection
        /// </summary>
        /// <param name="auth"></param>
        public void setUpConnection(string auth)
        {
            logger.Info("Set up connection string json");
            string[] rv = auth.Split(';');
            string host = rv[0];
            string database = rv[1];
            int port = Convert.ToInt32(rv[2]);
            string user = rv[3];
            string pass = rv[4];
            conString = String.Format("Server={0};Port={1};" + "User Id={2};Password={3};Database={4};", host, port, user, pass, database);

        }
        /// <summary>
        /// get connection
        /// </summary>
        /// <returns></returns>
        public NpgsqlConnection getConnection()
        {
            return conn;
        }
        /// <summary>
        /// open db
        /// </summary>
        /// <returns></returns>
        public bool openDb()
        {
            bool status = false;
            try
            {
                conn = new NpgsqlConnection(conString);
                conn.Open();
                status = true;
                logger.Info("Db is open");
            }
            catch (Exception msg)
            {
                logger.Debug(msg);
            }
            return status;
        }

        /// <summary>
        /// close db
        /// </summary>
        /// <returns></returns>
        public bool closeDb()
        {
            bool status = false;
            try
            {

                conn.Close();
                status = true;
                logger.Info("Db is closed");
            }
            catch (Exception msg)
            {
                logger.Debug(msg);
            }
            return status;
        }

        /// <summary>
        /// read json, format {"host": "localhost","database": "my_db","port": 9999,"user": "my_user","pass" : "my_password", "description" : "you get the picture"}
        /// </summary>
        /// <returns></returns>
        public string readJsonMakeConnectionString()
        {

            string jAuth = "";
            bool status = false;
            try
            {

                using (StreamReader file = File.OpenText(@"auth.json"))
                using (JsonTextReader reader = new JsonTextReader(file))
                {
                    JObject authFile = (JObject)JToken.ReadFrom(reader);
                    //logger.Debug(reader.TokenType + " " + reader.ValueType + " " + reader.Value);
                    foreach (KeyValuePair<string, JToken> keyValue in authFile)
                    {
                        //get the values
                        //logger.Debug(keyValue.Value.ToString());
                        jAuth += keyValue.Value.ToString() + ";";
                        status = true;
                    }

                }

            }
            catch (FileNotFoundException msg)
            {
                logger.Debug(msg);
            }
            logger.Debug("Read json file status: " + status);
            return jAuth;
        }

    }
}
