using MySql.Data.MySqlClient;
using System;
using System.Data;
using System.Drawing;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Forms;
using MessageBox = System.Windows.MessageBox;
using Brushes = System.Windows.Media.Brushes;
using Brush = System.Windows.Media.Brush;
using System.Windows.Media;
using ColorWPF = System.Windows.Media.Color;
using Color = System.Drawing.Color;

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

        private void CodIn_NuevoRegistro_TextChanged(object sender, EventArgs e)
        {
            //Se quito auto est cadena conexion aqui
            MySqlConnection buscar = new MySqlConnection(RegistroAsist.CadenaConexion());
            try
            {
                buscar.Open();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"No hay conexion para la busqueda\n\n{ex.Message}");
            }
            if (buscar.State == ConnectionState.Open)
            {
                MySqlDataReader reader = RegistroAsist.ListPrestados(buscar, CodIn_NuevoRegistro.Text,false);
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        string[] row = { reader.GetString(0), reader.GetString(1), reader.GetString(2), reader.GetString(3), reader.GetString(4), reader.GetString(5), reader.GetString(6), reader.GetString(7), reader.GetString(8), reader.GetString(9), reader.GetString(10), reader.GetString(11) };
                        codinvSource.Add(row[0]);
                    }
                }
                reader.Close();
                reader = RegistroAsist.ListPrestadosVerification(buscar, CodIn_NuevoRegistro.Text);
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        string[] row = { reader.GetString(0), reader.GetString(1), reader.GetString(2), reader.GetString(3), reader.GetString(4), reader.GetString(5), reader.GetString(6), reader.GetString(7), reader.GetString(8), reader.GetString(9), reader.GetString(10), reader.GetString(11) };
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
                        TimeSpan resultado = DevolucionDP_NuevoRegistro.SelectedDate.Value - DateTime.Today;
                        if (resultado.Days > 0)
                        {
                            LogEstado_NuevoRegistro.Content = $"Faltan {resultado.Days.ToString()} dia(s)\n para ser devuelto";
                        }
                        else
                        {
                            if (resultado.Days == 0)
                            {
                                LogEstado_NuevoRegistro.Content = "ESTE OBJETO \nYA DEBE SER DEVUELTO";
                            }
                            else
                            {
                                LogEstado_NuevoRegistro.Content = "ESTE OBJETO \nYA DEBIO SER DEVUELTO";
                            }

                        }
                    }
                }
                else
                {
                    LogEstado_NuevoRegistro.Content = "";
                }
                reader.Close();
            }
            datos.Clear();
            buscar.Close();
            MySqlConnection.ClearPool(buscar);
        }

        private void CatalogoSBN_NuevoRegistro_TextChanged(object sender, TextChangedEventArgs e)
        {
                CatalogoList_NuevoRegistro.Visibility = Visibility.Visible;
                CatalogoList_NuevoRegistro.Items.Clear();

            //Aqui tambien se quito auto Set cadena de conexion

            MySqlConnection connection = new MySqlConnection(RegistroAsist.CadenaConexion());
            try
            {
                connection.Open();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            if (connection.State == ConnectionState.Open)
            {
                MySqlDataReader reader = RegistroAsist.ListCatalogo(connection, CatalogoSBN_NuevoRegistro.Text);

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        string[] row = { reader.GetString(0) };

                        CatalogoList_NuevoRegistro.Items.Add(row[0]);
                    }
                }
                reader.Close();
            }
            //Se quito datos clear
            connection.Close();
            MySqlConnection.ClearPool(connection);
        }

        private void Prestamo_NuevoRegistro_Click(object sender, RoutedEventArgs e)
        {
            if (Prestamo_NuevoRegistro.IsChecked == true)
            {
                UsuarioPrestamo_NuevoRegistro.Enabled = true;
                DevolucionDP_NuevoRegistro.IsEnabled = true;
                EstadosItem_NuevoRegistro.SelectedIndex = 1;
                EstadosItem_NuevoRegistro.IsEnabled = false;
                PrestamoPanel_NuevoRegistro.Visibility = Visibility.Visible;
                EquipoPanel_NuevoRegistro.CornerRadius = new CornerRadius(20, 20, 0, 0);
            }
            else
            {
                UsuarioPrestamo_NuevoRegistro.Enabled = false;
                DevolucionDP_NuevoRegistro.IsEnabled = false;
                EstadosItem_NuevoRegistro.IsEnabled = true;
                PrestamoPanel_NuevoRegistro.Visibility = Visibility.Hidden;
                EquipoPanel_NuevoRegistro.CornerRadius = new CornerRadius(20,20,20,20);
            }
        }

        private void Panel_NuevoRegistro_Loaded(object sender, RoutedEventArgs e)
        {
            /*CatalogoSBN_NuevoRegistro.Text = "Escriba para recibir sugerencias";

            obs_RegistroNuevo.Text = "Si hay algo importante que saber del producto escribir aqui";*/
            RegistroAsist.AutoSetCadenaConexion(datos);
            CatalogoSBN_NuevoRegistro.Text = "Escriba para recibir sugerencias";
            CatalogoList_NuevoRegistro.Visibility = Visibility.Hidden;
            CatalogoSBN_NuevoRegistro.Foreground = Brushes.Gray;

            EstadosItem_NuevoRegistro.Items.Add("Entrada");
            EstadosItem_NuevoRegistro.Items.Add("Salida");
            CodIn_NuevoRegistro.Focus();

            DateTime NewDefault = DateTime.Now;
            TimePicker_NuevoRegistro.Text = $"{NewDefault.Hour.ToString()}:{NewDefault.ToString("mm")}";
            DatePicker_NuevoRegistro.SelectedDate = NewDefault.Date;

            DevolucionDP_NuevoRegistro.SelectedDate = NewDefault.AddDays(1);

            nuevo.Height = 23;
            nuevo.Width = 520;
            nuevo.AutoCompleteCustomSource = GetSource();
            nuevo.AutoCompleteMode = AutoCompleteMode.Suggest;
            nuevo.AutoCompleteSource = AutoCompleteSource.CustomSource;
            nuevo.BackColor = Color.FromArgb(255, 255, 255);
            hostResponsable.Child = nuevo;

            CodIn_NuevoRegistro.Height = 23;
            CodIn_NuevoRegistro.Width = 147;
            CodIn_NuevoRegistro.AutoCompleteCustomSource = codinvSource;
            CodIn_NuevoRegistro.AutoCompleteMode = AutoCompleteMode.Suggest;
            CodIn_NuevoRegistro.AutoCompleteSource = AutoCompleteSource.CustomSource;
            CodIn_NuevoRegistro.BackColor = Color.FromArgb(255,255,255);
            hostCodin.Child = CodIn_NuevoRegistro;
        }

        private AutoCompleteStringCollection GetSource()
        {
            AutoCompleteStringCollection salida = new AutoCompleteStringCollection();
            try
            {
                datos.ReadXml("bd.conf.xml", XmlReadMode.Auto);
                for (int x = 0; x < datos.Tables["Responsables"].Rows.Count; x++)
                {
                    salida.Add(datos.Tables["Responsables"].Rows[x]["Autocompletado"].ToString());
                }
                return salida;
            }
            catch
            {
                MessageBox.Show("No se agregaron responsables de ambiente al registro", "Primer uso");
                DataTable tabla = new DataTable("Responsables");
                tabla.Columns.Add(new DataColumn("Autocompletado", Type.GetType("System.String")));
                datos.Tables.Add(tabla);
                datos.WriteXml("bd.conf.xml", XmlWriteMode.WriteSchema);
                
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
                            RegistroAsist.AddResponsable(datos, nuevo.Text);
                            datos.Clear();
                            reader.Close();
                            break;
                        case 1:
                            reader = RegistroAsist.In_Salida(submit, CodIn_NuevoRegistro.Text, CatalogoSBN_NuevoRegistro.Text, Marca_NuevoRegistro.Text, Modelo_NuevoRegistro.Text, Nroserie_NuevoRegistro.Text, DatePicker_NuevoRegistro.SelectedDate.Value, TimePicker_NuevoRegistro.Text, nuevo.Text, obs_RegistroNuevo.Text);
                            RegistroAsist.AddResponsable(datos, nuevo.Text);
                            datos.Clear();
                            GetSharedData();
                            Registrar_qr nuevoObjeto = new Registrar_qr();
                            nuevoObjeto.Owner = this;
                            nuevoObjeto.Show();
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
                //

                if (Prestamo_NuevoRegistro.IsChecked == true)
                {
                    reader = RegistroAsist.ListPrestadosVerification(submit, CodIn_NuevoRegistro.Text);
                    if (reader.HasRows)
                    {
                        try
                        {
                            reader.Close();
                            reader = RegistroAsist.UpdatePrestados(submit, obs_RegistroNuevo.Text, CodIn_NuevoRegistro.Text, UsuarioPrestamo_NuevoRegistro.Text, DatePicker_NuevoRegistro.SelectedDate.Value, DevolucionDP_NuevoRegistro.SelectedDate.Value);
                            
                            ClearRegistro();
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show($"Verifique que no haya cambiado los datos principales\n\n{ex.Message}", "Error");
                        }
                        
                    }
                    else
                    {
                        reader.Close();
                        reader = RegistroAsist.In_Prestados(submit, CodIn_NuevoRegistro.Text, CatalogoSBN_NuevoRegistro.Text, Marca_NuevoRegistro.Text, Modelo_NuevoRegistro.Text, Nroserie_NuevoRegistro.Text, UsuarioPrestamo_NuevoRegistro.Text, DatePicker_NuevoRegistro.SelectedDate.Value, DevolucionDP_NuevoRegistro.SelectedDate.Value, TimePicker_NuevoRegistro.Text, nuevo.Text, obs_RegistroNuevo.Text);
                    }
                    //if(se encuentra registrado un producto se colocara como devuelto): Se marcara el producto como devuelto
                    reader.Close();
                }
            }
            catch (Exception ex)
            {
                _ = MessageBox.Show($"No se logro enviar los datos\n\n{ex.Message}", "Error");
            }
            ClearRegistro();
            submit.Close();
            MySqlConnection.ClearPool(submit);
        }

        private void GetSharedData()
        {
            SharedData.Codin = CodIn_NuevoRegistro.Text;
            SharedData.Marca = Marca_NuevoRegistro.Text;
            SharedData.Modelo = Modelo_NuevoRegistro.Text;
            SharedData.Serie = Nroserie_NuevoRegistro.Text;
        }

        private void ClearRegistro()
        {
            LogTittle_NuevoRegistro.Content = $"Equipo {CodIn_NuevoRegistro.Text} registrado correctamente";
            CodIn_NuevoRegistro.Text = "";
            nuevo.Text = "";
            EstadosItem_NuevoRegistro.IsEnabled = true;
            EstadosItem_NuevoRegistro.SelectedIndex = -1;
            CatalogoSBN_NuevoRegistro.Text = "";
            Marca_NuevoRegistro.Text = "";
            Modelo_NuevoRegistro.Text = "";
            Nroserie_NuevoRegistro.Text = "";
            UsuarioPrestamo_NuevoRegistro.Text = "";
            DevolucionDP_NuevoRegistro.SelectedDate = DateTime.Now.AddDays(1);
            obs_RegistroNuevo.Text = "";
            Prestamo_NuevoRegistro.IsChecked = false;
        }

        private void CatalogoList_NuevoRegistro_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (CatalogoList_NuevoRegistro.SelectedItem != null)
            {
                CatalogoSBN_NuevoRegistro.Text = CatalogoList_NuevoRegistro.SelectedItem.ToString();
            }
        }

        private void CodIn_NuevoRegistro_GotFocus(object sender, EventArgs e)
        {
            Brush Azulprofundo = new SolidColorBrush(ColorWPF.FromArgb(255,52, 76, 230));
            LB1Codin.Foreground = Brushes.White;
            LB1Codin.Background = Azulprofundo;// FF344CE6
        }

        private void CodIn_NuevoRegistro_LostFocus(object sender, EventArgs e)
        {
            Brush ModoNormal = new SolidColorBrush(ColorWPF.FromArgb(255, 127, 144, 255));
            LB1Codin.Foreground = Brushes.Black;
            LB1Codin.Background = ModoNormal;//FF7F90FF
        }

        private void obs_RegistroNuevo_GotFocus(object sender, RoutedEventArgs e)
        {
            if (obs_RegistroNuevo.Text == "Si hay algo importante que saber del producto escribir aqui")
            {
                obs_RegistroNuevo.Text = "";
                obs_RegistroNuevo.Foreground = Brushes.Black;
            }
        }

        private void obs_RegistroNuevo_LostFocus(object sender, RoutedEventArgs e)
        {
            if (obs_RegistroNuevo.Text == "")
            {
                obs_RegistroNuevo.Text = "Si hay algo importante que saber del producto escribir aqui";
                obs_RegistroNuevo.Foreground = Brushes.Gray;
            }
        }

        private void CatalogoSBN_NuevoRegistro_LostFocus(object sender, RoutedEventArgs e)
        {
            
            if (CatalogoSBN_NuevoRegistro.Text == "")
            {
                CatalogoSBN_NuevoRegistro.Text = "Escriba para recibir sugerencias";
                CatalogoSBN_NuevoRegistro.Foreground = Brushes.Gray;
            }
            CatalogoList_NuevoRegistro.Visibility = Visibility.Hidden;
        }

        private void CatalogoSBN_NuevoRegistro_GotFocus(object sender, RoutedEventArgs e)
        {
            if (CatalogoSBN_NuevoRegistro.Text == "Escriba para recibir sugerencias")
            {
                CatalogoSBN_NuevoRegistro.Text = "";
                CatalogoSBN_NuevoRegistro.Foreground = Brushes.Black;
            }
        }
    }
}
