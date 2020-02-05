using CompilerWCL.view.Lexico;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompilerWCL.model.Semantico
{
    class principalSemantico
    {
        //public static Semantico_srl claseSemantica;
        //public static prueba2Semantico claseSemantica;
        public static prueba3Semantico claseSemantica;

        public static void iniciarSemamntico(FrmEditor frmE)
        {
            //claseSemantica = new Semantico_srl();
            //claseSemantica = new prueba2Semantico();
            claseSemantica = new prueba3Semantico(frmE);
        }      
    }
}
