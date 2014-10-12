using PasswordManager.Algoritmo_Encriptacion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace PasswordManager.Vistas
{
    /// <summary>
    /// Lógica de interacción para vista_recuperar_password.xaml
    /// </summary>
    public partial class vista_recuperar_password : UserControl
    {
        private MainWindow main;
        public vista_recuperar_password(MainWindow main)
        {
            InitializeComponent();
            this.main = main;
        }

        private void btn_recuperar_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (!string.IsNullOrEmpty(this.txt_username.Text.Trim()))
            {
                Login login_ = new Login(this.txt_username.Text);
                controller_login c_login = new controller_login();
                String restore = c_login.restore_password(login_);

                EnviaCorreo e_correo = new EnviaCorreo(login_.username, restore);
                if (e_correo.send() > 0)
                {
                    new Vistas.dialogo(this.main, "La password se envio a su correo correctamente.").ShowDialog();
                }
                else
                {
                    new Vistas.dialogo(this.main, "La password no se envio corectamente\n Porfavor reenvie el correo.");
                }
            }
            else {
                new Vistas.dialogo(this.main,"Escriba su nombre de usuario (Email).").ShowDialog();
            }
        }

        private void btn_back_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.main.grid_body.Children.Clear();
            this.main.grid_body.Children.Add(new Vistas.vista_login(this.main));
        }
    }
}
