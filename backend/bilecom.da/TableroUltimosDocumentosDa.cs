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
    public class TableroUltimosDocumentosDa
    {
        public List<TableroUltimosDocumentosBe> Listar(int EmpresaId, int CantidadRegistros,SqlConnection cn)
        {
            List<TableroUltimosDocumentosBe> respuesta = null;
            try
            {
                using (SqlCommand cmd = new SqlCommand("dbo.usp_tablero_ultimosdocumentosemitidos", cn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@empresaId", EmpresaId.GetNullable());
                    cmd.Parameters.AddWithValue("@cantidadRegistros", CantidadRegistros.GetNullable());
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.HasRows)
                        {
                            respuesta = new List<TableroUltimosDocumentosBe>();
                            while (dr.Read())
                            {
                                TableroUltimosDocumentosBe item = new TableroUltimosDocumentosBe();
                                item.TipoComprobanteId = dr.GetData<int>("TipoComprobanteId");
                                item.TipoComprobanteCodigoSunat = dr.GetData<string>("TipoComprobanteCodigoSunat");
                                item.TipoComprobanteNombre = dr.GetData<string>("TipoComprobanteNombre");
                                item.TipoDocumentoDescripcion = dr.GetData<string>("TipoDocumentoDescripcion");
                                item.ClienteRazonSocial = dr.GetData<string>("ClienteRazonSocial");
                                item.Serie = dr.GetData<string>("Serie");
                                item.Numero = dr.GetData<string>("Numero");
                                item.FechaEmision = dr.GetData<DateTime>("FechaEmision");
                                item.Total = dr.GetData<decimal>("Total");
                                item.SimboloMoneda = dr.GetData<string>("SimboloMoneda");
                                item.EstadoSunatId = dr.GetData<int?>("EstadoSunatId");
                                item.FlagAnulado = dr.GetData<bool>("FlagAnulado");
                                respuesta.Add(item);
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
