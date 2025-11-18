using System;
using System.Windows;
using System.Windows.Controls;

namespace Vistas
{
    public partial class EstadoCursos : Window
    {
        public EstadoCursos()
        {
            InitializeComponent();
            cboEstados.SelectionChanged += CboEstados_SelectionChanged;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            if (cboEstados.Items.Count > 0)
            {
                cboEstados.SelectedIndex = 0;
            }
            else
            {
                txtEstadoSeleccionado.Text = "No hay datos";
                txtDescripcion.Text = "No se pudieron cargar los estados desde el XML";
            }
        }

        private void CboEstados_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ActualizarDescripcion();
        }

        private void ActualizarDescripcion()
        {
            if (cboEstados.SelectedValue == null)
                return;

            string estado = cboEstados.SelectedValue.ToString();
            txtEstadoSeleccionado.Text = estado;

            switch (estado.ToLower())
            {
                case "programado":
                    txtDescripcion.Text = "El curso está programado y listo para iniciar";
                    break;
                case "en_curso":
                    txtDescripcion.Text = "El curso se encuentra actualmente en desarrollo";
                    break;
                case "finalizado":
                    txtDescripcion.Text = "El curso ha finalizado exitosamente";
                    break;
                case "cancelado":
                    txtDescripcion.Text = "El curso ha sido cancelado";
                    break;
                default:
                    txtDescripcion.Text = "Estado no reconocido";
                    break;
            }
        }

        private void btnCerrar_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
