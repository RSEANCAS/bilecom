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
    public class FacturaDetalleDa
    {
        public List<FacturaDetalleBe> Listar(int empresaId, int facturaId, SqlConnection cn)
        {
            List<FacturaDetalleBe> lista = null;
            using (SqlCommand cmd = new SqlCommand("usp_facturadetalle_listar", cn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@empresaId", empresaId.GetNullable());
                cmd.Parameters.AddWithValue("@facturaId", facturaId.GetNullable());
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    if (dr.HasRows)
                    {
                        lista = new List<FacturaDetalleBe>();

                        while (dr.Read())
                        {
                            FacturaDetalleBe item = new FacturaDetalleBe();

                            item.Fila = dr.GetData<int>("Fila");
                            item.EmpresaId = dr.GetData<int>("EmpresaId");
                            item.FacturaId = dr.GetData<int>("FacturaId");
                            item.FacturaDetalleId = dr.GetData<int>("FacturaDetalleId");
                            item.TipoProductoId = dr.GetData<int>("TipoProductoId");
                            item.Cantidad = dr.GetData<decimal>("Cantidad");
                            item.UnidadMedidaId = dr.GetData<int>("UnidadMedidaId");
                            item.UnidadMedida = new UnidadMedidaBe();
                            item.UnidadMedida.UnidadMedidaId = dr.GetData<int>("UnidadMedidaId");
                            item.UnidadMedida.Descripcion = dr.GetData<string>("DescripcionUnidadMedida");
                            item.UnidadMedida.Id = dr.GetData<string>("IdUnidadMedida");
                            item.ProductoId = dr.GetData<int>("ProductoId");
                            item.CodigoSunat = dr.GetData<string>("CodigoSunat");
                            item.Codigo = dr.GetData<string>("Codigo");
                            item.Descripcion = dr.GetData<string>("Descripcion");
                            item.TipoAfectacionIgvId = dr.GetData<int>("TipoAfectacionIgvId");
                            item.TipoAfectacionIgv = new TipoAfectacionIgvBe();
                            item.TipoAfectacionIgv.TipoAfectacionIgvId = dr.GetData<int>("TipoAfectacionIgvId");
                            item.TipoAfectacionIgv.TipoTributoId = dr.GetData<int>("TipoTributoIdTipoAfectacionIgv");
                            item.TipoAfectacionIgv.Id = dr.GetData<string>("IdTipoAfectacionIgv");
                            item.TipoAfectacionIgv.FlagGratuito = dr.GetData<bool>("FlagGravadoTipoAfectacionIgv");
                            item.TipoAfectacionIgv.FlagExonerado = dr.GetData<bool>("FlagExoneradoTipoAfectacionIgv");
                            item.TipoAfectacionIgv.FlagInafecto = dr.GetData<bool>("FlagInafectoTipoAfectacionIgv");
                            item.TipoAfectacionIgv.FlagExportacion = dr.GetData<bool>("FlagExportacionTipoAfectacionIgv");
                            item.TipoAfectacionIgv.FlagGratuito = dr.GetData<bool>("FlagGratuitoTipoAfectacionIgv");
                            item.TipoAfectacionIgv.FlagVentaArrozPilado = dr.GetData<bool>("FlagVentaArrozPiladoTipoAfectacionIgv");
                            item.Descuento = dr.GetData<decimal>("Descuento");
                            item.PorcentajeISC = dr.GetData<decimal>("PorcentajeISC");
                            item.ISC = dr.GetData<decimal>("ISC");
                            item.TipoTributoIdISC = dr.GetData<int?>("TipoTributoIdISC");
                            item.PorcentajeIGV = dr.GetData<decimal>("PorcentajeIGV");
                            item.IGV = dr.GetData<decimal>("IGV");
                            item.TipoTributoIdIGV = dr.GetData<int?>("TipoTributoIdIGV");
                            item.TipoTributoIGV = new TipoTributoBe();
                            item.TipoTributoIGV.TipoTributoId = dr.GetData<int>("TipoTributoIdIGV");
                            item.TipoTributoIGV.Nombre = dr.GetData<string>("NombreTipoTributoIdIGV");
                            item.TipoTributoIGV.Descripcion = dr.GetData<string>("DescripcionTipoTributoIdIGV");
                            item.TipoTributoIGV.Codigo = dr.GetData<string>("CodigoTipoTributoIdIGV");
                            item.TipoTributoIGV.CodigoNombre = dr.GetData<string>("CodigoNombreTipoTributoIdIGV");
                            item.PorcentajeOTH = dr.GetData<decimal>("PorcentajeOTH");
                            item.OTH = dr.GetData<decimal>("OTH");
                            item.TipoTributoIdOTH = dr.GetData<int?>("TipoTributoIdOTH");
                            item.FlagAplicaICPBER = dr.GetData<bool>("FlagAplicaICPBER");
                            item.ICPBER = dr.GetData<decimal>("ICPBER");
                            item.PorcentajeICPBER = dr.GetData<decimal>("PorcentajeICPBER");
                            item.ValorUnitario = dr.GetData<decimal>("ValorUnitario");
                            item.PrecioUnitario = dr.GetData<decimal>("PrecioUnitario");
                            item.ValorVenta = dr.GetData<decimal>("ValorVenta");
                            item.PrecioVenta = dr.GetData<decimal>("PrecioVenta");
                            item.ImporteTotal = dr.GetData<decimal>("ImporteTotal");

                            lista.Add(item);
                        }
                    }
                }
            }
            return lista;
        }

        public bool Guardar(FacturaDetalleBe registro, SqlConnection cn, out int? facturaDetalleId)
        {
            facturaDetalleId = null;
            bool seGuardo = false;
            try
            {
                using (SqlCommand cmd = new SqlCommand("dbo.usp_facturadetalle_guardar", cn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@empresaId", registro.EmpresaId.GetNullable());
                    cmd.Parameters.AddWithValue("@facturaId", registro.FacturaId.GetNullable());
                    cmd.Parameters.Add(new SqlParameter { ParameterName = "@facturaDetalleId", SqlDbType = SqlDbType.Int, Value = registro.FacturaDetalleId.GetNullable(), Direction = ParameterDirection.InputOutput });
                    cmd.Parameters.AddWithValue("@tipoProductoId", registro.TipoProductoId.GetNullable());
                    cmd.Parameters.AddWithValue("@cantidad", registro.Cantidad.GetNullable());
                    cmd.Parameters.AddWithValue("@unidadMedidaId", registro.UnidadMedidaId.GetNullable());
                    cmd.Parameters.AddWithValue("@codigoSunat", registro.CodigoSunat.GetNullable());
                    cmd.Parameters.AddWithValue("@productoId", registro.ProductoId.GetNullable());
                    cmd.Parameters.AddWithValue("@codigo", registro.Codigo.GetNullable());
                    cmd.Parameters.AddWithValue("@descripcion", registro.Descripcion.GetNullable());
                    cmd.Parameters.AddWithValue("@flagAplicaICPBER", registro.FlagAplicaICPBER.GetNullable());
                    cmd.Parameters.AddWithValue("@tipoAfectacionIgvId", registro.TipoAfectacionIgvId.GetNullable());
                    cmd.Parameters.AddWithValue("@descuento", registro.Descuento.GetNullable());
                    cmd.Parameters.AddWithValue("@isc", registro.ISC.GetNullable());
                    cmd.Parameters.AddWithValue("@porcentajeIGV", registro.PorcentajeIGV.GetNullable());
                    cmd.Parameters.AddWithValue("@igv", registro.IGV.GetNullable());
                    cmd.Parameters.AddWithValue("@icpber", registro.ICPBER.GetNullable());
                    cmd.Parameters.AddWithValue("@porcentajeICPBER", registro.PorcentajeICPBER.GetNullable());
                    cmd.Parameters.AddWithValue("@valorUnitario", registro.ValorUnitario.GetNullable());
                    cmd.Parameters.AddWithValue("@precioUnitario", registro.PrecioUnitario.GetNullable());
                    cmd.Parameters.AddWithValue("@valorVenta", registro.ValorVenta.GetNullable());
                    cmd.Parameters.AddWithValue("@precioVenta", registro.PrecioVenta.GetNullable());
                    cmd.Parameters.AddWithValue("@importeTotal", registro.ImporteTotal.GetNullable());
                    //cmd.Parameters.AddWithValue("@usuario", registro.Usuario.GetNullable());

                    int filasAfectadas = cmd.ExecuteNonQuery();
                    seGuardo = filasAfectadas > 0;
                    if (seGuardo) facturaDetalleId = (int?)cmd.Parameters["@facturaDetalleId"].Value;
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
