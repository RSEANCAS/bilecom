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
        public List<TipoSedeBe> fListar(SqlConnection cn, int empresaId)
        {
            List<TipoSedeBe> lTipoSede = new List<TipoSedeBe>();
            TipoSedeBe oTipoSede;

            using (SqlCommand oCommand = new SqlCommand("dbo.usp_tiposede_Listar", cn))
            {
                oCommand.CommandType = CommandType.StoredProcedure;
                oCommand.Parameters.AddWithValue("@empresaId", empresaId.GetNullable());
                using (SqlDataReader oDr = oCommand.ExecuteReader())
                {
                    if (oDr.HasRows)
                    {
                        if (oDr.Read())
                        {
                            oTipoSede = new TipoSedeBe();
                            oTipoSede.SedeId = oDr.GetData<int>("SedeId");
                            oTipoSede.EmpresaId = oDr.GetData<int>("EmpresaId");
                            oTipoSede.Nombre = oDr.GetData<string>("Nombre");
                            oTipoSede.FlagActivo = oDr.GetData<bool>("FlagActivo");
                            lTipoSede.Add(oTipoSede);

                        }
                    }
                }
            }

            return lTipoSede;
        }
    }
}
