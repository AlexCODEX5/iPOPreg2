using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using MySql.Data;
using MySql.Data.MySqlClient;

namespace iPOPreg
{
    class BDasistente
    {
        /*public class User
        {
            private string dni = "";
            private string nombre= "";
            private string apellido= "";
            private string ambiente = "";
            
            public void SetDni(string newdni)
            {
                dni = newdni;
            }
            public void SetNombre(string newnombre)
            {
                nombre = newnombre;
            }
            public void SetApellido(string newapellido)
            {
                apellido = newapellido;
            }
            public void SetAmbiente(string newambiente)
            {
                ambiente = newambiente;
            }
        }*/

        public string Encriptar(string _cadenaAencriptar)
        {
            string result = string.Empty;
            byte[] encryted = System.Text.Encoding.Unicode.GetBytes(_cadenaAencriptar);
            result = Convert.ToBase64String(encryted);
            return result;
        }

        public string DesEncriptar(string _cadenaAdesencriptar)
        {
            string result = string.Empty;
            byte[] decryted = Convert.FromBase64String(_cadenaAdesencriptar);
            //result = System.Text.Encoding.Unicode.GetString(decryted, 0, decryted.ToArray().Length);
            result = System.Text.Encoding.Unicode.GetString(decryted);
            return result;
        }

        private string conectionString;
        private string table;
        private string query = "SELECT";

        public string CadenaConexion()
        {
            return conectionString;
        }

        public void SetCadenaConexion(string datasource,string port, string username, string password,string database)
        {
            conectionString = $"datasource={datasource};port={port};username={username};password={password};database={database};";
        }

        public void AutoSetCadenaConexion(DataSet datos)
        {
            datos.ReadXml("bd.conf.xml", XmlReadMode.Auto);
            SetCadenaConexion(Datasource(datos),Port(datos), Username(datos), Password(datos), Database(datos));
        }
        
        public string CadenaTable()
        {
            return table;
        }

        public void SetCadenaTable(string newTable)
        {
            table = newTable;
        }

        public string CadenaQuery()
        {
            return query;
        }

        //reader
        public MySqlDataReader ListUserVerification(MySqlConnection conexion,string usuario, string contraseña)
        {
            query = $"SELECT `DNI`, `Usuario`, `CONTRASEÑA`, `ESTADO` FROM `docente` WHERE `Usuario`='{usuario}' AND `CONTRASEÑA`='{contraseña}'";
            MySqlCommand comando = new MySqlCommand(query, conexion);
            MySqlDataReader reader = comando.ExecuteReader();
            return reader;
        }
        
        public MySqlDataReader ListUser(MySqlConnection conexion)
        {
            query = "SELECT `Nombre`,`Apellidos` FROM `docente`";
            MySqlCommand comando = new MySqlCommand(query,conexion);
            comando.CommandTimeout = 60;
            MySqlDataReader reader;
            reader = comando.ExecuteReader();
            return reader;
        } 

        public MySqlDataReader ListCatalogo(MySqlConnection conexion, String indicio)
        {
            query = $"SELECT `Denominacion` FROM `catalogo` WHERE `Denominacion` LIKE '%{indicio}%'";
            MySqlCommand comando = new MySqlCommand(query, conexion);
            MySqlDataReader reader;
            comando.CommandTimeout = 60;
            reader = comando.ExecuteReader();
            return reader;
        }

        public MySqlDataReader ListPrestados(MySqlConnection conexion, string codinvent, bool consulta)
        {
            if (consulta)
            {
                query = $"SELECT * FROM `equipos_prestados` ORDER BY `fecha_salida` DESC";
            }
            else
            {
                query = $"SELECT * FROM `equipos_prestados` WHERE `Nro_inventario` LIKE '%{codinvent}%' AND `Devuelto`='no'";
            }
            MySqlCommand comando = new MySqlCommand(query,conexion);
            comando.CommandTimeout = 60;
            MySqlDataReader reader = comando.ExecuteReader();
            return reader;
        }

        public MySqlDataReader ListEntrada(MySqlConnection conexion)
        {
                query = $"SELECT * FROM `equipos_entrada` ORDER BY `fecha_entrada` DESC";
            MySqlCommand comando = new MySqlCommand(query, conexion);
            comando.CommandTimeout = 60;
            MySqlDataReader reader = comando.ExecuteReader();
            return reader;
        }

        public MySqlDataReader ListSalida(MySqlConnection conexion)
        {
            query = $"SELECT * FROM `equipos_salida` ORDER BY `fecha_salida` DESC";
            MySqlCommand comando = new MySqlCommand(query, conexion);
            comando.CommandTimeout = 60;
            MySqlDataReader reader = comando.ExecuteReader();
            return reader;
        }

        public MySqlDataReader ListPrestadosVerification(MySqlConnection conexion, string codinvent)
        {
            query =  $"SELECT * FROM `equipos_prestados` WHERE `Nro_inventario`='{codinvent}' AND `Devuelto`='no'";
            MySqlCommand comando = new MySqlCommand(query, conexion) ;
            comando.CommandTimeout = 60;
            MySqlDataReader reader = comando.ExecuteReader();
            return reader;
        }

