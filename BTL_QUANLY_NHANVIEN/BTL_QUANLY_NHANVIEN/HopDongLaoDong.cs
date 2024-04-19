using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BTL_QUANLY_NHANVIEN
{
    public partial class HopDongLaoDong : Form
    {
        public HopDongLaoDong()
        {
            InitializeComponent();
        }
        Modify modify = new Modify();
        private void HopDongLaoDong_Load(object sender, EventArgs e)
        {
            modify = new Modify();
            try
            {
                guna2DataGridView1.DataSource = modify.getAllHDLD();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi" + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            AddDataCbx();
 


        }
        private void AddDataCbx()
        {
            string query = "SELECT * FROM CHUCVU ";
           
        }
        /*private void AddDataCbx_NV()
        {
            string query = "SELECT * FROM NHANVIEN ";
            cbx_maNV.DataSource = DataProvider.Instance.ExcuteQuery(query);
            cbx_maNV.DisplayMember = "MaNV";
            cbx_maNV.SelectedIndex = -1;
        }*/

        private void guna2DataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            int i;
            i = guna2DataGridView1.CurrentRow.Index;
            txt_mahd.Text = guna2DataGridView1.Rows[i].Cells[0].Value.ToString();
            txt_manv.Text = guna2DataGridView1.Rows[i].Cells[1].Value.ToString();
            dt_ngayra.Text = guna2DataGridView1.Rows[i].Cells[2].Value.ToString();
            dt_ngayvao.Text = guna2DataGridView1.Rows[i].Cells[3].Value.ToString();
        }

        private void btn_them_Click(object sender, EventArgs e)
        {

            DateTime nv = this.dt_ngayvao.Value;
            DateTime nr = this.dt_ngayra.Value;
            string maNV = txt_manv.Text;
            try
            {
                // Tạo ID mới cho bản ghi
                string queryCountId = "SELECT COUNT(*) FROM HOPDONGLAODONG WHERE MaHD = @MaHD";
                string maHD = DataProvider.Instance.GenerateId(queryCountId, "HD");
                // Thực hiện câu truy vấn INSERT
                string query = "INSERT INTO HOPDONGLAODONG(MaHD,MaNV, Ngayvao, Ngayra) VALUES (@MaHD,@MaNV, @Ngayvao, @Ngayra)";
                object[] parameters = new object[] { maHD,maNV,nv,nr };
                DataProvider.Instance.ExcuteNonQuery(query, parameters);

                // Cập nhật DataGridView với dữ liệu mới
                guna2DataGridView1.DataSource = modify.getAllHDLD();

                // Hiển thị thông báo thành công
                MessageBox.Show("Thêm bản ghi thành công.");

                // Xóa dữ liệu đã nhập sau khi thêm thành công

                //   cbx_maCV.SelectedIndex = -1;
            }
            catch (Exception ex)
            {
                // Hiển thị thông báo lỗi nếu có lỗi xảy ra
                MessageBox.Show("Đã xảy ra lỗi: " + ex.Message);
            }
        }

        private void btn_sua_Click(object sender, EventArgs e)
        {

            // Check if a row is selected
            if (guna2DataGridView1.SelectedRows.Count == 0)
            {
                MessageBox.Show("Vui lòng chọn một dòng để sửa.");
                return;
            }

            // Get the information of the selected row from DataGridView
            string maHD = guna2DataGridView1.SelectedRows[0].Cells["MaHD"].Value.ToString();
            string maNV = txt_manv.Text;
            DateTime ngayVao = dt_ngayvao.Value;
            DateTime ngayRa = dt_ngayra.Value;

            // Validate the input data
            if (string.IsNullOrWhiteSpace(maNV))
            {
                MessageBox.Show("Vui lòng nhập đủ thông tin.");
                return;
            }

            try
            {
                // Perform the UPDATE query
                string query = "UPDATE HOPDONGLAODONG SET MaNV = @MaNV, Ngayvao = @Ngayvao, Ngayra = @Ngayra WHERE MaHD = @MaHD";
                object[] parameters = new object[] { maNV, ngayVao, ngayRa, maHD };
                DataProvider.Instance.ExcuteNonQuery(query, parameters);

                // Update the DataGridView with the new data
                guna2DataGridView1.DataSource = modify.getAllHDLD();

                // Show a success message
                MessageBox.Show("Sửa bản ghi thành công.");

                // Clear the input fields and reset the selection
                txt_manv.Clear();
                dt_ngayvao.Value = DateTime.Now; // Reset to current date
                dt_ngayra.Value = DateTime.Now; // Reset to current date
            }
            catch (Exception ex)
            {
                // Show an error message if an error occurs
                MessageBox.Show("Đã xảy ra lỗi: " + ex.Message);
            }
        }

        private void btn_xoa_Click(object sender, EventArgs e)
        {

            // Check if a row is selected
            if (guna2DataGridView1.SelectedRows.Count == 0)
            {
                MessageBox.Show("Vui lòng chọn một dòng để xóa.");
                return;
            }

            // Display a confirmation dialog
            DialogResult result = MessageBox.Show("Bạn có chắc chắn muốn xóa bản ghi này?", "Xác nhận xóa", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.No)
                return;

            try
            {
                // Get the MaHD of the selected record to delete
                string maHD = guna2DataGridView1.SelectedRows[0].Cells["MaHD"].Value.ToString();

                // Construct the delete query
                string query = "DELETE FROM HOPDONGLAODONG WHERE MaHD = @MaHD";

                // Set up the parameters for the query
                object[] parameters = new object[] { maHD };

                // Execute the delete query
                DataProvider.Instance.ExcuteNonQuery(query, parameters);

                // Refresh the DataGridView with the updated data
                guna2DataGridView1.DataSource = modify.getAllHDLD();

                // Show a success message
                MessageBox.Show("Xóa bản ghi thành công.");
            }
            catch (Exception ex)
            {
                // Show an error message if an error occurs
                MessageBox.Show("Đã xảy ra lỗi: " + ex.Message);
            }
        }
    }
}
