using SemanaAcademica.Models.DAL;
using SemanaAcademica.Models.ObjectModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SemanaAcademica.Models.BLL
{
    public class ParticipanteBLL
    {
        static public bool InsertParticipante(ParticipanteModel model)
        {
            try
            {
                var entities = new SemanaAcademicaEntities();
                if (entities.Participante.Any(p => p.id_pessoa == model.IdPessoa))
                    return false;

                entities.Participante.Add(
                    new Participante
                    {
                        id_pessoa = model.IdPessoa,
                        contribuicao = model.Contribuicao,
                        matricula = model.Matricula,
                        registro = model.Registro,
                        curso = model.Curso,
                        universidade = model.Universidade
                    }
                );
                entities.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }


        static public IEnumerable<ParticipanteModel> SelectParticipantes()
        {
            int total = 0;
            return SelectParticipantes(null, 0, out total);
        }
        static public IEnumerable<ParticipanteModel> SelectParticipantes(string filtro, int pag, out int total)
        {
            total = 0;
            try
            {
                var entities = new SemanaAcademicaEntities();
                var participantes = entities.Participante.OrderBy(p => p.Pessoa.nome).Where(p => p.registro.ToLower().Contains(filtro.ToLower()) || p.Pessoa.email.ToLower().Contains(filtro.ToLower()) || p.Pessoa.nome.ToLower().Contains(filtro.ToLower()) || filtro == null || filtro == "");

                total = participantes.Count();

                if (pag != 0)
                {
                    participantes = participantes.Skip(10 * (pag - 1)).Take(10);
                }

                return participantes.Select(p => new ParticipanteModel
                {
                    Confirmado = p.Pessoa.confirmado,
                    Nome = p.Pessoa.nome,
                    Chave = p.Pessoa.chave,
                    Email = p.Pessoa.email,
                    IdParticipante = p.id_participante,
                    IdPessoa = p.id_pessoa,
                    Matricula = p.matricula,
                    Registro = p.registro,
                    Telefone = p.Pessoa.telefone,
                    Contribuicao = p.contribuicao,
                    Curso = p.curso,
                    Universidade = p.universidade
                });
            }
            catch
            {
                return null;
            }
        }


        public static bool UpdateContribuicao(int id, bool status)
        {
            try
            {
                var entities = new SemanaAcademicaEntities();
                var participante = entities.Participante.FirstOrDefault(p => p.id_participante == id);
                participante.contribuicao = status;
                entities.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public static bool UpdateParticipante(ParticipanteModel model)
        {
            try
            {
                var entities = new SemanaAcademicaEntities();
                var participante = entities.Participante.FirstOrDefault(p => p.id_participante == model.IdParticipante);
                participante.Pessoa.nome = model.Nome;
                participante.Pessoa.email = model.Email;
                participante.registro = model.Registro;
                participante.matricula = model.Matricula;
                participante.universidade = model.Universidade;
                participante.curso = model.Curso;
                entities.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}