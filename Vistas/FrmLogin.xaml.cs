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
using ClasesBase.Entity;
using ClasesBase.DataAccess;

namespace Vistas
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private List<Usuario> usuarios = new List<Usuario>();

        public MainWindow()
        {
            InitializeComponent();

        }

        //    Se ejecutará cuando el UserControl dispare el evento "EnterPressed"
        private void login_EnterPressed(object sender, RoutedEventArgs e)
        {
            // Simplemente llamamos a la lógica del botón que ya existe
            btnIngresar_Click(sender, e);
        }

        private void btnIngresar_Click(object sender, RoutedEventArgs e)
        {
            string usuario = login.txtUsuario.Text;
            string clave = login.txtPassword.Password;

            // 1. Creamos un objeto de la clase TrabajarUsuario
            TrabajarUsuario trabajarUsuario = new TrabajarUsuario();

            // Usar la base de datos en lugar de la lista en memoria
            Usuario encontrado = trabajarUsuario.ValidarLogin(usuario, clave);

            if (encontrado != null)
            {
                MessageBoxCustom.ShowSuccess("Bienvenido " + encontrado.Usu_ApellidoNombre);
                Principal win = new Principal();
                win.Show();
                this.Close();
            }
            else
            {
                MessageBoxCustom.ShowError("Usuario o contraseña incorrectos");
            }
        }

        private void btnSalir_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
   
    }
}
