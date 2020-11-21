﻿using bilecom.be;
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
        public List<Cliente> fListar(SqlConnection cn, int empresaId)
        {
            List<Cliente> lCliente = new List<Cliente>();
            Cliente oCliente;
            using (SqlCommand oCommand = new SqlCommand("dbo.usp_cliente_listar", cn))
            {
                oCommand.CommandType = CommandType.StoredProcedure;
                oCommand.Parameters.AddWithValue("@empresaId", empresaId);
                using (SqlDataReader oDr = oCommand.ExecuteReader())
                {
                    if(oDr.HasRows)
                    {
                        if(oDr.Read())
                        {
                            oCliente = new Cliente();
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
