using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Net.WebRequestMethods;

namespace BTL_QUANLY_NHANVIEN
{
    public partial class Forgot_password : Form
    {
        public Forgot_password()
        {
            InitializeComponent();
        }


        private void btn_thoat_Click(object sender, EventArgs e)
        {
            Close();
            Login login = new Login();
            login.Visible = true;
        }
        Modify modify = new Modify();
        Random random = new Random();
        int otp;
        private void btn_layMK_Click(object sender, EventArgs e)
        {
            if (otp.ToString().Equals(txt_otp.Text))
            {
                MessageBox.Show("Xác Minh Thành Công");

                string email = txt_email.Text;
                string query = "SELECT * FROM DANGNHAP WHERE Email = '" + email + "'";
                List<loginClass> taiKhoans = modify.TaiKhoans(query);

                if (taiKhoans.Count != 0)
                {
                    label4.ForeColor = Color.Black;
                    label4.Text = "Mật Khẩu  : " + taiKhoans[0].Password;
                }
                else
                {
                    MessageBox.Show("Không tìm thấy tài khoản với địa chỉ email này!");
                }
            }
            else
            {
                MessageBox.Show("OTP Không chính xác !!!!");
            }
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            //tạo mã radom 6 chữ số
            try
            {
                // Tạo một OTP ngẫu nhiên có 6 chữ số
                otp = random.Next(100000, 1000000);

                // Thiết lập email
                var fromAddress = new MailAddress("demodoan638@gmail.com");
                var toAddress = new MailAddress(txt_email.Text);
                const string frompass = "rcsw vpod vtjn uhbr";
                const string subject = "Mã OTP";
                string body = otp.ToString();

                // Thiết lập SMTP
                var smtp = new SmtpClient()
                {
                    Host = "smtp.gmail.com",
                    Port = 587,
                    EnableSsl = true,
                    DeliveryMethod = SmtpDeliveryMethod.Network,
                    UseDefaultCredentials = false,
                    Credentials = new NetworkCredential(fromAddress.Address, frompass),
                    Timeout = 10000 // Điều chỉnh thời gian chờ theo nhu cầu
                };

                // Gửi email
                using (var message = new MailMessage(fromAddress, toAddress)
                {
                    Subject = subject,
                    Body = body
                })
                {
                    smtp.Send(message);
                }

                // Cung cấp phản hồi cho người dùng
                MessageBox.Show("Mã OTP đã được gửi qua email");
            }
            catch (Exception ex)
            {
                // Ghi log về ngoại lệ cụ thể để hỗ trợ gỡ lỗi
                MessageBox.Show($"Lỗi: {ex.Message}");
            }
            
        }

        private void Forgot_password_Load(object sender, EventArgs e)
        {

        }

        private void guna2Button2_Click(object sender, EventArgs e)
        {
            Close();
            doimk dmk = new doimk();
            dmk.ShowDialog();
        }

    }
}
