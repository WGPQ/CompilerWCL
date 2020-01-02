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
    public partial class FrmTDS : Form
    {
        public FrmTDS()
        {
            InitializeComponent();
            cargarTDS();
        }

        public void cargarTDS()
        {
            GenerarTabla.generarTableTDS(tbl_TDS, Lexico_tk.listDTSReconocidos);
        }

    }
}
