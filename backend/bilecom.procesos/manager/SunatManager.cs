using bilecom.bl;
using bilecom.procesos.dto;
using bilecom.ut;
using FileHelpers;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace bilecom.procesos.manager
{
    public class SunatManager
    {
        public static void ProcesarPadronSunat()
        {
            string linkPadronSunat = AppSettings.Get<string>("sunat.link.padron_sunat");
            string linkPadronSunatLocal = AppSettings.Get<string>("sunat.link.padron_sunat_local");
            string folderPadronSunat = AppSettings.Get<string>("sunat.folder.padron_sunat").Replace(@"~\", AppDomain.CurrentDomain.BaseDirectory);
            DirectoryInfo dirInfoPadronSunat = new DirectoryInfo(folderPadronSunat);
            string filePadronSunatZip = AppSettings.Get<string>("sunat.file.zip.padron_sunat");
            string filePadronSunatLocalZip = AppSettings.Get<string>("sunat.file.zip.padron_sunat_local").Replace("~", AppDomain.CurrentDomain.BaseDirectory);
            string pathFilePadronSunatZip = Path.Combine(dirInfoPadronSunat.FullName, filePadronSunatZip);
            string pathFilePadronSunatLocalZip = Path.Combine(dirInfoPadronSunat.FullName, filePadronSunatLocalZip);
            FileInfo filInfoPadronSunatZip = new FileInfo(pathFilePadronSunatZip);
            FileInfo filInfoPadronSunatLocalZip = new FileInfo(pathFilePadronSunatLocalZip);

            string filePadronSunatTxt = AppSettings.Get<string>("sunat.file.txt.padron_sunat");
            string filePadronSunatLocalTxt = AppSettings.Get<string>("sunat.file.txt.padron_sunat_local").Replace("~", AppDomain.CurrentDomain.BaseDirectory);
            string pathFilePadronSunatTxt = Path.Combine(dirInfoPadronSunat.FullName, filePadronSunatTxt);
            string pathFilePadronSunatLocalTxt = Path.Combine(dirInfoPadronSunat.FullName, filePadronSunatLocalTxt);
            FileInfo filInfoPadronSunatTxt = new FileInfo(pathFilePadronSunatTxt);
            FileInfo filInfoPadronSunatLocalTxt = new FileInfo(pathFilePadronSunatLocalTxt);

            if (!dirInfoPadronSunat.Exists) dirInfoPadronSunat.Create();
            if (filInfoPadronSunatZip.Exists) filInfoPadronSunatZip.Delete();
            if (filInfoPadronSunatLocalZip.Exists) filInfoPadronSunatLocalZip.Delete();
            if (filInfoPadronSunatTxt.Exists) filInfoPadronSunatTxt.Delete();
            if (filInfoPadronSunatLocalTxt.Exists) filInfoPadronSunatLocalTxt.Delete();

            try
            {
                using (var webClient = new WebClient())
                {
                    webClient.DownloadFile(linkPadronSunat, filInfoPadronSunatZip.FullName);
                    webClient.DownloadFile(linkPadronSunatLocal, filInfoPadronSunatLocalZip.FullName);
                }

                if (filInfoPadronSunatZip.Exists) ZipFile.ExtractToDirectory(filInfoPadronSunatZip.FullName, folderPadronSunat);

                if (filInfoPadronSunatTxt.Exists) LoadFilePadronSunat(filInfoPadronSunatTxt.FullName);
            }
            catch (WebException ex) { throw ex; }
            catch (Exception ex) { throw ex; }
        }

        static void LoadFilePadronSunat(string fileName)
        {
            CommonBl commonBl = new CommonBl();
            using (DataSet dsMasivo = new DataSet("vRecords"))
            {
                using (DataTable tbl = new DataTable("vRecord"))
                {
                    tbl.Columns.Add("Ruc", typeof(string));
                    tbl.Columns.Add("RazonSocial", typeof(string));
                    tbl.Columns.Add("EstadoContribuyente", typeof(string));
                    tbl.Columns.Add("CondicionDomiciliaria", typeof(string));
                    tbl.Columns.Add("Ubigeo", typeof(string));
                    tbl.Columns.Add("TipoVia", typeof(string));
                    tbl.Columns.Add("NombreVia", typeof(string));
                    tbl.Columns.Add("CodigoZona", typeof(string));
                    tbl.Columns.Add("TipoZona", typeof(string));
                    tbl.Columns.Add("Numero", typeof(string));
                    tbl.Columns.Add("Interior", typeof(string));
                    tbl.Columns.Add("Lote", typeof(string));
                    tbl.Columns.Add("Departamento", typeof(string));
                    tbl.Columns.Add("Manzana", typeof(string));
                    tbl.Columns.Add("Kilometro", typeof(string));

                    dsMasivo.Tables.Add(tbl);
                    var total = 0;

                    try
                    {
                        FileHelperEngine engine = new FileHelperEngine(typeof(SunatPadronDto));
                        var data = engine.ReadFile(fileName).Cast<SunatPadronDto>().ToList();
                        Queue<SunatPadronDto> dataQ = new Queue<SunatPadronDto>(data);

                        data = null;

                        while (dataQ.Count > 0)
                        {
                            var item = dataQ.Dequeue();

                            DataRow drMasivo = dsMasivo.Tables["vRecord"].NewRow();

                            drMasivo["Ruc"] = item.Ruc.Trim();
                            drMasivo["RazonSocial"] = item.RazonSocial.Trim();
                            drMasivo["EstadoContribuyente"] = item.EstadoContribuyente.Trim();
                            drMasivo["CondicionDomiciliaria"] = item.CondicionDomiciliaria.Trim();
                            drMasivo["Ubigeo"] = item.Ubigeo.Trim();
                            drMasivo["TipoVia"] = item.TipoVia.Trim();
                            drMasivo["NombreVia"] = item.NombreVia.Trim();
                            drMasivo["CodigoZona"] = item.CodigoZona.Trim();
                            drMasivo["TipoZona"] = item.TipoZona.Trim();
                            drMasivo["Numero"] = item.Numero.Trim();
                            drMasivo["Interior"] = item.Interior.Trim();
                            drMasivo["Lote"] = item.Lote.Trim();
                            drMasivo["Departamento"] = item.Departamento.Trim();
                            drMasivo["Manzana"] = item.Manzana.Trim();
                            drMasivo["Kilometro"] = item.Kilometro.Trim();

                            dsMasivo.Tables["vRecord"].Rows.Add(drMasivo);

                            total++;
                        }
                    }
                    catch (FileHelpersException ex)
                    {
                        Console.WriteLine(ex.Message);
                        total = 0;
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                        total = 0;
                    }

                    if (total > 0)
                    {
                        try
                        {
                            string tableName = "[dbo].[SunatPadron]";

                            bool bulkInsertComplete = commonBl.BulkInsert(tableName, dsMasivo, withTruncate: true);
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex.Message);
                        }
                    }
                }

            }
        }
    }
}
