namespace CompilerWCL
{
    partial class recuperarcuenta
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
            this.btnrecuperarpass = new System.Windows.Forms.Button();
            this.txtcorreo = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // btnrecuperarpass
            // 
            this.btnrecuperarpass.Location = new System.Drawing.Point(302, 156);
            this.btnrecuperarpass.Name = "btnrecuperarpass";
            this.btnrecuperarpass.Size = new System.Drawing.Size(75, 23);
            this.btnrecuperarpass.TabIndex = 0;
            this.btnrecuperarpass.Text = "RECUPERAR";
            this.btnrecuperarpass.UseVisualStyleBackColor = true;
            this.btnrecuperarpass.Click += new System.EventHandler(this.btnrecuperarpass_Click);
            // 
            // txtcorreo
            // 
            this.txtcorreo.Location = new System.Drawing.Point(255, 106);
            this.txtcorreo.Name = "txtcorreo";
            this.txtcorreo.Size = new System.Drawing.Size(200, 20);
            this.txtcorreo.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(160, 109);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(89, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Ingrese su correo";
            // 
            // recuperarcuenta
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(740, 216);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtcorreo);
            this.Controls.Add(this.btnrecuperarpass);
            this.Name = "recuperarcuenta";
            this.Text = "recuperarcuenta";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnrecuperarpass;
        private System.Windows.Forms.TextBox txtcorreo;
        private System.Windows.Forms.Label label1;
    }
}