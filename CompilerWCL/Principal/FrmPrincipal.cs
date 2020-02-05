using CompilerWCL.view.Lexico;
using CompilerWCL.view.Semantico;
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
        public int indiceResultado = -1;
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
        private Form formactivo3 = null;
        public void abrirformsecundarios(Form hijo)
        {
            if (formactivo2 != null)

                formactivo2.Close();
            formactivo2 = hijo;
            formactivo3 = hijo;

            hijo.TopLevel = false;
            hijo.FormBorderStyle = FormBorderStyle.None;
            hijo.Dock = DockStyle.Fill;
            panelvisualizadorsecundario.Controls.Add(hijo);
            panelvisualizadorsecundario.Tag = hijo;
            hijo.BringToFront();
            hijo.Show();

        }

        public void abrirReglas()
        {
            abrirformsecundarios(new FrmReglasReconocidas());
            panelvisualizadormenu.Visible = false;
            panelvisualizadormenuiconos.Visible = true;
            panelvisualizadorsecundario.Visible = true;
        }

        public void imprimir2(String d)
        {
            Console.WriteLine(d);
        }

        public void ejecutarVentanaResultado()
        {
            try
            {
                Console.WriteLine("1");
                switch (indiceResultado)
                {
                    case 0:
                        abrirformsecundarios(new FrmTDS());
                        break;
                    case 1:
                        abrirformsecundarios(new FrmMovimientos());
                        break;
                    case 2:
                        abrirformsecundarios(new Pila_SLR());
                        break;
                    case 3:
                        abrirformsecundarios(new FrmReglasReconocidas());
                        break;
                    case 4:
                        abrirformsecundarios(new FrmCodigoIntermedio());
                        break;
                    default:
                        return;
                        break;
                }
                panelvisualizadormenu.Visible = false;
                panelvisualizadormenuiconos.Visible = true;
                panelvisualizadorsecundario.Visible = true;
                Console.WriteLine("Correcto");
            }
            catch(Exception e)
            {
                Console.WriteLine("Error");
            }
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
            abrirformsprimarios(new FrmEditor(this));
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
                indiceResultado = 2; // tabla reglas reconocidas
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
                indiceResultado = 1; // tabla token reconocidos
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
                indiceResultado = 0; // tabla simbolos

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
                indiceResultado = 0; // tabla simbolos
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
                indiceResultado = 1; // tabla token reconocidos
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
                indiceResultado = 2; // tabla pila sintactica
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

        private void Button10_Click(object sender, EventArgs e)
        {

        }

        private void PictureBox4_Click(object sender, EventArgs e)
        {
            
                //SI
            try
            {
                abrirformsecundarios(new FrmReglasReconocidas());
                panelvisualizadormenu.Visible = false;
                panelvisualizadormenuiconos.Visible = true;
                panelvisualizadorsecundario.Visible = true;
                indiceResultado = 3; // tabla reglas reconocidas
            }
            catch (Exception ex)
            {
                panelvisualizadorsecundario.Visible = false;
                MessageBox.Show("TDS no contiene datos \n Posiblemente el editoreste vacio!!!");
            }
        }

        private void PictureBox6_Click(object sender, EventArgs e)
        {

            //SI
            try
            {
                abrirformsecundarios(new FrmCodigoIntermedio());
                panelvisualizadormenu.Visible = false;
                panelvisualizadormenuiconos.Visible = true;
                panelvisualizadorsecundario.Visible = true;
                indiceResultado = 4; // tabla cuadruplos
            }
            catch (Exception ex)
            {
                panelvisualizadorsecundario.Visible = false;
                MessageBox.Show("TDS no contiene datos \n Posiblemente el editoreste vacio!!!");
            }
        }
    }
}
