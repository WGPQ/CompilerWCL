namespace CompilerWCL.view.Semantico
{
    partial class FrmCodigoIntermedio
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
            this.tbl_codigoIntermedio = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.tbl_codigoIntermedio)).BeginInit();
            this.SuspendLayout();
            // 
            // tbl_codigoIntermedio
            // 
            this.tbl_codigoIntermedio.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.tbl_codigoIntermedio.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.tbl_codigoIntermedio.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tbl_codigoIntermedio.Location = new System.Drawing.Point(0, 0);
            this.tbl_codigoIntermedio.Name = "tbl_codigoIntermedio";
            this.tbl_codigoIntermedio.Size = new System.Drawing.Size(800, 450);
            this.tbl_codigoIntermedio.TabIndex = 0;
            // 
            // FrmCodigoIntermedio
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.tbl_codigoIntermedio);
            this.Name = "FrmCodigoIntermedio";
            this.Text = "FrmCodigoIntermedio";
            ((System.ComponentModel.ISupportInitialize)(this.tbl_codigoIntermedio)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView tbl_codigoIntermedio;
    }
}