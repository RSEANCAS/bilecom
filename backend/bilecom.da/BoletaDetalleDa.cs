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
    public class BoletaDetalleDa
    {
        public bool Guardar(BoletaDetalleBe registro, SqlConnection cn, out int? boletaDetalleId)
        {
            boletaDetalleId = null;
            bool seGuardo = false;
            try
            {
                using (SqlCommand cmd = new SqlCommand("dbo.usp_boletadetalle_guardar", cn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@empresaId", registro.EmpresaId.GetNullable());
                    cmd.Parameters.AddWithValue("@boletaId", registro.BoletaId.GetNullable());
                    cmd.Parameters.Add(new SqlParameter { ParameterName = "@boletaDetalleId", SqlDbType = SqlDbType.Int, Value = registro.BoletaDetalleId.GetNullable(), Direction = ParameterDirection.InputOutput });
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
                    if (seGuardo) boletaDetalleId = (int?)cmd.Parameters["@boletaDetalleId"].Value;
                }
            }
            catch (Exception ex)
            {
                seGuardo = false;
            }
            return seGuardo;
        }

        public List<BoletaDetalleBe> Listar(int empresaId, int boletaId, SqlConnection cn)
        {
            List<BoletaDetalleBe> lista = null;
            using (SqlCommand cmd = new SqlCommand("usp_boletadetalle_lista", cn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@empresaId", empresaId.GetNullable());
                cmd.Parameters.AddWithValue("@boletaId", boletaId.GetNullable());
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    if (dr.HasRows)
                    {
                        lista = new List<BoletaDetalleBe>();

                        while (dr.Read())
                        {
                            BoletaDetalleBe item = new BoletaDetalleBe();

                            item.Fila = dr.GetData<int>("Fila");
                            item.EmpresaId = dr.GetData<int>("EmpresaId");
                            item.BoletaId = dr.GetData<int>("BoletaId");
                            item.BoletaDetalleId = dr.GetData<int>("BoletaDetalleId");
                            item.TipoProductoId = dr.GetData<int>("TipoProductoId");
                            item.Cantidad = dr.GetData<decimal>("Cantidad");
                            item.UnidadMedidaId = dr.GetData<int>("UnidadMedidaId");
                            item.UnidadMedida = new UnidadMedidaBe();
                            item.UnidadMedida.UnidadMedidaId = dr.GetData<int>("UnidadMedidaId");
                            item.UnidadMedida.Descripcion = dr.GetData<string>("DescripcionUnidadMedida");
                            item.ProductoId = dr.GetData<int>("ProductoId");
                            item.CodigoSunat = dr.GetData<string>("CodigoSunat");
                            item.Codigo = dr.GetData<string>("Codigo");
                            item.Descripcion = dr.GetData<string>("Descripcion");
                            item.TipoAfectacionIgvId = dr.GetData<int>("TipoAfectacionIgvId");
                            item.Descuento = dr.GetData<decimal>("Descuento");
                            item.PorcentajeISC = dr.GetData<decimal>("PorcentajeISC");
                            item.ISC = dr.GetData<decimal>("ISC");
                            item.TipoTributoIdISC = dr.GetData<int?>("TipoTributoIdISC");
                            item.PorcentajeIGV = dr.GetData<decimal>("PorcentajeIGV");
                            item.IGV = dr.GetData<decimal>("IGV");
                            item.TipoTributoIdIGV = dr.GetData<int?>("TipoTributoIdIGV");
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
    }
}
