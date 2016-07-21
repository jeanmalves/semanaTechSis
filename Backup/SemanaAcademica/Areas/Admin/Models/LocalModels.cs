using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SemanaAcademica.Areas.Admin.Models
{


    public class CadastrarLocalModel
    {
        public string Descricao { get; set; }
    }

    public class ListarLocalModel
    {
        public int IdLocal { get; set; }
        public string Descricao { get; set; }
    }

    public class EditarLocalModel
    {
        public int IdLocal { get; set; }
        public string Descricao { get; set; }
    }

}