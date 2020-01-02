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
    public partial class loginVerificar : Form
    {
        private int numPing;
        private string email;
        private string user;
        ManagerUser c = new ManagerUser();
        public loginVerificar(string user,Object[] obj)
        {
            InitializeComponent();
            this.numPing = (int)obj[0];
            this.email = obj[1].ToString();
            this.user = user;
            lbl_envio_email.Text = "Se envio el ping al Email: " + email;
        }

        private void btn_ok_Click(object sender, EventArgs e)
        {
            /*FormWCL fp = new FormWCL();
            fp.Show();
            this.Close();*/
            int num = int.Parse(txt_num.Text);
            if (num == numPing)
            {
                this.Hide();
                FrmPrincipal fp = new FrmPrincipal(user, email);
                fp.Show();
                
            }
            else
            {
                MessageBox.Show("ping incorrecto");
            }
        }

        private void btn_otroping_Click(object sender, EventArgs e)
        {
            /*FormWCL fp = new FormWCL();
           fp.Show();
           this.Close();*/
            int num = int.Parse(txt_num.Text);
            if (num == numPing)
            {
                this.Hide();
                FrmPrincipal fp = new FrmPrincipal(user, email);
                fp.Show();
                
            }
            else
            {
                MessageBox.Show("ping incorrecto");
            }
        }

        private void cerra_Click(object sender, EventArgs e)
        {

        }

        private void minimi_Click(object sender, EventArgs e)
        {

        }
    }
}
