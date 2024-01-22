using System;
using System.Collections.Generic;
using System.Data.SqlClient;
//using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//using Microsoft.ReportingServices.Diagnostics.Internal;

namespace QLPT
{
    class DataProvider
    {
        //private static DataProvider instance;
        private static string connectionSTR = @"Data Source=ASUS;Initial Catalog=quanlynhatro;Integrated Security=True;Encrypt=True;Trust Server Certificate=True";
        public SqlConnection GetConnection()
        {
            return new SqlConnection(connectionSTR);
        }
    }

    //    public class DataProvider
    //    {
    //        private static DataProvider instance;
    //        private string connectionSTR = @"Data Source=ASUS;Initial Catalog=quanlynhatro;Integrated Security=True;Encrypt=True;Trust Server Certificate=True";

    //        // sử dụng mẫu thiết kế singleton đảm bảo lớp DataProvider chỉ có 1 thể hiện (instance) duy nhất
    //        public static DataProvider Instance
    //        {
    //            get { if (instance == null) instance = new DataProvider(); return DataProvider.instance; }
    //            private set { DataProvider.instance = value; }
    //        }

    //        // đưa dữ liệu ra DataTable
    //        public DataTable ExecuteQuery(string query, object[] parameters = null)
    //        {
    //            DataTable dt = new DataTable();

    //            using (SqlConnection connection = new SqlConnection(connectionSTR))
    //            {
    //                connection.Open();

    //                SqlCommand command = new SqlCommand(query, connection);

    //                if (parameters != null)
    //                {
    //                    string[] listPara = query.Split(' ');

    //                    int i = 0;

    //                    foreach (string item in listPara)
    //                    {
    //                        if (item.Contains('@'))
    //                        {
    //                            command.Parameters.AddWithValue(item, parameters[i]);
    //                            i++;
    //                        }
    //                    }
    //                }

    //                SqlDataAdapter adapter = new SqlDataAdapter(command);

    //                adapter.Fill(dt);

    //                connection.Close();
    //            }

    //            return dt;
    //        }

    //        // thêm, xóa
    //        public int ExecuteNonQuery(string query, object[] parameters = null)
    //        {
    //            int data = 0;

    //            using (SqlConnection connection = new SqlConnection(connectionSTR))
    //            {
    //                connection.Open();

    //                SqlCommand command = new SqlCommand(query, connection);

    //                if (parameters != null)
    //                {
    //                    string[] listPara = query.Split(' ');

    //                    int i = 0;

    //                    foreach (string item in listPara)
    //                    {
    //                        if (item.Contains('@'))
    //                        {
    //                            command.Parameters.AddWithValue(item, parameters[i]);
    //                            i++;
    //                        }
    //                    }
    //                }

    //                data = command.ExecuteNonQuery();
    //                connection.Close();
    //            }

    //            return data;
    //        }

    //        // lấy giá trị đơn (sử dụng cho các truy vấn trả về một giá trị duy nhất)
    //        public object ExecuteScalar(string query, object[] parameters = null)
    //        {
    //            object result = null;

    //            using (SqlConnection connection = new SqlConnection(connectionSTR))
    //            {
    //                connection.Open();

    //                SqlCommand command = new SqlCommand(query, connection);

    //                if (parameters != null)
    //                {
    //                    string[] listPara = query.Split(' ');

    //                    int i = 0;

    //                    foreach (string item in listPara)
    //                    {
    //                        if (item.Contains('@'))
    //                        {
    //                            command.Parameters.AddWithValue(item, parameters[i]);
    //                            i++;
    //                        }
    //                    }
    //                }

    //                result = command.ExecuteScalar();
    //                connection.Close();
    //            }

    //            return result;
    //        }
    //    }
}
