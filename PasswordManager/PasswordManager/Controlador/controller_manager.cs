using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using PasswordManager.Algoritmo_Encriptacion;

namespace PasswordManager
{
    class controller_manager
    {

        public int agregar(Manager manager)
        {
            String password = new Algoritmo_Encriptacion.Encriptacion().EncryptText(manager.password);
            try
            {
                using (SQLiteConnection c = new conexion_sqlite().getConection())
                {
                    c.Open();
                    String sql = "INSERT INTO MANAGER (username, password, url, id_login) values ('" + manager.username + "','" + password + "','" + manager.url + "'," + manager.login + ")";
                    using (SQLiteCommand cmd = new SQLiteCommand(sql, c))
                    {
                        return  cmd.ExecuteNonQuery();
                    }
                } 
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return 0;
            }
        }

        public List<Manager> findByIdLogin(Login login, int flag)
        {  
            List<Manager> lista = null;
            try {
                using (SQLiteConnection c = new conexion_sqlite().getConection())
                {
                    c.Open();
                    lista = new List<Manager>();
                    String sql = "SELECT username, password, url FROM MANAGER WHERE id_login = "+login.id;
                    using (SQLiteCommand cmd = new SQLiteCommand(sql, c))
                    {
                        using (SQLiteDataReader rdr = cmd.ExecuteReader())
                        {
                           while (rdr.Read())
                            {
                                String username = rdr["username"].ToString();
                                String password = flag == 0 ? rdr["password"].ToString() : new Encriptacion().DecryptText(rdr["password"].ToString());
                                String url = rdr["url"].ToString();

                                Manager manager_ = new Manager(username,password,url);
                                lista.Add(manager_);
                            }
                        }
                    }
                }
                return lista;
            }catch(Exception e){
                return lista;
            }
        }



    }
}
