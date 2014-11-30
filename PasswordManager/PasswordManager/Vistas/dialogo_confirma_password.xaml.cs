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
using PasswordManager.Algoritmo_Encriptacion;
using System.Windows.Media.Effects;
namespace PasswordManager.Vistas
{
    /// <summary>
    /// Lógica de interacción para dialogo_confirma_password.xaml
    /// </summary>
    public partial class dialogo_confirma_password : Window
    {
        private MainWindow main;
        private vista_manager v_manager;
        public dialogo_confirma_password(MainWindow main, vista_manager v_manager)
        {
            InitializeComponent();
            this.main = main;
            this.v_manager = v_manager;
            Placeholder.SetText(this.txt_password,"Password");

            this.Top = this.main.Top + ((this.main.Height / 2) - (this.Height / 2));
            this.Left = this.main.Left + ((this.main.Width / 2) - (this.Width / 2));

            Dispatcher.BeginInvoke(new Action(() => { addEfecto(); }));
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

        public void Acept()
        {
            int id = this.main.session;
            String password = this.txt_password.Password.Trim();

            Login login_ = new Login(id, password);
            controller_login c_login = new controller_login();

            if (c_login.ifExit(login_) > 0)
            {
                this.v_manager.btn_mostrar_password.Content = "Ocultar Contraeñas";
                this.v_manager.mostrar_pass = 1;
                this.v_manager.load_data(this.v_manager.mostrar_pass);
            }

            Dispatcher.BeginInvoke(new Action(() => { QuitarEfecto(); }));
            this.Close();
        }
        private void btn_aceptar_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Acept();
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                Acept(); 
            }
        }
    }
}
