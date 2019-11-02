using Gma.QrCodeNet.Encoding;
using Gma.QrCodeNet.Encoding.Windows.Render;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using Size = System.Drawing.Size;
using Point = System.Drawing.Point;
using Panel = System.Windows.Forms.Panel;

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
            GraphicsRenderer renderer = new GraphicsRenderer(new FixedCodeSize(400, QuietZoneModules.Zero),Brushes.DarkBlue,Brushes.White);
            MemoryStream ms = new MemoryStream();

            renderer.WriteToStream(qrCode.Matrix,ImageFormat.Png,ms);
            var imageTemp = new Bitmap(ms);
            var image = new Bitmap(imageTemp, new Size(new Point(200,200)));

            Panel QRresult_RegistroQR = new Panel();
            QRresult_RegistroQR.BackgroundImage = image;
            QRresult_RegistroQR.BackgroundImageLayout = ImageLayout.Stretch;
            QRresult_RegistroQR.AutoSize = true;

            host.Child = QRresult_RegistroQR;
            CodigoQR_RegistroQR.Clear();
            CodigoQR_RegistroQR.Focus();
        }
    }
}
