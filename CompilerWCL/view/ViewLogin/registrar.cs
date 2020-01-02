using CompilerWCL.Login_Resources;
using CompilerWCL.view.Principal;
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
    public partial class registrar : Form
    {
        public registrar()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            char genero;
            if (comgenero.Text.Equals("Masculino"))
            {
                genero = 'M';
            }
            else
            {
                if (comgenero.Text.Equals("Femenino"))
                {
                    genero = 'F';
                }
                else
                {
                    genero = 'O';
                }
            }
            ManagerUser mu = new ManagerUser();
            if (mu.Usuariorepetido(txtuser.Text))
            {
                usuario user = new usuario(txtnombre.Text, txtapellido.Text,
                                genero, txtuser.Text,
                                txtcorreo.Text, txtcontraseña.Text, txtconfcontraseña.Text);
                MessageBox.Show("Usuario creado exitosamente");
                this.Hide();
                FrmPrincipal f2 = new FrmPrincipal(txtuser.Text, txtcorreo.Text);
                f2.Show();
            }
            else
            {
                MessageBox.Show("Es usuario "+txtuser.Text+" ya esiste!!!"); 
            }
            
        }

        private void txtpassword_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtuser_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            ManagerUser manager = new ManagerUser();
            MessageBox.Show(manager.CargarData());
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
