﻿using System;
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

            Dispatcher.BeginInvoke(new Action(() => { addEfecto(); }));
        }

        private void btn_aceptar_MouseDown(object sender, MouseButtonEventArgs e)
        {
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

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                Dispatcher.BeginInvoke(new Action(() => { QuitarEfecto(); }));
                this.Close();
            }
        }
    }
}
