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
        private  SQLiteConnection conn = null;
        private SQLiteCommand sqlite_cmd;
        private SQLiteDataReader sqlite_datareader;

        public int agregar(Login login) {
            conn = new conexion_sqlite().getConection();
            try
            {
                conn.Open();
                sqlite_cmd = conn.CreateCommand();
                sqlite_cmd.CommandText = "INSERT OR IGNORE INTO LOGIN (username, password) values ('" + login.username + "','" + login.password + "')";
                return sqlite_cmd.ExecuteNonQuery();

            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return 0;
            }
            finally {
                if (conn != null) {
                    conn.Close();
                }
            }
        }

        public Boolean find(Login login) {
            conn = new conexion_sqlite().getConection();
            try
            {
                conn.Open();
                sqlite_cmd = conn.CreateCommand();
                sqlite_cmd.CommandText = "SELECT username, password FROM LOGIN WHERE username ='" + login.username + "' AND password = '" + login.password + "'";
              
                sqlite_datareader = sqlite_cmd.ExecuteReader();
                if (sqlite_datareader.Read())
                {
                    return true;
                }
                else {
                    return false;
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
                return false;
            }
            finally {
                if (conn != null) {
                    conn.Close();
                }
            }
        }
    }
}
