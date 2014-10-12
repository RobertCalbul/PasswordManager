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
        }

        private void btn_registrar_MouseDown(object sender, MouseButtonEventArgs e)
        {
            String username = this.txt_username.Text.Trim();
            String password = this.txt_repassword.Password.Trim();

            Login login = new Login(username, password);
            controller_login c_login = new controller_login();

            if (equal_passsword)
            {                
                if (c_login.agregar(login) > 0)
                {
                    this.main.grid_body.Children.Clear();
                    this.main.grid_body.Children.Add(new Vistas.vista_login(this.main));
                }
                else
                {
                    new Vistas.dialogo(this.main, "El usuario ya se encuentra registrado.").ShowDialog();
                }
            }
            else {
                new Vistas.dialogo(this.main, "Las contraseñas no considen.").ShowDialog();
            }
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
