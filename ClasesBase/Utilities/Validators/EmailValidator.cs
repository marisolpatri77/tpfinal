using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace ClasesBase.Utilities.Validators
{
    public static class EmailValidator
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

        private static readonly Regex EmailRegex = new Regex(
            @"^[a-zA-Z0-9.!#$%&'*+/=?^_`{|}~-]+@[a-zA-Z0-9](?:[a-zA-Z0-9-]{0,61}[a-zA-Z0-9])?(?:\.[a-zA-Z0-9](?:[a-zA-Z0-9-]{0,61}[a-zA-Z0-9])?)*$",
            RegexOptions.Compiled);

        /// <summary>
        /// Valida una dirección de email y retorna resultado detallado
        /// </summary>
        /// <param name="email">Dirección de email a validar</param>
        /// <returns>ValidationResult con el resultado de la validación</returns>
        public static ValidationResult ValidarEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
                return new ValidationResult(false, "El email no puede estar vacío");

            if (email.Length > 254)
                return new ValidationResult(false, "El email no puede exceder los 254 caracteres");

            if (!EmailRegex.IsMatch(email))
                return new ValidationResult(false, "Formato de email inválido");

            // Validar que tenga @ y dominio válido
            if (email.IndexOf('@') <= 0 || email.IndexOf('@') >= email.Length - 1)
                return new ValidationResult(false, "El email debe contener un símbolo @ en posición válida");

            // Validar dominio
            if (!IsDomainValid(email))
                return new ValidationResult(false, "El dominio del email no es válido");

            return new ValidationResult(true);
        }

        /// <summary>
        /// Valida si una dirección de email tiene formato válido (versión simple)
        /// </summary>
        /// <param name="email">Dirección de email a validar</param>
        /// <returns>True si el email es válido, false en caso contrario</returns>
        public static bool IsValid(string email)
        {
            return ValidarEmail(email).IsValid;
        }

        /// <summary>
        /// Valida si el dominio del email es válido
        /// </summary>
        private static bool IsDomainValid(string email)
        {
            try
            {
                var domain = email.Split('@')[1];

                // Verificar que el dominio tenga al menos un punto
                if (!domain.Contains("."))
                    return false;

                // Verificar que no termine con punto
                if (domain.EndsWith("."))
                    return false;

                // Verificar que el dominio no sea demasiado corto
                if (domain.Length < 3)
                    return false;

                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// Normaliza un email (minúsculas y sin espacios)
        /// </summary>
        public static string NormalizeEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
                return string.Empty;

            return email.Trim().ToLower();
        }
    }
}