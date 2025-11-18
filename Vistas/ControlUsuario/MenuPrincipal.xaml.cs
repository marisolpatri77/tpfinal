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

namespace Vistas.ControlUsuario
{
    /// <summary>
    /// Lógica de interacción para Menu.xaml
    /// </summary>
    public partial class MenuPrincipal : UserControl
    {
        public MenuPrincipal()
        {
            InitializeComponent();
        }

        private void MenuSalir_Click(object sender, RoutedEventArgs e)
        {
            Window parentWindow = Window.GetWindow(this);
            if (parentWindow != null)
            {
                parentWindow.Close();
            }
        }

        private void AltaCurso_Click(object sender, RoutedEventArgs e)
        {
            FrmCurso oFrmCurso = new FrmCurso();
            oFrmCurso.Show();
        }

        private void AltaDocente_Click(object sender, RoutedEventArgs e)
        {
            FrmDocente oFrmDocente = new FrmDocente();
            oFrmDocente.Show();
        }

        private void AltaAlumno_Click(object sender, RoutedEventArgs e)
        {
            FrmAlumno oFrmAlumno = new FrmAlumno();
            oFrmAlumno.Show();
        }

        private void AltaUsuario_Click(object sender, RoutedEventArgs e)
        {
            FrmUsuario oFrmUsuario = new FrmUsuario();
            oFrmUsuario.Show();
        }

    }
}
