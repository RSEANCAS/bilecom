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
    class CotizacionDa
    {
        public List<CotizacionBe> fListar(SqlConnection cn, int EmpresaId, string NombresCompletosPersonal, string RazonSocial, 
            DateTime FechaHoraEmisionDesde, DateTime FechaHoraEmisionHasta)
        {
            List<CotizacionBe> lCotizacion = null;
            using (SqlCommand cmd = new SqlCommand("usp_cotizacion_listar", cn))
            {
                // Instanciando a la funcion CommandType
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@empresaId", EmpresaId);
                cmd.Parameters.AddWithValue("@nombresPersonal", NombresCompletosPersonal);
                cmd.Parameters.AddWithValue("@cliente", RazonSocial);
                cmd.Parameters.AddWithValue("@fechaHoraEmisionDesde", FechaHoraEmisionDesde);
                cmd.Parameters.AddWithValue("@fechaHoraEmisionHasta", FechaHoraEmisionHasta);
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    if (dr.HasRows)
                    {
                        lCotizacion = new List<CotizacionBe>();

                        while (dr.Read())
                        {
                            CotizacionBe item = new CotizacionBe();
                            item.Fila = dr.GetData<int>("Fila");
                            item.SerieId = dr.GetData<int>("SerieId");
                            item.NroComprobante = dr.GetData<int>("NroComprobante");

                            lCotizacion.Add(item);
                        }
                    }
                }
            }
            return lCotizacion;
        }
    }
}
