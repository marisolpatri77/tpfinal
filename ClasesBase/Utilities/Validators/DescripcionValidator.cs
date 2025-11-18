using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace ClasesBase.Utilities.Validators
{
    public static class DescripcionValidator
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

        public static ValidationResult ValidarDescripcion(string descripcion)
        {
            if (string.IsNullOrWhiteSpace(descripcion))
                return new ValidationResult(false, "La descripción del curso no puede estar vacía");

            if (descripcion.Length < 5)
                return new ValidationResult(false, "La descripción debe tener al menos 5 caracteres");

            if (descripcion.Length > 100)
                return new ValidationResult(false, "La descripción no puede exceder los 100 caracteres");

            if (!Regex.IsMatch(descripcion, @"^[a-zA-ZáéíóúñÑ0-9\s.,!?-]+$"))
                return new ValidationResult(false, "La descripción contiene caracteres inválidos");

            return new ValidationResult(true);
        }

        // Versión simple que solo retorna bool
        public static bool IsValidDescripcion(string descripcion)
        {
            if (string.IsNullOrWhiteSpace(descripcion))
                return false;

            if (descripcion.Length < 5 || descripcion.Length > 100)
                return false;

            return Regex.IsMatch(descripcion, @"^[a-zA-ZáéíóúñÑ0-9\s.,!?-]+$");
        }
    }
}
