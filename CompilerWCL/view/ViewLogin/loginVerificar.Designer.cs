namespace CompilerWCL
{
    partial class loginVerificar
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
            this.btn_otroping = new System.Windows.Forms.Button();
            this.btn_ok = new System.Windows.Forms.Button();
            this.txt_num = new System.Windows.Forms.TextBox();
            this.lbl_envio_email = new System.Windows.Forms.Label();
            this.minimi = new System.Windows.Forms.PictureBox();
            this.cerra = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.minimi)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cerra)).BeginInit();
            this.SuspendLayout();
            // 
            // btn_otroping
            // 
            this.btn_otroping.Location = new System.Drawing.Point(330, 137);
            this.btn_otroping.Name = "btn_otroping";
            this.btn_otroping.Size = new System.Drawing.Size(107, 23);
            this.btn_otroping.TabIndex = 7;
            this.btn_otroping.Text = "Enviar nuevo ping";
            this.btn_otroping.UseVisualStyleBackColor = true;
            this.btn_otroping.Click += new System.EventHandler(this.btn_otroping_Click);
            // 
            // btn_ok
            // 
            this.btn_ok.Location = new System.Drawing.Point(246, 137);
            this.btn_ok.Name = "btn_ok";
            this.btn_ok.Size = new System.Drawing.Size(75, 23);
            this.btn_ok.TabIndex = 6;
            this.btn_ok.Text = "OK";
            this.btn_ok.UseVisualStyleBackColor = true;
            this.btn_ok.Click += new System.EventHandler(this.btn_ok_Click);
            // 
            // txt_num
            // 
            this.txt_num.Location = new System.Drawing.Point(246, 98);
            this.txt_num.Name = "txt_num";
            this.txt_num.Size = new System.Drawing.Size(123, 20);
            this.txt_num.TabIndex = 5;
            // 
            // lbl_envio_email
            // 
            this.lbl_envio_email.AutoSize = true;
            this.lbl_envio_email.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_envio_email.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.lbl_envio_email.Location = new System.Drawing.Point(182, 64);
            this.lbl_envio_email.Name = "lbl_envio_email";
            this.lbl_envio_email.Size = new System.Drawing.Size(97, 18);
            this.lbl_envio_email.TabIndex = 4;
            this.lbl_envio_email.Text = "envio_email";
            // 
            // minimi
            // 
            this.minimi.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.minimi.Cursor = System.Windows.Forms.Cursors.Hand;
            this.minimi.Image = global::CompilerWCL.Properties.Resources.minimazar;
            this.minimi.Location = new System.Drawing.Point(530, 3);
            this.minimi.Name = "minimi";
            this.minimi.Size = new System.Drawing.Size(27, 25);
            this.minimi.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.minimi.TabIndex = 9;
            this.minimi.TabStop = false;
            this.minimi.Click += new System.EventHandler(this.minimi_Click);
            // 
            // cerra
            // 
            this.cerra.Cursor = System.Windows.Forms.Cursors.Hand;
            this.cerra.Image = global::CompilerWCL.Properties.Resources.cerrar;
            this.cerra.Location = new System.Drawing.Point(581, 3);
            this.cerra.Name = "cerra";
            this.cerra.Size = new System.Drawing.Size(25, 25);
            this.cerra.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.cerra.TabIndex = 8;
            this.cerra.TabStop = false;
            this.cerra.Click += new System.EventHandler(this.cerra_Click);
            // 
            // loginVerificar
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(15)))), ((int)(((byte)(15)))));
            this.ClientSize = new System.Drawing.Size(618, 225);
            this.Controls.Add(this.minimi);
            this.Controls.Add(this.cerra);
            this.Controls.Add(this.btn_otroping);
            this.Controls.Add(this.btn_ok);
            this.Controls.Add(this.txt_num);
            this.Controls.Add(this.lbl_envio_email);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "loginVerificar";
            this.Opacity = 0.9D;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "loginVerificar";
            ((System.ComponentModel.ISupportInitialize)(this.minimi)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cerra)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btn_otroping;
        private System.Windows.Forms.Button btn_ok;
        private System.Windows.Forms.TextBox txt_num;
        private System.Windows.Forms.Label lbl_envio_email;
        private System.Windows.Forms.PictureBox minimi;
        private System.Windows.Forms.PictureBox cerra;
    }
}