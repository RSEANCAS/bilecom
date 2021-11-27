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
    public class SunatPadronDa
    {
        public SunatPadronBe ObtenerPorRuc(string ruc, SqlConnection cn)
        {
            SunatPadronBe item = null;
            try
            {
                using (SqlCommand cmd = new SqlCommand("dbo.usp_sunat_padron_obtener_x_ruc", cn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@ruc", ruc.GetNullable());

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.HasRows)
                        {
                            if (dr.Read())
                            {
                                item = new SunatPadronBe();
                                item.Ruc = dr.GetData<string>("Ruc");
                                item.RazonSocial = dr.GetData<string>("RazonSocial");
                                item.EstadoContribuyente = dr.GetData<string>("EstadoContribuyente");
                                item.CondicionDomiciliaria = dr.GetData<string>("CondicionDomiciliaria");
                                item.Ubigeo = dr.GetData<string>("Ubigeo");
                                item.DistritoId = dr.GetData<int?>("DistritoId");
                                item.ProvinciaId = dr.GetData<int?>("ProvinciaId");
                                item.DepartamentoId = dr.GetData<int?>("DepartamentoId");
                                item.PaisId = dr.GetData<int?>("PaisId");
                                item.Direccion = dr.GetData<string>("Direccion");
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
    }
}
