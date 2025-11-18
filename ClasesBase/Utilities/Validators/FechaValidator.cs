using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ClasesBase.Utilities.Validators
{
    public static class FechaValidator
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

        // Nueva versión que acepta DateTime?
        public static ValidationResult ValidarFecha(DateTime? fecha, string nombreCampo = "fecha")
        {
            if (!fecha.HasValue)
                return new ValidationResult(false, "La fecha " + nombreCampo + " no puede estar vacía");

            if (fecha.Value == DateTime.MinValue)
                return new ValidationResult(false, "La fecha" + nombreCampo +" no es válida");

            return new ValidationResult(true);
        }

        public static ValidationResult ValidarFechaFutura(DateTime? fecha, string tipoFecha)
        {
            var resultado = ValidarFecha(fecha, tipoFecha);
            if (!resultado.IsValid)
                return resultado;

            if (fecha.Value < DateTime.Today)
                return new ValidationResult(false, "La fecha " + tipoFecha + " no puede ser anterior al día actual");

            return new ValidationResult(true);
        }

        public static ValidationResult ValidarRangoFechas(DateTime? fechaInicio, DateTime? fechaFin)
        {
            var resultadoInicio = ValidarFecha(fechaInicio, "de inicio");
            if (!resultadoInicio.IsValid)
                return resultadoInicio;

            var resultadoFin = ValidarFecha(fechaFin, "de fin");
            if (!resultadoFin.IsValid)
                return resultadoFin;

            if (fechaInicio.Value > fechaFin.Value)
                return new ValidationResult(false, "La fecha de inicio no puede ser posterior a la fecha de fin");

            return new ValidationResult(true);
        }

        // Métodos originales para compatibilidad
        public static ValidationResult ValidarFecha(DateTime fecha)
        {
            if (fecha == DateTime.MinValue)
                return new ValidationResult(false, "La fecha no es válida");

            return new ValidationResult(true);
        }

        public static ValidationResult ValidarFechaFutura(DateTime fecha, string tipoFecha)
        {
            return ValidarFechaFutura((DateTime?)fecha, tipoFecha);
        }

        public static ValidationResult ValidarRangoFechas(DateTime fechaInicio, DateTime fechaFin)
        {
            return ValidarRangoFechas((DateTime?)fechaInicio, (DateTime?)fechaFin);
        }
    }
}
