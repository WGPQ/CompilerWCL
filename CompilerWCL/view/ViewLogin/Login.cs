using CompilerWCL.Login_Resources;
using CompilerWCL.view.Principal;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CompilerWCL
{
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
        }

        private void Login_Load(object sender, EventArgs e)
        {

        }

        private void cerra_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void minimi_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void txtuser_MouseEnter(object sender, EventArgs e)
        {
          
        }

        private void txtpassword_Enter(object sender, EventArgs e)
        {
            if (txtpasswordlog.Text == "CONTRASEÑA")
            {
                txtpasswordlog.Text = "";
                txtpasswordlog.ForeColor = Color.LightGray;
                txtpasswordlog.UseSystemPasswordChar = true;
            }
        }

        private void txtuser_Enter(object sender, EventArgs e)
        {
            if (txtuserlog.Text == "USUARIO")
            {
                txtuserlog.Text = "";
                txtuserlog.ForeColor = Color.LightGray;
            }
        }

        private void txtpassword_Leave(object sender, EventArgs e)
        {
            if (txtpasswordlog.Text == "")
            {
                txtpasswordlog.Text = "CONTRASEÑA";
                txtpasswordlog.ForeColor = Color.DimGray;
                txtpasswordlog.UseSystemPasswordChar = false;
            }
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            this.Hide();
            recuperarcuenta f2 = new recuperarcuenta();
            f2.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
           

        }

        private void button2_Click(object sender, EventArgs e)
        {
          
        }

        private void txtuser_Leave(object sender, EventArgs e)
        {
            if (txtuserlog.Text=="")
            {
                txtuserlog.Text = "USUARIO";
                txtuserlog.ForeColor = Color.DimGray;
            }
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            ManagerUser mu = new ManagerUser();
          
            string user = txtuserlog.Text;
            string password = txtpasswordlog.Text;
            persona p = mu.ObtenerUser(user);
            this.Hide();
            //MainMenu fp = new MainMenu(user, p.correo);
            FrmPrincipal fp = new FrmPrincipal(user, p.correo);
            fp.Show();
           /*Object[] obj_res = mu.VerificarUsuario(user, password); // obtengo el objeto {num, mensaje}
            if ((int)obj_res[0] > 0)
            {


                this.Hide();
                loginVerificar l2v = new loginVerificar(user, obj_res);

                l2v.Show(); // abro el login de 2 pasos
                //this.Close();
            }
            else if ((int)obj_res[0] < 0)
            {
                MessageBox.Show(obj_res[1].ToString());
            }*/

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtpassword_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtuser_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnregistrar_Click(object sender, EventArgs e)
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
                MessageBox.Show("Es usuario " + txtuser.Text + " ya esiste!!!");
            }
        }

        private void txtuserlog_DragEnter(object sender, DragEventArgs e)
        {

        }

        private void txtuserlog_Enter(object sender, EventArgs e)
        {
            if (txtuserlog.Text == "USUARIO")
            {
                txtuserlog.Text = "";
                txtuserlog.ForeColor = Color.LightGray;
            }
        }

        private void txtuserlog_Leave(object sender, EventArgs e)
        {
            if (txtuserlog.Text == "")
            {
                txtuserlog.Text = "USUARIO";
                txtuserlog.ForeColor = Color.DimGray;
            }
        }

        private void txtpasswordlog_Enter(object sender, EventArgs e)
        {
            if (txtpasswordlog.Text == "CONTRASEÑA")
            {
                txtpasswordlog.Text = "";
                txtpasswordlog.ForeColor = Color.LightGray;
                txtpasswordlog.UseSystemPasswordChar = true;
                
            }
        }

        private void txtpasswordlog_Leave(object sender, EventArgs e)
        {
            if (txtpasswordlog.Text == "")
            {
                txtpasswordlog.Text = "CONTRASEÑA";
                txtpasswordlog.ForeColor = Color.DimGray;
                txtpasswordlog.UseSystemPasswordChar = false;
            }
        }

        private void txtnombre_Enter(object sender, EventArgs e)
        {
            if (txtnombre.Text == "Nombre")
            {
                txtnombre.Text = "";
                txtnombre.ForeColor = Color.LightGray;
            }
        }

        private void txtapellido_Enter(object sender, EventArgs e)
        {
            if (txtapellido.Text == "Apellido")
            {
                txtapellido.Text = "";
                txtapellido.ForeColor = Color.LightGray;
            }
        }

        private void txtuser_Enter_1(object sender, EventArgs e)
        {
            if (txtuser.Text == "Usuario")
            {
                txtuser.Text = "";
                txtuser.ForeColor = Color.LightGray;
            }
        }

        private void txtcorreo_Enter(object sender, EventArgs e)
        {
            if (txtcorreo.Text == "Correo")
            {
                txtcorreo.Text = "";
                txtcorreo.ForeColor = Color.LightGray;
            }
        }

        private void txtcontraseña_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtcontraseña_Enter(object sender, EventArgs e)
        {
            if (txtcontraseña.Text == "Contraseña")
            {
                txtcontraseña.Text = "";
                txtcontraseña.ForeColor = Color.LightGray;
                txtcontraseña.UseSystemPasswordChar = true;
            }
        }

        private void txtnombre_Leave(object sender, EventArgs e)
        {
            if (txtnombre.Text == "")
            {
                txtnombre.Text = "Nombre";
                txtnombre.ForeColor = Color.DimGray;
            }
        }

        private void txtapellido_Leave(object sender, EventArgs e)
        {
            if (txtapellido.Text == "")
            {
                txtapellido.Text = "Apellido";
                txtapellido.ForeColor = Color.DimGray;
            }
        }

        private void txtuser_Leave_1(object sender, EventArgs e)
        {
            if (txtuser.Text == "")
            {
                txtuser.Text = "Usuario";
                txtuser.ForeColor = Color.DimGray;
            }
        }

        private void txtcorreo_Leave(object sender, EventArgs e)
        {
            if (txtcorreo.Text == "")
            {
                txtcorreo.Text = "Correo";
                txtcorreo.ForeColor = Color.DimGray;
            }
        }

        private void txtcontraseña_Leave(object sender, EventArgs e)
        {
            if (txtcontraseña.Text == "")
            {
                txtcontraseña.Text = "Contraseña";
                txtcontraseña.ForeColor = Color.DimGray;
                txtcontraseña.UseSystemPasswordChar = false;
            }
        }

        private void txtconfcontraseña_Enter(object sender, EventArgs e)
        {
            if (txtconfcontraseña.Text == "Confirmar Contraseña")
            {
                txtconfcontraseña.Text = "";
                txtconfcontraseña.ForeColor = Color.LightGray;
                txtconfcontraseña.UseSystemPasswordChar = true;
            }
        }

        private void txtconfcontraseña_Leave(object sender, EventArgs e)
        {
            if (txtconfcontraseña.Text == "")
            {
                txtconfcontraseña.Text = "Confirmar Contraseña";
                txtconfcontraseña.ForeColor = Color.DimGray;
                txtconfcontraseña.UseSystemPasswordChar = false;
            }
        }

        private void linkLabel1_LinkClicked_1(object sender, LinkLabelLinkClickedEventArgs e)
        {
           // MessageBox.Show(CargarData(a));
        }
        string a = "y.utn";
        public string CargarData(string archivo)
        {
            string content = "";
            try
            {
                var fs = File.OpenRead(archivo);
                //var stream = new StreamReader(fs);
                //string file=fs.Name;
                content = fs.Name + "";

                fs.Close();
            }
            catch (FileNotFoundException ex)
            {
                //MessageBox.Show("Error: " + ex.Message);
            }
            return content;

        }
    }
}
