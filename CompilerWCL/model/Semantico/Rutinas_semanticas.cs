using CompilerWCL.model.Lexico;
using CompilerWCL.model.Sintactico;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompilerWCL.model.Semantico
{
    class Rutinas_semanticas
    {
        public List<Atributos> list_pila_Semantica { get; set; }
        public List<Cuadruplos> list_codigo_intermedio { get; set; }
        private int numero_tupla = 0;


        //geracion de constructor


        public int next()
        {
            return this.numero_tupla++;
            
        }

        public void gen(string operador, string operando_1, string operando_2, string result)
        {
            list_codigo_intermedio.Add(new Cuadruplos(next(), operador, operando_1, operando_2, result));
        }

        public List<int> make_list(int valor)
        {
            List<int> aux_list = new List<int>();
            aux_list.Add(valor);
            return aux_list;
        }

        public List<int> merge(List<int> list_1, List<int> list_2)
        {
            List<int> merge_list = new List<int>();

            for (int i = 0; i < list_1.Count; i++)
            {
                merge_list.Add(list_1[i]);
            }

            for (int i = 0; i < list_2.Count; i++)
            {
                merge_list.Add(list_2[i]);
            }
            return merge_list;
        }

        public void backPatch(int direcion, List<int> lista)
        {
            foreach(int i in lista)
            {
                this.list_codigo_intermedio[i].resultado = "GOTO " + direcion;
            }
        }

        public void generar_codigoCuadruplo(string regla)
        {
            int num_regla = 0;
            for (int i = 0; i < Sintactico_srl.analizadorSRL.listReglasReconocidas.Count; i++)
            {
                num_regla = Sintactico_srl.analizadorSRL.listReglasReconocidas[i].numero_regla;

                switch (num_regla)
                {
                    case 1:
                        break;

                    case 2:
                        break;

                    case 3:
                        break;

                    case 4:
                        break;

                    case 5:
                        break;

                    case 6:
                        break;

                    case 7:
                        break;

                    case 8:
                        break;

                    case 9:
                        break;

                    case 10:
                        break;

                    case 11:
                        break;

                    case 12:
                        break;

                    case 13:
                        break;

                    case 14:
                        break;

                    case 15:
                        break;

                    case 16:
                        break;

                    case 17:
                        break;

                    case 18:
                        break;

                    case 19:
                        break;

                    case 20:
                        break;

                    case 21:
                        break;

                    case 22:
                        break;

                    case 23:
                        break;

                    case 24:
                        break;

                    case 25:
                        break;

                    case 26:
                        break;

                    case 27:
                        break;

                    case 28:
                        break;

                    case 29:
                        break;

                    case 30:
                        break;

                    default:
                        break;
                }
            }
        }



    }
}
