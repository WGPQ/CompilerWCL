using CompilerWCL.Login_Resources;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CompilerWCL
{
    public partial class recuperarcuenta : Form
    {
        public recuperarcuenta()
        {
            InitializeComponent();
        }

        private void btnrecuperarpass_Click(object sender, EventArgs e)
        {
            enviaremail enviar = new enviaremail();
            enviar.enviarEmail(txtcorreo.Text, "Este correo es para verificar", "4454");
        }
    }
}
