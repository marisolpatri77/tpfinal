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
    /// Lógica de interacción para Login.xaml
    /// </summary>
    public partial class Login : UserControl
    {
        public event RoutedEventHandler EnterPressed;
        public Login()
        {
            InitializeComponent();
        }
        //Manejador del evento KeyDown
        private void txtPassword_KeyDown(object sender, KeyEventArgs e)
        {
            // 3. Comprueba si la tecla presionada es "Enter"
            if (e.Key == Key.Enter)
            {
                // 4. Si es "Enter", dispara nuestro evento personalizado
                //    El (EnterPressed != null) comprueba si alguien 
                //    (en este caso, la MainWindow) está "escuchando" el evento.
                if (EnterPressed != null)
                {
                    EnterPressed(this, new RoutedEventArgs());
                }
            }
        }
       
    }
}
