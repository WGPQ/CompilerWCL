namespace CompilerWCL
{
    partial class registrar
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
            this.btnregistrar = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.txtapellido = new System.Windows.Forms.TextBox();
            this.txtnombre = new System.Windows.Forms.TextBox();
            this.txtcontraseña = new System.Windows.Forms.TextBox();
            this.txtcorreo = new System.Windows.Forms.TextBox();
            this.txtuser = new System.Windows.Forms.TextBox();
            this.txtconfcontraseña = new System.Windows.Forms.TextBox();
            this.comgenero = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // btnregistrar
            // 
            this.btnregistrar.BackColor = System.Drawing.Color.Black;
            this.btnregistrar.FlatAppearance.BorderSize = 0;
            this.btnregistrar.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Blue;
            this.btnregistrar.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Blue;
            this.btnregistrar.ForeColor = System.Drawing.Color.White;
            this.btnregistrar.Location = new System.Drawing.Point(195, 459);
            this.btnregistrar.Name = "btnregistrar";
            this.btnregistrar.Size = new System.Drawing.Size(359, 36);
            this.btnregistrar.TabIndex = 8;
            this.btnregistrar.Text = "REISTRAR";
            this.btnregistrar.UseVisualStyleBackColor = false;
            this.btnregistrar.Click += new System.EventHandler(this.button1_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Century Gothic", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(314, 42);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(100, 33);
            this.label1.TabIndex = 7;
            this.label1.Text = "LOGIN";
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // txtapellido
            // 
            this.txtapellido.BackColor = System.Drawing.Color.Black;
            this.txtapellido.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtapellido.ForeColor = System.Drawing.Color.DimGray;
            this.txtapellido.Location = new System.Drawing.Point(206, 149);
            this.txtapellido.Name = "txtapellido";
            this.txtapellido.Size = new System.Drawing.Size(348, 27);
            this.txtapellido.TabIndex = 6;
            this.txtapellido.Text = "Apellido";
            this.txtapellido.TextChanged += new System.EventHandler(this.txtpassword_TextChanged);
            // 
            // txtnombre
            // 
            this.txtnombre.BackColor = System.Drawing.SystemColors.InactiveCaptionText;
            this.txtnombre.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtnombre.ForeColor = System.Drawing.Color.DimGray;
            this.txtnombre.Location = new System.Drawing.Point(206, 97);
            this.txtnombre.Name = "txtnombre";
            this.txtnombre.Size = new System.Drawing.Size(348, 27);
            this.txtnombre.TabIndex = 5;
            this.txtnombre.Text = "Nombre";
            this.txtnombre.TextChanged += new System.EventHandler(this.txtuser_TextChanged);
            // 
            // txtcontraseña
            // 
            this.txtcontraseña.BackColor = System.Drawing.Color.Black;
            this.txtcontraseña.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtcontraseña.ForeColor = System.Drawing.Color.DimGray;
            this.txtcontraseña.Location = new System.Drawing.Point(206, 344);
            this.txtcontraseña.Name = "txtcontraseña";
            this.txtcontraseña.Size = new System.Drawing.Size(348, 27);
            this.txtcontraseña.TabIndex = 12;
            this.txtcontraseña.Text = "Contraseña";
            // 
            // txtcorreo
            // 
            this.txtcorreo.BackColor = System.Drawing.Color.Black;
            this.txtcorreo.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtcorreo.ForeColor = System.Drawing.Color.DimGray;
            this.txtcorreo.Location = new System.Drawing.Point(206, 296);
            this.txtcorreo.Name = "txtcorreo";
            this.txtcorreo.Size = new System.Drawing.Size(348, 27);
            this.txtcorreo.TabIndex = 11;
            this.txtcorreo.Text = "Correo";
            this.txtcorreo.TextChanged += new System.EventHandler(this.textBox2_TextChanged);
            // 
            // txtuser
            // 
            this.txtuser.BackColor = System.Drawing.SystemColors.InactiveCaptionText;
            this.txtuser.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtuser.ForeColor = System.Drawing.Color.DimGray;
            this.txtuser.Location = new System.Drawing.Point(206, 244);
            this.txtuser.Name = "txtuser";
            this.txtuser.Size = new System.Drawing.Size(348, 27);
            this.txtuser.TabIndex = 10;
            this.txtuser.Text = "Usuario";
            // 
            // txtconfcontraseña
            // 
            this.txtconfcontraseña.BackColor = System.Drawing.Color.Black;
            this.txtconfcontraseña.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtconfcontraseña.ForeColor = System.Drawing.Color.DimGray;
            this.txtconfcontraseña.Location = new System.Drawing.Point(206, 396);
            this.txtconfcontraseña.Name = "txtconfcontraseña";
            this.txtconfcontraseña.Size = new System.Drawing.Size(348, 27);
            this.txtconfcontraseña.TabIndex = 13;
            this.txtconfcontraseña.Text = "Confirmar Contraseña";
            // 
            // comgenero
            // 
            this.comgenero.BackColor = System.Drawing.Color.Black;
            this.comgenero.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.comgenero.ForeColor = System.Drawing.Color.DimGray;
            this.comgenero.FormattingEnabled = true;
            this.comgenero.Items.AddRange(new object[] {
            "Masculino",
            "Femenino",
            "Otros"});
            this.comgenero.Location = new System.Drawing.Point(206, 199);
            this.comgenero.Name = "comgenero";
            this.comgenero.Size = new System.Drawing.Size(348, 29);
            this.comgenero.TabIndex = 14;
            this.comgenero.Text = "Elija su genero";
            // 
            // registrar
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(796, 498);
            this.Controls.Add(this.comgenero);
            this.Controls.Add(this.txtconfcontraseña);
            this.Controls.Add(this.txtcontraseña);
            this.Controls.Add(this.txtcorreo);
            this.Controls.Add(this.txtuser);
            this.Controls.Add(this.btnregistrar);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtapellido);
            this.Controls.Add(this.txtnombre);
            this.Name = "registrar";
            this.Text = "registrar";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnregistrar;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtapellido;
        private System.Windows.Forms.TextBox txtnombre;
        private System.Windows.Forms.TextBox txtcontraseña;
        private System.Windows.Forms.TextBox txtcorreo;
        private System.Windows.Forms.TextBox txtuser;
        private System.Windows.Forms.TextBox txtconfcontraseña;
        private System.Windows.Forms.ComboBox comgenero;
    }
}