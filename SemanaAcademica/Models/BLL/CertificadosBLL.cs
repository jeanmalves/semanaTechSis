using SemanaAcademica.Models.ViewModel;

namespace SemanaAcademica.Models.BLL
{
    public class CertificadosBLL
    {
        public static CertificadosViewModel CheckCertificados()
        {
            CertificadosViewModel model = new CertificadosViewModel();

            var palestras = ParticipacaoBLL.GetPalestrasByIdPessoa(Usuario.SessionPersist.IdPessoa);
            var minicursos = ParticipacaoBLL.GetMinicursosByIdPessoa(Usuario.SessionPersist.IdPessoa);
            var oficinas = ParticipacaoBLL.GetOficinasByIdPessoa(Usuario.SessionPersist.IdPessoa);
            var visitas = ParticipacaoBLL.GetVisitasIdPessoa(Usuario.SessionPersist.IdPessoa);

            var trabalhoVoluntario = ParticipacaoBLL.GetAllByIdPessoa(Usuario.SessionPersist.IdPessoa);
            
            //Eventos
            model.HasPalestras = (palestras.Count != 0 && palestras != null);
            model.HasMinicurso = (minicursos.Count != 0 && minicursos != null);
            model.HasOficina = (oficinas.Count != 0 && oficinas != null);
            model.HasVisita = (visitas.Count != 0 && visitas != null);

            // Colaborador
            model.HasTrabalhoVoluntario = (trabalhoVoluntario.Count != 0 && trabalhoVoluntario != null);
            model.HasOrganizador = Usuario.SessionPersist.IsAdministrador;

            // Verifica se o participante fez a doação de alimento
            model.HasContribuicao = ParticipanteBLL.HasContribuicao(Usuario.SessionPersist.IdPessoa);

            return model;
        }
    }
}