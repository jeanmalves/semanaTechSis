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

            var participacoes = ParticipacaoBLL.GetPalestrasByIdPessoa(Usuario.SessionPersist.IdPessoa);
            var minicursos = ParticipacaoBLL.GetMinicursosByIdPessoa(Usuario.SessionPersist.IdPessoa);

            model.HasParticipacao = (participacoes.Count != 0 && participacoes != null);
            model.HasMinicurso = (minicursos.Count != 0 && minicursos != null);
            model.HasOrganizador = Usuario.SessionPersist.IsAdministrador;
            model.HasColaborador = Usuario.SessionPersist.IsColaborador;

            return model;
        }
    }
}