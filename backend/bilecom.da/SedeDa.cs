using bilecom.be;
using bilecom.ut;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bilecom.da
{
    public class SedeDa
    {
        public List<SedeBe> fListar(SqlConnection cn, int empresaId, string nombre, int pagina, int cantidadRegistros, string columnaOrden, string ordenMax, out int totalRegistros)
        {
            totalRegistros = 0;
            List<SedeBe> lSede = new List<SedeBe>();
            SedeBe oSede;
            using (SqlCommand oCommand = new SqlCommand("dbo.usp_sede_listar", cn))
            {
                oCommand.CommandType = CommandType.StoredProcedure;
                oCommand.Parameters.AddWithValue("@empresaId", empresaId.GetNullable());
                oCommand.Parameters.AddWithValue("@nombre", nombre.GetNullable());
                oCommand.Parameters.AddWithValue("@pagina", pagina.GetNullable());
                oCommand.Parameters.AddWithValue("@cantidadRegistros", cantidadRegistros.GetNullable());
                oCommand.Parameters.AddWithValue("@columnaOrden", columnaOrden.GetNullable());
                oCommand.Parameters.AddWithValue("@ordenMax", ordenMax.GetNullable());
                using (SqlDataReader oDr = oCommand.ExecuteReader())
                {
                    if (oDr.HasRows)
                    {
                        while (oDr.Read())
                        {
                            oSede = new SedeBe();
                            oSede.SedeId = oDr.GetData<int>("Fila");
                            //oSede.SedeId = oDr.GetData<int>("SedeId");
                            oSede.EmpresaId = oDr.GetData<int>("EmpresaId");
                            oSede.TipoSedeId = oDr.GetData<int>("TipoSedeId");
                            oSede.TipoSede = new TipoSedeBe();
                            oSede.TipoSede.Nombre = oDr.GetData<string>("TipoSedeNombre");
                            oSede.Nombre = oDr.GetData<string>("NombreSede");
                            oSede.DistritoId = oDr.GetData<int>("DistritoId");
                            oSede.Direccion = oDr.GetData<string>("Direccion");
                            oSede.FlagActivo = oDr.GetData<bool>("SedeActivo");
                            lSede.Add(oSede);
                            if (!DBNull.Value.Equals(oDr["Total"])) totalRegistros = (int)oDr["Total"];
                        }
                    }
                }
            }
            return lSede;
        }
    }
}
