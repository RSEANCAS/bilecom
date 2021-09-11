using bilecom.ut;
using System;
using System.Collections.Generic;
using System.IO;
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
            string filePadronSunat = AppSettings.Get<string>("sunat.file.padron_sunat");
            string filePadronSunatLocal = AppSettings.Get<string>("sunat.file.padron_sunat_local").Replace("~", AppDomain.CurrentDomain.BaseDirectory);
            string pathFilePadronSunat = Path.Combine(dirInfoPadronSunat.FullName, filePadronSunat);
            string pathFilePadronSunatLocal = Path.Combine(dirInfoPadronSunat.FullName, filePadronSunatLocal);
            FileInfo filInfoPadronSunat = new FileInfo(pathFilePadronSunat);
            FileInfo filInfoPadronSunatLocal = new FileInfo(pathFilePadronSunatLocal);

            if (!dirInfoPadronSunat.Exists) dirInfoPadronSunat.Create();
            if (filInfoPadronSunat.Exists) filInfoPadronSunat.Delete();
            if (filInfoPadronSunatLocal.Exists) filInfoPadronSunatLocal.Delete();

            try
            {
                using (var webClient = new WebClient())
                {
                    //webClient.DownloadProgressChanged += WebClient_DownloadProgressChanged;
                    webClient.DownloadFile(linkPadronSunat, filInfoPadronSunat.FullName);
                    webClient.DownloadFile(linkPadronSunatLocal, filInfoPadronSunatLocal.FullName);
                }
            }
            catch (WebException ex) { throw ex; }
            catch (Exception ex) { throw ex; }
        }

        //private static void WebClient_DownloadProgressChanged(object sender, DownloadProgressChangedEventArgs e)
        //{
        //    var webClient = (WebClient)sender;
            
        //    Console.WriteLine($"Descargando archivo de {webClient.BaseAddress} - {e.ProgressPercentage}%");
        //}
    }
}
