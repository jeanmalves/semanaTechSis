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
    public class ParticipanteController : Controller
    {
        //
        // GET: /Admin/Participante/

        public ActionResult Index(string filtro, int? pag)
        {
            int total = 0;
            var participantes = ParticipanteBLL.SelectParticipantes(filtro, pag ?? 1, out total).Select(p => new ListarParticipanteModel
            {
                Confirmado = p.Confirmado,
                Email = p.Email,
                IdParticipante = p.IdParticipante,
                Matricula = p.Matricula,
                Nome = p.Nome,
                Registro = p.Registro,
                Telefone = p.Telefone,
                Contribuicao = p.Contribuicao,
                Colaborador = ColaboradorBLL.SelectColaboradorByIdPessoa(p.IdPessoa) != null
            });
            ViewBag.Paginas = total / 10 + (total % 10 != 0 ? 1 : 0);
            return View(participantes);
        }


        public ActionResult Edit(int id)
        {
            var model = ParticipanteBLL.SelectParticipantes().Where(p => p.IdParticipante == id).Select(p => new EditarParticipanteModel
            {
                Nome = p.Nome,
                Email = p.Email,
                Matricula = p.Matricula,
                Registro = p.Registro
            }).First();
            return View(model);
        }


        [HttpPost]
        public ActionResult Edit(int id, EditarParticipanteModel model)
        {
            if (ModelState.IsValid)
            {
                if (ParticipanteBLL.UpdateParticipante(new ParticipanteModel
                {
                    IdParticipante = id,
                    Email = model.Email,
                    Matricula = model.Matricula,
                    Nome = model.Nome,
                    Registro = model.Registro
                }))
                    return RedirectToAction("Index");
            }
            ModelState.AddModelError("", "Erro alterando participante!");
            return View(model);
        }

        //
        // GET: /Admin/Participante/Create

        public ActionResult Create()
        {
            return View();
        }
        //
        // POST: /Admin/Participante/Create

        [HttpPost]
        public ActionResult Create(CadastrarParticipanteModel model)
        {
            if (ModelState.IsValid)
            {
                var pessoa = PessoaBLL.SelectPessoaByEmail(model.Email);
                if (pessoa == null)
                {
                    //senha randomica
                    var chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
                    var random = new Random();
                    var result = new string(
                        Enumerable.Repeat(chars, 8)
                                  .Select(s => s[random.Next(s.Length)])
                                  .ToArray());

                    model.Senha = result;
                    model.Telefone = String.Empty;

                    if (PessoaBLL.InsertPessoa(model))
                    {
                        pessoa = PessoaBLL.SelectPessoaByEmail(model.Email);
                    }
                    else
                    {
                        ModelState.AddModelError("", "Erro no cadastro. Por favor, tente novamente mais tarde!");
                        return View(model);
                    }
                }
                if (pessoa.IdPessoa == 0)
                {
                    ModelState.AddModelError("", "Erro no cadastro. Por favor, tente novamente mais tarde!");
                    return View(model);
                }
                model.IdPessoa = pessoa.IdPessoa;
                if (ParticipanteBLL.InsertParticipante(model))
                {
                    new Utils.Email().Send(pessoa.Email, "Confirmação de cadastro", "Olá!\n\nVocê foi cadastrado no sistema da Semana Academica de Eletrônica e Informática por um administrador do sistema.\n\n Por favor, utilize este link para confirmar seu cadastro: http://" + Request.Url.Host + "/Cadastro/Confirmacao/" + pessoa.Chave.ToString() + "\n\nApós a confirmação, você poderá logar no sistema utilizando seu e-mail e a senha gerada automaticamente: " + model.Senha);
                    return View("Success");
                }
                else
                {
                    ModelState.AddModelError("", "Erro cadastrando participante! Já está cadastrado?");
                    return View(model);
                }
            }
            else return View(model);
        }

        public ActionResult Success()
        {
            return View();
        }

        public JsonResult Contribuicao(int id, bool status)
        {
            return new JsonResult { JsonRequestBehavior = JsonRequestBehavior.AllowGet, Data = (ParticipanteBLL.UpdateContribuicao(id, status) ? (status ? "Entregue!" : "Não entregue!") : "Erro!") };
        }

        [Administrador]
        public JsonResult Colaborador(int id, bool status)
        {
            var participante = ParticipanteBLL.SelectParticipantes().FirstOrDefault(p => p.IdParticipante == id);
            string mensagem = String.Empty;
            if (status)
            {
                mensagem = ColaboradorBLL.InsertColaborador(participante.IdPessoa) ? "Adicionado!" : "Erro!";
            }
            else
            {
                mensagem = ColaboradorBLL.DeleteColaborador(participante.IdPessoa) ? "Removido!" : "Erro!";
            }
            return new JsonResult { JsonRequestBehavior = JsonRequestBehavior.AllowGet, Data = mensagem };
        }

        public ActionResult Presenca(int id)
        {
            var participante = ParticipanteBLL.SelectParticipantes().FirstOrDefault(p => p.IdParticipante == id);
            ViewBag.NomeParticipante = participante.Nome;

            var model = ParticipacaoBLL.SelectByIdParticipante(id) ?? new List<ParticipacaoModel>();
            return View(model.Select(p => new PresencaParticipanteModel
            {
                idParticipacao = p.idParticipacao,
                idParticipante = id,
                HoraEntrada = p.HoraEntrada,
                HoraSaida = p.HoraSaida,
                idEvento = p.idEvento,
                NomeEvento = p.NomeEvento
            }));

        }
    }
}
