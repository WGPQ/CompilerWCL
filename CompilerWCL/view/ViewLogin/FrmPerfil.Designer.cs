﻿namespace CompilerWCL.view.ViewLogin
{
    partial class FrmPerfil
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
            this.lblperfil = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // lblperfil
            // 
            this.lblperfil.AutoSize = true;
            this.lblperfil.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblperfil.Location = new System.Drawing.Point(12, 9);
            this.lblperfil.Name = "lblperfil";
            this.lblperfil.Size = new System.Drawing.Size(58, 25);
            this.lblperfil.TabIndex = 0;
            this.lblperfil.Text = "user";
            this.lblperfil.Click += new System.EventHandler(this.label1_Click);
            // 
            // FrmPerfil
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.lblperfil);
            this.Name = "FrmPerfil";
            this.Text = "FrmPerfil";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblperfil;
    }
}