//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace SemanaAcademica.Models.DAL
{
    using System;
    using System.Collections.Generic;
    
    public partial class Visita
    {
        public int id_visita { get; set; }
        public int id_evento { get; set; }
        public string locomocao { get; set; }
        public string contribuicao { get; set; }
        public Nullable<int> vagas { get; set; }
    
        public virtual Evento Evento { get; set; }
    }
}
