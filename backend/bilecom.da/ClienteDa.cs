﻿using bilecom.be;
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
    public class ClienteDa
    {
        public List<ClienteBe> fListar(SqlConnection cn, int empresaId, string nroDocumentoIdentidad, string razonSocial)
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
                            if (!DBNull.Value.Equals(oDr["ClienteId"])) oCliente.ClienteId = (int)oDr["ClienteId"];
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
        public bool ClienteGuardar(ClienteBe clienteBe, SqlConnection cn)
        {
            bool respuesta = false;
            try
            {
                using (SqlCommand oCommand = new SqlCommand("dbo.usp_cliente_guardar", cn))
                {
                    oCommand.CommandType = CommandType.StoredProcedure;
                    oCommand.Parameters.AddWithValue("@ClienteId", clienteBe.ClienteId.GetNullable());
                    oCommand.Parameters.AddWithValue("@EmpresaId", clienteBe.EmpresaId.GetNullable());
                    oCommand.Parameters.AddWithValue("@TipoDocumentoIdentidad", clienteBe.TipoDocumento.GetNullable());
                    oCommand.Parameters.AddWithValue("@NroDocumentoIdentidad", clienteBe.NroDocumentoIdentidad.GetNullable());
                    oCommand.Parameters.AddWithValue("@RazonSocial", clienteBe.RazonSocial.GetNullable());
                    oCommand.Parameters.AddWithValue("@NombreComercial", clienteBe.NombreComercial.GetNullable());
                    oCommand.Parameters.AddWithValue("@DistritoId", clienteBe.DistritoId.GetNullable());
                    oCommand.Parameters.AddWithValue("@Direccion", clienteBe.Direccion.GetNullable());
                    oCommand.Parameters.AddWithValue("@Correo", clienteBe.Correo.GetNullable());
                    oCommand.Parameters.AddWithValue("@FlagActivo", clienteBe.FlagActivo.GetNullable());
                    oCommand.Parameters.AddWithValue("@Usuario", clienteBe.Usuario.GetNullable());

                    int result = oCommand.ExecuteNonQuery();
                    if (result > 0) respuesta = true;
                }
            }
            catch (Exception ex)
            {
                respuesta = false;
            }
            return respuesta;
        }
    }
}
