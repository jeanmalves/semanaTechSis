
//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------


namespace SemanaAcademica.Models.DAL
{

using System;
    using System.Collections.Generic;
    
public partial class Administrador
{

    public int id_administrador { get; set; }

    public int id_pessoa { get; set; }



    public virtual Pessoa Pessoa { get; set; }

}

}
