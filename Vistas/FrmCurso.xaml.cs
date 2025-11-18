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
using ClasesBase.DataAccess;
using System.Data.SqlClient;
using System.Data;

namespace Vistas
{
    /// <summary>
    /// Interaction logic for FrmCurso.xaml
    /// </summary>
    public partial class FrmCurso : Window
    {
        //private List<Estado> estados = new List<Estado>();
        //private List<EstadoType> estadoTypes = new List<EstadoType>();
        //private List<Docente> docentes = new List<Docente>();
        string errores = "";
        private Curso cursoEditando = null;

        public FrmCurso()
        {
            InitializeComponent();
           
        }


        public FrmCurso(Curso curso) : this()
        {
            cursoEditando = curso;
            btnGuardar.Content = "Actualizar";

            altaCurso.txtNombre.Text = curso.Cur_Nombre;
            altaCurso.txtDescripcion.Text = curso.Cur_Descripcion;
            altaCurso.txtCupo.Text = curso.Cur_Cupo.ToString();
            altaCurso.dpFechaInicio.SelectedDate = curso.Cur_FechaInicio;
            altaCurso.dpFechaFin.SelectedDate = curso.Cur_FechaFin;

            //obtener el tipo de estado del curso seleccionado
            DataTable dt = TrabajarEstado.listar_estados();
            var tipoEstado = dt.AsEnumerable()
                               .Where(r => r.Field<int>("est_id") == curso.Est_ID)
                               .Select(r => new
                               {
                                   esty_id = r.Field<int>("esty_id"),
                                   esty_nombre = r.Field<string>("esty_nombre")
                               })
                               .FirstOrDefault();

            //seleccionar el tipo de estado
            if (tipoEstado != null)
            {
                altaCurso.cmbTipoEstado.SelectedValue = tipoEstado.esty_id;
                CargarEstadosPorTipo(tipoEstado.esty_id);
                altaCurso.cmbEstado.SelectedValue = curso.Est_ID;
            }

            altaCurso.cmbDocente.SelectedValue = curso.Doc_ID;
        }



        private void CargarEstadosPorTipo(int tipoEstadoId)
        {
            DataTable dt = TrabajarEstado.listar_estados();

            var estadosFiltrados = dt.AsEnumerable()
                                     .Where(r => r.Field<int>("esty_id") == tipoEstadoId);

            if (estadosFiltrados.Any())
            {
                DataTable estadosDT = estadosFiltrados.CopyToDataTable();
                altaCurso.cmbEstado.ItemsSource = estadosDT.DefaultView;
                altaCurso.cmbEstado.SelectedValuePath = "est_id";
            }
            else
            {
                altaCurso.cmbEstado.ItemsSource = null;
            }
        }


        

        private void Limpiar_Campos()
        {
            altaCurso.txtNombre.Clear();
            altaCurso.txtDescripcion.Clear();
            altaCurso.txtCupo.Clear();
            altaCurso.dpFechaInicio.SelectedDate = null;
            altaCurso.dpFechaFin.SelectedDate = null;
            altaCurso.cmbTipoEstado.SelectedIndex = -1;
            altaCurso.cmbDocente.SelectedIndex = -1;
        }

   

        private void btnCancelar_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

       /** private void btnGuardar_Click(object sender, RoutedEventArgs e)
        {
            var confirmacion = new MessageBoxConfirm("¿Desea guardar este Curso?", "Atención");
            bool? resultado = confirmacion.ShowDialog();

            if (resultado == true)
            {
                if (verificarCampos())
                {
                    Curso oCurso = new Curso();
                    oCurso.Cur_Nombre = altaCurso.txtNombre.Text;
                    oCurso.Cur_Descripcion = altaCurso.txtDescripcion.Text;
                    oCurso.Cur_Cupo = int.Parse(altaCurso.txtCupo.Text);
                    oCurso.Cur_FechaInicio = altaCurso.dpFechaInicio.SelectedDate ?? DateTime.MinValue;
                    oCurso.Cur_FechaFin = altaCurso.dpFechaFin.SelectedDate ?? DateTime.MinValue;
                    int tipoSeleccionado = altaCurso.cmbTipoEstado.SelectedValue != null ? (int)altaCurso.cmbTipoEstado.SelectedValue : 0;
                    //oCurso.Est_ID = cmbTipoEstado.SelectedValue != null ? (int)cmbTipoEstado.SelectedValue : 0;

                    oCurso.Doc_ID = altaCurso.cmbDocente.SelectedValue != null ? (int)altaCurso.cmbDocente.SelectedValue : 0;


                    MessageBoxCustom.ShowSuccess(

                      "Datos del curso guardado"
                  );

                    Limpiar_Campos();
                }
                else
                {
                    MessageBoxCustom.ShowError(errores);
                    errores = "";
                }
            }
        } **/


