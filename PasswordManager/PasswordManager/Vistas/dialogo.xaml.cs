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
using System.Windows.Shapes;

namespace PasswordManager.Vistas
{
    /// <summary>
    /// Lógica de interacción para dialogo.xaml
    /// </summary>
    public partial class dialogo : Window
    {
        private MainWindow main;
        public dialogo(MainWindow main, String message)
        {
            InitializeComponent();
            this.main = main;
            this.txt_contenido.Text = message;
            this.Top = this.main.Top + ((this.main.Height / 2) - (this.Height / 2));
            this.Left = this.main.Left + ((this.main.Width / 2) - (this.Width / 2));
        }

        private void btn_aceptar_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.Close();
        }
    }
}
