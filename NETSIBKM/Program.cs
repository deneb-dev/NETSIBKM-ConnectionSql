using NETSIBKM.Models;
using System;
using System.Data.SqlClient;

namespace SIBKMNET
{
    class Program
    {      
            SqlConnection sqlConnection;

            /*
             * Data Source -> Server
             * Initial Catalog -> Database
             * User ID -> username
             * Password -> password
             * Connect Timeout
             */
            string connectionString = "Data Source=LAPTOP-7FOH7BBK;Initial Catalog=DENEBNET;" +
                "User ID=sibkmnet;Password=1234567890;Connect Timeout=30;";

            static void Main(string[] args)
            {
                Program program = new Program();

                //program.GetById(1);

                Country country = new Country()
                {
                    Name = "TES",
                    //Update = "TES dua",
                    Id = 6



                };
            //program.Insert(country);
            //program.Update(country);

            program.GetAll();
            program.Delete(country);
        }

            void GetAll()
            {
                string query = "SELECT * FROM Country";

                sqlConnection = new SqlConnection(connectionString);
                SqlCommand sqlCommand = new SqlCommand(query, sqlConnection);
                try
                {
                    sqlConnection.Open();
                    using (SqlDataReader sqlDataReader = sqlCommand.ExecuteReader())
                    {
                        if (sqlDataReader.HasRows)
                        {
                            while (sqlDataReader.Read())
                            {
                                Console.WriteLine(sqlDataReader[0] + " - " + sqlDataReader[1]);
                            }
                        }
                        else
                        {
                            Console.WriteLine("No Data Rows");
                        }
                        sqlDataReader.Close();
                    }
                    sqlConnection.Close();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.InnerException);
                }
            }

            void GetById(int id)
            {
                string query = "SELECT * FROM Country WHERE Id = @id";

                SqlParameter sqlParameter = new SqlParameter();
                sqlParameter.ParameterName = "@id";
                sqlParameter.Value = id;

                sqlConnection = new SqlConnection(connectionString);
                SqlCommand sqlCommand = new SqlCommand(query, sqlConnection);
                sqlCommand.Parameters.Add(sqlParameter);
                try
                {
                    sqlConnection.Open();
                    using (SqlDataReader sqlDataReader = sqlCommand.ExecuteReader())
                    {
                        if (sqlDataReader.HasRows)
                        {
                            while (sqlDataReader.Read())
                            {
                                Console.WriteLine(sqlDataReader[0] + " - " + sqlDataReader[1]);
                            }
                        }
                        else
                        {
                            Console.WriteLine("No Data Rows");
                        }
                        sqlDataReader.Close();
                    }
                    sqlConnection.Close();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.InnerException);
                }
            }

            //INSERT
            void Insert(Country country)
            {
                using (SqlConnection sqlConnection = new SqlConnection(connectionString))
                {
                    sqlConnection.Open();
                    SqlTransaction sqlTransaction = sqlConnection.BeginTransaction();

                    SqlCommand sqlCommand = sqlConnection.CreateCommand();
                    sqlCommand.Transaction = sqlTransaction;

                    SqlParameter sqlParameter = new SqlParameter();
                    sqlParameter.ParameterName = "@name";
                    sqlParameter.Value = country.Name;

                    sqlCommand.Parameters.Add(sqlParameter);

                    try
                    {
                        sqlCommand.CommandText = "INSERT INTO Country " +
                            "(Name) VALUES (@name)";
                        sqlCommand.ExecuteNonQuery();
                        sqlTransaction.Commit();
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.InnerException);
                    }
                }
            }

            /* Update */

            void Update(Country country)
            {
                using (SqlConnection sqlConnection = new SqlConnection(connectionString))
                {
                    sqlConnection.Open();
                    SqlTransaction sqlTransaction = sqlConnection.BeginTransaction();

                    SqlCommand sqlCommand = sqlConnection.CreateCommand();
                    sqlCommand.Transaction = sqlTransaction;

                    SqlParameter sqlParameter = new SqlParameter();
                    SqlParameter sqlParameter1 = new SqlParameter();
                    SqlParameter sqlParameter2 = new SqlParameter();
                    sqlParameter.ParameterName = "@name";
                    sqlParameter.Value = country.Name;
                    sqlParameter1.ParameterName = "@update";
                    sqlParameter1.Value = country.Update;
                    sqlParameter2.ParameterName = "@id";
                    sqlParameter2.Value = country.Id;

                    sqlCommand.Parameters.Add(sqlParameter);
                    sqlCommand.Parameters.Add(sqlParameter1);
                    sqlCommand.Parameters.Add(sqlParameter2);

                    try
                    {
                        sqlCommand.CommandText = "UPDATE Country SET name = (@update)" + "WHERE Id = (@id)";
                        sqlCommand.ExecuteNonQuery();
                        sqlTransaction.Commit();
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.InnerException);
                    }
                }
            }

            /* Delete */
            void Delete(Country country)
            {
                using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                    sqlConnection.Open();
                    SqlTransaction sqlTransaction = sqlConnection.BeginTransaction();

                    SqlCommand sqlCommand = sqlConnection.CreateCommand();
                    sqlCommand.Transaction = sqlTransaction;

                    SqlParameter sqlParameter = new SqlParameter();
                    sqlParameter.ParameterName = "@id";
                    sqlParameter.Value = country.Id;

                    sqlCommand.Parameters.Add(sqlParameter);

                    try
                    {
                        sqlCommand.CommandText = "DELETE Country " + "WHERE (Id) = (@id)";
                        sqlCommand.ExecuteNonQuery();
                        sqlTransaction.Commit();
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.InnerException);
                    }
                }
            }
        }
    }

            