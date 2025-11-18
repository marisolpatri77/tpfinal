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
    /// Lógica de interacción para AltaDocente.xaml
    /// </summary>
    public partial class AltaDocente : UserControl
    {
        private Docente _docente;

        public AltaDocente()
        {
            InitializeComponent();
            _docente = new Docente();
            this.DataContext = _docente;
        }

        // Propiedad para acceder al docente desde fuera
        public Docente Docente
        {
            get { return _docente; }
            set
            {
                _docente = value;
                this.DataContext = _docente;
            }
        }

        // Método para verificar si hay errores de validación
        public bool IsValid()
        {
            return string.IsNullOrEmpty(_docente["Doc_DNI"]) &&
                   string.IsNullOrEmpty(_docente["Doc_Nombre"]) &&
                   string.IsNullOrEmpty(_docente["Doc_Apellido"]) &&
                   string.IsNullOrEmpty(_docente["Doc_Email"]);
        }

        // Método para limpiar los campos
        public void LimpiarCampos()
        {
            _docente = new Docente();
            this.DataContext = _docente;
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
