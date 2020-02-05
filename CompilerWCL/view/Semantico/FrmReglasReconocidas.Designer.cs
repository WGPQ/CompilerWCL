namespace CompilerWCL.view.Semantico
{
    partial class FrmReglasReconocidas
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
            this.tbl_reglasReconocidas = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.tbl_reglasReconocidas)).BeginInit();
            this.SuspendLayout();
            // 
            // tbl_reglasReconocidas
            // 
            this.tbl_reglasReconocidas.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.tbl_reglasReconocidas.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.tbl_reglasReconocidas.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tbl_reglasReconocidas.Location = new System.Drawing.Point(0, 0);
            this.tbl_reglasReconocidas.Name = "tbl_reglasReconocidas";
            this.tbl_reglasReconocidas.Size = new System.Drawing.Size(800, 450);
            this.tbl_reglasReconocidas.TabIndex = 0;
            // 
            // FrmReglasReconocidas
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.tbl_reglasReconocidas);
            this.Name = "FrmReglasReconocidas";
            this.Text = "FrmReglasReconocidas";
            ((System.ComponentModel.ISupportInitialize)(this.tbl_reglasReconocidas)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView tbl_reglasReconocidas;
    }
}