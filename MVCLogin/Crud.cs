namespace MVCLogin
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;

    public partial class CrudDB
    {
        [Display(Name = "Nome do Funcionário:")]
        public string Nome { get; set; }
        [Display(Name = "Telefone:")]
        public string Telefone { get; set; }
}
}
