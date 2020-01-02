namespace CompilerWCL.view.Sintactico
{
    partial class Pila_SLR
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
            this.tbl_pila = new System.Windows.Forms.DataGridView();
            this.btnVerPIla = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            ((System.ComponentModel.ISupportInitialize)(this.tbl_pila)).BeginInit();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // tbl_pila
            // 
            this.tbl_pila.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.tbl_pila.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.DisplayedCells;
            this.tbl_pila.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.tbl_pila.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tbl_pila.Location = new System.Drawing.Point(0, 0);
            this.tbl_pila.Name = "tbl_pila";
            this.tbl_pila.Size = new System.Drawing.Size(800, 399);
            this.tbl_pila.TabIndex = 0;
            this.tbl_pila.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.DataGridView1_CellContentClick);
            // 
            // btnVerPIla
            // 
            this.btnVerPIla.Location = new System.Drawing.Point(56, 12);
            this.btnVerPIla.Name = "btnVerPIla";
            this.btnVerPIla.Size = new System.Drawing.Size(75, 23);
            this.btnVerPIla.TabIndex = 1;
            this.btnVerPIla.Text = "Ver Pila";
            this.btnVerPIla.UseVisualStyleBackColor = true;
            this.btnVerPIla.Click += new System.EventHandler(this.BtnVerPIla_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btnVerPIla);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(800, 51);
            this.panel1.TabIndex = 2;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.tbl_pila);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(0, 51);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(800, 399);
            this.panel2.TabIndex = 3;
            // 
            // Pila_SLR
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Name = "Pila_SLR";
            this.Text = "Pila_SLR";
            ((System.ComponentModel.ISupportInitialize)(this.tbl_pila)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView tbl_pila;
        private System.Windows.Forms.Button btnVerPIla;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
    }
}