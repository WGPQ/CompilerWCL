using CompilerWCL.herramientas;
using CompilerWCL.model.Sintactico;
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
    public partial class Pila_SLR : Form
    {
        public Pila_SLR()
        {
            InitializeComponent();
        }

        private void BtnVerPIla_Click(object sender, EventArgs e)
        {

            GenerarTabla.generarTablaPilaSRL(tbl_pila, Sintactico_srl.analisisDeEntradas);
        }

        private void DataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
