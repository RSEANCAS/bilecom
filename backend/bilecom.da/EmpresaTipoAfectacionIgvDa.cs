using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bilecom.da
{
    public class EmpresaTipoAfectacionIgvDa
    {
        public bool Guardar(int empresaId, int tipoAfectacionIgvId, SqlConnection cn)
        {
            bool seGuardo = false;

            try
            {
                using (SqlCommand cmd = new SqlCommand("usp_empresatipoafectacionigv_guardar", cn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@empresaId", empresaId);
                    cmd.Parameters.AddWithValue("@tipoAfectacionIgvId", tipoAfectacionIgvId);

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
                using (SqlCommand cmd = new SqlCommand("usp_empresatipoafectacionigv_eliminar_x_empresa", cn))
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
