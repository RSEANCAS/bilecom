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
    public class KardexDa
    {
        public List<KardexNivel1Be> BuscarNivel1(int empresaId, int almacenId, int productoId,DateTime fechaInicio,DateTime fechaFinal, int pagina, int cantidadRegistros, string columnaOrden, string ordenMax, SqlConnection cn, out int totalRegistros)
        {
            List<KardexNivel1Be> respuesta = null;
            totalRegistros = 0;
            try
            {
                using (SqlCommand cmd = new SqlCommand("dbo.usp_kardex_buscar", cn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Nivel", 1);
                    cmd.Parameters.AddWithValue("@EmpresaId", empresaId.GetNullable());
                    cmd.Parameters.AddWithValue("@AlmacenId", almacenId.GetNullable());
                    cmd.Parameters.AddWithValue("@ProductoId", productoId.GetNullable());
                    cmd.Parameters.AddWithValue("@FechaInicio", fechaInicio.GetNullable());
                    cmd.Parameters.AddWithValue("@FechaFinal", fechaFinal.GetNullable());
                    cmd.Parameters.AddWithValue("@pagina", pagina.GetNullable());
                    cmd.Parameters.AddWithValue("@cantidadRegistros", cantidadRegistros.GetNullable());
                    cmd.Parameters.AddWithValue("@columnaOrden", columnaOrden.GetNullable());
                    cmd.Parameters.AddWithValue("@ordenMax", ordenMax.GetNullable());

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.HasRows)
                        {
                            respuesta = new List<KardexNivel1Be>();
                            while (dr.Read())
                            {
                                KardexNivel1Be item = new KardexNivel1Be();
                                item.Fila = dr.GetData<int>("Fila");
                                item.ProductoId = dr.GetData<int>("ProductoId");
                                item.Codigo = dr.GetData<string>("Codigo");
                                item.CodigoSunat = dr.GetData<string>("CodigoSunat");
                                item.Nombre = dr.GetData<string>("Nombre");
                                item.UnidadMedidaDescripcion = dr.GetData<string>("UnidadMedidaDescripcion");
                                item.StockActual = dr.GetData<decimal>("StockActual");
                                respuesta.Add(item);

                                totalRegistros = dr.GetData<int>("Total");
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                respuesta = null;
            }
            return respuesta;
        }
        public List<KardexNivel2Be> BuscarNivel2(int empresaId, int almacenId, int productoId, DateTime fechaInicio, DateTime fechaFinal, int pagina, int cantidadRegistros, string columnaOrden, string ordenMax, SqlConnection cn, out int totalRegistros)
        {
            List<KardexNivel2Be> respuesta = null;
            totalRegistros = 0;
            try
            {
                using (SqlCommand cmd = new SqlCommand("dbo.usp_kardex_buscar", cn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Nivel", 2);
                    cmd.Parameters.AddWithValue("@EmpresaId", empresaId.GetNullable());
                    cmd.Parameters.AddWithValue("@AlmacenId", almacenId.GetNullable());
                    cmd.Parameters.AddWithValue("@ProductoId", productoId.GetNullable());
                    cmd.Parameters.AddWithValue("@FechaInicio", fechaInicio.GetNullable());
                    cmd.Parameters.AddWithValue("@FechaFinal", fechaFinal.GetNullable());
                    cmd.Parameters.AddWithValue("@pagina", pagina.GetNullable());
                    cmd.Parameters.AddWithValue("@cantidadRegistros", cantidadRegistros.GetNullable());
                    cmd.Parameters.AddWithValue("@columnaOrden", columnaOrden.GetNullable());
                    cmd.Parameters.AddWithValue("@ordenMax", ordenMax.GetNullable());

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.HasRows)
                        {
                            respuesta = new List<KardexNivel2Be>();
                            while (dr.Read())
                            {
                                KardexNivel2Be item = new KardexNivel2Be();
                                item.Fila = dr.GetData<int>("Fila");
                                item.FechaHoraEmision = dr.GetData<DateTime>("FechaHoraEmision");
                                item.ProductoId = dr.GetData<int>("ProductoId");
                                item.Cantidad = dr.GetData<decimal>("Cantidad");
                                item.PrecioUnitario = dr.GetData<decimal>("PrecioUnitario");
                                item.TotalImporte = dr.GetData<decimal>("TotalImporte");
                                item.TipoMovimientoDescripcion = dr.GetData<string>("TipoMovimientoDescripcion");
                                respuesta.Add(item);

                                totalRegistros = dr.GetData<int>("Total");
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                respuesta = null;
            }
            return respuesta;
        }
    }
}
