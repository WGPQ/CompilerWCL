using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace CompilerWCL.model.Errores
{
    class ErroresReconocidos
    {
        private XDocument document;
        public List<Error> listErrores { get; set; }

        public List<Error> list_erroresReconocidos { get; set; }
        public ErroresReconocidos(string ruta)
        {
            this.document = XDocument.Load(ruta);
            this.listErrores = list_errores();
            this.list_erroresReconocidos = new List<Error>();
        }

        /**
         * Estraigo los errores de un archivo xml
         * 
         * return: retorno una lista de errores
         */
        private List<Error> list_errores()
        {
            // Errores
            var transicion = from tran in this.document.Descendants("Errores") select tran;
            List<Error> l_error = new List<Error>();

            int numError = -1;
            string detalle = "";

            foreach (XElement u in transicion.Elements("error"))
            {
                numError = int.Parse(u.Element("numError").Value);
                detalle = u.Element("detalle").Value;
                l_error.Add(new Error(numError, detalle));
            }
            return l_error;
        }

        public void insertarErroresReconocidos(int numError, string entrada_valor, int linea)
        {
            // busco el mensaje con el número de error ingresado
            string mensajeError = this.listErrores[this.listErrores.FindIndex(x => x.numError == numError)].detalle;
            if (this.list_erroresReconocidos.Count <= 8) // 8 el numero de errores maximos
            {
                this.list_erroresReconocidos.Add(new Error(numError, "Error " +
                    numError + " : " + entrada_valor + " -> " + mensajeError + " ( Linea " + linea + " )"));
            }
        }

        public void insertarMesajeSurencia(int numError, string entrada_valor, string correcion, int linea)
        {
            // busco el mensaje con el número de error ingresado
            string mensajeError = this.listErrores[this.listErrores.FindIndex(x => x.numError == numError)].detalle;
            if (this.list_erroresReconocidos.Count <= 8) // 8 el numero de errores maximos
            {
                this.list_erroresReconocidos.Add(new Error(numError, entrada_valor + " -> " + mensajeError + " : " + correcion + " ( Linea " + linea + " )"));
            }
        }

        public string imprimir_erroresConsola()
        {
            string res = "";
            foreach (Error er in this.list_erroresReconocidos)
            {
                res += er.detalle + "\n";
            }
            return res;
        }

        public void resetearLista()
        {
            this.list_erroresReconocidos = new List<Error>();
        }
    }
}
