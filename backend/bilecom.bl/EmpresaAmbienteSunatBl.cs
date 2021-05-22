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
    public class EmpresaAmbienteSunatBl : Conexion
    {
        EmpresaAmbienteSunatDa empresaAmbienteSunatDa = new EmpresaAmbienteSunatDa();

        public EmpresaAmbienteSunatBe ObtenerAmbienteSunat(int empresaid, int ambienteSunatId)
        {
            EmpresaAmbienteSunatBe item = null;
            try
            {
                cn.Open();
                item = empresaAmbienteSunatDa.Obtener(empresaid, ambienteSunatId, cn);
                cn.Close();
            }
            catch (Exception ex) { item = null; }
            finally { if (cn.State == ConnectionState.Open) cn.Close(); }
            return item;
        }
    }
}
