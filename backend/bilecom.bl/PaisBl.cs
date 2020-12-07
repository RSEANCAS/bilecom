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
    public class PaisBl : Conexion
    {
        PaisDa paisDa = new PaisDa();

        public List<PaisBe> ListarPais()
        {
            List<PaisBe> lista = null;
            try
            {
                cn.Open();
                lista = paisDa.Listar(cn);
                cn.Close();
            }
            catch (Exception ex) { lista = null; }
            finally { if (cn.State == ConnectionState.Open) cn.Close(); }
            return lista;
        }
    }
}
