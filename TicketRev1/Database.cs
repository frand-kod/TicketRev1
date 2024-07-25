using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;


namespace TicketRev1
{
    public class Database
    {
        private string strConn;
        private string server = "localhost";
        private string databaseName= "test";
        private string username="root";
        private string password="";

        public Database()
        {
            strConn = $"server={server} + databaseName={databaseName} + username={username} + password={password}";
        }

        public MySqlConnection getConnection()
        {
            return new MySqlConnection(strConn);
        }
    }
    public class Kota
    {
        private int Id {  get; set; }
        private string Name { get; set; }

    }


}
