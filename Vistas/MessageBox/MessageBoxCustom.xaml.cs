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
    /// Lógica de interacción para MessageBoxCustom.xaml
    /// </summary>
    public partial class MessageBoxCustom : Window
    {
        public MessageBoxCustom(string message, string title, MessageType type)
    {
        InitializeComponent();
        txtTitle.Text = title;
        txtMessage.Text = message;
        
        
    }

    private void btnAceptar_Click(object sender, RoutedEventArgs e)
    {
        this.DialogResult = true;
        this.Close();
    }

    // Métodos estáticos para fácil uso
    public static void Show(string message, string title, MessageType type)
    {
        MessageBoxCustom box = new MessageBoxCustom(message, title, type);
        box.Owner = Application.Current.MainWindow;
        box.ShowDialog();
    }

    public static void ShowSuccess(string message)
    {
        Show(message, "Éxito", MessageType.Success);
    }

    public static void ShowError(string message)
    {
        Show(message, "Error", MessageType.Error);
    }

    public static void ShowWarning(string message)
    {
        Show(message, "Advertencia", MessageType.Warning);
    }

    public static void ShowInfo(string message)
    {
        Show(message, "Información", MessageType.Info);
    }
}

public enum MessageType
{
    Success,
    Error,
    Warning,
    Info
}
    }

