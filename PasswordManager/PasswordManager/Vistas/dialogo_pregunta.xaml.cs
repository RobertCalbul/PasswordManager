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
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace PasswordManager.Vistas
{
    /// <summary>
    /// Lógica de interacción para dialogo_pregunta.xaml
    /// </summary>
    public partial class dialogo_pregunta : Window
    {
        MainWindow main;
        public dialogo_pregunta(MainWindow main,String pregunta)
        {
            InitializeComponent();
            this.main = main;
            this.Top = this.main.Top + ((this.main.Height / 2) - (this.Height / 2));
            this.Left = this.main.Left + ((this.main.Width / 2) - (this.Width / 2));
            this.txt_contenido.Text = pregunta;
            Dispatcher.BeginInvoke(new Action(() => { addEfecto(); })); 
        }

        private void btn_aceptar_MouseDown(object sender, MouseButtonEventArgs e)
        {
            DialogResult = true;
            Dispatcher.BeginInvoke(new Action(() => { QuitarEfecto(); }));
            this.Close();
        }

        private void btn_cancelar_MouseDown(object sender, MouseButtonEventArgs e)
        {
            DialogResult = false;
            Dispatcher.BeginInvoke(new Action(() => { QuitarEfecto(); }));
            this.Close();
        }
        public void addEfecto()
        {
            BlurBitmapEffect myBlurEffect = new BlurBitmapEffect();
            myBlurEffect.Radius = 2;
            myBlurEffect.KernelType = KernelType.Box;
            this.main.BitmapEffect = myBlurEffect;
        }

        public void QuitarEfecto()
        {
            BlurBitmapEffect myBlurEffect = new BlurBitmapEffect();
            myBlurEffect.Radius = 0;
            myBlurEffect.KernelType = KernelType.Box;
            this.main.BitmapEffect = myBlurEffect;
        }
    }
}
