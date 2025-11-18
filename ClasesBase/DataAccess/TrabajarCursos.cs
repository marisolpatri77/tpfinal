using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace ClasesBase.DataAccess
{
    public class TrabajarCursos
    {
        public static DataTable TraerCursos()
        {
            DataTable dt = new DataTable();

            using (SqlConnection cnn = new SqlConnection(ClasesBase.Properties.Settings.Default.institutoConnectionString))
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = @"
            SELECT 
                c.cur_id AS IDCurso,
                c.cur_nombre AS Nombre,
                c.cur_descripcion AS Descripcion,
                c.cur_cupo AS Cupo,
                c.cur_fecha_inicio AS FechaInicio,
                c.cur_fecha_fin AS FechaFin,
                c.est_id AS IDEstado,
                e.est_nombre AS Estado,
                e.esty_id AS IDTipoEstado,
                d.doc_id AS IDDocente,
                d.doc_apellido + ', ' + d.doc_nombre AS Docente
            FROM curso c
            INNER JOIN estado e ON c.est_id = e.est_id
            INNER JOIN docente d ON c.doc_id = d.doc_id
            ORDER BY c.cur_id";
                cmd.CommandType = CommandType.Text;
                cmd.Connection = cnn;

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
            }

            return dt;
        }


        public static void insert_curso(Curso curso)
        {
            SqlConnection cnn = new SqlConnection(ClasesBase.Properties.Settings.Default.institutoConnectionString);

            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "insertar_curso_sp";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = cnn;

            cmd.Parameters.AddWithValue("@cur_nombre", curso.Cur_Nombre);
            cmd.Parameters.AddWithValue("@cur_descripcion", curso.Cur_Descripcion);
            cmd.Parameters.AddWithValue("@cur_cupo", curso.Cur_Cupo);
            cmd.Parameters.AddWithValue("@cur_fecha_inicio", curso.Cur_FechaInicio);
            cmd.Parameters.AddWithValue("@cur_fecha_fin", curso.Cur_FechaFin);
            cmd.Parameters.AddWithValue("@est_id", curso.Est_ID);
            cmd.Parameters.AddWithValue("@doc_id", curso.Doc_ID);

            cnn.Open();
            cmd.ExecuteNonQuery();
            cnn.Close();
        }


        public static void update_curso(Curso curso)
        {
            SqlConnection cnn = new SqlConnection(ClasesBase.Properties.Settings.Default.institutoConnectionString);

            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "modificar_curso_sp";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = cnn;

            cmd.Parameters.AddWithValue("@cur_id", curso.Cur_ID);
            cmd.Parameters.AddWithValue("@cur_nombre", curso.Cur_Nombre);
            cmd.Parameters.AddWithValue("@cur_descripcion", curso.Cur_Descripcion);
            cmd.Parameters.AddWithValue("@cur_cupo", curso.Cur_Cupo);
            cmd.Parameters.AddWithValue("@cur_fecha_inicio", curso.Cur_FechaInicio);
            cmd.Parameters.AddWithValue("@cur_fecha_fin", curso.Cur_FechaFin);
            cmd.Parameters.AddWithValue("@est_id", curso.Est_ID);
            cmd.Parameters.AddWithValue("@doc_id", curso.Doc_ID);

            try
            {
                cnn.Open();
                int filasAfectadas = cmd.ExecuteNonQuery();

                //verificar que se actualizó al menos una fila
                if (filasAfectadas == 0)
                {
                    throw new Exception("No se actualizó ningún registro. Verifique que el curso existe.");
                }

                cnn.Close();
            }
            catch (Exception ex)
            {
                if (cnn.State == ConnectionState.Open)
                    cnn.Close();
                throw ex;
            }
        }

        public static void delete_curso(int cur_id)
        {
            SqlConnection cnn = new SqlConnection(ClasesBase.Properties.Settings.Default.institutoConnectionString);

            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "eliminar_curso_sp";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = cnn;

            cmd.Parameters.AddWithValue("@cur_id", cur_id);

            cnn.Open();
            cmd.ExecuteNonQuery();
            cnn.Close();
        }

        public static DataTable obtener_curso_programado()
        {
            try
            {
                SqlConnection cnn = new SqlConnection(ClasesBase.Properties.Settings.Default.institutoConnectionString);
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "traer_cursos_programados_sp";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = cnn;

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);

                return dt;
            }
            catch (Exception)
            {
                throw; // deja que la UI maneje el error
            }
        }

    }
}