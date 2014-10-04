using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SemanaAcademica.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Message = "Bem vindo!";

            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Este software foi desenvolvido por André Mansur para auxliar na gestão das Semanas Acadêmicas da Universidade Tecnológica Federal do Paraná.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "André Mansur: projects at amansur dot com";

            return View();
        }
    }
}
