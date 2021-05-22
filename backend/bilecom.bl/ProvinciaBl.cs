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
    public class ProvinciaBl:Conexion
    {
        ProvinciaDa provinciaDa = new ProvinciaDa();

        public List<ProvinciaBe> ListarProvincia()
        {
            List<ProvinciaBe> lista = null;
            try
            {
                cn.Open();
                lista = provinciaDa.Listar(cn);
                cn.Close();
            }
            catch (Exception ex) { lista = null; }
            finally { if (cn.State == ConnectionState.Open) cn.Close(); }
            return lista;
        }

        public ProvinciaBe ObtenerProvincia(int provinciaId)
        {
            ProvinciaBe item = null;
            try
            {
                cn.Open();
                item = provinciaDa.Obtener(provinciaId, cn);
                cn.Close();
            }
            catch (Exception ex) { item = null; }
            finally { if (cn.State == ConnectionState.Open) cn.Close(); }
            return item;
        }
    }
}
