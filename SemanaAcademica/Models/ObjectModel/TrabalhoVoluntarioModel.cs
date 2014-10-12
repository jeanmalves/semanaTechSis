using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SemanaAcademica.Models.ObjectModel
{
    public class TrabalhoVoluntarioModel
    {
        public int IdTrabalhoVoluntario { get; set; }
        public string Descricao { get; set; }
        public int Horas { get; set; }
        public int IdParticipante { get; set; }
    }
}