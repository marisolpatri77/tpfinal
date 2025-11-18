using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using ClasesBase.DataAccess;
using ClasesBase.Entity;
using ClasesBase.Utilities.Validators;

namespace Vistas.ControlUsuario
{
    public partial class AltaUsuario : UserControl
    {
        private string errores = "";

        public AltaUsuario()
        {
            InitializeComponent();
            InicializarRoles();
        }

        public bool Validar()
        {
            if (!verificarCampos())
            {
                MessageBoxCustom.ShowError(errores);
                errores = "";
                return false;
            }
            return true;
        }

        public bool Guardar()
        {
            if (!verificarCampos())
            {
                MessageBoxCustom.ShowError(errores);
                errores = "";
                return false;
            }
            var confirmacion = new MessageBoxConfirm("¿Desea guardar el Usuario?", "Atención");
            if (confirmacion.ShowDialog() != true)
            {
                return false;
            }

            Usuario oUsuario = new Usuario
            {
                Usu_ApellidoNombre = txtApellido.Text + " " + txtNombre.Text,
                Usu_Contrasenia = txtPassword.Password,
                Usu_NombreUsuario = txtNombreUsuario.Text,
                Rol_ID = (int)cmbRol.SelectedValue
            };

            TrabajarUsuario trabajarUsuario = new TrabajarUsuario();
            int resultadoInsercion = trabajarUsuario.InsertarUsuario(oUsuario);

            if (resultadoInsercion == 1)
            {
                MessageBoxCustom.ShowSuccess("Usuario guardado correctamente");
                LimpiarCampos();
                return true;
            }
            else if (resultadoInsercion == -1)
            {
                MessageBoxCustom.ShowError("El nombre de usuario ya existe. Por favor, elija otro.");
            }
            else
            {
                MessageBoxCustom.ShowError("Error al guardar el usuario. Por favor, intente nuevamente.");
            }
            return false;
        }

        public void CargarDatos(Usuario usuario)
        {
            if (usuario != null)
            {
                txtNombreUsuario.Text = usuario.Usu_NombreUsuario;
                txtPassword.Password = usuario.Usu_Contrasenia;
                cmbRol.SelectedValue = usuario.Rol_ID;

                string nombreCompleto = usuario.Usu_ApellidoNombre;
                int ultimoEspacio = nombreCompleto.LastIndexOf(' ');

                if (ultimoEspacio > 0)
                {
                    txtApellido.Text = nombreCompleto.Substring(0, ultimoEspacio);
                    txtNombre.Text = nombreCompleto.Substring(ultimoEspacio + 1);
                }
                else
                {
                    txtApellido.Text = nombreCompleto;
                    txtNombre.Text = "";
                }
            }
        }

        public void LimpiarCampos()
        {
            txtApellido.Clear();
            txtNombre.Clear();
            txtNombreUsuario.Clear();
            txtPassword.Clear();
            cmbRol.SelectedIndex = -1;
        }

        private void InicializarRoles()
        {
            TrabajarRoles trabajarRoles = new TrabajarRoles();
            cmbRol.ItemsSource = trabajarRoles.TraerRoles();
            cmbRol.SelectedValuePath = "Rol_ID";
        }

        private bool verificarCampos()
        {
            errores = "";
            bool esValido = true;

            var resNombre = StringValidatorNombreApellido.ValidarNombreApellido("Nombre", txtNombre.Text);
            if (!resNombre.IsValid)
            {
                errores += resNombre.ErrorMessage + "\n";
                esValido = false;
            }

            var resApellido = StringValidatorNombreApellido.ValidarNombreApellido("Apellido", txtApellido.Text);
            if (!resApellido.IsValid)
            {
                errores += resApellido.ErrorMessage + "\n";
                esValido = false;
            }

            var resultadoUsuario = UserValidator.ValidarUsuario(txtNombreUsuario.Text);
            if (!resultadoUsuario.IsValid)
            {
                errores += resultadoUsuario.ErrorMessage + "\n";
                esValido = false;
            }

            var resultadoContraseña = PasswordValidator.ValidarPassword(txtPassword.Password);
            if (!resultadoContraseña.IsValid)
            {
                errores += resultadoContraseña.ErrorMessage + "\n";
                esValido = false;
            }

            var resultadoCmbRol = ComboBoxValidator.ValidarSeleccion(cmbRol, "Rol");
            if (!resultadoCmbRol.IsValid)
            {
                errores += resultadoCmbRol.ErrorMessage + "\n";
                esValido = false;
            }

            return esValido;
        }
    }
}