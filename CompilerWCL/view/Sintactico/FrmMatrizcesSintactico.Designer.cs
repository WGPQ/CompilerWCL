namespace CompilerWCL.view.Sintactico
{
    partial class FrmMatrizcesSintactico
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
            this.lblgoto = new System.Windows.Forms.Panel();
            this.button1 = new System.Windows.Forms.Button();
            this.lblaction = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.datagridgoto = new System.Windows.Forms.DataGridView();
            this.panel3 = new System.Windows.Forms.Panel();
            this.datagridaction = new System.Windows.Forms.DataGridView();
            this.panel4 = new System.Windows.Forms.Panel();
            this.lblgoto.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.datagridgoto)).BeginInit();
            this.panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.datagridaction)).BeginInit();
            this.SuspendLayout();
            // 
            // lblgoto
            // 
            this.lblgoto.Controls.Add(this.button1);
            this.lblgoto.Controls.Add(this.lblaction);
            this.lblgoto.Controls.Add(this.label1);
            this.lblgoto.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblgoto.Location = new System.Drawing.Point(0, 0);
            this.lblgoto.Name = "lblgoto";
            this.lblgoto.Size = new System.Drawing.Size(800, 58);
            this.lblgoto.TabIndex = 0;
            this.lblgoto.Paint += new System.Windows.Forms.PaintEventHandler(this.Lblgoto_Paint);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(26, 18);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(117, 23);
            this.button1.TabIndex = 2;
            this.button1.Text = "CARGAR MATRIZ";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // lblaction
            // 
            this.lblaction.AutoSize = true;
            this.lblaction.Location = new System.Drawing.Point(191, 23);
            this.lblaction.Name = "lblaction";
            this.lblaction.Size = new System.Drawing.Size(47, 13);
            this.lblaction.TabIndex = 1;
            this.lblaction.Text = "ACTION";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(536, 23);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "GO TO";
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.datagridgoto);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel2.Location = new System.Drawing.Point(419, 58);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(381, 392);
            this.panel2.TabIndex = 1;
            // 
            // datagridgoto
            // 
            this.datagridgoto.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.datagridgoto.Dock = System.Windows.Forms.DockStyle.Fill;
            this.datagridgoto.Location = new System.Drawing.Point(0, 0);
            this.datagridgoto.Name = "datagridgoto";
            this.datagridgoto.Size = new System.Drawing.Size(381, 392);
            this.datagridgoto.TabIndex = 0;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.datagridaction);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel3.Location = new System.Drawing.Point(0, 58);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(378, 392);
            this.panel3.TabIndex = 2;
            // 
            // datagridaction
            // 
            this.datagridaction.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.datagridaction.Dock = System.Windows.Forms.DockStyle.Fill;
            this.datagridaction.Location = new System.Drawing.Point(0, 0);
            this.datagridaction.Name = "datagridaction";
            this.datagridaction.Size = new System.Drawing.Size(378, 392);
            this.datagridaction.TabIndex = 1;
            // 
            // panel4
            // 
            this.panel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel4.Location = new System.Drawing.Point(378, 58);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(41, 392);
            this.panel4.TabIndex = 2;
            // 
            // FrmMatrizcesSintactico
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.panel4);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.lblgoto);
            this.Name = "FrmMatrizcesSintactico";
            this.Text = "FrmMatrizcesSintactico";
            this.lblgoto.ResumeLayout(false);
            this.lblgoto.PerformLayout();
            this.panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.datagridgoto)).EndInit();
            this.panel3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.datagridaction)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel lblgoto;
        private System.Windows.Forms.Label lblaction;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.DataGridView datagridgoto;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.DataGridView datagridaction;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Button button1;
    }
}