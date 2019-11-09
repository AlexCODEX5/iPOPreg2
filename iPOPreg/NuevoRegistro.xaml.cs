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
        AutoCompleteStringCollection codinvSource = new AutoCompleteStringCollection();

        private void CatalogoSBN_NuevoRegistro_TextChanged(object sender, TextChangedEventArgs e)
        {
            CatalogoList_NuevoRegistro.Items.Clear();
            RegistroAsist.AutoSetCadenaConexion(datos);

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
                UsuarioPrestamo_NuevoRegistro.Enabled = true;
                DevolucionDP_NuevoRegistro.IsEnabled = true;
                AddResp_NuevoRegistro.IsEnabled = true;
                EstadosItem_NuevoRegistro.SelectedIndex = 1;
                EstadosItem_NuevoRegistro.IsEnabled = false;
            }
            else
            {
                UsuarioPrestamo_NuevoRegistro.Enabled = false;
                DevolucionDP_NuevoRegistro.IsEnabled = false;
                AddResp_NuevoRegistro.IsEnabled = false;
                EstadosItem_NuevoRegistro.IsEnabled = true;
            }
        }

        private void Panel_NuevoRegistro_Loaded(object sender, RoutedEventArgs e)
        {
            EstadosItem_NuevoRegistro.Items.Add("Entrada");
            EstadosItem_NuevoRegistro.Items.Add("Salida");

            DateTime NewDefault = DateTime.Now;
            TimePicker_NuevoRegistro.Text = $"{NewDefault.Hour.ToString()}:{NewDefault.ToString("mm")}";
            DatePicker_NuevoRegistro.SelectedDate = NewDefault.Date;

            DevolucionDP_NuevoRegistro.SelectedDate = NewDefault.AddDays(1);

            nuevo.Height = 23;
            nuevo.Width = 520;
            nuevo.AutoCompleteCustomSource = GetSource();
            nuevo.AutoCompleteMode = AutoCompleteMode.Suggest;
            nuevo.AutoCompleteSource = AutoCompleteSource.CustomSource;
            hostResponsable.Child = nuevo;

            CodIn_NuevoRegistro.Height = 23;
            CodIn_NuevoRegistro.Width = 147;
            CodIn_NuevoRegistro.AutoCompleteCustomSource = codinvSource;
            CodIn_NuevoRegistro.AutoCompleteMode = AutoCompleteMode.Suggest;
            CodIn_NuevoRegistro.AutoCompleteSource = AutoCompleteSource.CustomSource;
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
            RegistroAsist.AutoSetCadenaConexion(datos);
            MySqlConnection submit = new MySqlConnection(RegistroAsist.CadenaConexion());
            MySqlDataReader reader;
            try
            {
                submit.Open();
                if (EstadosItem_NuevoRegistro.SelectedItem != null)
                {
                    switch (EstadosItem_NuevoRegistro.SelectedIndex)
                    {
                        case 0:
                            reader = RegistroAsist.In_Entrada(submit, CodIn_NuevoRegistro.Text, CatalogoSBN_NuevoRegistro.Text, Marca_NuevoRegistro.Text, Modelo_NuevoRegistro.Text, Nroserie_NuevoRegistro.Text, DatePicker_NuevoRegistro.SelectedDate.Value, TimePicker_NuevoRegistro.Text, nuevo.Text, obs_RegistroNuevo.Text);
                            reader.Close();
                            break;
                        case 1:
                            reader = RegistroAsist.In_Salida(submit, CodIn_NuevoRegistro.Text, CatalogoSBN_NuevoRegistro.Text, Marca_NuevoRegistro.Text, Modelo_NuevoRegistro.Text, Nroserie_NuevoRegistro.Text, DatePicker_NuevoRegistro.SelectedDate.Value, TimePicker_NuevoRegistro.Text, nuevo.Text, obs_RegistroNuevo.Text);
                            reader.Close();
                            break;
                        default:
                            MessageBox.Show("Seleccione si va ser de salida o entrada");
                            break;
                    }
                    
                }
                else
                {
                    MessageBox.Show("Seleccione si va ser de salida o entrada");
                }
                
                if (Prestamo_NuevoRegistro.IsChecked == true)
                {
                    reader = RegistroAsist.In_Prestados(submit, CodIn_NuevoRegistro.Text, CatalogoSBN_NuevoRegistro.Text, Marca_NuevoRegistro.Text, Modelo_NuevoRegistro.Text, Nroserie_NuevoRegistro.Text, UsuarioPrestamo_NuevoRegistro.Text, DatePicker_NuevoRegistro.SelectedDate.Value, DevolucionDP_NuevoRegistro.SelectedDate.Value, TimePicker_NuevoRegistro.Text, nuevo.Text, obs_RegistroNuevo.Text);
                    //if(se encuentra registrado un producto se colocara como devuelto): Se marcara el producto como devuelto
                    reader.Close();
                }
                
                this.Close();
            }
            catch(Exception ex)
            {
                MessageBox.Show($"No se logro enviar los datos\n\n{ex.Message}", "Error");
            }
            
            submit.Close();
            MySqlConnection.ClearPool(submit);
        }

        private void CatalogoList_NuevoRegistro_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (CatalogoList_NuevoRegistro.SelectedItem != null)
            {
                CatalogoSBN_NuevoRegistro.Text = CatalogoList_NuevoRegistro.SelectedItem.ToString();
            }
        }

        private void CodIn_NuevoRegistro_TextChanged(object sender, TextChangedEventArgs e)
        {
            RegistroAsist.AutoSetCadenaConexion(datos);
            MySqlConnection buscar = new MySqlConnection(RegistroAsist.CadenaConexion());

            try
            {
                buscar.Open();
            }
            catch
            {
                MessageBox.Show("No hay conexion para la busqueda");
            }
            if (buscar.State == ConnectionState.Open)
            {
                MySqlDataReader reader = RegistroAsist.ListPrestados(buscar,CodIn_NuevoRegistro.Text) ;
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        string[] row = { reader.GetString(0), reader.GetString(1), reader.GetString(2), reader.GetString(3), reader.GetString(4), reader.GetString(5), reader.GetString(6), reader.GetString(7), reader.GetString(8), reader.GetString(9), reader.GetString(10), reader.GetString(11) };
                        codinvSource.Add(row[0]);
                        CatalogoSBN_NuevoRegistro.Text = row[1];
                        Marca_NuevoRegistro.Text = row[2];
                        Modelo_NuevoRegistro.Text = row[3];
                        Nroserie_NuevoRegistro.Text = row[4];
                        UsuarioPrestamo_NuevoRegistro.Text = row[5];
                        //DatePicker_NuevoRegistro.SelectedDate = DateTime.Parse(row[6]);
                        TimePicker_NuevoRegistro.Text = row[7];
                        nuevo.Text = row[8];
                        DevolucionDP_NuevoRegistro.SelectedDate = DateTime.Parse(row[9]);
                        if (row[10] == "no")
                        {
                            Prestamo_NuevoRegistro.IsChecked = true;
                            EstadosItem_NuevoRegistro.SelectedIndex = 0;
                        }
                        obs_RegistroNuevo.Text = row[11];
                        TimeSpan resultado = DateTime.Today - DevolucionDP_NuevoRegistro.SelectedDate.Value;
                        if (resultado.Days > 0)
                        {
                            LogEstado_NuevoRegistro.Content = $"Faltan {resultado.Days} para ser devuelto";
                        }
                        else
                        {
                            LogEstado_NuevoRegistro.Content = "ESTE OBJETO YA DEBIO SER DEVUELTO";
                        }
                    }
                }
            }

            buscar.Close();
            MySqlConnection.ClearPool(buscar);
        }
    }
}
