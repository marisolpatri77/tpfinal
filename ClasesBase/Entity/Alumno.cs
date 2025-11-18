using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using ClasesBase.Utilities.Validators;
using System.Text.RegularExpressions;

namespace ClasesBase
{
    public class Alumno : IDataErrorInfo
    {
        private int alu_ID;
        private string alu_DNI;
        private string alu_Apellido;
        private string alu_Nombre;
        private string alu_Email;

        public string Alu_Email
        {
            get { return alu_Email; }
            set { alu_Email = value; }
        }

        public string Alu_Nombre
        {
            get { return alu_Nombre; }
            set { alu_Nombre = value; }
        }

        public string Alu_Apellido
        {
            get { return alu_Apellido; }
            set { alu_Apellido = value; }
        }

        public string Alu_DNI
        {
            get { return alu_DNI; }
            set { alu_DNI = value; }
        }

        public int Alu_ID
        {
            get { return alu_ID; }
            set { alu_ID = value; }
        }

        public string Error
        {
            get { return null; }
            //get { throw new NotImplementedException(); }
        }

        public string this[string columnName]
        {
            get
            {
                string msg_error = null;

                switch (columnName)
                {
                    case "Alu_DNI":
                        msg_error = validar_DNI(); break;
                    case "Alu_Apellido":
                        msg_error = validar_Apellido(); break;
                    case "Alu_Nombre":
                        msg_error = validar_Nombre(); break;
                    case "Alu_Email":
                        msg_error = validar_Email(); break;

                }
                return msg_error;
            }
        }

        private string validar_DNI()
        {
            var dni_valido = DniValidator.ValidarDni(Alu_DNI);
            if (!dni_valido.IsValid)
            {
                return dni_valido.ErrorMessage;
            }
            return null;
        }
        
        private string validar_Apellido()
        {
            if (String.IsNullOrEmpty(Alu_Apellido))
            {
                return "El Apellido es obligatorio";
            }
             if (Alu_Apellido.Length > 50)
            {
                return "El Apellido debe tener menos de 50 caracteres";
            }

             if (!Regex.IsMatch(Alu_Apellido, @"^[a-zA-ZáéíóúñÑ\s]+$"))
                 return  "El Apellido solo puede contener letras y espacios";

            return null;
        }

        private string validar_Nombre()
        {
            if (String.IsNullOrEmpty(Alu_Nombre))
            {
                return "El Nombre es obligatorio";
            }
             if (Alu_Nombre.Length > 50)
            {
                return "El Nombre debe tener menos de 50 caracteres";
            }
             if (!Regex.IsMatch(Alu_Nombre, @"^[a-zA-ZáéíóúñÑ\s]+$"))
                 return "El Nombre solo puede contener letras y espacios";
            return null;
        }

        private string validar_Email()
        {
            if (String.IsNullOrEmpty(Alu_Email))
            {
                return "El Email es obligatorio";
            }
            else
            {
                var email_Valido = EmailValidator.ValidarEmail(Alu_Email);
                if (!email_Valido.IsValid)
                {
                    return email_Valido.ErrorMessage;
                }
            }
            return null;
        }

    }
}
