using CompilerWCL.herramientas;
using CompilerWCL.model.Lexico;
using System;
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
    public partial class FrmAFD : Form
    {
        public FrmAFD()
        {
            InitializeComponent();
        }

        public string ruta;
        public static string ruta2;
        private void btnCargarAFD_Click(object sender, EventArgs e)
        {
            //Abrir explorador de archivos
            OpenFileDialog buscar = new OpenFileDialog
            {
                InitialDirectory = @"C:\",
                Title = "Browse Text Files",

                CheckFileExists = true,
                CheckPathExists = true,

                DefaultExt = "txt",
                Filter = "txt files (*.txt;*.xml)|*.txt;*.xml", //solo permite cargar txt y xml
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
                cargarArchivo(ruta);
                ruta2 = ruta;
                MessageBox.Show("El fichero fue cargado con exito");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: el fichero no es compatible");
            }

        }
        public void cargarArchivo(string ruta)
        {
            //Lexico_tk l = new Lexico_tk();
            //Lexico_tk.
            //Lexico_tk.getRuta_afd(ruta);
            GenerarTabla.generarTableMatrizTransicion(tbl_tabla, Lexico_tk.columnSimbolos, Lexico_tk.matriz_tabla_transicion.GetLength(0));
            GenerarTabla.imprimirTablaMatrizTransicion(tbl_tabla, Lexico_tk.matriz_tabla_transicion);
        }
       
    }
}
