using SemanaAcademica.Areas.Admin.Models;
using SemanaAcademica.Models.DAL;
using SemanaAcademica.Models.ObjectModel;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SemanaAcademica.Models.BLL
{
    public static class LocalBLL
    {
        static public IEnumerable<LocalModel> SelectLocais()
        {
            try
            {
                var entities = new SemanaAcademicaEntities();
                return entities.Local.Select(l => new LocalModel
                {
                    IdLocal = l.id_local,
                    Descricao = l.descricao
                });
            }
            catch
            {
                return null;
            }
        }


        static public bool InsertLocal(LocalModel model)
        {
            try
            {
                var entities = new SemanaAcademicaEntities();
                var local = new Local
                {
                    descricao = model.Descricao
                };

                entities.Local.Add(local);
                entities.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }


        public static bool DeleteLocal(int id)
        {
            try
            {
                var entities = new SemanaAcademicaEntities();
                var local = entities.Local.FirstOrDefault(h => h.id_local == id);
                entities.Local.Remove(local);
                entities.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public static bool UpdateLocal(int id, EditarLocalModel model)
        {
            try
            {
                var entities = new SemanaAcademicaEntities();
                var local = entities.Local.FirstOrDefault(h => h.id_local == id);
                local.descricao = model.Descricao;
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