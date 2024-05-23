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
            string lcb = txt_luongcb.Text;
            string hsl = txt_hsLuong.Text;
            string hspc = txt_hsphucap.Text;
            int snc;
            if (int.TryParse(this.txt_ngaycong.Text, out snc))
            {
                try
                {
                    string queryCountId = "SELECT COUNT(*) FROM LUONG WHERE MaLuong = @MaLuong";
                    string maluong = DataProvider.Instance.GenerateId(queryCountId, "NV"); 
                    if (string.IsNullOrWhiteSpace(maluong) || string.IsNullOrWhiteSpace(lcb) || string.IsNullOrWhiteSpace(hsl) || string.IsNullOrWhiteSpace(hspc) || snc <= 0)
                    {
                        MessageBox.Show("Vui lòng nhập đủ thông tin.");
                        return;
                    }
                    decimal luongCB, hsLuong, hsPC;
                    if (!decimal.TryParse(lcb, out luongCB) || !decimal.TryParse(hsl, out hsLuong) || !decimal.TryParse(hspc, out hsPC))
                    {
                        MessageBox.Show("Vui lòng nhập đúng định dạng số cho lương cơ bản, hệ số lương và hệ số phụ cấp.");
                        return;
                    }
                    decimal tl = luongCB * hsLuong * snc + hsPC;

                    string query = "INSERT INTO LUONG(MaLuong, LuongCB, HSLuong, HSPC, Songaycong, TongLuong) VALUES (@MaLuong, @LuongCB, @HSLuong, @HSPC, @Songaycong, @TongLuong)";
                    object[] parameter_L = new object[] { maluong, luongCB, hsLuong, hsPC, snc, tl };
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
                MessageBox.Show("Nhập số nguyên cho ngày công.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
            txt_ngaycong.Text = guna2DataGridView1.Rows[i].Cells[4].Value.ToString();
        }

        private void btn_sua_Click(object sender, EventArgs e)
        {
            if (guna2DataGridView1.SelectedRows.Count == 0)
            {
                MessageBox.Show("Vui lòng chọn một dòng để sửa.");
                return;
            }

            string maluong = guna2DataGridView1.SelectedRows[0].Cells["MaLuong"].Value.ToString();
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
                string query = "UPDATE LUONG SET LuongCB = @LuongCB, HSLuong = @HSLuong, HSPC = @HSPC,Songaycong = @Songaycong WHERE MaLuong = @MaLuong";
                object[] parameters = new object[] { lcb, hsl, hspc,snc, maluong };
                DataProvider.Instance.ExcuteNonQuery(query, parameters);
                guna2DataGridView1.DataSource = modify.getAllLUONG();
                MessageBox.Show("Sửa bản ghi thành công.");
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
                string maLuong = guna2DataGridView1.SelectedRows[0].Cells[0].Value.ToString(); 
                string query = "DELETE FROM LUONG WHERE MaLuong = @MaLuong";
                object[] parameters = new object[] { maLuong };
                DataProvider.Instance.ExcuteNonQuery(query, parameters);
                guna2DataGridView1.DataSource = modify.getAllLUONG();
                MessageBox.Show("Xóa bản ghi thành công.");
                txt_hsLuong.Clear();
                txt_hsphucap.Clear();
                txt_luongcb.Clear();
                txt_MaLuong.Clear();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Đã xảy ra lỗi: " + ex.Message);
            }
        }

        private void cbx_bacluong_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
