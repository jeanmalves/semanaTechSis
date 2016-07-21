using SemanaAcademica.Models.DAL;
using SemanaAcademica.Models.ObjectModel;
using System;
using System.Linq;

namespace SemanaAcademica.Models.BLL
{

    public static class PessoaBLL
    {
        static public PessoaModel SelectPessoaByEmail(string email)
        {
            try
            {
                var entities = new SemanaAcademicaEntities();
                var pessoa = entities.Pessoa.First(p => p.email == email);
                return new PessoaModel
                {
                    IdPessoa = pessoa.id_pessoa,
                    Email = pessoa.email,
                    Nome = pessoa.nome,
                    Senha = pessoa.senha,
                    Telefone = pessoa.telefone,
                    Chave = pessoa.chave,
                    Confirmado = pessoa.confirmado
                };
            }
            catch
            {
                return null;
            }
        }
        static public bool InsertPessoa(PessoaModel model)
        {
            try
            {
                var entities = new SemanaAcademicaEntities();
                entities.Pessoa.Add(
                    new Pessoa
                    {
                        email = model.Email,
                        nome = model.Nome,
                        telefone = model.Telefone,
                        senha = System.Text.ASCIIEncoding.ASCII.GetString(new System.Security.Cryptography.MD5CryptoServiceProvider().ComputeHash(System.Text.ASCIIEncoding.ASCII.GetBytes(model.Senha))),
                        chave = Guid.NewGuid()
                    }
                );
                entities.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                System.IO.File.AppendAllText(@"c:\log.txt", e.Message + "\n" + e.InnerException + "\n" + e.StackTrace);
                return false;
            }
        }
        static public bool ConfirmaPessoa(Guid chave)
        {
            try
            {
                var entities = new SemanaAcademicaEntities();
                entities.Pessoa.Where(p => p.chave == chave).ToList().ForEach(p => p.confirmado = true);
                entities.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public static bool UpdateSenha(int idpessoa, string senha)
        {
            try
            {
                var entities = new SemanaAcademicaEntities();
                var pessoa = entities.Pessoa.First(p => p.id_pessoa == idpessoa);
                pessoa.senha = System.Text.ASCIIEncoding.ASCII.GetString(new System.Security.Cryptography.MD5CryptoServiceProvider().ComputeHash(System.Text.ASCIIEncoding.ASCII.GetBytes(senha)));
                entities.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}