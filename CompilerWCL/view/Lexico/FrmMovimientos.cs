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
    public partial class FrmMovimientos : Form
    {
        public FrmMovimientos()
        {
            InitializeComponent();
            cargarArchivo();
        }

        public void cargarArchivo()
        {
            GenerarTabla.generarTablaAlfabeto(tbl_tokenReconocidos, Lexico_tk.listTokensReconocidos);

        }
    }
}
