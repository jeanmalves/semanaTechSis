using Rotativa;
using Rotativa.Options;
using SemanaAcademica.Models.BLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Mvc;

namespace SemanaAcademica.Areas.Admin.Controllers
{
    public class RelatorioController : Controller
    {
        public ActionResult listarPresenca(int id)
        {
            var evento = EventoBLL.SelectEvento(id);
            ViewBag.nomeEvento = evento.Nome;
            ViewBag.descricao = "Lista de Presença - Palestra: " + evento.Nome;

            var listaPresenca = ParticipacaoBLL.listarParticipacao(id);
            var pdf = new ViewAsPdf
            {
                Model = listaPresenca,
                ViewName = "ListarPresenca",
                MasterName = "_Relatorio",
                FileName = "listaPresenca-" + evento.Nome+".pdf",
                PageSize = Size.A4,
                IsGrayScale = true,
                PageMargins = new Margins
                {
                    Bottom = 5,
                    Left = 5,
                    Right = 5,
                    Top = 5
                }
            };
            return pdf;
            // return new ViewAsPdf("ListarPresenca", "_Relatorio", listaPresenca);
        }
    }
}
