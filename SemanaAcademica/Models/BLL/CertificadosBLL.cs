using SemanaAcademica.Models.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SemanaAcademica.Models.BLL
{
    public class CertificadosBLL
    {
        public static CertificadosViewModel CheckCertificados()
        {
            CertificadosViewModel model = new CertificadosViewModel();

            var palestras = ParticipacaoBLL.GetPalestrasByIdPessoa(Usuario.SessionPersist.IdPessoa);
            var minicursos = ParticipacaoBLL.GetMinicursosByIdPessoa(Usuario.SessionPersist.IdPessoa);

            var trabalhoVoluntario = ParticipacaoBLL.GetAllByIdPessoa(Usuario.SessionPersist.IdPessoa);
            
            //Eventos
            model.HasPalestras = (palestras.Count != 0 && palestras != null);
            model.HasMinicurso = (minicursos.Count != 0 && minicursos != null);

            // Colaborador
            model.HasTrabalhoVoluntario = (trabalhoVoluntario.Count != 0 && trabalhoVoluntario != null);
            model.HasOrganizador = Usuario.SessionPersist.IsAdministrador;

            return model;
        }
    }
}