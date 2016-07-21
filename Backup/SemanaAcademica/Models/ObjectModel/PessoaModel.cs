using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SemanaAcademica.Models.ObjectModel
{
    public class PessoaModel
    {
        public int IdPessoa { get; set; }
        public virtual String Email { get; set; }
        public virtual String Nome { get; set; }
        public virtual String Telefone { get; set; }
        public virtual String Senha { get; set; }
        public virtual bool Confirmado { get; set; }
        public virtual Guid Chave { get; set; }
    }
}