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
                string queryCountId = "SELECT COUNT(*) FROM HOPDONGLAODONG WHERE MaHD = @MaHD";
                string maHD = DataProvider.Instance.GenerateId(queryCountId, "HD");
                string query = "INSERT INTO HOPDONGLAODONG(MaHD,MaNV, Ngayvao, Ngayra) VALUES (@MaHD,@MaNV, @Ngayvao, @Ngayra)";
                object[] parameters = new object[] { maHD,maNV,nv,nr };
                DataProvider.Instance.ExcuteNonQuery(query, parameters);
                guna2DataGridView1.DataSource = modify.getAllHDLD();
                MessageBox.Show("Thêm bản ghi thành công.");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Đã xảy ra lỗi: " + ex.Message);
            }
        }

        private void btn_sua_Click(object sender, EventArgs e)
        {
            if (guna2DataGridView1.SelectedRows.Count == 0)
            {
                MessageBox.Show("Vui lòng chọn một dòng để sửa.");
                return;
            }
            string maHD = guna2DataGridView1.SelectedRows[0].Cells["MaHD"].Value.ToString();
            string maNV = txt_manv.Text;
            DateTime ngayVao = dt_ngayvao.Value;
            DateTime ngayRa = dt_ngayra.Value;
            if (string.IsNullOrWhiteSpace(maNV))
            {
                MessageBox.Show("Vui lòng nhập đủ thông tin.");
                return;
            }

            try
            {
                string query = "UPDATE HOPDONGLAODONG SET MaNV = @MaNV, Ngayvao = @Ngayvao, Ngayra = @Ngayra WHERE MaHD = @MaHD";
                object[] parameters = new object[] { maNV, ngayVao, ngayRa, maHD };
                DataProvider.Instance.ExcuteNonQuery(query, parameters);
                guna2DataGridView1.DataSource = modify.getAllHDLD();
                MessageBox.Show("Sửa bản ghi thành công.");
                txt_manv.Clear();
                dt_ngayvao.Value = DateTime.Now; 
                dt_ngayra.Value = DateTime.Now; 
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
                string maHD = guna2DataGridView1.SelectedRows[0].Cells["MaHD"].Value.ToString();
                string query = "DELETE FROM HOPDONGLAODONG WHERE MaHD = @MaHD";
                object[] parameters = new object[] { maHD };
                DataProvider.Instance.ExcuteNonQuery(query, parameters);
                guna2DataGridView1.DataSource = modify.getAllHDLD();
                MessageBox.Show("Xóa bản ghi thành công.");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Đã xảy ra lỗi: " + ex.Message);
            }
        }
    }
}
