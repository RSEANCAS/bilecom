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
    public class MonedaDa
    {
        public List<MonedaBe> Listar(SqlConnection cn)
        {
            List<MonedaBe> lista = null;
            try
            {
                using (SqlCommand cmd = new SqlCommand("dbo.usp_moneda_listar", cn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.HasRows)
                        {
                            lista = new List<MonedaBe>();
                            while (dr.Read())
                            {
                                MonedaBe item = new MonedaBe();
                                item.MonedaId = dr.GetData<int>("MonedaId");
                                item.Nombre = dr.GetData<string>("Nombre");
                                item.Simbolo = dr.GetData<string>("Simbolo");
                                item.Codigo = dr.GetData<string>("Codigo");
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

        public List<MonedaBe> ListarPorEmpresa(int empresaId, SqlConnection cn)
        {
            List<MonedaBe> lista = null;
            try
            {
                using (SqlCommand cmd = new SqlCommand("dbo.usp_moneda_listar_x_empresa", cn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@empresaId", empresaId);

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.HasRows)
                        {
                            lista = new List<MonedaBe>();
                            while (dr.Read())
                            {
                                MonedaBe item = new MonedaBe();
                                item.MonedaId = dr.GetData<int>("MonedaId");
                                item.Nombre = dr.GetData<string>("Nombre");
                                item.Simbolo = dr.GetData<string>("Simbolo");
                                item.Codigo = dr.GetData<string>("Codigo");
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

        public MonedaBe Obtener(int monedaId, SqlConnection cn)
        {
            MonedaBe respuesta = null;
            try
            {
                using (SqlCommand cmd = new SqlCommand("dbo.usp_moneda_obtener", cn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    //cmd.Parameters.AddWithValue("@empresaId", empresaId.GetNullable());
                    cmd.Parameters.AddWithValue("@monedaId", monedaId.GetNullable());

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.HasRows)
                        {
                            respuesta = new MonedaBe();

                            if (dr.Read())
                            {
                                respuesta.MonedaId = dr.GetData<int>("MonedaId");
                                respuesta.Nombre = dr.GetData<string>("Nombre");
                                respuesta.Simbolo = dr.GetData<string>("Simbolo");
                                respuesta.Codigo = dr.GetData<string>("Codigo");
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
