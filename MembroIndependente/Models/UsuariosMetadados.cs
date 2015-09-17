using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace MembroIndependente.Models
{
    [MetadataType(typeof(UsuariosMetadados))]
    public partial class Usuarios
    {
    }

    public class UsuariosMetadados
    {
        [Required(ErrorMessage = "Obrigatório informar o Nome")]
        [StringLength(80, ErrorMessage = "O Nome deve possuir no máximo 80 caracteres")]
        public string Nome { get; set; }
        
        [Required(ErrorMessage = "Obrigatório informar o Login")]
        [StringLength(20, ErrorMessage = "O Login deve possuir no máximo 20 caracteres")]
        public string Login { get; set; }
        
        [Required(ErrorMessage = "Obrigatório informar o Senha")]
        [DataType(DataType.Password)]
        [StringLength(16, MinimumLength=8, ErrorMessage = "A senha deve ter entre 8 e 16 caracteres")]
        [RegularExpression(@"[a-zA-Z0-9]*", ErrorMessage="Na senha somente são permitidos caracteres alfanuméricos")]
        [Display(Name = "Senha")]
        public string Senha { get; set; }

        [Display(Name = "E-mail")]
        //[RegularExpression(@"^(([A-Za-z0-9]+_+)|([A-Za-z0-9]+\-+)|([A-Za-z0-9]+\.+)|([A-Za-z0-9]+\++))*[A-Za-z0-9]+@((\w+\-+)|(\w+\.))*\w{1,63}\.[a-zA-Z]{2,6}$", ErrorMessage = "Informe um email válido")]
        [StringLength(100, ErrorMessage = "O Email deve possuir no máximo 100 caracteres")]
        [EmailAddress(ErrorMessage = "E-mail em formato inválido.")]
        public string Email { get; set; }
    }
}