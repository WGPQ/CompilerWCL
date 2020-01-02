using CompilerWCL.model.Lexico;
using CompilerWCL.model.Sintactico;
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
    public partial class Logo : Form
    {
        /* string rutaAFD = @"C:\Users\WGPROOT\Desktop\xml\automata_wcl.xml";
         string rutaAlfabeto = @"C:\Users\WGPROOT\Desktop\xml\Alfabeto.xml";
         string ruta_error = @"C:\Users\WGPROOT\Desktop\xml\Error.xml";*/
        //string rutaAFD = CargarData("automata_wcl.xml");
        

        public Logo()
        {
            InitializeComponent();
            timer1.Enabled = true;
            //Console.WriteLine("---> " + @CargarRuta("\\..\\..\\automata_wcl.xml"));
            // --- LEXICO ----
            Lexico_tk.getRuta_afd(@CargarRuta("automata_wcl.xml"));
            Lexico_tk.getRuta_alfabeto(@CargarRuta("Alfabeto.xml"));
            Lexico_tk.inicializarError(@CargarRuta("Error.xml"));

            // --- SINTACTICO ---
            Sintactico_srl.inicializarCargarSLR("Gramatica_SLR2.xml");

            Console.WriteLine("FICHEROS CARGADOS");
           
        }
        public string CargarRuta(string archivo)
        {
            string content = "";
            try
            {
                var fs = File.OpenRead(archivo);
                content = fs.Name + "";

                fs.Close();
            }
            catch (FileNotFoundException ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
            return content;

        }
        private void timer1_Tick(object sender, EventArgs e)
        {
            if (progressBar1.Value < 100)
            {
                progressBar1.Value = progressBar1.Value + 5;
                label2.Text = "Carando el sistema al " + progressBar1.Value + " %";
            }
            else
            {
                timer1.Enabled = false;
                this.Hide();
                Login f2 = new Login();
                f2.Show();
               
            }

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }
    }
   
}
