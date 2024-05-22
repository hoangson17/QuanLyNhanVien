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
    public partial class Chucvu : Form
    {
        public Chucvu()
        {
            InitializeComponent();
        }
        Modify modify = new Modify();
        private void Chucvu_Load(object sender, EventArgs e)
        {
            modify = new Modify();
            try
            {
                guna2DataGridView1.DataSource = modify.getAllCHUCVU();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi" + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            string tencv = txt_tencv.Text;

            if (string.IsNullOrWhiteSpace(tencv))
            {
                MessageBox.Show("Vui lòng nhập đủ thông tin.");
                return;
            }
            try
            {
                string queryCountId = "SELECT COUNT(*) FROM CHUCVU WHERE MaCV = @MaCV";
                string macv = DataProvider.Instance.GenerateId(queryCountId, "CV");
                string query = "INSERT INTO CHUCVU(MaCV,TenCV) VALUES (@MaCV,@TenCV)";
                object[] parameter = new object[] {macv,tencv  };
                DataProvider.Instance.ExcuteNonQuery(query, parameter);
                guna2DataGridView1.DataSource = modify.getAllCHUCVU();
                MessageBox.Show("Thêm bản ghi thành công.");
                txt_macv.Clear();
                txt_tencv.Clear();
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
            txt_macv.Text = guna2DataGridView1.Rows[i].Cells[0].Value.ToString();
            txt_tencv.Text = guna2DataGridView1.Rows[i].Cells[1].Value.ToString();
            
        }

        private void guna2Button2_Click(object sender, EventArgs e)
        {
            if (guna2DataGridView1.SelectedRows.Count == 0)
            {
                MessageBox.Show("Vui lòng chọn một dòng để sửa.");
                return;
            }

            try
            {

                DataGridViewRow selectedRow = guna2DataGridView1.SelectedRows[0];
                string maCV = selectedRow.Cells["MaCV"].Value.ToString(); 
                string tencv = txt_tencv.Text; 
                if (string.IsNullOrWhiteSpace(tencv))
                {
                    MessageBox.Show("Vui lòng nhập đủ thông tin.");
                    return;
                }
                string query = "UPDATE CHUCVU SET TenCV = @TenCV WHERE MaCV = @MaCV";
                object[] parameters = new object[] { tencv, maCV };
                DataProvider.Instance.ExcuteNonQuery(query, parameters);
                guna2DataGridView1.DataSource = modify.getAllCHUCVU();
                MessageBox.Show("Sửa bản ghi thành công.");
                txt_macv.Clear();
                txt_tencv.Clear();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Đã xảy ra lỗi: " + ex.Message);
            }
        }

        private void guna2Button3_Click(object sender, EventArgs e)
        {
          
            if (guna2DataGridView1.SelectedRows.Count == 0)
            {
                MessageBox.Show("Vui lòng chọn một dòng để xóa.");
                return;
            }

            try
            {
                DataGridViewRow selectedRow = guna2DataGridView1.SelectedRows[0];
                string maCV = selectedRow.Cells["MaCV"].Value.ToString();
                string query = "DELETE FROM CHUCVU WHERE MaCV = @MaCV";
                object[] parameters = new object[] { maCV };
                DataProvider.Instance.ExcuteNonQuery(query, parameters);
                guna2DataGridView1.DataSource = modify.getAllCHUCVU();
                MessageBox.Show("Xóa bản ghi thành công.");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Đã xảy ra lỗi: " + ex.Message);
            }
        }
    }
}
