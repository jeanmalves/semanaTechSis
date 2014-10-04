using SemanaAcademica.Models.ObjectModel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SemanaAcademica.Models.ViewModel
{
    public class CadastroParticipanteViewModel : ParticipanteModel
    {
        [Required]
        [StringLength(200, MinimumLength = 10, ErrorMessage = "Tamanho de nome inválido!")]
        public override String Nome { get; set; }
        [Required(ErrorMessage = "Campo obrigatório.")]
        [EmailAddress]
        [Display(Name = "E-mail")]
        public override String Email { get; set; }
        [Required(ErrorMessage = "Campo obrigatório.")]
        [EmailAddress]
        [Compare("Email")]
        [Display(Name = "Confirmação de E-mail")]
        public String EmailConfirmacao { get; set; }
        [Required(ErrorMessage = "Campo obrigatório.")]
        [StringLength(200, MinimumLength = 6, ErrorMessage = "A sua senha deve ter pelo menos 6 caracteres.")]
        public override String Senha { get; set; }
        [Required(ErrorMessage = "Campo obrigatório.")]
        [Compare("Senha")]
        [Display(Name = "Confirmação de Senha")]
        public String SenhaConfirmacao { get; set; }
        [Required(ErrorMessage = "Campo obrigatório.")]
        [Phone]
        public override String Telefone { get; set; }
        [Required(ErrorMessage = "Campo obrigatório.")]
        [Display(Name = "Matrícula/CPF")]
        public override string Registro { get; set; }
        [Required(ErrorMessage = "Campo obrigatório.")]
        [Display(Name = "É matrícula?")]
        public override bool Matricula { get; set; }
    }

    public class EntrarViewModel {
        [Required(ErrorMessage = "Campo obrigatório.")]
        [EmailAddress]
        [Display(Name = "E-mail")]
        public String Email { get; set; }
        [Required(ErrorMessage = "Campo obrigatório.")]
        [StringLength(200, MinimumLength = 6, ErrorMessage = "A sua senha deve ter pelo menos 6 caracteres.")]
        public String Senha { get; set; }
    }

    public class RedefinirSenhaViewModel {
        [Required(ErrorMessage = "Campo obrigatório.")]
        [StringLength(200, MinimumLength = 6, ErrorMessage = "A sua senha deve ter pelo menos 6 caracteres.")]
        public String Senha { get; set; }
        [Required(ErrorMessage = "Campo obrigatório.")]
        [Compare("Senha")]
        [Display(Name = "Confirmação de Senha")]
        public String SenhaConfirmacao { get; set; }

        public String t { get; set; }
        public String e { get; set; }
        public String h { get; set; }

    }
}