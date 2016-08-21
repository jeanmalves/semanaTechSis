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

            if (participacoes == null || participacoes.Count == 0) { return; }

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
            var image = System.Drawing.Image.FromFile(Server.MapPath("\\Content\\Templates\\certificado2016.png"));

            using (var g = System.Drawing.Graphics.FromImage(image))
            {
                g.DrawString("CERTIFICADO", new System.Drawing.Font("Arial", 20),
                new System.Drawing.SolidBrush(System.Drawing.Color.Black),
                new System.Drawing.Rectangle(500, 300, 1000, 360),
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
                  new System.Drawing.Rectangle(300, 460, 1400, 760),
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
        // GET: /Certificado/Minicursos
        [Authorize]
        public void Minicursos()
        {
            // Recupera participações em visitas
            var minicursos = ParticipacaoBLL.GetMinicursosByIdPessoa(Usuario.SessionPersist.IdPessoa);

            if (minicursos == null || minicursos.Count == 0) { return; }

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
                    new System.Drawing.Rectangle(500, 300, 1000, 360),
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
                      new System.Drawing.Rectangle(300, 460, 1400, 760),
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

        //
        // GET: /Certificado/Visitas
        [Authorize]
        public void Visitas()
        {
            // Recupera participações em visitas
            var visitas = ParticipacaoBLL.GetVisitasIdPessoa(Usuario.SessionPersist.IdPessoa);

            if (visitas == null || visitas.Count == 0) { return; }

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

            foreach (ParticipacaoModel p in visitas)
            {
                var image = System.Drawing.Image.FromFile(Server.MapPath("\\Content\\Templates\\Fundo.jpg"));

                using (var g = System.Drawing.Graphics.FromImage(image))
                {
                    g.DrawString("CERTIFICADO", new System.Drawing.Font("Arial", 20),
                    new System.Drawing.SolidBrush(System.Drawing.Color.Black),
                    new System.Drawing.Rectangle(500, 300, 1000, 360),
                    new System.Drawing.StringFormat { Alignment = System.Drawing.StringAlignment.Center, Trimming = System.Drawing.StringTrimming.Word }
                    );

                    // Adiciona parametros
                    g.DrawString(
                      String.Format(
                      ConfigurationManager.AppSettings["Certificado.Visita"].ToString(),
                      Usuario.SessionPersist.Nome,
                      p.NomeEvento,
                      String.Format("{0:dd/MM/yyyy}", p.HoraEntrada),
                      Math.Ceiling((double)(p.HoraSaida.Value - p.HoraEntrada.Value).TotalMinutes / 60)),
                      new System.Drawing.Font("Arial", 12),
                      new System.Drawing.SolidBrush(System.Drawing.Color.Black),
                      new System.Drawing.Rectangle(300, 460, 1400, 760),
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

        //
        // GET: /Certificado/Oficinas
        [Authorize]
        public void Oficinas()
        {
            // Recupera participações em visitas
            var oficinas = ParticipacaoBLL.GetOficinasByIdPessoa(Usuario.SessionPersist.IdPessoa);

            if (oficinas == null || oficinas.Count == 0) { return; }

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

            foreach (ParticipacaoModel p in oficinas)
            {
                var image = System.Drawing.Image.FromFile(Server.MapPath("\\Content\\Templates\\Fundo.jpg"));

                using (var g = System.Drawing.Graphics.FromImage(image))
                {
                    g.DrawString("CERTIFICADO", new System.Drawing.Font("Arial", 20),
                    new System.Drawing.SolidBrush(System.Drawing.Color.Black),
                    new System.Drawing.Rectangle(500, 300, 1000, 360),
                    new System.Drawing.StringFormat { Alignment = System.Drawing.StringAlignment.Center, Trimming = System.Drawing.StringTrimming.Word }
                    );

                    // Adiciona parametros
                    g.DrawString(
                      String.Format(
                      ConfigurationManager.AppSettings["Certificado.Oficina"].ToString(),
                      Usuario.SessionPersist.Nome,
                      p.NomeEvento,
                      Math.Ceiling((double)(p.HoraSaida.Value - p.HoraEntrada.Value).TotalMinutes / 60)),
                      new System.Drawing.Font("Arial", 12),
                      new System.Drawing.SolidBrush(System.Drawing.Color.Black),
                      new System.Drawing.Rectangle(300, 460, 1400, 760),
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

        //[Authorize]
        //public void Colaborador()
        //{
        //    // Cria documento
        //    var html = System.IO.File.ReadAllText(Server.MapPath("\\Content\\Templates\\Certificado.html"));
        //    var doc = new Document(PageSize.A4_LANDSCAPE.Rotate(), 0, 0, 0, 0);
        //    doc.AddAuthor("Semana Acadêmica de Eletrônica e Informática - UTFPR");
        //    doc.AddTitle("Certificado de Colaborador");

        //    var stream = new MemoryStream();

        //    var writer = PdfWriter.GetInstance(doc, stream);
        //    writer.CloseStream = false;

        //    doc.Open();

        //    var image = System.Drawing.Image.FromFile(Server.MapPath("\\Content\\Templates\\Fundo.jpg"));

        //    // Certificado de colaborador
        //    if (Usuario.SessionPersist.IsColaborador)
        //    {
        //        using (var g = System.Drawing.Graphics.FromImage(image))
        //        {
        //            g.DrawString("CERTIFICADO", new System.Drawing.Font("Arial", 20),
        //            new System.Drawing.SolidBrush(System.Drawing.Color.Black),
        //            new System.Drawing.Rectangle(140, 120, 730, 360),
        //            new System.Drawing.StringFormat { Alignment = System.Drawing.StringAlignment.Center, Trimming = System.Drawing.StringTrimming.Word }
        //            );

        //            g.DrawString(
        //              String.Format(
        //              ConfigurationManager.AppSettings["Certificado.Colaborador"].ToString(),
        //              Usuario.SessionPersist.Nome),
        //              new System.Drawing.Font("Arial", 12),
        //              new System.Drawing.SolidBrush(System.Drawing.Color.Black),
        //              new System.Drawing.Rectangle(140, 190, 730, 360),
        //              new System.Drawing.StringFormat { Alignment = System.Drawing.StringAlignment.Center, Trimming = System.Drawing.StringTrimming.Word }
        //              );
        //        }
        //    }

        //    var pdfImage = Image.GetInstance(image, new BaseColor(0, 0, 0));
        //    pdfImage.ScaleToFit(PageSize.A4_LANDSCAPE.Rotate());
        //    doc.Add(pdfImage);
        //    doc.Close();

        //    Response.ClearContent();
        //    Response.ClearHeaders();
        //    Response.AddHeader("Content-Disposition", "inline;filename=CertificadoTrabalhoVoluntario.pdf");
        //    Response.ContentType = "application/pdf";

        //    stream.Seek(0, SeekOrigin.Begin);
        //    stream.CopyTo(Response.OutputStream);

        //    Response.Flush();
        //    Response.Clear();
        //}

        [Authorize]
        public void Administrador()
        {
            // Cria documento
            var html = System.IO.File.ReadAllText(Server.MapPath("\\Content\\Templates\\Certificado.html"));
            var doc = new Document(PageSize.A4_LANDSCAPE.Rotate(), 0, 0, 0, 0);
            doc.AddAuthor("Semana Acadêmica de Eletrônica e Informática - UTFPR");
            doc.AddTitle("Certificado de Organizador");

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
                    new System.Drawing.Rectangle(500, 300, 1000, 360),
                    new System.Drawing.StringFormat { Alignment = System.Drawing.StringAlignment.Center, Trimming = System.Drawing.StringTrimming.Word }
                    );

                    g.DrawString(
                      String.Format(
                      ConfigurationManager.AppSettings["Certificado.Administrador"].ToString(),
                      Usuario.SessionPersist.Nome),
                      new System.Drawing.Font("Arial", 12),
                      new System.Drawing.SolidBrush(System.Drawing.Color.Black),
                      new System.Drawing.Rectangle(300, 460, 1400, 760),
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
            // Recupera participações em visitas
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
                    new System.Drawing.Rectangle(500, 300, 1000, 360),
                    new System.Drawing.StringFormat { Alignment = System.Drawing.StringAlignment.Center, Trimming = System.Drawing.StringTrimming.Word }
                    );

                    // Adiciona parametros
                    g.DrawString(
                      String.Format(
                      ConfigurationManager.AppSettings["Certificado.TrabalhoVoluntario"].ToString(),
                      Usuario.SessionPersist.Nome,
                      String.Format("{0:dd/MM/yyyy}", t.DataInicio),
                      String.Format("{0:dd/MM/yyyy}", t.DataFim),
                      t.Horas
                      ),
                      new System.Drawing.Font("Arial", 12),
                      new System.Drawing.SolidBrush(System.Drawing.Color.Black),
                      new System.Drawing.Rectangle(300, 460, 1400, 760),
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