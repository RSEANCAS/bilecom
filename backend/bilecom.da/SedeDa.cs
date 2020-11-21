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
        public List<SedeBe> fListar(SqlConnection cn, int empresaId)
        {
            List<SedeBe> lSede = new List<SedeBe>();
            SedeBe oSede;
            using (SqlCommand oCommand = new SqlCommand("dbo.usp_sede_listar", cn))
            {
                oCommand.CommandType = CommandType.StoredProcedure;
                oCommand.Parameters.AddWithValue("@empresaId", empresaId);
                using (SqlDataReader oDr = oCommand.ExecuteReader())
                {
                    if (oDr.HasRows)
                    {
                        if (oDr.Read())
                        {
                            oSede = new SedeBe();
                            oSede.SedeId = oDr.GetData<int>("SedeId");
                            oSede.EmpresaId = oDr.GetData<int>("EmpresaId");
                            oSede.TipoSedeId = oDr.GetData<int>("TipoSedeId");
                            oSede.Nombre = oDr.GetData<string>("Nombre");
                            oSede.DistritoId = oDr.GetData<int>("DistritoId");
                            oSede.Direccion = oDr.GetData<string>("Direccion");
                            oSede.FlagActivo = oDr.GetData<bool>("FlagActivo");
                        }
                    }
                }
            }
            return lSede;
        }
    }
}
