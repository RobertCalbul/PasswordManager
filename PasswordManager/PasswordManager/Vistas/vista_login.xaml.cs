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
    /// Lógica de interacción para vista_login.xaml
    /// </summary>
    public partial class vista_login : UserControl
    {
        private MainWindow main;
        private Login login;
        private controller_login c_login;

        public vista_login(MainWindow main)
        {
            InitializeComponent();
            this.main = main;

        }

        private void btn_login_MouseDown(object sender, MouseButtonEventArgs e)
        {
            String username = this.txt_username.Text.Trim();
            String password = this.txt_password.Password.Trim();

            login = new Login(username, password);
            c_login = new controller_login();

            if (c_login.find(login))
            {

                this.main.grid_body.Children.Clear();
                this.main.grid_body.Children.Add(new Vistas.vista_manager());
            }
            else { new Vistas.dialogo(this.main, "Revise el usuario o password.").ShowDialog(); }
        }

        private void link_recuperar_pass_MouseDown(object sender, MouseButtonEventArgs e)
        {
           
        }

        private void btn_nuevo_usuario_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.main.grid_body.Children.Clear();
            this.main.grid_body.Children.Add(new Vistas.vista_new_user(this.main));
        }
    }
}
