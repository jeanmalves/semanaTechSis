using iTextSharp.text;
using iTextSharp.text.html.simpleparser;
using iTextSharp.text.pdf;
using SemanaAcademica.Models;
using SemanaAcademica.Models.BLL;
using SemanaAcademica.Models.ViewModel;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Web;
using System.Web.Mvc;

namespace SemanaAcademica.Controllers
{
    public class CadastroController : Controller
    {
        [Authorize]
        public void Certificado()
        {
            // Certificado para participações
            var participacoes = ParticipacaoBLL.GetMinicursosByIdPessoa(Usuario.SessionPersist.IdPessoa);

            var html = System.IO.File.ReadAllText(Server.MapPath("\\Content\\Templates\\Certificado.html"));

            var doc = new Document(PageSize.A4_LANDSCAPE.Rotate(), 0, 0, 0, 0);
            doc.AddAuthor("Semana Acadêmica de Eletrônica e Informática - UTFPR");
            doc.AddTitle("Certificado de Presença");

            var stream = new MemoryStream();

            var writer = PdfWriter.GetInstance(doc, stream);
            writer.CloseStream = false;

            doc.Open();


            var image = System.Drawing.Image.FromFile(Server.MapPath("\\Content\\Templates\\Fundo.jpg"));

            using (var g = System.Drawing.Graphics.FromImage(image))
            {
                g.DrawString("CERTIFICADO", new System.Drawing.Font("Arial", 20),
                new System.Drawing.SolidBrush(System.Drawing.Color.Black),
                new System.Drawing.Rectangle(140, 120, 730, 360),
                new System.Drawing.StringFormat { Alignment = System.Drawing.StringAlignment.Center, Trimming = System.Drawing.StringTrimming.Word }
                );

                g.DrawString(
                  String.Format(
                  ConfigurationManager.AppSettings["Certificado.Participante"].ToString(),
                  Usuario.SessionPersist.Nome,
                  Math.Ceiling
                  (
                      (double)participacoes
                      .Where(p => p.HoraEntrada.HasValue && p.HoraSaida.HasValue)
                      .Sum(p => (p.HoraSaida.Value - p.HoraEntrada.Value).TotalMinutes) / 60
                  )),
                  new System.Drawing.Font("Arial", 12),
                  new System.Drawing.SolidBrush(System.Drawing.Color.Black),
                  new System.Drawing.Rectangle(140, 190, 730, 360),
                  new System.Drawing.StringFormat { Alignment = System.Drawing.StringAlignment.Center, Trimming = System.Drawing.StringTrimming.Word }
                  );

            }

            var pdfImage = Image.GetInstance(image, new BaseColor(0, 0, 0));
            pdfImage.ScaleToFit(PageSize.A4_LANDSCAPE.Rotate());

            doc.Add(pdfImage);

            // Certificado de colaborador
            if (Usuario.SessionPersist.IsColaborador)
            {
                image = System.Drawing.Image.FromFile(Server.MapPath("\\Content\\Templates\\Fundo.jpg"));

                using (var g = System.Drawing.Graphics.FromImage(image))
                {
                    g.DrawString("CERTIFICADO", new System.Drawing.Font("Arial", 20),
                    new System.Drawing.SolidBrush(System.Drawing.Color.Black),
                    new System.Drawing.Rectangle(140, 120, 730, 360),
                    new System.Drawing.StringFormat { Alignment = System.Drawing.StringAlignment.Center, Trimming = System.Drawing.StringTrimming.Word }
                    );

                    g.DrawString(
                      String.Format(
                      ConfigurationManager.AppSettings["Certificado.Colaborador"].ToString(),
                      Usuario.SessionPersist.Nome),
                      new System.Drawing.Font("Arial", 12),
                      new System.Drawing.SolidBrush(System.Drawing.Color.Black),
                      new System.Drawing.Rectangle(140, 190, 730, 360),
                      new System.Drawing.StringFormat { Alignment = System.Drawing.StringAlignment.Center, Trimming = System.Drawing.StringTrimming.Word }
                      );

                }

                pdfImage = Image.GetInstance(image, new BaseColor(0, 0, 0));
                pdfImage.ScaleToFit(PageSize.A4_LANDSCAPE.Rotate());
                doc.Add(pdfImage);

            }

            if (Usuario.SessionPersist.IsAdministrador)
            {
                image = System.Drawing.Image.FromFile(Server.MapPath("\\Content\\Templates\\Fundo.jpg"));

                using (var g = System.Drawing.Graphics.FromImage(image))
                {
                    g.DrawString("CERTIFICADO", new System.Drawing.Font("Arial", 20),
                    new System.Drawing.SolidBrush(System.Drawing.Color.Black),
                    new System.Drawing.Rectangle(140, 120, 730, 360),
                    new System.Drawing.StringFormat { Alignment = System.Drawing.StringAlignment.Center, Trimming = System.Drawing.StringTrimming.Word }
                    );

                    g.DrawString(
                      String.Format(
                      ConfigurationManager.AppSettings["Certificado.Administrador"].ToString(),
                      Usuario.SessionPersist.Nome),
                      new System.Drawing.Font("Arial", 12),
                      new System.Drawing.SolidBrush(System.Drawing.Color.Black),
                      new System.Drawing.Rectangle(140, 190, 730, 360),
                      new System.Drawing.StringFormat { Alignment = System.Drawing.StringAlignment.Center, Trimming = System.Drawing.StringTrimming.Word }
                      );

                }

                pdfImage = Image.GetInstance(image, new BaseColor(0, 0, 0));
                pdfImage.ScaleToFit(PageSize.A4_LANDSCAPE.Rotate());
                doc.Add(pdfImage);

            }
            doc.Close();


            Response.ClearContent();
            Response.ClearHeaders();
            Response.AddHeader("Content-Disposition", "inline;filename=Certificado.pdf");
            Response.ContentType = "application/pdf";

            stream.Seek(0, SeekOrigin.Begin);
            stream.CopyTo(Response.OutputStream);

            Response.Flush();
            Response.Clear();
        }

