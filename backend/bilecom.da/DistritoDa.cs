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
    public class DistritoDa
    {
        public List<DistritoBe> DistritoListar(SqlConnection cn)
        {
            List<DistritoBe> respuesta = null;
            try
            {
                using (SqlCommand oCommand = new SqlCommand("dbo.usp_distrito_listar", cn))
                {
                    oCommand.CommandType = CommandType.StoredProcedure;
                    using (SqlDataReader oDr = oCommand.ExecuteReader())
                    {
                        if (oDr.HasRows)
                        {
                            respuesta = new List<DistritoBe>();
                            while (oDr.Read())
                            {
                                respuesta.Add(new DistritoBe
                                {
                                    DistritoId = oDr.GetData<int>("DistritoId"),
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
