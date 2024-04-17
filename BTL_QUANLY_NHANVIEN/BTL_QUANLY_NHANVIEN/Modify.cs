using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;
using System.Collections;

namespace BTL_QUANLY_NHANVIEN
{
    internal class Modify
    {
        SqlDataAdapter dataAdapter; //truy xuất dữ liệu vào bảng
                                    //Dataset trả về nhiều bảng
                                    //Datatable trả về một bảng
        public Modify() { }
        SqlCommand sqlCommand;//dùng để truy vấn câu lệnh insert, update,delete,...
        private SqlDataReader dataReader;
        SqlDataReader sqlDataReader;//dùng để đọc dữ liệu trong bảng
        private SqlConnection connection;
        public List<loginClass> TaiKhoans(string query)//check tai khoan
        {
            List<loginClass> TaiKhoans = new List<loginClass>();
            using (SqlConnection sqlConnection = Connection.GetSqlConnection())
            {
                sqlConnection.Open();
                sqlCommand = new SqlCommand(query, sqlConnection);
                dataReader = sqlCommand.ExecuteReader();
                while (dataReader.Read())
                {
                    TaiKhoans.Add(new loginClass(dataReader.GetString(8), dataReader.GetString(9)));
                }


                sqlConnection.Close();
            }
            return TaiKhoans;
        }
        public void Command(string query)//dang ky tai khoan
        {
            using (SqlConnection sqlConnection = Connection.GetSqlConnection())
            {
                sqlConnection.Open();
                sqlCommand = new SqlCommand(query, sqlConnection);
                sqlCommand.ExecuteNonQuery();//thuc thi cau truy van
                sqlConnection.Close();
            }
        }

        public DataTable XemDL(string sql)
        {
            DataTable dataTable = new DataTable();
            using (SqlConnection sqlConnection = Connection.GetSqlConnection())
            {
                sqlConnection.Open();
                dataAdapter = new SqlDataAdapter(sql, sqlConnection);
                dataAdapter.Fill(dataTable);
                sqlConnection.Close();
            }


            return dataTable;

        }
        public DataTable getAllNhanvien()
        {
            DataTable dataTable = new DataTable();
            string query = "select * from NHANVIEN";
            using (SqlConnection sqlConnection = Connection.GetSqlConnection())
            {
                sqlConnection.Open();
                dataAdapter = new SqlDataAdapter(query, sqlConnection);
                dataAdapter.Fill(dataTable);
                sqlConnection.Close();
            }


            return dataTable;
        }

        public DataTable getAllTDHV() { 
            DataTable dataTable = new DataTable();
            string query = "select * from TRINHDOHOCVAN";
            using (SqlConnection sqlConnection = Connection.GetSqlConnection())
            {
                sqlConnection.Open();
                dataAdapter = new SqlDataAdapter(query, sqlConnection);
                dataAdapter.Fill(dataTable);
                sqlConnection.Close();
            }


            return dataTable;
        }


        public DataTable getAllPHONGBAN()
        {
            DataTable dataTable = new DataTable();
            string query = "select * from PHONGBAN";
            using (SqlConnection sqlConnection = Connection.GetSqlConnection())
            {
                sqlConnection.Open();
                dataAdapter = new SqlDataAdapter(query, sqlConnection);
                dataAdapter.Fill(dataTable);
                sqlConnection.Close();
            }


            return dataTable;
        }

        public DataTable getAllLUONG()
        {
            DataTable dataTable = new DataTable();
            string query = "select * from LUONG";
            using (SqlConnection sqlConnection = Connection.GetSqlConnection())
            {
                sqlConnection.Open();
                dataAdapter = new SqlDataAdapter(query, sqlConnection);
                dataAdapter.Fill(dataTable);
                sqlConnection.Close();
            }


            return dataTable;
        }

        public DataTable getAllHDLD()
        {
            DataTable dataTable = new DataTable();
            string query = "select * from HOPDONGLAODONG";
            using (SqlConnection sqlConnection = Connection.GetSqlConnection())
            {
                sqlConnection.Open();
                dataAdapter = new SqlDataAdapter(query, sqlConnection);
                dataAdapter.Fill(dataTable);
                sqlConnection.Close();
            }


            return dataTable;
        }

        public DataTable getAllCHUCVU()
        {
            DataTable dataTable = new DataTable();
            string query = "select * from CHUCVU";
            using (SqlConnection sqlConnection = Connection.GetSqlConnection())
            {
                sqlConnection.Open();
                dataAdapter = new SqlDataAdapter(query, sqlConnection);
                dataAdapter.Fill(dataTable);
                sqlConnection.Close();
            }


            return dataTable;
        }
        /*public bool insert(Nhanvien nhanvien)
        {
            string query = "INSERT INTO NHANVIEN (MaNV, TenNV, DanToc, Gioitinh, Ngaysinh, Quequan, SDTNV, MaCV, MaTDHV, Bacluong, MaPB, Email, TaiKhoan, MatKhau) " +
                            "VALUES (@MaNV, @TenNV, @DanToc, @Gioitinh, @NgaySinh, @QueQuan, @SDTNV, @MaCV, @MaTDHV, @Bacluong, @MaPB, @Email, @TaiKhoan, @MatKhau)";
            try
            {
                using (var connection = Connection.GetSqlConnection())
                using (var command = new SqlCommand(query, connection))
                {
                    connection.Open();
                    command.Parameters.AddWithValue("@MaNV", nhanvien.MaNV1);
                    command.Parameters.AddWithValue("@TenNV", nhanvien.TenNV1);
                    command.Parameters.AddWithValue("@DanToc", nhanvien.DanToc1);
                    command.Parameters.AddWithValue("@GioiTinh", nhanvien.GioiTinh1);
                    command.Parameters.AddWithValue("@NgaySinh", nhanvien.NgaySinh1);
                    command.Parameters.AddWithValue("@QueQuan", nhanvien.QueQuan1);
                    command.Parameters.AddWithValue("@SDTNV", nhanvien.SDT1);
                    command.Parameters.AddWithValue("@MaCV", nhanvien.MaCV1);
                    command.Parameters.AddWithValue("@MaTDHV", nhanvien.MaTDHV1);
                    command.Parameters.AddWithValue("@Bacluong", nhanvien.BacLuong1);
                    command.Parameters.AddWithValue("@MaPB", nhanvien.MaPB1);
                    command.Parameters.AddWithValue("@Email", nhanvien.Email1);
                    command.Parameters.AddWithValue("@TaiKhoan", nhanvien.TaiKhoan1);
                    command.Parameters.AddWithValue("@MatKhau", nhanvien.MatKhau1);
                    command.ExecuteNonQuery();
                }
                return true;
            }
            catch (Exception ex)
            {
                // Xử lý lỗi cụ thể và ghi nhật ký hoặc thông báo lỗi
                Console.WriteLine("Lỗi khi thêm nhân viên: " + ex.Message);
                return false;
            }

        }*/

    }
}
