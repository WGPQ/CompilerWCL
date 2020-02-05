using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Arreglos
{
    class Program
    {
        static void Main(string[] args)
        {
            List<int> lista_num1 = new List<int>();
            lista_num1 = llenar_lista(lista_num1);
            Console.WriteLine("lista 1");
            imprmirList(lista_num1);
            Console.WriteLine("\n");

            List<int> lista_num2 = new List<int>();
            lista_num2 = llenar_lista(lista_num2);
            Console.WriteLine("lista 2");
            imprmirList(lista_num2);
            Console.WriteLine("\n");

            List<int> merge_list = new List<int>();
            merge_list = merge(lista_num1, lista_num2);
            Console.WriteLine("union");
            imprmirList(merge_list);


            int holamundocomoestas_muybienytueeeeeeeeee=0;
            int holamundocomoestas_muybienytueeeeeeeee = 0;

            Console.WriteLine("Hola");
            Console.ReadLine();
        }

        public static List<int> llenar_lista(List<int> lista)
        {
            Random rd = new Random();
            List<int> aux_lista = new List<int>();
            for (int i = 0; i < 5; i++)
            {
                aux_lista.Add(rd.Next(1, 25));
            }
            return aux_lista;
        }

        public static List<int> merge(List<int> list_1, List<int> list_2)
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

        public static void imprmirList(List<int> list)
        {
            foreach (int index in list)
            {
                Console.Write(index + " - ");
            }
        }
    }
}
