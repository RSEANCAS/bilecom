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
                            item.Fila = dr.GetData<int>("Fila");
                            item.ProveedorId = dr.GetData<int>("ProveedorId");
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

        public ProveedorBe Obtener(int empresaId, int proveedorId,SqlConnection cn)
        {

            ProveedorBe respuesta = null;

            using (SqlCommand cmd = new SqlCommand("usp_proveedor_obtener", cn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@empresaId", empresaId.GetNullable());
                cmd.Parameters.AddWithValue("@ProveedorId", proveedorId.GetNullable());
                
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    if (dr.HasRows)
                    {
                        respuesta = new ProveedorBe();
                        while (dr.Read())
                        {
                            respuesta.TipoDocumentoIdentidadId = dr.GetData<int>("TipoDocumentoIdentidadId");
                            respuesta.NroDocumentoIdentidad = dr.GetData<string>("NroDocumentoIdentidad");
                            respuesta.RazonSocial = dr.GetData<string>("RazonSocial");
                            respuesta.NombreComercial = dr.GetData<string>("NombreComercial");
                            respuesta.PaisId = dr.GetData<int>("PaisId");
                            respuesta.DistritoId = dr.GetData<int>("DistritoId");
                            respuesta.Direccion = dr.GetData<string>("Direccion");
                            respuesta.Correo = dr.GetData<string>("Correo");
                        }
                    }
                }
            }

            return respuesta;
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
                    cmd.Parameters.AddWithValue("@TipoDocumentoIdentidadId", proveedorBe.TipoDocumentoIdentidadId.GetNullable());
                    cmd.Parameters.AddWithValue("@NroDocumentoIdentidad", proveedorBe.NroDocumentoIdentidad.GetNullable());
                    cmd.Parameters.AddWithValue("@RazonSocial", proveedorBe.RazonSocial.GetNullable());
                    cmd.Parameters.AddWithValue("@NombreComercial", proveedorBe.NombreComercial.GetNullable());
                    cmd.Parameters.AddWithValue("@PaisId", proveedorBe.PaisId.GetNullable());
                    cmd.Parameters.AddWithValue("@DistritoId", proveedorBe.DistritoId.GetNullable());
                    cmd.Parameters.AddWithValue("@Direccion", proveedorBe.Direccion.GetNullable());
                    cmd.Parameters.AddWithValue("@Correo", proveedorBe.Correo.GetNullable());
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

        public bool Eliminar(int empresaId, int proveedorId, string Usuario, SqlConnection cn)
        {
            bool seElimino = false;
            try
            {
                using (SqlCommand cmd = new SqlCommand("dbo.usp_proveedor_eliminar", cn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@proveedorId", proveedorId.GetNullable());
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
