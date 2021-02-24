using bilecom.be;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bilecom.da
{
    public class TipoCambioDa
    {
        public TipoCambioBe Obtener(string fecha)
        {
            if (string.IsNullOrEmpty(fecha))
            {
                return null;
            }
            int anio = Int32.Parse(fecha.Substring(0, 4));
            int mes = fecha.Length > 4 ? Int32.Parse(fecha.Substring(5, 2)) : 0;
            int dia = fecha.Length > 7 ? Int32.Parse(fecha.Substring(8, 2)) : 0;

            if (new DateTime(anio, mes, dia) != DateTime.Today)
            {
                return null;
            }
            TipoCambioBe oTipoCambio = new TipoCambioBe();
            try
            {
                System.Net.WebClient client = new System.Net.WebClient();
                Byte[] pageData = client.DownloadData("https://www.sunat.gob.pe/a/txt/tipoCambio.txt");
                string strHTML = Encoding.UTF8.GetString(pageData);
                if (strHTML != null)
                {
                    string[] datos = strHTML.Split('|');
                    oTipoCambio = new TipoCambioBe() { Dia = datos[0], Compra= Convert.ToDecimal(datos[1]), Venta = Convert.ToDecimal(datos[2]) };
                }
            }
            catch (Exception ex)
            {
                oTipoCambio = null;
            }

            return oTipoCambio;
        }
    }
}
