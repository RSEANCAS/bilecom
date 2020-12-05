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
    public class ProvinciaDa
    {
        public List<ProvinciaBe> ProvinciaListar(SqlConnection cn)
        {
            List<ProvinciaBe> respuesta = null;
            try
            {
                using (SqlCommand oCommand = new SqlCommand("dbo.usp_provincia_listar", cn))
                {
                    oCommand.CommandType = CommandType.StoredProcedure;
                    using (SqlDataReader oDr = oCommand.ExecuteReader())
                    {
                        if (oDr.HasRows)
                        {
                            respuesta = new List<ProvinciaBe>();
                            while (oDr.Read())
                            {
                                respuesta.Add(new ProvinciaBe
                                {
                                    DepartamentoId = oDr.GetData<int>("DepartamentoId"),
                                    ProvinciaId = oDr.GetData<int>("ProvinciaId"),
                                    CodigoUbigeo = oDr.GetData<string>("CodigoUbigeo"),
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
