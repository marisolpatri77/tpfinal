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
    /// Lógica de interacción para MessageBoxConfirm.xaml
    /// </summary>
    public partial class MessageBoxConfirm : Window
    {
        public bool Resultado { get; private set; }
        public MessageBoxConfirm(string mensaje, string titulo = "Confirmación")
        {
            InitializeComponent();
            txtMessage.Text = mensaje;
            txtTitle.Text = titulo;

        }
        private void btnSi_Click(object sender, RoutedEventArgs e)
        {
            Resultado = true;
            this.DialogResult = true; // para que funcione ShowDialog()
            this.Close();
        }

        private void btnNo_Click(object sender, RoutedEventArgs e)
        {
            Resultado = false;
            this.DialogResult = false;
            this.Close();
        }

        public static bool Show(string mensaje, string titulo = "Confirmación")
        {
            var msg = new MessageBoxConfirm(mensaje, titulo);
            msg.ShowDialog();
            return msg.Resultado;
        }
    }
}
