using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using ClasesBase;
using ClasesBase.Utilities.Validators;

namespace Vistas
{
    public partial class FrmAlumno : Window
    {
        string errores = "";
        public FrmAlumno()
        {
            InitializeComponent();
        }

        private void btnCancelar_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Limpiar_Campos()
        {
            altaAlumno.LimpiarCampos(); // Usar el método del control
        }

        private void btnGuardar_Click(object sender, RoutedEventArgs e)
        {
            var confirmacion = new MessageBoxConfirm("¿Desea guardar el Alumno?", "Atención");
            bool? resultado = confirmacion.ShowDialog();

            if (resultado == true)
            {
                // OPCIÓN 1: Usar IDataErrorInfo (Recomendado)
                if (altaAlumno.IsValid())
                {
                    GuardarAlumno();
                }
                else
                {
                    MessageBoxCustom.ShowError("Por favor, corrija los errores en el formulario antes de guardar.");
                }
              
            }
        }

        // MÉTODO PARA GUARDAR EN BASE DE DATOS
        private void GuardarAlumno()
        {
            try
            {
                // Obtener el alumno del control
                Alumno oAlumno = altaAlumno.Alumno;

                // Guardar en la base de datos
                TrabajarAlumno.insert_alumno(oAlumno);

                MessageBoxCustom.ShowSuccess("Alumno guardado correctamente");
                Limpiar_Campos();
            }
            catch (Exception ex)
            {
                MessageBoxCustom.ShowError("Error al guardar en la base de datos: " + ex.Message);
            }
        }

        private Boolean verificarCampos()
        {
            Boolean verificado = false;
            var resultadoNombre = StringValidatorNombreApellido.ValidarNombreApellido("Nombre", altaAlumno.txtNombre.Text);
            var resultadoApellido = StringValidatorNombreApellido.ValidarNombreApellido("Apellido", altaAlumno.txtApellido.Text);
            var resultadoDni = DniValidator.ValidarDni(altaAlumno.txtDNI.Text);
            var resultadoEmail = EmailValidator.ValidarEmail(altaAlumno.txtEmail.Text);

            if (resultadoNombre.IsValid && resultadoApellido.IsValid &&
                resultadoEmail.IsValid && resultadoDni.IsValid)
            {
                verificado = true;
            }
            else
            {
                if (!resultadoNombre.IsValid)
                {
                    errores = resultadoNombre.ErrorMessage + "\n";
                }

                if (!resultadoApellido.IsValid)
                {
                    errores = errores + " " + resultadoApellido.ErrorMessage + "\n";
                }

                if (!resultadoDni.IsValid)
                {
                    errores = errores + " " + resultadoDni.ErrorMessage + "\n";
                }

                if (!resultadoEmail.IsValid)
                {
                    errores = errores + " " + resultadoEmail.ErrorMessage + "\n";
                }
            }

            return verificado;
        }
    }
}