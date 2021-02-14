using bilecom.be;
using bilecom.da;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bilecom.bl
{
    public class OpcionBl : Conexion
    {
        OpcionDa opcionDa = new OpcionDa();

        public List<OpcionBe> ListarOpcionPorPerfil(int empresaId, int perfilId)
        {
            List<OpcionBe> lista = null;

            try
            {
                cn.Open();

                lista = opcionDa.ListarPorPerfil(perfilId, empresaId, cn);
            }
            catch (Exception ex) { throw ex; }
            finally { if (cn.State == ConnectionState.Open) cn.Close(); }

            return lista;
        }
    }
}
