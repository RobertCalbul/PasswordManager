using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Finisar.SQLite;

namespace PasswordManager
{
    class controller_login
    {
        public SQLiteConnection conn = null;
        public int agregar(/*Login login*/) {
            conn = new conexion_sqlite().getConection();
            try {
                SQLiteCommand sqlite_cmd;
                conn.Open();
                sqlite_cmd = conn.CreateCommand();
                sqlite_cmd.CommandText = "CREATE TABLE LOGIN (id integer primary key, username varchar(100), password varchar(100));";
                return sqlite_cmd.ExecuteNonQuery();
               
            }catch(Exception e){
                return 0;
            }
        }
    }
}
