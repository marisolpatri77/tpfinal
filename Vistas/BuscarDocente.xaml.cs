using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using ClasesBase;
using ClasesBase.Utilities.Validators;

namespace Vistas
{
    public partial class BuscarDocente : Window
    {
        public BuscarDocente()
        {
            InitializeComponent();
            btnActualizar.IsEnabled = false;
        }

        private void btnBuscar_Click(object sender, RoutedEventArgs e)
        {
            BuscarDocenteEnBaseDeDatos();
        }

        private void txtBusqueda_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                BuscarDocenteEnBaseDeDatos();
            }
        }

        //BUSCAR DOCENTE
        private void BuscarDocenteEnBaseDeDatos()
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

                Docente docente = TrabajarDocente.TraerDocente(parametroBusqueda);

                if (docente != null)
                {
                    AltaDocente.Docente = docente;
                    MessageBoxCustom.ShowSuccess("Docente encontrado correctamente");
                    btnActualizar.IsEnabled = true;
                    btnEliminar.IsEnabled = true;
                }
                else
                {
                    MessageBoxCustom.ShowSuccess("No se encontró ningún docente con ese ID o DNI");
                    AltaDocente.LimpiarCampos();
                    btnActualizar.IsEnabled = false;
                    btnEliminar.IsEnabled = false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al buscar docente: " + ex.Message, "Error");
            }
        }


        // ACTUALIZAR DOCENTE
        private void btnActualizar_Click(object sender, RoutedEventArgs e)
        {
            if (AltaDocente.Docente != null && AltaDocente.IsValid())
            {
                try
                {
                    TrabajarDocente.update_docente(AltaDocente.Docente);
                    MessageBoxCustom.ShowSuccess("Docente actualizado correctamente");
                    AltaDocente.LimpiarCampos();
                    txtBusqueda.Clear();
                    btnActualizar.IsEnabled = false;
                }
                catch (Exception ex)
                {
                    MessageBoxCustom.ShowError("Error al actualizar docente: " + ex.Message);
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


        //ELIMINAR DOCENTE

        private void btnEliminar_Click(object sender, RoutedEventArgs e)
        {
            // Verificamos si hay un docente cargado en el control
            if (AltaDocente.Docente == null)
            {
                MessageBoxCustom.ShowError("No hay un docente cargado para eliminar.");
                return;
            }

            Docente docente = AltaDocente.Docente;

            // Confirmación antes de eliminar
            MessageBoxResult result = MessageBox.Show(
                "¿Está seguro que desea eliminar al docente " + docente.Doc_Apellido + " " + docente.Doc_Nombre + " DNI: " + docente.Doc_DNI +
                "? Confirmar eliminación.",
                "Confirmación",
                MessageBoxButton.YesNo,
                MessageBoxImage.Warning
            );

            if (result == MessageBoxResult.Yes)
            {
                try
                {
                    TrabajarDocente.delete_docente(docente.Doc_ID);
                    MessageBoxCustom.ShowSuccess("Docente eliminado correctamente.");

                    // Limpiar campos y restablecer el formulario
                    AltaDocente.LimpiarCampos();
                    txtBusqueda.Clear();
                    btnActualizar.IsEnabled = false;
                    btnEliminar.IsEnabled = false;
                }
                catch (Exception ex)
                {
                    MessageBoxCustom.ShowError("Error al eliminar docente: " + ex.Message);
                }
            }
        }
    }
}
