using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace ClasesBase.Utilities.Validators
{
    public static class DniValidator
    {
        public class ValidationResult
        {
            public bool IsValid { get; set; }
            public string ErrorMessage { get; set; }

            public ValidationResult(bool isValid, string errorMessage = "")
            {
                IsValid = isValid;
                ErrorMessage = errorMessage;
            }
        }

        public static ValidationResult ValidarDni(string dni)
        {
            if (string.IsNullOrWhiteSpace(dni))
                return new ValidationResult(false, "El DNI no puede estar vacío");

            if (!Regex.IsMatch(dni, @"^\d+$"))
                return new ValidationResult(false, "El DNI solo puede contener números");

            if (dni.Length != 8)
                return new ValidationResult(false, "El DNI debe tener 8 dígitos");

            return new ValidationResult(true);
        }

        // Versión simple que solo retorna bool
        public static bool IsValidDni(string dni)
        {
            if (string.IsNullOrWhiteSpace(dni))
                return false;

            if (dni.Length != 8)
                return false;

            return Regex.IsMatch(dni, @"^\d+$");
        }
    }
}
