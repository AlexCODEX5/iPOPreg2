using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data;
using MySql.Data.MySqlClient;

namespace iPOPreg
{
    class BDasistente
    {
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

        public MySqlDataReader In_Prestados(MySqlConnection conexion, string nroinvent, string descripcion , string marca, string modelo, string nroserie, string usuarioresp, DateTime fechasalida, DateTime fechadevolucion, string hora, string personal)
        {
            query = $"INSERT INTO `equipos_prestados`(`Nro_inventario`, `descripcion`, `marca`, `modelo`, `nro_serie`, `usuario_responsable`, `fecha_salida`, `hora`, `personal_atencion`, `fecha_devolucion`) VALUES ('{nroinvent}','{descripcion}','{marca}','{modelo}','{nroserie}','{usuarioresp}','{fechasalida.Date.ToString("DD/MM/AAAA")}', '{hora}', '{personal}','{fechadevolucion.Date.ToString("DD/MM/AAAA")}'";
            MySqlCommand comando = new MySqlCommand(query, conexion);
            MySqlDataReader reader = comando.ExecuteReader();
           
            return reader;
        }

        public MySqlDataReader In_Salida(MySqlConnection conexion, string nroinvent, string descripcion, string marca, string modelo, string nroserie, string usuarioresp, DateTime fechasalida, string hora, string personal)
        {
            query = $"INSERT INTO `equipos_salida`(`Nro_inventario`, `descripcion`, `marca`, `modelo`, `nro_serie`, `usuario_responsable`, `fecha_salida`, `hora`, `personal_atencion`) VALUES ('{nroinvent}','{descripcion}','{marca}','{modelo}','{nroserie}','{usuarioresp}','{fechasalida.Date.ToString("dd/mm/aaaa")}','{hora}','{personal}')";
            MySqlCommand comando = new MySqlCommand(query,conexion);
            MySqlDataReader reader = comando.ExecuteReader();
            return reader;
        }

        public MySqlDataReader In_Entrada(MySqlConnection conexion, string nroinvent, string descripcion, string marca, string modelo, string nroserie, string usuarioresp, DateTime fechasalida, string hora, string personal)
        {
            query = $"INSERT INTO `equipos_salida`(`Nro_inventario`, `descripcion`, `marca`, `modelo`, `nro_serie`, `usuario_responsable`, `fecha_salida`, `hora`, `personal_atencion`) VALUES ('{nroinvent}','{descripcion}','{marca}','{modelo}','{nroserie}','{usuarioresp}','{fechasalida.Date.ToString("dd/mm/aaaa")}','{hora}','{personal}')";
            MySqlCommand comando = new MySqlCommand(query, conexion);
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
    }
}
