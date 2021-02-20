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
    public class ProvinciaDa
    {
        public List<ProvinciaBe> Listar(SqlConnection cn)
        {
            List<ProvinciaBe> lista = null;
            try
            {
                using (SqlCommand cmd = new SqlCommand("dbo.usp_provincia_listar", cn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.HasRows)
                        {
                            lista = new List<ProvinciaBe>();
                            while (dr.Read())
                            {
                                ProvinciaBe item = new ProvinciaBe();
                                item.DepartamentoId = dr.GetData<int>("DepartamentoId");
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

        public ProvinciaBe Obtener(int provinciaId, SqlConnection cn)
        {
            ProvinciaBe respuesta = null;
            try
            {
                using (SqlCommand cmd = new SqlCommand("dbo.usp_provincia_obtener", cn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@provinciaId", provinciaId.GetNullable());

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.HasRows)
                        {
                            respuesta = new ProvinciaBe();

                            if (dr.Read())
                            {
                                respuesta.ProvinciaId = dr.GetData<int>("ProvinciaId");
                                respuesta.Nombre = dr.GetData<string>("Nombre");
                                respuesta.DepartamentoId = dr.GetData<int>("DepartamentoId");
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
