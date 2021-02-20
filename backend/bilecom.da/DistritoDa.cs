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
    public class DistritoDa
    {
        public List<DistritoBe> Listar(SqlConnection cn)
        {
            List<DistritoBe> lista = null;
            try
            {
                using (SqlCommand cmd = new SqlCommand("dbo.usp_distrito_listar", cn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.HasRows)
                        {
                            lista = new List<DistritoBe>();
                            while (dr.Read())
                            {
                                DistritoBe item = new DistritoBe();
                                item.DistritoId = dr.GetData<int>("DistritoId");
                                item.ProvinciaId = dr.GetData<int>("ProvinciaId");
                                item.CodigoUbigeo = dr.GetData<string>("CodigoUbigeo");
                                item.Nombre = dr.GetData<string>("Nombre");
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

        public DistritoBe Obtener(int distritoId, SqlConnection cn)
        {
            DistritoBe respuesta = null;
            try
            {
                using (SqlCommand cmd = new SqlCommand("dbo.usp_distrito_obtener", cn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@distritoId", distritoId.GetNullable());

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.HasRows)
                        {
                            respuesta = new DistritoBe();

                            if (dr.Read())
                            {
                                respuesta.DistritoId = dr.GetData<int>("DistritoId");
                                respuesta.Nombre = dr.GetData<string>("Nombre");
                                respuesta.ProvinciaId = dr.GetData<int>("ProvinciaId");
                                respuesta.CodigoUbigeo = dr.GetData<string>("CodigoUbigeo");
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
