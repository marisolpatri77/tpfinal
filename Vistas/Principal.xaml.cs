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

namespace Vistas
{
    /// <summary>
    /// Lógica de interacción para Principal.xaml
    /// </summary>
    public partial class Principal : Window
    {
        public Principal()
        {
            InitializeComponent();
            InicializarVentana();
        }

        #region Métodos de Inicialización

        private void InicializarVentana()
        {
            // Ya no es necesario llamar a MostrarPantallaBienvenida() aquí,
            // porque el XAML ya la muestra por defecto.
        }

        #endregion

        #region Métodos de Gestión de Contenido

        /// <summary>
        /// Muestra la pantalla de bienvenida y elimina otros controles
        /// </summary>
        public void MostrarPantallaBienvenida()
        {
            // Elimina cualquier UserControl que se haya cargado previamente,
            // pero deja intacto el panel de bienvenida.
            var otrosControles = ContentArea.Children.OfType<UserControl>().ToList();
            foreach (var control in otrosControles)
            {
                ContentArea.Children.Remove(control);
            }

            // Asegura que el panel de bienvenida sea visible.
            PantallaBienvenida.Visibility = Visibility.Visible;
        }

        /// <summary>
        /// Carga un UserControl en el área de contenido principal
        /// </summary>
        /// <param name="control">UserControl a cargar</param>
        private void CargarUserControl(UserControl control)
        {
            // Oculta la pantalla de bienvenida antes de cargar un nuevo control.
            PantallaBienvenida.Visibility = Visibility.Collapsed;

            // Limpia cualquier UserControl anterior.
            var otrosControles = ContentArea.Children.OfType<UserControl>().ToList();
            foreach (var uc in otrosControles)
            {
                ContentArea.Children.Remove(uc);
            }

            // Agrega el nuevo UserControl.
            ContentArea.Children.Add(control);
        }

        #endregion

        #region Eventos del Menú Sistema

        private void menuUsuarios_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // Ejemplo de cómo usarías el método CargarUserControl:
                // CargarUserControl(new VistaGestionUsuarios()); 
                MessageBox.Show("Módulo de Usuarios - En desarrollo", "Información", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                // CORREGIDO: Se muestra el mensaje de error real.
                MessageBox.Show("Error al abrir módulo de usuarios: " + ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void menuSalir_Click(object sender, RoutedEventArgs e)
        {
            // OPTIMIZADO: Llama al cierre de la ventana para centralizar la lógica.
            this.Close();
        }

        #endregion

        #region Eventos del Menú Académico

        private void menuCursos_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // CargarUserControl(new VistaGestionCursos());
                MessageBox.Show("Módulo de Cursos - En desarrollo", "Información", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al abrir módulo de cursos: " + ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void menuDocentes_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // CargarUserControl(new VistaGestionDocentes());
                MessageBox.Show("Módulo de Docentes - En desarrollo", "Información", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al abrir módulo de docentes: " + ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void menuAlumnos_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // CargarUserControl(new VistaGestionAlumnos());
                MessageBox.Show("Módulo de Alumnos - En desarrollo", "Información", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al abrir módulo de alumnos: " + ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        #endregion

        #region Eventos de Formularios de Alta

        private void AltaUsuario_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // Esta lógica está bien si FrmUsuario es una ventana emergente (Window).
                FrmUsuario oFrmUsuario = new FrmUsuario();
                oFrmUsuario.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al abrir formulario de Usuario: " + ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }



        private void AltaCurso_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // Esta lógica está bien si FrmCurso es una ventana emergente (Window).
                FrmCurso oFrmCurso = new FrmCurso();
                oFrmCurso.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al abrir formulario de curso: " + ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void AltaDocente_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                FrmDocente oFrmDocente = new FrmDocente();
                oFrmDocente.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al abrir formulario de docente: " + ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }


        private void ModificarDocente_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                BuscarDocente oFrmModificarDocente = new BuscarDocente();
                oFrmModificarDocente.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al abrir formulario de docente: " + ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void AltaAlumno_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                FrmAlumno oFrmAlumno = new FrmAlumno();
                oFrmAlumno.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al abrir formulario de alumno: " + ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void ModificarAlumno_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                BuscarAlumno oFrmModificarAlumno = new BuscarAlumno();
                oFrmModificarAlumno.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al abrir formulario de alumno: " + ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        #endregion

        #region Eventos de Otros Módulos

        private void menuInscripciones_Click(object sender, RoutedEventArgs e)
        {
            InscripcionAlta ventanaInscripciones = new InscripcionAlta();
            ventanaInscripciones.Show();
        }

        private void menuReportes_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // CargarUserControl(new VistaReportes());
                MessageBox.Show("Módulo de Reportes - En desarrollo", "Información", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al abrir módulo de reportes: " + ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        #endregion

        #region Eventos de Accesos Rápidos

        private void btnNuevoAlumno_Click(object sender, RoutedEventArgs e)
        {
            AltaAlumno_Click(sender, e);
        }

        private void btnNuevoCurso_Click(object sender, RoutedEventArgs e)
        {
            AltaCurso_Click(sender, e);
        }

        private void btnNuevoDocente_Click(object sender, RoutedEventArgs e)
        {
            AltaDocente_Click(sender, e);
        }

        #endregion

        #region Eventos de Ventana

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            // Aquí puedes poner lógica que necesites ejecutar cuando la ventana se cargue.
        }

        protected override void OnClosing(System.ComponentModel.CancelEventArgs e)
        {
            // OPTIMIZADO: Lógica de confirmación centralizada.
            var confirmacion = new MessageBoxConfirm("¿Desea Salir del sistema?", "Atención");
            bool? resultado = confirmacion.ShowDialog();
            if (resultado == false)
            {
                e.Cancel = true; // Cancela el cierre de la ventana.
            }
            else
            {
                Application.Current.Shutdown(); // Cierra la aplicación completamente.
            }
        }

        #endregion

        private void menuListarCursos_Click(object sender, RoutedEventArgs e)
        {
            ListarCursos listarCursos = new ListarCursos();
            listarCursos.Owner = this;
            listarCursos.ShowDialog();
        }

        private void btnEstadoCursos_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                EstadoCursos ventanaEstado = new EstadoCursos();
                ventanaEstado.Owner = this; // para que se centre respecto al principal
                ventanaEstado.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al abrir Estado de Cursos: " + ex.Message,
                                "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        
        private void menuABMUsuarios_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                ABMUsuarios ventanaABM = new ABMUsuarios();
                ventanaABM.ShowDialog(); // ShowDialog abre la ventana de forma modal
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al abrir ABM de Usuarios: " + ex.Message,
                                "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        private void menuListadoUsuarios_Click(object sender, RoutedEventArgs e)
        {
            ListadoUsuarios ventanaListado = new ListadoUsuarios();
            ventanaListado.Show();
        }

     

    }
}