using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;
using System.Windows;
using System.Management;
namespace PasswordManager
{
    class conexion_sqlite
    {
        public SQLiteConnection sqlite_conn;

        public SQLiteConnection getConection()
        {
            // create a new database connection:  Version=3;New=False;Compress=True;
            try
            {
                sqlite_conn = new SQLiteConnection(@"Data Source=C:\PasswordManager\database.db;Version=3;New=False;Compress=True;");
                return sqlite_conn;
            }
            catch (Exception e) {

                sqlite_conn = new SQLiteConnection(@"Data Source=database.db;Version=3;New=False;Compress=True;");
                return sqlite_conn;
            }
        }

        public void copyBD() {

            string fileName = "database.db";
            string sourcePath = @"C:\Program Files (x86)\TheKiller\Setup\";
            string targetPath = @"C:\PasswordManager";

            // Use Path class to manipulate file and directory paths.
            string sourceFile = System.IO.Path.Combine(sourcePath, fileName);
            string destFile = System.IO.Path.Combine(targetPath, fileName);

            try
            {
                if (!System.IO.Directory.Exists(sourcePath))
                {
                    sourcePath = @"C:\Program Files\TheKiller\Setup\";
                }

                if (!System.IO.Directory.Exists(targetPath))
                {
                    System.IO.Directory.CreateDirectory(targetPath);
                }
                if (!System.IO.File.Exists(destFile))
                {
                    MessageBox.Show("NO EXISTE");
                    System.IO.File.Copy(sourceFile, destFile, true);
                }
            }
            catch (Exception e) {
                Console.WriteLine("Error conexion_sqlite.copyDB(): "+e.Message);
            }
        }

    }
}
