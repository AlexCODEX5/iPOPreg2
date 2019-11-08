using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using MySql.Data.MySqlClient;
using MySql.Data;
using MessageBox = System.Windows.MessageBox;
using TextBox = System.Windows.Forms.TextBox;

namespace iPOPreg
{
    /// <summary>
    /// Lógica de interacción para NuevoRegistro.xaml
    /// </summary>
    public partial class NuevoRegistro : Window
    {
        public NuevoRegistro()
        {
            InitializeComponent();
        }
        BDasistente RegistroAsist = new BDasistente();
        DataSet datos = new DataSet();
        AutoCompleteStringCollection responsablesSource = new AutoCompleteStringCollection();

        private void CatalogoSBN_NuevoRegistro_TextChanged(object sender, TextChangedEventArgs e)
        {
            CatalogoList_NuevoRegistro.Items.Clear();
            datos.ReadXml("bd.conf.xml", XmlReadMode.Auto);
            string datasource = RegistroAsist.Datasource(datos);
            string port = RegistroAsist.Port(datos);
            string username = RegistroAsist.Username(datos);
            string password = RegistroAsist.Password(datos);
            string database = RegistroAsist.Database(datos);
            RegistroAsist.SetCadenaConexion(datasource, port, username, password, database);

            MySqlConnection connection = new MySqlConnection(RegistroAsist.CadenaConexion());
            try
            {
                connection.Open();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            if(connection.State == ConnectionState.Open)
            {
                MySqlDataReader reader = RegistroAsist.ListCatalogo(connection,CatalogoSBN_NuevoRegistro.Text) ;

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        string[] row = { reader.GetString(0) };
                        CatalogoList_NuevoRegistro.Items.Add(row[0]);
                    }
                }
            }

            connection.Close();
            MySqlConnection.ClearPool(connection);
        }

        private void Prestamo_NuevoRegistro_Click(object sender, RoutedEventArgs e)
        {
            if(Prestamo_NuevoRegistro.IsChecked == true)
            {
                UsuarioPrestamo_NuevoRegistro.IsEnabled = true;
                DevolucionDP_NuevoRegistro.IsEnabled = true;
                AddResp_NuevoRegistro.IsEnabled = true;
                EstadosItem_NuevoRegistro.SelectedIndex = 1;
            }
            else
            {
                UsuarioPrestamo_NuevoRegistro.IsEnabled = false;
                DevolucionDP_NuevoRegistro.IsEnabled = false;
                AddResp_NuevoRegistro.IsEnabled = false;
            }
        }

        private void Panel_NuevoRegistro_Loaded(object sender, RoutedEventArgs e)
        {
            EstadosItem_NuevoRegistro.Items.Add("Entrada");
            EstadosItem_NuevoRegistro.Items.Add("Salida");

            DateTime NewDefault = new DateTime();
            NewDefault = DateTime.Now;
            TimePicker_NuevoRegistro.Text = $"{NewDefault.Hour.ToString()}:{NewDefault.Minute.ToString()}";
            DatePicker_NuevoRegistro.SelectedDate = NewDefault.Date;

            DevolucionDP_NuevoRegistro.SelectedDate = NewDefault.AddDays(1);

            nuevo.Height = 23;
            nuevo.Width = 520;
            nuevo.AutoCompleteCustomSource = GetSource();
            nuevo.AutoCompleteMode = AutoCompleteMode.Suggest;
            nuevo.AutoCompleteSource = AutoCompleteSource.CustomSource;
            hostResponsable.Child = nuevo;
        }

        private AutoCompleteStringCollection GetSource()
        {
            AutoCompleteStringCollection salida = new AutoCompleteStringCollection();
            try
            {
                datos.ReadXml("bd.conf.xml",XmlReadMode.Auto);
                for (int x = 0; x < datos.Tables["Responsables"].Rows.Count; x++)
                {
                    salida.Add(datos.Tables["Responsables"].Rows[x]["Autocompletado"].ToString());
                }
                return salida;
            }
            catch
            {
                MessageBox.Show("No se agregaron responsables de ambiente al registro","Primer uso");
                DataTable tabla = new DataTable("Responsables");
                tabla.Columns.Add(new DataColumn("Autocompletado", Type.GetType("System.String")));
                datos.Tables.Add(tabla);
                datos.WriteXml("bd.conf.xml",XmlWriteMode.WriteSchema);
                for (int x = 0; x < datos.Tables["Responsables"].Rows.Count; x++)
                {
                    salida.Add(datos.Tables["Responsables"].Rows[x]["Autocompletado"].ToString());
                }
                return salida;
            }
        }

        private void DevolucionDP_NuevoRegistro_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            DateTime fechaUsada = DateTime.Today;
            TimeSpan dias = DevolucionDP_NuevoRegistro.SelectedDate.Value - fechaUsada;
            string result;

            result = dias.Days.ToString();
            DisplayDev_NuevoRegistro.Content = $"Sera devuelto en {result} Día(s)";
        }

        private void Enviar_NuevoRegistrar_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
