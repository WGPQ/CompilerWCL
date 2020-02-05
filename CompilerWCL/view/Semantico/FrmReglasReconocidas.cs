using CompilerWCL.herramientas;
using CompilerWCL.model.Semantico;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CompilerWCL.view.Semantico
{
    public partial class FrmReglasReconocidas : Form
    {
        public FrmReglasReconocidas()
        {
            InitializeComponent();
            GenerarTabla.generarTabaReglasReconocidas(tbl_reglasReconocidas, principalSemantico.claseSemantica.listaReglasReconocidas);
        }
    }
}
