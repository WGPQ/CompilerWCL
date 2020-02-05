namespace CompilerWCL.Principal
{
    partial class Form3
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
            this.LineNumberTextBox = new System.Windows.Forms.RichTextBox();
            this.rich_Editor = new System.Windows.Forms.RichTextBox();
            this.SuspendLayout();
            // 
            // LineNumberTextBox
            // 
            this.LineNumberTextBox.Dock = System.Windows.Forms.DockStyle.Left;
            this.LineNumberTextBox.Location = new System.Drawing.Point(0, 0);
            this.LineNumberTextBox.Name = "LineNumberTextBox";
            this.LineNumberTextBox.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.None;
            this.LineNumberTextBox.Size = new System.Drawing.Size(32, 450);
            this.LineNumberTextBox.TabIndex = 0;
            this.LineNumberTextBox.Text = "";
            this.LineNumberTextBox.MouseDown += new System.Windows.Forms.MouseEventHandler(this.LineNumberTextBox_MouseDown);
            // 
            // rich_Editor
            // 
            this.rich_Editor.AcceptsTab = true;
            this.rich_Editor.Dock = System.Windows.Forms.DockStyle.Left;
            this.rich_Editor.Location = new System.Drawing.Point(32, 0);
            this.rich_Editor.Name = "rich_Editor";
            this.rich_Editor.Size = new System.Drawing.Size(767, 450);
            this.rich_Editor.TabIndex = 1;
            this.rich_Editor.Text = "";
            this.rich_Editor.SelectionChanged += new System.EventHandler(this.rich_Editor_SelectionChanged);
            this.rich_Editor.VScroll += new System.EventHandler(this.rich_Editor_VScroll);
            this.rich_Editor.FontChanged += new System.EventHandler(this.rich_Editor_FontChanged);
            this.rich_Editor.TextChanged += new System.EventHandler(this.rich_Editor_TextChanged);
            // 
            // Form3
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.rich_Editor);
            this.Controls.Add(this.LineNumberTextBox);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "Form3";
            this.Text = "Form3";
            this.Load += new System.EventHandler(this.Form3_Load);
            this.ResumeLayout(false);

        }


        #endregion

        public System.Windows.Forms.RichTextBox LineNumberTextBox;
        public System.Windows.Forms.RichTextBox rich_Editor;
    }
}