        public MySqlDataReader UpdatePrestados(MySqlConnection conexion, string observaciones, string Nroinventario, string usuarioresp, DateTime fecha, DateTime fechaDevolucion)
        {
            CultureInfo ci = new CultureInfo("Es-es");
            query = $"UPDATE `equipos_prestados` SET `Devuelto`='si',`observaciones`='{observaciones}. Ha sido devuelto el {ci.DateTimeFormat.GetDayName(fecha.DayOfWeek)} {fecha.ToString("d")} a las {DateTime.Now.Hour.ToString()}:{DateTime.Now.ToString("mm")}' WHERE `Nro_inventario`='{Nroinventario}' AND `usuario_responsable`='{usuarioresp}' AND `fecha_devolucion`='{fechaDevolucion.ToString("yyyyMMdd")}' AND `observaciones`='{observaciones}'";
            MySqlCommand comando = new MySqlCommand(query,conexion);
            comando.CommandTimeout = 3;
            MySqlDataReader reader = comando.ExecuteReader();
            
            return reader;
        }

        public MySqlDataReader In_Prestados(MySqlConnection conexion, string nroinvent, string descripcion , string marca, string modelo, string nroserie, string usuarioresp, DateTime fechasalida, DateTime fechadevolucion, string hora, string personal, string observaciones)
        {
            query = $"INSERT INTO `equipos_prestados`(`Nro_inventario`, `descripcion`, `marca`, `modelo`, `nro_serie`, `usuario_responsable`, `fecha_salida`, `hora`, `personal_atencion`, `fecha_devolucion`, `observaciones`) VALUES ('{nroinvent}','{descripcion}','{marca}','{modelo}','{nroserie}','{usuarioresp}','{fechasalida.ToString("yyyyMMdd")}', '{hora}', '{personal}','{fechadevolucion.ToString("yyyyMMdd")}','{observaciones}')";
            MySqlCommand comando = new MySqlCommand(query, conexion);
            comando.CommandTimeout = 60;
            MySqlDataReader reader = comando.ExecuteReader();
           
            return reader;
        }

        public MySqlDataReader In_Salida(MySqlConnection conexion, string nroinvent, string descripcion, string marca, string modelo, string nroserie, DateTime fechasalida, string hora, string personal, string observaciones)
        {
            query = $"INSERT INTO `equipos_salida`(`Nro_inventario`, `descripcion`, `marca`, `modelo`, `nro_serie`, `fecha_salida`, `hora`, `personal_atencion`, `observaciones`) VALUES ('{nroinvent}','{descripcion}','{marca}','{modelo}','{nroserie}','{fechasalida.ToString("yyyyMMdd")}','{hora}','{personal}','{observaciones}')";
            MySqlCommand comando = new MySqlCommand(query,conexion);
            comando.CommandTimeout = 60;
            MySqlDataReader reader = comando.ExecuteReader();
            return reader;
        }

        public MySqlDataReader In_Entrada(MySqlConnection conexion, string nroinvent, string descripcion, string marca, string modelo, string nroserie, DateTime fechasalida, string hora, string personal, string observaciones)
        {
            query = $"INSERT INTO `equipos_entrada`(`Nro_inventario`, `descripcion`, `marca`, `modelo`, `nro_serie`, `fecha_entrada`, `hora`, `personal_atencion`, `observaciones`) VALUES ('{nroinvent}','{descripcion}','{marca}','{modelo}','{nroserie}','{fechasalida.ToString("yyyyMMdd")}','{hora}','{personal}','{observaciones}')";
            MySqlCommand comando = new MySqlCommand(query, conexion);
            comando.CommandTimeout = 60;
            MySqlDataReader reader = comando.ExecuteReader();
            return reader;
        }

        //configuracion
        public void ConfigDefault(DataSet datos) //NewConfig
        {
            DataTable tabla = new DataTable("MySQL");
                tabla.Columns.Add(new DataColumn("datasource", Type.GetType("System.String")));
                tabla.Columns.Add(new DataColumn("port", Type.GetType("System.String")));
                tabla.Columns.Add(new DataColumn("username", Type.GetType("System.String")));
                tabla.Columns.Add(new DataColumn("password", Type.GetType("System.String")));
                tabla.Columns.Add(new DataColumn("database", Type.GetType("System.String")));
                datos.Tables.Add(tabla);

                DataRow filaNueva = tabla.NewRow();
                filaNueva["datasource"] = "127.0.0.1";
                filaNueva["port"] = "3306";
                filaNueva["username"] = "root";
                filaNueva["password"] = "";
                filaNueva["database"] = "bd_ipopreg_iiee";
                tabla.Rows.Add(filaNueva);

            datos.WriteXml("bd.conf.xml", XmlWriteMode.WriteSchema);
        }

        public void AddResponsable(DataSet datos, string responsable)
        {
            datos.ReadXml("bd.conf.xml", XmlReadMode.Auto);
            bool posible = true;
            for (int x = 0; x < datos.Tables["Responsables"].Rows.Count; x++)
            {
                if (responsable == datos.Tables["Responsables"].Rows[x]["Autocompletado"].ToString())
                {
                    posible = false;
                }
            }
            if (posible)
            {
                DataRow nueva = datos.Tables["Responsables"].NewRow();
                nueva["Autocompletado"] = responsable;
                datos.Tables["Responsables"].Rows.Add(nueva);
                datos.WriteXml("bd.conf.xml", XmlWriteMode.WriteSchema);
            }
        }

        public string Datasource(DataSet datos)
        {
            return datos.Tables["MySQL"].Rows[0]["datasource"].ToString();
        }

        public string Port(DataSet datos)
        {
            return datos.Tables["MySQL"].Rows[0]["port"].ToString();
        }

        public string Username(DataSet datos)
        {
            return datos.Tables["MySQL"].Rows[0]["username"].ToString();
        }

        public string Password(DataSet datos)
        {
            return DesEncriptar(datos.Tables["MySQL"].Rows[0]["password"].ToString());
        }

        public string Database(DataSet datos)
        {
            return datos.Tables["MySQL"].Rows[0]["database"].ToString();
        }

        ~BDasistente(){

        }
    }
}
