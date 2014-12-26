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
        private MainWindow main;
        public  int mostrar_pass = 0;
        public vista_manager(MainWindow main)
        {
            InitializeComponent();
            this.main = main;
            data_items_Loaded();
            //Console.WriteLine(">>"+new Algoritmo_Encriptacion.Encriptacion().EncryptText("aaa","aaaa"));
           // Console.WriteLine(new Algoritmo_Encriptacion.Encriptacion().DecryptText("gjOo+AUOsdk1Ubh3ah/yqw==", "aaaa"));
        }

        private void btn_agregar_nuevo_item_MouseDown(object sender, MouseButtonEventArgs e)
        {
            agregar_layout_agregar_item(1,0);
        }

        public void agregar_layout_agregar_item(int flag, int id)
        {
            this.btn_agregar_nuevo_item.IsEnabled = false;
            this.grid_agregar_items.Children.Clear();
            this.grid_agregar_items.Children.Add(new Vistas.vista_agregar_items(this.main, this, flag, id));
        }
        public void quitar_layout_agregar_item()
        {
            this.grid_agregar_items.Children.Clear();
            this.btn_agregar_nuevo_item.IsEnabled = true;
        }

        private void data_items_Loaded()
        {
            load_data(mostrar_pass);
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
            for (int c = 0; c < header.Count-2; c++)
            {
                ColumnDefinition cd = new ColumnDefinition();
                cd.Width = new GridLength(100, GridUnitType.Star);
                DynamicGrid.ColumnDefinitions.Add(cd);
            }
            ColumnDefinition cd1 = new ColumnDefinition();
            cd1.Width = new GridLength(100, GridUnitType.Auto);
            DynamicGrid.ColumnDefinitions.Add(cd1);

            ColumnDefinition cd2 = new ColumnDefinition();
            cd2.Width = new GridLength(100, GridUnitType.Auto);
            DynamicGrid.ColumnDefinitions.Add(cd2);

            for (int f = 0; f <= largo; f++)
            {
                RowDefinition rd = new RowDefinition();
                rd.Height = new GridLength(50, GridUnitType.Auto); //new GridLength(50, GridUnitType.Auto);
                DynamicGrid.RowDefinitions.Add(rd);

            }

            for (int c = 0; c < header.Count-2; c++)
            {
                Label lbl1 = new Label();
                lbl1.Content = header[c];
                lbl1.Width = Double.NaN;
                lbl1.Margin = new Thickness(0, 0, 0, 0);
                lbl1.FontWeight = FontWeights.Bold;
                lbl1.Foreground = new SolidColorBrush(Colors.White) ;
                lbl1.Background = c != 3 || c != 4 ? (SolidColorBrush)new BrushConverter().ConvertFromString("#FFF50057") : new SolidColorBrush(Colors.Transparent);
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
                txt.Foreground = f % 2 == 0 ? (SolidColorBrush)new BrushConverter().ConvertFromString("#FFFF4081")/*new SolidColorBrush(Colors.White) */: (SolidColorBrush)new BrushConverter().ConvertFromString("#FFFF80AB");
                txt.Background = f % 2 == 0 ? (SolidColorBrush)new BrushConverter().ConvertFromString("#FFFF80AB") : (SolidColorBrush)new BrushConverter().ConvertFromString("#FFFF4081")/*new SolidColorBrush(Colors.White)*/;
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
                txt1.Foreground = f % 2 == 0 ? (SolidColorBrush)new BrushConverter().ConvertFromString("#FFFF4081")/*new SolidColorBrush(Colors.White)*/ : (SolidColorBrush)new BrushConverter().ConvertFromString("#FFFF80AB");
                txt1.Background = f % 2 == 0 ? (SolidColorBrush)new BrushConverter().ConvertFromString("#FFFF80AB") : (SolidColorBrush)new BrushConverter().ConvertFromString("#FFFF4081")/*new SolidColorBrush(Colors.White)*/;
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
                txt2.Foreground = f % 2 == 0 ? (SolidColorBrush)new BrushConverter().ConvertFromString("#FFFF4081")/*new SolidColorBrush(Colors.White) */: (SolidColorBrush)new BrushConverter().ConvertFromString("#FFFF80AB");
                txt2.Background = f % 2 == 0 ? (SolidColorBrush)new BrushConverter().ConvertFromString("#FFFF80AB") : (SolidColorBrush)new BrushConverter().ConvertFromString("#FFFF4081")/*new SolidColorBrush(Colors.White)*/;
                txt2.VerticalAlignment = VerticalAlignment.Stretch;
                txt2.HorizontalAlignment = HorizontalAlignment.Stretch;
                txt2.Margin = new Thickness(0, 0, 0, 0);
                Grid.SetRow(txt2, f + 1);
                Grid.SetColumn(txt2, 2);
                Grid.SetRowSpan(txt2, 1);
                Grid.SetColumnSpan(txt2, 1);
                if (elementos[f].url.Substring(0, 4) == "http")
                    txt2.MouseDoubleClick += new MouseButtonEventHandler(txt_url__MouseDoubleClick);

                Image imgEdit = new Image();
                imgEdit.Name = "id_0000" + elementos[f].id;
                imgEdit.Width = 22;
                imgEdit.Height = 22;
                imgEdit.VerticalAlignment = VerticalAlignment.Stretch;
                imgEdit.HorizontalAlignment = HorizontalAlignment.Stretch;
                imgEdit.Margin = new Thickness(0, 0, 0, 0);
                var uri1 = new Uri(@"pack://application:,,,/PasswordManager;component/src/edit.png");
                imgEdit.Source = new BitmapImage(uri1);
                Grid.SetRow(imgEdit, f + 1);
                Grid.SetColumn(imgEdit, 3);
                Grid.SetRowSpan(imgEdit, 1);
                Grid.SetColumnSpan(imgEdit, 1);

                imgEdit.MouseDown += new MouseButtonEventHandler(btn_edit);


                Image img = new Image();
                img.Name = "id_0000"+elementos[f].id;
                img.Width = 22;
                img.Height = 22;
                img.VerticalAlignment = VerticalAlignment.Stretch;
                img.HorizontalAlignment = HorizontalAlignment.Stretch;
                img.Margin = new Thickness(0, 0, 0, 0);
                var uri = new Uri(@"pack://application:,,,/PasswordManager;component/src/close.png");
                img.Source = new BitmapImage(uri);
                Grid.SetRow(img, f + 1);
                Grid.SetColumn(img, 4);
                Grid.SetRowSpan(img, 1);
                Grid.SetColumnSpan(img, 1);

                img.MouseDown += new MouseButtonEventHandler(btn_delete);

                DynamicGrid.Children.Add(txt);
                DynamicGrid.Children.Add(txt1);
                DynamicGrid.Children.Add(txt2);
                DynamicGrid.Children.Add(imgEdit);
                DynamicGrid.Children.Add(img);

            }
            #endregion
            this.grid_datos.Children.Clear();
            this.grid_datos.Children.Add(scrollViewer_);
        }
        private void btn_delete(object sender, MouseButtonEventArgs e)
        {
            try
            {
                dialogo_pregunta d_pregunta = new dialogo_pregunta(this.main, "¿Desea borrar estos datos definitivamente?");
                d_pregunta.ShowDialog();
                if (d_pregunta.DialogResult == true)
                {
                    String getId = ((Image)sender).Name.ToString().Substring(((Image)sender).Name.ToString().Length - 5, 5);
                    int id = Convert.ToInt32(getId);
                    Manager manager_ = new Manager(id);
                    controller_manager c_manager = new controller_manager();
                    if (c_manager.deleteById(manager_) > 0)
                    {
                        new dialogo(this.main, "Se ha borrado correctamente.").ShowDialog();
                        load_data(this.mostrar_pass);
                    }
                    else
                    {
                        new dialogo(this.main, "Ha ocurrido un error al borrar, porfavor intente nuevamente.").ShowDialog();
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            
        }

        private void btn_edit(object sender, MouseButtonEventArgs e)
        {
            String getId = ((Image)sender).Name.ToString().Substring(((Image)sender).Name.ToString().Length - 5, 5);
            int id = Convert.ToInt32(getId);
            agregar_layout_agregar_item(2, id);
        }
        private void txt_url__MouseDoubleClick(object sender, MouseButtonEventArgs e)
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
            if (this.mostrar_pass == 0)
            {
                new dialogo_confirma_password(this.main, this).ShowDialog();
            }
            else
            {
                this.btn_mostrar_password.Content = "Mostrar Contraeñas";
                this.mostrar_pass = 0;
                this.load_data(this.mostrar_pass);
            }

        }
    }
}
