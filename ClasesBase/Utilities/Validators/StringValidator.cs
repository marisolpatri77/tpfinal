using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace ClasesBase.Utilities.Validators
{
    public static class StringValidatorNombreApellido
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

        public static ValidationResult ValidarNombreApellido(string campo, string nombre)
        {
            if (string.IsNullOrWhiteSpace(nombre))
                return new ValidationResult(false, campo + " no puede estar vacío");

            if (nombre.Length < 2 || nombre.Length > 50)
                return new ValidationResult(false, campo + " debe tener entre 2 y 50 caracteres");

            if (!Regex.IsMatch(nombre, @"^[a-zA-ZáéíóúñÑ\s]+$"))
                return new ValidationResult(false, campo + " solo puede contener letras y espacios");

            return new ValidationResult(true);
        }

        // Versión simple que solo retorna bool
        public static bool IsValidNombreApellido(string nombre)
        {
            if (string.IsNullOrWhiteSpace(nombre))
                return false;

            if (nombre.Length < 2 || nombre.Length > 50)
                return false;

            return Regex.IsMatch(nombre, @"^[a-zA-ZáéíóúñÑ\s]+$");
        }
    }
}
