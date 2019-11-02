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
        private string conectionString;
        private string table;
        private string query;

        public string CadenaConexion()
        {
            return conectionString;
        }

        public void SetCadenaConexion(string newCadena)
        {
            conectionString = newCadena;
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
