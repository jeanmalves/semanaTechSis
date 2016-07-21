using SemanaAcademica.Models.DAL;
using SemanaAcademica.Models.ObjectModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SemanaAcademica.Models.BLL
{
    public class AdministradorBLL
    {
        static public AdministradorModel SelectAdministradorByIdPessoa(int IdPessoa)
        {
            var entities = new SemanaAcademicaEntities();
            try
            {
                return entities.Administrador.Where(c => c.id_pessoa == IdPessoa).Select(c =>
                   new AdministradorModel
                   {
                       IdAdministrador = c.id_administrador,
                       IdPessoa = c.id_pessoa
                   }
               ).SingleOrDefault();
            }
            catch
            {
                return null;
            }
        }

        //static public bool InsertColaborador(ColaboradorModel model)
        //{
        //    var entities = new SemanaAcademicaEntities();
        //    try
        //    {
        //        if (entities.Colaborador.Any(p => p.id_pessoa == model.IdPessoa))
        //            return false;

        //        entities.Participante.Add(
        //            new Participante
        //            {
        //                id_pessoa = model.IdPessoa,
        //                contribuicao = model.Contribuicao,
        //                matricula = model.Matricula,
        //                registro = model.Registro
        //            }
        //        );
        //        entities.SaveChanges();
        //        return true;
        //    }
        //    catch
        //    {
        //        return false;
        //    }
        //}
    }
}