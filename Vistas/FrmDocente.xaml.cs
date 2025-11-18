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
    /// <summary>
    /// Interaction logic for FrmDocente.xaml
    /// </summary>
    public partial class FrmDocente : Window
    {
        string errores = "";
        public FrmDocente()
        {
            InitializeComponent();
        }

        private void Limpiar_Campos()
        {

            altaDocente.LimpiarCampos();
        }

        private void btnCancelar_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btnGuardar_Click(object sender, RoutedEventArgs e)
        {
            var confirmacion = new MessageBoxConfirm("¿Desea guardar el Docente?", "Atención");
            bool? resultado = confirmacion.ShowDialog();

            if (resultado == true)
            {
                //uso de IDataErrorInfo
                if (altaDocente.IsValid())
                {
                    GuardarDocente();
                }
                else
                {
                    MessageBoxCustom.ShowError("Por favor, corrija los errores en el formulario antes de guardar.");
                }

            }
        }

        // MÉTODO PARA GUARDAR EN BASE DE DATOS
        private void GuardarDocente()
        {
            try
            {
                // Obtener el docente del control
                Docente oDocente = altaDocente.Docente;
                TrabajarDocente.insert_docente(oDocente);

                MessageBoxCustom.ShowSuccess("Docente guardado correctamente");
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
            var resultadoNombre = StringValidatorNombreApellido.ValidarNombreApellido("Nombre", altaDocente.txtNombre.Text);
            var resultadoApellido = StringValidatorNombreApellido.ValidarNombreApellido("Apellido", altaDocente.txtApellido.Text);
            var resultadoDni = DniValidator.ValidarDni(altaDocente.txtDNI.Text);
            var resultadoEmail = EmailValidator.ValidarEmail(altaDocente.txtEmail.Text);

            if (resultadoNombre.IsValid && resultadoApellido.IsValid &&
                resultadoEmail.IsValid && resultadoDni.IsValid
                )
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
