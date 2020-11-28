using System;
using System.ComponentModel.DataAnnotations;

namespace CompanyMannager.Validation
{
    public class EnumAsStringAttribute : ValidationAttribute
    {
        public Type TargetEnum { get; set; }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (TargetEnum.IsEnum == false)
            {
                throw new ArgumentException("TargetEnum is not propper enum type");
            }

            if (Enum.TryParse(TargetEnum, (string)value, out _))
            {
                return ValidationResult.Success;
            }

            return new ValidationResult($"Please enter valid enum value for '{TargetEnum.Name}'.");
        }
    }
}