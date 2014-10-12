using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Web.Script.Serialization;
using System.Collections;

namespace PasswordManager.Algoritmo_Encriptacion
{
    class EnviaCorreo
    {
        public String to { get; set; }
        public String password { get; set; }
        public EnviaCorreo() { }

        public EnviaCorreo(String to, String password)
        {
            this.to = to;
            this.password = password;
        }
        public void send()
        {

            JSON json = getJson();

            SmtpClient server = new SmtpClient("smtp.gmail.com", 587);
            server.Credentials = new System.Net.NetworkCredential(json.correo, json.password);
            server.EnableSsl = true;
            try
            {
                MailMessage mnsj = new MailMessage();


                mnsj.Subject = "Restauracion contraseña.";

                mnsj.To.Add(new MailAddress(this.to));

                mnsj.From = new MailAddress(json.correo);


                // mnsj.Attachments.Add(new Attachment("C:\\archivo.pdf"));

                mnsj.Body = " Su contraseña es: "+this.password;

                server.Send(mnsj);

                MessageBox.Show("El Mail se ha Enviado Correctamente");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        public JSON getJson()
        {

            //string path = Environment.CurrentDirectory + @"\Algoritmo_Encriptacion\datos_correos.json";
            string path = Path.Combine(Environment.CurrentDirectory, @"..\..\Algoritmo_Encriptacion\datos_correos.json");
            Console.WriteLine("path " + path);
            String aux = null;
            if (File.Exists(path))
            {
                StreamReader sreamReader = new StreamReader(path);
                String line = sreamReader.ReadLine();
                while (line != null)
                {
                    aux += line;
                    line = sreamReader.ReadLine();
                }
            }

            JSON json = (JSON)new JavaScriptSerializer().Deserialize(aux, typeof(JSON));
            Console.WriteLine(json.correo);
            return json;
        }



    }

    public class JSON
    {
        public string correo { get; set; }
        public string password { get; set; }
    }
}

