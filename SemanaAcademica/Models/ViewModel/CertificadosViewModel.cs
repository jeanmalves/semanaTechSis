using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SemanaAcademica.Models.ViewModel
{
    public class CertificadosViewModel
    {
        public bool HasContribuicao { get; set; }
        public bool HasPalestras { get; set; }
        public bool HasMinicurso { get; set; }
        public bool HasOficina { get; set; }
        public bool HasVisita { get; set; }
        public bool HasOrganizador { get; set; }
        public bool HasTrabalhoVoluntario { get; set; }
    }
}