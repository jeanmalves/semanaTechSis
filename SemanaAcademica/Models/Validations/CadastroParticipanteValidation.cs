using SemanaAcademica.Models.Collections;
using SemanaAcademica.Models.ViewModel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SemanaAcademica.Models.Validations
{
    public class CadastroParticipanteValidation
    {

        public static ValidationResult ValidaCampoUniversidadeMatricula(string universidade, ValidationContext validationContext)
        {
            CadastroParticipanteViewModel objParticipanteViewModel = (CadastroParticipanteViewModel)validationContext.ObjectInstance;
            
            if (objParticipanteViewModel.Matricula)
            {
                if (String.IsNullOrEmpty(universidade))
                {
                    return new ValidationResult("Campo obrigatório.");
                }
                return ValidationResult.Success;
            }
            else
            {
                return ValidationResult.Success;
            }
        }

        public static ValidationResult ValidaCurso(string curso, ValidationContext validationContext)
        {
            CadastroParticipanteViewModel objParticipanteViewModel = (CadastroParticipanteViewModel)validationContext.ObjectInstance;
            if (objParticipanteViewModel.Matricula && String.IsNullOrEmpty(objParticipanteViewModel.Curso))
            {
                return new ValidationResult("Campo obrigatório.");
            }
            return ValidationResult.Success;
        }
    }
}