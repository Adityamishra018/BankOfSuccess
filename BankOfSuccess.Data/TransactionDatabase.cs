using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



namespace BankOfSuccess.Data
{
    public class TransactionDatabase
    {
        static public void AddTransactionData(int fromAcc, int toAcc, DateTime time, double amount)
        {
            SqlConnection conn = null;
            try
            {
                //Console.WriteLine("gdd");
                conn = new SqlConnection();
                conn.ConnectionString = ConfigurationManager.ConnectionStrings["default"].ConnectionString;
                // string sqlInsert = $"INSERT INTO TransactionLogs VALUES({fromAcc},{amount},{null},{toAcc})";
                //string sqlInsert = "select FromAccNo from TransactionLogs Where FromAccNo = 123";
                SqlCommand cmd = new SqlCommand();
                string sqlInsert = $"insert into TransactionLogs(FromAccNo,Amount,TransactionDateTime,ToAccNo) values(@fromAcc,@amount,@time,@toAcc)";





                // SqlCommand cmd = new SqlCommand();






                cmd.Parameters.AddWithValue("@fromAcc", fromAcc);
                cmd.Parameters.AddWithValue("@amount", amount);
                cmd.Parameters.AddWithValue("@time", time);
                cmd.Parameters.AddWithValue("@toAcc", toAcc);
                cmd.CommandText = sqlInsert;
                cmd.Connection = conn;
                conn.Open();
                cmd.ExecuteNonQuery();
                /*while(reader.Read())
                {
                    Console.WriteLine(reader[0].ToString());
                }*/




            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                conn.Close();
            }
        }

        static public void AddTransactionData(int fromAcc, DateTime time, double amount)
        {
            SqlConnection conn = null;
            try
            {
               // Console.WriteLine("gdd");
                conn = new SqlConnection();
                conn.ConnectionString = ConfigurationManager.ConnectionStrings["default"].ConnectionString;
                // string sqlInsert = $"INSERT INTO TransactionLogs VALUES({fromAcc},{amount},{null},{toAcc})";
                //string sqlInsert = "select FromAccNo from TransactionLogs Where FromAccNo = 123";
                SqlCommand cmd = new SqlCommand();
                string sqlInsert = $"insert into TransactionLogs(FromAccNo,Amount,TransactionDateTime) values(@fromAcc,@amount,@time)";





                // SqlCommand cmd = new SqlCommand();






                cmd.Parameters.AddWithValue("@fromAcc", fromAcc);
                cmd.Parameters.AddWithValue("@amount", amount);
                cmd.Parameters.AddWithValue("@time", time);
       
                cmd.CommandText = sqlInsert;
                cmd.Connection = conn;
                conn.Open();
                cmd.ExecuteNonQuery();
                /*while(reader.Read())
                {
                    Console.WriteLine(reader[0].ToString());
                }*/




            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                conn.Close();
            }
        }
    }
}