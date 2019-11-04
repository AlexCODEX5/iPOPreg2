using System;
using System.Collections.Generic;
using System.Data;
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

namespace iPOPreg
{
    /// <summary>
    /// Lógica de interacción para MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        DataSet datos = new DataSet("MiBaseDatos");
        DataTable tabla = new DataTable("MySQL");

        private void Salir_Login_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Min_Login_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }

        private void Verificar_Login_Click(object sender, RoutedEventArgs e)
        {
            Panel_control ingreso = new Panel_control();
            ingreso.Show();
            this.Close();
        }

        private void BordeTit_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }

        private void Panel_Login_Loaded(object sender, RoutedEventArgs e)
        {
            
            User_Login.Focus();
            try
            {
                datos.ReadXml("MiArchivo.xml", XmlReadMode.Auto);//Cambiar Nombre de archivo
            }
            catch(Exception ex)
            {
                MessageBox.Show($"Se creara una nueva tabla de datos de conexion de la Base de Datos\n\n{ex.Message}","No se encontro el Archivo de Conexión");
                tabla.Columns.Add(new DataColumn("datasource", Type.GetType("System.String")));
                tabla.Columns.Add(new DataColumn("port", Type.GetType("System.String")));
                tabla.Columns.Add(new DataColumn("username", Type.GetType("System.String")));
                tabla.Columns.Add(new DataColumn("password", Type.GetType("System.String")));
                tabla.Columns.Add(new DataColumn("datasource", Type.GetType("System.String")));
            }
        }


    }
}
