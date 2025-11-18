using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace ClasesBase.Utilities.Validators
{
    public static class ComboBoxValidator
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

        public static ValidationResult ValidarSeleccion(object comboBox, string nombreCampo)
        {
            dynamic cmb = comboBox;
            if (cmb.SelectedIndex == -1)
                return new ValidationResult(false, "Debe seleccionar un " + nombreCampo);

            if (cmb.SelectedItem == null)
                return new ValidationResult(false, "La selección de " + nombreCampo  + " no es válida");

            return new ValidationResult(true);
        }

        // Versión sobrecargada sin nombre de campo
        public static ValidationResult ValidarSeleccion(object comboBox)
        {
            return ValidarSeleccion(comboBox, "elemento");
        }

        // Versión simple que solo retorna bool
        public static bool IsValidSeleccion(object comboBox)
        {    dynamic cmb = comboBox;
            return cmb.SelectedIndex != -1 && cmb.SelectedItem != null;
        }

        // Validar que no sea el primer item (si se usa placeholder)
        public static ValidationResult ValidarSeleccionNoPlaceholder(object comboBox, string nombreCampo)
        {
            dynamic cmb = comboBox;
            if (cmb.SelectedIndex <= 0) // Asumiendo que el índice 0 es el placeholder
                return new ValidationResult(false, "Debe seleccionar un " + nombreCampo + " válido");

            if (cmb.SelectedItem == null)
                return new ValidationResult(false, "La selección de " +nombreCampo + " no es válida");

            return new ValidationResult(true);
        }
    }
}