        public ActionResult RecuperarSenha()
        {
            return View();
        }

        [HttpPost]
        public ActionResult RecuperarSenha(string email)
        {
            try
            {
                if (String.IsNullOrEmpty(email))
                {
                    ModelState.AddModelError("email", "Email não pode ser nulo!");
                    return View();
                }

                var pessoa = PessoaBLL.SelectPessoaByEmail(email);

                if (pessoa == null)
                {
                    ModelState.AddModelError("email", "Email não encontrado!");
                    return View();
                }

                var ticks = DateTime.Now.AddDays(1).Ticks;
                var qs = String.Format("e={0}&t={1}&h={2}",
                    email,
                    ticks,
                    Convert.ToBase64String(new MD5CryptoServiceProvider().ComputeHash(System.Text.ASCIIEncoding.ASCII.GetBytes("SemanaAcademica_" + pessoa.IdPessoa + pessoa.Chave + ticks))).Replace('+', '-').Replace('/', '_')
                    );


                new Utils.Email().Send(pessoa.Email, "Redefinição de senha", "Olá, " + pessoa.Nome + "\n\nRecebemos uma solicitação de redefinição de senha para seu cadastro. Caso não tenha sido efetuada por você, ignore. Caso contrário, clique neste link em até 24 horas: http://" + Request.Url.Host + "/Cadastro/RedefinirSenha?" + qs);

                return View("RecuperarSenhaEmailSucesso");
            }
            catch
            {
                ModelState.AddModelError("", "Erro desconhecido!");
                return View();
            }
        }

        public ActionResult RedefinirSenha(string t, string e, string h)
        {
            try
            {
                var pessoa = PessoaBLL.SelectPessoaByEmail(e);

                var hash = Convert.ToBase64String(new MD5CryptoServiceProvider().ComputeHash(System.Text.ASCIIEncoding.ASCII.GetBytes("SemanaAcademica_" + pessoa.IdPessoa + pessoa.Chave + t))).Replace('+', '-').Replace('/', '_');

                if (hash != h)
                    return View("RedefinirSenhaLinkErro");

                var date = new DateTime(long.Parse(t));
                if (DateTime.Now > date)
                    return View("RedefinirSenhaLinkErro");

                return View(new RedefinirSenhaViewModel
                {
                    e = e,
                    h = h,
                    t = t
                });
            }
            catch
            {
                return View("RedefinirSenhaLinkErro");
            }

        }

