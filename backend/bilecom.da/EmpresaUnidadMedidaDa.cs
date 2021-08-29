using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bilecom.da
{
    public class EmpresaUnidadMedidaDa
    {
        public bool Guardar(int empresaId, int unidadMedidaId, SqlConnection cn)
        {
            bool seGuardo = false;

            try
            {
                using (SqlCommand cmd = new SqlCommand("usp_empresaunidadmedida_guardar", cn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@empresaId", empresaId);
                    cmd.Parameters.AddWithValue("@unidadMedidaId", unidadMedidaId);

                    int FilaAfectadas = cmd.ExecuteNonQuery();
                    seGuardo = (FilaAfectadas != -1);
                }
            }
            catch (Exception ex) { throw ex; }

            return seGuardo;
        }

        public bool EliminarPorEmpresa(int empresaId, SqlConnection cn)
        {
            bool seGuardo = false;

            try
            {
                using (SqlCommand cmd = new SqlCommand("usp_empresaunidadmedida_eliminar_x_empresa", cn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@empresaId", empresaId);

                    int FilaAfectadas = cmd.ExecuteNonQuery();
                    seGuardo = (FilaAfectadas != -1);
                }
            }
            catch (Exception ex) { throw ex; }

            return seGuardo;
        }
    }
}
