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
    public class DepartamentoBl : Conexion
    {
        DepartamentoDa departamentoDa = new DepartamentoDa();

        public List<DepartamentoBe> ListarDepartamento()
        {
            List<DepartamentoBe> lista = null;
            try
            {
                cn.Open();
                lista = departamentoDa.Listar(cn);
                cn.Close();
            }
            catch (Exception ex) { lista = null; }
            finally { if (cn.State == ConnectionState.Open) cn.Close(); }
            return lista;
        }

        public DepartamentoBe ObtenerDepartamento(int departamentoId)
        {
            DepartamentoBe item = null;
            try
            {
                cn.Open();
                item = departamentoDa.Obtener(departamentoId, cn);
                cn.Close();
            }
            catch (Exception ex) { item = null; }
            finally { if (cn.State == ConnectionState.Open) cn.Close(); }
            return item;
        }
    }
}
