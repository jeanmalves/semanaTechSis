using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SemanaAcademica.Controllers
{
    public class CertificadoController : Controller
    {
        //
        // GET: /Certificado/
        [Authorize]
        public ActionResult Index()
        {
            return View();
        }

        [Authorize]
        public ActionResult Palestras()
        {

            return View();
        }

        [Authorize]
        public ActionResult Minicursos()
        {

            return View();
        }

        [Authorize]
        public ActionResult Organizacao()
        {

            return View();
        }

        [Authorize]
        public ActionResult TrabalhoVoluntario()
        {
            return View();
        }

    }
}
