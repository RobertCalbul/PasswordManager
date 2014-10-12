using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;


namespace PasswordManager
{
    class controller_manager
    {


        public int agregar(Manager manager)
        {
            try
            {
                using (SQLiteConnection c = new conexion_sqlite().getConection())
                {
                    c.Open();
                    String sql = "INSERT INTO MANAGER (username, password, url, id_login) values ('" + manager.username + "','" + manager.password + "','" + manager.url + "'," + manager.login + ")";
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

        public List<Manager> findByIdLogin(Login login)
        {   List<Manager> lista = null;
            try {
                using (SQLiteConnection c = new conexion_sqlite().getConection())
                {
                    c.Open();
                    lista = new List<Manager>();
                    String sql = "SELECT id, username, password, url FROM MANAGER WHERE id_login = "+login.id;
                    using (SQLiteCommand cmd = new SQLiteCommand(sql, c))
                    {
                        using (SQLiteDataReader rdr = cmd.ExecuteReader())
                        {
                           while (rdr.Read())
                            {
                                Manager manager_ = new Manager(rdr["username"].ToString(),rdr["password"].ToString(),rdr["url"].ToString());
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
