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

namespace iPOPreg
{
    /// <summary>
    /// Lógica de interacción para Panel_control.xaml
    /// </summary>
    public partial class Panel_control : Window
    {
        
        public Panel_control()
        {
            InitializeComponent();
        }

        private void Panel_PControl_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }

        private void Logout_Panel_Click(object sender, RoutedEventArgs e)
        {
            MainWindow logout = new MainWindow();
            logout.Show();
            this.Close();
        }

        private void InsertarQR_Panel_Click(object sender, RoutedEventArgs e)
        {
            Registrar_qr registroQR = new Registrar_qr();
            registroQR.Show();
            registroQR.Owner = this;
        }

        private void RegistroEq_Panel_Click(object sender, RoutedEventArgs e)
        {
            NuevoRegistro nuevoRegistro = new NuevoRegistro();
            nuevoRegistro.Owner = this;
            nuevoRegistro.ShowDialog();
        }

        private void Panel_PControl_Loaded(object sender, RoutedEventArgs e)
        {
            if (!(System.IO.File.Exists("bd.conf.xml")))

            {
                MessageBox.Show("Su sesion termina aqui\n\nNo se encontro el archivo bd.conf.xml");
                MainWindow logout = new MainWindow();
                logout.Show();
                this.Close();
            }
        }
    }
}
