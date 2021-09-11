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
    public class BoletaDa
    {
        public List<BoletaBe> Buscar(int empresaId, int ambienteSunatId, string nroDocumentoIdentidadCliente, string razonSocialCliente, DateTime fechaHoraEmisionDesde, DateTime fechaHoraEmisionHasta, int pagina, int cantidadRegistros, string columnaOrden, string ordenMax, SqlConnection cn, out int totalRegistros)
        {
            totalRegistros = 0;
            List<BoletaBe> lista = null;
            using (SqlCommand cmd = new SqlCommand("usp_boleta_buscar", cn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@empresaId", empresaId.GetNullable());
                cmd.Parameters.AddWithValue("@ambienteSunatId", ambienteSunatId.GetNullable());
                cmd.Parameters.AddWithValue("@nroDocumentoIdentidadCliente", nroDocumentoIdentidadCliente.GetNullable());
                cmd.Parameters.AddWithValue("@razonSocialCliente", razonSocialCliente.GetNullable());
                cmd.Parameters.AddWithValue("@fechaHoraEmisionDesde", fechaHoraEmisionDesde.GetNullable());
                cmd.Parameters.AddWithValue("@fechaHoraEmisionHasta", fechaHoraEmisionHasta.GetNullable());
                cmd.Parameters.AddWithValue("@pagina", pagina.GetNullable());
                cmd.Parameters.AddWithValue("@cantidadRegistros", cantidadRegistros.GetNullable());
                cmd.Parameters.AddWithValue("@columnaOrden", columnaOrden.GetNullable());
                cmd.Parameters.AddWithValue("@ordenMax", ordenMax.GetNullable());
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    if (dr.HasRows)
                    {
                        lista = new List<BoletaBe>();

                        while (dr.Read())
                        {
                            BoletaBe item = new BoletaBe();
                            item.Fila = dr.GetData<int>("Fila");
                            item.BoletaId = dr.GetData<int>("BoletaId");
                            item.SerieId = dr.GetData<int>("SerieId");
                            item.Serie = new SerieBe();
                            item.Serie.SerieId = dr.GetData<int>("SerieId");
                            item.Serie.Serial = dr.GetData<string>("SerialSerie");
                            item.NroComprobante = dr.GetData<int>("NroComprobante");
                            item.FechaHoraEmision = dr.GetData<DateTime>("FechaHoraEmision");
                            item.FechaVencimiento = dr.GetData<DateTime>("FechaVencimiento");
                            item.Cliente = new ClienteBe();
                            item.Cliente.NroDocumentoIdentidad = dr.GetData<string>("NroDocumentoIdentidadCliente");
                            item.Cliente.RazonSocial = dr.GetData<string>("RazonSocialCliente");
                            item.ImporteTotal = dr.GetData<decimal>("ImporteTotal");
                            item.FlagAnulado = dr.GetData<bool>("FlagAnulado");
                            item.CodigoRespuestaSunat = dr.GetData<string>("CodigoRespuestaSunat");
                            item.DescripcionRespuestaSunat = dr.GetData<string>("DescripcionRespuestaSunat");
                            item.EstadoIdRespuestaSunat = dr.GetData<int?>("EstadoIdRespuestaSunat");
                            item.RutaXml = dr.GetData<string>("RutaXml");
                            item.RutaPdf = dr.GetData<string>("RutaPdf");
                            item.RutaCdr = dr.GetData<string>("RutaCdr");
                            lista.Add(item);

                            totalRegistros = dr.GetData<int>("Total");
                        }
                    }
                }
            }
            return lista;
        }

        public BoletaBe Obtener(int empresaId, int boletaId, SqlConnection cn)
        {
            BoletaBe item = null;

            using (SqlCommand cmd = new SqlCommand("usp_factura_obtener", cn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@empresaId", empresaId.GetNullable());
                cmd.Parameters.AddWithValue("@boletaId", boletaId.GetNullable());

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    if (dr.HasRows)
                    {
                        item = new BoletaBe();

                        if (dr.Read())
                        {
                            item.EmpresaId = dr.GetData<int>("EmpresaId");
                            item.BoletaId = dr.GetData<int>("BoletaId");
                            item.AmbienteSunatId = dr.GetData<int>("AmbienteSunatId");
                            item.SedeId = dr.GetData<int>("SedeId");
                            item.SerieId = dr.GetData<int>("SerieId");
                            item.NroComprobante = dr.GetData<int>("NroComprobante");
                            item.FechaHoraEmision = dr.GetData<DateTime>("FechaHoraEmision");
                            item.FechaVencimiento = dr.GetData<DateTime>("FechaVencimiento");
                            item.MonedaId = dr.GetData<int>("MonedaId");
                            item.TipoOperacionVentaId = dr.GetData<int>("TipoOperacionVentaId");
                            item.ClienteId = dr.GetData<int>("ClienteId");
                            item.FormaPagoId = dr.GetData<int>("FormaPagoId");
                            item.FlagExportacion = dr.GetData<bool>("FlagExportacion");
                            item.FlagGratuito = dr.GetData<bool>("FlagGratuito");
                            item.FlagEmisorItinerante = dr.GetData<bool>("FlagEmisorItinerante");
                            item.FlagAnticipo = dr.GetData<bool>("FlagAnticipo");
                            item.FlagISC = dr.GetData<bool>("FlagISC");
                            item.FlagOtrosCargos = dr.GetData<bool>("FlagOtrosCargos");
                            item.FlagOtrosTributos = dr.GetData<bool>("FlagOtrosTributos");
                            item.TotalGravado = dr.GetData<decimal>("TotalGravado");
                            item.TotalExonerado = dr.GetData<decimal>("TotalExonerado");
                            item.TipoTributoIdExonerado = dr.GetData<int?>("TipoTributoIdExonerado");
                            item.TotalInafecto = dr.GetData<decimal>("TotalInafecto");
                            item.TipoTributoIdInafecto = dr.GetData<int?>("TipoTributoIdInafecto");
                            item.TotalExportacion = dr.GetData<decimal>("TotalExportacion");
                            item.TipoTributoIdExportacion = dr.GetData<int?>("TipoTributoIdExportacion");
                            item.TotalGratuito = dr.GetData<decimal>("TotalGratuito");
                            item.TipoTributoIdGratuito = dr.GetData<int?>("TipoTributoIdGratuito");
                            item.TotalVentaArrozPilado = dr.GetData<decimal>("TotalVentaArrozPilado");
                            item.TotalIgv = dr.GetData<decimal>("TotalIgv");
                            item.TipoTributoIdIgv = dr.GetData<int?>("TipoTributoIdIgv");
                            item.TotalIsc = dr.GetData<decimal>("TotalIsc");
                            item.TipoTributoIdIsc = dr.GetData<int?>("TipoTributoIdIsc");
                            item.TotalOtrosTributos = dr.GetData<decimal>("TotalOtrosTributos");
                            item.TipoTributoIdOtrosTributos = dr.GetData<int?>("TipoTributoIdOtrosTributos");
                            item.TotalOtrosCargos = dr.GetData<decimal>("TotalOtrosCargos");
                            item.TotalBaseImponible = dr.GetData<decimal>("TotalBaseImponible");
                            item.TotalDescuentos = dr.GetData<decimal>("TotalDescuentos");
                            item.ImporteTotal = dr.GetData<decimal>("ImporteTotal");
                            item.ImporteTotalEnLetras = dr.GetData<string>("ImporteTotalEnLetras");
                            item.Observacion = dr.GetData<string>("Observacion");
                            item.Hash = dr.GetData<string>("Hash");
                            item.FlagAnulado = dr.GetData<bool>("FlagAnulado");
                            item.CodigoRespuestaSunat = dr.GetData<string>("CodigoRespuestaSunat");
                            item.DescripcionRespuestaSunat = dr.GetData<string>("DescripcionRespuestaSunat");
                            item.EstadoIdRespuestaSunat = dr.GetData<int?>("EstadoIdRespuestaSunat");
                            item.RutaXml = dr.GetData<string>("RutaXml");
                            item.RutaPdf = dr.GetData<string>("RutaPdf");
                            item.RutaCdr = dr.GetData<string>("RutaCdr");
                            item.FormatoId = dr.GetData<int?>("FormatoId");
                        }
                    }
                }
            }

            return item;
        }

        public bool Guardar(BoletaBe registro, SqlConnection cn, out int? boletaId, out int? nroComprobante, out DateTime? fechaHoraEmision, out string totalImporteEnLetras)
        {
            boletaId = null;
            nroComprobante = null;
            fechaHoraEmision = null;
            totalImporteEnLetras = null;
            bool seGuardo = false;
            try
            {
                using (SqlCommand cmd = new SqlCommand("dbo.usp_boleta_guardar", cn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@empresaId", registro.EmpresaId.GetNullable());
                    cmd.Parameters.Add(new SqlParameter { ParameterName = "@boletaId", SqlDbType = SqlDbType.Int, Value = registro.BoletaId.GetNullable(), Direction = ParameterDirection.InputOutput });
                    cmd.Parameters.AddWithValue("@sedeId", registro.SedeId.GetNullable());
                    cmd.Parameters.AddWithValue("@serieId", registro.SerieId.GetNullable());
                    cmd.Parameters.Add(new SqlParameter { ParameterName = "@nroComprobante", SqlDbType = SqlDbType.Int, Value = registro.NroComprobante.GetNullable(), Direction = ParameterDirection.InputOutput });
                    cmd.Parameters.Add(new SqlParameter { ParameterName = "@fechaHoraEmision", SqlDbType = SqlDbType.DateTime, Value = DBNull.Value, Direction = ParameterDirection.InputOutput });
                    cmd.Parameters.AddWithValue("@fechaVencimiento", registro.FechaVencimiento.GetNullable());
                    cmd.Parameters.AddWithValue("@monedaId", registro.MonedaId.GetNullable());
                    cmd.Parameters.AddWithValue("@tipoOperacionVentaId", registro.TipoOperacionVentaId.GetNullable());
                    cmd.Parameters.AddWithValue("@formaPagoId", registro.FormaPagoId.GetNullable());
                    cmd.Parameters.AddWithValue("@formatoId", registro.FormatoId.GetNullable());
                    cmd.Parameters.AddWithValue("@observacion", registro.Observacion.GetNullable());
                    cmd.Parameters.AddWithValue("@clienteID", registro.ClienteId.GetNullable());
                    cmd.Parameters.AddWithValue("@flagExportacion", registro.FlagExportacion.GetNullable());
                    cmd.Parameters.AddWithValue("@flagGratuito", registro.FlagGratuito.GetNullable());
                    cmd.Parameters.AddWithValue("@flagEmisorItinerante", registro.FlagEmisorItinerante.GetNullable());
                    cmd.Parameters.AddWithValue("@flagAnticipo", registro.FlagAnticipo.GetNullable());
                    cmd.Parameters.AddWithValue("@flagISC", registro.FlagISC.GetNullable());
                    cmd.Parameters.AddWithValue("@flagOtrosCargos", registro.FlagOtrosCargos.GetNullable());
                    cmd.Parameters.AddWithValue("@flagOtrosTributos", registro.FlagOtrosTributos.GetNullable());
                    cmd.Parameters.AddWithValue("@totalGravado", registro.TotalGravado.GetNullable());
                    cmd.Parameters.AddWithValue("@totalExonerado", registro.TotalExonerado.GetNullable());
                    cmd.Parameters.AddWithValue("@tipoTributoIdExonerado", registro.TipoTributoIdExonerado.GetNullable());
                    cmd.Parameters.AddWithValue("@totalInafecto", registro.TotalInafecto.GetNullable());
                    cmd.Parameters.AddWithValue("@tipoTributoIdInafecto", registro.TipoTributoIdInafecto.GetNullable());
                    cmd.Parameters.AddWithValue("@totalExportacion", registro.TotalExportacion.GetNullable());
                    cmd.Parameters.AddWithValue("@tipoTributoIdExportacion", registro.TipoTributoIdExportacion.GetNullable());
                    cmd.Parameters.AddWithValue("@totalGratuito", registro.TotalGratuito.GetNullable());
                    cmd.Parameters.AddWithValue("@tipoTributoIdGratuito", registro.TipoTributoIdGratuito.GetNullable());
                    cmd.Parameters.AddWithValue("@totalVentaArrozPilado", registro.TotalVentaArrozPilado.GetNullable());
                    cmd.Parameters.AddWithValue("@totalIgv", registro.TotalIgv.GetNullable());
                    cmd.Parameters.AddWithValue("@tipoTributoIdIgv", registro.TipoTributoIdIgv.GetNullable());
                    cmd.Parameters.AddWithValue("@totalIsc", registro.TotalIsc.GetNullable());
                    cmd.Parameters.AddWithValue("@tipoTributoIdIsc", registro.TipoTributoIdIsc.GetNullable());
                    cmd.Parameters.AddWithValue("@totalOtrosTributos", registro.TotalOtrosTributos.GetNullable());
                    cmd.Parameters.AddWithValue("@tipoTributoIdOtrosTributos", registro.TipoTributoIdOtrosTributos.GetNullable());
                    cmd.Parameters.AddWithValue("@totalOtrosCargos", registro.TotalOtrosCargos.GetNullable());
                    cmd.Parameters.AddWithValue("@totalBaseImponible", registro.TotalBaseImponible.GetNullable());
                    cmd.Parameters.AddWithValue("@totalDescuentos", registro.TotalDescuentos.GetNullable());
                    cmd.Parameters.AddWithValue("@importeTotal", registro.ImporteTotal.GetNullable());
                    cmd.Parameters.AddWithValue("@ambienteSunatId", registro.AmbienteSunatId.GetNullable());
                    cmd.Parameters.Add(new SqlParameter { ParameterName = "@importeTotalEnLetras", SqlDbType = SqlDbType.VarChar, Size= -1, Value = DBNull.Value, Direction = ParameterDirection.InputOutput });
                    cmd.Parameters.AddWithValue("@usuario", registro.Usuario.GetNullable());

                    int filasAfectadas = cmd.ExecuteNonQuery();
                    seGuardo = filasAfectadas > 0;
                    if (seGuardo)
                    {
                        boletaId = (int?)cmd.Parameters["@boletaId"].Value;
                        nroComprobante = (int?)cmd.Parameters["@nroComprobante"].Value;
                        fechaHoraEmision = (DateTime?)cmd.Parameters["@fechaHoraEmision"].Value;
                        totalImporteEnLetras = (string)cmd.Parameters["@importeTotalEnLetras"].Value;
                    }
                }
            }
            catch (Exception ex)
            {
                seGuardo = false;
            }
            return seGuardo;
        }

        public bool Anular(BoletaBe registro, SqlConnection cn)
        {
            bool seGuardo = false;
            try
            {
                using (SqlCommand cmd = new SqlCommand("dbo.usp_boleta_anular", cn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@empresaId", registro.EmpresaId.GetNullable());
                    cmd.Parameters.AddWithValue("@boletaId", registro.BoletaId.GetNullable());
                    cmd.Parameters.AddWithValue("@usuario", registro.Usuario.GetNullable());

                    int filasAfectadas = cmd.ExecuteNonQuery();
                    seGuardo = filasAfectadas > 0;
                }
            }
            catch (Exception ex)
            {
                seGuardo = false;
            }
            return seGuardo;
        }

        public bool GuardarRespuestaSunat(BoletaBe registro, SqlConnection cn)
        {
            bool seGuardo = false;
            try
            {
                using (SqlCommand cmd = new SqlCommand("dbo.usp_boleta_guardar_respuestasunat", cn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@empresaId", registro.EmpresaId.GetNullable());
                    cmd.Parameters.AddWithValue("@boletaId", registro.BoletaId.GetNullable());
                    cmd.Parameters.AddWithValue("@codigoRespuestaSunat", registro.CodigoRespuestaSunat.GetNullable());
                    cmd.Parameters.AddWithValue("@descripcionRespuestaSunat", registro.DescripcionRespuestaSunat.GetNullable());
                    cmd.Parameters.AddWithValue("@estadoIdRespuestaSunat", registro.EstadoIdRespuestaSunat.GetNullable());
                    cmd.Parameters.AddWithValue("@rutaXml", registro.RutaXml.GetNullable());
                    cmd.Parameters.AddWithValue("@rutaCdr", registro.RutaCdr.GetNullable());
                    cmd.Parameters.AddWithValue("@usuario", registro.Usuario.GetNullable());

                    int filasAfectadas = cmd.ExecuteNonQuery();
                    seGuardo = filasAfectadas > 0;
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
