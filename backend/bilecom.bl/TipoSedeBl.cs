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
    public class TipoSedeBl : Conexion
    {
        TipoSedeDa tipoSedeDa = new TipoSedeDa();
        public List<TipoSedeBe> ListarTipoSede(int empresaId)
        {
            List<TipoSedeBe> respuesta = null;
            try
            {
                using (var cn = new SqlConnection(CadenaConexion))
                {
                    cn.Open();
                    respuesta = tipoSedeDa.Listar(empresaId, cn);
                    cn.Close();
                }
            }
            catch (Exception ex) { respuesta = null; }
            //finally { if (cn.State == ConnectionState.Open) cn.Close(); }
            return respuesta;
        }

        public List<TipoSedeBe> BuscarTipoSede(int empresaId, string nombre, int pagina, int cantidadRegistros, string columnaOrden, string ordenMax, out int totalRegistros)
        {
            totalRegistros = 0;
            List<TipoSedeBe> lista = new List<TipoSedeBe>();
            try
            {
                using (var cn = new SqlConnection(CadenaConexion))
                {
                    cn.Open();
                    lista = tipoSedeDa.Buscar(empresaId, nombre, pagina, cantidadRegistros, columnaOrden, ordenMax, cn, out totalRegistros);
                    cn.Close();
                }
            }
            catch (Exception ex) { lista = null; }
            //finally { if (cn.State == ConnectionState.Open) cn.Close(); }
            return lista;
        }

        public TipoSedeBe Obtener(int EmpresaId, int TipoSedeId)
        {
            TipoSedeBe respuesta = null;
            try
            {
                using (var cn = new SqlConnection(CadenaConexion))
                {
                    cn.Open();
                    respuesta = tipoSedeDa.Obtener(EmpresaId, TipoSedeId, cn);
                    cn.Close();
                }
            }
            catch (Exception ex) { respuesta = null; }
            //finally { if (cn.State == ConnectionState.Open) cn.Close(); }
            return respuesta;
        }

        public bool Guardar(TipoSedeBe registro)
        {
            bool seGuardo = false;
            try
            {
                using (var cn = new SqlConnection(CadenaConexion))
                {
                    cn.Open();
                    seGuardo = tipoSedeDa.Guardar(registro, cn);
                    cn.Close();
                }
            }
            catch (Exception ex) { seGuardo = false; }
            //finally { if (cn.State == ConnectionState.Open) cn.Close(); }
            return seGuardo;
        }

        public bool Eliminar(int empresaId, int tiposedeId, string Usuario)
        {
            bool seGuardo = false;
            try
            {
                using (var cn = new SqlConnection(CadenaConexion))
                {
                    cn.Open();
                    seGuardo = tipoSedeDa.Eliminar(empresaId, tiposedeId, Usuario, cn);
                    cn.Close();
                }
            }
            catch (Exception ex) { seGuardo = false; }
            //finally { if (cn.State == ConnectionState.Open) cn.Close(); }
            return seGuardo;
        }
    }
}
