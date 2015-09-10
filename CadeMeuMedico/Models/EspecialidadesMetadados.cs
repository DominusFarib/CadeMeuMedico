using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MembroIndependente.Models
{
    [MetadataType(typeof(EspecialidadesMetadados))]
    public partial class Especialidades
    {
    }

    public class EspecialidadesMetadados
    {
        [Required(ErrorMessage = "Obrigatório informar o Nome da Especialidade")]
        [StringLength(60, ErrorMessage = "O nome da Especialidade deve possuir no máximo 60 caracteres")]
        public string Nome { get; set; }
    }
}    
