using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;


namespace ClasesBase
{
    public class TrabajarDocente
    {
        public static Docente TraerDocente(string parametroBusqueda)
        {
            Docente doc = null;

            SqlConnection cnn = new SqlConnection(ClasesBase.Properties.Settings.Default.institutoConnectionString);

            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "traer_docente_sp"; 
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = cnn;

            int id;
            if (int.TryParse(parametroBusqueda, out id))
            {
                cmd.Parameters.AddWithValue("@doc_id", id);
                cmd.Parameters.AddWithValue("@doc_dni", parametroBusqueda);
            }

            cnn.Open();
            SqlDataReader dr = cmd.ExecuteReader();

            if (dr.Read())
            {
                doc = new Docente()
                {
                    Doc_ID = (int)dr["doc_id"],
                    Doc_DNI = dr["doc_dni"].ToString(),
                    Doc_Nombre = dr["doc_nombre"].ToString(),
                    Doc_Apellido = dr["doc_apellido"].ToString(),
                    Doc_Email = dr["doc_email"].ToString()
                };
            }
            dr.Close();
            cnn.Close();
            return doc;
        }

        // Metodo para actualizar los datos del docente
        public static void update_docente(Docente docente)
        {
            SqlConnection cnn = new SqlConnection(ClasesBase.Properties.Settings.Default.institutoConnectionString);

            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "modificar_docente_sp"; 
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = cnn;

            cmd.Parameters.AddWithValue("@doc_id", docente.Doc_ID);
            cmd.Parameters.AddWithValue("@doc_dni", docente.Doc_DNI);
            cmd.Parameters.AddWithValue("@doc_nombre", docente.Doc_Nombre);
            cmd.Parameters.AddWithValue("@doc_apellido", docente.Doc_Apellido);
            cmd.Parameters.AddWithValue("@doc_email", docente.Doc_Email);

            cnn.Open();
            cmd.ExecuteNonQuery();
            cnn.Close();
        }

        public static void insert_docente(Docente docente)
        {
            SqlConnection cnn = new SqlConnection(ClasesBase.Properties.Settings.Default.institutoConnectionString);

            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "insertar_docente_sp"; 
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = cnn;

            cmd.Parameters.AddWithValue("@Doc_dni", docente.Doc_DNI);
            cmd.Parameters.AddWithValue("@Doc_nombre", docente.Doc_Nombre);
            cmd.Parameters.AddWithValue("@Doc_apellido", docente.Doc_Apellido);
            cmd.Parameters.AddWithValue("@Doc_email", docente.Doc_Email);

            cnn.Open();
            cmd.ExecuteNonQuery();
            cnn.Close();
        }

        public static void delete_docente(int doc_id)
        {
            SqlConnection cnn = new SqlConnection(ClasesBase.Properties.Settings.Default.institutoConnectionString);

            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "eliminar_docente_sp"; 
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = cnn;

            cmd.Parameters.AddWithValue("@doc_id", doc_id);

            cnn.Open();
            cmd.ExecuteNonQuery();
            cnn.Close();
        }

        public static DataTable listar_docentes()
        {
            SqlConnection cnn = new SqlConnection(ClasesBase.Properties.Settings.Default.institutoConnectionString);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "listar_docentes_sp";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = cnn;

            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);

            return dt;
        }


        public static List<Docente> ListarDocentes()
        {
            List<Docente> lista = new List<Docente>();

            SqlConnection cnn = new SqlConnection(ClasesBase.Properties.Settings.Default.institutoConnectionString);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "listar_docentes_sp";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = cnn;

                cnn.Open();
                SqlDataReader dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    Docente doc = new Docente()
                    {
                        Doc_ID = (int)dr["doc_id"],
                        Doc_DNI = dr["doc_dni"].ToString(),
                        Doc_Apellido = dr["doc_apellido"].ToString(),
                        Doc_Nombre = dr["doc_nombre"].ToString(),
                        Doc_Email = dr["doc_email"].ToString()
                    };

                    lista.Add(doc);
                }

                dr.Close();
            

            return lista;
        }
    }
}