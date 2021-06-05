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
    }
}
