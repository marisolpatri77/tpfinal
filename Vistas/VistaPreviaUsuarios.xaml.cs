using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents; //para IDocumentPaginatorSource
using ClasesBase.Entity;

namespace Vistas
{
    public partial class VistaPreviaUsuarios : Window
    {
        /// <summary>
        /// Constructor que recibe la lista de usuarios a mostrar.
        /// </summary>
        /// <param name="listaUsuarios">La lista (filtrada o completa) de usuarios.</param>
        public VistaPreviaUsuarios(List<Usuario> listaUsuarios)
        {
            InitializeComponent();
            CargarDatosEnDocumento(listaUsuarios);
        }

        /// <summary>
        /// Recorre la lista de usuarios y construye las filas de la tabla en el FlowDocument.
        /// </summary>
        private void CargarDatosEnDocumento(List<Usuario> listaUsuarios)
        {
            foreach (Usuario usuario in listaUsuarios)
            {
                // 1. Crear una nueva Fila
                TableRow fila = new TableRow();

                // 2. Crear las Celdas para esta fila
                TableCell celdaNombreCompleto = new TableCell(new Paragraph(new Run(usuario.Usu_ApellidoNombre)));
                TableCell celdaUsuario = new TableCell(new Paragraph(new Run(usuario.Usu_NombreUsuario)));
                TableCell celdaRol = new TableCell(new Paragraph(new Run(usuario.Rol.Rol_Descripcion)));

                // 3. Añadir las celdas a la fila
                fila.Cells.Add(celdaNombreCompleto);
                fila.Cells.Add(celdaUsuario);
                fila.Cells.Add(celdaRol);

                // 4. Añadir la fila al grupo de filas de la tabla
                rgUsuarios.Rows.Add(fila);
            }
        }

        /// <summary>
        /// Cierra la ventana de vista previa.
        /// </summary>
        private void btnCerrar_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// Abre el diálogo de impresión y envía el documento.
        /// </summary>
        private void btnImprimir_Click(object sender, RoutedEventArgs e)
        {
            PrintDialog printDialog = new PrintDialog();
            if (printDialog.ShowDialog() == true)
            {
                // Obtiene el paginador del documento que creamos en el XAML
                IDocumentPaginatorSource paginador = docImprimir;

                // Envía el documento a la impresora seleccionada
                printDialog.PrintDocument(paginador.DocumentPaginator, "Impresión de Listado de Usuarios");
            }
        }
    }
}