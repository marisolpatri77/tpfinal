using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;

namespace Vistas
{
    public partial class FrmUsuario : Window
    {
        public FrmUsuario()
        {
            InitializeComponent();
            
        }

        private void btnCancelar_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btnGuardar_Click(object sender, RoutedEventArgs e)
        {
            // Simplemente le damos la orden al control de usuario.
            // No necesitamos saber CÓMO lo hace, solo si tuvo éxito.
            bool guardadoExitoso = altaUsuario.Guardar();

            if (guardadoExitoso)
            {
                // Si se guardó con éxito, podemos cerrar la ventana.
                this.Close();
            }
        }
    }
}
