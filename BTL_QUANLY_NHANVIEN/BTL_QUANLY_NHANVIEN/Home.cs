using Guna.UI2.WinForms;
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
    public partial class Home : Form
    {
        public Home()
        {
            InitializeComponent();
        }
        private Form currenFormChild;
        Modify modify  = new Modify();
        private void OpenChildForm(Form childForm)
        {
            if (currenFormChild != null)
            {
                currenFormChild.Close();
            }
            currenFormChild = childForm;
            childForm.TopLevel = false;
            childForm.FormBorderStyle = FormBorderStyle.None;
            childForm.Dock = DockStyle.Fill;
            guna2Panel3.Controls.Add(currenFormChild);
            guna2Panel3.Tag = childForm;
            childForm.BringToFront();
            childForm.Show();
        }

        private void btn_chucvu_Click(object sender, EventArgs e)
        {
            OpenChildForm(new Chucvu());
            txt_hienthi.Text = btn_chucvu.Text;
        }

        private void btn_nhanvien_Click(object sender, EventArgs e)
        {
            OpenChildForm(new NhanVien());
            txt_hienthi.Text = btn_nhanvien.Text;
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            if (currenFormChild != null)
            {
                currenFormChild.Close();
            }
            txt_hienthi.Text = "Tìm Kiếm";
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            Login login = new Login();
            login.Visible = true;
            Close();
        }

        private void btn_hopdong_Click(object sender, EventArgs e)
        {
            OpenChildForm(new HopDongLaoDong());
            txt_hienthi.Text = btn_hopdong.Text;
        }

        private void btn_luong_Click(object sender, EventArgs e)
        {
            OpenChildForm(new Luong());
            txt_hienthi.Text = btn_luong.Text;
        }

        private void btn_phongban_Click(object sender, EventArgs e)
        {
            OpenChildForm(new PhongBan());
            txt_hienthi.Text = btn_phongban.Text;
        }

        private void guna2GradientButton13_Click(object sender, EventArgs e)
        {
            OpenChildForm(new TrinhDoHocVan());
            txt_hienthi.Text = guna2GradientButton13.Text;
        }

        private void cbx_timkiem_SelectedIndexChanged(object sender, EventArgs e)
        {
           
        }

        private void guna2Panel3_Paint(object sender, PaintEventArgs e)
        {

        }

        private void Home_Load(object sender, EventArgs e)
        {
            cbx_timkiem.Text = "Nhân Viên";
        }

        private void btn_timkiem_Click(object sender, EventArgs e)
        {
            if (cbx_timkiem.Text == "Nhân Viên")
            {
                data.DataSource = modify.XemDL("select * from NHANVIEN where MaNV like '%" + txt_timkiem.Text.Trim() + "%'");
            }
            if (cbx_timkiem.Text == "Chức Vụ")
            {
                data.DataSource = modify.XemDL("select * from CHUCVU where MaCV like '%" + txt_timkiem.Text.Trim() + "%'");
            }
            if (cbx_timkiem.Text == "Hợp Đồng")
            {
                data.DataSource = modify.XemDL("select * from HOPDONGLAODONG where MaHD like '%" + txt_timkiem.Text.Trim() + "%'");
            }
            if (cbx_timkiem.Text == "Lương")
            {
                data.DataSource = modify.XemDL("select * from LUONG where MaLuong like '%" + txt_timkiem.Text.Trim() + "%'");
            }
            if (cbx_timkiem.Text == "Phòng Ban")
            {
                data.DataSource = modify.XemDL("select * from PHONGBAN where MaPB like '%" + txt_timkiem.Text.Trim() + "%'");
            }
            if (cbx_timkiem.Text == "Trình Độ Học vấn")
            {
                data.DataSource = modify.XemDL("select * from TRINHDOHOCVAN where MaTDHV like '%" + txt_timkiem.Text.Trim() + "%'");
            }
        }

        private void data_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void guna2Panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void txt_timkiem_TextChanged(object sender, EventArgs e)
        {

        }

        private void btn_capTK_Click(object sender, EventArgs e)
        {
            this.Visible = false;
            Register rg = new Register();
            rg.ShowDialog();
        }


        private void guna2GradientButton1_Click(object sender, EventArgs e)
        {
            this.Visible = false;
            doimk dmk = new doimk();
            dmk.ShowDialog();
        }
    }
}
