using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PasswordManager
{
    class Manager
    {
        public int id { get; set; }
        public String username { get; set; }
        public String password { get; set; }
        public String url { get; set; }
        public int login { get; set; }
        public Manager() 
        { 
        }
        public Manager(int id)
        {
            this.id = id;
        }
        public Manager(String username, String password, String url, int login)
        {
            this.username = username;
            this.password = password;
            this.url = url;
            this.login = login;
        }
        public Manager(String username, String password, String url)
        {
            this.username = username;
            this.password = password;
            this.url = url;
        }
        public Manager(int id, String username, String password, String url)
        {
            this.id = id;
            this.username = username;
            this.password = password;
            this.url = url;
        }
        public Manager(int id, String username, String password, String url, int login) 
        {
            this.id = id;
            this.username = username;
            this.password = password;
            this.url = url;
            this.login = login;
        }
        
    }
}
