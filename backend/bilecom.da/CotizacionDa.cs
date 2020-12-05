using bilecom.be;
using bilecom.ut;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// En el DA van los métodos a ser llamados por el BL
namespace bilecom.da
{
    public class CotizacionDa
    {
        public List<CotizacionBe> Listar(SqlConnection cn, int EmpresaId, string NombresCompletosPersonal, string RazonSocial, DateTime FechaHoraEmisionDesde, DateTime FechaHoraEmisionHasta)
        {
            List<CotizacionBe> lCotizacion = null;
            using (SqlCommand cmd = new SqlCommand("usp_cotizacion_listar", cn))
            {
                // Instanciando a la funcion CommandType
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@empresaId", EmpresaId);
                cmd.Parameters.AddWithValue("@nombresPersonal", NombresCompletosPersonal);
                cmd.Parameters.AddWithValue("@cliente", RazonSocial);
                cmd.Parameters.AddWithValue("@fechaHoraEmisionDesde", FechaHoraEmisionDesde);
                cmd.Parameters.AddWithValue("@fechaHoraEmisionHasta", FechaHoraEmisionHasta);
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    if (dr.HasRows)
                    {
                        lCotizacion = new List<CotizacionBe>();

                        while (dr.Read())
                        {
                            CotizacionBe item = new CotizacionBe();
                            item.Fila = dr.GetData<int>("Fila");
                            item.SerieId = dr.GetData<int>("SerieId");
                            item.NroComprobante = dr.GetData<int>("NroComprobante");
                            item.NroPedido = dr.GetData<string>("NroPedido");
                            item.FechaHoraEmision = dr.GetData<DateTime>("FechaHoraEmision");
                            //Para personal antes de asignarlo hay que instanciar
                            item.Personal = new PersonalBe();
                            item.Personal.NombresCompletos = dr.GetData<string>("NombresCompletosPersonal");
                            item.Cliente = new ClienteBe();
                            item.Cliente.RazonSocial = dr.GetData<string>("RazonSocialCliente");
                            item.CotizacionId = dr.GetData<int>("CotizacionId");
                            lCotizacion.Add(item);
                        }
                    }
                }
            }
            return lCotizacion;
        }
        public bool CotizacionGuardar(CotizacionBe cotizacion, SqlConnection cn, out int? cotizacionId)
        {
            cotizacionId = null;
            bool seGuardo = false;
            try
            {
                using (SqlCommand oCommand = new SqlCommand("dbo.usp_cotizacion_guardar", cn))
                {
                    oCommand.CommandType = CommandType.StoredProcedure;
                    oCommand.Parameters.AddWithValue("@cotizacionId", cotizacion.CotizacionId);
                    oCommand.Parameters.AddWithValue("@empresaId", cotizacion.EmpresaId);
                    oCommand.Parameters.AddWithValue("@serieId", cotizacion.SerieId);
                    oCommand.Parameters.AddWithValue("@nroComprobante", cotizacion.NroComprobante);
                    oCommand.Parameters.AddWithValue("@monedaId", cotizacion.MonedaId);
                    oCommand.Parameters.AddWithValue("@clienteID", cotizacion.ClienteId);
                    oCommand.Parameters.AddWithValue("@personalId", cotizacion.PersonalId);
                    oCommand.Parameters.AddWithValue("@totalImporte", cotizacion.TotalImporte);
                    oCommand.Parameters.AddWithValue("@usuario", cotizacion.CreadoPor);
                    int result = oCommand.ExecuteNonQuery();
                    if (result > 0) seGuardo = true;
                }
            }
            catch (Exception ex)
            {
                seGuardo = false;
            }
            return seGuardo;
        }
    }

}
