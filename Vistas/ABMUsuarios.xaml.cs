using System;
using System.Windows;
using System.Windows.Data;
using System.Collections.ObjectModel;
using ClasesBase.DataAccess;
using ClasesBase.Entity;
using System.Linq;
using System.Windows.Input;

namespace Vistas
{
    public partial class ABMUsuarios : Window
    {
        private CollectionView vista;
        private ObservableCollection<Usuario> listaUsuarios;
        private readonly TrabajarUsuario trabajarUsuario = new TrabajarUsuario();
        private bool esNuevo = false;

        public ABMUsuarios()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                ObjectDataProvider odp = (ObjectDataProvider)this.Resources["LIST_USER"];
                listaUsuarios = odp.Data as ObservableCollection<Usuario>;
                vista = (CollectionView)CollectionViewSource.GetDefaultView(pnlPrincipal.DataContext);

                if (listaUsuarios != null && listaUsuarios.Any())
                {
                    ucAltaUsuario.CargarDatos((Usuario)vista.CurrentItem);
                }
            }
            catch (Exception ex)
            {
                MessageBoxCustom.ShowError("Error fatal al cargar los usuarios: \n\n" + ex.Message);
                this.Close();
                return;
            }

            btnNuevo.Click += btnNuevo_Click;
            btnModificar.Click += btnModificar_Click;
            btnGuardar.Click += btnGuardar_Click;
            btnEliminar.Click += btnEliminar_Click;
            btnCancelar.Click += btnCancelar_Click;

            GestionarBotones(true);
        }

        private void GestionarBotones(bool estadoInicial)
        {
            btnNuevo.IsEnabled = estadoInicial;
            btnGuardar.IsEnabled = !estadoInicial;
            btnCancelar.IsEnabled = !estadoInicial;

            ucAltaUsuario.IsEnabled = !estadoInicial;

            btnPrimero.IsEnabled = estadoInicial;
            btnAnterior.IsEnabled = estadoInicial;
            btnSiguiente.IsEnabled = estadoInicial;
            btnUltimo.IsEnabled = estadoInicial;

            // Lógica de habilitación de Modificar y Eliminar
            if (estadoInicial)
            {
                Usuario usuarioActual = (Usuario)vista.CurrentItem;
                bool puedeModificarOEliminar = (usuarioActual != null && usuarioActual.Usu_ID != 1);
                btnModificar.IsEnabled = puedeModificarOEliminar;
                btnEliminar.IsEnabled = puedeModificarOEliminar;
            }
            else
            {
                btnModificar.IsEnabled = false;
                btnEliminar.IsEnabled = false;
            }
        }

        private void btnNuevo_Click(object sender, RoutedEventArgs e)
        {
            esNuevo = true;
            GestionarBotones(false);
            ucAltaUsuario.LimpiarCampos();
            ucAltaUsuario.txtNombreUsuario.Focus();
        }

        private void btnModificar_Click(object sender, RoutedEventArgs e)
        {
            esNuevo = false;
            GestionarBotones(false);
        }

        private void btnGuardar_Click(object sender, RoutedEventArgs e)
        {
            if (esNuevo)
            {
                if (ucAltaUsuario.Guardar())
                {
                    listaUsuarios = trabajarUsuario.TraerUsuarios();
                    pnlPrincipal.DataContext = listaUsuarios;
                    vista = (CollectionView)CollectionViewSource.GetDefaultView(pnlPrincipal.DataContext);
                    vista.MoveCurrentToLast();
                    GestionarBotones(true);
                    esNuevo = false;
                }
            }
            else // Lógica de Modificación
            {
                if (ucAltaUsuario.Validar())
                {
                    Usuario usuarioActual = (Usuario)vista.CurrentItem;
                    if (usuarioActual != null)
                    {
                        Usuario usuarioModificado = new Usuario
                        {
                            Usu_ID = usuarioActual.Usu_ID,
                            Usu_NombreUsuario = ucAltaUsuario.txtNombreUsuario.Text,
                            Usu_Contrasenia = ucAltaUsuario.txtPassword.Password,
                            Usu_ApellidoNombre = ucAltaUsuario.txtApellido.Text + " " + ucAltaUsuario.txtNombre.Text,
                            Rol_ID = (int)ucAltaUsuario.cmbRol.SelectedValue
                        };

                        trabajarUsuario.ModificarUsuario(usuarioModificado);
                        MessageBoxCustom.ShowSuccess("Usuario modificado con éxito.");

                        int index = listaUsuarios.IndexOf(usuarioActual);
                        if (index != -1)
                        {
                            listaUsuarios[index] = usuarioModificado;
                        }

                        GestionarBotones(true);
                        // Refrescar la vista para asegurar que los botones se actualicen
                        vista.Refresh();
                    }
                }
            }
        }

        private void btnEliminar_Click(object sender, RoutedEventArgs e)
        {
            Usuario usuarioSeleccionado = (Usuario)vista.CurrentItem;
            if (usuarioSeleccionado != null)
            {
                string mensaje = string.Format("¿Está seguro de que desea eliminar a {0}?", usuarioSeleccionado.Usu_NombreUsuario);
                MessageBoxResult resultado = MessageBox.Show(mensaje, "Confirmación de eliminación", MessageBoxButton.YesNo, MessageBoxImage.Warning);

                if (resultado == MessageBoxResult.Yes)
                {
                    trabajarUsuario.EliminarUsuario(usuarioSeleccionado.Usu_ID);
                    listaUsuarios.Remove(usuarioSeleccionado);
                    MessageBoxCustom.ShowSuccess("Usuario eliminado.");
                    ucAltaUsuario.CargarDatos((Usuario)vista.CurrentItem);
                }
            }
        }

        private void btnCancelar_Click(object sender, RoutedEventArgs e)
        {
            GestionarBotones(true);
            ucAltaUsuario.CargarDatos((Usuario)vista.CurrentItem); // Restaura los datos originales
            esNuevo = false;
        }

        private void MoverEnVista(Action<CollectionView> accion)
        {
            if (listaUsuarios != null && listaUsuarios.Any())
            {
                accion(vista);
                ucAltaUsuario.CargarDatos((Usuario)vista.CurrentItem);
                // Después de mover, reevaluamos el estado de los botones
                GestionarBotones(true);
            }
        }

        private void btnPrimero_Click(object sender, RoutedEventArgs e)
        {
            MoverEnVista(v => v.MoveCurrentToFirst());
        }

        private void btnAnterior_Click(object sender, RoutedEventArgs e)
        {
            MoverEnVista(v => { if (!v.IsCurrentBeforeFirst) v.MoveCurrentToPrevious(); });
        }

        private void btnSiguiente_Click(object sender, RoutedEventArgs e)
        {
            MoverEnVista(v => { if (!v.IsCurrentAfterLast) v.MoveCurrentToNext(); });
        }

        private void btnUltimo_Click(object sender, RoutedEventArgs e)
        {
            MoverEnVista(v => v.MoveCurrentToLast());
        }
    }
}