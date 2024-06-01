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
    public partial class doimk : Form
    {
        Modify modify = new Modify();
        public doimk()
        {
            InitializeComponent();
        }

        private void guna2TextBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void doimk_Load(object sender, EventArgs e)
        {

        }

        private void guna2Button2_Click(object sender, EventArgs e)
        {
            Close();
            Login login = new Login();
            login.Visible = true;
        }

        public bool CheckAccount(string ac)//check mat khau va ten tai khoan
        {
            return Regex.IsMatch(ac, "^[a-zA-Z0-9]{6,24}$");
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            string tentk = txt_tk.Text;
            string oldMatkhau = txt_mkc.Text; 
            string newMatkhau = txt_mkm.Text; 
            string xnmatkhau = txt_xnmk.Text;   
            string email = txt_email.Text;

            if (!CheckAccount(newMatkhau))
            {
                MessageBox.Show("Vui lòng nhập mật khẩu dài 6-24 ký tự, với các ký tự chữ và số, chữ hoa và chữ thường !");
                return;
            }
            if (xnmatkhau != newMatkhau)
            {
                MessageBox.Show("Vui lòng xác nhận mật khẩu chính xác !!");
                return;
            }
            if (modify.TaiKhoans("Select * from DANGNHAP where Email = '" + email + "' ").Count == 0)
            {
                MessageBox.Show("Email này chưa được đăng ký !!!");
                return;
            }

            var account = modify.TaiKhoans("Select * from DANGNHAP where Email = '" + email + "' AND MatKhau = '" + oldMatkhau + "'");
            if (account.Count == 0)
            {
                MessageBox.Show("Mật khẩu cũ không chính xác !!!");
                return;
            }

            try
            {
                string query = "UPDATE DANGNHAP SET MatKhau = @NewMatKhau WHERE Email = @Email AND TaiKhoan = @TaiKhoan AND MatKhau = @OldMatKhau";
                object[] parameter = new object[] { newMatkhau, email, tentk, oldMatkhau };
                DataProvider.Instance.ExcuteNonQuery(query, parameter);
                MessageBox.Show("Đổi mật khẩu thành công !!! ");
                guna2Button2_Click(sender, e);
            }
            catch
            {
                MessageBox.Show("Đổi mật khẩu thất bại !!! ");
            }
        }
    }
}
