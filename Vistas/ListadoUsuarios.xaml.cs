using System;
using System.Collections.Generic;
using System.ComponentModel;
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
using ClasesBase.Entity;

namespace Vistas
{
    public partial class ListadoUsuarios : Window
    {
        public ListadoUsuarios()
        {
            InitializeComponent();
        }

        private void btnCerrar_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// Este evento se dispara cada vez que el texto en el cuadro de búsqueda cambia.
        /// </summary>
        private void txtFiltroUsuario_TextChanged(object sender, TextChangedEventArgs e)
        {
            // Obtenemos la vista de la colección a la que está enlazada la lista.
            CollectionViewSource cvs = (CollectionViewSource)this.Resources["VISTA_USUARIOS"];
            if (cvs != null && cvs.View != null)
            {
                // Le decimos a la vista que se "refresque". Esto automáticamente
                // volverá a llamar al método de filtrado (FiltroUsuarios_Filter).
                cvs.View.Refresh();
            }
        }

        /// <summary>
        /// Este es el método que decide qué ítems se muestran en la lista.
        /// Se ejecuta por cada usuario en la colección original.
        /// </summary>
        private void FiltroUsuarios_Filter(object sender, FilterEventArgs e)
        {
            Usuario usuario = e.Item as Usuario;
            string filtro = txtFiltroUsuario.Text;

            // Si no hay nada en el cuadro de búsqueda, aceptamos todos los ítems.
            if (string.IsNullOrEmpty(filtro))
            {
                e.Accepted = true;
                return;
            }

            // Comprobamos si el nombre de usuario CONTIENE el texto del filtro,
            // ignorando mayúsculas y minúsculas.
            if (usuario.Usu_NombreUsuario.IndexOf(filtro, StringComparison.OrdinalIgnoreCase) >= 0)
            {
                e.Accepted = true;
            }
            else
            {
                e.Accepted = false;
            }
        }
    
        // --- MÉTODO VISTA PREVIA ---
        private void btnVistaPrevia_Click(object sender, RoutedEventArgs e)
        {
            // 1. Obtenemos la CollectionViewSource desde los recursos de la ventana
            CollectionViewSource cvs = (CollectionViewSource)this.Resources["VISTA_USUARIOS"];
            
            // 2. Creamos una lista nueva
            List<Usuario> listaFiltrada = new List<Usuario>();

            // 3. Recorremos la VISTA (que ya está filtrada y ordenada) y añadimos
            //    cada usuario a nuestra nueva lista.
            if (cvs.View != null)
            {
                foreach (Usuario usuario in cvs.View)
                {
                    listaFiltrada.Add(usuario);
                }
            }

            // llamaremos a la nueva ventana de impresión
            //    pasándole la 'listaFiltrada'.
            //
            VistaPreviaUsuarios oPreview = new VistaPreviaUsuarios(listaFiltrada);
            oPreview.ShowDialog();

        }
    }
}
