using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SBO_f
{
    public class DataBase
    {
        MySqlConnection connection = new MySqlConnection("server = 25.44.81.138; port = 3306; username = test; database = sbo_db; SSL Mode = None");

        public void openConnection()
        {
            if(connection.State == System.Data.ConnectionState.Closed) connection.Open();
        }
        public void closeConnection()
        {
            if(connection.State == System.Data.ConnectionState.Open) connection.Close();
        }
        public MySqlConnection getConnection()
        {
            return connection;
        }
    }
}
