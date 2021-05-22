using bilecom.be;
using bilecom.bl;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using static bilecom.enums.Enums;

namespace bilecom.sunat.testenvio
{
    public partial class EnvioSunat : Form
    {
        int EmpresaId { get; set; }
        string XmlString { get; set; }
        List<AmbienteSunatBe> ListaAmbienteSunat { get; set; }
        List<TipoComprobanteBe> ListaTipoComprobante { get; set; }

        EmpresaBl empresaBl = new EmpresaBl();
        EmpresaConfiguracionBl empresaConfiguracionBl = new EmpresaConfiguracionBl();
        EmpresaAmbienteSunatBl empresaAmbienteSunatBl = new EmpresaAmbienteSunatBl();
        AmbienteSunatBl ambienteSunatBl = new AmbienteSunatBl();
        TipoComprobanteBl tipoComprobanteBl = new TipoComprobanteBl();

        Emitir emitir = new Emitir();

        public EnvioSunat()
        {
            InitializeComponent();
        }

        private void EnvioSunat_Load(object sender, EventArgs e)
        {
            CargarDatos();
        }

        void CargarDatos()
        {
            ListaAmbienteSunat = ambienteSunatBl.ListarAmbienteSunat();
            ListaAmbienteSunat.Prepend(new AmbienteSunatBe { AmbienteSunatId = -1, Nombre = "[Seleccione...]" });

            ListaTipoComprobante = tipoComprobanteBl.ListarTipoComprobante();
            ListaTipoComprobante.Prepend(new TipoComprobanteBe { TipoComprobanteId = -1, Nombre = "[Seleccione...]" });

            cmbAmbienteSunatId.DataSource = ListaAmbienteSunat;
            cmbTipoComprobanteId.DataSource = ListaTipoComprobante;
        }

