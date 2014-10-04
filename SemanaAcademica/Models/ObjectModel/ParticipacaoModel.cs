using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SemanaAcademica.Models.ObjectModel
{
    public enum ParticipacaoSentido { Entrada, Saida }

    public class ParticipacaoModel 
    {
        public int idParticipacao { get; set; }
        public int idEvento { get; set; }
        public string NomeEvento { get; set; }
        public int idParticipante { get; set; }
        public string NomeParticipante { get; set; }
        public DateTime? HoraEntrada { get; set; }
        public DateTime? HoraSaida { get; set; }
    }
}