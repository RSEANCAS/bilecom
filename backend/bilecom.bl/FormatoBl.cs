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
    public class FormatoBl : Conexion
    {
        FormatoDa formatoDa = new FormatoDa();

        public List<FormatoBe> ListarFormatoPorTipoComprobante(int tipoComprobanteId)
        {
            List<FormatoBe> lista = null;
            try
            {
                cn.Open();
                lista = formatoDa.ListarPorTipoComprobante(tipoComprobanteId, cn);
                cn.Close();
            }
            catch (Exception ex) { lista = null; }
            finally { if (cn.State == ConnectionState.Open) cn.Close(); }
            return lista;
        }

        public FormatoBe Obtener(int FormatoId)
        {
            FormatoBe respuesta = null;
            try
            {
                cn.Open();
                respuesta = formatoDa.Obtener(FormatoId, cn);
                cn.Close();
            }
            catch (Exception ex) { respuesta = null; }
            finally { if (cn.State == ConnectionState.Open) cn.Close(); }
            return respuesta;
        }
    }
}
