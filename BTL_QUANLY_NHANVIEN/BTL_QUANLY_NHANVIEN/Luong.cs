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
    public partial class Luong : Form
    {
        public Luong()
        {
            InitializeComponent();
        }
        Modify modify = new Modify();
        private void Luong_Load(object sender, EventArgs e)
        {
            modify = new Modify();
            try
            {
                guna2DataGridView1.DataSource = modify.getAllLUONG();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi" + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btn_them_Click(object sender, EventArgs e)
        {
            string maluong = txt_MaLuong.Text;
            string lcb = txt_luongcb.Text;
            string hsl = txt_hsLuong.Text;
            string hspc = txt_hsphucap.Text;
            int snc;
            if(int.TryParse(this.txt_ngaycong.Text, out snc))
            {
                try
                {
                    if (string.IsNullOrWhiteSpace(maluong) || string.IsNullOrWhiteSpace(lcb) || string.IsNullOrWhiteSpace(hsl) || string.IsNullOrWhiteSpace(hspc) || string.IsNullOrWhiteSpace(snc.ToString()))
                    {
                        MessageBox.Show("Vui lòng nhập đủ thông tin.");
                        return;
                    }
                    string query = "INSERT INTO LUONG(MaLuong, LuongCB, HSLuong, HSPC,Songaycong) VALUES (MaLuong, @LuongCB, @HSLuong, @HSPC,@Songaycong)";
                    object[] parameter_L = new object[] { maluong, lcb, hsl, hspc, snc };
                    DataProvider.Instance.ExcuteNonQuery(query, parameter_L);
                    guna2DataGridView1.DataSource = modify.getAllLUONG();
                    MessageBox.Show("Thêm bản ghi thành công.");
                    txt_hsLuong.Clear();
                    txt_hsphucap.Clear();
                    txt_luongcb.Clear();
                    txt_ngaycong.Clear();
                    txt_MaLuong.Clear();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Đã xảy ra lỗi: " + ex.Message);
                }
            }
            else
            {
                MessageBox.Show("Lỗi " + "Nhập số nguyên cho ngày công ", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }


        }



        private void guna2DataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            int i;
            i = guna2DataGridView1.CurrentRow.Index;
            txt_MaLuong.Text = guna2DataGridView1.Rows[i].Cells[0].Value.ToString();
            txt_luongcb.Text = guna2DataGridView1.Rows[i].Cells[1].Value.ToString();
            txt_hsLuong.Text = guna2DataGridView1.Rows[i].Cells[2].Value.ToString();
            txt_hsphucap.Text = guna2DataGridView1.Rows[i].Cells[3].Value.ToString();
        }

        private void btn_sua_Click(object sender, EventArgs e)
        {
            if (guna2DataGridView1.SelectedRows.Count == 0)
            {
                MessageBox.Show("Vui lòng chọn một dòng để sửa.");
                return;
            }

            string maluong = guna2DataGridView1.SelectedRows[0].Cells["Bacluong"].Value.ToString();
            string lcb = txt_luongcb.Text;
            string hsl = txt_hsLuong.Text;
            string hspc = txt_hsphucap.Text;
            int snc = Int32.Parse(txt_ngaycong.Text);
            if (string.IsNullOrWhiteSpace(maluong) || string.IsNullOrWhiteSpace(lcb) || string.IsNullOrWhiteSpace(hsl) || string.IsNullOrWhiteSpace(hspc)|| string.IsNullOrWhiteSpace(snc.ToString()))
            {
                MessageBox.Show("Vui lòng nhập đủ thông tin.");
                return;
            }

            try
            {
                // Thực hiện câu truy vấn UPDATE
                string query = "UPDATE LUONG SET LuongCB = @LuongCB, HSLuong = @HSLuong, HSPC = @HSPC,Songaycong = @Songaycong WHERE Bacluong = @MacLuong";
                object[] parameters = new object[] { lcb, hsl, hspc,snc, maluong };
                DataProvider.Instance.ExcuteNonQuery(query, parameters);

                // Cập nhật DataGridView với dữ liệu mới
                guna2DataGridView1.DataSource = modify.getAllLUONG();

                // Hiển thị thông báo sửa thành công
                MessageBox.Show("Sửa bản ghi thành công.");
                txt_hsLuong.Clear();
                txt_hsphucap.Clear();
                txt_luongcb.Clear();
                txt_ngaycong.Clear();
                txt_MaLuong.Clear();
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
                string maLuong = guna2DataGridView1.SelectedRows[0].Cells[0].Value.ToString(); 

                // Thực hiện câu truy vấn DELETE
                string query = "DELETE FROM LUONG WHERE Bacluong = @Bacluong";
                object[] parameters = new object[] { maLuong };
                DataProvider.Instance.ExcuteNonQuery(query, parameters);

                // Cập nhật DataGridView với dữ liệu mới
                guna2DataGridView1.DataSource = modify.getAllLUONG();

                // Hiển thị thông báo xóa thành công
                MessageBox.Show("Xóa bản ghi thành công.");
                txt_hsLuong.Clear();
                txt_hsphucap.Clear();
                txt_luongcb.Clear();
                txt_MaLuong.Clear();
            }
            catch (Exception ex)
            {
                // Hiển thị thông báo lỗi nếu có lỗi xảy ra
                MessageBox.Show("Đã xảy ra lỗi: " + ex.Message);
            }
        }

        private void cbx_bacluong_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
