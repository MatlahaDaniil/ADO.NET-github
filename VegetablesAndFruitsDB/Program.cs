using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VegetablesAndFruitsDB
{
    class VegetablesAndFruitsDB
    {
        SqlConnection connection;

        bool CheckConnect;
        public VegetablesAndFruitsDB(string dbInfo)
        {
            Console.WriteLine("Check Path");

            if (!checkConection(dbInfo))
            {
                Console.WriteLine("Path fail");
                return;
            }
            else
            {
                Console.WriteLine("Path Successful");
            }
        }
        public void ConnectDB()
        {
            if (CheckConnect)
            {
                connection.Open();
                Console.WriteLine("Connection successful");
            }
            else
            {
                Console.WriteLine("Connection fail");
                return;  
            }              
        }

        public void DisconnectDB()
        {
            if (CheckConnect)
            {
                connection.Close();
                Console.WriteLine("Disconnected successful");
            }
            else
            {
                Console.WriteLine("Disconnected fail");
                return;
            }
        }

        private bool checkConection(string dbInfo)
        {
            try
            {
                connection = new SqlConnection(dbInfo);
            }
            catch(Exception)
            {
                return CheckConnect = false;
            }

            return CheckConnect = true;
        }

        public void RequestInfo(string cmd)
        {
            Console.Clear();
            SqlCommand command = new SqlCommand(cmd,connection);
            SqlDataReader reader;
            try
            {
                reader = command.ExecuteReader();
            }
            catch (Exception)
            {
                Console.WriteLine("Invalid Request");
                Console.ReadLine();
                return;
            }
            while (reader.Read())
            {
                for (int i = 0; i < reader.FieldCount; i++)
                {
                    Console.Write(reader[i] + "\t");
                }
                Console.WriteLine();
            }
            reader.Close();
            Console.ReadLine();
        }

        public bool GetCheckConnectionn() { return CheckConnect; }
    }

    internal class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;

            VegetablesAndFruitsDB db = new VegetablesAndFruitsDB("Data Source=DESKTOP-Q6M74QL\\SQLEXPRESS;Initial Catalog=VegetablesAndFruits;Integrated Security=true");
            db.ConnectDB();
            

            Console.WriteLine("\n\nPress key for continue");
            Console.ReadKey();
            string strChoice , strChoice2;
            while(true)
            {
                Console.Clear();

                Console.WriteLine("1 - Вивести всю інформацію\n2 - Вивести назви овочів і фруктів\n" +
                    "3 - Вивести всі кольори\n4 - Показати максимальну калорійність\n" +
                    "5 - Показати мінімальну калорійність\n6 - Показати середню калорійність\n7 - Показати кількість овочів\n" +
                    "8 - Показати кількість фруктів\n9 - Показати кількість овочів і фруктів заданого кольору\n10  - Показати кількість овочів і фруктів кожного кольору" +
                    "\n11 - Показати овочі та фрукти з калорійністю нижче вказаної\n12 - Показати овочі та фрукти з калорійністю вище вказаної.\n" +
                    "13 - Показати овочі та фрукти з калорійністю у вказаному діапазоні\n14 - Показати усі овочі та фрукти жовтого або червоного кольору\n" +
                    "15 - exit");

                int choice = Convert.ToInt32(Console.ReadLine());

                if (choice == 15) break;

                switch (choice)
                {
                    case 1:
                        db.RequestInfo("select * from Info");
                        break;
                    case 2:
                        db.RequestInfo("select [Name] from Info");
                        break;
                    case 3:
                        db.RequestInfo("select [Color] from Info");
                        break;
                    case 4:
                        db.RequestInfo("select max([Calories]) from Info");
                        break;
                    case 5:
                        db.RequestInfo("select min([Calories]) from Info");
                        break;
                    case 6:
                        db.RequestInfo("select avg([Calories]) from Info");
                        break;
                    case 7:
                        db.RequestInfo("select count([Id]) from Info");
                        break;
                    case 8:
                        db.RequestInfo("select count([Id]) from Info where [TypeProduct] = 'фрукт'");
                        break;
                    case 9:
                        Console.WriteLine("Введіть кольор продукту");
                        strChoice = Console.ReadLine();
                        db.RequestInfo($"select count([Id]) from Info where [Color] = '{strChoice}'");
                        break;
                    case 10:
                        db.RequestInfo($"select [Color], count([Id]) from Info group by [Color]");
                        break;
                    case 11:
                        Console.WriteLine("Введіть калорійність");
                        strChoice = Console.ReadLine();
                        db.RequestInfo($"select * from Info where [Calories] < {strChoice}");
                        break;
                    case 12:
                        Console.WriteLine("Введіть калорійність");
                        strChoice = Console.ReadLine();
                        db.RequestInfo($"select * from Info where [Calories] > {strChoice}");
                        break;
                    case 13:
                        Console.WriteLine("Введіть перше число діапазону");
                        strChoice = Console.ReadLine();
                        Console.WriteLine("Введіть друге число діапазону");
                        strChoice2 = Console.ReadLine();
                        db.RequestInfo($"select * from Info where [Calories] >= {strChoice} AND [Calories] <= {strChoice2}");
                        break;
                    case 14:
                        db.RequestInfo($"select * from Info where [Color] = 'жовтий' OR [Color] = 'червоний'");
                        break;

                    default:
                        Console.WriteLine("Invalid Value");
                        break;
                }
            }
            db.DisconnectDB();
            Console.ReadKey();
        }
    }
}
