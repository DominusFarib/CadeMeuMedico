using System.ComponentModel.DataAnnotations;

namespace CadeMeuMedico.Models
{
    [MetadataType(typeof(CidadesMetadados))]
    public partial class Cidades
    {
    }

    public class CidadesMetadados
    {
        [Required(ErrorMessage = "Obrigatório informar o Nome do município")]
        [StringLength(100, ErrorMessage = "O nome do munícipio deve possuir no máximo 100 caracteres")]
        public string Nome { get; set; }
    }
}