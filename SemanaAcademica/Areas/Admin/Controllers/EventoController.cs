using SemanaAcademica.Areas.Admin.Models;
using SemanaAcademica.Models.BLL;
using SemanaAcademica.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SemanaAcademica.Areas.Admin.Controllers
{
    [Administrador]
    public class EventoController : Controller
    {
        //
        // GET: /Admin/Evento/

        public ActionResult Index()
        {
            var eventos = EventoBLL.SelectEventos().Select(e => new ListarEventoModel
            {
                Descricao = e.Descricao,
                IdEvento = e.IdEvento,
                Nome = e.Nome
            });
            return View(eventos);
        }
    }
}
