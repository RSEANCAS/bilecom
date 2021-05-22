
namespace bilecom.sunat.testenvio
{
    partial class EnvioSunat
    {
        /// <summary>
        /// Variable del diseñador necesaria.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpiar los recursos que se estén usando.
        /// </summary>
        /// <param name="disposing">true si los recursos administrados se deben desechar; false en caso contrario.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código generado por el Diseñador de Windows Forms

        /// <summary>
        /// Método necesario para admitir el Diseñador. No se puede modificar
        /// el contenido de este método con el editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtRazonSocialEmpresa = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtRucEmpresa = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.txtUsuarioSOL = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.txtContraseñaCertificado = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.txtRutaCertificado = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.txtContraseñaSOL = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txtRucSOL = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.cmbAmbienteSunatId = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.btnEditarXML = new System.Windows.Forms.Button();
            this.btnVisualizarXML = new System.Windows.Forms.Button();
            this.btnEnviar = new System.Windows.Forms.Button();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.txtNumero = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.txtSerie = new System.Windows.Forms.TextBox();
            this.label16 = new System.Windows.Forms.Label();
            this.cmbTipoComprobanteId = new System.Windows.Forms.ComboBox();
            this.label17 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.txtRazonSocialEmpresa);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.txtRucEmpresa);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(418, 74);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Datos de la Empresa";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.ForeColor = System.Drawing.Color.Red;
            this.label3.Location = new System.Drawing.Point(6, 55);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(61, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "[Error RUC]";
            // 
            // txtRazonSocialEmpresa
            // 
            this.txtRazonSocialEmpresa.Location = new System.Drawing.Point(93, 32);
            this.txtRazonSocialEmpresa.Name = "txtRazonSocialEmpresa";
            this.txtRazonSocialEmpresa.ReadOnly = true;
            this.txtRazonSocialEmpresa.Size = new System.Drawing.Size(316, 20);
            this.txtRazonSocialEmpresa.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(90, 16);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(70, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Razón Social";
            // 
            // txtRucEmpresa
            // 
            this.txtRucEmpresa.Location = new System.Drawing.Point(9, 32);
            this.txtRucEmpresa.Name = "txtRucEmpresa";
            this.txtRucEmpresa.Size = new System.Drawing.Size(78, 20);
            this.txtRucEmpresa.TabIndex = 1;
            this.txtRucEmpresa.TextChanged += new System.EventHandler(this.txtRucEmpresa_TextChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(30, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "RUC";
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.Controls.Add(this.txtUsuarioSOL);
            this.groupBox2.Controls.Add(this.label10);
            this.groupBox2.Controls.Add(this.label9);
            this.groupBox2.Controls.Add(this.txtContraseñaCertificado);
            this.groupBox2.Controls.Add(this.label7);
            this.groupBox2.Controls.Add(this.txtRutaCertificado);
            this.groupBox2.Controls.Add(this.label8);
            this.groupBox2.Controls.Add(this.txtContraseñaSOL);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Controls.Add(this.txtRucSOL);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.cmbAmbienteSunatId);
            this.groupBox2.Controls.Add(this.label6);
            this.groupBox2.Location = new System.Drawing.Point(12, 92);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(418, 115);
            this.groupBox2.TabIndex = 5;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Configuración Ambiente Sunat";
            // 
            // txtUsuarioSOL
            // 
            this.txtUsuarioSOL.Location = new System.Drawing.Point(220, 32);
            this.txtUsuarioSOL.Name = "txtUsuarioSOL";
            this.txtUsuarioSOL.Size = new System.Drawing.Size(74, 20);
            this.txtUsuarioSOL.TabIndex = 12;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(217, 16);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(67, 13);
            this.label10.TabIndex = 11;
            this.label10.Text = "Usuario SOL";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.ForeColor = System.Drawing.Color.Red;
            this.label9.Location = new System.Drawing.Point(6, 95);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(113, 13);
            this.label9.TabIndex = 10;
            this.label9.Text = "[Error Ambiente Sunat]";
            // 
            // txtContraseñaCertificado
            // 
            this.txtContraseñaCertificado.Location = new System.Drawing.Point(300, 72);
            this.txtContraseñaCertificado.Name = "txtContraseñaCertificado";
            this.txtContraseñaCertificado.Size = new System.Drawing.Size(112, 20);
            this.txtContraseñaCertificado.TabIndex = 9;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(297, 56);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(114, 13);
            this.label7.TabIndex = 8;
            this.label7.Text = "Contraseña Certificado";
            // 
            // txtRutaCertificado
            // 
            this.txtRutaCertificado.Location = new System.Drawing.Point(9, 72);
            this.txtRutaCertificado.Name = "txtRutaCertificado";
            this.txtRutaCertificado.Size = new System.Drawing.Size(285, 20);
            this.txtRutaCertificado.TabIndex = 7;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(6, 56);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(83, 13);
            this.label8.TabIndex = 6;
            this.label8.Text = "Ruta Certificado";
            // 
            // txtContraseñaSOL
            // 
            this.txtContraseñaSOL.Location = new System.Drawing.Point(300, 32);
            this.txtContraseñaSOL.Name = "txtContraseñaSOL";
            this.txtContraseñaSOL.Size = new System.Drawing.Size(112, 20);
            this.txtContraseñaSOL.TabIndex = 5;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(297, 16);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(85, 13);
            this.label5.TabIndex = 4;
            this.label5.Text = "Contraseña SOL";
            // 
            // txtRucSOL
            // 
            this.txtRucSOL.Location = new System.Drawing.Point(136, 32);
            this.txtRucSOL.Name = "txtRucSOL";
            this.txtRucSOL.Size = new System.Drawing.Size(78, 20);
            this.txtRucSOL.TabIndex = 3;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(133, 16);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(54, 13);
            this.label4.TabIndex = 2;
            this.label4.Text = "RUC SOL";
            // 
            // cmbAmbienteSunatId
            // 
            this.cmbAmbienteSunatId.DisplayMember = "Nombre";
            this.cmbAmbienteSunatId.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbAmbienteSunatId.FormattingEnabled = true;
            this.cmbAmbienteSunatId.Location = new System.Drawing.Point(9, 32);
            this.cmbAmbienteSunatId.Name = "cmbAmbienteSunatId";
            this.cmbAmbienteSunatId.Size = new System.Drawing.Size(121, 21);
            this.cmbAmbienteSunatId.TabIndex = 1;
            this.cmbAmbienteSunatId.ValueMember = "AmbienteSunatId";
            this.cmbAmbienteSunatId.SelectedIndexChanged += new System.EventHandler(this.cmbAmbienteSunatId_SelectedIndexChanged);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(6, 16);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(82, 13);
            this.label6.TabIndex = 0;
            this.label6.Text = "Ambiente Sunat";
            // 
            // btnEditarXML
            // 
            this.btnEditarXML.Location = new System.Drawing.Point(12, 295);
            this.btnEditarXML.Name = "btnEditarXML";
            this.btnEditarXML.Size = new System.Drawing.Size(75, 23);
            this.btnEditarXML.TabIndex = 6;
            this.btnEditarXML.Text = "Editar XML";
            this.btnEditarXML.UseVisualStyleBackColor = true;
            this.btnEditarXML.Click += new System.EventHandler(this.btnEditarXML_Click);
            // 
            // btnVisualizarXML
            // 
            this.btnVisualizarXML.Location = new System.Drawing.Point(166, 295);
            this.btnVisualizarXML.Name = "btnVisualizarXML";
            this.btnVisualizarXML.Size = new System.Drawing.Size(111, 23);
            this.btnVisualizarXML.TabIndex = 8;
            this.btnVisualizarXML.Text = "Visualizar XML";
            this.btnVisualizarXML.UseVisualStyleBackColor = true;
            this.btnVisualizarXML.Click += new System.EventHandler(this.btnVisualizarXML_Click);
            // 
            // btnEnviar
            // 
            this.btnEnviar.Location = new System.Drawing.Point(355, 295);
            this.btnEnviar.Name = "btnEnviar";
            this.btnEnviar.Size = new System.Drawing.Size(75, 23);
            this.btnEnviar.TabIndex = 9;
            this.btnEnviar.Text = "Enviar";
            this.btnEnviar.UseVisualStyleBackColor = true;
            this.btnEnviar.Click += new System.EventHandler(this.btnEnviar_Click);
            // 
            // groupBox3
            // 
            this.groupBox3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox3.Controls.Add(this.label12);
            this.groupBox3.Controls.Add(this.txtNumero);
            this.groupBox3.Controls.Add(this.label11);
            this.groupBox3.Controls.Add(this.txtSerie);
            this.groupBox3.Controls.Add(this.label16);
            this.groupBox3.Controls.Add(this.cmbTipoComprobanteId);
            this.groupBox3.Controls.Add(this.label17);
            this.groupBox3.Location = new System.Drawing.Point(12, 213);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(418, 76);
            this.groupBox3.TabIndex = 10;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Datos del comprobante";
            // 
            // txtNumero
            // 
            this.txtNumero.Location = new System.Drawing.Point(220, 32);
            this.txtNumero.Name = "txtNumero";
            this.txtNumero.Size = new System.Drawing.Size(74, 20);
            this.txtNumero.TabIndex = 12;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(217, 16);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(44, 13);
            this.label11.TabIndex = 11;
            this.label11.Text = "Número";
            // 
            // txtSerie
            // 
            this.txtSerie.Location = new System.Drawing.Point(136, 32);
            this.txtSerie.Name = "txtSerie";
            this.txtSerie.Size = new System.Drawing.Size(78, 20);
            this.txtSerie.TabIndex = 3;
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(133, 16);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(31, 13);
            this.label16.TabIndex = 2;
            this.label16.Text = "Serie";
            // 
            // cmbTipoComprobanteId
            // 
            this.cmbTipoComprobanteId.DisplayMember = "Nombre";
            this.cmbTipoComprobanteId.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbTipoComprobanteId.FormattingEnabled = true;
            this.cmbTipoComprobanteId.Location = new System.Drawing.Point(9, 32);
            this.cmbTipoComprobanteId.Name = "cmbTipoComprobanteId";
            this.cmbTipoComprobanteId.Size = new System.Drawing.Size(121, 21);
            this.cmbTipoComprobanteId.TabIndex = 1;
            this.cmbTipoComprobanteId.ValueMember = "AmbienteSunatId";
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Location = new System.Drawing.Point(6, 16);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(94, 13);
            this.label17.TabIndex = 0;
            this.label17.Text = "Tipo Comprobante";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.ForeColor = System.Drawing.Color.Red;
            this.label12.Location = new System.Drawing.Point(6, 56);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(101, 13);
            this.label12.TabIndex = 13;
            this.label12.Text = "[Error Comprobante]";
            // 
            // EnvioSunat
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(442, 330);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.btnEnviar);
            this.Controls.Add(this.btnVisualizarXML);
            this.Controls.Add(this.btnEditarXML);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Name = "EnvioSunat";
            this.Text = "Envío Sunat";
            this.Load += new System.EventHandler(this.EnvioSunat_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtRazonSocialEmpresa;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtRucEmpresa;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox txtContraseñaCertificado;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txtRutaCertificado;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox txtContraseñaSOL;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtRucSOL;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox cmbAmbienteSunatId;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button btnEditarXML;
        private System.Windows.Forms.Button btnVisualizarXML;
        private System.Windows.Forms.Button btnEnviar;
        private System.Windows.Forms.TextBox txtUsuarioSOL;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.TextBox txtNumero;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox txtSerie;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.ComboBox cmbTipoComprobanteId;
        private System.Windows.Forms.Label label17;
    }
}

