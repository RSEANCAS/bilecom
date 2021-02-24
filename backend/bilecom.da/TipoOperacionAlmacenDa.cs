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
    public class TipoOperacionAlmacenDa
    {
        public List<TipoOperacionAlmacenBe> Listar(SqlConnection cn)
        {
            List<TipoOperacionAlmacenBe> respuesta = null;
            try
            {
                using (SqlCommand cmd = new SqlCommand("dbo.usp_tipooperacionalmacen_listar", cn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.HasRows)
                        {
                            respuesta = new List<TipoOperacionAlmacenBe>();
                            while (dr.Read())
                            {
                                TipoOperacionAlmacenBe item = new TipoOperacionAlmacenBe();
                                item.Id = dr.GetData<int>("Id");
                                item.TipoMovimientoId = dr.GetData<int>("TipoMovimientoId");
                                item.Numero = dr.GetData<string>("Numero");
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
