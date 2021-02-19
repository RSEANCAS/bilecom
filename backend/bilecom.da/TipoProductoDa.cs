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
    public class TipoProductoDa
    {
        public List<TipoProductoBe> Listar(SqlConnection cn)
        {
            List<TipoProductoBe> respuesta = null;
            try
            {
                using (SqlCommand cmd = new SqlCommand("dbo.usp_tipoproducto_listar", cn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.HasRows)
                        {
                            respuesta = new List<TipoProductoBe>();
                            while (dr.Read())
                            {
                                TipoProductoBe item = new TipoProductoBe();
                                item.TipoProductoId = dr.GetData<int>("TipoProductoId");
                                item.Nombre = dr.GetData<string>("Nombre");
                                respuesta.Add(item);
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

        public List<TipoProductoBe> ListarPorEmpresa(int empresaId, SqlConnection cn)
        {
            List<TipoProductoBe> respuesta = null;
            try
            {
                using (SqlCommand cmd = new SqlCommand("dbo.usp_tipoproducto_listar_x_empresa", cn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@empresaId", empresaId);

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.HasRows)
                        {
                            respuesta = new List<TipoProductoBe>();
                            while (dr.Read())
                            {
                                TipoProductoBe item = new TipoProductoBe();
                                item.TipoProductoId = dr.GetData<int>("TipoProductoId");
                                item.Nombre = dr.GetData<string>("Nombre");
                                respuesta.Add(item);
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
    }
}
