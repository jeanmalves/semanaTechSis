using iTextSharp.text;
using iTextSharp.text.pdf;
using SemanaAcademica.Models.BLL;
using SemanaAcademica.Models.ObjectModel;
using SemanaAcademica.Models.ViewModel;
using System;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Web.Mvc;

namespace SemanaAcademica.Controllers
{
    public class CertificadosController : Controller
    {
        //
        // GET: /Certificado/
        [Authorize]
        public ActionResult Index()
        {
            CertificadosViewModel model = CertificadosBLL.CheckCertificados();

            return View(model);
        }

        //
        // GET: /Certificado/Palestras
        [Authorize]
        public void Palestras()
        {
            // Gera apenas um certificado
            var participacoes = ParticipacaoBLL.GetPalestrasByIdPessoa(Usuario.SessionPersist.IdPessoa);

            // Cria documento
            var html = System.IO.File.ReadAllText(Server.MapPath("\\Content\\Templates\\Certificado.html"));

            var doc = new Document(PageSize.A4_LANDSCAPE.Rotate(), 0, 0, 0, 0);
            doc.AddAuthor("Semana Acadêmica de Eletrônica e Informática - UTFPR");
            doc.AddTitle("Certificado de Presença");

            var stream = new MemoryStream();

            var writer = PdfWriter.GetInstance(doc, stream);
            writer.CloseStream = false;

            doc.Open();

            // Imagem do certificado
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
                  ConfigurationManager.AppSettings["Certificado.Palestra"].ToString(),
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

        //
        // GET: /Certificado/Palestras
        [Authorize]
        public void Minicursos()
        {
            // Recupera participações em minicursos
            var minicursos = ParticipacaoBLL.GetMinicursosByIdPessoa(Usuario.SessionPersist.IdPessoa);

            // Cria documento
            var html = System.IO.File.ReadAllText(Server.MapPath("\\Content\\Templates\\Certificado.html"));
            var doc = new Document(PageSize.A4_LANDSCAPE.Rotate(), 0, 0, 0, 0);
            doc.AddAuthor("Semana Acadêmica de Eletrônica e Informática - UTFPR");
            doc.AddTitle("Certificado de Presença");

            var stream = new MemoryStream();

            var writer = PdfWriter.GetInstance(doc, stream);
            writer.CloseStream = false;

            doc.Open();

            // Edita imagem

            foreach (ParticipacaoModel p in minicursos)
            {
                var image = System.Drawing.Image.FromFile(Server.MapPath("\\Content\\Templates\\Fundo.jpg"));

                using (var g = System.Drawing.Graphics.FromImage(image))
                {
                    g.DrawString("CERTIFICADO", new System.Drawing.Font("Arial", 20),
                    new System.Drawing.SolidBrush(System.Drawing.Color.Black),
                    new System.Drawing.Rectangle(140, 120, 730, 360),
                    new System.Drawing.StringFormat { Alignment = System.Drawing.StringAlignment.Center, Trimming = System.Drawing.StringTrimming.Word }
                    );

                    // Adiciona parametros
                    g.DrawString(
                      String.Format(
                      ConfigurationManager.AppSettings["Certificado.Minicurso"].ToString(),
                      Usuario.SessionPersist.Nome,
                      p.NomeEvento,
                      Math.Ceiling((double)(p.HoraSaida.Value - p.HoraEntrada.Value).TotalMinutes / 60)),
                      new System.Drawing.Font("Arial", 12),
                      new System.Drawing.SolidBrush(System.Drawing.Color.Black),
                      new System.Drawing.Rectangle(140, 190, 730, 360),
                      new System.Drawing.StringFormat { Alignment = System.Drawing.StringAlignment.Center, Trimming = System.Drawing.StringTrimming.Word }
                      );
                }

                var pdfImage = Image.GetInstance(image, new BaseColor(0, 0, 0));
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

        [Authorize]
        public void Colaborador()
        {
            // Cria documento
            var html = System.IO.File.ReadAllText(Server.MapPath("\\Content\\Templates\\Certificado.html"));
            var doc = new Document(PageSize.A4_LANDSCAPE.Rotate(), 0, 0, 0, 0);
            doc.AddAuthor("Semana Acadêmica de Eletrônica e Informática - UTFPR");
            doc.AddTitle("Certificado de Colaborador");

            var stream = new MemoryStream();

            var writer = PdfWriter.GetInstance(doc, stream);
            writer.CloseStream = false;

            doc.Open();

            var image = System.Drawing.Image.FromFile(Server.MapPath("\\Content\\Templates\\Fundo.jpg"));

            // Certificado de colaborador
            if (Usuario.SessionPersist.IsColaborador)
            {
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
            }

            var pdfImage = Image.GetInstance(image, new BaseColor(0, 0, 0));
            pdfImage.ScaleToFit(PageSize.A4_LANDSCAPE.Rotate());
            doc.Add(pdfImage);
            doc.Close();

            Response.ClearContent();
            Response.ClearHeaders();
            Response.AddHeader("Content-Disposition", "inline;filename=CertificadoTrabalhoVoluntario.pdf");
            Response.ContentType = "application/pdf";

            stream.Seek(0, SeekOrigin.Begin);
            stream.CopyTo(Response.OutputStream);

            Response.Flush();
            Response.Clear();
        }

        [Authorize]
        public void Administrador()
        {
            // Cria documento
            var html = System.IO.File.ReadAllText(Server.MapPath("\\Content\\Templates\\Certificado.html"));
            var doc = new Document(PageSize.A4_LANDSCAPE.Rotate(), 0, 0, 0, 0);
            doc.AddAuthor("Semana Acadêmica de Eletrônica e Informática - UTFPR");
            doc.AddTitle("Certificado de Colaborador");

            var stream = new MemoryStream();

            var writer = PdfWriter.GetInstance(doc, stream);
            writer.CloseStream = false;

            doc.Open();

            var image = System.Drawing.Image.FromFile(Server.MapPath("\\Content\\Templates\\Fundo.jpg"));
            if (Usuario.SessionPersist.IsAdministrador)
            {

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

                var pdfImage = Image.GetInstance(image, new BaseColor(0, 0, 0));
                pdfImage.ScaleToFit(PageSize.A4_LANDSCAPE.Rotate());
                doc.Add(pdfImage);

            }
            doc.Close();

            Response.ClearContent();
            Response.ClearHeaders();
            Response.AddHeader("Content-Disposition", "inline;filename=CertificadoTrabalhoVoluntario.pdf");
            Response.ContentType = "application/pdf";

            stream.Seek(0, SeekOrigin.Begin);
            stream.CopyTo(Response.OutputStream);

            Response.Flush();
            Response.Clear();
        }

        [Authorize]
        public void TrabalhoVoluntario()
        {
            // Recupera participações em minicursos
            var trabalhos = TrabalhoVoluntarioBLL.GetByIdPessoa(Usuario.SessionPersist.IdPessoa);

            // Cria documento
            var html = System.IO.File.ReadAllText(Server.MapPath("\\Content\\Templates\\Certificado.html"));
            var doc = new Document(PageSize.A4_LANDSCAPE.Rotate(), 0, 0, 0, 0);
            doc.AddAuthor("Semana Acadêmica de Eletrônica e Informática - UTFPR");
            doc.AddTitle("Certificado de Presença");

            var stream = new MemoryStream();

            var writer = PdfWriter.GetInstance(doc, stream);
            writer.CloseStream = false;

            doc.Open();

            // Edita imagem

            foreach (TrabalhoVoluntarioModel t in trabalhos)
            {
                var image = System.Drawing.Image.FromFile(Server.MapPath("\\Content\\Templates\\Fundo.jpg"));

                using (var g = System.Drawing.Graphics.FromImage(image))
                {
                    g.DrawString("CERTIFICADO", new System.Drawing.Font("Arial", 20),
                    new System.Drawing.SolidBrush(System.Drawing.Color.Black),
                    new System.Drawing.Rectangle(140, 120, 730, 360),
                    new System.Drawing.StringFormat { Alignment = System.Drawing.StringAlignment.Center, Trimming = System.Drawing.StringTrimming.Word }
                    );

                    // Adiciona parametros
                    g.DrawString(
                      String.Format(
                      ConfigurationManager.AppSettings["Certificado.TrabalhoVoluntario"].ToString(),
                      Usuario.SessionPersist.Nome,
                      t.Descricao,
                      t.Horas),
                      new System.Drawing.Font("Arial", 12),
                      new System.Drawing.SolidBrush(System.Drawing.Color.Black),
                      new System.Drawing.Rectangle(140, 190, 730, 360),
                      new System.Drawing.StringFormat { Alignment = System.Drawing.StringAlignment.Center, Trimming = System.Drawing.StringTrimming.Word }
                      );
                }

                var pdfImage = Image.GetInstance(image, new BaseColor(0, 0, 0));
                pdfImage.ScaleToFit(PageSize.A4_LANDSCAPE.Rotate());

                doc.Add(pdfImage);
            }

            doc.Close();

            Response.ClearContent();
            Response.ClearHeaders();
            Response.AddHeader("Content-Disposition", "inline;filename=CertificadoTrabalhoVoluntario.pdf");
            Response.ContentType = "application/pdf";

            stream.Seek(0, SeekOrigin.Begin);
            stream.CopyTo(Response.OutputStream);

            Response.Flush();
            Response.Clear();
        }
    }
}