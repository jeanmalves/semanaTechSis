using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SemanaAcademica.Models.ViewModel
{
    public class CertificadosViewModel
    {
        public bool HasParticipacao { get; set; }
        public bool HasMinicurso { get; set; }
        public bool HasOrganizador { get; set; }
        public bool HasColaborador { get; set; }
        public bool HasPalestrante { get; set; }
    }
}