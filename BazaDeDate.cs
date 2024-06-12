using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chestionar_Auto
{
    public class BazaDeDate
    {
        public  string connectionString = "Server=localhost; Port=3306 ; Database=Chestionar_auto_in_dotnet ; Uid=root ; Pwd=02102002gG;";
        private  MySqlConnection connection;

        public  void OpenConnection()
        {
            try
            {
                connection = new MySqlConnection(connectionString);
                connection.Open();
                Console.WriteLine("Conexiunea la baza de date a fost deschisă cu succes!");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Eroare la deschiderea conexiunii: " + ex.Message);
            }
        }

        public  void CloseConnection()
        {
            try
            {
                if (connection != null && connection.State == System.Data.ConnectionState.Open)
                {
                    connection.Close();
                    Console.WriteLine("Conexiunea la baza de date a fost închisă cu succes!");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Eroare la închiderea conexiunii: " + ex.Message);
            }
        }

        public  MySqlConnection GetConnection()
        {
            OpenConnection();
            return connection;
        }
    }
}
