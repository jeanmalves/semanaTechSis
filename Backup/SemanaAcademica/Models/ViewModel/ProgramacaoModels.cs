using SemanaAcademica.Models.ObjectModel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SemanaAcademica.Models.ViewModel
{
    public class IndexProgramacaoViewModel
    {
        public IEnumerable<EventoProgramacaoViewModel> Eventos { get; set; }
        public IEnumerable<HorarioProgramacaoViewModel> Horarios { get; set; }
        public IEnumerable<LocalProgramacaoViewModel> Locais { get; set; }
    }
    public class EventoProgramacaoViewModel
    {
        public int IdEvento { get; set; }
        public virtual String Nome { get; set; }
        public virtual String Descricao { get; set; }
    }
    public class HorarioProgramacaoViewModel
    {
        public int IdHorario { get; set; }
        public int IdEvento { get; set; }
        public int IdLocal { get; set; }
        public DateTime HoraInicio { get; set; }
        public DateTime HoraFim { get; set; }
    }
    public class LocalProgramacaoViewModel
    {
        public int IdLocal { get; set; }
        public String Nome { get; set; }
    }


}