using Gma.QrCodeNet.Encoding;
using Gma.QrCodeNet.Encoding.Windows.Render;
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
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Drawing;
using System.IO;

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
            QrEncoder qrEncoder = new QrEncoder(ErrorCorrectionLevel.H);
            QrCode qrCode = new QrCode();
            qrEncoder.TryEncode(CodigoQR_RegistroQR.Text,out qrCode);
            GraphicsRenderer renderer = new GraphicsRenderer(new FixedCodeSize(400, QuietZoneModules.Zero),Brushes.Black,Brushes.White);
            MemoryStream ms = new MemoryStream();
            CodigoQR_RegistroQR.Clear();
            CodigoQR_RegistroQR.Focus();
        }
    }
}
