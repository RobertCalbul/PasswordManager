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

            Placeholder.SetText(this.txt_username, "Username");
            Placeholder.SetText(this.txt_password,"Password");
            /*xmlns:src="clr-namespace:Huan.WhiteDwarf.UI" to the root element, e.g. Window, and add
            src:Placeholder.Text="Your Placeholder" to the TextBox/RichTextBox/PasswordBox.

        To use it in code, use this
        Huan.WhiteDwarf.UI.Placeholder.SetText(TextBoxName, PlaceholderText);*/
        }

        private void LogIn(){
            String username = this.txt_username.Text.Trim();
            String password = this.txt_password.Password.Trim();

            login = new Login(username, password);
            c_login = new controller_login();

            int id_login = c_login.find(login);
            if (id_login > 0)
            {
                this.main.session = id_login;
                this.main.grid_body.Children.Clear();
                this.main.grid_body.Children.Add(new Vistas.vista_manager(this.main));
            }
            else
            {
                this.main.session = 0;
                new Vistas.dialogo(this.main, "Revise el usuario o password.").ShowDialog();
            }
        }
        private void btn_login_MouseDown(object sender, MouseButtonEventArgs e)
        {
            LogIn();
        }

        private void link_recuperar_pass_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.main.grid_body.Children.Clear();
            this.main.grid_body.Children.Add(new Vistas.vista_recuperar_password(this.main));
        }

        private void btn_nuevo_registro_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.main.grid_body.Children.Clear();
            this.main.grid_body.Children.Add(new Vistas.vista_new_user(this.main));
        }

        private void UserControl_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                LogIn();
            }
        }

    }
}
