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
using System.Windows.Navigation;
using System.Windows.Shapes;
using ClasesBase;
namespace Vistas.ControlUsuario
{
    /// <summary>
    /// Lógica de interacción para AlumnoControl.xaml
    /// </summary>
    public partial class AlumnoControl : UserControl
    {
        private Alumno _alumno;

        public AlumnoControl()
        {
            InitializeComponent();
            _alumno = new Alumno();
            this.DataContext = _alumno;
        }

        // Propiedad para acceder al alumno desde fuera
        public Alumno Alumno
        {
            get { return _alumno; }
            set
            {
                _alumno = value;
                this.DataContext = _alumno;
            }
        }

        // Método para verificar si hay errores de validación
        public bool IsValid()
        {
            // Retorna TRUE si no hay errores en ningún campo
            // Retorna FALSE si hay al menos un error en algún campo
            return string.IsNullOrEmpty(_alumno["Alu_DNI"]) &&
                   string.IsNullOrEmpty(_alumno["Alu_Nombre"]) &&
                   string.IsNullOrEmpty(_alumno["Alu_Apellido"]) &&
                   string.IsNullOrEmpty(_alumno["Alu_Email"]);
        }

        // Método para limpiar los campos
        public void LimpiarCampos()
        {
            _alumno = new Alumno();
            this.DataContext = _alumno;
        }

        // Propiedades para acceder a los TextBox sin recursión
        public string TxtDNIText
        {
            get { return txtDNI.Text; }
            set { txtDNI.Text = value; }
        }

        public string TxtNombreText
        {
            get { return txtNombre.Text; }
            set { txtNombre.Text = value; }
        }

        public string TxtApellidoText
        {
            get { return txtApellido.Text; }
            set { txtApellido.Text = value; }
        }

        public string TxtEmailText
        {
            get { return txtEmail.Text; }
            set { txtEmail.Text = value; }
        }
    }
}
