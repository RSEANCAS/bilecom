using bilecom.be;
using bilecom.da;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bilecom.bl
{
    public class FormaPagoBl : Conexion
    {
        FormaPagoDa formaPagoDa = new FormaPagoDa();

        public List<FormaPagoBe> ListarFormaPago()
        {
            List<FormaPagoBe> lista = null;
            try
            {
                using (var cn = new SqlConnection(CadenaConexion))
                {
                    cn.Open();
                    lista = formaPagoDa.Listar(cn);
                    cn.Close();
                }
            }
            catch (Exception ex) { lista = null; }
            //finally { if (cn.State == ConnectionState.Open) cn.Close(); }
            return lista;
        }
    }
}
