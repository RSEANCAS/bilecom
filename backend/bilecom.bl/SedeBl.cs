using bilecom.be;
using bilecom.da;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bilecom.bl
{
    public class SedeBl : Conexion
    {
        SedeDa sedeDa = new SedeDa();

        public List<SedeBe> BuscarSede(int empresaId, string nombre, int pagina, int cantidadRegistros, string columnaOrden, string ordenMax, out int totalRegistros)
        {
            totalRegistros = 0;
            List<SedeBe> lista = null;
            try
            {
                cn.Open();
                lista = sedeDa.Buscar(empresaId, nombre, pagina, cantidadRegistros, columnaOrden, ordenMax, cn, out totalRegistros);
                cn.Close();
            }
            catch (Exception) { lista = null; }
            finally { if (cn.State == ConnectionState.Open) cn.Close(); }

            return lista;
        }

        public SedeBe Obtener(int empresaId, int sedeId)
        {
            SedeBe respuesta = null;
            try
            {
                cn.Open();
                respuesta = sedeDa.Obtener(empresaId, sedeId, cn);
                cn.Close();
            }
            catch (Exception ex) { respuesta = null; }
            finally { if (cn.State == ConnectionState.Open) cn.Close(); }
            return respuesta;
        }
        public bool Guardar(SedeBe sedeBe)
        {
            bool seGuardo = false;
            try
            {
                cn.Open();
                seGuardo = sedeDa.Guardar(sedeBe, cn);
                cn.Close();
            }
            catch (Exception ex) { seGuardo = false; }
            finally { if (cn.State == ConnectionState.Open) cn.Close(); }
            return seGuardo;
        }

        public bool EliminarSede(int empresaId, int sedeId, string Usuario)
        {
            bool seGuardo = false;
            try
            {
                cn.Open();
                seGuardo = sedeDa.Eliminar(empresaId, sedeId, Usuario, cn);
                cn.Close();
            }
            catch (Exception ex) { seGuardo = false; }
            finally { if (cn.State == ConnectionState.Open) cn.Close(); }
            return seGuardo;
        }
    }
}
