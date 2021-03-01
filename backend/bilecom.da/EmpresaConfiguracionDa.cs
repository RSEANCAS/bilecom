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
    public class EmpresaConfiguracionDa
    {
        public EmpresaConfiguracionBe Obtener(int empresaId, SqlConnection cn)
        {
            EmpresaConfiguracionBe respuesta = null;
            try
            {
                using (SqlCommand cmd = new SqlCommand("dbo.usp_empresaconfiguracion_obtener", cn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    //cmd.Parameters.AddWithValue("@empresaId", empresaId.GetNullable());
                    cmd.Parameters.AddWithValue("@empresaId", empresaId.GetNullable());

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.HasRows)
                        {
                            respuesta = new EmpresaConfiguracionBe();

                            if (dr.Read())
                            {
                                respuesta.EmpresaId = dr.GetData<int>("EmpresaId");
                                respuesta.AmbienteSunatId = dr.GetData<int>("AmbienteSunatId");
                                respuesta.RutaCertificado = dr.GetData<string>("RutaCertificado");
                                respuesta.ClaveCertificado = dr.GetData<string>("ClaveCertificado");
                                respuesta.CuentaCorriente = dr.GetData<string>("CuentaCorriente");
                                respuesta.ComentarioLegal = dr.GetData<string>("ComentarioLegal");
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
