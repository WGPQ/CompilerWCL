using CompilerWCL.model.Lexico;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompilerWCL.model.Sintactico
{
    class Sintactico_srl
    {
        // Matriz cargada
        public static CargarSLR cargar_slr;
        public static int[,] matrizAccion;
        public static int[,] matrizGoTo;
        public static List<char> listaX;
        public static List<char> listaN;

        public static TablaCompacta tablaCompacta;

        public static void inicializarCargarSLR(string ruta)
        {
            cargar_slr = new CargarSLR(ruta);
            matrizAccion = cargar_slr.matriz_tabla_transicionAccion;
            matrizGoTo = cargar_slr.matriz_tabla_transicionGoTo;
            listaX = cargar_slr.listX;
            listaN = cargar_slr.listN;

            tablaCompacta = new TablaCompacta(listaX, matrizAccion, listaN, matrizGoTo);
        }

        // Analizador SLR
        public static AnalizadorSLR analizadorSRL;
        public static List<Token> listTokensR = Lexico_tk.listTokensReconocidos;
        //public static List<string> listaPilaSLR;
        public static List<AnalisisDeUnaEntrada> analisisDeEntradas;
        public static void inicializarAnalizadorSRL() {
            analizadorSRL = new AnalizadorSLR(Lexico_tk.listTokensReconocidos, Lexico_tk.listAlfabeto,
                cargar_slr, tablaCompacta, Lexico_tk.claseErroresReconocidos);

            analizadorSRL.analizadorSLR();         
            analisisDeEntradas = analizadorSRL.analisisDeEntradas;

            /*
            listaPilaSLR = analizadorSRL.lista_todaPila;
            imprimir("analizador iniciado " + analizadorSRL.lista_todaPila.Count);
            foreach(string r in analizadorSRL.lista_todaPila)
            {
                imprimir("---> "+r);
            }*/
        }

        public static void imprimir(string a)
        {
            Console.WriteLine(a);
        }

       
    }
}
