using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace ClasesBase.Entity
{
    // Paso 1: Heredar de INotifyPropertyChanged
    public class Usuario : INotifyPropertyChanged
    {
        private int usu_ID;
        private string usu_NombreUsuario;
        private int rol_ID;
        private string usu_Contrasenia;
        private string usu_ApellidoNombre;
        private Rol rol;

        // Paso 2: Implementar el evento que exige la interfaz
        public event PropertyChangedEventHandler PropertyChanged;

        // Paso 3: Crear un método para invocar el evento
        protected void OnPropertyChanged(string name)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(name));
            }
        }

        public string Usu_ApellidoNombre
        {
            get { return usu_ApellidoNombre; }
            // Paso 4: Llamar a OnPropertyChanged en cada 'set'
            set
            {
                usu_ApellidoNombre = value;
                OnPropertyChanged("Usu_ApellidoNombre");
            }
        }

        public string Usu_Contrasenia
        {
            get { return usu_Contrasenia; }
            set
            {
                usu_Contrasenia = value;
                OnPropertyChanged("Usu_Contrasenia");
            }
        }

        public int Rol_ID
        {
            get { return rol_ID; }
            set
            {
                rol_ID = value;
                OnPropertyChanged("Rol_ID");
            }
        }

        public string Usu_NombreUsuario
        {
            get { return usu_NombreUsuario; }
            set
            {
                usu_NombreUsuario = value;
                OnPropertyChanged("Usu_NombreUsuario");
            }
        }

        public int Usu_ID
        {
            get { return usu_ID; }
            set
            {
                usu_ID = value;
                OnPropertyChanged("Usu_ID");
            }
        }

        public Rol Rol
        {
            get { return rol; }
            set
            {
                rol = value;
                OnPropertyChanged("Rol");
            }
        }

        
        // Constructor vacío (necesario para binding y deserialización)
        public Usuario() { }

        // Constructor alternativo (por ID de rol)
        public Usuario(int id, string nombreUsuario, string contrasenia, string apellidoNombre, int rolId)
        {
            Usu_ID = id;
            Usu_NombreUsuario = nombreUsuario;
            Usu_Contrasenia = contrasenia;
            Usu_ApellidoNombre = apellidoNombre;
            Rol_ID = rolId;
        }
       
    }
}