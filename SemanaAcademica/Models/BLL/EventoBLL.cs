using SemanaAcademica.Areas.Admin.Models;
using SemanaAcademica.Models.DAL;
using SemanaAcademica.Models.ObjectModel;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SemanaAcademica.Models.BLL
{
    public static class EventoBLL
    {
        static public IEnumerable<EventoModel> SelectEventos()
        {
            try
            {
                var entities = new SemanaAcademicaEntities();
                return entities.Evento.OrderBy(e => e.nome).Select(e => new EventoModel
                {
                    IdEvento = e.id_evento,
                    Nome = e.nome,
                    Descricao = e.descricao,
                });
            }
            catch
            {
                return null;
            }
        }

        static public IEnumerable<HorarioModel> SelectHorarios(int idEvento)
        {
            try
            {
                var entities = new SemanaAcademicaEntities();
                return entities.Horario.Where(h => h.id_evento == idEvento).Select(h => new HorarioModel
                {
                    IdHorario = h.id_horario,
                    IdLocal = h.id_local,
                    HoraInicio = h.hora_inicio,
                    HoraFim = h.hora_fim
                });
            }
            catch
            {
                return null;
            }

        }


        static public IEnumerable<PalestraModel> SelectPalestras()
        {
            try
            {
                var entities = new SemanaAcademicaEntities();
                return entities.Palestra.Select(e => new PalestraModel
                {
                    IdPalestra = e.id_palestra,
                    Nome = e.Evento.nome,
                    Descricao = e.Evento.descricao
                });
            }
            catch
            {
                return null;
            }
        }

        static public IEnumerable<MinicursoModel> SelectMinicursos()
        {
            try
            {
                var entities = new SemanaAcademicaEntities();
                return entities.Minicurso.Select(e => new MinicursoModel
                {
                    IdMinicurso = e.id_minicurso,
                    Nome = e.Evento.nome,
                    Descricao = e.Evento.descricao,
                    Vagas = e.vagas ?? 0
                });
            }
            catch
            {
                return null;
            }
        }

        static public IEnumerable<VisitaModel> SelectVisitas()
        {
            try
            {
                var entities = new SemanaAcademicaEntities();
                return entities.Visita.Select(e => new VisitaModel
                {
                    IdVisita = e.id_visita,
                    Nome = e.Evento.nome,
                    Descricao = e.Evento.descricao,
                    Vagas = e.vagas ?? 0,
                    Contribuicao = e.contribuicao,
                    Locomocao = e.locomocao
                });
            }
            catch
            {
                return null;
            }
        }

        static public bool InsertPalestra(PalestraModel model)
        {
            try
            {
                var entities = new SemanaAcademicaEntities();
                var palestra = new Palestra
                {
                    Evento = new Evento
                    {
                        nome = model.Nome,
                        descricao = model.Descricao
                    }
                };

                entities.Palestra.Add(palestra);
                entities.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        static public bool InsertMinicurso(MinicursoModel model)
        {
            try
            {
                var entities = new SemanaAcademicaEntities();
                var minicurso = new Minicurso
                {
                    vagas = model.Vagas,
                    Evento = new Evento
                    {
                        nome = model.Nome,
                        descricao = model.Descricao
                    }
                };

                entities.Minicurso.Add(minicurso);
                entities.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        static public bool InsertVisita(VisitaModel model)
        {
            try
            {
                var entities = new SemanaAcademicaEntities();
                var visita = new Visita
                {
                    contribuicao = model.Contribuicao,
                    vagas = model.Vagas,
                    locomocao = model.Locomocao,
                    Evento = new Evento
                    {
                        nome = model.Nome,
                        descricao = model.Descricao
                    }
                };

                entities.Visita.Add(visita);
                entities.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        static public bool InsertHorario(HorarioModel model)
        {
            try
            {
                var entities = new SemanaAcademicaEntities();
                var horario = new Horario
                {
                    id_evento = model.IdEvento,
                    hora_inicio = model.HoraInicio,
                    hora_fim = model.HoraFim,
                    id_local = model.IdLocal
                };

                entities.Horario.Add(horario);
                entities.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public static bool DeleteHorario(int id, out int idEvento)
        {
            idEvento = 0;
            try
            {
                var entities = new SemanaAcademicaEntities();
                var horario = entities.Horario.FirstOrDefault(h => h.id_horario == id);
                idEvento = horario.id_evento;
                entities.Horario.Remove(horario);
                entities.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public static bool UpdatePalestra(int id, PalestraModel model)
        {
            try
            {
                var entities = new SemanaAcademicaEntities();
                var palestra = entities.Palestra.FirstOrDefault(p => p.id_palestra == id);
                palestra.Evento.descricao = model.Descricao;
                palestra.Evento.nome = model.Nome;
                entities.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public static bool UpdateMinicurso(int id, MinicursoModel model)
        {
            try
            {
                var entities = new SemanaAcademicaEntities();
                var minicurso = entities.Minicurso.FirstOrDefault(m => m.id_minicurso == id);
                minicurso.Evento.descricao = model.Descricao;
                minicurso.Evento.nome = model.Nome;
                minicurso.vagas = model.Vagas;
                entities.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public static bool UpdateVisita(int id, VisitaModel model)
        {
            try
            {
                var entities = new SemanaAcademicaEntities();
                var visita = entities.Visita.FirstOrDefault(v => v.id_visita == id);
                visita.Evento.descricao = model.Descricao;
                visita.Evento.nome = model.Nome;
                visita.vagas = model.Vagas;
                visita.locomocao = model.Locomocao;
                visita.contribuicao = model.Contribuicao;
                entities.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }


        public static bool DeletePalestra(int id)
        {
            try
            {
                var entities = new SemanaAcademicaEntities();
                var palestra = entities.Palestra.FirstOrDefault(p => p.id_palestra == id);
                entities.Palestra.Remove(palestra);
                entities.Evento.Remove(palestra.Evento);
                entities.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public static bool DeleteMinicurso(int id)
        {
            try
            {
                var entities = new SemanaAcademicaEntities();
                var minicurso = entities.Minicurso.FirstOrDefault(m => m.id_minicurso == id);
                entities.Minicurso.Remove(minicurso);
                entities.Evento.Remove(minicurso.Evento);
                entities.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public static bool DeleteVisita(int id)
        {
            try
            {
                var entities = new SemanaAcademicaEntities();
                var visita = entities.Visita.FirstOrDefault(v => v.id_visita == id);
                entities.Visita.Remove(visita);
                entities.Evento.Remove(visita.Evento);
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