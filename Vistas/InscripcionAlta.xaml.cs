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
using System.Data;
using ClasesBase.DataAccess;
using ClasesBase.Utilities.Validators;

namespace Vistas
{
    /// <summary>
    /// Lógica de interacción para InscripcionAlta.xaml
    /// </summary>
    public partial class InscripcionAlta : Window
    {
   
       private int alu_id;
        public InscripcionAlta()
        {
            try
            {
                Console.WriteLine("Iniciando constructor...");
                InitializeComponent();
                Console.WriteLine("InitializeComponent completado");

                CargarCursosDesdeBD();
                Console.WriteLine("CargarCursosDesdeBD completado");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error en constructor: " + ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void CargarCursosDesdeBD()
        {
            try
            {
                Console.WriteLine("Iniciando CargarCursosDesdeBD...");

                DataTable dt = TrabajarCursos.obtener_curso_programado();

                if (dt == null)
                {
                    MessageBox.Show("No se pudieron cargar los cursos. La base de datos no respondió.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                if (dt.Rows.Count == 0)
                {
                    MessageBox.Show("No hay cursos programados disponibles.", "Información", MessageBoxButton.OK, MessageBoxImage.Information);
                    return;
                }

                Console.WriteLine("Cursos encontrados en BD: " + dt.Rows.Count);

                // Convertir DataTable a lista de objetos Curso (incluyendo fechas)
                var cursos = dt.AsEnumerable()
                             .Select(r => new Curso
                             {
                                 Cur_ID = r.Field<int>("cur_id"),
                                 Cur_Nombre = r.Field<string>("cur_nombre"),
                                 Cur_FechaInicio = r.Field<DateTime>("cur_fecha_inicio"),
                                 Cur_FechaFin = r.Field<DateTime>("cur_fecha_fin")
                             })
                             .ToList();

                Console.WriteLine("Cursos convertidos: " + cursos.Count);

                // Asignar datos al ComboBox
                cmbCurso.ItemsSource = cursos;
                cmbCurso.SelectedValuePath = "Cur_ID";

                Console.WriteLine("Carga desde BD completada - " + cursos.Count + " cursos cargados");

            }
            catch (Exception ex)
            {
                string mensajeError = "Error al cargar cursos desde BD: " + ex.Message;
                if (ex.InnerException != null)
                {
                    mensajeError = mensajeError + "\nDetalle: " + ex.InnerException.Message;
                }

                Console.WriteLine(mensajeError);
                MessageBox.Show(mensajeError, "Error Crítico", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void cmbCurso_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                Console.WriteLine("SelectionChanged iniciado");

                if (cmbCurso.SelectedItem == null)
                {
                    Console.WriteLine("No hay item seleccionado");
                    // Limpiar fechas si no hay selección
                    lblFechaInicio.Content = "--/--/----";
                    lblFechaFin.Content = "--/--/----";
                    return;
                }

                // Obtener el objeto seleccionado como Curso
                Curso cursoSeleccionado = cmbCurso.SelectedItem as Curso;

                if (cursoSeleccionado != null)
                {
                    int idCurso = cursoSeleccionado.Cur_ID;
                    string nombreCurso = cursoSeleccionado.Cur_Nombre;

                    Console.WriteLine("Curso seleccionado - ID: " + idCurso + ", Nombre: " + nombreCurso);

                    // Mostrar las fechas del curso seleccionado
                    lblFechaInicio.Content = cursoSeleccionado.Cur_FechaInicio.ToString("dd/MM/yyyy");
                    lblFechaFin.Content = cursoSeleccionado.Cur_FechaFin.ToString("dd/MM/yyyy");
                }

            }
            catch (Exception ex)
            {
                string errorMsg = "Error en SelectionChanged: " + ex.Message;
                Console.WriteLine(errorMsg);
                MessageBox.Show(errorMsg, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void btnInscribir_Click(object sender, RoutedEventArgs e)
        {
            // Verificar que se haya seleccionado un curso
            if (cmbCurso.SelectedItem == null)
            {
                MessageBoxCustom.ShowWarning("Por favor, seleccione un curso antes de inscribir.");
                return;
            }

            // Verificar que se haya buscado y seleccionado un alumno (aquí agregarás esa lógica después)
            if (string.IsNullOrEmpty(txtBusqueda.Text))
            {
                 MessageBoxCustom.ShowWarning("Por favor, busque un alumno por DNI antes de inscribir.");
                return;
            }

            Curso cursoSeleccionado = cmbCurso.SelectedItem as Curso;
            bool repetido = TrabajarInscripcion.alumno_ya_inscripto(alu_id, cursoSeleccionado.Cur_ID);
            if (repetido) {
                MessageBoxCustom.ShowError("El alumno ya está inscripto en este curso.");
                borderAlumnoInfo.Visibility = Visibility.Visible;
                return;
            }
            
            TrabajarInscripcion.insertar_inscripcion(DateTime.Now, cursoSeleccionado.Cur_ID, alu_id, 5);
            MessageBoxCustom.ShowSuccess("Inscripcion Realizada");
            Limpiar_Campos();
            
        }

        private void Limpiar_Campos()
        {
            txtBusqueda.Clear();
            borderAlumnoInfo.Visibility = Visibility.Collapsed;
            cmbCurso.SelectedIndex = -1;
            
        }


        private void btnBuscar_Click(object sender, RoutedEventArgs e)
        {
            BuscarAlumnoPorDNI();
        }

        private void txtBusqueda_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                BuscarAlumnoPorDNI();
            }
        }

        private void BuscarAlumnoPorDNI()
        {
            string dni = txtBusqueda.Text.Trim();
              var resultadoDni = DniValidator.ValidarDni(dni);
            if(!resultadoDni.IsValid){
                MessageBoxCustom.ShowInfo(resultadoDni.ErrorMessage);
                return;
            }

            if (string.IsNullOrEmpty(dni))
            {
                MessageBox.Show("Por favor, ingrese un DNI para buscar.", "Advertencia", MessageBoxButton.OK, MessageBoxImage.Warning);
                borderAlumnoInfo.Visibility = Visibility.Collapsed;
                borderAlumnoNoEncontrado.Visibility = Visibility.Collapsed;
                return;
            }

            try
            {
                Alumno alumno = TrabajarAlumno.TraerAlumno(dni);

                if (alumno != null)
                {
                    string nombre = alumno.Alu_Nombre;
                    string apellido = alumno.Alu_Apellido;
                    alu_id = alumno.Alu_ID;

                    lblAlumnoInfo.Content = apellido + ", " + nombre;
                    borderAlumnoInfo.Visibility = Visibility.Visible;
                    borderAlumnoNoEncontrado.Visibility = Visibility.Collapsed;

                   MessageBoxCustom.ShowSuccess("Alumno encontrado: " + apellido + ", " + nombre);
                }
                else
                {
                    borderAlumnoInfo.Visibility = Visibility.Collapsed;
                    borderAlumnoNoEncontrado.Visibility = Visibility.Visible;
                    MessageBoxCustom.ShowError("No se encontró ningún alumno con el DNI ingresado.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al buscar alumno: " + ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                borderAlumnoInfo.Visibility = Visibility.Collapsed;
                borderAlumnoNoEncontrado.Visibility = Visibility.Collapsed;
            }
        }

        private void btnCancelar_Click(object sender, RoutedEventArgs e)
        {

            this.Close();
        }
    }
}