using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SemanaAcademica.Models.ObjectModel
{
    public class EventoModel
    {
        public int IdEvento { get; set; }
        public virtual String Nome { get; set; }
        public virtual String Descricao { get; set; }
    }

    public class VisitaModel : EventoModel
    {
        public int IdVisita { get; set; }
        public int Vagas { get; set; }
        public string Locomocao { get; set; }
        public string Contribuicao { get; set; }
    }
    public class PalestraModel : EventoModel
    {
        public int IdPalestra { get; set; }
    }
    public class MinicursoModel : EventoModel
    {
        public int IdMinicurso { get; set; }
        public int Vagas { get; set; }
    }

    public class OficinaModel : EventoModel
    {
        public int IdOficina { get; set; }
        public int Vagas { get; set; }
    }
}