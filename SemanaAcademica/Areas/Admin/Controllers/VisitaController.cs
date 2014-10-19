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
    public class VisitaController : Controller
    {
        //
        // GET: /Admin/Visita/

        public ActionResult Index()
        {
            var visita = EventoBLL.SelectVisitas().Select(p => new ListarVisitaModel
            {
                IdEvento = p.IdEvento,
                Descricao = p.Descricao,
                IdVisita = p.IdVisita,
                Vagas = p.Vagas,
                Nome = p.Nome,
                Contribuicao = p.Contribuicao,
                Locomocao = p.Locomocao
            });
            return View(visita);
        }


        //
        // GET: /Admin/Visita/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Admin/Visita/Create

        [HttpPost]
        public ActionResult Create(CadastrarVisitaModel model)
        {
            try
            {
                var success = EventoBLL.InsertVisita(new VisitaModel
                {
                    Descricao = model.Descricao,
                    Nome = model.Nome,
                    Vagas = model.Vagas,
                    Contribuicao = model.Contribuicao,
                    Locomocao = model.Locomocao
                });
                if (success)
                    return RedirectToAction("Index");
                else
                {
                    ModelState.AddModelError("", "Falha criando visita!");
                    return View(model);
                }
            }
            catch
            {
                return View(model);
            }
        }

        //
        // GET: /Admin/Visita/Edit/5

        public ActionResult Edit(int id)
        {
            var visita = EventoBLL.SelectVisitas().Select(v => new EditarVisitaModel { Descricao = v.Descricao, Contribuicao = v.Contribuicao, IdVisita = v.IdVisita, Locomocao = v.Locomocao, Nome = v.Nome, Vagas = v.Vagas }).FirstOrDefault(v => v.IdVisita == id);
            return View(visita);
        }


        //
        // POST: /Admin/Visita/Edit/5

        [HttpPost]
        public ActionResult Edit(int id, EditarVisitaModel model)
        {
            try
            {
                if (EventoBLL.UpdateVisita(id, new VisitaModel { Descricao = model.Descricao, Nome = model.Nome, Vagas = model.Vagas, Contribuicao = model.Contribuicao, Locomocao = model.Locomocao }))
                    return RedirectToAction("Index");
            }
            catch
            {
            }
            ModelState.AddModelError("", "Erro atualizando visita.");
            return View(model);
        }

        //
        // GET: /Admin/Visita/Delete/5

        public ActionResult Delete(int id)
        {
            try
            {
                if (EventoBLL.DeleteVisita(id))
                    return RedirectToAction("Index");
            }
            catch
            {
            }
            ModelState.AddModelError("", "Erro excluindo visita. Referenciado em outro objeto?");
            return View();
        }
    }
}
