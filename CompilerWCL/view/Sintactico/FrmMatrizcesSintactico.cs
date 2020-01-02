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
    public partial class FrmMatrizcesSintactico : Form
    {
        private string ruta = "";
        public FrmMatrizcesSintactico()
        {
            InitializeComponent();

            GenerarTabla.generarTableMatrizTransicionSRL(datagridaction, Sintactico_srl.listaX, Sintactico_srl.matrizAccion.GetLength(0));
            GenerarTabla.imprimirTablaMatrizSRL(datagridaction, Sintactico_srl.matrizAccion);
            GenerarTabla.generarTableMatrizTransicionSRL(datagridgoto, Sintactico_srl.listaN, Sintactico_srl.matrizGoTo.GetLength(0));
            GenerarTabla.imprimirTablaMatrizSRL(datagridgoto, Sintactico_srl.matrizGoTo);           
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //Abrir explorador de archivos
            OpenFileDialog buscar = new OpenFileDialog
            {
                InitialDirectory = @"C:\",
                Title = "Browse Text Files",

                CheckFileExists = true,
                CheckPathExists = true,

                DefaultExt = "txt",
                Filter = "txt files (*.xml)|*.xml", //solo permite cargar txt y xml
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
            abriArchivo_srl(ruta);
            /*try
            {
                abriArchivo_srl(ruta);
                MessageBox.Show("El fichero fue cargado con exito");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: el fichero no es compatible");
            }*/
        }
        private void abriArchivo_srl(string ruta)
        {
            Sintactico_srl.inicializarCargarSLR(ruta);
            GenerarTabla.generarTableMatrizTransicionSRL(datagridaction, Sintactico_srl.listaX, Sintactico_srl.matrizAccion.GetLength(0));
            GenerarTabla.imprimirTablaMatrizSRL(datagridaction, Sintactico_srl.matrizAccion);
            GenerarTabla.generarTableMatrizTransicionSRL(datagridgoto, Sintactico_srl.listaN, Sintactico_srl.matrizGoTo.GetLength(0));
            GenerarTabla.imprimirTablaMatrizSRL(datagridgoto, Sintactico_srl.matrizGoTo);

            

            //Console.WriteLine("f: "+ Sintactico_srl.matrizGoTo.GetLength(0)+ "  c: "+ Sintactico_srl.matrizGoTo.GetLength(1));
            /*for (int i = 0; i < Sintactico_srl.matrizGoTo.GetLength(0); i++)
            {
                for (int j = 0; j < Sintactico_srl.matrizGoTo.GetLength(1); j++)
                {
                    Console.Write(Sintactico_srl.matrizGoTo[i,j]+" ");
                }
                Console.WriteLine("");
            }*/
        }

        private void Lblgoto_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
