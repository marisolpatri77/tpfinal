using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using ClasesBase.Entity;

namespace ClasesBase.DataAccess
{
    public class TrabajarRoles
    {
        public List<Rol> TraerRoles()
        {
            List<Rol> roles = new List<Rol>();
            using (SqlConnection cnn = new SqlConnection(Properties.Settings.Default.institutoConnectionString))
            {
                SqlCommand cmd = new SqlCommand("SELECT * FROM Rol", cnn);
                cnn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    roles.Add(new Rol
                    {
                        Rol_ID = (int)reader["rol_id"],
                        Rol_Descripcion = reader["rol_descripcion"].ToString()
                    });
                }
            }
            return roles;
        }
    }
}