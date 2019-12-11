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
using MessageBox = System.Windows.MessageBox;

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

        AutoCompleteStringCollection CodinSource = new AutoCompleteStringCollection();
        AutoCompleteStringCollection DescSource = new AutoCompleteStringCollection();
        private DataSet datos = new DataSet();
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
            
            MySqlConnection BajaDatosCon = new MySqlConnection(Baja_equiposAsist.CadenaConexion());
            try
            {
                BajaDatosCon.Open();
            }
            catch(Exception ex)
            {
                MessageBox.Show($"Se perdio conexion con la base de datos\n\n{ex.Message}");
                this.Close();
            }
            if (BajaDatosCon.State == ConnectionState.Open)
            {
                MySqlDataReader reader = Baja_equiposAsist.ListEntrada(BajaDatosCon);
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        string[] row = { reader.GetString(0) };
                        CodinSource.Add(row[0]);
                    }
                    reader.Close();
                }
                reader = Baja_equiposAsist.ListSalida(BajaDatosCon);
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        string[] row = { reader.GetString(0) };
                        CodinSource.Add(row[0]);
                    }
                    reader.Close();
                }
                reader = Baja_equiposAsist.ListCatalogo(BajaDatosCon, Descripcion_BajaEquipos.Text);
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        string[] row = { reader.GetString(0) };
                        DescSource.Add(row[0]);
                    }
                }
                reader.Close();
            }
            BajaDatosCon.Close();
            MySqlConnection.ClearAllPools();

            CodIn_BajaEquipos.AutoCompleteMode = AutoCompleteMode.Suggest;
            CodIn_BajaEquipos.AutoCompleteSource = AutoCompleteSource.CustomSource;
            CodIn_BajaEquipos.AutoCompleteCustomSource = CodinSource;
            HostCodin_BajaEquipos.Child = CodIn_BajaEquipos;

            Descripcion_BajaEquipos.AutoCompleteMode = AutoCompleteMode.Suggest;
            Descripcion_BajaEquipos.AutoCompleteSource = AutoCompleteSource.CustomSource;
            Descripcion_BajaEquipos.AutoCompleteCustomSource = DescSource;
            HostDescripcion_BajaEquipos.Child = Descripcion_BajaEquipos;
        }

        private void Aceptar_BajaEquipos_Click(object sender, RoutedEventArgs e)
        {

            MySqlConnection BajaDatosCon = new MySqlConnection(Baja_equiposAsist.CadenaConexion());
            try
            {
                BajaDatosCon.Open();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Se perdio conexion con la base de datos\n\n{ex.Message}");
                this.Close();
            }
            if (BajaDatosCon.State == ConnectionState.Open)
            {
                try
                {
                    MySqlDataReader reader = Baja_equiposAsist.In_Baja(BajaDatosCon, CodIn_BajaEquipos.Text, Descripcion_BajaEquipos.Text, Marca_BajaEquipos.Text, Modelo.Text, NumeroSerie.Text, Responsable.Text);
                    reader.Close();
                    this.Close();
                }
                catch(Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error");
                    CodIn_BajaEquipos.Clear();
                    Marca_BajaEquipos.Clear();
                    Modelo.Clear();
                    NumeroSerie.Clear();
                    Responsable.Clear();
                    Descripcion_BajaEquipos.Clear();
                }
            }
            BajaDatosCon.Close();
            MySqlConnection.ClearPool(BajaDatosCon);
        }
    }
}
