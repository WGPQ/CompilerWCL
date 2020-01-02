using CompilerWCL.herramientas;
using CompilerWCL.model.Lexico;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CompilerWCL.view.Lexico
{
    public partial class FrmTC : Form
    {
        public FrmTC()
        {
            InitializeComponent();
            cargarTC();
        }
        
        public void cargarTC()
        {
            string ruta = FrmAFD.ruta2;
            //Lexico_tk l = new Lexico_tk();
            //Lexico_tk.getRuta_afd(ruta);
            GenerarTabla.generarTablaCompacta_valor(tbl_valor, Lexico_tk.tablaCompacta_valor);
            GenerarTabla.generarTablaCompacta_prifil(tbl_prifil, Lexico_tk.tablaCompacta_prifil);
        }

        private void tbl_valor_AutoSizeColumnModeChanged(object sender, DataGridViewAutoSizeColumnModeEventArgs e)
        {

        }
    }
}
