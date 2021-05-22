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
        TipoComprobanteDa tipoComprobanteDa = new TipoComprobanteDa();

        public List<FormatoBe> ListarFormato(bool withTipoComprobante = false)
        {
            List<FormatoBe> lista = null;
            try
            {
                cn.Open();
                lista = formatoDa.Listar(cn);
                if(lista != null && withTipoComprobante)
                {
                    foreach(var item in lista)
                    {
                        item.TipoComprobante = tipoComprobanteDa.Obtener(item.TipoComprobanteId, cn);
                    }
                }
                cn.Close();
            }
            catch (Exception ex) { lista = null; }
            finally { if (cn.State == ConnectionState.Open) cn.Close(); }
            return lista;
        }

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
