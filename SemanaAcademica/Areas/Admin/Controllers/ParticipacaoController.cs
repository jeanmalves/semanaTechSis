using PagedList;
using Rotativa;
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
    [Colaborador]
    public class ParticipacaoController : Controller
    {
        //
        // GET: /Admin/Participacao/

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

        public ActionResult Create(int id)
        {
            ViewBag.Eventos = EventoBLL.SelectEventos().Select(e => new ListarEventoModel
            {
                Descricao = e.Descricao,
                IdEvento = e.IdEvento,
                Nome = e.Nome
            });
            return View();
        }

        [HttpPost]
        public ActionResult Create(int id, CriarParticipacaoModel model)
        {

            if (ParticipacaoBLL.InsertParticipacao(new ParticipacaoModel { HoraEntrada = model.HoraInicio, HoraSaida = model.HoraFim, idEvento = model.idEvento, idParticipante = id }))
                return RedirectToAction("Presenca", "Participante", new { id = id });
            else
            {
                ModelState.AddModelError("", "Erro adicionando participação!");
                return View(model);
            }
        }


        //
        // GET: /Admin/Participacao/Registrar/5

        public ActionResult Registrar(int id)
        {
            var evento = EventoBLL.SelectEventos().FirstOrDefault(e => e.IdEvento == id);
            var horarios = HorarioBLL.SelectHorarios().Where(h => h.IdEvento == id);
            var model = new RegistrarParticipacaoModel
            {
                idEvento = id,
                NomeEvento = evento.Nome,
                HoraInicio = horarios.Select(h => h.HoraInicio).ToArray(),
                HoraFim = horarios.Select(h => h.HoraFim).ToArray(),
            };
            return View(model);
        }


        [HttpPost]
        public ActionResult Registrar(int id, string registro)
        {


            var evento = EventoBLL.SelectEventos().FirstOrDefault(e => e.IdEvento == id);
            var horarios = HorarioBLL.SelectHorarios().Where(h => h.IdEvento == id);
            var model = new RegistrarParticipacaoModel
            {
                idEvento = id,
                NomeEvento = evento.Nome,
                HoraInicio = horarios.Select(h => h.HoraInicio).ToArray(),
                HoraFim = horarios.Select(h => h.HoraFim).ToArray(),
            };


            if (String.IsNullOrEmpty(registro))
            {
                ModelState.AddModelError("", "Registro nulo?");
                return View(model);
            }

            registro = registro.Trim();

            int id_participante = 0;
            try
            {
                var participante = ParticipanteBLL.SelectParticipantes().FirstOrDefault(p => p.Registro.Trim() == registro || p.Registro.Trim() == String.Concat(registro.SkipWhile(r => r == '0')));
                id_participante = participante.IdParticipante;
                model.UltimaParticipacaoNome = participante.Nome;
                model.UltimaParticipacaoRegistro = participante.Registro;
            }
            catch
            {
                ModelState.AddModelError("", "Participante não registrado!");
                return View(model);
            }

            var sentido = SemanaAcademica.Models.ObjectModel.ParticipacaoSentido.Entrada;
            if (!ParticipacaoBLL.RegistrarParticipacao(id_participante, id, out sentido))
            {
                ModelState.AddModelError("", "Erro gravando participação!");
                return View(model);
            }

            model.Sentido = sentido == SemanaAcademica.Models.ObjectModel.ParticipacaoSentido.Entrada ? "ENTRADA" : "SAÍDA";
            return View(model);
        }

        public ActionResult Delete(int id)
        {
            int idParticipante = 0;
            if (ParticipacaoBLL.DeleteParticipacao(id, out idParticipante))
                return RedirectToAction("Presenca", "Participante", new { id = idParticipante });
            else
            {
                ModelState.AddModelError("", "Erro excluindo participacao...");
                return RedirectToAction("Index", "Participante");
            }
        }

        public ActionResult Edit(int id)
        {
            var model = ParticipacaoBLL.SelectParticipacao(id);
            return View(new EditarParticipacaoModel { HoraFim = model.HoraSaida, HoraInicio = model.HoraEntrada, NomeEvento = model.NomeEvento, NomeParticipante = model.NomeParticipante });
        }

        [HttpPost]
        public ActionResult Edit(int id, EditarParticipacaoModel model)
        {
            int idParticipante = 0;
            if (ParticipacaoBLL.UpdateParticipacao(id, new ParticipacaoModel { HoraEntrada = model.HoraInicio, HoraSaida = model.HoraFim }, out idParticipante))
            {
                return RedirectToAction("Presenca", "Participante", new { id = idParticipante });
            }
            else
            {
                ModelState.AddModelError("", "Erro editando participacao...");
                return View(model);
            }
        }

        public ActionResult TrabalhoVoluntario()
        {
            return View();
        }

        public ActionResult Listar(int id, int? pagina)
        {
            var evento = EventoBLL.SelectEvento(id);
            ViewBag.nomeEvento = evento.Nome;
            ViewBag.idEvento = evento.IdEvento;
            var listaPresenca = ParticipacaoBLL.listarParticipacao(id);

            int paginaTamanho = 4;
            int paginaNumero = (pagina ?? 1);

            return View(listaPresenca.ToPagedList(paginaNumero, paginaTamanho));
        }
    }
}
