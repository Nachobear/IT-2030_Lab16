using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Lab13.Models.Validation
{
    public class GreaterThanAttribute : ValidationAttribute
    {
        public object compareValue { get; set; }

        public GreaterThanAttribute(object val) => compareValue = val;


        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            
            if(value is int)
            {
                int valueToCheck = (int)value;
                int valueToCompare = (int)compareValue;
                if(valueToCheck > valueToCompare)
                {
                    return ValidationResult.Success;
                }
            }
            else if(value is double)
            {
                double valueToCheck = (double)value;
                double valueToCompare = (double)compareValue;
                if (valueToCheck > valueToCompare)
                {
                    return ValidationResult.Success;
                }
            }
            else if(value is DateTime)
            {
                DateTime valueToCheck = (DateTime)value;
                DateTime valueToCompare = new DateTime();
                DateTime.TryParse(compareValue.ToString(), out valueToCompare);
                
                if (valueToCheck > valueToCompare)
                {
                    return ValidationResult.Success;
                }
            }
            else
            {
                
                return ValidationResult.Success;
            }

            string message = base.ErrorMessage ?? $"{validationContext.DisplayName} must be greater than {compareValue.ToString()}.";
            return new ValidationResult(message);


        }
    }
}
