using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data;
using MySql.Data.MySqlClient;

namespace iPOPreg
{
    class BDasistente
    {
        public static string Encriptar(string _cadenaAencriptar)
        {
            string result = string.Empty;
            byte[] encryted = System.Text.Encoding.Unicode.GetBytes(_cadenaAencriptar);
            result = Convert.ToBase64String(encryted);
            return result;
        }

        public static string DesEncriptar(string _cadenaAdesencriptar)
        {
            string result = string.Empty;
            byte[] decryted = Convert.FromBase64String(_cadenaAdesencriptar);
            //result = System.Text.Encoding.Unicode.GetString(decryted, 0, decryted.ToArray().Length);
            result = System.Text.Encoding.Unicode.GetString(decryted);
            return result;
        }

        private string conectionString;
        private string table;
        private string query;

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

    }
}
