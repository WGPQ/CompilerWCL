using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompilerWCL.Login_Resources
{
    class usuario
    {
        public List<persona> listapersona;
        public usuario(string nombre,string apellido,char genero,string usuario, string correo, string pass,string confpass)
        {
            listapersona = new List<persona>();
            user(nombre,apellido,genero,usuario, correo, pass,confpass);
            guardar();
        }
        public void user(string nombre, string apellido, char genero, string usuario,string correo,string pass, string confpass)
        {
            persona p = new persona(nombre,apellido,genero,usuario, correo, pass,confpass);
            listapersona.Add(p);

        }
        public void guardar()
        {
            ManagerUser mu = new ManagerUser();
            mu.GuardarUser(listapersona);
        }
    }
}
