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
    public class TipoSedeDa
    {
        public List<TipoSedeBe> fListar(SqlConnection cn, int empresaId, string nombre, int pagina, int cantidadRegistros, string columnaOrden, string ordenMax, out int totalRegistros)
        {
            totalRegistros = 0;
            List<TipoSedeBe> lTipoSede = new List<TipoSedeBe>();
            TipoSedeBe oTipoSede;

            using (SqlCommand oCommand = new SqlCommand("dbo.usp_tiposede_Listar", cn))
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
                            oTipoSede = new TipoSedeBe();
                            oTipoSede.TipoSedeId = oDr.GetData<int>("Fila");
                            oTipoSede.EmpresaId = oDr.GetData<int>("EmpresaId");
                            oTipoSede.Nombre = oDr.GetData<string>("Nombre");
                            oTipoSede.FlagActivo = oDr.GetData<bool>("FlagActivo");
                            lTipoSede.Add(oTipoSede);
                            if (!DBNull.Value.Equals(oDr["Total"])) totalRegistros = (int)oDr["Total"];

                        }
                    }
                }
            }

            return lTipoSede;
        }
    }
}
