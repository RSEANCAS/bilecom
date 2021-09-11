using bilecom.be;
using bilecom.be.Custom;
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
    public class CommonDa
    {
        public List<ComprobanteCustom> BuscarComprobanteVenta(int empresaId, int ambienteSunatId, int tipoComprobanteId, int serieId, string nroComprobante/*, string clienteRazonSocial*/, SqlConnection cn)
        {
            List<ComprobanteCustom> lista = new List<ComprobanteCustom>();
            using (SqlCommand cmd = new SqlCommand("dbo.usp_common_comprobanteventa_buscar", cn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@empresaId", empresaId.GetNullable());
                cmd.Parameters.AddWithValue("@ambienteSunatId", ambienteSunatId.GetNullable());
                cmd.Parameters.AddWithValue("@tipoComprobanteId", tipoComprobanteId.GetNullable());
                cmd.Parameters.AddWithValue("@serieId", serieId.GetNullable());
                cmd.Parameters.AddWithValue("@nroComprobante", nroComprobante.GetNullable());
                //cmd.Parameters.AddWithValue("@clienteRazonSocial", clienteRazonSocial.GetNullable());
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            ComprobanteCustom item = new ComprobanteCustom();
                            //item.Fila = dr.GetData<int>("Fila");
                            item.TipoComprobanteId = dr.GetData<int>("TipoComprobanteId");
                            item.ComprobanteId = dr.GetData<int>("ComprobanteId");
                            //item.TipoComprobante = new TipoComprobanteBe();
                            //item.TipoComprobante.Nombre = dr.GetData<string>("NombreTipoComprobante");
                            //item.SerieId = dr.GetData<int>("SerieId");
                            //item.Serie = new SerieBe();
                            //item.Serie.SerieId = dr.GetData<int>("SerieId");
                            //item.Serie.Serial = dr.GetData<string>("SerieSerial");
                            item.NroComprobante = dr.GetData<int>("NroComprobante");
                            //item.FechaHoraEmision = dr.GetData<DateTime>("FechaHoraEmision");
                            //item.ClienteId = dr.GetData<int>("ClienteId");
                            //item.Cliente = new ClienteBe();
                            //item.Cliente.ClienteId = dr.GetData<int>("ClienteId");
                            //item.Cliente.TipoDocumentoIdentidadId = dr.GetData<int>("ClienteTipoDocumentoIdentidadId");
                            //item.Cliente.TipoDocumentoIdentidad = new TipoDocumentoIdentidadBe();
                            //item.Cliente.TipoDocumentoIdentidad.TipoDocumentoIdentidadId = dr.GetData<int>("ClienteTipoDocumentoIdentidadId");
                            //item.Cliente.TipoDocumentoIdentidad.Descripcion = dr.GetData<string>("ClienteTipoDocumentoIdentidadDescripcion");
                            //item.Cliente.NroDocumentoIdentidad = dr.GetData<string>("ClienteNroDocumentoIdentidad");
                            //item.Cliente.RazonSocial = dr.GetData<string>("ClienteRazonSocial");
                            //item.Cliente.Direccion = dr.GetData<string>("ClienteDireccion");
                            lista.Add(item);
                        }
                    }
                }
            }
            return lista;
        }
    }
}
