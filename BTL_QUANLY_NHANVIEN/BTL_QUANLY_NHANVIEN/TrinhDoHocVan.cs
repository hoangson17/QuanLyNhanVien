using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BTL_QUANLY_NHANVIEN
{
    public partial class TrinhDoHocVan : Form
    {
        public TrinhDoHocVan()
        {
            InitializeComponent();
        }
        Modify modify = new Modify();
        private void TrinhDoHocVan_Load(object sender, EventArgs e)
        {
            modify = new Modify();
            try
            {
                guna2DataGridView1.DataSource = modify.getAllTDHV();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi" + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void guna2DataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            int i;
            i = guna2DataGridView1.CurrentRow.Index;
            txt_matdhv.Text = guna2DataGridView1.Rows[i].Cells[0].Value.ToString();
            txt_tentdhv.Text = guna2DataGridView1.Rows[i].Cells[1].Value.ToString();
            txt_chuyennganh.Text = guna2DataGridView1.Rows[i].Cells[2].Value.ToString();
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            string tenTDHV = txt_tentdhv.Text;
            string chuyennganh = txt_chuyennganh.Text;
            if (string.IsNullOrWhiteSpace(tenTDHV) || string.IsNullOrWhiteSpace(chuyennganh))
            {
                MessageBox.Show("Vui lòng nhập đủ thông tin.");
                return;
            }

            try
            {
                string queryCountId = "SELECT COUNT(*) FROM TRINHDOHOCVAN WHERE MaTDHV = @MaTDHV";
                string maTDHV = DataProvider.Instance.GenerateId(queryCountId, "TD");
                string query = "INSERT INTO TRINHDOHOCVAN(MaTDHV, TDHV, CNganh) VALUES (@MaTDHV, @TDHV, @CNganh)";
                object[] parameter_TDHV = new object[] { maTDHV, tenTDHV, chuyennganh };
                DataProvider.Instance.ExcuteNonQuery(query, parameter_TDHV);
                guna2DataGridView1.DataSource = modify.getAllTDHV();
                MessageBox.Show("Thêm bản ghi thành công.");
                txt_matdhv.Clear();
                txt_tentdhv.Clear();
                txt_chuyennganh.Clear();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Đã xảy ra lỗi: " + ex.Message);
            }

        }

        private void guna2Button2_Click(object sender, EventArgs e)
        {
            if (guna2DataGridView1.SelectedRows.Count == 0)
            {
                MessageBox.Show("Vui lòng chọn một dòng để sửa.");
                return;
            }
            DataGridViewRow selectedRow = guna2DataGridView1.SelectedRows[0];
            string maTDHV = selectedRow.Cells["MaTDHV"].Value.ToString();
            string tenTDHV = txt_tentdhv.Text;
            string chuyennganh = txt_chuyennganh.Text;

            if (string.IsNullOrWhiteSpace(tenTDHV) || string.IsNullOrWhiteSpace(chuyennganh))
            {
                MessageBox.Show("Vui lòng nhập đủ thông tin.");
                return;
            }

            try
            {
                string query = "UPDATE TRINHDOHOCVAN SET TDHV = @TDHV, CNganh = @CNganh WHERE MaTDHV = @MaTDHV";
                object[] parameters = new object[] { tenTDHV, chuyennganh, maTDHV };
                DataProvider.Instance.ExcuteNonQuery(query, parameters);
                guna2DataGridView1.DataSource = modify.getAllTDHV();
                MessageBox.Show("Sửa bản ghi thành công.");
                txt_matdhv.Clear();
                txt_tentdhv.Clear();
                txt_chuyennganh.Clear();
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
            DialogResult result = MessageBox.Show("Bạn có chắc chắn muốn xóa bản ghi này?", "Xác nhận xóa", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.No)
                return;

            try
            {
                DataGridViewRow selectedRow = guna2DataGridView1.SelectedRows[0];
                string maTDHV = selectedRow.Cells["MaTDHV"].Value.ToString(); 
                string query = "DELETE FROM TRINHDOHOCVAN WHERE MaTDHV = @MaTDHV";
                object[] parameters = new object[] { maTDHV };
                DataProvider.Instance.ExcuteNonQuery(query, parameters);
                guna2DataGridView1.DataSource = modify.getAllTDHV();
                MessageBox.Show("Xóa bản ghi thành công.");
                txt_matdhv.Clear();
                txt_tentdhv.Clear();
                txt_chuyennganh.Clear();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Đã xảy ra lỗi: " + ex.Message);
            }
        }
    }
}
