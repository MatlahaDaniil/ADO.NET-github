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
        static void RequestDB(SqlConnection conn , string command)
        {
            Console.Clear();

            SqlCommand sqlCommand = conn.CreateCommand();
            sqlCommand = new SqlCommand(command, conn);
            SqlDataReader reader;
            try
            {
                reader = sqlCommand.ExecuteReader();
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

        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;
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

            if (!checkConnect)
                Console.WriteLine("Path fail");
            else
                Console.WriteLine("Path Successful");

            Console.WriteLine("\n\n\npress key");
            Console.ReadLine();
            conn.Open();
            string strChoice;

            while (true)
            {
                Console.Clear();
                Console.WriteLine("1 - Всі записи\n2 - Відображати ПІБ усіх студентів\n3 - Відображати усі середні оцінки\n" +
                                  "4 - Показати ПІБ усіх студентів з мінімальною оцінкою,більшою, ніж зазначена\n" +
                                  "5 - Показати назви усіх предметів із мінімальними середніми оцінками\n6 - Показати мінімальну середню оцінку\n" +
                                  "7 - Показати максимальну середню оцінку\n8 - Показати кількість студентів з мінімальною середньою оцінкою з математики\n" +
                                  "9 - Показати кількість студентів, в яких максимальна середня оцінка з математики\n" +
                                  "10 - Показати кількість студентів у кожній групі\n11 - exit");
                int choice = Convert.ToInt32(Console.ReadLine());

                if (choice == 11) break;

                switch (choice)
                {
                    case 1:
                        RequestDB(conn, "select * from StudentsInfo");
                        break;
                    case 2:
                        RequestDB(conn, "select [Fullname] from StudentsInfo");
                        break;
                    case 3:
                        RequestDB(conn, "select [AvgMarkAllSubjectsForYear] from StudentsInfo");
                        break;
                    case 4:
                        Console.WriteLine("Введіть мінімальну оцінку");
                        strChoice = Console.ReadLine();
                        RequestDB(conn, $"select [Fullname] from StudentsInfo where [AvgMarkAllSubjectsForYear] > {strChoice}");
                        break;
                    case 5:
                        RequestDB(conn, "select [NameSubjectWithMinMark] from StudentsInfo");
                        break;
                    case 6:
                        RequestDB(conn, "select min([AvgMarkAllSubjectsForYear]) from StudentsInfo");
                        break;
                    case 7:
                        RequestDB(conn, "select max([AvgMarkAllSubjectsForYear]) from StudentsInfo");
                        break;
                    case 8:
                        RequestDB(conn, "select count([AvgMarkAllSubjectsForYear]) from StudentsInfo where [NameSubjectWithMinMark] = 'Maths'");
                        break;
                    case 9:
                        RequestDB(conn, "select count([AvgMarkAllSubjectsForYear]) from StudentsInfo where [NameSubjectWithMaxMark] = 'Maths'");
                        break;
                    case 10:
                        RequestDB(conn, "select [NameGroup],count([Id]) from StudentsInfo group by [NameGroup]");
                        break;
                    
                    default:
                        Console.WriteLine("Invalid value");
                        break;
                }
            }
            conn.Close();

            Console.ReadLine();
        }
    }
}
