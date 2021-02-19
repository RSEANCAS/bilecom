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
    public class PerfilBl : Conexion
    {
        OpcionDa opcionDa = new OpcionDa();
        PerfilDa perfilDa = new PerfilDa();

        public List<PerfilBe> ListarPerfilPorUsuario(int empresaId, int usuarioId, bool loadListaOpcion = false)
        {
            List<PerfilBe> lista = null;

            try
            {
                cn.Open();

                lista = perfilDa.ListarPorUsuario(usuarioId, empresaId, cn);

                if (lista != null)
                {
                    if (loadListaOpcion)
                    {
                        foreach (var itemPerfil in lista)
                        {
                            itemPerfil.ListaOpcion = opcionDa.ListarPorPerfil(itemPerfil.PerfilId, empresaId, cn);
                        }
                    }
                }
            }
            catch (Exception ex) { throw ex; }
            finally { if (cn.State == ConnectionState.Open) cn.Close(); }

            return lista;
        }
    }
}
