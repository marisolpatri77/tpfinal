using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;

namespace ClasesBase.Entity
{
    public class ConversorDeEstados : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
                return new SolidColorBrush(Color.FromRgb(204, 204, 204));

            string estado = value.ToString().ToLower().Trim();

            switch (estado)
            {
                case "programado":
                    return new SolidColorBrush(Color.FromRgb(76, 175, 80));    // Verde
                case "en_curso":
                    return new SolidColorBrush(Color.FromRgb(255, 152, 0));     // Naranja
                case "finalizado":
                    return new SolidColorBrush(Color.FromRgb(33, 150, 243));    // Azul
                case "cancelado":
                    return new SolidColorBrush(Color.FromRgb(153, 5, 6));       // Rojo
                default:
                    return new SolidColorBrush(Color.FromRgb(204, 204, 204));   // Gris
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException("No se requiere conversión inversa.");
        }
    }
}
