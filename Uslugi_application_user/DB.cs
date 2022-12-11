using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Uslugi_application_user
{
    class DB
    {
        MySqlConnection connectionUsers = new MySqlConnection("server=localhost; port=3306; username=root; password=root; database=parkingservice");

        public void openConnection()
        {
            if (connectionUsers.State == System.Data.ConnectionState.Closed)
                connectionUsers.Open();
        }
        public void closeConnection()
        {
            if (connectionUsers.State == System.Data.ConnectionState.Closed)
                connectionUsers.Close();
        }
        public MySqlConnection getConnection()
        {
            return connectionUsers;
        }
    }
}
