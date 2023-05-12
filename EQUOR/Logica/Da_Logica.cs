using EQUOR.DataContext;
using EQUOR.Models;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using NuGet.Protocol.Plugins;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace EQUOR.Logica
{
    public class Da_Logica
    {

       public string connectionString = "Server=DESKTOP-JK7M5A1\\SQLEXPRESS;Database=EQUOR;Integrated Security=True;trustServerCertificate=True";
       

        public Consumer ValidarUsuario(string _correo, string _clave)
        {
           
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
                            consumer.Email = reader.GetString(2);
                            consumer.Password = reader.GetString(3);
                            consumer.IdRole = reader.GetInt32(4);


                            return consumer;
                        }
                    }
                }
            }
            return null;
        }

        public Company ValidarCompany(string _correo, string _clave)
        {
            
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
                            company.Email = reader.GetString(5);
                            company.Password = reader.GetString(6);
                            company.IdRole = reader.GetInt32(7);
                            return company;
                        }
                    }
                }
            }
            return null;
        }
        public Manager ValidarManager(string _correo, string _clave)
        {
            
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string sql = "SELECT * FROM Manager WHERE Email=@Email AND Password=@Password";
                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@Email", _correo);
                    command.Parameters.AddWithValue("@Password", _clave);
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            Manager manager = new Manager();
                            manager.Email = reader.GetString(2);
                            manager.Password = reader.GetString(3);
                            manager.IdRole = reader.GetInt32(5);
                            return manager;
                        }
                    }
                }
            }
            return null;
        }


    }
}