        [HttpPost]
        public ActionResult RedefinirSenha(RedefinirSenhaViewModel model)
        {
            try
            {
                if (!ModelState.IsValid)
                    return View();

                var pessoa = PessoaBLL.SelectPessoaByEmail(model.e);

                var hash = Convert.ToBase64String(new MD5CryptoServiceProvider().ComputeHash(System.Text.ASCIIEncoding.ASCII.GetBytes("SemanaAcademica_" + pessoa.IdPessoa + pessoa.Chave + model.t))).Replace('+', '-').Replace('/', '_');

                if (hash != model.h)
                    return View("RedefinirSenhaLinkErro");

                var date = new DateTime(long.Parse(model.t));
                if (DateTime.Now > date)
                    return View("RedefinirSenhaLinkErro");


                if (!PessoaBLL.UpdateSenha(pessoa.IdPessoa, model.Senha))
                {
                    ModelState.AddModelError("", "Erro desconhecido!");
                    return View();
                }

                return View("RedefinirSenhaSucesso");
            }
            catch
            {
                return View("RedefinirSenhaLinkErro");
            }
        }


        public ActionResult Entrar()
        {
            return View(new EntrarViewModel());
        }

        [HttpPost]
        public ActionResult Entrar(EntrarViewModel model)
        {
            var pessoa = PessoaBLL.SelectPessoaByEmail(model.Email);
            if (pessoa == null)
            {
                ModelState.AddModelError("", "E-mail não encontrado!");
                return View(model);
            }
            if (!pessoa.Confirmado)
            {
                ModelState.AddModelError("", "Seu cadastro não foi confirmado pelo link enviado ao seu e-mail. O link foi enviado novamente. Verifique!");
                new Utils.Email().Send(pessoa.Email, "Confirmação de cadastro", "Por favor, utilize este link para confirmar seu cadastro: http://" + Request.Url.Host + "/Cadastro/Confirmacao/" + pessoa.Chave.ToString());
                return View(model);
            }
            if (pessoa.Senha == System.Text.ASCIIEncoding.ASCII.GetString(new System.Security.Cryptography.MD5CryptoServiceProvider().ComputeHash(System.Text.ASCIIEncoding.ASCII.GetBytes(model.Senha))))
            {
                System.Web.Security.FormsAuthentication.SetAuthCookie(pessoa.Email, false);
                return RedirectToAction("Index", "Home");
            }
            else
            {
                ModelState.AddModelError("", "Dados não conferem!");
                return View(model);
            }
        }

        public ActionResult Sair()
        {
            HttpContext.Session.Clear();
            System.Web.Security.FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Home");
        }

        //
        // GET: /Cadastro/Participante

        public ActionResult Participante()
        {
            return View(new CadastroParticipanteViewModel());
        }

        //
        // POST: /Cadastro/Participante
        [HttpPost]
        public ActionResult Participante(CadastroParticipanteViewModel model)
        {
            if (ModelState.IsValid)
            {
<<<<<<< HEAD
=======
                if (model.Curso != null)
                {
                    if (model.Matricula == true)
                    {
                        model.Curso = model.ListaCurso[Convert.ToInt32(model.Curso)];
                    }
                }

>>>>>>> 8f24a3c... Adicionei jquery pra habilitar e desabilitar campo quando matricula habilitada
                var pessoa = PessoaBLL.SelectPessoaByEmail(model.Email);
                if (pessoa == null)
                {
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
                    new Utils.Email().Send(pessoa.Email, "Confirmação de cadastro", "Por favor, utilize este link para confirmar seu cadastro: http://" + Request.Url.Host + "/Cadastro/Confirmacao/" + pessoa.Chave.ToString());
                    return View("Confirmar");
                }
                else
                {
                    ModelState.AddModelError("", "Erro cadastrando participante! Já está cadastrado?");
                    return View(model);
                }
            }
            else return View(model);
        }

        public ActionResult Confirmacao(string id)
        {
            bool success;
            try
            {
                success = PessoaBLL.ConfirmaPessoa(Guid.Parse(id));
            }
            catch
            {
                success = false;
            }
            return View(success);
        }
    }
}