        private void btnGuardar_Click(object sender, RoutedEventArgs e)
        {
            var confirmacion = new MessageBoxConfirm("¿Desea guardar este curso?", "Atención");
            bool? resultado = confirmacion.ShowDialog();

            if (resultado != true)
                return;

            if (!verificarCampos())
            {
                MessageBoxCustom.ShowError(errores);
                errores = "";
                return;
            }

            try
            {
                Curso oCurso = new Curso
                {
                    Cur_Nombre = altaCurso.txtNombre.Text.Trim(),
                    Cur_Descripcion = altaCurso.txtDescripcion.Text.Trim(),
                    Cur_Cupo = int.Parse(altaCurso.txtCupo.Text),
                    Cur_FechaInicio = altaCurso.dpFechaInicio.SelectedDate ?? DateTime.MinValue,
                    Cur_FechaFin = altaCurso.dpFechaFin.SelectedDate ?? DateTime.MinValue,
                    Est_ID = altaCurso.cmbEstado.SelectedValue != null
                                ? Convert.ToInt32(altaCurso.cmbEstado.SelectedValue)
                                : 0,
                    Doc_ID = altaCurso.cmbDocente.SelectedValue != null
                                ? Convert.ToInt32(altaCurso.cmbDocente.SelectedValue)
                                : 0
                };

                //validar que se hayan seleccionado IDs validos
                if (oCurso.Est_ID == 0)
                {
                    MessageBoxCustom.ShowError("Debe seleccionar un estado válido antes de guardar el curso.");
                    return;
                }

                if (oCurso.Doc_ID == 0)
                {
                    MessageBoxCustom.ShowError("Debe seleccionar un docente válido antes de guardar el curso.");
                    return;
                }

                //alta o modificacion segun corresponda
                if (cursoEditando == null)
                {
                    //insertar
                    TrabajarCursos.insert_curso(oCurso);
                    MessageBoxCustom.ShowSuccess("El curso se guardó correctamente.");
                }
                else
                {
                    //actualizar
                    oCurso.Cur_ID = cursoEditando.Cur_ID;
                    TrabajarCursos.update_curso(oCurso);
                    MessageBoxCustom.ShowSuccess("El curso se actualizó correctamente.");
                }

                //limpiar campos solo si es un nuevo curso
                if (cursoEditando == null)
                {
                    Limpiar_Campos();
                }

                //cerrar el formulario
                this.DialogResult = true;
                this.Close();
            }
            catch (SqlException sqlEx)
            {
                if (sqlEx.Message.Contains("FK_curso_estado"))
                    MessageBoxCustom.ShowError("El estado seleccionado no existe en la base de datos.");
                else if (sqlEx.Message.Contains("FK_curso_docente"))
                    MessageBoxCustom.ShowError("El docente seleccionado no existe en la base de datos.");
                else
                    MessageBoxCustom.ShowError("Error en la base de datos: " + sqlEx.Message);
            }
            catch (Exception ex)
            {
                MessageBoxCustom.ShowError("Ocurrió un error al guardar el curso: " + ex.Message);
            }
        }




        private Boolean verificarCampos()
        {
            Boolean verificado = false;
            int cupo;
            var resultadoNombre = StringValidatorNombreApellido.ValidarNombreApellido("Nombre", altaCurso.txtNombre.Text);
            var resultadoDescripcion = DescripcionValidator.ValidarDescripcion(altaCurso.txtDescripcion.Text);
            var resultadoCmbEstado = ComboBoxValidator.ValidarSeleccion(altaCurso.cmbEstado, "Estado");
            var resultadoCmbTipoEstado = ComboBoxValidator.ValidarSeleccion(altaCurso.cmbTipoEstado, "Tipo Estado");
            var resultadoCmbDocente = ComboBoxValidator.ValidarSeleccion(altaCurso.cmbDocente, "Docente");
            var resultadoFechaInicio = FechaValidator.ValidarFechaFutura(altaCurso.dpFechaInicio.SelectedDate, "Inicio");
            var resultadoFechaFin = FechaValidator.ValidarFechaFutura(altaCurso.dpFechaFin.SelectedDate , "Fin");
            var resultadoRangoFecha = FechaValidator.ValidarRangoFechas(altaCurso.dpFechaInicio.SelectedDate, altaCurso.dpFechaFin.SelectedDate);

            if (
               resultadoNombre.IsValid &&
               resultadoCmbEstado.IsValid &&
               resultadoCmbTipoEstado.IsValid &&
               resultadoCmbDocente.IsValid &&
               resultadoDescripcion.IsValid &&
               int.TryParse(altaCurso.txtCupo.Text, out cupo) &&
               resultadoFechaInicio.IsValid &&
               resultadoFechaFin.IsValid && resultadoRangoFecha.IsValid
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

                if (!resultadoDescripcion.IsValid)
                {
                    errores = errores  + resultadoDescripcion.ErrorMessage + "\n";
                }

                if (!resultadoCmbEstado.IsValid) {
                    errores = errores + resultadoCmbEstado.ErrorMessage + "\n";
                }

                if (!resultadoCmbTipoEstado.IsValid)
                {
                    errores = errores + resultadoCmbEstado.ErrorMessage + "\n";
                }

                if (!resultadoCmbDocente.IsValid)
                {
                    errores = errores + resultadoCmbDocente.ErrorMessage + "\n";
                }

                if (!int.TryParse(altaCurso.txtCupo.Text, out cupo)) {
                    errores = errores + "El curso debe ser numero"+ "\n";
                }

                if (!resultadoFechaInicio.IsValid)
                {
                    errores = errores + resultadoFechaInicio.ErrorMessage + "\n";
                }

                if (!resultadoFechaFin.IsValid)
                {
                    errores = errores + resultadoFechaFin.ErrorMessage + "\n";
                }

                if (!resultadoRangoFecha.IsValid)
                {
                    errores = errores + resultadoRangoFecha.ErrorMessage + "\n";
                }


            }

            return verificado;
        }

    }
}
