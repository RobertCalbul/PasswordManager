using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace PasswordManager
{
    class controller_manager
    {
        public Guid id { get; set; }
        public String username { get; set; }
        public String password { get; set; }
        public String url { get; set; }
        public Login login { get; set; }

        public controller_manager(){}

        public controller_manager(Guid id, String username, String password, String url, Login login)
        {
            this.id = id;
            this.username = username;
            this.password = password;
            this.url = url;
            this.login = login;
        }
    }
}
