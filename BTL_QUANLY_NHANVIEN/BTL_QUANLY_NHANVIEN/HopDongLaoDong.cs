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
            AddDataCbx_NV();


        }
        private void AddDataCbx()
        {
            string query = "SELECT * FROM CHUCVU ";
            cbx_maCV.DataSource = DataProvider.Instance.ExcuteQuery(query);
            cbx_maCV.DisplayMember = "MaCV";
            cbx_maCV.SelectedIndex = -1;
        }
        private void AddDataCbx_NV()
        {
            string query = "SELECT * FROM NHANVIEN ";
            cbx_maNV.DataSource = DataProvider.Instance.ExcuteQuery(query);
            cbx_maNV.DisplayMember = "MaNV";
            cbx_maNV.SelectedIndex = -1;
        }

        private void guna2DataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            int i;
            i = guna2DataGridView1.CurrentRow.Index;
            cbx_maNV.Text = guna2DataGridView1.Rows[i].Cells[0].Value.ToString();
            cbx_maCV.Text = guna2DataGridView1.Rows[i].Cells[1].Value.ToString();
            txt_ngay.Text = guna2DataGridView1.Rows[i].Cells[2].Value.ToString();
        }

        private void btn_them_Click(object sender, EventArgs e)
        {
            string mcv = cbx_maCV.SelectedItem?.ToString();
            string mnv = cbx_maNV.SelectedItem?.ToString(); 
            string dt = txt_ngay.Text.Trim();

            if (string.IsNullOrWhiteSpace(mcv) || string.IsNullOrWhiteSpace(mnv))
            {
                MessageBox.Show("Vui lòng nhập đủ thông tin.");
                return;
            }

            try
            {
                // Thực hiện câu truy vấn INSERT
                string query = "INSERT INTO HOPDONGLAODONG(MaNV, MaCV, Ngaynhamchuc) VALUES (@MaNV, @MaCV, @Ngaynhamchuc)";
                object[] parameters = new object[] { mnv, mcv, dt };
                DataProvider.Instance.ExcuteNonQuery(query, parameters);

                // Cập nhật DataGridView với dữ liệu mới
                guna2DataGridView1.DataSource = modify.getAllHDLD();

                // Hiển thị thông báo thành công
                MessageBox.Show("Thêm bản ghi thành công.");

                // Xóa dữ liệu đã nhập sau khi thêm thành công
                
                cbx_maCV.SelectedIndex = -1;
            }
            catch (Exception ex)
            {
                // Hiển thị thông báo lỗi nếu có lỗi xảy ra
                MessageBox.Show("Đã xảy ra lỗi: " + ex.Message);
            }
        }
    }
}
