using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BTL_QUANLY_NHANVIEN
{
    internal class loginClass
    {
        private string tenTK;
        private string password;

        public loginClass()
        {
        }

        public loginClass(string tenTK, string password)
        {
            this.TenTK = tenTK;
            this.Password = password;
        }

        public string TenTK { get => tenTK; set => tenTK = value; }
        public string Password { get => password; set => password = value; }
    }
}
