using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CompilerWCL.view.Sintactico
{
    public partial class MainSintactico : Form
    {
        public MainSintactico()
        {
            InitializeComponent();
        }

        private void button11_Click(object sender, EventArgs e)
        {
            abrirforms(new FrmMatrizcesSintactico());
            lbltitulo.Text = "MATRICES SLR";
           // ocultarsubmenu();
        }

        private void button12_Click(object sender, EventArgs e)
        {
            abrirforms(new Pila_SLR());
            lbltitulo.Text = "PILA SLR";
            //ocultarsubmenu();
        }

        private void button13_Click(object sender, EventArgs e)
        {
            abrirforms(new FrmTablaCompacta());
            //lbltitulo.Text = "TABLA COMPACTA";
            //ocultarsubmenu();
        }

        private Form formactivo = null;
        private void abrirforms(Form hijo)
        {
            if (formactivo != null)

                formactivo.Close();
            formactivo = hijo;
            hijo.TopLevel = false;
            hijo.FormBorderStyle = FormBorderStyle.None;
            hijo.Dock = DockStyle.Fill;
            panelhijo.Controls.Add(hijo);
            panelhijo.Tag = hijo;
            hijo.BringToFront();
            hijo.Show();

        }
    }
}
