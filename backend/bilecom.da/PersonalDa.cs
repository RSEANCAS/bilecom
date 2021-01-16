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
    public class PersonalDa
    {
        public List<PersonalBe> Buscar(int empresaId, string nroDocumentoIdentidad, string nombresCompletos, int pagina, int cantidadRegistros, string columnaOrden, string ordenMax, SqlConnection cn, out int totalRegistros)
        {
            totalRegistros = 0;
            List<PersonalBe> lista = new List<PersonalBe>();

            using (SqlCommand cmd = new SqlCommand("dbo.usp_personal_buscar", cn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@empresaId", empresaId.GetNullable());
                cmd.Parameters.AddWithValue("@nroDocumentoIdentidad", nroDocumentoIdentidad.GetNullable());
                cmd.Parameters.AddWithValue("@nombresCompletos", nombresCompletos.GetNullable());
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
                            PersonalBe item = new PersonalBe();
                            item.Fila = dr.GetData<int>("Fila");
                            item.PersonalId = dr.GetData<int>("PersonalId");
                            item.EmpresaId = dr.GetData<int>("EmpresaId");
                            item.TipoDocumentoIdentidadId = dr.GetData<int>("TipoDocumentoIdentidadId");
                            item.TipoDocumentoIdentidad = new TipoDocumentoIdentidadBe();
                            item.TipoDocumentoIdentidad.Descripcion = dr.GetData<string>("DescripcionTipoDocumentoIdentidad");
                            item.NroDocumentoIdentidad = dr.GetData<string>("NroDocumentoIdentidad");
                            item.NombresCompletos = dr.GetData<string>("NombresCompletos");
                            item.Direccion = dr.GetData<string>("Direccion");
                            item.Correo = dr.GetData<string>("Correo");
                            item.FlagActivo = dr.GetData<bool>("FlagActivo");
                            lista.Add(item);

                            totalRegistros = dr.GetData<int>("Total");
                        }
                    }
                }
            }
            return lista;
        }

        public PersonalBe Obtener(int empresaId, int personalId, SqlConnection cn)
        {
            PersonalBe item = null;
            try
            {
                using (SqlCommand cmd = new SqlCommand("dbo.usp_personal_obtener", cn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@empresaId", empresaId.GetNullable());
                    cmd.Parameters.AddWithValue("@personalId", personalId.GetNullable());

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.HasRows)
                        {
                            if (dr.Read())
                            {
                                item = new PersonalBe();
                                item.PersonalId = dr.GetData<int>("PersonalId");
                                item.EmpresaId = dr.GetData<int>("EmpresaId");
                                item.TipoDocumentoIdentidadId = dr.GetData<int>("TipoDocumentoIdentidadId");
                                item.NroDocumentoIdentidad = dr.GetData<string>("NroDocumentoIdentidad");
                                item.NombresCompletos = dr.GetData<string>("NombresCompletos");
                                item.Direccion = dr.GetData<string>("Direccion");
                                item.Correo = dr.GetData<string>("Correo");
                                item.FlagActivo = dr.GetData<bool>("FlagActivo");
                                item.DistritoId = dr.GetData<int>("DistritoId");
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                item = null;
            }
            return item;
        }

        public bool Guardar(PersonalBe registro, SqlConnection cn)
        {
            bool seGuardo = false;
            try
            {
                using (SqlCommand cmd = new SqlCommand("dbo.usp_personal_guardar", cn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@personalId", registro.PersonalId.GetNullable());
                    cmd.Parameters.AddWithValue("@empresaId", registro.EmpresaId.GetNullable());
                    cmd.Parameters.AddWithValue("@tipoDocumentoIdentidadId", registro.TipoDocumentoIdentidadId.GetNullable());
                    cmd.Parameters.AddWithValue("@nroDocumentoIdentidad", registro.NroDocumentoIdentidad.GetNullable());
                    cmd.Parameters.AddWithValue("@nombresCompletos", registro.NombresCompletos.GetNullable());
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

        public bool Eliminar(int empresaId, int personalId, string Usuario, SqlConnection cn)
        {
            bool seElimino = false;
            try
            {
                using (SqlCommand cmd = new SqlCommand("dbo.usp_personal_eliminar", cn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@personalId", personalId.GetNullable());
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
