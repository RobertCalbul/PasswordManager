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
    /// Lógica de interacción para vista_new_user.xaml
    /// </summary>
    public partial class vista_new_user : UserControl
    {
        private MainWindow main;
        private Boolean equal_passsword = false;
        public vista_new_user(MainWindow main)
        {
            InitializeComponent();
            this.main = main;

            Placeholder.SetText(this.txt_username, "E-Mail");
            Placeholder.SetText(this.txt_password, "Password");
            Placeholder.SetText(this.txt_repassword, "Repita Password");
        }

        private void btn_registrar_MouseDown(object sender, MouseButtonEventArgs e)
        {
            String username = this.txt_username.Text.Trim();
            String password = this.txt_repassword.Password.Trim();

            Login login = new Login(username, password);
            controller_login c_login = new controller_login();

            if (validar_campos() == true)
            {
                if (equal_passsword)
                {
                    if (c_login.agregar(login) > 0)
                    {

                        EnviaCorreo e_correo = new EnviaCorreo(username, password);

                        switch (e_correo.send())
                        {
                            case 0: new Vistas.dialogo(this.main, "La password no se envio corectamente\n Porfavor reenvie el correo."); break;
                            case 1: new Vistas.dialogo(this.main, "La password se envio a su correo correctamente.").ShowDialog(); break;
                            case 2: new Vistas.dialogo(this.main, "Ingrese un correo valido.").ShowDialog(); break;
                        }
                        this.main.grid_body.Children.Clear();
                        this.main.grid_body.Children.Add(new Vistas.vista_login(this.main));
                    }
                    else
                    {
                        new Vistas.dialogo(this.main, "El usuario ya se encuentra registrado.").ShowDialog();
                    }
                }
                else
                {
                    new Vistas.dialogo(this.main, "Las contraseñas no considen.").ShowDialog();
                }
            } else
            {
                new Vistas.dialogo(this.main, "Uno o mas campos estan vacios.").ShowDialog();

            }
        }

        public Boolean validar_campos() {
            Boolean ok = false;
            ok = !string.IsNullOrEmpty(this.txt_username.Text) && !string.IsNullOrEmpty(this.txt_password.Password) && !string.IsNullOrEmpty(this.txt_repassword.Password) ? true : false;
            return ok;
        }
        private void btn_back_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.main.grid_body.Children.Clear();
            this.main.grid_body.Children.Add(new Vistas.vista_login(this.main));
        }

        private void txt_repassword_KeyUp(object sender, KeyEventArgs e)
        {
            equal_passsword = this.txt_password.Password.Equals(this.txt_repassword.Password) ? true : false;
            this.img_ok.Visibility = this.txt_password.Password.Equals(this.txt_repassword.Password) ? Visibility.Visible : Visibility.Hidden;
        }


    }
}
