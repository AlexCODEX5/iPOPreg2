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
    /// Lógica de interacción para Registrar_qr.xaml
    /// </summary>
    public partial class Registrar_qr : Window
    {
        public Registrar_qr()
        {
            InitializeComponent();
        }

        private void RegistroQR_Panel_Loaded(object sender, RoutedEventArgs e)
        {
            CodigoQR_RegistroQR.Focus();
        }

        private void Escanear_RegistroQR_Click(object sender, RoutedEventArgs e)
        {
            CodigoQR_RegistroQR.Clear();
            CodigoQR_RegistroQR.Focus();
        }
    }
}
