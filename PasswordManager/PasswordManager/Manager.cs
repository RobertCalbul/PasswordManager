using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PasswordManager
{
    class Manager
    {
        public Guid id { get; set; }
        public String username { get; set; }
        public String password { get; set; }
        public String url { get; set; }

        public Manager() 
        { 
        }

        public Manager(Guid id, String username, String password, String url) 
        {
            this.id = id;
            this.username = username;
            this.password = password;
            this.url = url;
        }
    }
}
