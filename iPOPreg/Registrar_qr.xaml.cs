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
using Image = System.Drawing.Image;

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
            host.Child = null;
            QrEncoder qrEncoder = new QrEncoder(ErrorCorrectionLevel.H);
            QrCode qrCode = new QrCode();
            qrEncoder.TryEncode(CodigoQR_RegistroQR.Text,out qrCode);
            GraphicsRenderer renderer = new GraphicsRenderer(new FixedCodeSize(400, QuietZoneModules.Zero),Brushes.DarkBlue,Brushes.White);
            MemoryStream ms = new MemoryStream();

            renderer.WriteToStream(qrCode.Matrix,ImageFormat.Png,ms);
            var imageTemp = new Bitmap(ms);
            var image = new Bitmap(imageTemp, new Size(new Point(400,400)));

            QRresult_RegistroQR.BackgroundImage = image;
            QRresult_RegistroQR.BackgroundImageLayout = ImageLayout.Stretch;
            QRresult_RegistroQR.AutoSize = true;

            host.Child = QRresult_RegistroQR;

            Guardar_RegistrosQR.IsEnabled = false;
            CodigoQR_RegistroQR.Clear();
            CodigoQR_RegistroQR.Focus();
        }

        private void Generar_RegistrosQR_Click(object sender, RoutedEventArgs e)
        {
            host.Child = null;
            QrEncoder qrEncoder = new QrEncoder(ErrorCorrectionLevel.H);
            QrCode qrCode = new QrCode();
            qrEncoder.TryEncode(CodigoQR_RegistroQR.Text, out qrCode);
            GraphicsRenderer renderer = new GraphicsRenderer(new FixedCodeSize(400, QuietZoneModules.Zero), Brushes.Black, Brushes.White);
            MemoryStream ms = new MemoryStream();

            renderer.WriteToStream(qrCode.Matrix, ImageFormat.Png, ms);
            var imageTemp = new Bitmap(ms);
            var image = new Bitmap(imageTemp, new Size(new Point(400, 400)));

            QRresult_RegistroQR.BackgroundImage = image;
            QRresult_RegistroQR.BackgroundImageLayout = ImageLayout.Stretch;
            QRresult_RegistroQR.AutoSize = true;

            host.Child = QRresult_RegistroQR;
            Guardar_RegistrosQR.IsEnabled = true;
        }

        private void Guardar_RegistrosQR_Click(object sender, RoutedEventArgs e)
        {
            Image img = (Image)QRresult_RegistroQR.BackgroundImage;
            SaveFileDialog CajaDialogoGuardar = new SaveFileDialog();
            CajaDialogoGuardar.AddExtension = true;
            CajaDialogoGuardar.Filter = "Image PNG (*.png) |*.png";
            CajaDialogoGuardar.ShowDialog();
            if (!string.IsNullOrEmpty(CajaDialogoGuardar.FileName))
            {
                img.Save(CajaDialogoGuardar.FileName,ImageFormat.Png);
            }
            img.Dispose();
        }
    }
}
