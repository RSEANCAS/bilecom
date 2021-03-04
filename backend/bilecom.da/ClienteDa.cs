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
    public class ClienteDa
    {
        public List<ClienteBe> Buscar(int empresaId, string nroDocumentoIdentidad, string razonSocial, int pagina, int cantidadRegistros, string columnaOrden, string ordenMax, SqlConnection cn, out int totalRegistros)
        {
            totalRegistros = 0;
            List<ClienteBe> lista = new List<ClienteBe>();
            using (SqlCommand cmd = new SqlCommand("dbo.usp_cliente_buscar", cn))
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
                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            ClienteBe item = new ClienteBe();
                            item.Fila = dr.GetData<int>("Fila");
                            item.ClienteId = dr.GetData<int>("ClienteId");
                            item.EmpresaId = dr.GetData<int>("EmpresaId");
                            item.TipoDocumentoIdentidadId = dr.GetData<int>("TipoDocumentoIdentidadId");
                            item.TipoDocumentoIdentidad = new TipoDocumentoIdentidadBe();
                            item.TipoDocumentoIdentidad.Descripcion = dr.GetData<string>("DescripcionTipoDocumentoIdentidad");
                            item.NroDocumentoIdentidad = dr.GetData<string>("NroDocumentoIdentidad");
                            item.RazonSocial = dr.GetData<string>("RazonSocial");
                            item.NombreComercial = dr.GetData<string>("NombreComercial");
                            item.DistritoId = dr.GetData<int>("DistritoId");
                            item.Direccion = dr.GetData<string>("Direccion");
                            item.Correo = dr.GetData<string>("Correo");
                            lista.Add(item);

                            totalRegistros = dr.GetData<int>("Total");
                        }
                    }
                }
            }
            return lista;
        }

        public ClienteBe Obtener(int empresaId, int clienteId, SqlConnection cn)
        {
            ClienteBe respuesta = null;
            try
            {
                using (SqlCommand cmd = new SqlCommand("dbo.usp_cliente_obtener", cn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@EmpresaId", empresaId.GetNullable());
                    cmd.Parameters.AddWithValue("@ClienteId", clienteId.GetNullable());

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.HasRows)
                        {
                            if (dr.Read())
                            {
                                respuesta = new ClienteBe();
                                respuesta.ClienteId = dr.GetData<int>("ClienteId");
                                respuesta.TipoDocumentoIdentidadId = dr.GetData<int>("TipoDocumentoIdentidadId");
                                respuesta.NroDocumentoIdentidad = dr.GetData<string>("NroDocumentoIdentidad");
                                respuesta.RazonSocial = dr.GetData<string>("RazonSocial");
                                respuesta.NombreComercial = dr.GetData<string>("NombreComercial");
                                respuesta.DistritoId = dr.GetData<int>("DistritoId");
                                respuesta.Direccion = dr.GetData<string>("Direccion");
                                respuesta.Correo = dr.GetData<string>("Correo");
                                respuesta.FlagActivo = dr.GetData<bool>("FlagActivo");
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

        public bool Guardar(ClienteBe registro, SqlConnection cn)
        {
            bool seGuardo = false;
            try
            {
                using (SqlCommand cmd = new SqlCommand("dbo.usp_cliente_guardar", cn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@clienteId", registro.ClienteId.GetNullable());
                    cmd.Parameters.AddWithValue("@empresaId", registro.EmpresaId.GetNullable());
                    cmd.Parameters.AddWithValue("@tipoDocumentoIdentidadId", registro.TipoDocumentoIdentidadId.GetNullable());
                    cmd.Parameters.AddWithValue("@nroDocumentoIdentidad", registro.NroDocumentoIdentidad.GetNullable());
                    cmd.Parameters.AddWithValue("@razonSocial", registro.RazonSocial.GetNullable());
                    cmd.Parameters.AddWithValue("@nombreComercial", registro.NombreComercial.GetNullable());
                    cmd.Parameters.AddWithValue("@distritoId", registro.DistritoId.GetNullable());
                    cmd.Parameters.AddWithValue("@direccion", registro.Direccion.GetNullable());
                    cmd.Parameters.AddWithValue("@correo", registro.Correo.GetNullable());
                    cmd.Parameters.AddWithValue("@flagActivo", registro.FlagActivo.GetNullable());
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

        public bool Eliminar(int empresaId,int clienteId,string Usuario,SqlConnection cn)
        {
            bool seElimino = false;
            try
            {
                using (SqlCommand cmd = new SqlCommand("dbo.usp_cliente_eliminar", cn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@clienteId", clienteId.GetNullable());
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
