using System.ComponentModel.DataAnnotations;

namespace net_il_mio_fotoalbum.Models
{
    public class MinWordsAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            string fieldValue = (string)value;
            var parole = fieldValue?.Split(' ');
            if (parole?.Where(s => s.Length > 0).Count() < 5)
            {
                return new ValidationResult("Il campo deve contenere almeno cinque parole");
            }
            return ValidationResult.Success;
        }
    }
}

