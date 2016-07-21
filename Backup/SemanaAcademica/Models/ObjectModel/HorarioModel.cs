using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SemanaAcademica.Models.ObjectModel
{
    public class HorarioModel
    {
        public int IdHorario { get; set; }
        public int IdEvento { get; set; }
        public int IdLocal { get; set; }
        public DateTime HoraInicio { get; set; }
        public DateTime HoraFim { get; set; }
    }
}