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
        public List<SerieBe> BuscarSerie(int empresaId, int? tipoComprobanteId, string serial, int pagina, int cantidadRegistros, string columnaOrden, string ordenMax, out int totalRegistros)
        {
            totalRegistros = 0;
            List<SerieBe> lista = null;
            try
            {
                cn.Open();
                lista = serieDa.Buscar(empresaId, tipoComprobanteId, serial, pagina, cantidadRegistros, columnaOrden, ordenMax, cn, out totalRegistros);
                cn.Close();
            }
            catch (Exception ex) { lista = null; }
            finally { if (cn.State == ConnectionState.Open) cn.Close(); }
            return lista;
        }
    }
}
