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


            //las dos formas anteriores devuelven lo mismo un tipo de objeto Iqueryable que es hijo de IEnumerable 
            //y permite mantener abierta la comunicación con la DB
            return datos;

        }

        public static int Contar() //vamos a usar métodos de agregación, si solo quiero el número no hace falta que cree un IEnumerable
        {
            var n = db.Curso.Count(o=>o.inicio>DateTime.Now);//esto es para contar, y lo que te devuelve es el número de registros que cumplen la condición en los ()

            return n;
        }

        public static int Sumar()//vamos a usar métodos de agregación
        {
            var n = db.Curso.Sum(o => o.duracion);//suma
            var n2 = db.Curso.Max(o => o.duracion);//máximo
            var n3 = db.Curso.Min(o => o.duracion);//mínimo
            var n4 = db.Curso.Average(o => o.duracion);//media aritmética
            var n5 = db.Curso.Where(o=>o.profesor==1).Average(o => o.duracion);//media aritmética de los cursos del profesor 1

            return n2;
        }

        //PARA HACER CONSULTAS NO DE IGUALDAD COMO LAS ANTERIORES DOS FUNCIONES

        public static IEnumerable<Curso> BuscarDentro()
        {
            String[] array = {"a", "b", "c", "m"};//array para poder poner el ejemplo de c4



            var c = db.Curso.Where(o => o.nombre.StartsWith("c") || o.nombre.StartsWith("d"));//empieza con, estas son las funciones propias de string,
            //se puede usar el && y el || perfectamente para cotar las búsquedas
            var cc = db.Curso.Where(o => o.duracion > 100 && o.nombre.Contains("c") && o.profesor == 1);
            var c2 = db.Curso.Where(o => o.nombre.EndsWith("c"));//termina con
            var c3 = db.Curso.Where(o => o.nombre.Contains("c"));//contiene
            var c4 = db.Curso.Where(o => array.Contains(o.nombre));//aquí busca un nombre de curso que esté contenido dentro del array, es decir,
            //si los nombres de los cursos fueran a,x,w,e y m, la búsqueda devolvería a y m que son los que están contenido en el array.

            return c;
        }

        public static int[] IdProfesores()//he puesto el tipo corchetes
        {
            //la manera vieja de hacerse un array de los ids de profesores
            /*int[] ids = new int[db.Profesor.Count()];

            int n = 0;
            foreach (var profesor in db.Profesor)
            {
                ids[n++] = profesor.idProfesor;
            }

            return ids;*/

            var ids = db.Profesor.Select(o => o.idProfesor);//aquí le digo que lo que quiero seleccionar es cuando o es el idProfesor
            return ids.ToArray();//aquí lo manda a un array de manera normal
        }
        
        public static Curso GetById(int id)
            //where es un método diseñado para buscar colecciones de objetos, para buscar un solo valor hay dos métodos, este es el primero
        {
            //estos tres métodos solo pueden usarse con expresiones landa, para los demás con where
            var datos = db.Curso.Find(id);

            var datos2 = db.Curso.FirstOrDefault(o => o.idCurso == id);//esto es el segundo método, devuelve el primero y sino encuentra nulo

            var datos3 = db.Curso.First(o => o.idCurso == id);//este es un tercer método pero que debe usarse 
            //cuando se está seguro de que hay un objeto como el que se busca xq sino encuentra da una excepción, 
            //los otros devuelven sino null, y devuelve el primero
           //los tres métodos devuelven un objeto de tipo curso, xq es solo uno lo que se devuelve, en el caso de where al ser una coleccion es un IEnumerable 
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
