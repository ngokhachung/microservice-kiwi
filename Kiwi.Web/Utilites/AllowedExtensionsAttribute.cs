using System.ComponentModel.DataAnnotations;

namespace Kiwi.Web.Utilites
{
    public class AllowedExtensionsAttribute(string[] extensions) : ValidationAttribute
    {
        private readonly string[] _extensions = extensions;

        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            var file = value as IFormFile;

            if (file != null)
            {
                var extension = Path.GetExtension(file.FileName);
                if (!_extensions.Contains(extension.ToLower()))
                {
                    return new ValidationResult("This photo extension is not allowed!");
                }
            }

            return ValidationResult.Success;
        }

    }
}
