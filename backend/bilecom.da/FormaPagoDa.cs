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
    public class FormaPagoDa
    {
        public List<FormaPagoBe> Listar(SqlConnection cn)
        {
            List<FormaPagoBe> lista = null;
            try
            {
                using (SqlCommand cmd = new SqlCommand("dbo.usp_formapago_listar", cn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.HasRows)
                        {
                            lista = new List<FormaPagoBe>();
                            while (dr.Read())
                            {
                                FormaPagoBe item = new FormaPagoBe();
                                item.FormaPagoId = dr.GetData<int>("FormaPagoId");
                                item.Descripcion = dr.GetData<string>("Descripcion");
                                item.DescripcionSunat = dr.GetData<string>("DescripcionSunat");
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
