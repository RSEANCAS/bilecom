using bilecom.be;
using bilecom.ut;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bilecom.da
{
    public class TipoTributoDa
    {
        public TipoTributoBe Obtener(int tipoTributoId, SqlConnection cn)
        {
            TipoTributoBe respuesta = null;
            try
            {
                using (SqlCommand cmd = new SqlCommand("dbo.usp_tipoTributo_obtener", cn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@tipoTributoId", tipoTributoId.GetNullable());

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.HasRows)
                        {
                            respuesta = new TipoTributoBe();

                            if (dr.Read())
                            {
                                respuesta.TipoTributoId = dr.GetData<int>("TipoTributoId");
                                respuesta.Descripcion = dr.GetData<string>("Descripcion");
                                respuesta.Nombre = dr.GetData<string>("Nombre");
                                respuesta.Codigo = dr.GetData<string>("Codigo");
                                respuesta.CodigoNombre = dr.GetData<string>("CodigoNombre");
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                respuesta = null;
            }
            return respuesta;
        }

        public List<TipoTributoBe> Listar(SqlConnection cn)
        {
            List<TipoTributoBe> lista = null;
            try
            {
                using (SqlCommand cmd = new SqlCommand("dbo.usp_tipotributo_listar", cn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.HasRows)
                        {
                            lista = new List<TipoTributoBe>();

                            while (dr.Read())
                            {
                                TipoTributoBe item = new TipoTributoBe();

                                item.TipoTributoId = dr.GetData<int>("TipoTributoId");
                                item.Descripcion = dr.GetData<string>("Descripcion");
                                item.Nombre = dr.GetData<string>("Nombre");
                                item.Codigo = dr.GetData<string>("Codigo");
                                item.CodigoNombre = dr.GetData<string>("CodigoNombre");

                                lista.Add(item);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                lista = null;
            }
            return lista;
        }
    }
}
