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
        }
    }
}
