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
                registroQR.Owner = this;
                registroQR.Show();
                registroQR.Topmost = true;

        }
    }
}
