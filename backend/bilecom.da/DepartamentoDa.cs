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
    public class DepartamentoDa
    {
        public List<DepartamentoBe> DepartamentoListar (SqlConnection cn)
        {
            List<DepartamentoBe> respuesta = null;
            try
            {
                using (SqlCommand oCommand = new SqlCommand("dbo.usp_departamento_listar", cn))
                {
                    oCommand.CommandType = CommandType.StoredProcedure;
                    using (SqlDataReader oDr = oCommand.ExecuteReader())
                    {
                        if (oDr.HasRows)
                        {
                            respuesta = new List<DepartamentoBe>();
                            while (oDr.Read())
                            {
                                respuesta.Add(new DepartamentoBe
                                {
                                    DepartamentoId = oDr.GetData<int>("DepartamentoId"),
                                    PaisId = oDr.GetData<int>("PaisId"),
                                    CodigoUbigeo = oDr.GetData<string>("CodigoUbigeo"),
                                    Nombre = oDr.GetData<string>("Nombre")
                                });
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
