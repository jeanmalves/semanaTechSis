using SemanaAcademica.Areas.Admin.Models;
using SemanaAcademica.Models.BLL;
using SemanaAcademica.Models.DAL;
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
    public class PalestraController : Controller
    {
        //
        // GET: /Admin/Palestra/

        public ActionResult Index()
        {
            var palestras = EventoBLL.SelectPalestras();
            return View(palestras);
        }

        //
        // GET: /Admin/Palestra/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Admin/Palestra/Create

        [HttpPost]
        public ActionResult Create(CadastrarPalestraModel model)
        {
            try
            {
                var success = EventoBLL.InsertPalestra(new PalestraModel
                {
                    Descricao = model.Descricao,
                    Nome = model.Nome
                });
                if (success)
                    return RedirectToAction("Index");
                else
                {
                    ModelState.AddModelError("", "Falha criando palestra!");
                    return View(model);
                }
            }
            catch
            {
                return View(model);
            }
        }

        //
        // GET: /Admin/Palestra/Edit/5

        public ActionResult Edit(int id)
        {
            if(id > 0)
            {
                var palestra = EventoBLL.selectPalestra(id);
                if(palestra != null)
                    return View(palestra);
            }
            
            return RedirectToAction("Index");
        }

        //
        // POST: /Admin/Palestra/Edit/5

        [HttpPost]
        public ActionResult Edit(Palestra palestra)
        {
            try
            {
                if (EventoBLL.UpdatePalestra(palestra))
                    return RedirectToAction("Index");
            }
            catch
            {
            }
            ModelState.AddModelError("", "Erro atualizando palestra."); 
            return View(palestra);
        }

        //
        // GET: /Admin/Palestra/Delete/5

        public ActionResult Delete(int id)
        {
            try
            {
                if (EventoBLL.DeletePalestra(id))
                    return RedirectToAction("Index");
            }
            catch
            {
            }
            ModelState.AddModelError("", "Erro excluindo palestra. Referenciado em outro objeto?");
            return View();
        }
    }
}
