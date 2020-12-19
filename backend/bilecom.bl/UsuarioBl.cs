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
    public class UsuarioBl : Conexion
    {
        UsuarioDa usuarioDa = new UsuarioDa();
        PerfilDa perfilDa = new PerfilDa();
        OpcionDa opcionDa = new OpcionDa();

        public UsuarioBe ObtenerUsuarioPorNombre(string nombre, int? empresaId, bool loadListaPerfil = false, bool loadListaOpcionxPerfil = false)
        {
            UsuarioBe item = null;

            try
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
                            foreach(var itemPerfil in item.ListaPerfil)
                            {
                                itemPerfil.ListaOpcion = opcionDa.ListarPorPerfil(itemPerfil.PerfilId, item.EmpresaId.Value, cn);
                            }
                        }
                    }
                }
            }
            catch (Exception ex) { throw ex; }
            finally { if (cn.State == ConnectionState.Open) cn.Close(); }

            return item;
        }
    }
}
