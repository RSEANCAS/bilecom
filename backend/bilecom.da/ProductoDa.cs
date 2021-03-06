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
    public class ProductoDa
    {
        public List<ProductoBe> Buscar(string categoriaNombre, string nombre, int empresaId, int pagina, int cantidadRegistros, string columnaOrden, string ordenMax, SqlConnection cn, out int totalRegistros)
        {
            totalRegistros = 0;
            List<ProductoBe> lista = null;
            
            using (SqlCommand cmd = new SqlCommand("dbo.usp_producto_buscar", cn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@categoriaNombre", categoriaNombre.GetNullable());
                cmd.Parameters.AddWithValue("@nombre", nombre.GetNullable());
                cmd.Parameters.AddWithValue("@EmpresaId", empresaId.GetNullable());
                cmd.Parameters.AddWithValue("@pagina", pagina.GetNullable());
                cmd.Parameters.AddWithValue("@cantidadRegistros", cantidadRegistros.GetNullable());
                cmd.Parameters.AddWithValue("@columnaOrden", columnaOrden.GetNullable());
                cmd.Parameters.AddWithValue("@ordenMax", ordenMax.GetNullable());
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    if(dr.HasRows)
                    {
                        lista = new List<ProductoBe>();
                        while (dr.Read())
                        {
                            ProductoBe item = new ProductoBe();
                            item.Fila = dr.GetData<int>("Fila");
                            item.ProductoId = dr.GetData<int>("ProductoId");
                            item.Nombre = dr.GetData<string>("NombreProducto");
                            item.TipoAfectacionIgvId = dr.GetData<int>("TipoAfectacionIgvId");
                            item.UnidadMedidaId = dr.GetData<string>("UnidadMedidaId");
                            item.TipoCalculo = dr.GetData<string>("TipoCalculo");
                            item.Monto = dr.GetData<decimal>("Monto");
                            item.StockMinimo = dr.GetData<decimal>("StockMinimo");
                            item.categoriaProducto = new CategoriaProductoBe();
                            item.categoriaProducto.Nombre = dr.GetData<string>("NombreCategoria");
                            item.StockMinimo = dr.GetData<decimal>("StockMinimo");
                            lista.Add(item);

                            totalRegistros = dr.GetData<int>("Total");
                        }
                    }
                }
            }
            return lista;
        }

        public List<ProductoBe> BuscarPorCodigo(int? tipoProductoId, string codigo, int empresaId, SqlConnection cn, int sedeAlmacenId = 0)
        {
            List<ProductoBe> lista = null;

            using (SqlCommand cmd = new SqlCommand("dbo.usp_producto_buscar_x_codigo", cn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@tipoProductoId", tipoProductoId.GetNullable());
                cmd.Parameters.AddWithValue("@codigo", codigo.GetNullable());
                cmd.Parameters.AddWithValue("@empresaId", empresaId.GetNullable());
                cmd.Parameters.AddWithValue("@SedeAlmacenId", sedeAlmacenId.GetNullable());
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    if (dr.HasRows)
                    {
                        lista = new List<ProductoBe>();
                        while (dr.Read())
                        {
                            ProductoBe item = new ProductoBe();
                            item.Fila = dr.GetData<int>("Fila");
                            item.ProductoId = dr.GetData<int>("ProductoId");
                            item.EmpresaId = dr.GetData<int>("EmpresaId");
                            item.CategoriaId = dr.GetData<int>("CategoriaId");
                            item.TipoProductoId = dr.GetData<int>("TipoProductoId");
                            item.Codigo = dr.GetData<string>("Codigo");
                            item.CodigoSunat = dr.GetData<string>("CodigoSunat");
                            item.Nombre = dr.GetData<string>("Nombre");
                            item.StockActual = dr.GetData<decimal>("Stock");
                            item.TipoAfectacionIgvId = dr.GetData<int>("TipoAfectacionIgvId");
                            item.UnidadMedidaId = dr.GetData<string>("UnidadMedidaId");
                            item.TipoCalculo = dr.GetData<string>("TipoCalculo");
                            //item.categoriaProducto = new CategoriaProductoBe();
                            //item.categoriaProducto.Nombre = dr.GetData<string>("NombreCategoria");
                            //item.Stock = dr.GetData<int>("Stock");
                            item.StockMinimo = dr.GetData<decimal>("StockMinimo");
                            lista.Add(item);
                        }
                    }
                }
            }
            return lista;
        }

        public List<ProductoBe> BuscarPorNombre(int? tipoProductoId, string nombre, int empresaId, SqlConnection cn, int sedeAlmacenId = 0)
        {
            List<ProductoBe> lista = null;

            using (SqlCommand cmd = new SqlCommand("dbo.usp_producto_buscar_x_nombre", cn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@tipoProductoId", tipoProductoId.GetNullable());
                cmd.Parameters.AddWithValue("@nombre", nombre.GetNullable());
                cmd.Parameters.AddWithValue("@empresaId", empresaId.GetNullable());
                cmd.Parameters.AddWithValue("@SedeAlmacenId", sedeAlmacenId.GetNullable());
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    if (dr.HasRows)
                    {
                        lista = new List<ProductoBe>();
                        while (dr.Read())
                        {
                            ProductoBe item = new ProductoBe();
                            item.Fila = dr.GetData<int>("Fila");
                            item.ProductoId = dr.GetData<int>("ProductoId");
                            item.EmpresaId = dr.GetData<int>("EmpresaId");
                            item.CategoriaId = dr.GetData<int>("CategoriaId");
                            item.TipoProductoId = dr.GetData<int>("TipoProductoId");
                            item.Codigo = dr.GetData<string>("Codigo");
                            item.CodigoSunat = dr.GetData<string>("CodigoSunat");
                            item.Nombre = dr.GetData<string>("Nombre");
                            item.StockActual = dr.GetData<decimal>("Stock");
                            item.TipoAfectacionIgvId = dr.GetData<int>("TipoAfectacionIgvId");
                            item.UnidadMedidaId = dr.GetData<string>("UnidadMedidaId");
                            item.TipoCalculo = dr.GetData<string>("TipoCalculo");
                            //item.categoriaProducto = new CategoriaProductoBe();
                            //item.categoriaProducto.Nombre = dr.GetData<string>("NombreCategoria");
                            //item.Stock = dr.GetData<int>("Stock");
                            item.StockMinimo = dr.GetData<decimal>("StockMinimo");
                            lista.Add(item);
                        }
                    }
                }
            }
            return lista;
        }

        public bool Guardar(ProductoBe productoBe, SqlConnection cn)
        {
            bool seGuardo = false;
            try
            { 
                using (SqlCommand cmd = new SqlCommand("dbo.usp_producto_guardar", cn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@EmpresaId", productoBe.EmpresaId.GetNullable());
                    cmd.Parameters.AddWithValue("@ProductoId", productoBe.ProductoId.GetNullable());
                    cmd.Parameters.AddWithValue("@CategoriaId", productoBe.CategoriaId.GetNullable());
                    cmd.Parameters.AddWithValue("@Nombre", productoBe.Nombre.GetNullable());
                    cmd.Parameters.AddWithValue("@TipoAfectacionIgvId", productoBe.TipoAfectacionIgvId.GetNullable());
                    cmd.Parameters.AddWithValue("@UnidadMedidaId", productoBe.UnidadMedidaId.GetNullable());
                    cmd.Parameters.AddWithValue("@TipoCalculo", productoBe.TipoCalculo.GetNullable());
                    cmd.Parameters.AddWithValue("@Monto", productoBe.Monto.GetNullable());
                    cmd.Parameters.AddWithValue("@StockMinimo", productoBe.StockMinimo.GetNullable());
                    cmd.Parameters.AddWithValue("@Usuario", productoBe.Usuario.GetNullable());
                    cmd.Parameters.AddWithValue("@TipoProductoId", productoBe.TipoProductoId.GetNullable());
                    cmd.Parameters.AddWithValue("@CodigoSunat", productoBe.CodigoSunat.GetNullable());
                    cmd.Parameters.AddWithValue("@Codigo", productoBe.Codigo.GetNullable());


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

        public ProductoBe Obtener(int empresaId, int productoId, SqlConnection cn)
        {
            ProductoBe respuesta = null;
            try
            {
                using (SqlCommand cmd = new SqlCommand("dbo.usp_producto_obtener", cn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@EmpresaId", empresaId.GetNullable());
                    cmd.Parameters.AddWithValue("@ProductoId", productoId.GetNullable());

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.HasRows)
                        {
                            if (dr.Read())
                            {
                                respuesta = new ProductoBe();
                                respuesta.Nombre = dr.GetData<string>("Nombre");
                                respuesta.CategoriaId = dr.GetData<int>("CategoriaId");
                                respuesta.TipoAfectacionIgvId = dr.GetData<int>("TipoAfectacionIgvId");
                                respuesta.UnidadMedidaId = dr.GetData<string>("UnidadMedidaId");
                                respuesta.TipoCalculo = dr.GetData<string>("TipoCalculo");
                                respuesta.Monto = dr.GetData<decimal>("Monto");
                                respuesta.StockMinimo = dr.GetData<decimal>("StockMinimo");
                                respuesta.Codigo = dr.GetData<string>("Codigo");
                                respuesta.CodigoSunat = dr.GetData<string>("CodigoSunat");
                                respuesta.TipoProductoId = dr.GetData<int>("TipoProductoId");
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

        public bool Eliminar(int empresaId, int productoId, string Usuario, SqlConnection cn)
        {
            bool seElimino = false;
            try
            {
                using (SqlCommand cmd = new SqlCommand("dbo.usp_producto_eliminar", cn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@productoId", productoId.GetNullable());
                    cmd.Parameters.AddWithValue("@empresaId", empresaId.GetNullable());
                    cmd.Parameters.AddWithValue("@Usuario", Usuario.GetNullable());

                    int filasAfectadas = cmd.ExecuteNonQuery();
                    seElimino = filasAfectadas > 0;
                }
            }
            catch (Exception ex)
            {
                seElimino = false;
            }
            return seElimino;
        }

    }
}
