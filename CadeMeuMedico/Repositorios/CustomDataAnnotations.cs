using System;
using System.ComponentModel.DataAnnotations;
using System.Globalization;

namespace CadeMeuMedico.Repositorios
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
}