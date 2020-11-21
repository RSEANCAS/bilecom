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
    public class ClienteDa
    {
<<<<<<< HEAD
        public List<ClienteBe> fListar(SqlConnection cn, int empresaId)
=======
        public List<Cliente> fListar(SqlConnection cn, int empresaId, string nroDocumentoIdentidad, string razonSocial)
>>>>>>> 376a00755f6331d21a221b94219bb10102f3f4fe
        {
            List<ClienteBe> lCliente = new List<ClienteBe>();
            ClienteBe oCliente;
            using (SqlCommand oCommand = new SqlCommand("dbo.usp_cliente_listar", cn))
            {
                oCommand.CommandType = CommandType.StoredProcedure;
                oCommand.Parameters.AddWithValue("@empresaId", empresaId);
                oCommand.Parameters.AddWithValue("@nroDocumentoIdentidad", nroDocumentoIdentidad);
                oCommand.Parameters.AddWithValue("@razonSocial", razonSocial);
                using (SqlDataReader oDr = oCommand.ExecuteReader())
                {
                    if(oDr.HasRows)
                    {
                        if(oDr.Read())
                        {
                            oCliente = new ClienteBe();
                            if (!DBNull.Value.Equals(oDr["ClienteId"])) oCliente.ClientId = (int)oDr["ClienteId"];
                            if (!DBNull.Value.Equals(oDr["EmpresaId"])) oCliente.EmpresaId = (int)oDr["EmpresaId"];
                            if (!DBNull.Value.Equals(oDr["TipoDocumentoIdentidad"])) oCliente.TipoDocumento = (string)oDr["TipoDocumentoIdentidad"];
                            if (!DBNull.Value.Equals(oDr["NroDocumentoIdentidad"])) oCliente.NroDocumentoIdentidad = (string)oDr["NroDocumentoIdentidad"];
                            if (!DBNull.Value.Equals(oDr["RazonSocial"])) oCliente.RazonSocial = (string)oDr["RazonSocial"];
                            if (!DBNull.Value.Equals(oDr["NombreComercial"])) oCliente.NombreComercial = (string)oDr["NombreComercial"];
                            if (!DBNull.Value.Equals(oDr["DistritoId"])) oCliente.DistritoId = (int)oDr["DistritoId"];
                            if (!DBNull.Value.Equals(oDr["Direccion"])) oCliente.Direccion = (string)oDr["Direccion"];
                            if (!DBNull.Value.Equals(oDr["Correo"])) oCliente.Direccion = (string)oDr["Correo"];
                            if (!DBNull.Value.Equals(oDr["FlagActivo"])) oCliente.FlagActivo = (bool)oDr["FlagActivo"];
                            lCliente.Add(oCliente);
                        }
                    }
                }
            }
            return lCliente;
        }
    }
}
