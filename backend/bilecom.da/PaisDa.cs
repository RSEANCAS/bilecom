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
    public class PaisDa
    {
        public List<PaisBe> PaisListar(SqlConnection cn)
        {
            List<PaisBe> respuesta = null;
            try
            {
                using (SqlCommand oCommand = new SqlCommand("dbo.usp_pais_listar", cn))
                {
                    oCommand.CommandType = CommandType.StoredProcedure;
                    using (SqlDataReader oDr = oCommand.ExecuteReader())
                    {
                        if (oDr.HasRows)
                        {
                            respuesta = new List<PaisBe>();
                            while (oDr.Read())
                            {
                                respuesta.Add(new PaisBe
                                {
                                    PaisId = oDr.GetData<int>("PaisId"),
                                    Nombre = oDr.GetData<string>("Nombre")
                                });
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                respuesta = null;
            }
            return respuesta;
        }
    }
}
