using System;
using System.Windows;
using System.Windows.Input;
using System.Data;
using ClasesBase.DataAccess;
using ClasesBase;

namespace Vistas
{
    public partial class ListarCursos : Window
    {
        public ListarCursos()
        {
            InitializeComponent();
            
        }

        
        private void btnActualizar_Click(object sender, RoutedEventArgs e)
        {
            // Actualizar el ObjectDataProvider como en la consigna
            var dataProvider = (System.Windows.Data.ObjectDataProvider)this.Resources["list_cursos"];
            dataProvider.Refresh();
        }


        
        private void btnNuevoCurso_Click(object sender, RoutedEventArgs e)
        {
            FrmCurso nuevoCurso = new FrmCurso();
            nuevoCurso.Owner = this;
            bool? resultado = nuevoCurso.ShowDialog();

            if (resultado == true)
            {
                // Actualizar después de cerrar si se guardo un nuevo curso
                btnActualizar_Click(sender, e);
            }
        } 

        private void btnCerrar_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }


        private void dgCursos_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            // Al hacer doble clic en un curso
            if (dgCursos.SelectedItem != null)
            {
                DataRowView filaSeleccionada = (DataRowView)dgCursos.SelectedItem;
                string nombreCurso = filaSeleccionada["Nombre"].ToString();
                MessageBox.Show("Curso seleccionado: " + nombreCurso,
                            "Información del Curso", 
                              MessageBoxButton.OK, 
                              MessageBoxImage.Information);
            }
        }


        private void btnEditar_Click(object sender, RoutedEventArgs e)
        {
            if (dgCursos.SelectedItem == null)
            {
                MessageBoxCustom.ShowError("Seleccione un curso para editar.");
                return;
            }

            DataRowView fila = (DataRowView)dgCursos.SelectedItem;

            Curso curso = new Curso()
            {
                Cur_ID = Convert.ToInt32(fila["IDCurso"]),
                Cur_Nombre = fila["Nombre"].ToString(),
                Cur_Descripcion = fila["Descripcion"].ToString(),
                Cur_Cupo = Convert.ToInt32(fila["Cupo"]),
                Cur_FechaInicio = Convert.ToDateTime(fila["FechaInicio"]),
                Cur_FechaFin = Convert.ToDateTime(fila["FechaFin"]),
                Est_ID = Convert.ToInt32(fila["IDEstado"]),
                Doc_ID = Convert.ToInt32(fila["IDDocente"])
            };

            FrmCurso frm = new FrmCurso(curso);
            frm.Owner = this;
            bool? resultado = frm.ShowDialog();

            //actualizar la grilla
            if (resultado == true)
            {
                btnActualizar_Click(sender, e); 
            }
        }

        private void btnEliminar_Click(object sender, RoutedEventArgs e)
        {
            if (dgCursos.SelectedItem == null)
            {
                MessageBox.Show("Seleccione un curso para eliminar.", "Atención", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            DataRowView fila = (DataRowView)dgCursos.SelectedItem;
            int idCurso = Convert.ToInt32(fila["IDCurso"]);

            var confirmacion = MessageBox.Show(
                "¿Desea eliminar el curso " + fila["Nombre"] + "?",
                "Confirmación",
                MessageBoxButton.YesNo,
                MessageBoxImage.Question
            );
            if (confirmacion == MessageBoxResult.Yes)
            {
                TrabajarCursos.delete_curso(idCurso);
                MessageBox.Show("Curso eliminado correctamente.", "Éxito", MessageBoxButton.OK, MessageBoxImage.Information);
                btnActualizar_Click(sender, e);
            }
        }
    }
}