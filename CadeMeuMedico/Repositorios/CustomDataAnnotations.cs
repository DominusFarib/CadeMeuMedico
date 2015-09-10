using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Web;

namespace MembroIndependente.Repositorios
{
    // DATA Annotations personalisadas
    public class AtributoDiferente : ValidationAttribute
    {
        private const string DefaultErrorMessage =  "'{0}' e o '{1}' devem ser diferentes.";
        
        public string BasePropertyName { get; private set; }

        public AtributoDiferente(string basePropertyName)
            : base(DefaultErrorMessage)
        {
            BasePropertyName = basePropertyName;
        }

        public override string FormatErrorMessage(string name)
        {
            string msg = this.ErrorMessage == string.Empty ? DefaultErrorMessage : this.ErrorMessage;
            return string.Format(DefaultErrorMessage, name, BasePropertyName);
        }
        // VALIDANDO
        protected override ValidationResult IsValid(object valorAtributo1, ValidationContext validationContext)
        {
            var atribComparado = validationContext.ObjectType.GetProperty(BasePropertyName);

            if (atribComparado == null)
            {
                return new ValidationResult(string.Format(CultureInfo.CurrentCulture, "{0} propriedade invalida", BasePropertyName));
            }

            var valorAtributo2 = atribComparado.GetValue(validationContext.ObjectInstance, null);

            if (object.Equals(valorAtributo1, valorAtributo2))
            {
                return new ValidationResult(FormatErrorMessage(validationContext.DisplayName));
            }

            return ValidationResult.Success;
        }
    }

    public class TamanhoMaximoAttribute : ValidationAttribute
    {
        private readonly int _maxSize;

        public TamanhoMaximoAttribute(int maxSize)
        {
            _maxSize = maxSize;
        }

        public override bool IsValid(object value)
        {
            if (value == null) return true;

            return _maxSize > (value as HttpPostedFileBase).ContentLength;
        }

        public override string FormatErrorMessage(string name)
        {
            return string.Format("O tamanho do arquivo excede o máximo permitido de {0}", _maxSize);
        }
    }

    public class ExtensoesAttribute: ValidationAttribute
    {
        // private readonly List<string> _types;
        private readonly string[] Extensoes;

        public ExtensoesAttribute(String types)
        {
            Extensoes = types.Split(',');
        }

        public override bool IsValid(object value)
        {
            if (value == null) return true;
            HttpPostedFileBase dados = value as HttpPostedFileBase;
            var fileExt = System.IO.Path.GetExtension(dados.FileName).Substring(1);

            //return Extensoes.Contains(fileExt, StringComparer.OrdinalIgnoreCase);
            
            if (!Array.Exists(Extensoes, element => element == fileExt)) return false;
            
            return true;

        }

        public override string FormatErrorMessage(string name)
        {
            return string.Format("Tipo de arquivo inválido. Envie arquivo dos tipos {0}.", String.Join(", ", Extensoes));
        }
    }

    public class ValidaArquivoAttribute : ValidationAttribute
    {
        public int TamanhoMax { get; set; }
        public int TamanhoMin { get; set; }
        public string[] Extensoes { get; set; }

        public override bool IsValid(Object arquivo)
        {
            try
            {

                var dados = arquivo as HttpPostedFileBase;

                if (dados != null)
                {
                    if (dados.ContentLength > TamanhoMax) return false;
                    if (dados.ContentLength < TamanhoMin) return false;
                    if (string.IsNullOrEmpty(dados.FileName) && string.IsNullOrWhiteSpace(dados.FileName)) return false;
                    if (!Array.Exists(Extensoes, element => element == dados.FileName.Substring(dados.FileName.LastIndexOf('.')))) return false;
                }
            }
            catch(Exception e)
            {
                throw (e);
            }
            return true;

        }

    }

}