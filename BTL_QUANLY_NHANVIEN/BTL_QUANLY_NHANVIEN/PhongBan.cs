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
            // Lấy dữ liệu từ các ô văn bản
            string tenPB = txt_tenPB.Text;
            string diachi = txt_diachi.Text;
            string sdt = txt_sdt.Text;

            // Kiểm tra dữ liệu có hợp lệ không (ví dụ: không rỗng)
            if (string.IsNullOrWhiteSpace(tenPB) || string.IsNullOrWhiteSpace(diachi) || string.IsNullOrWhiteSpace(sdt))
            {
                MessageBox.Show("Vui lòng nhập đủ thông tin.");
                return;
            }

            try
            {
                // Tạo ID mới cho bản ghi
                string queryCountId = "SELECT COUNT(*) FROM PHONGBAN WHERE MaPB = @MaPB";
                string maPB = DataProvider.Instance.GenerateId(queryCountId, "PB");

                // Thực hiện câu truy vấn INSERT
                string query = "INSERT INTO PHONGBAN(MaPB, TenPB, DiachiPB, SDTPB) VALUES (@MaPB, @TenPB, @DiachiPB, @SDTPB)";
                object[] parameter_PB = new object[] { maPB,tenPB,diachi,sdt };
                DataProvider.Instance.ExcuteNonQuery(query, parameter_PB);

                // Cập nhật DataGridView với dữ liệu mới
                guna2DataGridView1.DataSource = modify.getAllPHONGBAN();

                // Hiển thị thông báo thành công
                MessageBox.Show("Thêm bản ghi thành công.");
                txt_maPB.Clear();
                txt_tenPB.Clear();
                txt_diachi.Clear();
                txt_sdt.Clear();
            }
            catch (Exception ex)
            {
                // Hiển thị thông báo lỗi nếu có lỗi xảy ra
                MessageBox.Show("Đã xảy ra lỗi: " + ex.Message);
            }
        }

        private void guna2DataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            int i;
            i = guna2DataGridView1.CurrentRow.Index;
            txt_maPB.Text = guna2DataGridView1.Rows[i].Cells[0].Value.ToString();
            txt_tenPB.Text = guna2DataGridView1.Rows[i].Cells[1].Value.ToString();
            txt_diachi.Text = guna2DataGridView1.Rows[i].Cells[2].Value.ToString();
            txt_sdt.Text = guna2DataGridView1.Rows[i].Cells[3].Value.ToString();
        }

        private void btn_sua_Click(object sender, EventArgs e)
        {
            // Kiểm tra xem người dùng đã chọn một dòng để sửa chưa
            if (guna2DataGridView1.SelectedRows.Count == 0)
            {
                MessageBox.Show("Vui lòng chọn một dòng để sửa.");
                return;
            }

            // Lấy ID của dòng được chọn từ DataGridView
            string maPB = guna2DataGridView1.SelectedRows[0].Cells["MaPB"].Value.ToString(); // Giả sử cột MaPB chứa ID

            // Lấy thông tin mới từ các ô văn bản
            string tenPB = txt_tenPB.Text;
            string diachi = txt_diachi.Text;
            string sdt = txt_sdt.Text;

            // Kiểm tra dữ liệu có hợp lệ không (ví dụ: không rỗng)
            if (string.IsNullOrWhiteSpace(tenPB) || string.IsNullOrWhiteSpace(diachi) || string.IsNullOrWhiteSpace(sdt))
            {
                MessageBox.Show("Vui lòng nhập đủ thông tin.");
                return;
            }

            try
            {
                // Thực hiện câu truy vấn UPDATE
                string query = "UPDATE PHONGBAN SET TenPB = @TenPB, DiachiPB = @DiachiPB, SDTPB = @SDTPB WHERE MaPB = @MaPB";
                object[] parameters = new object[] { tenPB, diachi, sdt, maPB };
                DataProvider.Instance.ExcuteNonQuery(query, parameters);

                // Cập nhật DataGridView với dữ liệu mới
                guna2DataGridView1.DataSource = modify.getAllPHONGBAN();

                // Hiển thị thông báo sửa thành công
                MessageBox.Show("Sửa bản ghi thành công.");
            }
            catch (Exception ex)
            {
                // Hiển thị thông báo lỗi nếu có lỗi xảy ra
                MessageBox.Show("Đã xảy ra lỗi: " + ex.Message);
            }
        }

        private void btn_xoa_Click(object sender, EventArgs e)
        {
            // Kiểm tra xem người dùng đã chọn một dòng để xóa chưa
            if (guna2DataGridView1.SelectedRows.Count == 0)
            {
                MessageBox.Show("Vui lòng chọn một dòng để xóa.");
                return;
            }

            // Hiển thị hộp thoại xác nhận xóa
            DialogResult result = MessageBox.Show("Bạn có chắc chắn muốn xóa bản ghi này?", "Xác nhận xóa", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.No)
                return;

            try
            {
                // Lấy ID của bản ghi được chọn từ DataGridView
                string maPB = guna2DataGridView1.SelectedRows[0].Cells["MaPB"].Value.ToString(); // Giả sử cột MaPB chứa ID

                // Thực hiện câu truy vấn DELETE
                string query = "DELETE FROM PHONGBAN WHERE MaPB = @MaPB";
                object[] parameters = new object[] { maPB };
                DataProvider.Instance.ExcuteNonQuery(query, parameters);

                // Cập nhật DataGridView với dữ liệu mới
                guna2DataGridView1.DataSource = modify.getAllPHONGBAN();

                // Hiển thị thông báo xóa thành công
                MessageBox.Show("Xóa bản ghi thành công.");
            }
            catch (Exception ex)
            {
                // Hiển thị thông báo lỗi nếu có lỗi xảy ra
                MessageBox.Show("Đã xảy ra lỗi: " + ex.Message);
            }
        }
    }
}
