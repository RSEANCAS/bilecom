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
    public class TipoDocumentoIdentidadDa
    {
        public List<TipoDocumentoIdentidadBe> Listar(SqlConnection cn)
        {
            List<TipoDocumentoIdentidadBe> lista = null;
            try
            {
                using (SqlCommand cmd = new SqlCommand("dbo.usp_tipodocumentoidentidad_listar", cn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.HasRows)
                        {
                            lista = new List<TipoDocumentoIdentidadBe>();
                            while (dr.Read())
                            {
                                TipoDocumentoIdentidadBe item = new TipoDocumentoIdentidadBe();
                                item.TipoDocumentoIdentidadId = dr.GetData<int>("TipoDocumentoIdentidadId");
                                item.Descripcion = dr.GetData<string>("Descripcion");
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
