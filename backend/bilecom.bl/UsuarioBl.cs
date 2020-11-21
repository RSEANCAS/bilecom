using bilecom.be;
using bilecom.da;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bilecom.bl
{
    public class UsuarioBl : Conexion
    {
        UsuarioDa usuarioDa = new UsuarioDa();

        public UsuarioBe ObtenerPorNombre(string nombre, int? empresaId)
        {
            UsuarioBe item = null;

            try
            {
                cn.Open();

                item = usuarioDa.ObtenerPorNombre(nombre, empresaId, cn);
            }
            catch (Exception ex) { throw ex; }
            finally { cn.Close(); }

            return item;
        }
    }
}
