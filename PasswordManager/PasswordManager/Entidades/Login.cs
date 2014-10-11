using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PasswordManager
{
    class Login
    {
        public Guid id { get; set; }
        public String username { get; set; }
        public String password { get; set; }

        public Login() { }

        public Login(Guid id, String username, String password)
        {
            this.id = id;
            this.username = username;
            this.password = password;
        }
        public Login(String username, String password)
        {
            this.username = username;
            this.password = password;
        }
    }
}
