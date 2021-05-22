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
    public class SerieBl : Conexion
    {
        SerieDa serieDa = new SerieDa();

        public List<SerieBe> ListarSeriePorTipoComprobante(int empresaId, int ambienteSunatId, int tipoComprobanteId)
        {
            List<SerieBe> lista = new List<SerieBe>();
            try
            {
                cn.Open();
                lista = serieDa.ListarPorTipoComprobante(empresaId, ambienteSunatId, tipoComprobanteId, cn);
                cn.Close();
            }
            catch (Exception) { lista = null; }
            finally { if (cn.State == ConnectionState.Open) cn.Close(); }
            return lista;
        }
        
        public List<SerieBe> BuscarSerie(int empresaId, int ambienteSunatId, int? tipoComprobanteId, string serial, int pagina, int cantidadRegistros, string columnaOrden, string ordenMax, out int totalRegistros)
        {
            totalRegistros = 0;
            List<SerieBe> lista = null;
            try
            {
                cn.Open();
                lista = serieDa.Buscar(empresaId, ambienteSunatId, tipoComprobanteId, serial, pagina, cantidadRegistros, columnaOrden, ordenMax, cn, out totalRegistros);
                cn.Close();
            }
            catch (Exception ex) { lista = null; }
            finally { if (cn.State == ConnectionState.Open) cn.Close(); }
            return lista;
        }

        public SerieBe Obtener(int empresaId, int serieId)
        {
            SerieBe respuesta = null;
            try
            {
                cn.Open();
                respuesta = serieDa.Obtener(empresaId, serieId, cn);
                cn.Close();
            }
            catch (Exception ex) { respuesta = null; }
            finally { if (cn.State == ConnectionState.Open) cn.Close(); }
            return respuesta;
        }

        public bool Guardar(SerieBe serieBe)
        {
            bool seGuardo = false;
            try
            {
                cn.Open();
                seGuardo = serieDa.Guardar(serieBe, cn);
                cn.Close();
            }
            catch (Exception ex) { seGuardo = false; }
            finally { if (cn.State == ConnectionState.Open) cn.Close(); }
            return seGuardo;
        }

        public bool Eliminar(int empresaId, int serieId, string Usuario)
        {
            bool seGuardo = false;
            try
            {
                cn.Open();
                seGuardo = serieDa.Eliminar(empresaId, serieId, Usuario, cn);
                cn.Close();
            }
            catch (Exception ex) { seGuardo = false; }
            finally { if (cn.State == ConnectionState.Open) cn.Close(); }
            return seGuardo;
        }
    }
}
