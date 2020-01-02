using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace CompilerWCL.Login_Resources
{
    class ManagerUser
    {
        string archivo = "user.txt";
        private enviaremail env = new enviaremail();

        public string user_to_txt(persona u)
        {     
            string cadena = u.nombre+","+u.apellido+","+u.genero+","+u.usuario+","+u.correo+","+u.pass+","+u.confpass;
            return cadena;
        }
        public bool verificacio()
        {
            
            return false;
        }

        public void GuardarUser(List<persona> u)
        {
            
            string dato = ""; 
            try
            {
                foreach (persona us in u)
                {
               
                    
                    dato=CargarData() + user_to_txt(us);
                    File.WriteAllText(archivo, dato);
                  
                }
                
            }
            catch (IOException ex)
            {
                // MessageBox.Show("Error: " + ex.Message);
            }

        }
       
        public ManagerUser()
        {
            if (!File.Exists(archivo))
            {
                StreamWriter writer = File.CreateText(archivo);
                writer.Close();
            }
            //ObtenerUser();
        }
        public string  CargarData()
        {
            string content = "";
            try
            {
                var fs = File.OpenRead(archivo);
                var stream = new StreamReader(fs);
                //string file=fs.Name;
                String line;
                while ((line = stream.ReadLine()) != null)
                {
                    
                    content += line + "\n";
                }
                fs.Close();
            }
            catch (FileNotFoundException ex)
            {
                //MessageBox.Show("Error: " + ex.Message);
            }
            return content;

        }
        public persona ObtenerUser(string user)
        {
           
            List<string> lista = new List<string>();
            string content = "";
            string line;
            persona p=null;
            try
            {
                var fs = File.OpenRead(archivo);
                var stream = new StreamReader(fs);
                //string file=fs.Name;
                while ((line = stream.ReadLine()) != null)
                {
                    lista.Add(line);
                }
                fs.Close();
            }
            catch (FileNotFoundException ex)
            {
                //MessageBox.Show("Error: " + ex.Message);
            }

            string persona=buscar(lista, user);
            
            if (persona!="")
            {
               p= txt_to_persona(persona);
            }
            return p;
        }

        public string buscar(List<string> users,string usuario)
        {      
            
            string u="";
            foreach (string user in users)
            {
                string[] us = user.Split(',');
                if (us[3]==usuario)
                {  
                    u = user;  
                }
                
            }
            return u;
        }

        public persona txt_to_persona(string usuario)
        {
            string []user = usuario.Split(',');
            string nombre = user[0];
            string apellido = user[1];
            char genero = char.Parse(user[2]);
            string us = user[3];
            string correo = user[4];
            string pass = user[5];
            string confpas = user[6];
            persona p = new persona(nombre,apellido,genero, us, correo, pass,confpas);
            return p;
        }

        public Object[] VerificarUsuario(string user, string password)
        {
            string pwd = "";
            string email = "";
            int numping = 0;
            if (ObtenerUser(user) != null)
            {
                persona p = ObtenerUser(user);
                pwd = p.pass;
                email = p.correo;
                if (pwd == password)
                {
                    numping = enviarMensaje(email);
                    if (numping > 0)
                    {
                        return new Object[] { numping, email };
                    }
                    else // -2 error de mensaje no enviado
                    {
                        return new Object[] { numping, "Email no enviado" };
                    }
                    /*this.Hide();
                    Form2 f2 = new Form2(p.usuario);
                    f2.Show();*/
                }
                else 
                {
                    return new Object[] { -1, "Password incorrecto" }; // error de password incorrecto   
                }
            }
            else
            {
                return new Object[] { -3, "Usuario no encontrado" }; // error -3: usuario no encontrado
            }
               
            
        }

        /**
        * Esta funcion se encarga de enviar al correo el ping aleatorio
        * -2 email no se pudo enviar
        *
        * @param email: correo del ususario al consultar en la bd
        * @return : retorno en num aleatroio o el error -2
        */
        public int enviarMensaje(string email)
        {
            string asunto = "Envio de ping para inicio de sesión";
            int num = numRandomico();
            string mensaje = "ping: " + num;
            try
            {
                env.enviarEmail(email, asunto, mensaje);
                return num;
            }
            catch (Exception ex)
            {
                return -2; // error -2: mensaje no enviado
            }
        }

        /**
         * Genero el numero aleatorio
         */
        public int numRandomico()
        {
            Random rnd = new Random();
            return rnd.Next(10000, 90000);
        }


        public bool Usuariorepetido(string user)
        {
            if (ObtenerUser(user)!=null)
            {
                return false;
            }
            return true;
        }

    }
}
