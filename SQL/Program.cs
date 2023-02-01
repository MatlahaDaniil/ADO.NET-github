using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace SQL
{
    internal class Program
    {
        static void Main(string[] args)
        {
            SqlConnection conn = new SqlConnection();

            string connectionInfo = "Data Source=DESKTOP-Q6M74QL\\SQLEXPRESS;Initial Catalog=StudentsMarks;Integrated Security=true";
            bool checkConnect = true;
            try
            {
                conn = new SqlConnection(connectionInfo);
            }
            catch (Exception)
            {
                checkConnect = false;
            }
            
            if(!checkConnect)
                Console.WriteLine("connection fail");
            else          
                Console.WriteLine("Connection Successful");

            Console.WriteLine("Press 1 for connection OR 2 for disconnection");
            int num = Convert.ToInt32(Console.ReadLine());
            if(num == 1)
            {
                Console.WriteLine("Connect");
                conn.Open();
            }
            else if (num == 2)
            {
                Console.WriteLine("Disconnect");
                conn.Close();
            }
            SqlCommand sqlCommand= conn.CreateCommand();
            sqlCommand = new SqlCommand("select * from Info" , conn);

            conn.Close();

            Console.ReadLine();
        }
    }
}
