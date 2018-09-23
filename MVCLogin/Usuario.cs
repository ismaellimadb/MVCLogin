
namespace MVCLogin
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;

    public partial class Usuario
    {
        public int UserID { get; set; }
        public string Nome { get; set; }
        [Required(ErrorMessage="Por favor informe o Usuário", AllowEmptyStrings=false)]
        [DisplayName("Usuário")]
        public string Usuario1 { get; set; }
        [Required(ErrorMessage = "Por favor informe a Senha", AllowEmptyStrings = false)]
        [DataType(System.ComponentModel.DataAnnotations.DataType.Password)]
        public string Senha { get; set; }
    }
}
