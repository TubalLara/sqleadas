//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace CursosEF.Modelo
{
    using System;
    using System.Collections.Generic;
    
    public partial class Curso
    {
        public int idCurso { get; set; }
        public string nombre { get; set; }
        public Nullable<int> profesor { get; set; }
        public System.DateTime inicio { get; set; }
        public int duracion { get; set; }
    
        public virtual Profesor Profesor1 { get; set; }
    }
}
