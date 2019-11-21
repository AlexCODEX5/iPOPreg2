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
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using MySql.Data.MySqlClient;

namespace iPOPreg
{
    /// <summary>
    /// Lógica de interacción para Panel_control.xaml
    /// </summary>
    public partial class Panel_control : Window
    {
        
        public Panel_control()
        {
            InitializeComponent();
        }
        BDasistente panelAsist = new BDasistente();
        DataSet datos = new DataSet();

        private void Panel_PControl_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }

        private void Logout_Panel_Click(object sender, RoutedEventArgs e)
        {
            MainWindow logout = new MainWindow();
            logout.Show();
            this.Close();
        }

        private void InsertarQR_Panel_Click(object sender, RoutedEventArgs e)
        {
            Registrar_qr registroQR = new Registrar_qr();
            registroQR.Show();
            registroQR.Owner = this;
        }

        private void RegistroEq_Panel_Click(object sender, RoutedEventArgs e)
        {
            NuevoRegistro nuevoRegistro = new NuevoRegistro();
            nuevoRegistro.Owner = this;
            nuevoRegistro.Show();
        }

        private void Panel_PControl_Loaded(object sender, RoutedEventArgs e)
        {
            if (!(System.IO.File.Exists("bd.conf.xml")))

            {
                MessageBox.Show("Su sesion termina aqui\n\nNo se encontro el archivo bd.conf.xml");
                MainWindow logout = new MainWindow();
                logout.Show();
                this.Close();
            }
            else
            {
                panelAsist.AutoSetCadenaConexion(datos);
            }
        }

