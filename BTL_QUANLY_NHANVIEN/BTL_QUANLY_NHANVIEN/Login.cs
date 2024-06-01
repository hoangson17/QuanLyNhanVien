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
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
        }

        private void Login_Load(object sender, EventArgs e)
        {
            txt_user.Text = Properties.Settings.Default.luuTK;
            txt_password.Text = Properties.Settings.Default.luuMK;
            if (!string.IsNullOrEmpty(Properties.Settings.Default.luuMK))
            {
                cb_luu_mk.Checked = true;
            }
        }

        private void btn_thoat_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
        Modify modify = new Modify();


        private void linkLabel_QuenMK_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            this.Visible = false;
            Forgot_password fP = new Forgot_password();
            fP.ShowDialog();
        }

        private void cb_luu_mk_CheckedChanged(object sender, EventArgs e)
        {
            if (txt_user.Text != "" && txt_password.Text != "")
            {
                if (cb_luu_mk.Checked)
                {
                    string tk = txt_user.Text;
                    string mk = txt_password.Text;

                    // Save the username and password to application settings
                    Properties.Settings.Default.luuTK = tk;
                    Properties.Settings.Default.luuMK = mk;
                    Properties.Settings.Default.Save();
                }
            }
        }

        private void btn_login_Click(object sender, EventArgs e)
        {
            string tentk = txt_user.Text;
            string matkhau = txt_password.Text;
            if (tentk.Trim() == "") { MessageBox.Show("Vui lòng nhập tài khoản !!"); }
            else if (matkhau.Trim() == "") { MessageBox.Show("Vui lòng nhập mật khẩu !!"); }
            else
            {
                string query = "Select * from DANGNHAP where TaiKhoan = '" + tentk + "' and MatKhau = '" + matkhau + "'";
                if (modify.TaiKhoans(query).Count != 0)
                {
                    this.Visible = false;
                    Home home = new Home();
                    home.ShowDialog();
                    

                }
                else
                {
                    MessageBox.Show("Tên tài khoản hoặc mật khẩu không chính xác !!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            
        }

        private void cb_hien_mk_CheckedChanged(object sender, EventArgs e)
        {
            if (cb_hien_mk.Checked)
            {
                txt_password.PasswordChar = '\0';
            }
            else
            {
                txt_password.PasswordChar = '*';
            }
        }
    }
}
