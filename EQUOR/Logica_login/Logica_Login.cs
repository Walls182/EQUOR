using EQUOR.Models;
using Microsoft.Data.SqlClient;
using System.Data;

namespace EQUOR.Logica_login
{
    public class Logica_Login
    {
        public Manager EncontrarManager(string correo, string clave)
        {

            Manager objeto = new Manager();

            using (SqlConnection con = new SqlConnection("Server=DESKTOP-JK7M5A1\\SQLEXPRESS;Database=EQUOR;Integrated Security=True;trustServerCertificate=True"))
            {

                string query = "select Email,Password from Manager where Email=@pEmail and Password=@pPassword";

                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@pEmail", correo);
                cmd.Parameters.AddWithValue("@pPassword", clave);

                cmd.CommandType = CommandType.Text;

                con.Open();

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        objeto = new Manager()
                        {
                            Email = reader["Email"].ToString(),
                            Password = reader["Password"].ToString(),
                            IdRole = (int)reader["IdRole"],

                        };
                    }
                }
            }
            return objeto;
        }
        public Consumer EncontrarConsumer(string correo, string clave)
        {

            Consumer objeto = new Consumer();

            using (SqlConnection con = new SqlConnection("Server=DESKTOP-JK7M5A1\\SQLEXPRESS;Database=EQUOR;Integrated Security=True;trustServerCertificate=True"))
            {

                string query = "select Email,Password from Consumers where Email=@pEmail and Password=@pPassword";

                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@pEmail", correo);
                cmd.Parameters.AddWithValue("@pPassword", clave);

                cmd.CommandType = CommandType.Text;

                con.Open();

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        objeto = new Consumer()
                        {
                            Email = reader["Email"].ToString(),
                            Password = reader["Password"].ToString(),
                            

                        };
                    }
                }
            }
            return objeto;
        }
        public Company EncontrarCompany(string correo, string clave)
        {

            Company objeto = new Company();

            using (SqlConnection con = new SqlConnection("Server=DESKTOP-JK7M5A1\\SQLEXPRESS;Database=EQUOR;Integrated Security=True;trustServerCertificate=True"))
            {

                string query = "select Email,Password from Manager where Email=@pEmail and Password=@pPassword";

                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@pEmail", correo);
                cmd.Parameters.AddWithValue("@pPassword", clave);

                cmd.CommandType = CommandType.Text;

                con.Open();

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        objeto = new Company()
                        {
                            Email = reader["Email"].ToString(),
                            Password = reader["Password"].ToString(),
                            IdRole = (int)reader["IdRole"],

                        };
                    }
                }
            }
            return objeto;
        }
    }
}
