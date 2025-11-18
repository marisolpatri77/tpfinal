using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ClasesBase
{
    public class Estado
    {
        private int est_ID;
        private string est_Nombre;
        private int esty_ID;

        public int Esty_ID
        {
            get { return esty_ID; }
            set { esty_ID = value; }
        }

        public string Est_Nombre
        {
            get { return est_Nombre; }
            set { est_Nombre = value; }
        }

        public int Est_ID
        {
            get { return est_ID; }
            set { est_ID = value; }
        }
    }
}
