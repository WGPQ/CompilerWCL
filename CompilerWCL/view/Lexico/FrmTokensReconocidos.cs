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
    public partial class FrmTokensReconocidos : Form
    {
        public FrmTokensReconocidos()
        {
            InitializeComponent();
            //FrmEditor ed = new FrmEditor();
            //ed.Show();
            cargarArchivo();
        }
        public string ruta;
        public static string ruta2;
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
                Filter = "txt files (*.txt;*.xml)|*.txt;*.xml;*.utn", //solo permite cargar txt y xml
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

            //try
            //{
                //cargarArchivo(ruta);
            ruta2 = ruta;
                MessageBox.Show("El fichero fue cargado con exito");
            /*}
            catch (Exception ex)
            {
                MessageBox.Show("Error: el fichero no es compatible");
            }*/
        }

        public void cargarArchivo()
        {
                //Lexico_tk l = new Lexico_tk();
            // Lexico_tk.getRutaTokensReconocidos(ruta);
            try
            {
                GenerarTabla.generarTokensReconocidos(tbl_tabla, Lexico_tk.listSimbolosReconocidos);
            }
            catch(Exception ex)
            {
                MessageBox.Show("No se encontro ningun movimiento");
            }
            
        }

    }
}
