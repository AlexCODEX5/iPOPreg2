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
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using MySql.Data.MySqlClient;

namespace iPOPreg
{
    /// <summary>
    /// Lógica de interacción para Baja_equipos.xaml
    /// </summary>
    public partial class Baja_equipos : Window
    {
        public Baja_equipos()
        {
            InitializeComponent();
        }

        private DataSet datos;
        BDasistente Baja_equiposAsist = new BDasistente();

        private void Cerrar_BajaEquipos_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Header_BajaEquipos_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }

        private void Grid_Loaded(object sender, RoutedEventArgs e)
        {
            Baja_equiposAsist.AutoSetCadenaConexion(datos);
            AutoCompleteSource CodinSource = new AutoCompleteSource();
            AutoCompleteSource DescSource = new AutoCompleteSource();
            MySqlConnection BajaDatosCon = new MySqlConnection(Baja_equiposAsist.CadenaConexion());
            try
            {
                BajaDatosCon.Open();
            }
            catch
            {

            }

        }
    }
}
