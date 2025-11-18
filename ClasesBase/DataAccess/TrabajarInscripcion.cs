using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace ClasesBase.DataAccess
{
   public class TrabajarInscripcion
    {
        public static DataTable insertar_inscripcion(DateTime ins_fecha, int cur_id, int alu_id, int est_id)
        {
            SqlConnection cnn = new SqlConnection(ClasesBase.Properties.Settings.Default.institutoConnectionString);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "insertar_inscripcion_sp";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = cnn;

            // Pasar solo fecha (sin hora)
            cmd.Parameters.AddWithValue("@ins_fecha", ins_fecha.Date);

            cmd.Parameters.AddWithValue("@cur_id", cur_id);
            cmd.Parameters.AddWithValue("@alu_id", alu_id);
            cmd.Parameters.AddWithValue("@est_id", est_id);

            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);

            return dt;
        }

        public static bool alumno_ya_inscripto(int alu_id, int cur_id)
        {
            bool resultado = false;

            using (SqlConnection cnn = new SqlConnection(ClasesBase.Properties.Settings.Default.institutoConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand("alumno_ya_inscripto_sp", cnn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@alu_id", alu_id);
                    cmd.Parameters.AddWithValue("@cur_id", cur_id);

                    cnn.Open();

                    object res = cmd.ExecuteScalar();

                    if (res != null && res != DBNull.Value)
                    {
                        // SP devuelve 1 o 0 (bit)
                        resultado = Convert.ToBoolean(res);
                    }
                }
            }

            return resultado;
        }




    }
}
