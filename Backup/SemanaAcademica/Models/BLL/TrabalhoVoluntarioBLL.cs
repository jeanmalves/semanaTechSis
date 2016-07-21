using SemanaAcademica.Models.DAL;
using SemanaAcademica.Models.ObjectModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SemanaAcademica.Models.BLL
{
    public class TrabalhoVoluntarioBLL
    {
        /// <summary>
        /// Insere um novo trabalho voluntário
        /// </summary>
        /// <param name="model">trabalho voluntário a ser inserido</param>
        public static bool InsertTrabalhoVoluntario(TrabalhoVoluntarioModel model)
        {
            try
            {
                var entities = new SemanaAcademicaEntities();

                entities.TrabalhoVoluntario.Add(new TrabalhoVoluntario{
                    horas = model.Horas,
                    id_participante = model.IdParticipante,
                    data_inicio = model.DataInicio,
                    data_fim = model.DataFim
                });

                entities.SaveChanges();

                return true;
            }
            catch
            {
                return false;
            }

        }

        /// <summary>
        /// Busca todos os trabalhos voluntários que o participante realizou.
        /// </summary>
        /// <param name="IdPessoa">Id do participante</param>
        /// <returns>Lista dos trabalhos voluntários</returns>
        public static List<TrabalhoVoluntarioModel> GetByIdPessoa(int IdPessoa)
        {
            try
            {
                var entities = new SemanaAcademicaEntities();
                var trabalhos = (from t in entities.TrabalhoVoluntario
                                 where t.id_participante == IdPessoa
                                 select new TrabalhoVoluntarioModel
                                 {
                                     IdTrabalhoVoluntario = t.id_trabalho,
                                     IdParticipante = t.id_participante,
                                     Horas = t.horas,
                                     DataInicio = t.data_inicio,
                                     DataFim = t.data_fim
                                 }).ToList();

                return trabalhos;
            }
            catch
            {
                return null;
            }
        }
    }
}