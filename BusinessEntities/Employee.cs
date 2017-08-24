using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace BusinessEntities
{
    public class Employee
    {
        [Key]
        public int EmployeeId { get; set; }

       // [Required(ErrorMessage = "Enter First name")]
        [FirstNameValidation]
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Salary { get; set; }
    }

    //Custom validation

    public class FirstNameValidation : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value == null)
            {
                return new ValidationResult("First name required");
            }

            return ValidationResult.Success;
        }
    }
}