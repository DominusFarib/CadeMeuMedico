using System;
using System.Web;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using MembroIndependente.Repositorios;
using System.ComponentModel;

namespace MembroIndependente.Models
{
    [MetadataType(typeof(MedicosMetadados))]
    public partial class Medicos
    {
    }

    public class MedicosMetadados
    {
        [Required(ErrorMessage="Obrigatório informar o CRM")]
        [StringLength(30, ErrorMessage="O CRM deve possuir no máximo 30 caracteres")]
        public string CRM { get; set; }
        
        [Required(ErrorMessage = "Obrigatório informar o Nome")]
        [StringLength(80, ErrorMessage = "O Nome deve possuir no máximo 80 caracteres")]
        public string Nome { get; set; }
        
        // [FileExtensions(ErrorMessage = "Arquivo de foto não é válido, envie imgens com as seguinte extenções(jpg, jpeg, png)", Extensions = "jpg,jpeg,png,bmp")]
        // [ValidaArquivo(Extensoes = new string[] { ".bmp", ".gif", ".png", ".jpeg", ".jpg" }, TamanhoMax = 1024 * 1024 * 3, TamanhoMin = 1024 * 100, ErrorMessage = "O arquivo/imagem deve ter o tamanho máximo de 3M, por favor, tente novamente !")]
        [NotMapped]
        [Display(Name = "Perfil")]
        [DataType(DataType.Upload)]
        [TamanhoMaximo(10240)]
        [Extensoes("jpg,jpeg,png,bmp,gif")]
        private HttpPostedFileBase Imagem { get; set; }

        [Required(ErrorMessage = "Obrigatório informar o Endereço")]
        [StringLength(100, ErrorMessage = "O Endereço deve possuir no máximo 100 caracteres")]
        public string Endereco { get; set; }
        
        [Required(ErrorMessage = "Obrigatório informar o Bairro")]
        [StringLength(60, ErrorMessage = "O Bairro deve possuir no máximo 60 caracteres")]
        public string Bairro { get; set; }
        
        [Required(ErrorMessage = "Obrigatório informar o E-mail")]
        [StringLength(100, ErrorMessage = "O E-mail deve possuir no máximo 100 caracteres")]
        public string Email { get; set; }
                
        [DisplayName("Atende Convenio")]
        [Required(ErrorMessage = "Obrigatório informar se Atende por Convênio")]
        public bool AtendePorConvenio { get; set; }

        [DisplayName("Possui clínica")]
        [Required(ErrorMessage = "Obrigatório informar se Tem Clínica")]
        public bool TemClinica { get; set; }
        
        [StringLength(80, ErrorMessage = "O Website deve possuir no máximo 80 caracteres")]
        public string WebsiteBlog { get; set; }
        [DisplayName("Cidade")]
        [Required(ErrorMessage = "Obrigatório informar a Cidade")]
        public int FKCidade { get; set; }
        [DisplayName("Especialidade")]
        [Required(ErrorMessage = "Obrigatório informar a Especialidade")]
        public int FKEspecialidade { get; set; }



    }
}