        private void txtRucEmpresa_TextChanged(object sender, EventArgs e)
        {
            string ruc = txtRucEmpresa.Text.Trim();
            var empresa = empresaBl.ObtenerEmpresaPorRuc(ruc);
            if (empresa == null) return;
            EmpresaId = empresa.EmpresaId;
            empresa.EmpresaConfiguracion = empresaConfiguracionBl.ObtenerEmpresaConfiguracion(empresa.EmpresaId);

            txtRazonSocialEmpresa.Text = empresa.RazonSocial;
            cmbAmbienteSunatId.SelectedValue = empresa.EmpresaConfiguracion.AmbienteSunatId;
            txtRucSOL.Text = empresa.EmpresaConfiguracion.EmpresaAmbienteSunat.RucSOL;
            txtUsuarioSOL.Text = empresa.EmpresaConfiguracion.EmpresaAmbienteSunat.UsuarioSOL;
            txtContraseñaSOL.Text = empresa.EmpresaConfiguracion.EmpresaAmbienteSunat.ClaveSOL;
            txtRutaCertificado.Text = empresa.EmpresaConfiguracion.RutaCertificado.Replace(@"~\", AppDomain.CurrentDomain.BaseDirectory);
            txtContraseñaCertificado.Text = empresa.EmpresaConfiguracion.ClaveCertificado;
        }

        private void btnEditarXML_Click(object sender, EventArgs e)
        {
            EditarXml frmEditarXml = new EditarXml(XmlString);
            DialogResult dr = frmEditarXml.ShowDialog();
            if (dr == DialogResult.OK) XmlString = frmEditarXml.XmlString;
        }

        private void btnVisualizarXML_Click(object sender, EventArgs e)
        {
            string rutaCertificado = txtRutaCertificado.Text.Trim();
            string claveCertificado = txtContraseñaCertificado.Text.Trim();
            string hash = null;

            TipoComprobanteBe tipoComprobante = (TipoComprobanteBe)cmbTipoComprobanteId.SelectedItem;
            string tipoComprobanteCodigo = $"{tipoComprobante.Codigo}";
            string prefijoComprobanteBusqueda = tipoComprobanteCodigo == "01" ? "/tns:Invoice" : tipoComprobanteCodigo == "07" ? "/tns:CreditNote" : "";
            string tnsString = tipoComprobanteCodigo == "01" ? "urn:oasis:names:specification:ubl:schema:xsd:Invoice-2" : tipoComprobanteCodigo == "07" ? "urn:oasis:names:specification:ubl:schema:xsd:CreditNote-2" : "";
            string contenidoXmlFirmado = Generar.RetornarXmlFirmado(prefijoComprobanteBusqueda, tnsString, XmlString, rutaCertificado, claveCertificado, out hash);

            VerXml frmVerXml = new VerXml(contenidoXmlFirmado);
            frmVerXml.ShowDialog();
        }

        private void btnEnviar_Click(object sender, EventArgs e)
        {
            string ruc = txtRucEmpresa.Text.Trim();
            string rutaCertificado = txtRutaCertificado.Text.Trim();
            string claveCertificado = txtContraseñaCertificado.Text.Trim();
            string rucSOL = txtRucSOL.Text.Trim();
            string usuarioSOL = txtUsuarioSOL.Text.Trim();
            string contraseñaSOL = txtContraseñaSOL.Text.Trim();

            var ambienteSunat = (AmbienteSunatBe)cmbAmbienteSunatId.SelectedItem;

            XmlDocument xmlDocument = new XmlDocument();
            xmlDocument.LoadXml(XmlString);

            XmlNamespaceManager xmlNamespaceManager = new XmlNamespaceManager(xmlDocument.NameTable);
            xmlNamespaceManager.AddNamespace("cbc", "urn:oasis:names:specification:ubl:schema:xsd:CommonBasicComponents-2");

            TipoComprobanteBe tipoComprobante = (TipoComprobanteBe)cmbTipoComprobanteId.SelectedItem;

            string serieNumero = $"{txtSerie.Text.Trim()}-{txtNumero.Text.Trim()}";
            string tipoComprobanteCodigo = $"{tipoComprobante.Codigo}";

            string nombreArchivo = $"{ruc}-{tipoComprobanteCodigo}-{serieNumero}";
            string nombreArchivoXml = $"{nombreArchivo}.xml";
            string nombreArchivoZip = $"{nombreArchivo}.zip";
            string nombreArchivoCdr = $"R-{nombreArchivoZip}";

            string hash = null;

            string prefijoComprobanteBusqueda = tipoComprobanteCodigo == "01" ? "/tns:Invoice" : tipoComprobanteCodigo == "07" ? "/tns:CreditNote" : "";
            string tnsString = tipoComprobanteCodigo == "01" ? "urn:oasis:names:specification:ubl:schema:xsd:Invoice-2" : tipoComprobanteCodigo == "07" ? "urn:oasis:names:specification:ubl:schema:xsd:CreditNote-2" : "";
            string contenidoXmlFirmado = Generar.RetornarXmlFirmado(prefijoComprobanteBusqueda, tnsString, XmlString, rutaCertificado, claveCertificado, out hash);
            byte[] contenidoZipBytes = Generar.RetornarXmlComprimido(contenidoXmlFirmado, nombreArchivoXml);

            string codigoCdr = null, descripcionCdr = null;
            EstadoCdr? estadoCdr = null;
            byte[] cdrBytes = null;

            bool seEmitio = false;

            if ((new string[] { "01", "03", "07", "08" }).Contains(tipoComprobanteCodigo)) emitir.Venta(ambienteSunat.ServicioWebUrlVenta, nombreArchivoZip, contenidoZipBytes, rucSOL, usuarioSOL, contraseñaSOL, out cdrBytes, out codigoCdr, out descripcionCdr, out estadoCdr);
            else if ((new string[] { "RA" }).Contains(tipoComprobanteCodigo)) emitir.VentaBaja(ambienteSunat.ServicioWebUrlVenta, nombreArchivoZip, contenidoZipBytes, rucSOL, usuarioSOL, contraseñaSOL, out cdrBytes, out codigoCdr, out descripcionCdr, out estadoCdr);

            if (seEmitio)
            {
                MessageBox.Show($"El comprobante {serieNumero} retornó código {codigoCdr} y el siguiente mensaje \"{descripcionCdr}\"", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Information);
                if(cdrBytes != null)
                {
                    FolderBrowserDialog fbd = new FolderBrowserDialog();
                    DialogResult dr = fbd.ShowDialog();
                    if (dr != DialogResult.OK) return;
                    string rutaArchivoGuardadoCdr = Path.Combine(fbd.SelectedPath, nombreArchivoCdr);
                    if (!Directory.Exists(fbd.SelectedPath)) Directory.CreateDirectory(fbd.SelectedPath);
                    File.WriteAllBytes(rutaArchivoGuardadoCdr, cdrBytes);
                }
            }
            else
                MessageBox.Show($"El comprobante no pudo ser enviado: {descripcionCdr}");
        }

        private void cmbAmbienteSunatId_SelectedIndexChanged(object sender, EventArgs e)
        {
            var ambienteSunat = (AmbienteSunatBe)cmbAmbienteSunatId.SelectedItem;
            var empresaAmbienteSunat = empresaAmbienteSunatBl.ObtenerAmbienteSunat(EmpresaId, ambienteSunat.AmbienteSunatId);
            if (empresaAmbienteSunat == null) return;
            txtRucSOL.Text = empresaAmbienteSunat.RucSOL;
            txtUsuarioSOL.Text = empresaAmbienteSunat.UsuarioSOL;
            txtContraseñaSOL.Text = empresaAmbienteSunat.ClaveSOL;
        }
    }
}
