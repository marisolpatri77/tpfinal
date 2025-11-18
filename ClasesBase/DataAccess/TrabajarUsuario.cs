using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Collections.ObjectModel;
using ClasesBase.Entity;
using System.ComponentModel;


namespace ClasesBase.DataAccess
{
    public class TrabajarUsuario
    {
        public ObservableCollection<Usuario> TraerUsuarios()
        {
            ObservableCollection<Usuario> usuarios = new ObservableCollection<Usuario>();

            // Si algo falla aquí (ej: connection string incorrecta), 
            // la excepción será "lanzada" para que la ventana la capture.
            using (SqlConnection cnn = new SqlConnection(ClasesBase.Properties.Settings.Default.institutoConnectionString))
            {
                SqlCommand cmd = new SqlCommand("listar_usuarios_sp", cnn);
                cmd.CommandType = CommandType.StoredProcedure;

                cnn.Open();
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Usuario usuario = new Usuario();
                        usuario.Usu_ID = Convert.ToInt32(reader["usu_id"]);
                        usuario.Usu_NombreUsuario = reader["usu_nombre_usuario"].ToString();
                        usuario.Usu_Contrasenia = reader["usu_contraseña"].ToString();
                        usuario.Usu_ApellidoNombre = reader["usu_apellido_nombre"].ToString();
                        usuario.Rol_ID = Convert.ToInt32(reader["rol_id"]);
                        usuario.Rol = new Rol();
                        usuario.Rol.Rol_ID = Convert.ToInt32(reader["rol_id"]);
                        usuario.Rol.Rol_Descripcion = reader["rol_descripcion"].ToString();

                        usuarios.Add(usuario);
                        
                    }
                }
            }
            return usuarios;
        }

        public Usuario TraerUsuario(string nombreUsuario)
        {
            Usuario usuario = null;
            using (SqlConnection cnn = new SqlConnection(ClasesBase.Properties.Settings.Default.institutoConnectionString))
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "traer_usuario_sp"; 
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = cnn;

                cmd.Parameters.AddWithValue("@usu_nombre_usuario", nombreUsuario);

                cnn.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    usuario = new Usuario
                    {
                        Usu_ID = Convert.ToInt32(reader["usu_id"]),
                        Usu_NombreUsuario = reader["usu_nombre_usuario"].ToString(),
                        Usu_Contrasenia = reader["usu_contraseña"].ToString(),
                        Usu_ApellidoNombre = reader["usu_apellido_nombre"].ToString(),
                        Rol_ID = Convert.ToInt32(reader["rol_id"])
                    };
                }
                reader.Close();
            }
            return usuario;
        }

        public int InsertarUsuario(Usuario usuario)
        {
            int resultado = 0;
            using (SqlConnection cnn = new SqlConnection(ClasesBase.Properties.Settings.Default.institutoConnectionString))
            {
                SqlCommand cmd = new SqlCommand("insertar_usuario_sp", cnn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@usu_nombre_usuario", usuario.Usu_NombreUsuario);
                cmd.Parameters.AddWithValue("@usu_contraseña", usuario.Usu_Contrasenia);
                cmd.Parameters.AddWithValue("@usu_apellido_nombre", usuario.Usu_ApellidoNombre);
                cmd.Parameters.AddWithValue("@rol_id", usuario.Rol_ID);

                SqlParameter returnParameter = cmd.Parameters.Add("@ReturnValue", SqlDbType.Int);
                returnParameter.Direction = ParameterDirection.ReturnValue;

                cnn.Open();
                cmd.ExecuteNonQuery();
                resultado = (int)returnParameter.Value;
            }
            return resultado;
        }

        public void ModificarUsuario(Usuario usuario)
        {
            using (SqlConnection cnn = new SqlConnection(ClasesBase.Properties.Settings.Default.institutoConnectionString))
            {
                string updateQuery = @"UPDATE Usuario SET usu_nombre_usuario = @username, usu_contraseña = @password, usu_apellido_nombre = @apeynom, rol_id = @rolid WHERE usu_id = @id";
                SqlCommand cmd = new SqlCommand(updateQuery, cnn);
                cmd.CommandType = CommandType.Text;

                cmd.Parameters.AddWithValue("@id", usuario.Usu_ID);
                cmd.Parameters.AddWithValue("@username", usuario.Usu_NombreUsuario);
                cmd.Parameters.AddWithValue("@password", usuario.Usu_Contrasenia);
                cmd.Parameters.AddWithValue("@apeynom", usuario.Usu_ApellidoNombre);
                cmd.Parameters.AddWithValue("@rolid", usuario.Rol_ID);

                cnn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public void EliminarUsuario(int idUsuario)
        {
            using (SqlConnection cnn = new SqlConnection(ClasesBase.Properties.Settings.Default.institutoConnectionString))
            {
                string deleteQuery = "DELETE FROM Usuario WHERE usu_id = @id";
                SqlCommand cmd = new SqlCommand(deleteQuery, cnn);
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("@id", idUsuario);

                cnn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public Usuario ValidarLogin(string nombreUsuario, string contrasenia)
        {
            Usuario usuario = TraerUsuario(nombreUsuario);
            if (usuario != null && usuario.Usu_Contrasenia == contrasenia)
            {
                return usuario;
            }
            return null;
        }

        public bool ExisteUsuario(string nombreUsuario)
        {
            return TraerUsuario(nombreUsuario) != null;
        }

     
    }
}