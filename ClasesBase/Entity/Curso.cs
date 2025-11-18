using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ClasesBase
{
    public class Curso
    {
        private int cur_ID;
        private string cur_Nombre;
        private int est_ID;
        private int doc_ID;
        private string cur_Descripcion;
        private int cur_Cupo;
        private DateTime cur_FechaInicio;
        private DateTime cur_FechaFin;

        public DateTime Cur_FechaFin
        {
            get { return cur_FechaFin; }
            set { cur_FechaFin = value; }
        }

        public DateTime Cur_FechaInicio
        {
            get { return cur_FechaInicio; }
            set { cur_FechaInicio = value; }
        }

        public int Cur_Cupo
        {
            get { return cur_Cupo; }
            set { cur_Cupo = value; }
        }

        public string Cur_Descripcion
        {
            get { return cur_Descripcion; }
            set { cur_Descripcion = value; }
        }

        public int Doc_ID
        {
            get { return doc_ID; }
            set { doc_ID = value; }
        }

        public int Est_ID
        {
            get { return est_ID; }
            set { est_ID = value; }
        }

        public string Cur_Nombre
        {
            get { return cur_Nombre; }
            set { cur_Nombre = value; }
        }

        public int Cur_ID
        {
            get { return cur_ID; }
            set { cur_ID = value; }
        }
    }
}
