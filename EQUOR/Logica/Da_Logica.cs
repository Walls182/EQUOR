using EQUOR.Models;
using Microsoft.Data.SqlClient;

namespace EQUOR.Logica
{
    public class Da_Logica
    {
        

        public Consumer ValidarUsuario(string _correo, string _clave)
        {
            string connectionString = "Server=DESKTOP-JK7M5A1\\SQLEXPRESS;Database=EQUOR;Integrated Security=True;trustServerCertificate=True";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string sql = "SELECT * FROM Consumers WHERE Email=@Email AND Password=@Password";
                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@Email", _correo);
                    command.Parameters.AddWithValue("@Password", _clave);
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            Consumer consumer = new Consumer();
                            consumer.Email = reader.GetString(1);
                            consumer.Password = reader.GetString(2);
                            return consumer;
                        }
                    }
                }
            }
            return null;
        }

        public Company ValidarCompany(string _correo, string _clave)
        {
            string connectionString = "Server=DESKTOP-JK7M5A1\\SQLEXPRESS;Database=EQUOR;Integrated Security=True;trustServerCertificate=True";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string sql = "SELECT * FROM Companies WHERE Email=@Email AND Password=@Password";
                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@Email", _correo);
                    command.Parameters.AddWithValue("@Password", _clave);
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            Company company = new Company();
                            company.Email = reader.GetString(1);
                            company.Password = reader.GetString(2);
                            return company;
                        }
                    }
                }
            }
            return null;
        }


    }
}
