using bilecom.ut;
using bilecom.be;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bilecom.da
{
    public class TipoAfectacionIgvDa
    {
        public List<TipoAfectacionIgvBe> Listar (SqlConnection cn)
        {
            List<TipoAfectacionIgvBe> respuesta=null;
            try
            {
                using (SqlCommand cmd = new SqlCommand("dbo.usp_tipoafectacionigv_listar",cn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        if(dr.HasRows)
                        {
                            respuesta = new List<TipoAfectacionIgvBe>();
                            while(dr.Read())
                            {
                                TipoAfectacionIgvBe item = new TipoAfectacionIgvBe();
                                item.Id = dr.GetData<string>("Id");
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
    }
}
