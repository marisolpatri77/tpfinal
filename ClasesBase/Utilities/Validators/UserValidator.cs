using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace ClasesBase.Utilities.Validators
{
    public static class UserValidator
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

        public static ValidationResult ValidarUsuario(string usuario)
        {
            if (string.IsNullOrWhiteSpace(usuario))
                return new ValidationResult(false,"El usuario no puede estar vacío");

            if (usuario.Length < 2 || usuario.Length > 10)
                return new ValidationResult(false,"El usuario debe tener entre 2 y 10 caracteres");

            if (!Regex.IsMatch(usuario, @"^(?=.*[a-zA-ZáéíóúñÑ])(?=.*\d)[a-zA-ZáéíóúñÑ\d]+$"))
                return new ValidationResult(false, "El usuario debe contener letras y números");

            return new ValidationResult(true);
        }

        // Versión simple que solo retorna bool
        public static bool IsValidUsuario(string usuario)
        {
            if (string.IsNullOrWhiteSpace(usuario))
                return false;

            if (usuario.Length < 2 || usuario.Length > 100)
                return false;

            return Regex.IsMatch(usuario, @"^(?=.*[a-zA-ZáéíóúñÑ])(?=.*\d)[a-zA-ZáéíóúñÑ\d]+$");
        }
    }
}
