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
        // PostgeSQL-style connection string
        private static string conString = String.Format("Server={0};Port={1};" + "User Id={2};Password={3};Database={4};", "localhost", 5432, "postgres", "rabbit", "rabbitmq_db");

        public DataBaseConnection()
        {
            readJson();
        }

        public NpgsqlConnection getConnection()
        {
            return conn;
        }
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

        public void readJson()
        {
            logger.Debug("Read json");
            try
            {

           
            JObject o1 = JObject.Parse(File.ReadAllText(@"auth.json"));
            using (StreamReader file = File.OpenText(@"auth.json"))
            using (JsonTextReader reader = new JsonTextReader(file))
            {
                JObject o2 = (JObject)JToken.ReadFrom(reader);
                logger.Debug("json: " + o2);
            }
            }
            catch (FileNotFoundException msg)
            {
                logger.Debug(msg);
            }
        }

    }
}
