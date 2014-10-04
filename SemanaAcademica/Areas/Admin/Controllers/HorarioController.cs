using SemanaAcademica.Areas.Admin.Models;
using SemanaAcademica.Models.BLL;
using SemanaAcademica.Models.ObjectModel;
using SemanaAcademica.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SemanaAcademica.Areas.Admin.Controllers
{
    [Administrador]
    public class HorarioController : Controller
    {
        //
        // GET: /Horario/Index/1

        public ActionResult Index(int id)
        {

            var horarios = EventoBLL.SelectHorarios(id).Select(h => new ListarHorarioModel
            {
                idHorario = h.IdHorario,
                HoraInicio = h.HoraInicio,
                Horafim = h.HoraFim,
                Local = LocalBLL.SelectLocais().FirstOrDefault(l => l.IdLocal == h.IdLocal).Descricao
            });

            ViewBag.Locais = LocalBLL.SelectLocais().Select(l => new ListarLocalModel
            {
                IdLocal = l.IdLocal,
                Descricao = l.Descricao
            });

            ViewBag.Evento = EventoBLL.SelectEventos().FirstOrDefault(e => e.IdEvento == id).Nome;

            return View(horarios);
        }


        //
        // POST: /Admin/Horario/Create

        [HttpPost]
        public ActionResult Create(CadastrarHorarioModel model)
        {
            try
            {
                if (EventoBLL.InsertHorario(new HorarioModel { IdEvento = model.idEvento, HoraInicio = model.HoraInicio, HoraFim = model.Horafim, IdLocal = model.idLocal }))
                    return RedirectToAction("Index", new { id = model.idEvento });
            }
            catch
            {
            }

            ModelState.AddModelError("", "Erro salvando horário. Está em formato correto?");
            return View(model);
        }


        //
        // GET: /Admin/Horario/Delete/5

        public ActionResult Delete(int id)
        {
            try
            {
                int idEvento = 0;
                if (EventoBLL.DeleteHorario(id, out idEvento))
                    return RedirectToAction("Horario", new { id = idEvento });
            }
            catch
            {
            }
            ModelState.AddModelError("", "Erro excluindo horário. Referenciado em outro objeto?");
            return View();
        }
    }
}
