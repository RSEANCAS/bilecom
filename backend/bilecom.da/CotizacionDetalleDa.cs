using bilecom.be;
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
        public bool CotizacionDetalleGuardar(CotizacionDetalleBe cotizacionDetalle, SqlConnection cn)
        {
            bool seGuardo = false;
            try
            {
                using (SqlCommand oCommand = new SqlCommand("usp_cotizacion_detalle_guardar", cn))
                {
                    oCommand.CommandType = CommandType.StoredProcedure;
                    oCommand.Parameters.AddWithValue("@@cotizacionDetalleId", cotizacionDetalle.CotizacionDetalleId);
                    oCommand.Parameters.AddWithValue("@descripcion", cotizacionDetalle.Descripcion);
                    oCommand.Parameters.AddWithValue("@cantidad", cotizacionDetalle.Descripcion);
                    oCommand.Parameters.AddWithValue("@precioUnitario", cotizacionDetalle.PrecioUnitario);
                    oCommand.Parameters.AddWithValue("@totalImporte", cotizacionDetalle.TotalImporte);
                    oCommand.Parameters.AddWithValue("@usuario", cotizacionDetalle.CreadoPor);
                    
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
