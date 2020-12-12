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
    public class UsuarioBl : Conexion
    {
        UsuarioDa usuarioDa = new UsuarioDa();

        public UsuarioBe ObtenerUsuarioPorNombre(string nombre, int? empresaId)
        {
            UsuarioBe item = null;

            try
            {
                cn.Open();

                item = usuarioDa.ObtenerPorNombre(nombre, empresaId, cn);
            }
            catch (Exception ex) { throw ex; }
            finally { if (cn.State == ConnectionState.Open) cn.Close(); }

            return item;
        }
    }
}
