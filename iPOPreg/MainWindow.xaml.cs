using MySql.Data.MySqlClient;
using System;
using System.Data;
using System.Windows;
using System.Windows.Input;

namespace iPOPreg
{
    /// <summary>
    /// Lógica de interacción para MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        private DataSet datos = new DataSet("MiBaseDatos");
        private BDasistente loginAsist = new BDasistente();

        private void Salir_Login_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Min_Login_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }

        private void Verificar_Login_Click(object sender, RoutedEventArgs e)
        {
            loginAsist.AutoSetCadenaConexion(datos);

            MySqlConnection iniciar = new MySqlConnection(loginAsist.CadenaConexion());
            try
            {
                iniciar.Open();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error de conexion");
            }
            if (iniciar.State == ConnectionState.Open)
            {
                MySqlDataReader reader = loginAsist.ListUserVerification(iniciar, User_Login.Text, Psw_Login.Password);
                if (reader.HasRows)
                {
                    
                    datos.Clear();
                    Panel_control ingreso = new Panel_control();
                    ingreso.Show();
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Usuario o contraseña incorrectos, verifique que esta registrado", "No registrado");
                }
            }
        }

        private void BordeTit_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }

        private void Panel_Login_Loaded(object sender, RoutedEventArgs e)
        {

            User_Login.Focus();
            try
            {
                loginAsist.AutoSetCadenaConexion(datos);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Se creara una nueva tabla de datos de conexion de la Base de Datos\n\n{ex.Message}", "No se encontro el Archivo de Conexión");
                //datos se carga de XML
                
                loginAsist.ConfigDefault(datos);
                loginAsist.AutoSetCadenaConexion(datos);
            }

            MySqlConnection Login_Con = new MySqlConnection(loginAsist.CadenaConexion());
            try
            {
                Login_Con.Open();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            if (Login_Con.State == ConnectionState.Open)
            {
                MySqlDataReader reader = loginAsist.ListUser(Login_Con);
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        string[] row = { reader.GetString(0), reader.GetString(1) };
                        UserList_Login.Items.Add($"{row[0]} {row[1]}");
                    }
                }
            }
            Login_Con.Close();
            MySqlConnection.ClearPool(Login_Con);
        }

        private void RefreshList_Login_Click(object sender, RoutedEventArgs e)
        {
            UserList_Login.Items.Clear();
            string datasource = loginAsist.Datasource(datos);
            string port = loginAsist.Port(datos);
            string username = loginAsist.Username(datos);
            string password = loginAsist.Password(datos);
            string database = loginAsist.Database(datos);
            loginAsist.SetCadenaConexion(datasource, port, username, password, database);

            MySqlConnection Login_Con = new MySqlConnection(loginAsist.CadenaConexion());
            try
            {
                Login_Con.Open();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            if (Login_Con.State == ConnectionState.Open)
            {
                MySqlDataReader reader = loginAsist.ListUser(Login_Con);
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        string[] row = { reader.GetString(0), reader.GetString(1) };
                        UserList_Login.Items.Add($"{row[0]} {row[1]}");
                    }
                }
            }
            Login_Con.Close();
            MySqlConnection.ClearPool(Login_Con);
        }
    }
}
