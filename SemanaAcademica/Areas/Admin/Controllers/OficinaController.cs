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
            return View();
        }

    }
}
