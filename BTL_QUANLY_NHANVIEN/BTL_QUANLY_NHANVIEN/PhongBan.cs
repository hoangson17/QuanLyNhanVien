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
    public partial class PhongBan : Form
    {
        public PhongBan()
        {
            InitializeComponent();
        }
        Modify modify = new Modify();
        private void PhongBan_Load(object sender, EventArgs e)
        {
            modify = new Modify();
            try
            {
                guna2DataGridView1.DataSource = modify.getAllPHONGBAN();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi" + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btn_them_Click(object sender, EventArgs e)
        {
            string tenPB = txt_tenPB.Text;
            string diachi = txt_diachi.Text;
            string sdt = txt_sdt.Text;
            if (string.IsNullOrWhiteSpace(tenPB) || string.IsNullOrWhiteSpace(diachi) || string.IsNullOrWhiteSpace(sdt))
            {
                MessageBox.Show("Vui lòng nhập đủ thông tin.");
                return;
            }

            try
            {
                string queryCountId = "SELECT COUNT(*) FROM PHONGBAN WHERE MaPB = @MaPB";
                string maPB = DataProvider.Instance.GenerateId(queryCountId, "PB");
                string query = "INSERT INTO PHONGBAN(MaPB, TenPB, SDTPB, Diachi) VALUES (@MaPB, @TenPB, @SDTPB, @Diachi)";
                object[] parameter_PB = new object[] { maPB,tenPB,sdt,diachi };
                DataProvider.Instance.ExcuteNonQuery(query, parameter_PB);
                guna2DataGridView1.DataSource = modify.getAllPHONGBAN();
                MessageBox.Show("Thêm bản ghi thành công.");
                txt_maPB.Clear();
                txt_tenPB.Clear();
                txt_diachi.Clear();
                txt_sdt.Clear();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Đã xảy ra lỗi: " + ex.Message);
            }
        }

        private void guna2DataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            int i;
            i = guna2DataGridView1.CurrentRow.Index;
            txt_maPB.Text = guna2DataGridView1.Rows[i].Cells[0].Value.ToString();
            txt_tenPB.Text = guna2DataGridView1.Rows[i].Cells[1].Value.ToString();
            txt_diachi.Text = guna2DataGridView1.Rows[i].Cells[3].Value.ToString();
            txt_sdt.Text = guna2DataGridView1.Rows[i].Cells[2].Value.ToString();
        }

        private void btn_sua_Click(object sender, EventArgs e)
        {
            if (guna2DataGridView1.SelectedRows.Count == 0)
            {
                MessageBox.Show("Vui lòng chọn một dòng để sửa.");
                return;
            }
            string maPB = guna2DataGridView1.SelectedRows[0].Cells["MaPB"].Value.ToString(); // Giả sử cột MaPB chứa ID
            string tenPB = txt_tenPB.Text;
            string diachi = txt_diachi.Text;
            string sdt = txt_sdt.Text;
            if (string.IsNullOrWhiteSpace(tenPB) || string.IsNullOrWhiteSpace(diachi) || string.IsNullOrWhiteSpace(sdt))
            {
                MessageBox.Show("Vui lòng nhập đủ thông tin.");
                return;
            }

            try
            {
                string query = "UPDATE PHONGBAN SET TenPB = @TenPB, SDTPB = @SDTPB, Diachi = @Diachi WHERE MaPB = @MaPB";
                object[] parameters = new object[] { tenPB, sdt,diachi, maPB };
                DataProvider.Instance.ExcuteNonQuery(query, parameters);
                guna2DataGridView1.DataSource = modify.getAllPHONGBAN();
                MessageBox.Show("Sửa bản ghi thành công.");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Đã xảy ra lỗi: " + ex.Message);
            }
        }

        private void btn_xoa_Click(object sender, EventArgs e)
        {
            if (guna2DataGridView1.SelectedRows.Count == 0)
            {
                MessageBox.Show("Vui lòng chọn một dòng để xóa.");
                return;
            }
            DialogResult result = MessageBox.Show("Bạn có chắc chắn muốn xóa bản ghi này?", "Xác nhận xóa", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.No)
                return;

            try
            {
                string maPB = guna2DataGridView1.SelectedRows[0].Cells["MaPB"].Value.ToString(); // Giả sử cột MaPB chứa ID
                string query = "DELETE FROM PHONGBAN WHERE MaPB = @MaPB";
                object[] parameters = new object[] { maPB };
                DataProvider.Instance.ExcuteNonQuery(query, parameters);
                guna2DataGridView1.DataSource = modify.getAllPHONGBAN();
                MessageBox.Show("Xóa bản ghi thành công.");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Đã xảy ra lỗi: " + ex.Message);
            }
        }
    }
}