        private void Prestado_Panel_Click(object sender, RoutedEventArgs e)
        {
            Visualizador_Panel.Columns.Clear();
            Visualizador_Panel.Items.Clear();
            DataGridTextColumn dataColumn = new DataGridTextColumn
            {
                Header = "N° INVENTARIO",
                Binding = new Binding("NroInventario"),
                Width = 95
            };
            Visualizador_Panel.Columns.Add(dataColumn);

            DataGridTextColumn dataColumn2 = new DataGridTextColumn
            {
                Header = "DESCRIPCION",
                Binding = new Binding("Descripcion"),
                Width = 130
            };
            Visualizador_Panel.Columns.Add(dataColumn2);

            DataGridTextColumn dataColumn3 = new DataGridTextColumn
            {
                Header = "MARCA",
                Binding = new Binding("Marca"),
                Width = 95
            };
            Visualizador_Panel.Columns.Add(dataColumn3);

            DataGridTextColumn dataColumn4 = new DataGridTextColumn
            {
                Header = "MODELO",
                Binding = new Binding("Modelo"),
                Width = 95
            };
            Visualizador_Panel.Columns.Add(dataColumn4);

            DataGridTextColumn dataColumn5 = new DataGridTextColumn
            {
                Header = "N° SERIE",
                Binding = new Binding("NroSerie"),
                Width = 95
            };
            Visualizador_Panel.Columns.Add(dataColumn5);

            DataGridTextColumn dataColumn6 = new DataGridTextColumn
            {
                Header = "USUARIO\nRESPONSABLE",
                Binding = new Binding("UsuarioResp"),
                Width = 95
            };
            Visualizador_Panel.Columns.Add(dataColumn6);

            DataGridTextColumn dataColumn7 = new DataGridTextColumn
            {
                Header = "FECHA\nSALIDA",
                Binding = new Binding("FechaSalida"),
                Width = 70
            };
            Visualizador_Panel.Columns.Add(dataColumn7);

            DataGridTextColumn dataColumn8 = new DataGridTextColumn
            {
                Header = "HORA",
                Binding = new Binding("Hora"),
                Width = 45
            };
            Visualizador_Panel.Columns.Add(dataColumn8);

            DataGridTextColumn dataColumn9 = new DataGridTextColumn
            {
                Header = "PERSONAL\nDE ATENCIÓN",
                Binding = new Binding("Personal"),
                Width = 110
            };
            Visualizador_Panel.Columns.Add(dataColumn9);

            DataGridTextColumn dataColumn10 = new DataGridTextColumn
            {
                Header = "FECHA\nDEVOLUCIÓN",
                Binding = new Binding("FechaDevolucion"),
                Width = 60
            };
            Visualizador_Panel.Columns.Add(dataColumn10);

            DataGridTextColumn dataColumn11 = new DataGridTextColumn
            {
                Header = "DEVUELTO",
                Binding = new Binding("Devuelto"),
                Width = 60
            };
            Visualizador_Panel.Columns.Add(dataColumn11);

            DataGridTextColumn dataColumn12 = new DataGridTextColumn
            {
                Header = "OBSERVACIONES",
                Binding = new Binding("Observaciones"),
                Width = 160
            };
            Visualizador_Panel.Columns.Add(dataColumn12);

            MySqlConnection panelCon = new MySqlConnection(panelAsist.CadenaConexion());
            try
            {
                panelCon.Open();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error");
            }
            if (panelCon.State == ConnectionState.Open)
            {
                MySqlDataReader reader = panelAsist.ListPrestados(panelCon,"",true);
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        string[] row = { reader.GetString(0), reader.GetString(1), reader.GetString(2), reader.GetString(3), reader.GetString(4), reader.GetString(5), reader.GetString(6), reader.GetString(7), reader.GetString(8), reader.GetString(9), reader.GetString(10), reader.GetString(11) };
                        var data = new SelectItem {
                            NroInventario = row[0],
                            Descripcion = row[1],
                            Marca = row[2],
                            Modelo = row[3],
                            NroSerie = row[4],
                            UsuarioResp = row[5],
                            FechaSalida = DateTime.Parse(row[6]).ToString("yyyy-MM-dd"),
                            Hora = row[7],
                            Personal = row[8],
                            FechaDevolucion = row[9],
                            Devuelto = row[10],
                            Observaciones = row[11]
                        };
                        Visualizador_Panel.Items.Add(data);
                    }
                }
                reader.Close();
            }

            panelCon.Close();
            MySqlConnection.ClearPool(panelCon);
        }

        private void Salida_Panel_Click(object sender, RoutedEventArgs e)
        {
            Visualizador_Panel.Columns.Clear();
            Visualizador_Panel.Items.Clear();
            DataGridTextColumn dataColumn = new DataGridTextColumn
            {
                Header = "N° INVENTARIO",
                Binding = new Binding("NroInventario"),
                Width = 95
            };
            Visualizador_Panel.Columns.Add(dataColumn);

            DataGridTextColumn dataColumn2 = new DataGridTextColumn
            {
                Header = "DESCRIPCION",
                Binding = new Binding("Descripcion"),
                Width = 130
            };
            Visualizador_Panel.Columns.Add(dataColumn2);

            DataGridTextColumn dataColumn3 = new DataGridTextColumn
            {
                Header = "MARCA",
                Binding = new Binding("Marca"),
                Width = 95
            };
            Visualizador_Panel.Columns.Add(dataColumn3);

            DataGridTextColumn dataColumn4 = new DataGridTextColumn
            {
                Header = "MODELO",
                Binding = new Binding("Modelo"),
                Width = 95
            };
            Visualizador_Panel.Columns.Add(dataColumn4);

            DataGridTextColumn dataColumn5 = new DataGridTextColumn
            {
                Header = "N° SERIE",
                Binding = new Binding("NroSerie"),
                Width = 110
            };
            Visualizador_Panel.Columns.Add(dataColumn5);

            DataGridTextColumn dataColumn7 = new DataGridTextColumn
            {
                Header = "FECHA\nSALIDA",
                Binding = new Binding("FechaSalida"),
                Width = 70
            };
            Visualizador_Panel.Columns.Add(dataColumn7);

            DataGridTextColumn dataColumn8 = new DataGridTextColumn
            {
                Header = "HORA",
                Binding = new Binding("Hora"),
                Width = 45
            };
            Visualizador_Panel.Columns.Add(dataColumn8);

            DataGridTextColumn dataColumn9 = new DataGridTextColumn
            {
                Header = "PERSONAL\nDE ATENCIÓN",
                Binding = new Binding("Personal"),
                Width = 140
            };
            Visualizador_Panel.Columns.Add(dataColumn9);

            DataGridTextColumn dataColumn12 = new DataGridTextColumn
            {
                Header = "OBSERVACIONES",
                Binding = new Binding("Observaciones"),
                Width = 180
            };
            Visualizador_Panel.Columns.Add(dataColumn12);

            MySqlConnection panelCon = new MySqlConnection(panelAsist.CadenaConexion());
            try
            {
                panelCon.Open();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error");
            }
            if (panelCon.State == ConnectionState.Open)
            {
                MySqlDataReader reader = panelAsist.ListSalida(panelCon);
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        string[] row = { reader.GetString(0), reader.GetString(1), reader.GetString(2), reader.GetString(3), reader.GetString(4), reader.GetString(5), reader.GetString(6), reader.GetString(7), reader.GetString(8) };
                        var data = new SelectItem
                        {
                            NroInventario = row[0],
                            Descripcion = row[1],
                            Marca = row[2],
                            Modelo = row[3],
                            NroSerie = row[4],
                            FechaSalida = DateTime.Parse(row[5]).ToString("yyyy-MM-dd"),
                            Hora = row[6],
                            Personal = row[7],
                            Observaciones = row[8]
                        };
                        Visualizador_Panel.Items.Add(data);
                    }
                }
                reader.Close();
            }

            panelCon.Close();
            MySqlConnection.ClearPool(panelCon);
        }

        private void Entrada_Panel_Click(object sender, RoutedEventArgs e)
        {
            Visualizador_Panel.Columns.Clear();
            Visualizador_Panel.Items.Clear();
            DataGridTextColumn dataColumn = new DataGridTextColumn
            {
                Header = "N° INVENTARIO",
                Binding = new Binding("NroInventario"),
                Width = 95
            };
            Visualizador_Panel.Columns.Add(dataColumn);

            DataGridTextColumn dataColumn2 = new DataGridTextColumn
            {
                Header = "DESCRIPCION",
                Binding = new Binding("Descripcion"),
                Width = 130
            };
            Visualizador_Panel.Columns.Add(dataColumn2);

            DataGridTextColumn dataColumn3 = new DataGridTextColumn
            {
                Header = "MARCA",
                Binding = new Binding("Marca"),
                Width = 95
            };
            Visualizador_Panel.Columns.Add(dataColumn3);

            DataGridTextColumn dataColumn4 = new DataGridTextColumn
            {
                Header = "MODELO",
                Binding = new Binding("Modelo"),
                Width = 95
            };
            Visualizador_Panel.Columns.Add(dataColumn4);

            DataGridTextColumn dataColumn5 = new DataGridTextColumn
            {
                Header = "N° SERIE",
                Binding = new Binding("NroSerie"),
                Width = 110
            };
            Visualizador_Panel.Columns.Add(dataColumn5);

            DataGridTextColumn dataColumn7 = new DataGridTextColumn
            {
                Header = "FECHA\nENTRADA",
                Binding = new Binding("FechaEntrada"),
                Width = 70
            };
            Visualizador_Panel.Columns.Add(dataColumn7);

            DataGridTextColumn dataColumn8 = new DataGridTextColumn
            {
                Header = "HORA",
                Binding = new Binding("Hora"),
                Width = 45
            };
            Visualizador_Panel.Columns.Add(dataColumn8);

            DataGridTextColumn dataColumn9 = new DataGridTextColumn
            {
                Header = "PERSONAL\nDE ATENCIÓN",
                Binding = new Binding("Personal"),
                Width = 140
            };
            Visualizador_Panel.Columns.Add(dataColumn9);

            DataGridTextColumn dataColumn12 = new DataGridTextColumn
            {
                Header = "OBSERVACIONES",
                Binding = new Binding("Observaciones"),
                Width = 180
            };
            Visualizador_Panel.Columns.Add(dataColumn12);

            MySqlConnection panelCon = new MySqlConnection(panelAsist.CadenaConexion());
            try
            {
                panelCon.Open();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message,"Error");
            }
            if(panelCon.State == ConnectionState.Open)
            {
                MySqlDataReader reader = panelAsist.ListEntrada(panelCon);
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        string[] row = { reader.GetString(0), reader.GetString(1), reader.GetString(2), reader.GetString(3), reader.GetString(4), reader.GetString(5), reader.GetString(6), reader.GetString(7), reader.GetString(8) };
                        var data = new SelectItem
                        {
                            NroInventario = row[0],
                            Descripcion = row[1],
                            Marca = row[2],
                            Modelo = row[3],
                            NroSerie = row[4],
                            FechaEntrada = DateTime.Parse(row[5]).ToString("yyyy-MM-dd"),
                            Hora = row[6],
                            Personal = row[7],
                            Observaciones = row[8]
                        };
                        Visualizador_Panel.Items.Add(data);
                    }
                }
                reader.Close();
            }

            panelCon.Close();
            MySqlConnection.ClearPool(panelCon);
        }
    }
    public class SelectItem
    {
        public string Personal { get; set; }

        public string NroInventario { get; set; }
        public string Descripcion { get; set; }
        public string Marca { get;  set; }
        public string Modelo { get; set; }
        public string NroSerie { get; set; }
        public string Observaciones { get; set; }
        public string Devuelto { get; set; }
        public string FechaDevolucion { get; set; }
        public string FechaSalida { get; set; }
        public string UsuarioResp { get; set; }
        public string Hora { get; set; }
        public string FechaEntrada { get; set; }
    }
}
