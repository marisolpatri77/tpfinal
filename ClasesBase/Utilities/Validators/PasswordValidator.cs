using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace ClasesBase.Utilities.Validators
{
    public static class PasswordValidator
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

        public static ValidationResult ValidarPassword(string password)
        {
            if (string.IsNullOrWhiteSpace(password))
                return new ValidationResult(false, "La contraseña no puede estar vacía");

            if (password.Length < 8)
                return new ValidationResult(false, "La contraseña debe tener al menos 8 caracteres");

            if (!Regex.IsMatch(password, @"[A-Z]"))
                return new ValidationResult(false, "La contraseña debe contener al menos una mayúscula");

            if (!Regex.IsMatch(password, @"[a-z]"))
                return new ValidationResult(false, "La contraseña debe contener al menos una minúscula");

            if (!Regex.IsMatch(password, @"\d"))
                return new ValidationResult(false, "La contraseña debe contener al menos un número");

            return new ValidationResult(true);
        }

        // Versión simple que solo retorna bool
        public static bool IsValidPassword(string password)
        {
            if (string.IsNullOrWhiteSpace(password))
                return false;

            if (password.Length < 8)
                return false;

            if (!Regex.IsMatch(password, @"[A-Z]"))
                return false;

            if (!Regex.IsMatch(password, @"[a-z]"))
                return false;

            if (!Regex.IsMatch(password, @"\d"))
                return false;

            return true;
        }
    }
}
