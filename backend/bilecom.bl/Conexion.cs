using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bilecom.bl
{
    public class Conexion
    {
        static string NombreConexion = ConfigurationManager.AppSettings["NombreConexion"];
        static string CadenaConexion = ConfigurationManager.ConnectionStrings[NombreConexion].ConnectionString;
        protected SqlConnection cn = new SqlConnection(CadenaConexion);
    }
}
