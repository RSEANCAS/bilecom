using bilecom.be;
using bilecom.ut;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// En el DA van los métodos a ser llamados por el BL
namespace bilecom.da
{
    public class CotizacionDa
    {
        public List<CotizacionBe> Buscar(int empresaId, string nombresCompletosPersonal, string razonSocialCliente, DateTime fechaHoraEmisionDesde, DateTime fechaHoraEmisionHasta, int pagina, int cantidadRegistros, string columnaOrden, string ordenMax, SqlConnection cn, out int totalRegistros)
        {
            totalRegistros = 0;
            List<CotizacionBe> lista = null;
            using (SqlCommand cmd = new SqlCommand("usp_cotizacion_buscar", cn))
            {
                // Instanciando a la funcion CommandType
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@empresaId", empresaId.GetNullable());
                cmd.Parameters.AddWithValue("@nombresCompletosPersonal", nombresCompletosPersonal.GetNullable());
                cmd.Parameters.AddWithValue("@razonSocialCliente", razonSocialCliente.GetNullable());
                cmd.Parameters.AddWithValue("@fechaHoraEmisionDesde", fechaHoraEmisionDesde.GetNullable());
                cmd.Parameters.AddWithValue("@fechaHoraEmisionHasta", fechaHoraEmisionHasta.GetNullable());
                cmd.Parameters.AddWithValue("@pagina", pagina.GetNullable());
                cmd.Parameters.AddWithValue("@cantidadRegistros", cantidadRegistros.GetNullable());
                cmd.Parameters.AddWithValue("@columnaOrden", columnaOrden.GetNullable());
                cmd.Parameters.AddWithValue("@ordenMax", ordenMax.GetNullable());
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    if (dr.HasRows)
                    {
                        lista = new List<CotizacionBe>();

                        while (dr.Read())
                        {
                            CotizacionBe item = new CotizacionBe();
                            item.Fila = dr.GetData<int>("Fila");
                            item.CotizacionId = dr.GetData<int>("CotizacionId");
                            item.SerieId = dr.GetData<int>("SerieId");
                            item.Serie = new SerieBe();
                            item.Serie.SerieId = dr.GetData<int>("SerieId");
                            item.Serie.Serial = dr.GetData<string>("SerialSerie");
                            item.NroComprobante = dr.GetData<int>("NroComprobante");
                            //item.NroPedido = dr.GetData<string>("NroPedido");
                            item.FechaHoraEmision = dr.GetData<DateTime>("FechaHoraEmision");
                            //Para personal antes de asignarlo hay que instanciar
                            item.Personal = new PersonalBe();
                            item.Personal.NombresCompletos = dr.GetData<string>("NombresCompletosPersonal");
                            item.Cliente = new ClienteBe();
                            item.Cliente.RazonSocial = dr.GetData<string>("RazonSocialCliente");
                            item.TotalImporte = dr.GetData<decimal>("TotalImporte");
                            item.FlagAnulado = dr.GetData<bool>("FlagAnulado");
                            lista.Add(item);

                            totalRegistros = dr.GetData<int>("Total");
                        }
                    }
                }
            }
            return lista;
        }

        public CotizacionBe Obtener(int empresaId, int cotizacionId, SqlConnection cn)
        {
            CotizacionBe item = null;
            using (SqlCommand cmd = new SqlCommand("dbo.usp_cotizacion_obtener", cn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@empresaId", empresaId.GetNullable());
                cmd.Parameters.AddWithValue("@cotizacionId", cotizacionId.GetNullable());
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    if (dr.HasRows)
                    {
                        item = new CotizacionBe();

                        if(dr.Read())
                        {
                            item.EmpresaId = dr.GetData<int>("EmpresaId");
                            item.CotizacionId = dr.GetData<int>("CotizacionId");
                            item.FechaHoraEmision = dr.GetData<DateTime>("FechaHoraEmision");
                            item.SerieId = dr.GetData<int>("SerieId");
                            item.NroComprobante = dr.GetData<int>("NroComprobante");
                            item.ClienteId = dr.GetData<int>("ClienteId");
                            item.PersonalId = dr.GetData<int>("PersonalId");
                            item.MonedaId = dr.GetData<int>("MonedaId");
                            item.TotalImporte = dr.GetData<decimal>("TotalImporte");
                            item.FlagAnulado = dr.GetData<bool>("FlagAnulado");
                        }
                    }
                }
            }
            return item;
        }

        public bool Guardar(CotizacionBe registro, SqlConnection cn, out int? cotizacionId)
        {
            cotizacionId = null;
            bool seGuardo = false;
            try
            {
                using (SqlCommand cmd = new SqlCommand("dbo.usp_cotizacion_guardar", cn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter { ParameterName = "@cotizacionId", SqlDbType = SqlDbType.Int, Value = registro.CotizacionId.GetNullable(), Direction = ParameterDirection.InputOutput });
                    cmd.Parameters.AddWithValue("@empresaId", registro.EmpresaId.GetNullable());
                    cmd.Parameters.AddWithValue("@serieId", registro.SerieId.GetNullable());
                    cmd.Parameters.AddWithValue("@nroComprobante", registro.NroComprobante.GetNullable());
                    cmd.Parameters.AddWithValue("@monedaId", registro.MonedaId.GetNullable());
                    cmd.Parameters.AddWithValue("@clienteID", registro.ClienteId.GetNullable());
                    cmd.Parameters.AddWithValue("@personalId", registro.PersonalId.GetNullable());
                    cmd.Parameters.AddWithValue("@totalImporte", registro.TotalImporte.GetNullable());
                    cmd.Parameters.AddWithValue("@usuario", registro.Usuario.GetNullable());
                    
                    int filasAfectadas = cmd.ExecuteNonQuery();
                    seGuardo = filasAfectadas > 0;
                    if (seGuardo) cotizacionId = (int?)cmd.Parameters["@cotizacionId"].Value;
                }
            }
            catch (Exception ex)
            {
                seGuardo = false;
            }
            return seGuardo;
        }

        public bool Anular(CotizacionBe registro, SqlConnection cn)
        {
            bool seGuardo = false;
            try
            {
                using (SqlCommand cmd = new SqlCommand("dbo.usp_cotizacion_anular", cn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@empresaId", registro.EmpresaId.GetNullable());
                    cmd.Parameters.AddWithValue("@cotizacionId", registro.CotizacionId.GetNullable());
                    cmd.Parameters.AddWithValue("@usuario", registro.Usuario.GetNullable());

                    int filasAfectadas = cmd.ExecuteNonQuery();
                    seGuardo = filasAfectadas > 0;
                }
            }
            catch (Exception ex)
            {
                seGuardo = false;
            }
            return seGuardo;
        }
    }

}
