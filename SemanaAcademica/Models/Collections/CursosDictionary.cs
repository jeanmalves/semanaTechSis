using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SemanaAcademica.Models.Collections
{
    public class CursosDictionary
    {
        private static CursosDictionary objetoCursosDictionary;
        private static Dictionary<int, string> cursosDictionary;

        public CursosDictionary()
        {
            //Cursos atualmente ofertados nos processos de seleção à UTFPR
            cursosDictionary = new Dictionary<int, string>();
            cursosDictionary.Add(1, "Técnico em Eletrônica");
            cursosDictionary.Add(2, "Técnico em Mecânica");
            cursosDictionary.Add(3, "Engenharia Civil");
            cursosDictionary.Add(4, "Engenharia de Computação");
            cursosDictionary.Add(5, "Engenharia de Controle e Automação");
            cursosDictionary.Add(6, "Engenharia Elétrica");
            cursosDictionary.Add(7, "Engenharia Eletrônica");
            cursosDictionary.Add(8, "Engenharia Mecânica");
            cursosDictionary.Add(9, "Engenharia Mecatrônica");
            cursosDictionary.Add(10, "Administração");
            cursosDictionary.Add(11, "Arquitetura e Urbanismo");
            cursosDictionary.Add(12, "Comunicação Organizacional");
            cursosDictionary.Add(13, "Design");
            cursosDictionary.Add(14, "Bacharelado em Educação Física");
            cursosDictionary.Add(15, "Bacharelado em Química");
            cursosDictionary.Add(16, "Sistemas de Informação");
            cursosDictionary.Add(17, "Licenciatura em Física");
            cursosDictionary.Add(18, "Licenciatura em Letras Inglês");
            cursosDictionary.Add(19, "Licenciatura em Letras Português");
            cursosDictionary.Add(20, "Licenciatura em Matemática");
            cursosDictionary.Add(21, "Licenciatura em Química");
            cursosDictionary.Add(22, "Tecnologia em Automação Industrial");
            cursosDictionary.Add(23, "Tecnologia em Design Gráfico");
            cursosDictionary.Add(24, "Tecnologia em Processos Ambientais");
            cursosDictionary.Add(25, "Tecnologia em Radiologia");
            cursosDictionary.Add(26, "Tecnologia em Sistemas de Telecomunicações");

            //Cursos que deixaram de ser ofertados nos processos seleção para ingresso 
            //à UTFPR e ainda possuem alunos matriculados
            cursosDictionary.Add(27, "Engenharia de Produção Civil");
            cursosDictionary.Add(28, "Engenharia Industrial Elétrica Ênfase Automação");
            cursosDictionary.Add(29, "Engenharia Industrial Elétrica Ênfase Eletrônica/Telecomunicações");
            cursosDictionary.Add(30, "Engenharia Industrial Elétrica Ênfase Eletrotécnica");
            cursosDictionary.Add(31, "Engenharia Industrial Mecânica");
            cursosDictionary.Add(32, "Licenciatura em Letras Português Inglês");
            cursosDictionary.Add(33, "Tecnologia em Concreto");
            cursosDictionary.Add(34, "Tecnologia em Comunicações Digitais");
            cursosDictionary.Add(35, "Tecnologia em Design de Móveis");
            cursosDictionary.Add(36, "Tecnologia em Gestão Comercial");
            cursosDictionary.Add(37, "Tecnologia em Gestão da Manufatura");
            cursosDictionary.Add(38, "Tecnologia em Sistemas para Internet");
            cursosDictionary.Add(39, "Técnico em Gestão de Pequenas e Médias Empresas");
            cursosDictionary.Add(40, "Técnico em Segurança do Trabalho");
            cursosDictionary.Add(41, "Técnico em Edificações");
            cursosDictionary.Add(42, "Tecnologia em Comunicação Institucional");
            cursosDictionary.Add(43, "Tecnologia em Mecatrônica Industrial");

            //Pós-Graduação
            cursosDictionary.Add(44, "Programa de Pós-Graduação em Engenharia Elétrica e Informática Industrial - CPGEI");
            cursosDictionary.Add(45, "Programa de Pós-Graduação em Engenharia Mecânica e de Materiais - PPGEM");
            cursosDictionary.Add(46, "Programa de Pós-Graduação em Tecnologia - PPGTE");
            cursosDictionary.Add(47, "Programa de Pós-Graduação em Engenharia Civil - PPGEC");
            cursosDictionary.Add(48, "Programa de Pós-Graduação em Ciência e Tecnologia Ambiental - PPGCTA");
            cursosDictionary.Add(49, "Programa de Pós-Graduação em Computação Aplicada - PPGCA");
            cursosDictionary.Add(50, "Programa de Pós-Graduação em Engenharia Biomédica - PPGEB");
            cursosDictionary.Add(51, "Programa de Pós-Graduação em Planejamento e Governança Pública - PPGPGP");
            cursosDictionary.Add(52, "Mestrado Profissional em Matemática em Rede Nacional - PROFMAT");
            cursosDictionary.Add(53, "Programa de Pós-Graduação em Formação Científica, Educacional e Tecnológica - FCET");
            cursosDictionary.Add(54, "Programa de Pós-Graduação em Química - PPGQ");
            cursosDictionary.Add(55, "Programa de Pós-Graduação em Estudos de Linguagens - PPGEL");
            cursosDictionary.Add(56, "Programa de Pós-Graduação em Administração - PPGA");
            cursosDictionary.Add(57, "Programa de Pós-Graduação em Sistemas de Energia - PPGSE");
        }

        public static Dictionary<int, string> ListaCurso
        {
            get
            {
                if (cursosDictionary == null)
                {
                    objetoCursosDictionary = new CursosDictionary();
                }
                return cursosDictionary;
            }
        }
    }
}