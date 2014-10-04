using SemanaAcademica.Areas.Admin.Models;
using SemanaAcademica.Models.DAL;
using SemanaAcademica.Models.ObjectModel;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SemanaAcademica.Models.BLL
{
    public static class HorarioBLL
    {
        static public IEnumerable<HorarioModel> SelectHorarios()
        {
            try
            {
                var entities = new SemanaAcademicaEntities();
                return entities.Horario.Select(e => new HorarioModel
                {
                    IdEvento = e.id_evento,
                    IdHorario = e.id_horario,
                    IdLocal = e.id_local,
                    HoraFim = e.hora_fim,
                    HoraInicio = e.hora_inicio
                });
            }
            catch
            {
                return null;
            }
        }
    }
}