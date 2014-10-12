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
    /// Lógica de interacción para vista_manager.xaml
    /// </summary>
    public partial class vista_manager : UserControl
    {
        public MainWindow main;
        public vista_manager(MainWindow main)
        {
            InitializeComponent();
            this.main = main;

            //Console.WriteLine(">>"+new Algoritmo_Encriptacion.Encriptacion().EncryptText("aaa","aaaa"));
           // Console.WriteLine(new Algoritmo_Encriptacion.Encriptacion().DecryptText("gjOo+AUOsdk1Ubh3ah/yqw==", "aaaa"));
        }

        private void btn_agregar_nuevo_item_MouseDown(object sender, MouseButtonEventArgs e)
        {
            agregar_layout_agregar_item();
        }

        public void agregar_layout_agregar_item()
        {
            this.btn_agregar_nuevo_item.IsEnabled = false;
            this.grid_agregar_items.Children.Clear();
            this.grid_agregar_items.Children.Add(new Vistas.vista_agregar_items(this.main, this));
        }
        public void quitar_layout_agregar_item()
        {
            this.grid_agregar_items.Children.Clear();
            this.btn_agregar_nuevo_item.IsEnabled = true;
        }

        private void data_items_Loaded(object sender, RoutedEventArgs e)
        {
            load_data();
        }

        public void load_data() {

            Login login_ = new Login(this.main.session);
            controller_manager c_manager = new controller_manager();

            List<Manager> lista = c_manager.findByIdLogin(login_);

            this.data_items.ItemsSource = lista;
        }
    }
}
