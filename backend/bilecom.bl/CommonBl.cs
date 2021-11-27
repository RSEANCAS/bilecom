using bilecom.be.Custom;
using bilecom.da;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace bilecom.bl
{
    public class CommonBl : Conexion
    {
        CommonDa commonDa = new CommonDa();

        public List<ComprobanteCustom> BuscarComprobanteVenta(int empresaId, int ambienteSunatId, int tipoComprobanteId, int serieId, string nroComprobante/*, string clienteRazonSocial*/)
        {
            List<ComprobanteCustom> lista = null;
            try
            {
                using (var cn = new SqlConnection(CadenaConexion))
                {
                    cn.Open();
                    lista = commonDa.BuscarComprobanteVenta(empresaId, ambienteSunatId, tipoComprobanteId, serieId, nroComprobante/*, clienteRazonSocial*/, cn);
                    cn.Close();
                }
            }
            catch (Exception ex) { lista = null; }
            //finally { if (cn.State == ConnectionState.Open) cn.Close(); }
            return lista;
        }

        public bool BulkInsert(string tableName, DataSet dataBulk, bool withTruncate = false)
        {
            bool bulkInsertComplete = false;

            try
            {
                using (SqlConnection cn = new SqlConnection(CadenaConexion))
                {
                    cn.Open();
                    if (withTruncate) bulkInsertComplete = commonDa.TruncateTable(tableName, cn);
                    else bulkInsertComplete = true;

                    if (bulkInsertComplete) bulkInsertComplete = commonDa.BulkInsert(tableName, dataBulk, cn);

                    cn.Close();
                }
            }
            catch (Exception ex) { bulkInsertComplete = false; }

            return bulkInsertComplete;
        }
    }
}
