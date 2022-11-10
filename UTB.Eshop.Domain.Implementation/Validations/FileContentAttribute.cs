using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UTB.Eshop.Domain.Implementation.Validations
{
    public class FileContentAttribute : ValidationAttribute, IClientModelValidator
    {
        public string ContentType { get; set; }
        public FileContentAttribute(string contentType)
        {
            ContentType = contentType;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value == null)
            {
                return ValidationResult.Success;
            }
            else if(value is IFormFile formfile)
            {
                if(formfile.ContentType.ToLower().Contains(ContentType.ToLower()))
                {
                    return ValidationResult.Success;
                }
                else
                {
                    return new ValidationResult($"{validationContext.MemberName}: the file content is not {ContentType.ToLower()}");
                }
            }
            else
            {
                throw new NotImplementedException($"{nameof(FileContentAttribute)} cannot be used on the type: {value.GetType()}");
            }
        }

        public void AddValidation(ClientModelValidationContext context)
        {
            context.Attributes.Add("data-val", "true");
            context.Attributes.Add("data-val-filecontent", $"{context.ModelMetadata.Name}: the file content is not {ContentType.ToLower()}");
            context.Attributes.Add("data-val-filecontent-type", ContentType);
        }
    }
}
