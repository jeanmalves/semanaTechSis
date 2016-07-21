using SemanaAcademica.Models.ObjectModel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SemanaAcademica.Areas.Admin.Models
{
    public class ListarParticipanteModel
    {
        public bool Confirmado { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public int IdParticipante { get; set; }
        public bool Matricula { get; set; }
        public string Registro { get; set; }
        public string Telefone { get; set; }
        public bool Contribuicao { get; set; }
        public bool Colaborador { get; set; }
    }

    public class CadastrarParticipanteModel : ParticipanteModel
    {
        [Required]
        [StringLength(200, MinimumLength = 10, ErrorMessage = "Tamanho de nome inválido!")]
        public override String Nome { get; set; }
        [Required(ErrorMessage = "Campo obrigatório.")]
        [EmailAddress]
        [Display(Name = "E-mail")]
        public override String Email { get; set; }
        [Required(ErrorMessage = "Campo obrigatório.")]
        [Display(Name = "Matrícula/CPF")]
        public override string Registro { get; set; }
        [Required(ErrorMessage = "Campo obrigatório.")]
        [Display(Name = "É matrícula?")]
        public override bool Matricula { get; set; }
    }

    public class EditarParticipanteModel
    {
        [Required]
        [StringLength(200, MinimumLength = 10, ErrorMessage = "Tamanho de nome inválido!")]
        public String Nome { get; set; }
        [Required(ErrorMessage = "Campo obrigatório.")]
        [EmailAddress]
        [Display(Name = "E-mail")]
        public String Email { get; set; }
        [Required(ErrorMessage = "Campo obrigatório.")]
        [Display(Name = "Matrícula/CPF")]
        public string Registro { get; set; }
        [Required(ErrorMessage = "Campo obrigatório.")]
        [Display(Name = "É matrícula?")]
        public bool Matricula { get; set; }
    }

    public class PresencaParticipanteModel
    {
        public int idParticipante { get; set; }
        public int idParticipacao { get; set; }
        public int idEvento { get; set; }
        public string NomeEvento { get; set; }
        public DateTime? HoraEntrada { get; set; }
        public DateTime? HoraSaida { get; set; }
    }
}