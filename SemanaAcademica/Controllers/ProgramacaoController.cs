using SemanaAcademica.Models.BLL;
using SemanaAcademica.Models.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SemanaAcademica.Controllers
{
    public class ProgramacaoController : Controller
    {
        //
        // GET: /Programacao/

        public ActionResult Index(int? id)
        {
            var model = new IndexProgramacaoViewModel
            {
                Eventos = EventoBLL.SelectEventos().Select(e => new EventoProgramacaoViewModel
                {
                    Descricao = e.Descricao,
                    IdEvento = e.IdEvento,
                    Nome = e.Nome
                }),
                Horarios = id.HasValue ? HorarioBLL.SelectHorarios().Where(e => e.IdLocal == id.Value).Select(h => new HorarioProgramacaoViewModel
                {
                    HoraFim = h.HoraFim,
                    HoraInicio = h.HoraInicio,
                    IdEvento = h.IdEvento,
                    IdHorario = h.IdHorario,
                    IdLocal = h.IdLocal
                }) : HorarioBLL.SelectHorarios().Select(h => new HorarioProgramacaoViewModel
                {
                    HoraFim = h.HoraFim,
                    HoraInicio = h.HoraInicio,
                    IdEvento = h.IdEvento,
                    IdHorario = h.IdHorario,
                    IdLocal = h.IdLocal
                }),
                Locais = LocalBLL.SelectLocais().Select(l => new LocalProgramacaoViewModel
                {
                    IdLocal = l.IdLocal,
                    Nome = l.Descricao
                })
            };

            return View(model);
        }


    }
}
