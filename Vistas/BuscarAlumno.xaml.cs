using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using ClasesBase;
using ClasesBase.Utilities.Validators;

namespace Vistas
{
    public partial class BuscarAlumno : Window
    {
        public BuscarAlumno()
        {
            InitializeComponent();
            btnActualizar.IsEnabled = false;
        }

        private void btnBuscar_Click(object sender, RoutedEventArgs e)
        {
            BuscarAlumnoEnBaseDeDatos();
        }

        private void txtBusqueda_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                BuscarAlumnoEnBaseDeDatos();
            }
        }

        //BUCAR ALUMNO
        private void BuscarAlumnoEnBaseDeDatos()
        {
            if (string.IsNullOrWhiteSpace(txtBusqueda.Text))
            {
                MessageBox.Show("Ingrese un ID o DNI para buscar");
                return;
            }

            try
            {
                string parametroBusqueda = txtBusqueda.Text.Trim();

                //Declarar la variable id fuera del TryParse

                int id;
                bool esNumero = int.TryParse(parametroBusqueda, out id);

                // Validar usando tu DniValidator
                if (!esNumero)
                {
                    var validacionDNI = DniValidator.ValidarDni(parametroBusqueda);
                    if (!validacionDNI.IsValid)
                    {
                        MessageBox.Show(validacionDNI.ErrorMessage);
                        return;
                    }
                }

                Alumno alumno = TrabajarAlumno.TraerAlumno(parametroBusqueda);

                if (alumno != null)
                {
                    alumnoControl.Alumno = alumno;
                    MessageBoxCustom.ShowSuccess("Alumno encontrado correctamente");
                    btnActualizar.IsEnabled = true;
                    btnEliminar.IsEnabled = true;
                }
                else
                {
                    MessageBoxCustom.ShowSuccess("No se encontró ningún alumno con ese ID o DNI");
                    alumnoControl.LimpiarCampos();
                    btnActualizar.IsEnabled = false;
                    btnEliminar.IsEnabled = false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al buscar alumno: " + ex.Message, "Error");
            }
        }


        // ACTUALIZAR ALUMNO
        private void btnActualizar_Click(object sender, RoutedEventArgs e)
        {
            if (alumnoControl.Alumno != null && alumnoControl.IsValid())
            {
                try
                {
                    TrabajarAlumno.update_alumno(alumnoControl.Alumno);
                    MessageBoxCustom.ShowSuccess("Alumno actualizado correctamente");
                    // para limpiar el formulario
                    alumnoControl.LimpiarCampos();
                    txtBusqueda.Clear();
                    btnActualizar.IsEnabled = false;
                }
                catch (Exception ex)
                {
                    MessageBoxCustom.ShowError("Error al actualizar alumno: " + ex.Message);
                }
            }
            else
            {
                MessageBoxCustom.ShowError("No hay datos válidos para actualizar");
            }
        }

        private void btnCancelar_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }


        //ELIMINAR ALUMNO

        private void btnEliminar_Click(object sender, RoutedEventArgs e)
        {
             // Verificamos si hay un alumno cargado en el control
            if (alumnoControl.Alumno == null)
            {
                MessageBoxCustom.ShowError("No hay un alumno cargado para eliminar.");
                return;
            }

            Alumno alumno = alumnoControl.Alumno;

            // Confirmación antes de eliminar
            MessageBoxResult result = MessageBox.Show(
                "¿Está seguro que desea eliminar al alumno " + alumno.Alu_Apellido + " " + alumno.Alu_Nombre + " DNI: " + alumno.Alu_DNI +
                "? Confirmar eliminación.",
                "Confirmación",
                MessageBoxButton.YesNo,
                MessageBoxImage.Warning
            );

            if (result == MessageBoxResult.Yes)
            {
                try
                {
                    // Llamada al método en la capa de datos
                    TrabajarAlumno.delete_alumno(alumno.Alu_ID);

                    // Mensaje de éxito
                    MessageBoxCustom.ShowSuccess("Alumno eliminado correctamente.");

                    // Limpiar campos y restablecer el formulario
                    alumnoControl.LimpiarCampos();
                    txtBusqueda.Clear();
                    btnActualizar.IsEnabled = false;
                    btnEliminar.IsEnabled = false;
                }
                catch (Exception ex)
                {
                    MessageBoxCustom.ShowError("Error al eliminar alumno: " + ex.Message);
                }
            }
        }
    }
}