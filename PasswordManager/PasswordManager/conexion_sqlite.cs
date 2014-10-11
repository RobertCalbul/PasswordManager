using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;
namespace PasswordManager
{
    class conexion_sqlite
    {
        public SQLiteConnection sqlite_conn;

        public SQLiteConnection getConection()
        {
            // create a new database connection:
            sqlite_conn = new SQLiteConnection("Data Source=database.db;Version=3;New=False;Compress=True;");
            return sqlite_conn;
        }
    }
}
