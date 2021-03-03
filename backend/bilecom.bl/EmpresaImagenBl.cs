using bilecom.be;
using bilecom.da;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static bilecom.enums.Enums;

namespace bilecom.bl
{
    public class EmpresaImagenBl : Conexion
    {
        EmpresaImagenDa empresaImagenDa = new EmpresaImagenDa();

        public EmpresaImagenBe ObtenerDinamico(int empresaId, List<ColumnasEmpresaImagen> columnas)
        {
            EmpresaImagenBe respuesta = null;
            try
            {
                cn.Open();
                respuesta = empresaImagenDa.ObtenerDinamico(empresaId, columnas, cn);
                cn.Close();
            }
            catch (Exception ex) { respuesta = null; }
            finally { if (cn.State == ConnectionState.Open) cn.Close(); }
            return respuesta;
        }
    }
}
