namespace CompilerWCL.view.Lexico
{
    partial class FrmTDS
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
            this.tbl_TDS = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.tbl_TDS)).BeginInit();
            this.SuspendLayout();
            // 
            // tbl_TDS
            // 
            this.tbl_TDS.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.tbl_TDS.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tbl_TDS.Location = new System.Drawing.Point(0, 0);
            this.tbl_TDS.Name = "tbl_TDS";
            this.tbl_TDS.Size = new System.Drawing.Size(800, 450);
            this.tbl_TDS.TabIndex = 0;
            // 
            // FrmTDS
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.tbl_TDS);
            this.Name = "FrmTDS";
            this.Text = "FrmTDS";
            ((System.ComponentModel.ISupportInitialize)(this.tbl_TDS)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView tbl_TDS;
    }
}