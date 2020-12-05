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
        public List<TipoDocumentoIdentidadBe> TipoDocumentoIdentidadListar(SqlConnection cn)
        {
            List<TipoDocumentoIdentidadBe> respuesta = null;
            try
            {
                using (SqlCommand oCommand = new SqlCommand("dbo.usp_tipodocumentoidentidad_listar", cn))
                {
                    oCommand.CommandType = CommandType.StoredProcedure;
                    using (SqlDataReader oDr = oCommand.ExecuteReader())
                    {
                        if (oDr.HasRows)
                        {
                            respuesta = new List<TipoDocumentoIdentidadBe>();
                            while (oDr.Read())
                            {
                                respuesta.Add(new TipoDocumentoIdentidadBe
                                {
                                    TipoDocumentoIdentidadId = oDr.GetData<int>("TipoDocumentoIdentidadId"),
                                    Descripcion = oDr.GetData<string>("Descripcion")
                                });
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
