using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CursosEF.Modelo;

namespace CursosEF
{
    class Program
    {
        static datosEntities db = new datosEntities();
        private static void Main(string[] args)
        {


           
           // var db = new datosEntities();//para poder acceder de otra manera
                //esto siempre se conecta a la clase context, y lo que hace es decirle con qué base de datos quiero conectar


            //var cur = new Curso()
            // {
            //   duracion =200,
            // inicio = DateTime.Now,
            //nombre = "Costura",
            //profesor = 2
            //          };

            //db.Curso.Add(cur);
            //db.SaveChanges();


            //foreach (var curso in db.Curso)
            //{
            //  Console.WriteLine("curso: {0} empieza el {1:d} y lo imparte {2}", 
            //curso.nombre,curso.inicio,curso.profesor.HasValue?curso.Profesor1.nombre:"Sin profesor");//lo de abajo que comento es lo mismo
            // curso.Profesor1!=null?curso.Profesor1.nombre:"Sin profesor");


            //curso.profesor = 3;

            // db.Curso.Remove(curso);

            //  }
            // db.SaveChanges();


            var curso = db.Curso.Find(2); //el Find busca por clave primariaIMPORTANTE; SOLO BUSCA X CLAVE PRIMARIA; SINO USAR LINQ
           // db.Curso.Remove(db.Curso.Find(2));
            Console.WriteLine("curso: {0} empieza el {1:d} y lo imparte {2}",
                curso.nombre, curso.inicio, curso.profesor.HasValue ? curso.Profesor1.nombre : "Sin profesor");
                //lo de abajo que comento es lo mismo
            //curso.Profesor1!=null?curso.Profesor1.nombre:"Sin profesor");


            var cursos = GetByIdProfesor(1);
          //  db.Curso.RemoveRange(GetByIdProfesor(2));
            Console.ReadLine();

        
        
        }

        public static IEnumerable<Curso> GetByIdProfesor(int id)
        {

            var datos = db.Curso.Where(o => o.profesor == id).OrderBy(o => o.nombre);
            //por cada elemento que hay dentro va a buscar alguno en el que el campo profesor sea igual que id    
            
            
            /* var datos=from o in db.Curso
                      where  o.profesor == id
                      orderby o.nombre
                      select o;*/


            //las dos formas anteriores devuelven lo mismo un tipo de objeto Iqueryable que es hijo de IEnumerable y permite mantener abierta la comunicación con la DB
            return datos;

        }

        public static IEnumerable<dynamic> GetByIdProfesorDinamico(int id)
        {

            var datos = from o in db.Curso
                where o.profesor == id
                orderby o.nombre
                select new
                {
                    curso = o.nombre,
                    duracion = o.duracion
                };
         
            return datos;

        }


    }

}
