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
    public class PaisDa
    {
        public List<PaisBe> Listar(SqlConnection cn)
        {
            List<PaisBe> lista = null;
            try
            {
                using (SqlCommand cmd = new SqlCommand("dbo.usp_pais_listar", cn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.HasRows)
                        {
                            lista = new List<PaisBe>();
                            while (dr.Read())
                            {
                                PaisBe item = new PaisBe();
                                item.PaisId = dr.GetData<int>("PaisId");
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

        public PaisBe Obtener(int paisId, SqlConnection cn)
        {
            PaisBe respuesta = null;
            try
            {
                using (SqlCommand cmd = new SqlCommand("dbo.usp_pais_obtener", cn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@paisId", paisId.GetNullable());

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.HasRows)
                        {
                            respuesta = new PaisBe();

                            if (dr.Read())
                            {
                                respuesta.PaisId = dr.GetData<int>("PaisId");
                                respuesta.Nombre = dr.GetData<string>("Nombre");
                                respuesta.CodigoSunat = dr.GetData<string>("CodigoSunat");
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
