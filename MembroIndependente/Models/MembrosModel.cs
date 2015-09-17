using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MembroIndependente.Repositorios;
using System.ComponentModel.DataAnnotations;


namespace MembroIndependente.Models
{
    public class MembrosModel:Medicos
    {
        //[FileExtensions(ErrorMessage = "Arquivo de foto não é válido, envie imgens com as seguinte extenções(jpg, jpeg, png)", Extensions = "jpg,jpeg,png,bmp")]
        //[DataType(DataType.Upload)]
        //[TamanhoMaximo(10240)]
        //[Extensoes("jpg,jpeg,png,bmp,gif")]
        //[ValidaArquivo(Extensoes = new string[] { ".bmp", ".gif", ".png", ".jpeg", ".jpg" }, TamanhoMax = 1024 * 1024 * 3, TamanhoMin = 1024 * 50, ErrorMessage = "O arquivo/imagem deve ter o tamanho entre 50KB e 3MB, por favor, tente novamente !")]
        [Display(Name = "Foto")]
        public HttpPostedFileBase Imagem { get; set; }
        
        
        //[Required(ErrorMessage = "Obrigatório informar o CRM")]
        //[StringLength(30, ErrorMessage = "O CPF deve possuir no máximo 11 caracteres")]
        //public string Doc { get; set; }

        //[Required(ErrorMessage = "Obrigatório informar o Nome")]
        //[StringLength(80, ErrorMessage = "O Nome deve possuir no máximo 80 caracteres")]
        //public string Nome { get; set; }


        //[Required(ErrorMessage = "Obrigatório informar o Endereço")]
        //[StringLength(100, ErrorMessage = "O Endereço deve possuir no máximo 100 caracteres")]
        //public string Endereco { get; set; }

        //[Required(ErrorMessage = "Obrigatório informar o Bairro")]
        //[StringLength(60, ErrorMessage = "O Bairro deve possuir no máximo 60 caracteres")]
        //public string Bairro { get; set; }

        //[Required(ErrorMessage = "Obrigatório informar o E-mail")]
        //[StringLength(100, ErrorMessage = "O E-mail deve possuir no máximo 100 caracteres")]
        //public string Email { get; set; }

        //[Required(ErrorMessage = "Obrigatório informar se Atende por Convênio")]
        //public bool AtendePorConvenio { get; set; }

        //[Required(ErrorMessage = "Obrigatório informar se Tem Clínica")]
        //public bool TemClinica { get; set; }

        //[StringLength(80, ErrorMessage = "O Website deve possuir no máximo 80 caracteres")]
        //public string WebsiteBlog { get; set; }

        //[Required(ErrorMessage = "Obrigatório informar a Cidade")]
        
        ////public int FKCidade { get; set; }
        //public virtual Cidades FKCidade { get; set; }
        
        //[Required(ErrorMessage = "Obrigatório informar a Especialidade")]
        ////public int FKEspecialidade { get; set; }
        //public virtual Especialidades FKEspecialidade { get; set; }

    }
}

