using CompilerWCL.model.Lexico;
using CompilerWCL.model.Semantico; // ojo
using CompilerWCL.model.Sintactico;
using CompilerWCL.Principal;
using CompilerWCL.view.Principal;
using CompilerWCL.view.Semantico;
using System;
using System.Collections;
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
    public partial class FrmEditor : Form
    {
        private string ruta = "";
        ArrayList listapestañas = new ArrayList();
        int contrapestaña = 0;
        Panel panel;
        string res = "";
        Form3 f3;
        List<Form3> listaCode;
        List<string> lista = new List<string>();
        int indiceCode = -1;
        FrmPrincipal frmPri;

        public FrmEditor(FrmPrincipal p)
        {
            InitializeComponent();
            // timer1.Interval = 10;
            //timer1.Start();
            frmPri = p;
            listaCode = new List<Form3>();
            Crearnuevapestaña();
            //Form3.r1.Text = Lexico_tk.enviarCodeEditor();
            f3.rich_Editor.Text = Lexico_tk.enviarCodeEditor();
        }

         

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void paneltds_Paint(object sender, PaintEventArgs e)
        {

        }
        

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void rich_Editor_Enter(object sender, EventArgs e)
        {
        }

        private void rich_Editor_Leave(object sender, EventArgs e)
        {
            

        }

        private void rich_Editor_KeyPress(object sender, KeyPressEventArgs e)
        {

        }

        private void rich_Editor_KeyDown(object sender, KeyEventArgs e)
        {

        }

        private void rich_Editor_KeyUp(object sender, KeyEventArgs e)
        {

        }
       
        private void rich_Editor_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
          
        }

        private void rich_Editor_TabIndexChanged(object sender, EventArgs e)
        {

        }

        private void rich_Editor_TabStopChanged(object sender, EventArgs e)
        {

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            
        }
        private void picturenum_Paint(object sender, PaintEventArgs e)
        {
           
        }

        private void rich_Editor_VScroll(object sender, EventArgs e)
        {
           
        }

        private void picturenum_Paint_1(object sender, PaintEventArgs e)
        {
         
        }
       

     

        

       

    

      

        

       

        private void button3_Click_1(object sender, EventArgs e)
        {
           
        }
        public void mostrartr()
        {
            
        }

        private void pictureBox1_Click_1(object sender, EventArgs e)
        {
            
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
        }

        private void Btn_abrir_Click(object sender, EventArgs e)
        {
            
        }

        /**
         * Abro el achivo y lo convierto a texto
         * 
         * @param ruta: la ruta del archivo a leer
         * return : el texto del archivo
         */
        private void abriArchivo_utn(string ruta)
        {
            string lineasCodigo = "";
            string[] lineas = null;
            int numLinea = 0;
            try
            {

                lineas = System.IO.File.ReadAllLines(@ruta); // me lee todas las lineas que existe
                for (int i = 0; i < lineas.Length; i++)
                {
                    lineasCodigo += lineas[i] + "\n";
                    numLinea = i + 1;
                }
                //Form3.r1.Text = lineasCodigo;
                f3.rich_Editor.Text = lineasCodigo;
                f3.AddLineNumbers();
            }
            catch (Exception ex)
            {
                throw new System.ArgumentException("Error: al cargar el archivo", "original");
            }
        }

        public void imprimir(String s)
        {
            Console.WriteLine(s);
        }
        private void Crearnuevapestaña()
        {
            Panel panel = new Panel();
            panel.BackColor = Color.Red;
            panel.Dock = DockStyle.Fill;
            TabPage nuevapestaña = new TabPage("Nuevo archivo " + contrapestaña);
            f3 = new Form3();
            tabControl1.TabPages.Add(nuevapestaña);
            f3.TopLevel = false;
            f3.Parent = nuevapestaña;
            tabControl1.SelectedTab = nuevapestaña;
            f3.Dock = DockStyle.Fill;

            f3.Show();
            //nuevapestaña.Controls.Add(panel);
            // OpenConsola(f3, panel);
            listapestañas.Add(nuevapestaña);
            
            contrapestaña++;

            listaCode.Add(f3);
        }
        
        public void PrintSemantico(int num,string menssaje)
        {
            //ejecuciones por pint
            if (num==1)
            {
                rich_consola.SelectionColor = Color.White;
                rich_consola.SelectedText = Environment.NewLine + menssaje;
            }
            //ejecuciones errores
            if (num==2)
            {
                rich_consola.SelectionColor = Color.Red;
                rich_consola.SelectedText = Environment.NewLine + menssaje;
            }
            //ejecuciones exitosa
            if (num==3)
            {
                rich_consola.SelectionColor = Color.Green;
                rich_consola.SelectedText = Environment.NewLine + menssaje;
            }
            
        }
        
        private void pictureBox5_Click(object sender, EventArgs e)
        {            
            rich_consola.Text = "";
            //string txt_code = Form3.r1.Text;
            //string txt_code = f3.rich_Editor.Text;

            // indice de la ventana seleccionada para ejecutar codigo generado
            indiceCode = tabControl1.SelectedIndex;          
            string txt_code = listaCode[indiceCode].rich_Editor.Text;
            if (!"".Equals(txt_code))
            {
                // ---- Ejecutar programa -----------
                // LEXICO
                Lexico_tk.getIniciarTokensReconocidos(txt_code.Split('\n'));
                //txtl_consola.Text = Lexico_tk.erroresEjecucion;

                // SINTACTICO
                // si el lexico tiene 0 errores puedo seguir continuando con el sintactico
                if (Lexico_tk.claseErroresReconocidos.list_erroresReconocidos.Count == 0)
                {
                    Sintactico_srl.inicializarAnalizadorSRL();
                    
                }

                // SEMANTICO
                if (Lexico_tk.claseErroresReconocidos.list_erroresReconocidos.Count == 0)
                {
                    principalSemantico.iniciarSemamntico(this);
                    principalSemantico.claseSemantica.generar_codigoCuadruplo();
                }

                string res = Lexico_tk.claseErroresReconocidos.imprimir_erroresConsola();
                string[] imprimirconsola = res.Split('\n');
                for (int i = 0; i < imprimirconsola.Length; i++)
                {
                    PrintSemantico(2, imprimirconsola[i]);

                }


                //Semantico_srl s = new Semantico_srl();
                //pruebaSemantico s = new pruebaSemantico();
                //s.generar_codigoCuadruplo();
                // -------------------------------
                //abrirforms(new FrmTDS());
                imprimir("indiceCode ---->  " + indiceCode);
                //frmPri.abrirformsecundarios(new FrmReglasReconocidas());
                //frmPri.abrirReglas();
                frmPri.ejecutarVentanaResultado();


            }
            PrintSemantico(3, "Ejecucion Terminada");
        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            //Abrir explorador de archivos
            OpenFileDialog buscar = new OpenFileDialog
            {
                InitialDirectory = @"C:\",
                Title = "Browse Text Files",

                CheckFileExists = true,
                CheckPathExists = true,

                DefaultExt = "txt",
                Filter = "txt files (*.utn)|*.utn", //solo permite cargar txt y xml
                FilterIndex = 2,
                RestoreDirectory = true,

                ReadOnlyChecked = true,
                ShowReadOnly = true
            };

            try
            {
                buscar.ShowDialog();
                if (!string.IsNullOrEmpty(buscar.FileName))
                {
                    ruta = buscar.FileName;
                }

            }
            catch (Exception ex)
            {

                MessageBox.Show("Error: " + ex.ToString());
            }

            try
            {
                abriArchivo_utn(ruta);
                MessageBox.Show("El fichero fue cargado con exito");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: el fichero no es compatible");
            }
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            Crearnuevapestaña();
        }

        private void TabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
    
}
