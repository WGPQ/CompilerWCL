using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompilerWCL.model.Lexico
{
    class TTDManager
    {
        public List<TTD> listaTipoDatos;


        public TTDManager()
        {
            this.listaTipoDatos = cargarTablaTTD();

        }

        public List<TTD> cargarTablaTTD()
        {
            List<TTD> listaTipoDatos = new List<TTD>();

            listaTipoDatos.Add(new TTD(1, "integer", 4));
            listaTipoDatos.Add(new TTD(2, "real", 8));
            listaTipoDatos.Add(new TTD(3, "char", 1));
            listaTipoDatos.Add(new TTD(4, "string", 80));
            listaTipoDatos.Add(new TTD(5, "bool", 1));

            return listaTipoDatos;
        }


        public int getTipoDato(String variable)
        {
            int id = -101; //Error 101: Tipo de Dato desconocido.

            foreach (TTD ttd in this.listaTipoDatos)
            {
                if (ttd.nameType.Equals(variable))
                {
                    id = ttd.idType;
                }
            }
            return id;
        }

        public int getSize(String variable)
        {
            int id = this.listaTipoDatos.FindIndex(x => x.nameType.Equals(variable)); // si no encuentra retorna -1
            if (id >= 0)
            {
                return this.listaTipoDatos[id].size;
            }
            return -101; //Error 101: Tipo de Dato desconocido.
        }

    }
}
