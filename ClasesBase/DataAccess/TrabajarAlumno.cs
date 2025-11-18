using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;


namespace ClasesBase
{
    public class TrabajarAlumno
    {
        public static Alumno TraerAlumno(string parametroBusqueda)
        {
            Alumno alu = null;

            SqlConnection cnn = new SqlConnection(ClasesBase.Properties.Settings.Default.institutoConnectionString);

            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "traer_alumno_sp";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = cnn;

            int id;
            if (int.TryParse(parametroBusqueda, out id))
            {
                cmd.Parameters.AddWithValue("@alu_id", id);
                cmd.Parameters.AddWithValue("@alu_dni", parametroBusqueda);
            }
            
            cnn.Open();
            SqlDataReader dr = cmd.ExecuteReader();

            if (dr.Read())
            {
                alu = new Alumno()
                {
                    Alu_ID = (int)dr["alu_id"],
                    Alu_DNI = dr["alu_dni"].ToString(),
                    Alu_Nombre = dr["alu_nombre"].ToString(),
                    Alu_Apellido = dr["alu_apellido"].ToString(),
                    Alu_Email = dr["alu_email"].ToString()
                };
            }
            dr.Close();
            cnn.Close();
            return alu;
        }

        // Metodo para actualizar los datos del alumno
        public static void update_alumno(Alumno alumno)
        {
            SqlConnection cnn = new SqlConnection(ClasesBase.Properties.Settings.Default.institutoConnectionString);

            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "modificar_alumno_sp";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = cnn;

            cmd.Parameters.AddWithValue("@alu_id", alumno.Alu_ID);
            cmd.Parameters.AddWithValue("@alu_dni", alumno.Alu_DNI);
            cmd.Parameters.AddWithValue("@alu_nombre", alumno.Alu_Nombre);
            cmd.Parameters.AddWithValue("@alu_apellido", alumno.Alu_Apellido);
            cmd.Parameters.AddWithValue("@alu_email", alumno.Alu_Email);

            cnn.Open();
            cmd.ExecuteNonQuery();
            cnn.Close();
        }

        public static void insert_alumno(Alumno alumno)
        {
            SqlConnection cnn = new SqlConnection(ClasesBase.Properties.Settings.Default.institutoConnectionString);

            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "insertar_alumno_sp";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = cnn;

            cmd.Parameters.AddWithValue("@alu_dni", alumno.Alu_DNI);
            cmd.Parameters.AddWithValue("@alu_nombre", alumno.Alu_Nombre);
            cmd.Parameters.AddWithValue("@alu_apellido", alumno.Alu_Apellido);
            cmd.Parameters.AddWithValue("@alu_email", alumno.Alu_Email);

            cnn.Open();
            cmd.ExecuteNonQuery();
            cnn.Close();
        }

        public static void delete_alumno(int alu_id)
        {
            SqlConnection cnn = new SqlConnection(ClasesBase.Properties.Settings.Default.institutoConnectionString);

            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "eliminar_alumno_sp";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = cnn;

            cmd.Parameters.AddWithValue("@alu_id", alu_id);

            cnn.Open();
            cmd.ExecuteNonQuery();
            cnn.Close();
        }


        public static DataTable listar_alumnos()
        {
                SqlConnection cnn = new SqlConnection(ClasesBase.Properties.Settings.Default.institutoConnectionString);
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "listar_alumnos_sp";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = cnn;

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);

                return dt;
        }
    }
}