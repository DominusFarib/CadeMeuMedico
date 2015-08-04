using CadeMeuMedico.Repositorios;
using System.ComponentModel.DataAnnotations;

namespace CadeMeuMedico.Models
{
    public class ContasModel
    {
        [Required(ErrorMessage = "Obrigatório informar o Nome.")]
        [StringLength(80, ErrorMessage = "O Nome deve possuir no máximo 80 caracteres.")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "Obrigatório informar o Login")]
        [AtributoDiferente("Nome", ErrorMessage = "O campo LOGIN dever ser diferente do NOME")]
        [StringLength(20, ErrorMessage = "O Login deve possuir no máximo 20 caracteres.")]
        public string Login { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Senha")]
        [Required(ErrorMessage = "Obrigatório informar o Senha")]
        [StringLength(16, MinimumLength = 8, ErrorMessage = "A senha deve ter entre 8 e 16 caracteres")]
        [RegularExpression(@"[a-zA-Z0-9]*", ErrorMessage = "Na senha somente são permitidos caracteres alfanuméricos")]
        public string Senha { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirme a senha")]
        [Compare("Senha", ErrorMessage = "A senha e a confirmação de senha não conferem.")]
        public string ConfirmaSenha { get; set; }

        [DataType(DataType.EmailAddress)]
        [Display(Name = "E-mail")]
        [StringLength(100, ErrorMessage = "O Email deve possuir no máximo 100 caracteres")]
        [EmailAddress(ErrorMessage = "E-mail em formato inválido.")]
        public string Email { get; set; }

    }
}
