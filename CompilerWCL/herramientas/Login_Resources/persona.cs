using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompilerWCL.Login_Resources
{
    class persona
    {
        public string nombre { get; set; }
        public string apellido { get; set; }
        public char genero { get; set; }
        public string usuario { get; set; }
        public string correo { get; set; }
        public  string pass { get; set; }
        public string confpass { get; set; }

        public persona(string nombre, string apellido,char genero,string us, string email, string password, string confpass) {
            this.nombre = nombre;
            this.apellido = apellido;
            this.genero = genero;
            this.usuario = us;
            this.correo = email;
            this.pass = password;
            this.confpass = confpass;
        }

        
    }
}
