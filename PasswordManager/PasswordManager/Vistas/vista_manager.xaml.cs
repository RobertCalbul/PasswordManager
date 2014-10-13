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
            load_data(0);
        }

        public void load_data(int flag) {

            Login login_ = new Login(this.main.session);
            controller_manager c_manager = new controller_manager();
            List<Manager> elementos = c_manager.findByIdLogin(login_, flag);

            List<String> header = new List<string>();
            header.Add("Username");
            header.Add("Password");
            header.Add("Url");
            header.Add("");

            // Create the Grid
            Grid DynamicGrid = new Grid();
            DynamicGrid.Width = Double.NaN;
            DynamicGrid.HorizontalAlignment = HorizontalAlignment.Stretch;
            DynamicGrid.VerticalAlignment = VerticalAlignment.Stretch;
            DynamicGrid.ShowGridLines = false;
            DynamicGrid.Margin = new Thickness(0, 0, 0, 0);
            //DynamicGrid.Background = new SolidColorBrush(Colors.LightSteelBlue);

            ScrollViewer scrollViewer_ = new ScrollViewer();
            scrollViewer_.VerticalScrollBarVisibility = ScrollBarVisibility.Auto;
            scrollViewer_.Name = "Scrolling";
            scrollViewer_.CanContentScroll = true;

            scrollViewer_.Content = DynamicGrid;

            #region header y col y row
            int largo = elementos.Count;
            for (int c = 0; c < header.Count-1; c++)
            {
                ColumnDefinition cd = new ColumnDefinition();
                cd.Width = new GridLength(100, GridUnitType.Star);
                DynamicGrid.ColumnDefinitions.Add(cd);
            }
            ColumnDefinition cd1 = new ColumnDefinition();
            cd1.Width = new GridLength(100, GridUnitType.Auto);
            DynamicGrid.ColumnDefinitions.Add(cd1);

            for (int f = 0; f <= largo; f++)
            {
                RowDefinition rd = new RowDefinition();
                rd.Height = new GridLength(50, GridUnitType.Auto); //new GridLength(50, GridUnitType.Auto);
                DynamicGrid.RowDefinitions.Add(rd);

            }

            for (int c = 0; c < header.Count; c++)
            {
                Label lbl1 = new Label();
                lbl1.Content = header[c];
                lbl1.Width = Double.NaN;
                lbl1.Margin = new Thickness(0, 0, 0, 0);
                lbl1.FontWeight = FontWeights.Bold;
                lbl1.Foreground = new SolidColorBrush(Colors.White) ;
                lbl1.Background = c!=3?(SolidColorBrush)new BrushConverter().ConvertFromString("#FF68072E"): new SolidColorBrush(Colors.Transparent);
                lbl1.VerticalAlignment = VerticalAlignment.Stretch;
                lbl1.HorizontalAlignment = HorizontalAlignment.Stretch;

                Grid.SetRowSpan(lbl1, 1);
                Grid.SetColumnSpan(lbl1, 1);
                Grid.SetRow(lbl1, 0);
                Grid.SetColumn(lbl1, c);
                DynamicGrid.Children.Add(lbl1);
            }
            #endregion
            #region llena datos
            for (int f = 0; f < elementos.Count; f++)
            {


                TextBox txt = new TextBox();
                txt.Text = elementos[f].username;
                txt.Foreground = f % 2 == 0 ? new SolidColorBrush(Colors.White) : (SolidColorBrush)new BrushConverter().ConvertFromString("#FFE3005A");
                txt.Background = f % 2 == 0 ? (SolidColorBrush)new BrushConverter().ConvertFromString("#FFE3005A") : new SolidColorBrush(Colors.White);
                txt.VerticalAlignment = VerticalAlignment.Stretch;
                txt.HorizontalAlignment = HorizontalAlignment.Stretch;
                txt.Margin = new Thickness(0, 0, 0, 0);
                Grid.SetRow(txt, f + 1);
                Grid.SetColumn(txt, 0);
                Grid.SetRowSpan(txt, 1);
                Grid.SetColumnSpan(txt, 1);


                TextBox txt1 = new TextBox();
                txt1.Text = elementos[f].password;
                txt1.Width = Double.NaN;
                txt1.Foreground = f % 2 == 0 ? new SolidColorBrush(Colors.White) : (SolidColorBrush)new BrushConverter().ConvertFromString("#FFE3005A");
                txt1.Background = f % 2 == 0 ? (SolidColorBrush)new BrushConverter().ConvertFromString("#FFE3005A") : new SolidColorBrush(Colors.White);
                txt1.VerticalAlignment = VerticalAlignment.Stretch;
                txt1.HorizontalAlignment = HorizontalAlignment.Stretch;
                txt1.Margin = new Thickness(0, 0, 0, 0);
                Grid.SetRow(txt1, f + 1);
                Grid.SetColumn(txt1, 1);
                Grid.SetRowSpan(txt1, 1);
                Grid.SetColumnSpan(txt1, 1);


                TextBox txt2 = new TextBox();
                txt2.Text = elementos[f].url;
                txt2.Width = Double.NaN;
                txt2.Foreground = f % 2 == 0 ? new SolidColorBrush(Colors.White) : (SolidColorBrush)new BrushConverter().ConvertFromString("#FFE3005A");
                txt2.Background = f % 2 == 0 ? (SolidColorBrush)new BrushConverter().ConvertFromString("#FFE3005A") : new SolidColorBrush(Colors.White);
                txt2.VerticalAlignment = VerticalAlignment.Stretch;
                txt2.HorizontalAlignment = HorizontalAlignment.Stretch;
                txt2.Margin = new Thickness(0, 0, 0, 0);
                Grid.SetRow(txt2, f + 1);
                Grid.SetColumn(txt2, 2);
                Grid.SetRowSpan(txt2, 1);
                Grid.SetColumnSpan(txt2, 1);
                txt2.MouseDoubleClick += new MouseButtonEventHandler(TextBox_MouseDoubleClick);

                Image img = new Image();
                img.Name = "id_"+elementos[f].id;
                img.Width = 33;
                img.Height = 33;
                img.VerticalAlignment = VerticalAlignment.Stretch;
                img.HorizontalAlignment = HorizontalAlignment.Stretch;
                img.Margin = new Thickness(0, 0, 0, 0);
                var uri = new Uri(@"pack://application:,,,/PasswordManager;component/src/close.png");
                img.Source = new BitmapImage(uri);
                Grid.SetRow(img, f + 1);
                Grid.SetColumn(img, 3);
                Grid.SetRowSpan(img, 1);
                Grid.SetColumnSpan(img, 1);

                img.MouseDown += new MouseButtonEventHandler(btn_delete);

                DynamicGrid.Children.Add(txt);
                DynamicGrid.Children.Add(txt1);
                DynamicGrid.Children.Add(txt2);
                DynamicGrid.Children.Add(img);

            }
            #endregion
            this.grid_datos.Children.Clear();
            this.grid_datos.Children.Add(scrollViewer_);
        }
        private void btn_delete(object sender, MouseButtonEventArgs e)
        {
            MessageBox.Show(((Image)sender).Name.ToString());
        }
        private void TextBox_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            //MessageBox.Show(((TextBox)sender).Text);
            System.Diagnostics.Process.Start(((TextBox)sender).Text);
        }
        private void btn_cambia_usuario_MouseDown(object sender, MouseButtonEventArgs e)
        {
            dialogo_pregunta d_pregunta = new dialogo_pregunta(this.main, "¿Desea cambiar de usuario?");
            d_pregunta.ShowDialog();
            if (d_pregunta.DialogResult == true)
            {
                this.main.session = 0;
                this.main.grid_body.Children.Clear();
                this.main.grid_body.Children.Add(new Vistas.vista_login(this.main));
            }
        }

        private void btn_mostrar_password_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (this.btn_mostrar_password.Content.ToString().StartsWith("M"))
            {
                this.btn_mostrar_password.Content = "Ocultar Contraeñas";
                load_data(1);
            }
            else {
                this.btn_mostrar_password.Content = "Mostrar Contraeñas";
                load_data(0);
            }
        }
    }
}
