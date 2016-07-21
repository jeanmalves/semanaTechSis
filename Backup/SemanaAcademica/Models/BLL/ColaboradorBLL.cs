using SemanaAcademica.Models.DAL;
using SemanaAcademica.Models.ObjectModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SemanaAcademica.Models.BLL
{
    public class ColaboradorBLL
    {
        static public ColaboradorModel SelectColaboradorByIdPessoa(int IdPessoa)
        {
            var entities = new SemanaAcademicaEntities();
            try
            {
                return entities.Colaborador.Where(c => c.id_pessoa == IdPessoa).Select(c =>
                   new ColaboradorModel
                   {
                       IdColaborador = c.id_colaborador,
                       IdPessoa = c.id_pessoa
                   }
               ).SingleOrDefault();
            }
            catch
            {
                return null;
            }
        }

        static public bool DeleteColaborador(int id_pessoa)
        {

            try
            {
                var entities = new SemanaAcademicaEntities();
                foreach (var colaborador in entities.Colaborador.Where(c => c.id_pessoa == id_pessoa)) entities.Colaborador.Remove(colaborador);
                entities.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public static bool InsertColaborador(int id_pessoa)
        {
            try
            {
                var entities = new SemanaAcademicaEntities();
                if (entities.Colaborador.Count(c => c.id_pessoa == id_pessoa) == 0)
                {
                    entities.Colaborador.Add(new Colaborador { id_pessoa = id_pessoa });
                    entities.SaveChanges();
                }
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}