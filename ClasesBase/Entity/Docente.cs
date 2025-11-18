using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using ClasesBase.Utilities.Validators;
using System.Text.RegularExpressions;
namespace ClasesBase
{
    public class Docente : IDataErrorInfo
    {
        private int doc_ID;
        private string doc_DNI;
        private string doc_Apellido;
        private string doc_Nombre;
        private string doc_Email;

        public string Doc_Email
        {
            get { return doc_Email; }
            set { doc_Email = value; }
        }

        public string Doc_Nombre
        {
            get { return doc_Nombre; }
            set { doc_Nombre = value; }
        }

        public string Doc_Apellido
        {
            get { return doc_Apellido; }
            set { doc_Apellido = value; }
        }

        public string Doc_DNI
        {
            get { return doc_DNI; }
            set { doc_DNI = value; }
        }

        public int Doc_ID
        {
            get { return doc_ID; }
            set { doc_ID = value; }
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
                    case "Doc_DNI":
                        msg_error = validar_DNI(); break;
                    case "Doc_Apellido":
                        msg_error = validar_Apellido(); break;
                    case "Doc_Nombre":
                        msg_error = validar_Nombre(); break;
                    case "Doc_Email":
                        msg_error = validar_Email(); break;

                }
                return msg_error;
            }
        }

        private string validar_DNI()
        {
            var dni_valido = DniValidator.ValidarDni(Doc_DNI);
            if (!dni_valido.IsValid)
            {
                return dni_valido.ErrorMessage;
            }
            return null;
        }

        private string validar_Apellido()
        {
            if (String.IsNullOrEmpty(Doc_Apellido))
            {
                return "El Apellido es obligatorio";
            }
            if (Doc_Apellido.Length > 50)
            {
                return "El Apellido debe tener menos de 50 caracteres";
            }

            if (!Regex.IsMatch(Doc_Apellido, @"^[a-zA-ZáéíóúñÑ\s]+$"))
                return "El Apellido solo puede contener letras y espacios";

            return null;
        }

        private string validar_Nombre()
        {
            if (String.IsNullOrEmpty(Doc_Nombre))
            {
                return "El Nombre es obligatorio";
            }
            if (Doc_Nombre.Length > 50)
            {
                return "El Nombre debe tener menos de 50 caracteres";
            }
            if (!Regex.IsMatch(Doc_Nombre, @"^[a-zA-ZáéíóúñÑ\s]+$"))
                return "El Nombre solo puede contener letras y espacios";
            return null;
        }

        private string validar_Email()
        {
            if (String.IsNullOrEmpty(Doc_Email))
            {
                return "El Email es obligatorio";
            }
            else
            {
                var email_Valido = EmailValidator.ValidarEmail(Doc_Email);
                if (!email_Valido.IsValid)
                {
                    return email_Valido.ErrorMessage;
                }
            }
            return null;
        }
    }
}
