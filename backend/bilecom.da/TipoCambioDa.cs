using bilecom.be;
using bilecom.ut;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static bilecom.enums.Enums;

namespace bilecom.da
{
    public class TipoCambioDa
    {
        //public TipoCambioBe Obtener(string fecha)
        public TipoCambioBe ObtenerDolar()
        {
            //if (string.IsNullOrEmpty(fecha))
            //{
            //    return null;
            //}
            //int anio = Int32.Parse(fecha.Substring(0, 4));
            //int mes = fecha.Length > 4 ? Int32.Parse(fecha.Substring(5, 2)) : 0;
            //int dia = fecha.Length > 7 ? Int32.Parse(fecha.Substring(8, 2)) : 0;

            //if (new DateTime(anio, mes, dia) != DateTime.Today)
            //{
            //    return null;
            //}
            TipoCambioBe oTipoCambio = new TipoCambioBe();
            try
            {
                System.Net.WebClient client = new System.Net.WebClient();
                Byte[] pageData = client.DownloadData("https://www.sunat.gob.pe/a/txt/tipoCambio.txt");
                string strHTML = Encoding.UTF8.GetString(pageData);
                if (strHTML != null)
                {
                    string[] datos = strHTML.Split('|');
                    string fechaStr = datos[0];
                    DateTime fecha = DateTime.ParseExact(fechaStr, "dd/MM/yyyy", System.Globalization.CultureInfo.GetCultureInfo("es-PE"));
                    int monedaId = (int)Moneda.Dolares;
                    oTipoCambio = new TipoCambioBe() { Fecha = fecha, MonedaId = monedaId, Compra = Convert.ToDecimal(datos[1]), Venta = Convert.ToDecimal(datos[2]) };
                }
            }
            catch (Exception ex)
            {
                oTipoCambio = null;
            }

            return oTipoCambio;
        }
        public bool Guardar(TipoCambioBe tipoCambioBe, SqlConnection cn)
        {
            bool seGuardo = false;
            try
            {
                using (SqlCommand cmd = new SqlCommand("dbo.usp_tipocambio_guardar", cn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@fecha", tipoCambioBe.Fecha.GetNullable());
                    cmd.Parameters.AddWithValue("@monedaId", tipoCambioBe.MonedaId.GetNullable());
                    cmd.Parameters.AddWithValue("@montoCompra", tipoCambioBe.Compra.GetNullable());
                    cmd.Parameters.AddWithValue("@montoVenta", tipoCambioBe.Venta.GetNullable());
                    cmd.Parameters.AddWithValue("@usuario", tipoCambioBe.Usuario.GetNullable());
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
