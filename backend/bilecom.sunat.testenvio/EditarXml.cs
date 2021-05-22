using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace bilecom.sunat.testenvio
{
    public partial class EditarXml : Form
    {
        string _XmlString { get; set; }
        public string XmlString
        {
            get
            {
                return _XmlString;
            }
        }

        public EditarXml(string xmlString)
        {
            InitializeComponent();
            _XmlString = xmlString;
        }

        private void EditarXml_Load(object sender, EventArgs e)
        {
            txtContenido.Text = _XmlString;
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            _XmlString = txtContenido.Text.Trim();
            DialogResult = DialogResult.OK;
            Close();
        }
    }
}
