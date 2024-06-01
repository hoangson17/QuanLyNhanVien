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
    public partial class Register : Form
    {
        public Register()
        {
            InitializeComponent();
        }

        private void Register_Load(object sender, EventArgs e)
        {

        }
        public bool CheckAdmin(string ad)//check hoten
        {
            return Regex.IsMatch(ad, @"^[a-zA-Z0-9À-ỹ ]{5,24}$");
        }

        public bool CheckMa(string ma)//check manv
        {
            return Regex.IsMatch(ma, "^[a-zA-Z0-9]{3,24}$");
        }

        public bool CheckAccount(string ac)//check mat khau va ten tai khoan
        {
            return Regex.IsMatch(ac, "^[a-zA-Z0-9]{6,24}$");
        }
        public bool CheckEmail(string em)//check Email
        {
            return Regex.IsMatch(em, @"^[a-zA-Z0-9_.]{3,20}@gmail.com(.vn|)$");
        }
        Modify modify = new Modify();

        private void btn_thoat_Click(object sender, EventArgs e)
        {
            Close();
            Login login = new Login();
            login.Visible = true;
        }

        private void btn_dk_Click(object sender, EventArgs e)
        {
            string tentk = txt_user.Text;
            string matkhau = txt_MK.Text;
            string xnmatkhau = txt_XNMK.Text;
            string email = txt_email.Text;  
            //if (!CheckAdmin(hoten)) { MessageBox.Show("Vui lòng nhập tên dài 10-24 ký tự, với các ký tự chữ hoa và chữ thường !"); return; }
            if (!CheckAccount(tentk)) { MessageBox.Show("Vui lòng nhập tên tài khoản dài 6-24 ký tự, với các ký tự chữ và số, chữ hoa và chữ thường !"); return; }
            if (!CheckAccount(matkhau)) { MessageBox.Show("Vui lòng nhập mật khẩu dài 6-24 ký tự, với các ký tự chữ và số, chữ hoa và chữ thường !"); return; }
            if (xnmatkhau != matkhau) { MessageBox.Show("Vui lòng xác nhận mật khẩu chính xác !!"); return; }
            if (!CheckEmail(email)) { MessageBox.Show("Vui lòng nhập đúng định dạng Email !!"); return; }
            if (modify.TaiKhoans("Select * from DANGNHAP where Email = '" + email + "' ").Count != 0)
            {
                MessageBox.Show("Email này đã được đăng ký vui lòng sử dụng Email khác !!!");
                return;
            }
            try
            {
                string query = "INSERT INTO DANGNHAP(TaiKhoan,MatKhau,Email) VALUES ( @TaiKhoan, @MatKhau,@Email)";
                object[] parameter = new object[] {tentk,matkhau, email};
                DataProvider.Instance.ExcuteNonQuery(query, parameter);
                MessageBox.Show("Đăng kí thành công !!! ");
                btn_thoat_Click(sender, e);

            }
            catch
            {
                MessageBox.Show("Tài khoản đã được đăng kí !!! ");
            }
        }
    }
}
