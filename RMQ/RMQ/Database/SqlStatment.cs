using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//project
using Npgsql;

namespace RMQ.Database
{
    class SqlStatment
    {
        private DataBaseConnection dbConnection;
        private static readonly log4net.ILog logger = log4net.LogManager.GetLogger(typeof(SqlStatment));

        public SqlStatment()
        {
            dbConnection = new DataBaseConnection();
        }

        public void insertLogStatus()
        {
            try
            {
                dbConnection.openDb();
                NpgsqlCommand cmd = dbConnection.getConnection().CreateCommand();
                string sql = "insert into log_status (app_id, log_content) values (2, 'test content 2')";
                cmd.CommandText = sql;
                int rows = cmd.ExecuteNonQuery();
                string res = "Rows aff " + rows + " sql: " + sql;
                logger.Info(res);
                dbConnection.closeDb();

            }
            catch(NpgsqlException msg)
            {
                logger.Debug(msg);
            }
        }

    }
}
