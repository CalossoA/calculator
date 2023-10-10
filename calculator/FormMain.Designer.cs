namespace calculator
{
    partial class FormMain
    {
        /// <summary>
        /// Variabile di progettazione necessaria.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Pulire le risorse in uso.
        /// </summary>
        /// <param name="disposing">ha valore true se le risorse gestite devono essere eliminate, false in caso contrario.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Codice generato da Progettazione Windows Form

        /// <summary>
        /// Metodo necessario per il supporto della finestra di progettazione. Non modificare
        /// il contenuto del metodo con l'editor di codice.
        /// </summary>
        private void InitializeComponent()
        {
            this.lbl_result = new System.Windows.Forms.Label();
            this.panelTop = new System.Windows.Forms.Panel();
            this.lblCrono = new System.Windows.Forms.Label();
            this.panelTop.SuspendLayout();
            this.SuspendLayout();
            // 
            // lbl_result
            // 
            this.lbl_result.Dock = System.Windows.Forms.DockStyle.Top;
            this.lbl_result.Font = new System.Drawing.Font("Segoe UI Semibold", 36F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_result.Location = new System.Drawing.Point(0, 39);
            this.lbl_result.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lbl_result.Name = "lbl_result";
            this.lbl_result.Size = new System.Drawing.Size(433, 85);
            this.lbl_result.TabIndex = 2;
            this.lbl_result.Text = "0";
            this.lbl_result.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.lbl_result.TextChanged += new System.EventHandler(this.lbl_result_TextChanged);
            // 
            // panelTop
            // 
            this.panelTop.Controls.Add(this.lblCrono);
            this.panelTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelTop.Location = new System.Drawing.Point(0, 0);
            this.panelTop.Margin = new System.Windows.Forms.Padding(4);
            this.panelTop.Name = "panelTop";
            this.panelTop.Size = new System.Drawing.Size(433, 39);
            this.panelTop.TabIndex = 3;
            // 
            // lblCrono
            // 
            this.lblCrono.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblCrono.AutoSize = true;
            this.lblCrono.Location = new System.Drawing.Point(247, 0);
            this.lblCrono.Name = "lblCrono";
            this.lblCrono.Size = new System.Drawing.Size(44, 16);
            this.lblCrono.TabIndex = 0;
            this.lblCrono.Text = "label1";
            // 
            // FormMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(433, 603);
            this.Controls.Add(this.lbl_result);
            this.Controls.Add(this.panelTop);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "FormMain";
            this.Text = "FormMain";
            this.Load += new System.EventHandler(this.FormMain_Load);
            this.panelTop.ResumeLayout(false);
            this.panelTop.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label lbl_result;
        private System.Windows.Forms.Panel panelTop;
        private System.Windows.Forms.Label lblCrono;
    }
}

