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
    public partial class FrmTablaCompacta : Form
    {
        public FrmTablaCompacta()
        {
            InitializeComponent();
            // Tabla Compacta Accion
            GenerarTabla.generarTablaCompacta_valor(tbl_Taccion_valor, Sintactico_srl.tablaCompacta.list_tablaAccionValor);
            GenerarTabla.generarTablaCompacta_prifil(tbl_Taccion_prifil, Sintactico_srl.tablaCompacta.list_tablaAccionPrifil);
            // Tabla Compacta GoTo
            GenerarTabla.generarTablaCompacta_valor(tbl_Tgoto_valor, Sintactico_srl.tablaCompacta.list_tablaGoToValor);
            GenerarTabla.generarTablaCompacta_prifil(tbl_Tgoto_prifil, Sintactico_srl.tablaCompacta.list_tablaGoToPrifil);
        }
    }
}
