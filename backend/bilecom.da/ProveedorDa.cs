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
    public class ProveedorDa
    {
        public List<ProveedorBe> fListar(SqlConnection cn, int proveedorId)
        {
            List<ProveedorBe> lProveedor = new List<ProveedorBe>();
            ProveedorBe oProveedor;
            using (SqlCommand oCommand = new SqlCommand("usp_proveedor_listar", cn))
            {
                oCommand.CommandType = CommandType.StoredProcedure;
                oCommand.Parameters.AddWithValue("@proveedorId", proveedorId);
                using (SqlDataReader oDr = oCommand.ExecuteReader())
                {
                    if(oDr.HasRows)
                    {
                        if(oDr.Read())
                        {
                            oProveedor = new ProveedorBe();
                            if (!DBNull.Value.Equals(oDr["ProveedorId"])) oProveedor.ProveedorId = (int)oDr["ProveedorId"];
                            if (!DBNull.Value.Equals(oDr["TipoDocumentoIdentidad"])) oProveedor.TipoDocumentoIdentidad = (int)oDr["TipoDocumentoIdentidad"];
                            if (!DBNull.Value.Equals(oDr["NroDocumentoIdentidad"])) oProveedor.NroDocumentoIdentidad = (string)oDr["NroDocumentoIdentidad"];
                            if (!DBNull.Value.Equals(oDr["RazonSocial"])) oProveedor.RazonSocial = (string)oDr["RazonSocial"];
                            lProveedor.Add(oProveedor);
                        }
                    }
                }
            }

            return lProveedor;
        }
    }
}
