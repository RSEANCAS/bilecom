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
    public partial class VerXml : Form
    {
        string _XmlString { get; set; }

        public VerXml(string xmlString)
        {
            InitializeComponent();
            _XmlString = xmlString;
        }

        private void VerXml_Load(object sender, EventArgs e)
        {
            wbrXml.DocumentText = _XmlString;
        }
    }
}
