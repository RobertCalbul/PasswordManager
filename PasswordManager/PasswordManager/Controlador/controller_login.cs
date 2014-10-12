using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;
using System.Windows;

namespace PasswordManager
{
    class controller_login
    {


        public int agregar(Login login)
        {
           
            try
            {
                String password = new Algoritmo_Encriptacion.Encriptacion().EncryptText(login.password);
                using (SQLiteConnection c = new conexion_sqlite().getConection())
                {
                    c.Open();
                    String sql = "INSERT OR IGNORE INTO LOGIN (username, password) values ('" + login.username + "','" + password + "')";
                    using (SQLiteCommand cmd = new SQLiteCommand(sql, c))
                    {
                        return cmd.ExecuteNonQuery();
                    }
                }

            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
                return 0;
            }
        }

        public int find(Login login)
        {
            String passwordEncrypt = new Algoritmo_Encriptacion.Encriptacion().EncryptText(login.password);
            //String passwordDecencrypt = new Algoritmo_Encriptacion.Encriptacion().DecryptText(passwordEncrypt, login.password);
            try
            {
                using (SQLiteConnection c = new conexion_sqlite().getConection())
                {
                    c.Open();
                    String sql = "SELECT id, username, password FROM LOGIN WHERE username ='" + login.username + "' AND password = '" + passwordEncrypt + "'";
                    using (SQLiteCommand cmd = new SQLiteCommand(sql, c))
                    {
                        using (SQLiteDataReader rdr = cmd.ExecuteReader())
                        {
                            if (rdr.Read())
                            {
                                return Convert.ToInt32(rdr["id"]);
                            }
                            else
                            {
                                return -1;
                            }
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return -1;
            }

        }

        public String restore_password(Login login) {
            String password = null;
            try
            {
                using (SQLiteConnection c = new conexion_sqlite().getConection())
                {
                    c.Open();
                    String sql = "SELECT password FROM LOGIN WHERE username = '"+login.username+"'";
                    using (SQLiteCommand cmd = new SQLiteCommand(sql, c))
                    {
                        using (SQLiteDataReader rdr = cmd.ExecuteReader())
                        {
                            while (rdr.Read())
                            {
                                password =  new Algoritmo_Encriptacion.Encriptacion().DecryptText(rdr["password"].ToString());
                            }
                        }
                    }
                }
                return password;
            }
            catch (Exception e)
            {
                return password;
            }  
        }
    }
}
