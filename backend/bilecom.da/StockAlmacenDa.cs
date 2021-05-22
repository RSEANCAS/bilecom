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
    public class StockAlmacenDa
    {
        public List<StockAlmacenBe> Buscar(int empresaId, int almacenId,int filtro, int pagina, int cantidadRegistros, string columnaOrden, string ordenMax, SqlConnection cn, out int totalRegistros)
        {
            List<StockAlmacenBe> respuesta=null;
            totalRegistros = 0;
            try
            {
                using (SqlCommand cmd = new SqlCommand("dbo.usp_stockalmacen_buscar", cn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@EmpresaId", empresaId.GetNullable());
                    cmd.Parameters.AddWithValue("@AlmacenId", almacenId.GetNullable());
                    cmd.Parameters.AddWithValue("@Filtro", filtro.GetNullable());
                    cmd.Parameters.AddWithValue("@pagina", pagina.GetNullable());
                    cmd.Parameters.AddWithValue("@cantidadRegistros", cantidadRegistros.GetNullable());
                    cmd.Parameters.AddWithValue("@columnaOrden", columnaOrden.GetNullable());
                    cmd.Parameters.AddWithValue("@ordenMax", ordenMax.GetNullable());

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.HasRows)
                        {
                            respuesta = new List<StockAlmacenBe>();
                            while (dr.Read())
                            {
                                StockAlmacenBe item = new StockAlmacenBe();
                                item.Fila = dr.GetData<int>("Fila");
                                item.ProductoId = dr.GetData<int>("ProductoId");
                                item.Codigo = dr.GetData<string>("Codigo");
                                item.CodigoSunat = dr.GetData<string>("CodigoSunat");
                                item.Descripcion = dr.GetData<string>("Nombre");
                                item.UnidadMedidaDescripcion = dr.GetData<string>("UnidadMedidaDescripcion");
                                item.StockMinimo = dr.GetData<decimal>("StockMinimo");
                                item.StockActual = dr.GetData<decimal>("StockActual");
                                item.TipoCalculo = dr.GetData<string>("TipoCalculo");
                                item.Monto = dr.GetData<decimal>("Monto");
                                respuesta.Add(item);

                                totalRegistros = dr.GetData<int>("Total");
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
