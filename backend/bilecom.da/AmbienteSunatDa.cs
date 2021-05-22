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
    public class AmbienteSunatDa
    {
        public List<AmbienteSunatBe> Listar(SqlConnection cn)
        {
            List<AmbienteSunatBe> lista = null;
            try
            {
                using (SqlCommand cmd = new SqlCommand("dbo.usp_ambientesunat_listar", cn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.HasRows)
                        {
                            lista = new List<AmbienteSunatBe>();
                            while (dr.Read())
                            {
                                AmbienteSunatBe item = new AmbienteSunatBe();
                                item.AmbienteSunatId = dr.GetData<int>("AmbienteSunatId");
                                item.Nombre = dr.GetData<string>("Nombre");
                                item.ColorHexadecimal = dr.GetData<string>("ColorHexadecimal");
                                item.ServicioWebUrlVenta = dr.GetData<string>("ServicioWebUrlVenta");
                                item.ServicioWebUrlGuia = dr.GetData<string>("ServicioWebUrlGuia");
                                item.ServicioWebUrlOtros = dr.GetData<string>("ServicioWebUrlOtros");
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

        public AmbienteSunatBe Obtener(int ambienteSunatId, SqlConnection cn)
        {
            AmbienteSunatBe item = null;
            try
            {
                using (SqlCommand cmd = new SqlCommand("dbo.usp_ambientesunat_obtener", cn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@ambienteSunatId", ambienteSunatId.GetNullable());

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.HasRows)
                        {
                            item = new AmbienteSunatBe();

                            if (dr.Read())
                            {
                                item.AmbienteSunatId = dr.GetData<int>("AmbienteSunatId");
                                item.Nombre = dr.GetData<string>("Nombre");
                                item.ColorHexadecimal = dr.GetData<string>("ColorHexadecimal");
                                item.ServicioWebUrlVenta = dr.GetData<string>("ServicioWebUrlVenta");
                                item.ServicioWebUrlGuia = dr.GetData<string>("ServicioWebUrlGuia");
                                item.ServicioWebUrlOtros = dr.GetData<string>("ServicioWebUrlOtros");
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                item = null;
            }
            return item;
        }
    }
}
