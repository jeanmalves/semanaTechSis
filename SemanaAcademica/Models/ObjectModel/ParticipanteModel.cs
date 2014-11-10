using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SemanaAcademica.Models.ObjectModel
{
    public class ParticipanteModel : PessoaModel
    {
        public int IdParticipante { get; set; }
        public virtual string Registro { get; set; }
        public virtual bool Matricula { get; set; }
        public virtual bool Contribuicao { get; set; }
        public virtual string Curso { get; set; }
        public virtual string Universidade { get; set; }
    }
}