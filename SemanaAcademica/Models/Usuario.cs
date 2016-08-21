using SemanaAcademica.Models.BLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;

namespace SemanaAcademica
{
    public class Usuario
    {

        public int IdPessoa { get; set; }
        public string Nome { get; set; }
        public string NomeMenu { get; set; }
        public string Email { get; set; }
        public string Telefone { get; set; }
        public string Senha { get; set; }
        public bool Confirmado { get; set; }
        public bool IsAdministrador { get; set; }
        public bool IsColaborador { get; set; }

        public static Usuario SessionPersist
        {
            get
            {
                {
                    if (!HttpContext.Current.User.Identity.IsAuthenticated)
                    {
                        HttpContext.Current.Session.Clear();
                        return new Usuario { Confirmado = false, IsAdministrador = false, IsColaborador = false };
                    };

                    var _SessionUser = HttpContext.Current.Session[String.Format("UsuarioAutenticado.{0}", HttpContext.Current.User.Identity.Name)] as Usuario;

                    if (_SessionUser == null)
                    {
                        var pessoa = PessoaBLL.SelectPessoaByEmail(HttpContext.Current.User.Identity.Name);
                        var colaborador = ColaboradorBLL.SelectColaboradorByIdPessoa(pessoa.IdPessoa);
                        var administrador = AdministradorBLL.SelectAdministradorByIdPessoa(pessoa.IdPessoa);

                        if (pessoa != null)
                        {
                            string regexPatternPrimeiroNome = "[\\w]+[^\\s+]";
                            Regex r = new Regex(regexPatternPrimeiroNome, RegexOptions.IgnoreCase);
                            Match m = r.Match(pessoa.Nome);
                            _SessionUser = new Usuario
                            {
                                IdPessoa = pessoa.IdPessoa,
                                Nome = pessoa.Nome,
                                Email = pessoa.Email,
                                Senha = pessoa.Senha,
                                Telefone = pessoa.Telefone,
                                IsColaborador = colaborador != null,
                                IsAdministrador = administrador != null,
                                NomeMenu = m.Value
                            };

                            HttpContext.Current.Session[String.Format("UsuarioAutenticado.{0}", HttpContext.Current.User.Identity.Name)] = _SessionUser;
                        }
                    }
                    return _SessionUser;
                }
            }
        }
    }
}