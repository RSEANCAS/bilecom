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
        public bool Guardar(CotizacionDetalleBe registro, SqlConnection cn)
        {
            bool seGuardo = false;
            try
            {
                using (SqlCommand cmd = new SqlCommand("usp_cotizacion_detalle_guardar", cn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@cotizacionDetalleId", registro.CotizacionDetalleId.GetNullable());
                    cmd.Parameters.AddWithValue("@descripcion", registro.Descripcion.GetNullable());
                    cmd.Parameters.AddWithValue("@cantidad", registro.Cantidad.GetNullable());
                    cmd.Parameters.AddWithValue("@precioUnitario", registro.PrecioUnitario.GetNullable());
                    cmd.Parameters.AddWithValue("@totalImporte", registro.TotalImporte.GetNullable());
                    cmd.Parameters.AddWithValue("@usuario", registro.CreadoPor.GetNullable());

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
