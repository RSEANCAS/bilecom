using bilecom.be;
using bilecom.da;
using bilecom.ut;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bilecom.bl
{
    public class UsuarioBl : Conexion
    {
        UsuarioDa usuarioDa = new UsuarioDa();
        SedeDa sedeDa = new SedeDa();
        PerfilDa perfilDa = new PerfilDa();
        OpcionDa opcionDa = new OpcionDa();

        public UsuarioBe ObtenerUsuarioPorNombre(string nombre, int? empresaId, bool loadListaPerfil = false, bool loadListaOpcionxPerfil = false, bool LoadListaSede = false)
        {
            UsuarioBe item = null;

            try
            {
                using (var cn = new SqlConnection(CadenaConexion))
                {
                    cn.Open();

                    item = usuarioDa.ObtenerPorNombre(nombre, empresaId, cn);
                    if (item != null)
                    {
                        if (loadListaPerfil) item.ListaPerfil = perfilDa.ListarPorUsuario(item.UsuarioId, item.EmpresaId.Value, cn);
                        if (item.ListaPerfil != null)
                        {
                            if (loadListaOpcionxPerfil)
                            {
                                foreach (var itemPerfil in item.ListaPerfil)
                                {
                                    itemPerfil.ListaOpcion = opcionDa.ListarPorPerfil(itemPerfil.PerfilId, item.EmpresaId.Value, cn);
                                }
                            }
                        }
                        if (LoadListaSede) item.ListaSede = sedeDa.ListarPorUsuario(item.UsuarioId, item.EmpresaId.Value, cn);
                    }

                    cn.Close();
                }
            }
            catch (Exception ex) { throw ex; }
            //finally { if (cn.State == ConnectionState.Open) cn.Close(); }

            return item;
        }
        public UsuarioBe ObtenerUsuario(int empresaId,int usuarioId, bool loadListaPerfil = false, bool loadListaOpcionxPerfil = false, bool LoadListaSede = false)
        {
            UsuarioBe item = null;

            try
            {
                using (var cn = new SqlConnection(CadenaConexion))
                {
                    cn.Open();

                    item = usuarioDa.Obtener(empresaId, usuarioId, cn);
                    if (item != null)
                    {
                        if (loadListaPerfil) item.ListaPerfil = perfilDa.ListarPorUsuario(item.UsuarioId, item.EmpresaId.Value, cn);
                        if (item.ListaPerfil != null)
                        {
                            if (loadListaOpcionxPerfil)
                            {
                                foreach (var itemPerfil in item.ListaPerfil)
                                {
                                    itemPerfil.ListaOpcion = opcionDa.ListarPorPerfil(itemPerfil.PerfilId, item.EmpresaId.Value, cn);
                                }
                            }
                        }
                        if (LoadListaSede) item.ListaSede = sedeDa.ListarPorUsuario(item.UsuarioId, item.EmpresaId.Value, cn);
                    }

                    cn.Close();
                }
            }
            catch (Exception ex) { throw ex; }
            //finally { if (cn.State == ConnectionState.Open) cn.Close(); }

            return item;
        }
        public bool CambiarContrasena(int empresaId, int usuarioId, string contrasena, string modificadoPor)
        {
            bool seCambio = false;
            byte[] _contrasena = null;
            try
            {
                using (var cn = new SqlConnection(CadenaConexion))
                {
                    cn.Open();
                    _contrasena = Seguridad.MD5Byte(contrasena);
                    seCambio = usuarioDa.CambiarContrasena(empresaId, usuarioId, _contrasena, modificadoPor, cn);
                    cn.Close();
                }
            }
            catch (Exception ex) { throw ex; }
            //finally { if (cn.State == ConnectionState.Open) cn.Close(); }

            return seCambio;
        }
    }
}
