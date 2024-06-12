using System.ComponentModel.DataAnnotations;

namespace Kiwi.Web.Utilites
{
    public class MaxFileSizeAttribute(int maxFileSize) : ValidationAttribute
    {
        private readonly int _maxFileSize = maxFileSize;

        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            var file = value as IFormFile;

            if (file != null)
            {

                if (file.Length > (_maxFileSize * 2048 * 2048))
                {
                    return new ValidationResult($"Maximum allowed file size is {_maxFileSize} MB.");
                }
            }

            return ValidationResult.Success;
        }

    }
}
