using SemanaAcademica.Models.DAL;
using SemanaAcademica.Models.ObjectModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SemanaAcademica.Models.BLL
{
    public class ParticipacaoBLL
    {
        static public bool RegistrarParticipacao(int id_participante, int id_evento, out ParticipacaoSentido sentido)
        {
            sentido = ParticipacaoSentido.Entrada;

            try
            {
                var entities = new SemanaAcademicaEntities();
                var entradas = entities.Participacao.Where(p => p.id_evento == id_evento && p.id_participante == id_participante && p.hora_saida == null);

                if (entradas.Count() > 0)
                {
                    var entrada = entradas.OrderByDescending(e => e.hora_entrada).First();
                    entrada.hora_saida = DateTime.Now;
                    entities.SaveChanges();
                    sentido = ParticipacaoSentido.Saida;
                }
                else
                {
                    var participacao = new Participacao
                    {
                        hora_entrada = DateTime.Now,
                        id_evento = id_evento,
                        id_participante = id_participante
                    };
                    entities.Participacao.Add(participacao);
                    entities.SaveChanges();
                    sentido = ParticipacaoSentido.Entrada;
                }
                return true;
            }
            catch
            {
                return false;
            }
        }



        public static List<ParticipacaoModel> SelectByIdParticipante(int idParticipante)
        {
            try
            {
                var entities = new SemanaAcademicaEntities();
                return entities.Participacao.Where(p => p.id_participante == idParticipante).Select(p => new ParticipacaoModel
                {
                    idParticipacao = p.id_participacao,
                    HoraSaida = p.hora_saida,
                    HoraEntrada = p.hora_entrada,
                    idEvento = p.id_evento,
                    NomeEvento = p.Evento.nome,
                }).ToList();
            }
            catch
            {
                return null;
            }
        }

        public static bool DeleteParticipacao(int id, out int idParticipante)
        {
            idParticipante = 0;
            try
            {
                var entities = new SemanaAcademicaEntities();
                var model = entities.Participacao.FirstOrDefault(p => p.id_participacao == id);
                idParticipante = model.id_participante;
                entities.Participacao.Remove(model);
                entities.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public static ParticipacaoModel SelectParticipacao(int id)
        {
            try
            {
                var entities = new SemanaAcademicaEntities();
                return entities.Participacao.Where(p => p.id_participacao == id).Select(p => new ParticipacaoModel
                {
                    HoraEntrada = p.hora_entrada,
                    HoraSaida = p.hora_saida,
                    idEvento = p.id_evento,
                    idParticipacao = p.id_participacao,
                    NomeEvento = p.Evento.nome,
                    idParticipante = p.id_participante,
                    NomeParticipante = p.Participante.Pessoa.nome
                }).FirstOrDefault();
            }
            catch
            {
                return null;
            }
        }

        public static bool UpdateParticipacao(int id, ParticipacaoModel model, out int idParticipante)
        {
            idParticipante = 0;
            try
            {
                var entities = new SemanaAcademicaEntities();
                var participacao = entities.Participacao.FirstOrDefault(p => p.id_participacao == id);
                idParticipante = participacao.id_participante;
                participacao.hora_entrada = model.HoraEntrada;
                participacao.hora_saida = model.HoraSaida;
                entities.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public static bool InsertParticipacao(ParticipacaoModel model)
        {
            try
            {
                var entities = new SemanaAcademicaEntities();
                var participacao = new Participacao
                   {
                       hora_entrada = model.HoraEntrada,
                       hora_saida = model.HoraSaida,
                       id_evento = model.idEvento,
                       id_participante = model.idParticipante
                   };
                entities.Participacao.Add(participacao);
                entities.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        #region Selects

        [Obsolete("Utiliza agora select por tipo de evento ou de todos os eventos por <b>GetAllByIdPessoa</b>", true)]
        public static List<ParticipacaoModel> SelectByIdPessoa(int idpessoa)
        {
            try
            {
                var entities = new SemanaAcademicaEntities();

                return entities.Participacao
                    .Where(p => p.Participante.Pessoa.id_pessoa == idpessoa)
                    .Select(p => new ParticipacaoModel
                    {
                        idParticipacao = p.id_participacao,
                        HoraSaida = p.hora_saida,
                        HoraEntrada = p.hora_entrada,
                        idEvento = p.id_evento,
                        NomeEvento = p.Evento.nome,
                    }).ToList();
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// Retorna todas as participações de uma pessoa.
        /// </summary>
        /// <param name="idPessoa">Id Pessoa</param>
        /// <returns>List of ParticipacaoModel</returns>
        public static List<ParticipacaoModel> GetAllByIdPessoa(int idPessoa)
        {
            var entities = new SemanaAcademicaEntities();
            try
            {
                return entities.Participacao
                    .Where(p => p.Participante.Pessoa.id_pessoa == idPessoa)
                    .Select(p => new ParticipacaoModel
                    {
                        idParticipacao = p.id_participacao,
                        HoraSaida = p.hora_saida,
                        HoraEntrada = p.hora_entrada,
                        idEvento = p.id_evento,
                        NomeEvento = p.Evento.nome,
                    }).ToList();
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// Retorna apenas as palestras que uma pessoa participou.
        /// </summary>
        /// <param name="idPessoa">Id da Pessoa</param>
        /// <returns>Lista de participações em palestras</returns>
        public static List<ParticipacaoModel> GetPalestrasByIdPessoa(int idPessoa)
        {
            try
            {
                var entities = new SemanaAcademicaEntities();
                var palestras = (from prt in entities.Participacao
                                 join pl in entities.Palestra on prt.id_evento equals pl.id_evento
                                 where prt.id_participante == idPessoa
                                 select new ParticipacaoModel
                                 {
                                     idParticipacao = prt.id_participacao,
                                     HoraSaida = prt.hora_saida,
                                     HoraEntrada = prt.hora_entrada,
                                     idEvento = prt.id_evento,
                                     NomeEvento = prt.Evento.nome,
                                 }).ToList();

                return palestras;
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// Busca todos os Minicursos que o usuário participou.
        /// </summary>
        /// <param name="idPessoa">Id da Pessoa</param>
        /// <returns>Lista de participações em Minicursos</returns>
        public static List<ParticipacaoModel> GetMinicursosByIdPessoa(int idPessoa)
        {
            try
            {
                var entities = new SemanaAcademicaEntities();
                var minicursos = (from prt in entities.Participacao
                                 join m in entities.Minicurso on prt.id_evento equals m.id_evento
                                 where prt.id_participante == idPessoa
                                 select new ParticipacaoModel
                                 {
                                     idParticipacao = prt.id_participacao,
                                     HoraSaida = prt.hora_saida,
                                     HoraEntrada = prt.hora_entrada,
                                     idEvento = prt.id_evento,
                                     NomeEvento = prt.Evento.nome,
                                 }).ToList();

                return minicursos;
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// Busca todos as que Oficinas usuário participou.
        /// </summary>
        /// <param name="idPessoa">Id da Pessoa</param>
        /// <returns>Lista de participações em Oficinas</returns>
        public static List<ParticipacaoModel> GetOficinasByIdPessoa(int idPessoa)
        {
            try
            {
                var entities = new SemanaAcademicaEntities();
                var oficinas = (from prt in entities.Participacao
                                  join m in entities.Oficina on prt.id_evento equals m.id_evento
                                  where prt.id_participante == idPessoa
                                  select new ParticipacaoModel
                                  {
                                      idParticipacao = prt.id_participacao,
                                      HoraSaida = prt.hora_saida,
                                      HoraEntrada = prt.hora_entrada,
                                      idEvento = prt.id_evento,
                                      NomeEvento = prt.Evento.nome,
                                  }).ToList();

                return oficinas;
            }
            catch
            {
                return null;
            }
        }

        #endregion Selects
    }
}