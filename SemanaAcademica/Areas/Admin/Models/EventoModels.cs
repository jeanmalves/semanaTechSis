using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SemanaAcademica.Areas.Admin.Models
{


    public class CadastrarPalestraModel
    {
        public string Nome { get; set; }
        public string Descricao { get; set; }
    }

    public class CadastrarMinicursoModel
    {
        public int Vagas { get; set; }
        public string Nome { get; set; }
        public string Descricao { get; set; }
    }

    public class CadastrarVisitaModel
    {
        public int Vagas { get; set; }
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public string Locomocao { get; set; }
        public string Contribuicao { get; set; }
    }

    public class ListarEventoModel
    {
        public int IdEvento { get; set; }
        public string Nome { get; set; }
        public string Descricao { get; set; }
    }

    public class ListarPalestraModel
    {
        public int IdPalestra { get; set; }
        public string Nome { get; set; }
        public string Descricao { get; set; }
    }

    public class ListarMinicursoModel
    {
        public int IdMinicurso { get; set; }
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public int Vagas { get; set; }

    }

    public class ListarVisitaModel
    {
        public int IdVisita { get; set; }
        public int Vagas { get; set; }
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public string Locomocao { get; set; }
        public string Contribuicao { get; set; }
    }

    public class ListarHorarioModel
    {
        public int idHorario { get; set; }
        [Display(Name = "Horário inicial")]
        public DateTime HoraInicio { get; set; }
        [Display(Name = "Horário final")]
        public DateTime Horafim { get; set; }
        public string Local { get; set; }
    }

    public class CadastrarHorarioModel
    {
        public int idEvento { get; set; }
        [Display(Name = "Horário inicial")]
        public DateTime HoraInicio { get; set; }
        [Display(Name = "Horário final")]
        public DateTime Horafim { get; set; }
        [Display(Name = "Local")]
        public int idLocal { get; set; }
    }

    public class EditarPalestraModel
    {
        public int IdPalestra { get; set; }
        public string Nome { get; set; }
        public string Descricao { get; set; }
    }

    public class EditarMinicursoModel
    {
        public int IdMinicurso { get; set; }
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public int Vagas { get; set; }
    }

    public class EditarVisitaModel
    {
        public int IdVisita { get; set; }
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public int Vagas { get; set; }
        public string Locomocao { get; set; }
        public string Contribuicao { get; set; }

    }




}