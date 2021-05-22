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
    public class SerieDa
    {
        public List<SerieBe> ListarPorTipoComprobante(int empresaId, int ambienteSunatId, int tipoComprobanteId, SqlConnection cn)
        {
            List<SerieBe> lista = null;
            try
            {
                using (SqlCommand cmd = new SqlCommand("dbo.usp_serie_listar_x_tipocomprobante", cn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@empresaId", empresaId.GetNullable());
                    cmd.Parameters.AddWithValue("@ambienteSunatId", ambienteSunatId.GetNullable());
                    cmd.Parameters.AddWithValue("@tipoComprobanteId", tipoComprobanteId.GetNullable());

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.HasRows)
                        {
                            lista = new List<SerieBe>();
                            while (dr.Read())
                            {
                                SerieBe item = new SerieBe();
                                item.EmpresaId = dr.GetData<int>("EmpresaId");
                                item.SerieId = dr.GetData<int>("SerieId");
                                item.AmbienteSunatId = dr.GetData<int>("AmbienteSunatId");
                                item.TipoComprobanteId = dr.GetData<int>("TipoComprobanteId");
                                item.Serial = dr.GetData<string>("Serial");
                                item.ValorInicial = dr.GetData<int>("ValorInicial");
                                item.ValorFinal = dr.GetData<int?>("ValorFinal");
                                item.FlagSinFinal = dr.GetData<bool>("FlagSinFinal");
                                item.ValorActual = dr.GetData<int>("ValorActual");
                                lista.Add(item);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                lista = null;
            }
            return lista;
        }

        public List<SerieBe> Buscar(int empresaId, int ambienteSunatId, int? tipoComprobanteId, string serial, int pagina, int cantidadRegistros, string columnaOrden, string ordenMax, SqlConnection cn, out int totalRegistros)
        {
            totalRegistros = 0;
            List<SerieBe> lista = new List<SerieBe>();
            using (SqlCommand cmd = new SqlCommand("dbo.usp_serie_buscar", cn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@empresaId", empresaId.GetNullable());
                cmd.Parameters.AddWithValue("@ambienteSunatId", ambienteSunatId.GetNullable());
                cmd.Parameters.AddWithValue("@tipoComprobanteId", tipoComprobanteId.GetNullable());
                cmd.Parameters.AddWithValue("@serial", serial.GetNullable());
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
                            SerieBe item = new SerieBe();
                            item.Fila = dr.GetData<int>("Fila");
                            item.SerieId = dr.GetData<int>("SerieId");
                            item.EmpresaId = dr.GetData<int>("EmpresaId");
                            item.AmbienteSunatId = dr.GetData<int>("AmbienteSunatId");
                            item.TipoComprobanteId = dr.GetData<int>("TipoComprobanteId");
                            item.TipoComprobante = new TipoComprobanteBe();
                            item.TipoComprobante.Nombre = dr.GetData<string>("NombreTipoComprobante");
                            item.Serial = dr.GetData<string>("Serial");
                            item.ValorInicial = dr.GetData<int>("ValorInicial");
                            item.ValorFinal = dr.GetData<int>("ValorFinal");
                            item.ValorActual = dr.GetData<int>("ValorActual");
                            lista.Add(item);

                            totalRegistros = dr.GetData<int>("Total");
                        }
                    }
                }
            }
            return lista;
        }

        public SerieBe Obtener(int empresaId, int serieId, SqlConnection cn)
        {
            SerieBe respuesta = null;
            try
            {
                using (SqlCommand cmd = new SqlCommand("dbo.usp_serie_obtener", cn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@EmpresaId", empresaId.GetNullable());
                    cmd.Parameters.AddWithValue("@SerieId", serieId.GetNullable());

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.HasRows)
                        {
                            if (dr.Read())
                            {
                                respuesta = new SerieBe();
                                respuesta.SerieId = dr.GetData<int>("SerieId");
                                respuesta.AmbienteSunatId = dr.GetData<int>("AmbienteSunatId");
                                respuesta.TipoComprobanteId = dr.GetData<int>("TipoComprobanteId");
                                respuesta.Serial = dr.GetData<string>("Serial");
                                respuesta.ValorInicial = dr.GetData<int>("ValorInicial");
                                respuesta.ValorFinal = dr.GetData<int>("ValorFinal");
                                respuesta.FlagSinFinal = dr.GetData<bool>("FlagSinFinal");
                                respuesta.ValorActual = dr.GetData<int>("ValorActual");
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

        public bool Guardar(SerieBe serieBe, SqlConnection cn)
        {
            bool seGuardo = false;
            try
            {
                using (SqlCommand cmd = new SqlCommand("dbo.usp_serie_guardar", cn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@EmpresaId", serieBe.EmpresaId.GetNullable());
                    cmd.Parameters.AddWithValue("@SerieId", serieBe.SerieId.GetNullable());
                    cmd.Parameters.AddWithValue("@ambienteSunatId", serieBe.AmbienteSunatId.GetNullable());
                    cmd.Parameters.AddWithValue("@TipoComprobanteId", serieBe.TipoComprobanteId.GetNullable());
                    cmd.Parameters.AddWithValue("@Serial", serieBe.Serial.GetNullable());
                    cmd.Parameters.AddWithValue("@ValorInicial", serieBe.ValorInicial.GetNullable());
                    cmd.Parameters.AddWithValue("@ValorFinal", serieBe.ValorFinal.GetNullable());
                    cmd.Parameters.AddWithValue("@FlagSinFinal", serieBe.FlagSinFinal.GetNullable());
                    cmd.Parameters.AddWithValue("@ValorActual", serieBe.ValorActual.GetNullable());
                    cmd.Parameters.AddWithValue("@Usuario", serieBe.Usuario.GetNullable());

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

        public bool Eliminar(int empresaId, int serieId, string Usuario, SqlConnection cn)
        {
            bool seElimino = false;
            try
            {
                using (SqlCommand cmd = new SqlCommand("dbo.usp_serie_eliminar", cn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@serieId", serieId.GetNullable());
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
