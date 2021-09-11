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
                                respuesta.ComentarioLegalDetraccion = dr.GetData<string>("ComentarioLegalDetraccion");
                                respuesta.CantidadDecimalGeneral = dr.GetData<int>("CantidadDecimalGeneral");
                                respuesta.CantidadDecimalDetallado = dr.GetData<int>("CantidadDecimalDetallado");
                                respuesta.FormatoIds = dr.GetData<string>("FormatoIds");
                                respuesta.MonedaIdPorDefecto = dr.GetData<int?>("MonedaIdPorDefecto");
                                respuesta.TipoAfectacionIgvIdPorDefecto = dr.GetData<int?>("TipoAfectacionIgvIdPorDefecto");
                                respuesta.TipoComprobanteTipoOperacionVentaIdsPorDefecto = dr.GetData<string>("TipoComprobanteTipoOperacionVentaIdsPorDefecto");
                                respuesta.TipoProductoIdPorDefecto = dr.GetData<int?>("TipoProductoIdPorDefecto");
                                respuesta.UnidadMedidaIdPorDefecto = dr.GetData<int?>("UnidadMedidaIdPorDefecto");
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

        public bool Guardar(EmpresaConfiguracionBe registro, SqlConnection cn)
        {
            bool seGuardo = false;

            try
            {
                using (SqlCommand cmd = new SqlCommand("usp_empresaconfiguracion_guardar", cn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@empresaId", registro.EmpresaId.GetNullable());
                    cmd.Parameters.AddWithValue("@ambienteSunatId", (registro.AmbienteSunatId == 0 ? null : (int?)registro.AmbienteSunatId).GetNullable());
                    cmd.Parameters.AddWithValue("@rutaCertificado", registro.RutaCertificado.GetNullable());
                    cmd.Parameters.AddWithValue("@claveCertificado", registro.ClaveCertificado.GetNullable());
                    cmd.Parameters.AddWithValue("@cuentaCorriente", registro.CuentaCorriente.GetNullable());
                    cmd.Parameters.AddWithValue("@comentarioLegal", registro.ComentarioLegal.GetNullable());
                    cmd.Parameters.AddWithValue("@comentarioLegalDetraccion", registro.ComentarioLegalDetraccion.GetNullable());
                    cmd.Parameters.AddWithValue("@cantidadDecimalGeneral", registro.CantidadDecimalGeneral.GetNullable());
                    cmd.Parameters.AddWithValue("@cantidadDecimalDetallado", registro.CantidadDecimalDetallado.GetNullable());
                    cmd.Parameters.AddWithValue("@formatoIds", registro.FormatoIds.GetNullable());
                    cmd.Parameters.AddWithValue("@monedaIdPorDefecto", registro.MonedaIdPorDefecto.GetNullable());
                    cmd.Parameters.AddWithValue("@tipoAfectacionIgvIdPorDefecto", registro.TipoAfectacionIgvIdPorDefecto.GetNullable());
                    cmd.Parameters.AddWithValue("@tipoComprobanteTipoOperacionVentaIdsPorDefecto", registro.TipoComprobanteTipoOperacionVentaIdsPorDefecto.GetNullable());
                    cmd.Parameters.AddWithValue("@tipoProductoIdPorDefecto", registro.TipoProductoIdPorDefecto.GetNullable());
                    cmd.Parameters.AddWithValue("@unidadMedidaIdPorDefecto", registro.UnidadMedidaIdPorDefecto.GetNullable());
                    cmd.Parameters.AddWithValue("@creadoPor", registro.Usuario.GetNullable());

                    int FilaAfectadas = cmd.ExecuteNonQuery();
                    seGuardo = (FilaAfectadas != -1);
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }

            return seGuardo;
        }
    }
}
