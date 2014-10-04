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
    public class MinicursoController : Controller
    {
        //
        // GET: /Admin/Minicurso/

        public ActionResult Index()
        {
            var minicursos = EventoBLL.SelectMinicursos().Select(p => new ListarMinicursoModel
            {
                Descricao = p.Descricao,
                IdMinicurso = p.IdMinicurso,
                Vagas = p.Vagas,
                Nome = p.Nome
            });
            return View(minicursos);
        }

        //
        // GET: /Admin/Minicurso/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Admin/Minicurso/Create

        [HttpPost]
        public ActionResult Create(CadastrarMinicursoModel model)
        {
            try
            {
                var success = EventoBLL.InsertMinicurso(new MinicursoModel
                {
                    Descricao = model.Descricao,
                    Nome = model.Nome,
                    Vagas = model.Vagas
                });
                if (success)
                    return RedirectToAction("Index");
                else
                {
                    ModelState.AddModelError("", "Falha criando minicurso!");
                    return View(model);
                }
            }
            catch
            {
                return View(model);
            }
        }

        //
        // GET: /Admin/Minicurso/Edit/5

        public ActionResult Edit(int id)
        {
            var minicurso = EventoBLL.SelectMinicursos().Select(m => new EditarMinicursoModel { Descricao = m.Descricao, IdMinicurso = m.IdMinicurso, Nome = m.Nome, Vagas = m.Vagas }).FirstOrDefault(m => m.IdMinicurso == id);
            return View(minicurso);
        }

        //
        // POST: /Admin/Minicurso/Edit/5

        [HttpPost]
        public ActionResult Edit(int id, EditarMinicursoModel model)
        {
            try
            {
                if (EventoBLL.UpdateMinicurso(id, new MinicursoModel { Descricao = model.Descricao, Nome = model.Nome, Vagas = model.Vagas }))
                    return RedirectToAction("Index");
            }
            catch
            {
            }
            ModelState.AddModelError("", "Erro atualizando minicurso.");
            return View(model);
        }

        //
        // GET: /Admin/Minicurso/Delete/5

        public ActionResult Delete(int id)
        {
            try
            {
                if (EventoBLL.DeleteMinicurso(id))
                    return RedirectToAction("Index");
            }
            catch
            {
            }
            ModelState.AddModelError("", "Erro excluindo minicurso. Referenciado em outro objeto?");
            return View();
        }
    }
}
