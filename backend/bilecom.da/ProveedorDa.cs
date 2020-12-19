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
    public class ProveedorDa
    {
        public List<ProveedorBe> Buscar(int empresaId, string nroDocumentoIdentidad, string razonSocial, int pagina, int cantidadRegistros, string columnaOrden, string ordenMax, SqlConnection cn, out int totalRegistros)
        {
            totalRegistros = 0;
            List<ProveedorBe> lista = new List<ProveedorBe>();
            
            using (SqlCommand cmd = new SqlCommand("usp_proveedor_buscar", cn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@empresaId", empresaId.GetNullable());
                cmd.Parameters.AddWithValue("@nroDocumentoIdentidad", nroDocumentoIdentidad.GetNullable());
                cmd.Parameters.AddWithValue("@razonSocial", razonSocial.GetNullable());
                cmd.Parameters.AddWithValue("@pagina", pagina.GetNullable());
                cmd.Parameters.AddWithValue("@cantidadRegistros", cantidadRegistros.GetNullable());
                cmd.Parameters.AddWithValue("@columnaOrden", columnaOrden.GetNullable());
                cmd.Parameters.AddWithValue("@ordenMax", ordenMax.GetNullable());
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    if(dr.HasRows)
                    {
                        while(dr.Read())
                        {
                            ProveedorBe item = new ProveedorBe();
                            item.ProveedorId = dr.GetData<int>("Fila");
                            item.TipoDocumentoIdentidadId = dr.GetData<int>("TipoDocumento");
                            item.TipoDocumentoIdentidad = new TipoDocumentoIdentidadBe();
                            item.TipoDocumentoIdentidad.Descripcion = dr.GetData<string>("DescripcionTipoDocumentoIdentidad");
                            item.NroDocumentoIdentidad = dr.GetData<string>("NroDocumentoIdentidad");
                            item.RazonSocial = dr.GetData<string>("RazonSocial");
                            lista.Add(item);

                            totalRegistros = dr.GetData<int>("Total");
                        }
                    }
                }
            }

            return lista;
        }

        public bool Guardar(ProveedorBe proveedorBe, SqlConnection cn)
        {
            bool seGuardo = false;
            try
            {
                using (SqlCommand cmd = new SqlCommand("dbo.usp_proveedor_guardar", cn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@EmpresaId", proveedorBe.EmpresaId.GetNullable());
                    cmd.Parameters.AddWithValue("@ProveedorId", proveedorBe.ProveedorId.GetNullable());
                    cmd.Parameters.AddWithValue("@TipoDocumentoIdentidad", proveedorBe.TipoDocumentoIdentidad.GetNullable());
                    cmd.Parameters.AddWithValue("@NroDocumentoIdentidad", proveedorBe.NroDocumentoIdentidad.GetNullable());
                    cmd.Parameters.AddWithValue("@RazonSocial", proveedorBe.RazonSocial.GetNullable());
                    cmd.Parameters.AddWithValue("@Usuario", proveedorBe.Usuario.GetNullable());


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
