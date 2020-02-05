using CompilerWCL.model.Errores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompilerWCL.model.Lexico
{
    class TDSManager
    {
        public List<TDS> listaTDS;
        private TTDManager mgtipoDatos;
        private List<int> ordenDeclararVariable;
        private int declaracionMultiple = 1;
        private int auxDeclaracionMultiple = 1;
        private string auxType = "";

        public TDSManager()
        {
            this.listaTDS = new List<TDS>();
            this.mgtipoDatos = new TTDManager();

            this.ordenDeclararVariable = new List<int>();
        }

        public bool searchOnTDS(String nombreVariable, ErroresReconocidos erroresR, int linea)
        {
            bool errorCaracteres = false;
            if (nombreVariable.Length <= 8) // debe ser <= a ocho cara
            {
                return this.listaTDS.FindIndex(x => x.nametk.Equals(nombreVariable)) > -1 ? true : false;
            }
            else
            {
                // si la cadena supera los 8 caracteries, recorto la cadena                    
                errorCaracteres = this.listaTDS.FindIndex(x => (x.nametk.Length>8? x.nametk.Substring(0,8): x.nametk).Equals(nombreVariable.Substring(0, 8))) > -1 ? true : false;
                if (errorCaracteres)
                {
                    erroresR.insertarErroresReconocidos(-105, nombreVariable, linea); // Error -105 : Limite de caracteres sobrepasado.El 8vo caracter debe ser diferente.  
                    return true;
                }
                else
                {
                    return false;
                }
            }
           
        }

        public bool searchOnTDSMain(String nombreVariable)
        {
            return this.listaTDS.FindIndex(x => x.nametk.Equals(nombreVariable)) > -1 ? true : false;
        }

        public void tipoDato(string type)
        {
            this.listaTDS.Add(new TDS("", mgtipoDatos.getTipoDato(type), mgtipoDatos.getSize(type), null, false));
            auxType = type;
            auxDeclaracionMultiple = declaracionMultiple;
        }

        public bool nombreVariable(string nomVariable, ErroresReconocidos errorR, int linea)
        {
            
            if (auxDeclaracionMultiple != declaracionMultiple)
            {
                tipoDato(auxType);
            }
            declaracionMultiple++;
            if (!searchOnTDS(nomVariable.Substring(1, nomVariable.Length - 1), errorR, linea)) // si se encuentra el nombre de la variable ya declarada no entra
            {
                if (this.listaTDS.Count > 0)
                {
                    this.listaTDS[this.listaTDS.Count - 1].nametk = nomVariable.Substring(1, nomVariable.Length - 1);
                }          
                return true;
            }
            
            else
            {
                if (this.listaTDS.Count > 0)
                {
                    eliminarIdentificador();
                }              
                return false;
            }     
        }

        public void valorVariable(Object valor)
        {
            /*if (this.listaTDS.Count > 0)
            {
                this.listaTDS[this.listaTDS.Count - 1].value = valor;
                this.listaTDS[this.listaTDS.Count - 1].flagExistencia = true;
            }*/
            
        }

        /**
         * Elimino la ultima posicion ingresada cuando se produce 
         * al declarar una variable
         */
        public void eliminarIdentificador()
        {
            if (this.listaTDS.Count > 0)
            {
                this.listaTDS.RemoveAt(this.listaTDS.Count - 1);
            }
        }
        public void imprimerExpresion()
        {
            foreach (TDS o in this.listaTDS)
            {
                Console.WriteLine(o.type + "\t" + o.nametk+"\t"+o.size+"\t"+o.value);
            }
        }
    }
}
