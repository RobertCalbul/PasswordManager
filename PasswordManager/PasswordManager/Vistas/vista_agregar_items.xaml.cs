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
    /// Lógica de interacción para vista_agregar_items.xaml
    /// </summary>
    public partial class vista_agregar_items : UserControl
    {
        private Manager manager;
        private MainWindow main;
        private Vistas.vista_manager v_manager;
        public vista_agregar_items(MainWindow main, Vistas.vista_manager v_manager)
        {
            InitializeComponent();
            this.main = main;
            this.v_manager = v_manager;
            Placeholder.SetText(this.txt_username, "Username");
            Placeholder.SetText(this.txt_password, "Password");
            Placeholder.SetText(this.txt_url, "Url");
        }

        private void btn_agregar_item_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (isNotNull() == true)
            {
                String username = this.txt_username.Text.Trim();
                String password = this.txt_password.Text.Trim();
                String url = this.txt_url.Text.Trim();

                manager = new Manager( username, password, url, this.main.session);
                controller_manager c_manager = new controller_manager();

                if (c_manager.agregar(manager) > 0)
                {
                    this.v_manager.load_data(0);
                    new dialogo(this.main, "Item agregado correctamente.").ShowDialog();
                }
            }
            else {
                new dialogo(this.main,"Uno o mas campos se encuentran vacios.").ShowDialog();
            }
        }

        private Boolean isNotNull() { 
            Boolean ok = !string.IsNullOrEmpty(this.txt_username.Text) && !string.IsNullOrEmpty(this.txt_password.Text) && !string.IsNullOrEmpty(this.txt_url.Text)?true: false;
            return ok;
        }
        private void btn_close_agregar_item_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.v_manager.quitar_layout_agregar_item();
        }
    }
}
