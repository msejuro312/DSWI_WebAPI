using Microsoft.Data.SqlClient;
using WebApplication1.Interfaces;
using WebApplication1.Models;

namespace WebApplication1.Services
{
    public class LibroService: ILibroService
    {
        private readonly string? conexion;

        public LibroService(IConfiguration configuration)
        {
            conexion = configuration.GetConnectionString("conexion");


        }


        public List<Libro> list()
        {
            List<Libro> temporal = new List<Libro>();

            using (SqlConnection con = new SqlConnection(conexion))
            {
                using (SqlCommand command = new SqlCommand("sp_list_libros", con))
                {
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    con.Open();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Libro libro = new Libro
                            {
                                LibroId = reader.GetInt32(0),
                                Titulo = reader.GetString(1),
                                ISBN = reader.GetString(2),
                                AnioPublicacion = reader.GetInt32(3),
                                Autor = reader.GetString(4)
                            };
                            temporal.Add(libro);
                        }
                    }

                }
            }
            return temporal;
        }

        public Libro getById(int LibroId)
        {
            Libro libro = new Libro();

            using (SqlConnection con = new SqlConnection(conexion))
            {
                using (SqlCommand command = new SqlCommand("sp_find_libro_by_id", con))
                {
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@LibroId", LibroId);
                    con.Open();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            libro = new Libro
                            {
                                LibroId = reader.GetInt32(0),
                                Titulo = reader.GetString(1),
                                ISBN = reader.GetString(2),
                                AnioPublicacion = reader.GetInt32(3),
                                Autor = reader.GetString(4)
                            };
                            
                        }
                    }

                }
            }
            return libro; ;
        }

        public bool insert(Libro libro)
        {
            throw new NotImplementedException();
        }

        public bool delete(int LibroId)
        {
            throw new NotImplementedException();
        }

       

       

        

        public bool update(Libro libro)
        {
            throw new NotImplementedException();
        }
    }
}
