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
    public class MovimientoDa
    {
        public List<MovimientoBe> Buscar(int empresaId, string nombresCompletosPersonal, string razonSocial, DateTime fechaHoraEmisionDesde, DateTime fechaHoraEmisionHasta, int pagina, int cantidadRegistros, string columnaOrden, string ordenMax, SqlConnection cn, out int totalRegistros)
        {
            totalRegistros = 0;
            List<MovimientoBe> lista = null;
            using (SqlCommand cmd = new SqlCommand("web.usp_movimiento_buscar", cn))
            {
                // Instanciando a la funcion CommandType
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@empresaId", empresaId.GetNullable());
                cmd.Parameters.AddWithValue("@nombresCompletosPersonal", nombresCompletosPersonal.GetNullable());
                cmd.Parameters.AddWithValue("@razonSocialCliente", razonSocial.GetNullable());
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
                        lista = new List<MovimientoBe>();

                        while (dr.Read())
                        {
                            MovimientoBe item = new MovimientoBe();
                            item.Fila = dr.GetData<int>("Fila");
                            item.MovimientoId = dr.GetData<int>("MovimientoId");

                            item.TipoMovimientoId = dr.GetData<int>("TipoMovimientoId");
                            item.TipoMovimiento = new TipoMovimientoBe();
                            item.TipoMovimiento.Id = dr.GetData<int>("TipoMovimientoId");
                            item.TipoMovimiento.Descripcion= dr.GetData<string>("TipoMovimientoDescripcion");

                            item.SerieId = dr.GetData<int>("SerieId");
                            item.Serie = new SerieBe();
                            item.Serie.SerieId = dr.GetData<int>("SerieId");
                            item.Serie.Serial = dr.GetData<string>("SerialSerie");

                            item.NroMovimiento = dr.GetData<int>("NroMovimiento");
                            //item.NroPedido = dr.GetData<string>("NroPedido");
                            item.FechaHoraEmision = dr.GetData<DateTime>("FechaHoraEmision");
                            //Para personal antes de asignarlo hay que instanciar
                            item.Personal = new PersonalBe();
                            item.Personal.NombresCompletos = dr.GetData<string>("NombresCompletosPersonal");
                            item.Cliente = new ClienteBe();
                            item.Cliente.RazonSocial = dr.GetData<string>("RazonSocialCliente");
                            item.Proveedor = new ProveedorBe();
                            item.Proveedor.RazonSocial = dr.GetData<string>("RazonSocialProveedor");
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

        public MovimientoBe Obtener(int empresaId, int movimientoId, SqlConnection cn)
        {
            MovimientoBe item = null;
            using (SqlCommand cmd = new SqlCommand("web.usp_movimiento_obtener", cn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@empresaId", empresaId.GetNullable());
                cmd.Parameters.AddWithValue("@movimientoId", movimientoId.GetNullable());
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    if (dr.HasRows)
                    {
                        item = new MovimientoBe();

                        if (dr.Read())
                        {
                            item.EmpresaId = dr.GetData<int>("EmpresaId");
                            item.MovimientoId = dr.GetData<int>("MovimientoId");
                            item.FechaHoraEmision = dr.GetData<DateTime>("FechaHoraEmision");
                            item.SerieId = dr.GetData<int>("SerieId");
                            item.NroMovimiento = dr.GetData<int>("NroMovimiento");
                            item.ClienteId = dr.GetData<int>("ClienteId");
                            item.ProveedorId = dr.GetData<int>("ProveedorId");
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
        public bool Guardar(MovimientoBe registro, SqlConnection cn, out int? movimientoId)
        {
            movimientoId = null;
            bool seGuardo = false;
            try
            {
                using (SqlCommand cmd = new SqlCommand("web.usp_movimiento_guardar", cn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter { ParameterName = "@MovimientoId", SqlDbType = SqlDbType.Int, Value = registro.MovimientoId.GetNullable(), Direction = ParameterDirection.InputOutput });
                    cmd.Parameters.AddWithValue("@EmpresaId", registro.EmpresaId.GetNullable());
                    cmd.Parameters.AddWithValue("@SedeId", registro.SedeId.GetNullable());
                    cmd.Parameters.AddWithValue("@SerieId", registro.SerieId.GetNullable());
                    cmd.Parameters.AddWithValue("@NroMovimiento", registro.NroMovimiento.GetNullable());
                    cmd.Parameters.AddWithValue("@TipoMovimientoId", registro.TipoMovimientoId.GetNullable());
                    cmd.Parameters.AddWithValue("@ClienteId", registro.ClienteId.GetNullable());
                    cmd.Parameters.AddWithValue("@PersonalId", registro.PersonalId.GetNullable());
                    cmd.Parameters.AddWithValue("@ProveedorId", registro.ProveedorId.GetNullable());
                    cmd.Parameters.AddWithValue("@MonedaId", registro.MonedaId.GetNullable());
                    cmd.Parameters.AddWithValue("@FlagAnulado", registro.FlagAnulado.GetNullable());
                    cmd.Parameters.AddWithValue("@usuario", registro.Usuario.GetNullable());


                    int filasAfectadas = cmd.ExecuteNonQuery();
                    seGuardo = filasAfectadas > 0;
                    if (seGuardo) movimientoId = (int?)cmd.Parameters["@MovimientoId"].Value;
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
