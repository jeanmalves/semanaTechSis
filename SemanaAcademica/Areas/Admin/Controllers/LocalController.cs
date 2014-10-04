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
    public class LocalController : Controller
    {
        //
        // GET: /Admin/Local/

        public ActionResult Index()
        {
            var locais = LocalBLL.SelectLocais().Select(l => new ListarLocalModel
            {
                IdLocal = l.IdLocal,
                Descricao = l.Descricao
            });
            return View(locais);
        }

        //
        // POST: /Admin/Local/Create

        [HttpPost]
        public ActionResult Create(CadastrarLocalModel model)
        {
            try
            {
                if (LocalBLL.InsertLocal(new LocalModel { Descricao = model.Descricao }))
                    return RedirectToAction("Index");
            }
            catch
            {
            }

            ModelState.AddModelError("", "Erro salvando local. Tamanho ok?");
            return View(model);
        }

        //
        // GET: /Admin/Local/Edit/5

        public ActionResult Edit(int id)
        {
            var local = LocalBLL.SelectLocais().Select(l => new EditarLocalModel { Descricao = l.Descricao, IdLocal = l.IdLocal }).FirstOrDefault(l => l.IdLocal == id);
            return View(local);
        }

        //
        // POST: /Admin/Local/Edit/5

        [HttpPost]
        public ActionResult Edit(int id, EditarLocalModel model)
        {
            try
            {
                if (LocalBLL.UpdateLocal(id, model))

                    return RedirectToAction("Index");
            }
            catch
            {
            }
            ModelState.AddModelError("", "Erro atualizando local.");
            return View(model);
        }


        //
        // POST: /Admin/Local/Delete/5

        public ActionResult Delete(int id)
        {
            try
            {
                if (LocalBLL.DeleteLocal(id))
                    return RedirectToAction("Index");
            }
            catch
            {
            }
            ModelState.AddModelError("", "Erro excluindo local. Referenciado em outro objeto?");
            return View();
        }
    }
}
