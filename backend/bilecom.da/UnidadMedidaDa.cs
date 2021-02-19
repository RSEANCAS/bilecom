using bilecom.ut;
using bilecom.be;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace bilecom.da
{
    public class UnidadMedidaDa
    {
        public List<UnidadMedidaBe> Listar (SqlConnection cn)
        {
            List<UnidadMedidaBe> respuesta = null;
            try
            {
                using (SqlCommand cmd = new SqlCommand("dbo.usp_unidadmedida_listar",cn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.HasRows)
                        {
                            respuesta = new List<UnidadMedidaBe>();
                            while(dr.Read())
                            {
                                UnidadMedidaBe item = new UnidadMedidaBe();
                                item.UnidadMedidaId = dr.GetData<int>("UnidadMedidaId");
                                item.Id = dr.GetData<string>("Id");
                                item.TipoProductoId = dr.GetData<int>("TipoProductoId");
                                item.Descripcion = dr.GetData<string>("Descripcion");
                                respuesta.Add(item);
                            }
                        }
                    }
                }
            }
            catch(Exception ex)
            {
                respuesta = null;
            }
            return respuesta;
        }

        public List<UnidadMedidaBe> ListarPorEmpresa(int empresaId, SqlConnection cn)
        {
            List<UnidadMedidaBe> respuesta = null;
            try
            {
                using (SqlCommand cmd = new SqlCommand("dbo.usp_unidadmedida_listar_x_empresa", cn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@empresaId", empresaId);

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.HasRows)
                        {
                            respuesta = new List<UnidadMedidaBe>();
                            while (dr.Read())
                            {
                                UnidadMedidaBe item = new UnidadMedidaBe();
                                item.UnidadMedidaId = dr.GetData<int>("UnidadMedidaId");
                                item.Id = dr.GetData<string>("Id");
                                item.TipoProductoId = dr.GetData<int>("TipoProductoId");
                                item.Descripcion = dr.GetData<string>("Descripcion");
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
