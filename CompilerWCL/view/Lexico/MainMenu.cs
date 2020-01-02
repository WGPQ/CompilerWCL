using CompilerWCL.view.Lexico;
using CompilerWCL.view.Sintactico;
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

namespace CompilerWCL
{
    public partial class MainMenu : Form
    {
        public MainMenu()
        {
            InitializeComponent();
            
            personalizardiseño();
            paneldrawer.Visible = false;
            lbltitulo.Text = "BIENVENIDO AL COMPILADOR WCL";
            button8.Visible = false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            mostrarsubmenu(panelcargar);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            FrmAFD f = new FrmAFD();
            //if (FrmAFD.ruta2 != null)
            //{
                f.cargarArchivo(FrmAFD.ruta2);
            //}
                
                
                abrirforms(f);
                lbltitulo.Text = "MATRIZ DE TRANSICION";
                ocultarsubmenu();            
        }

        private void button3_Click(object sender, EventArgs e)
        {
            abrirforms(new FrmAlfabeto());
            lbltitulo.Text = "ALFABETO";
            ocultarsubmenu();
        }
        private void personalizardiseño()
        {
            panelanalizar.Visible = false;
            panelcargar.Visible = false;
        }
        private void ocultarsubmenu()
        {
            if (panelanalizar.Visible == true)
                panelanalizar.Visible = false;

            if (panelcargar.Visible == true)
                panelcargar.Visible = false;

        }
        private void mostrarsubmenu(Panel submenu)
        {
            if (submenu.Visible == false)
            {
                ocultarsubmenu();
                submenu.Visible = true;
            }
            else
            {
                submenu.Visible = false;
            }

        }

        private void button6_Click(object sender, EventArgs e)
        {
            FrmEditor f = new FrmEditor();
            //if (FrmEditor.ruta2 != null)
           // {
               // f.cargarArchivo(FrmEditor.ruta2);
           // }
            abrirforms(f);
            lbltitulo.Text = "MOVIMIENTOS";
            ocultarsubmenu();



        }

        private void button7_Click(object sender, EventArgs e)
        {
         
                abrirforms(new FrmTokensReconocidos());
                lbltitulo.Text = "TOKENS RECONOCIDOS";
                ocultarsubmenu();

        }

        private void button8_Click(object sender, EventArgs e)
        {
            lbltitulo.Text = "TABA DE SÍMBOLOS";
            /*if (FrmAFD.ruta2 != null)
            {*/
                abrirforms(new FrmTDS());
                //lbltitulo.Text = "TABLA COMPACTA";
            /*}
            else
            {
                MessageBox.Show("Aun no a cargado la matriz de trancicion!!!!");
            }*/
            ocultarsubmenu();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            mostrarsubmenu(panelanalizar);
        }
        private Form formactivo = null;
        private void abrirforms(Form hijo)
        {
            if (formactivo != null)
            
                formactivo.Close();
                formactivo = hijo;
                hijo.TopLevel = false;
                hijo.FormBorderStyle = FormBorderStyle.None;
                hijo.Dock = DockStyle.Fill;
                panelhijo.Controls.Add(hijo);
                panelhijo.Tag = hijo;
                hijo.BringToFront();
                hijo.Show();
            
        }

        private void button4_Click(object sender, EventArgs e)
        {
            /*if (FrmAFD.ruta2 !=null)
            {*/
                abrirforms(new FrmTC());
                lbltitulo.Text = "TABLA COMPACTA";
            /*}else{
                MessageBox.Show("Aun no a cargado la matriz de trancicion!!!!");
            }*/
            
           
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            panelmenulateral.Visible = false;
            paneldrawer.Visible = true;
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            panelmenulateral.Visible = true;
            paneldrawer.Visible = false;
        }

        private void button9_Click(object sender, EventArgs e)
        {
            abrirforms(new FrmPerfil(lbluser.Text));
            ocultarsubmenu();
            lbltitulo.Text = "MI PERFIL";
        }

        private void button10_Click(object sender, EventArgs e)
        {
            this.Hide();
            Login login = new Login();
            login.Show();
        }

        private void pnltitulo_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button11_Click(object sender, EventArgs e)
        {
            abrirforms(new FrmMatrizcesSintactico());
            lbltitulo.Text = "MATRICES SLR";
            ocultarsubmenu();
        }

        private void Button12_Click(object sender, EventArgs e)
        {
            abrirforms(new Pila_SLR());
            lbltitulo.Text = "PILA SLR";
            ocultarsubmenu();
        }

        private void Button13_Click(object sender, EventArgs e)
        {

            abrirforms(new FrmTablaCompacta());
            lbltitulo.Text = "TABLA COMPACTA";
            ocultarsubmenu();
        }
    }
}
