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
    
    public partial class Participante
    {
        public Participante()
        {
            this.Matricula1 = new HashSet<Matricula>();
            this.Participacao = new HashSet<Participacao>();
            this.TrabalhoVoluntario = new HashSet<TrabalhoVoluntario>();
        }
    
        public int id_participante { get; set; }
        public int id_pessoa { get; set; }
        public string registro { get; set; }
        public bool matricula { get; set; }
        public bool contribuicao { get; set; }
        public string universidade { get; set; }
        public string curso { get; set; }
    
        public virtual ICollection<Matricula> Matricula1 { get; set; }
        public virtual ICollection<Participacao> Participacao { get; set; }
        public virtual Pessoa Pessoa { get; set; }
        public virtual ICollection<TrabalhoVoluntario> TrabalhoVoluntario { get; set; }
    }
}