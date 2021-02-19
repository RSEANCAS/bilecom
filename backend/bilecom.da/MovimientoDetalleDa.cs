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
    public class MovimientoDetalleDa
    {
        public List<MovimientoDetalleBe> Listar(int empresaId, int movimientoId, SqlConnection cn)
        {
            List<MovimientoDetalleBe> lista = null;
            using (SqlCommand cmd = new SqlCommand("web.usp_movimientodetalle_listar", cn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@empresaId", empresaId.GetNullable());
                cmd.Parameters.AddWithValue("@movimientoId", movimientoId.GetNullable());
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    if (dr.HasRows)
                    {
                        lista = new List<MovimientoDetalleBe>();

                        while (dr.Read())
                        {
                            MovimientoDetalleBe item = new MovimientoDetalleBe();
                            item.Fila = dr.GetData<int>("Fila");
                            item.EmpresaId = dr.GetData<int>("EmpresaId");
                            item.MovimientoId = dr.GetData<int>("MovimientoId");
                            item.MovimientoDetalleId = dr.GetData<int>("MovimientoDetalleId");
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

        public bool Guardar(MovimientoDetalleBe registro, SqlConnection cn)
        {
            bool seGuardo = false;
            try
            {
                using (SqlCommand cmd = new SqlCommand("web.usp_movimientodetalle_guardar", cn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@EmpresaId", registro.EmpresaId.GetNullable());
                    cmd.Parameters.AddWithValue("@MovimientoId", registro.MovimientoId.GetNullable());
                    cmd.Parameters.AddWithValue("@MovimientoDetalleId", registro.MovimientoDetalleId.GetNullable());
                    cmd.Parameters.AddWithValue("@ProductoId", registro.ProductoId.GetNullable());
                    cmd.Parameters.AddWithValue("@Descripcion", registro.Descripcion.GetNullable());
                    cmd.Parameters.AddWithValue("@Cantidad", registro.Cantidad.GetNullable());
                    cmd.Parameters.AddWithValue("@PrecioUnitario", registro.PrecioUnitario.GetNullable());
                    cmd.Parameters.AddWithValue("@TotalImporte", registro.TotalImporte.GetNullable());
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

        public bool Eliminar(MovimientoDetalleBe registro, SqlConnection cn)
        {
            bool seGuardo = false;
            try
            {
                using (SqlCommand cmd = new SqlCommand("dbo.usp_movimientodetalle_eliminar", cn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@EmpresaId", registro.EmpresaId.GetNullable());
                    cmd.Parameters.AddWithValue("@MovimientoId", registro.MovimientoId.GetNullable());
                    cmd.Parameters.AddWithValue("@MovimientoDetalleId", registro.MovimientoDetalleId.GetNullable());
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
