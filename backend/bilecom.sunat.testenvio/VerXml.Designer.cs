
namespace bilecom.sunat.testenvio
{
    partial class VerXml
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.wbrXml = new System.Windows.Forms.WebBrowser();
            this.SuspendLayout();
            // 
            // wbrXml
            // 
            this.wbrXml.Dock = System.Windows.Forms.DockStyle.Fill;
            this.wbrXml.Location = new System.Drawing.Point(0, 0);
            this.wbrXml.MinimumSize = new System.Drawing.Size(20, 20);
            this.wbrXml.Name = "wbrXml";
            this.wbrXml.Size = new System.Drawing.Size(800, 450);
            this.wbrXml.TabIndex = 0;
            // 
            // VerXml
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.wbrXml);
            this.Name = "VerXml";
            this.Text = "VerXml";
            this.Load += new System.EventHandler(this.VerXml_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.WebBrowser wbrXml;
    }
}