using bilecom.ut;
using bilecom.be;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bilecom.da
{
    public class TipoAfectacionIgvDa
    {
        public List<TipoAfectacionIgvBe> Listar (SqlConnection cn)
        {
            List<TipoAfectacionIgvBe> respuesta=null;
            try
            {
                using (SqlCommand cmd = new SqlCommand("dbo.usp_tipoafectacionigv_listar",cn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        if(dr.HasRows)
                        {
                            respuesta = new List<TipoAfectacionIgvBe>();
                            while(dr.Read())
                            {
                                TipoAfectacionIgvBe item = new TipoAfectacionIgvBe();
                                item.TipoAfectacionIgvId = dr.GetData<int>("TipoAfectacionIgvId");
                                item.Id = dr.GetData<string>("Id");
                                item.Codigo = dr.GetData<string>("Codigo");
                                item.Descripcion = dr.GetData<string>("Descripcion");
                                item.FlagGravado = dr.GetData<bool>("FlagGravado");
                                item.FlagExonerado = dr.GetData<bool>("FlagExonerado");
                                item.FlagInafecto = dr.GetData<bool>("FlagInafecto");
                                item.FlagExportacion = dr.GetData<bool>("FlagExportacion");
                                item.FlagGratuito = dr.GetData<bool>("FlagGratuito");
                                item.FlagVentaArrozPilado = dr.GetData<bool>("FlagVentaArrozPilado");
                                respuesta.Add(item);
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

        public List<TipoAfectacionIgvBe> ListarPorEmpresa(int empresaId, SqlConnection cn)
        {
            List<TipoAfectacionIgvBe> respuesta = null;
            try
            {
                using (SqlCommand cmd = new SqlCommand("dbo.usp_tipoafectacionigv_listar_x_empresa", cn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@empresaId", empresaId);

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.HasRows)
                        {
                            respuesta = new List<TipoAfectacionIgvBe>();
                            while (dr.Read())
                            {
                                TipoAfectacionIgvBe item = new TipoAfectacionIgvBe();
                                item.TipoAfectacionIgvId = dr.GetData<int>("TipoAfectacionIgvId");
                                item.Id = dr.GetData<string>("Id");
                                item.Codigo = dr.GetData<string>("Codigo");
                                item.Descripcion = dr.GetData<string>("Descripcion");
                                item.FlagGravado = dr.GetData<bool>("FlagGravado");
                                item.FlagExonerado = dr.GetData<bool>("FlagExonerado");
                                item.FlagInafecto = dr.GetData<bool>("FlagInafecto");
                                item.FlagExportacion = dr.GetData<bool>("FlagExportacion");
                                item.FlagGratuito = dr.GetData<bool>("FlagGratuito");
                                item.FlagVentaArrozPilado = dr.GetData<bool>("FlagVentaArrozPilado");
                                respuesta.Add(item);
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
