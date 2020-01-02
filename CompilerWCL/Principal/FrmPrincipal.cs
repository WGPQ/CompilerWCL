using CompilerWCL.view.Lexico;
using CompilerWCL.view.Sintactico;
using CompilerWCL.view.ViewInicio;
using CompilerWCL.view.ViewLogin;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CompilerWCL.view.Principal
{
    public partial class FrmPrincipal : Form
    {
        public FrmPrincipal(string user, string email)
        {
            InitializeComponent();
            lbluser.Text = user;
            lblcorreo.Text = email;
            panelvisualizadorsecundario.Visible = false;
            panelvisualizadormenu.Visible = false;
            panelvisualizadormenuiconos.Visible = false;
            abrirformsprimarios(new FrmInicio());
            
           

        }

      
        private Form formactivo = null;
        private void abrirformsprimarios(Form hijo)
        {
            if (formactivo != null)

                formactivo.Close();
            formactivo = hijo;
            hijo.TopLevel = false;
            hijo.FormBorderStyle = FormBorderStyle.None;
            hijo.Dock = DockStyle.Fill;
            panelvisualizadorprincipal.Controls.Add(hijo);
           panelvisualizadorprincipal.Tag = hijo;
            hijo.BringToFront();
            hijo.Show();

        }


        private Form formactivo2 = null;
        private void abrirformsecundarios(Form hijo)
        {
            if (formactivo2 != null)

                formactivo2.Close();
            formactivo2 = hijo;
            hijo.TopLevel = false;
            hijo.FormBorderStyle = FormBorderStyle.None;
            hijo.Dock = DockStyle.Fill;
            panelvisualizadorsecundario.Controls.Add(hijo);
            panelvisualizadorsecundario.Tag = hijo;
            hijo.BringToFront();
            hijo.Show();

        }


        private void panel5_Paint(object sender, PaintEventArgs e)
        {

        }

       

        private void button6_Click(object sender, EventArgs e)
        {

            
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            /*panelvisualizador3.Visible = true;
            panelvisualizador4.Visible = false;
            panelvisualizador2.Visible = false;*/
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            panelvisualizadormenuiconos.Visible = false;
            panelvisualizadormenuiconos.Visible = true;
        }

        private void pictureBox2_Click_1(object sender, EventArgs e)
        {
            panelvisualizadormenuiconos.Visible = false;
            panelvisualizadormenuiconos.Visible = true;
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
           
        }

        private void button5_Click(object sender, EventArgs e)
        {
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            
        }

        private void button3_Click(object sender, EventArgs e)
        {
            abrirformsprimarios(new FrmMatrizcesSintactico());
            cerrarpaneles();
        }

        private void btnInicio_Click(object sender, EventArgs e)
        {
            //SI
            abrirformsprimarios(new FrmInicio());
            cerrarpaneles();
        }


      
        private void button6_Click_1(object sender, EventArgs e)
        {
           
        }

      
        public void cerrarpaneles()
        {
            panelvisualizadormenuiconos.Visible = false;
            panelvisualizadorsecundario.Visible = false;
            panelvisualizadormenuiconos.Visible = false;
        }

       

       

     


       
        
        private void btnconsole_Click_1(object sender, EventArgs e)
        {
            //SI
            abrirformsprimarios(new FrmEditor());
            panelvisualizadormenuiconos.Visible = true;
        }

        private void btnlexico_Click(object sender, EventArgs e)
        {
            //SI
            abrirformsprimarios(new MainMenu());
            cerrarpaneles();
        }

        private void btnsintactico_Click(object sender, EventArgs e)
        {
            //SI
            abrirformsprimarios(new MainSintactico());
        }

      
        private void btnperfil_Click(object sender, EventArgs e)
        {
            //SI
            abrirformsprimarios(new FrmPerfil(lbluser.Text));
        }

      

       

     

        private void pictureBox3_Click_2(object sender, EventArgs e)
        {
            //SI
            panelvisualizadormenu.Visible = false;
            panelvisualizadormenuiconos.Visible = true;
        }

        private void button5_Click_2(object sender, EventArgs e)
        {
            //SI
            try
            {
                abrirformsecundarios(new Pila_SLR());
                panelvisualizadormenu.Visible = false;
                panelvisualizadormenuiconos.Visible = true;
                panelvisualizadorsecundario.Visible = true;
            }
            catch (Exception ex)
            {
                panelvisualizadorsecundario.Visible = false;
                MessageBox.Show("TDS no contiene datos \n Posiblemente el editoreste vacio!!!");
            }
        }

        private void button6_Click_2(object sender, EventArgs e)
        {
            //SI
            try
            {
               abrirformsecundarios(new FrmMovimientos());
                panelvisualizadormenu.Visible = false;
                panelvisualizadormenuiconos.Visible = true;
                panelvisualizadorsecundario.Visible = true;
            }
            catch (Exception ex)
            {
                panelvisualizadorsecundario.Visible = false;
                MessageBox.Show("TDS no contiene datos \n Posiblemente el editoreste vacio!!!");
            }
        }

        private void button13_Click(object sender, EventArgs e)
        {
            //SI
            try
            {
                abrirformsecundarios(new FrmTDS());
                
               panelvisualizadormenuiconos.Visible = true;
                panelvisualizadorsecundario.Visible = true;
                panelvisualizadormenu.Visible = false;

            }
            catch (Exception ex)
            {
                panelvisualizadorsecundario.Visible = false;
                MessageBox.Show("TDS no contiene datos \n Posiblemente el editoreste vacio!!!");
            }
        }

        private void pictureBox2_Click_3(object sender, EventArgs e)
        {
            //SI
            panelvisualizadormenu.Visible = true;
            panelvisualizadormenuiconos.Visible = false;
            panelvisualizadorsecundario.Visible = false;
        }

        private void button4_Click_1(object sender, EventArgs e)
        {

        }

        private void pictureBox7_Click(object sender, EventArgs e)
        {
            //SI
            try
            {
                abrirformsecundarios(new FrmTDS());

                panelvisualizadormenuiconos.Visible = true;
                panelvisualizadorsecundario.Visible = true;
                panelvisualizadormenu.Visible = false;

            }
            catch (Exception ex)
            {
                panelvisualizadorsecundario.Visible = false;
                MessageBox.Show("TDS no contiene datos \n Posiblemente el editoreste vacio!!!");
            }
        }

        private void pictureBox8_Click(object sender, EventArgs e)
        {
            //SI
            try
            {
                abrirformsecundarios(new FrmMovimientos());
                panelvisualizadormenu.Visible = false;
                panelvisualizadormenuiconos.Visible = true;
                panelvisualizadorsecundario.Visible = true;
            }
            catch (Exception ex)
            {
                panelvisualizadorsecundario.Visible = false;
                MessageBox.Show("TDS no contiene datos \n Posiblemente el editoreste vacio!!!");
            }
        }

        private void pictureBox9_Click(object sender, EventArgs e)
        {
            //SI
            try
            {
                abrirformsecundarios(new Pila_SLR());
                panelvisualizadormenu.Visible = false;
                panelvisualizadormenuiconos.Visible = true;
                panelvisualizadorsecundario.Visible = true;
            }
            catch (Exception ex)
            {
                panelvisualizadorsecundario.Visible = false;
                MessageBox.Show("TDS no contiene datos \n Posiblemente el editoreste vacio!!!");
            }
        }

        
        private void button8_Click(object sender, EventArgs e)
        {
            panelvisualizadormenuiconos.Visible = true;
            panelvisualizadormenuiconos.Dock = DockStyle.Right;
        }


        private void pictureBox1_Click_2(object sender, EventArgs e)
        {
            panelvisualizadorsecundario.Visible = false;

        }
    }
}
