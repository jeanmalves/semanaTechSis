using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SemanaAcademica.Areas.Admin.Models
{
    public class RegistrarParticipacaoModel
    {
        public int idEvento { get; set; }
        public string NomeEvento { get; set; }
        public DateTime[] HoraInicio { get; set; }
        public DateTime[] HoraFim { get; set; }
        public string UltimaParticipacaoRegistro { get; set; }
        public string UltimaParticipacaoNome { get; set; }
        public string Sentido { get; set; }
    }

    public class EditarParticipacaoModel
    {
        public string NomeEvento { get; set; }
        public string NomeParticipante { get; set; }
        public DateTime? HoraInicio { get; set; }
        public DateTime? HoraFim { get; set; }
    }

    public class CriarParticipacaoModel
    {
        public int idEvento { get; set; }
        public int idParticipante { get; set; }
        public DateTime? HoraInicio { get; set; }
        public DateTime? HoraFim { get; set; }
    }
}