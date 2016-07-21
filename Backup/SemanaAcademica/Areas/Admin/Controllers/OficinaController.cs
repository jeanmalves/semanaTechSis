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
    public class OficinaController : Controller
    {
        //
        // GET: /Admin/Oficina/

        public ActionResult Index()
        {
            var Oficinas = EventoBLL.SelectOficinas().Select(p => new ListarOficinaModel
            {
                IdEvento = p.IdEvento,
                Descricao = p.Descricao,
                IdOficina = p.IdOficina,
                Vagas = p.Vagas,
                Nome = p.Nome
            });
            return View(Oficinas);
        }

        //
        // GET: /Admin/Oficina/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Admin/Oficina/Create

        [HttpPost]
        public ActionResult Create(CadastrarOficinaModel model)
        {
            try
            {
                var success = EventoBLL.InsertOficina(new OficinaModel
                {
                    Descricao = model.Descricao,
                    Nome = model.Nome,
                    Vagas = model.Vagas
                });
                if (success)
                    return RedirectToAction("Index");
                else
                {
                    ModelState.AddModelError("", "Falha criando Oficina!");
                    return View(model);
                }
            }
            catch
            {
                return View(model);
            }
        }

        //
        // GET: /Admin/Oficina/Edit/5

        public ActionResult Edit(int id)
        {
            var Oficina = EventoBLL.SelectOficinas().Select(m => new EditarOficinaModel { Descricao = m.Descricao, IdOficina = m.IdOficina, Nome = m.Nome, Vagas = m.Vagas }).FirstOrDefault(m => m.IdOficina == id);
            return View(Oficina);
        }

        //
        // POST: /Admin/Oficina/Edit/5

        [HttpPost]
        public ActionResult Edit(int id, EditarOficinaModel model)
        {
            try
            {
                if (EventoBLL.UpdateOficina(id, new OficinaModel { Descricao = model.Descricao, Nome = model.Nome, Vagas = model.Vagas }))
                    return RedirectToAction("Index");
            }
            catch
            {
            }
            ModelState.AddModelError("", "Erro atualizando Oficina.");
            return View(model);
        }

        //
        // GET: /Admin/Oficina/Delete/5

        public ActionResult Delete(int id)
        {
            try
            {
                if (EventoBLL.DeleteOficina(id))
                    return RedirectToAction("Index");
            }
            catch
            {
            }
            ModelState.AddModelError("", "Erro excluindo Oficina. Referenciado em outro objeto?");
            return View();
        }

    }
}
