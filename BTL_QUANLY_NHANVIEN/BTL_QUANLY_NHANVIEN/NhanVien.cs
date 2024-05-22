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
    public partial class NhanVien : Form
    {
        public NhanVien()
        {
            InitializeComponent();
        }
        Modify modify = new Modify();
        DataTable DataUI = new DataTable();

        private void addColumnsName()
        {
            DataUI.Columns.Add("MaNV", typeof(string));
            DataUI.Columns.Add("Hoten", typeof(string));
            DataUI.Columns.Add("SDT", typeof(string));
            DataUI.Columns.Add("Gioitinh", typeof(string));
            DataUI.Columns.Add("NgaySinh", typeof(DateTime));
            DataUI.Columns.Add("Dantoc", typeof(string));
            DataUI.Columns.Add("Quequan", typeof(string));
            DataUI.Columns.Add("Email", typeof(string));
            DataUI.Columns.Add("Taikhoan", typeof(string));
            DataUI.Columns.Add("Matkhau", typeof(string));
            DataUI.Columns.Add("MaCV", typeof(string));
            DataUI.Columns.Add("MaPB", typeof(string));
            DataUI.Columns.Add("MaTDHV", typeof(string));
            DataUI.Columns.Add("MaLuong", typeof(string));
            DataUI.Columns.Add("TongLuong", typeof (float));
        }


        private void loadDataTb()
        {
            String queryNhanVien = "select * from NHANVIEN";
            String queryLuong = "select TongLuong from LUONG";

            DataTable DtNhanVien = DataProvider.Instance.ExcuteQuery(queryNhanVien);
            DataTable DtLuong = DataProvider.Instance.ExcuteQuery(queryLuong);

            for (int i = 0; i < DtNhanVien.Rows.Count; i++)
            {
                DataRow newRow = DataUI.NewRow();
                newRow["MaNV"] = DtNhanVien.Rows[i]["MaNV"];
                newRow["Hoten"] = DtNhanVien.Rows[i]["Hoten"];
                newRow["SDT"] = DtNhanVien.Rows[i]["SDT"];
                newRow["Gioitinh"] = DtNhanVien.Rows[i]["Gioitinh"];
                newRow["NgaySinh"] = DtNhanVien.Rows[i]["NgaySinh"];
                newRow["Dantoc"] = DtNhanVien.Rows[i]["Dantoc"];
                newRow["Quequan"] = DtNhanVien.Rows[i]["Quequan"];
                newRow["Email"] = DtNhanVien.Rows[i]["Email"];
                newRow["Taikhoan"] = DtNhanVien.Rows[i]["Taikhoan"];
                newRow["Matkhau"] = DtNhanVien.Rows[i]["Matkhau"];
                newRow["MaCV"] = DtNhanVien.Rows[i]["MaCV"];
                newRow["MaPB"] = DtNhanVien.Rows[i]["MaPB"];
                newRow["MaTDHV"] = DtNhanVien.Rows[i]["MaTDHV"];
                newRow["MaLuong"] = DtNhanVien.Rows[i]["MaLuong"];
                newRow["TongLuong"] = DtLuong.Rows[i]["TongLuong"];
                DataUI.Rows.Add(newRow);
            }
            
            

            guna2DataGridView1.DataSource = DataUI;
        }


        private void NhanVien_Load(object sender, EventArgs e)
        {
            


            modify = new Modify();
            try
            {
                // guna2DataGridView1.DataSource = modify.getAllNhanvien();
                addColumnsName();
                loadDataTb();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi" + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            
            addDataMcv();
            addDataMTDHV();
            addDatamaluong();
            addDataPB();
        }

        private void addDataMcv()
        {
            string query = "SELECT * FROM CHUCVU ";
            cbx_macv.DataSource = DataProvider.Instance.ExcuteQuery(query);
            cbx_macv.DisplayMember = "MaCV";
            cbx_macv.SelectedIndex = -1;
        }
        private void addDataMTDHV()
        {
            string query = "SELECT * FROM TRINHDOHOCVAN ";
            cbx_tdhv.DataSource = DataProvider.Instance.ExcuteQuery(query);
            cbx_tdhv.DisplayMember = "MaTDHV";
            cbx_tdhv.SelectedIndex = -1;
        }
        private void addDatamaluong()
        {
            string query = "SELECT * FROM LUONG ";
            txt_maluong.DataSource = DataProvider.Instance.ExcuteQuery(query);
            txt_maluong.DisplayMember = "MaLuong";
            txt_maluong.SelectedIndex = -1;
        }
        private void addDataPB()
        {
            string query = "SELECT * FROM PHONGBAN ";
            cbx_mapb.DataSource = DataProvider.Instance.ExcuteQuery(query);
            cbx_mapb.DisplayMember = "MaPB";
            cbx_mapb.SelectedIndex = -1;
        }
        private void guna2Button1_Click(object sender, EventArgs e)
        {
            String tennv = txt_ten.Text;
            String sdt = txt_sdt.Text;
            String gioitinh = txt_gioitinh.Text;
            DateTime ngaysinh = this.ngaysinh.Value;
            String dantoc = txt_dantoc.Text;
            String quequan = txt_que.Text;
            String email = txt_email.Text;
            String tk = txt_tk.Text;
            String mk = txt_mk.Text;
            String mcv = cbx_macv.Text;
            String mpb = cbx_mapb.Text;
            String tdhv = cbx_tdhv.Text;
            String ml = txt_maluong.Text;
            if (string.IsNullOrWhiteSpace(tennv)|| string.IsNullOrWhiteSpace(sdt)|| string.IsNullOrWhiteSpace(gioitinh)|| string.IsNullOrWhiteSpace(dantoc)|| string.IsNullOrWhiteSpace(quequan) || string.IsNullOrWhiteSpace(quequan) || string.IsNullOrWhiteSpace(email)|| string.IsNullOrWhiteSpace(mk)|| string.IsNullOrWhiteSpace(tk))
            {
                MessageBox.Show("Vui lòng nhập đủ thông tin.");
                return;
            }
            try
            {
                string queryCountId = "SELECT COUNT(*) FROM NHANVIEN WHERE MaNV = @MaNV";
                string manv = DataProvider.Instance.GenerateId(queryCountId, "NV");
                string query = "INSERT INTO NHANVIEN(MaNV, Hoten, SDT, Gioitinh, NgaySinh, Dantoc, Quequan, Email, Taikhoan, Matkhau, MaCV, MaPB, MaTDHV, MaLuong) VALUES (@MaNV, @Hoten, @SDT, @Gioitinh, @NgaySinh, @Dantoc, @Quequan, @Email,@Taikhoan, @Matkhau, @MaCV, @MaPB, @MaTDHV, @MaLuong)";
                object[] parameter = new object[] {manv,tennv,sdt,gioitinh,ngaysinh,dantoc,quequan,email,tk,mk,mcv,mpb,tdhv,ml  };
                DataProvider.Instance.ExcuteNonQuery(query, parameter);
                guna2DataGridView1.DataSource = modify.getAllNhanvien();
                MessageBox.Show("Thêm bản ghi thành công.");
                 txt_manv.Clear();
                txt_ten.Clear();
                txt_sdt.Clear();
                txt_gioitinh.Clear();
                this.ngaysinh.Value = DateTime.Now; 
                txt_dantoc.Clear();
                txt_que.Clear();
                txt_email.Clear();
                txt_tk.Clear();
                txt_mk.Clear();
                cbx_macv.SelectedIndex = -1; 
                cbx_mapb.SelectedIndex = -1; 
                cbx_tdhv.SelectedIndex = -1; 
                txt_maluong.SelectedIndex = -1; 
                
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
            txt_manv.Text = guna2DataGridView1.Rows[i].Cells[0].Value.ToString();
            txt_ten.Text = guna2DataGridView1.Rows[i].Cells[1].Value.ToString();
            txt_sdt.Text = guna2DataGridView1.Rows[i].Cells[2].Value.ToString();
            txt_gioitinh.Text = guna2DataGridView1.Rows[i].Cells[3].Value.ToString();
            ngaysinh.Text = guna2DataGridView1.Rows[i].Cells[4].Value.ToString();
            txt_dantoc.Text = guna2DataGridView1.Rows[i].Cells[5].Value.ToString();
            txt_que.Text = guna2DataGridView1.Rows[i].Cells[6].Value.ToString();
            txt_email.Text = guna2DataGridView1.Rows[i].Cells[7].Value.ToString();
            txt_tk.Text = guna2DataGridView1.Rows[i].Cells[8].Value.ToString();

        }
        
        private void guna2Button2_Click(object sender, EventArgs e)
        {
            
            String manv = txt_manv.Text; 
            if (string.IsNullOrEmpty(manv))
            {
                MessageBox.Show("Vui lòng chọn nhân viên để sửa.");
                return;
            }

            String tennv = txt_ten.Text;
            String sdt = txt_sdt.Text;
            String gioitinh = txt_gioitinh.Text;
            DateTime ngaysinh = this.ngaysinh.Value;
            String dantoc = txt_dantoc.Text;
            String quequan = txt_que.Text;
            String email = txt_email.Text;
            String tk = txt_tk.Text;
            String mk = txt_mk.Text;
            String mcv = cbx_macv.Text;
            String mpb = cbx_mapb.Text;
            String tdhv = cbx_tdhv.Text;
            String ml = txt_maluong.Text;

            if (string.IsNullOrWhiteSpace(tennv) || string.IsNullOrWhiteSpace(sdt) || string.IsNullOrWhiteSpace(gioitinh) ||
                string.IsNullOrWhiteSpace(dantoc) || string.IsNullOrWhiteSpace(quequan) || string.IsNullOrWhiteSpace(email) ||
                string.IsNullOrWhiteSpace(tk) || string.IsNullOrWhiteSpace(mk))
            {
                MessageBox.Show("Vui lòng nhập đủ thông tin.");
                return;
            }

            try
            {
                string query = "UPDATE NHANVIEN SET Hoten = @Hoten, SDT = @SDT, Gioitinh = @Gioitinh, NgaySinh = @NgaySinh, " +
                               "Dantoc = @Dantoc, Quequan = @Quequan, Email = @Email, Taikhoan = @Taikhoan, Matkhau = @Matkhau, " +
                               "MaCV = @MaCV, MaPB = @MaPB, MaTDHV = @MaTDHV, MaLuong = @MaLuong WHERE MaNV = @MaNV";
                object[] parameters = { tennv, sdt, gioitinh, ngaysinh, dantoc, quequan, email, tk, mk, mcv, mpb, tdhv, ml, manv };
                DataProvider.Instance.ExcuteNonQuery(query, parameters);

                guna2DataGridView1.DataSource = modify.getAllNhanvien();
                MessageBox.Show("Cập nhật bản ghi thành công.");
                txt_manv.Clear();
                txt_ten.Clear();
                txt_sdt.Clear();
                txt_gioitinh.Clear();
                this.ngaysinh.Value = DateTime.Now;
                txt_dantoc.Clear();
                txt_que.Clear();
                txt_email.Clear();
                txt_tk.Clear();
                txt_mk.Clear();
                cbx_macv.SelectedIndex = -1;
                cbx_mapb.SelectedIndex = -1;
                cbx_tdhv.SelectedIndex = -1;
                txt_maluong.SelectedIndex = -1;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Đã xảy ra lỗi: " + ex.Message);
            }
        }

        private void guna2Button3_Click(object sender, EventArgs e)
        {
            String manv = txt_manv.Text; 

            if (string.IsNullOrEmpty(manv))
            {
                MessageBox.Show("Vui lòng chọn nhân viên để xóa.");
                return;
            }
            DialogResult dialogResult = MessageBox.Show("Bạn có chắc chắn muốn xóa nhân viên này?", "Xác nhận xóa", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.No)
            {
                return;
            }

            try
            {
                string query = "DELETE FROM NHANVIEN WHERE MaNV = @MaNV";
                object[] parameters = { manv };
                DataProvider.Instance.ExcuteNonQuery(query, parameters);
                guna2DataGridView1.DataSource = modify.getAllNhanvien();
                MessageBox.Show("Xóa bản ghi thành công.");
                txt_manv.Clear();
                txt_ten.Clear();
                txt_sdt.Clear();
                txt_gioitinh.Clear();
                ngaysinh.Value = DateTime.Now; 
                txt_dantoc.Clear();
                txt_que.Clear();
                txt_email.Clear();
                txt_tk.Clear();
                txt_mk.Clear();
                cbx_macv.SelectedIndex = -1; 
                cbx_mapb.SelectedIndex = -1; 
                cbx_tdhv.SelectedIndex = -1; 
                txt_maluong.SelectedIndex = -1; 
            }
            catch (Exception ex)
            {
                MessageBox.Show("Đã xảy ra lỗi: " + ex.Message);
            }
        }
    }
}
