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
    public class CotizacionDetalleDa
    {
        public List<CotizacionDetalleBe> Listar(int empresaId, int cotizacionId, SqlConnection cn)
        {
            List<CotizacionDetalleBe> lista = null;
            using (SqlCommand cmd = new SqlCommand("dbo.usp_cotizacion_detalle_listar", cn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@empresaId", empresaId.GetNullable());
                cmd.Parameters.AddWithValue("@cotizacionId", cotizacionId.GetNullable());
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    if (dr.HasRows)
                    {
                        lista = new List<CotizacionDetalleBe>();

                        while (dr.Read())
                        {
                            CotizacionDetalleBe item = new CotizacionDetalleBe();
                            item.Fila = dr.GetData<int>("Fila");
                            item.EmpresaId = dr.GetData<int>("EmpresaId");
                            item.CotizacionId = dr.GetData<int>("CotizacionId");
                            item.CotizacionDetalleId = dr.GetData<int>("CotizacionDetalleId");
                            item.ProductoId = dr.GetData<int>("ProductoId");
                            item.Descripcion = dr.GetData<string>("Descripcion");
                            item.Cantidad = dr.GetData<decimal>("Cantidad");
                            item.PrecioUnitario = dr.GetData<decimal>("PrecioUnitario");
                            item.TotalImporte = dr.GetData<decimal>("TotalImporte");
                            lista.Add(item);
                        }
                    }
                }
            }
            return lista;
        }

        public bool Guardar(CotizacionDetalleBe registro, SqlConnection cn)
        {
            bool seGuardo = false;
            try
            {
                using (SqlCommand cmd = new SqlCommand("web.usp_cotizacion_detalle_guardar", cn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@empresaId", registro.EmpresaId.GetNullable());
                    cmd.Parameters.AddWithValue("@cotizacionId", registro.CotizacionId.GetNullable());
                    cmd.Parameters.AddWithValue("@cotizacionDetalleId", registro.CotizacionDetalleId.GetNullable());
                    cmd.Parameters.AddWithValue("@productoId", registro.ProductoId.GetNullable());
                    cmd.Parameters.AddWithValue("@descripcion", registro.Descripcion.GetNullable());
                    cmd.Parameters.AddWithValue("@cantidad", registro.Cantidad.GetNullable());
                    cmd.Parameters.AddWithValue("@precioUnitario", registro.PrecioUnitario.GetNullable());
                    cmd.Parameters.AddWithValue("@totalImporte", registro.TotalImporte.GetNullable());
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

        public bool Eliminar(CotizacionDetalleBe registro, SqlConnection cn)
        {
            bool seGuardo = false;
            try
            {
                using (SqlCommand cmd = new SqlCommand("dbo.usp_cotizacion_detalle_eliminar", cn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@empresaId", registro.EmpresaId.GetNullable());
                    cmd.Parameters.AddWithValue("@cotizacionId", registro.CotizacionId.GetNullable());
                    cmd.Parameters.AddWithValue("@cotizacionDetalleId", registro.CotizacionDetalleId.GetNullable());
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
