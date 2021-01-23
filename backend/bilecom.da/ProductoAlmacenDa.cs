using bilecom.ut;
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
    public class ProductoAlmacenDa
    {
        public bool Guardar(ProductoAlmacenBe registro,SqlConnection cn)
        {
            bool seGuardo = false;
            try
            {
                using (SqlCommand cmd = new SqlCommand("usp_productoalmacen_guardar",cn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@ProductoId", registro.ProductoId.GetNullable());
                    cmd.Parameters.AddWithValue("@AlmacenId", registro.AlmacenId.GetNullable());
                    cmd.Parameters.AddWithValue("@TipoMovimiento", registro.TipoMovimientoId.GetNullable());
                    cmd.Parameters.AddWithValue("@Monto", registro.Monto.GetNullable());
                    cmd.Parameters.AddWithValue("@Usuario", registro.Usuario.GetNullable());
                    int filasAfectadas = cmd.ExecuteNonQuery();
                    seGuardo = filasAfectadas > 0;
                }
            }
            catch(Exception ex)
            {
                seGuardo = false;
            }
            return seGuardo;
        }

    }
}
