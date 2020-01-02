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
    public partial class FrmAlfabeto : Form
    {
        public string ruta;
        public FrmAlfabeto()
        {
            InitializeComponent();
            cargarAlfabeto();
        }

        private void button1_Click(object sender, EventArgs e)
        {


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

            Lexico_tk.getRuta_alfabeto(ruta);
            GenerarTabla.generarTablaAlfabeto(tbl_tabla, Lexico_tk.listAlfabeto);
        }

        public void cargarAlfabeto()
        {
            GenerarTabla.generarTablaAlfabeto(tbl_tabla, Lexico_tk.listAlfabeto);
        }

    }
}
