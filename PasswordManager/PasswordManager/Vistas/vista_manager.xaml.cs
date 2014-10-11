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
        public vista_manager()
        {
            InitializeComponent();
        }

        private void btn_agregar_nuevo_item_MouseDown(object sender, MouseButtonEventArgs e)
        {
            agregar_layout_agregar_item();
        }

        public void agregar_layout_agregar_item()
        {
            this.grid_agregar_items.Children.Clear();
            this.grid_agregar_items.Children.Add(new Vistas.vista_agregar_items());
        }
        public void quitar_layout_agregar_item()
        {
            this.grid_agregar_items.Children.Clear();
        }
    }
}
