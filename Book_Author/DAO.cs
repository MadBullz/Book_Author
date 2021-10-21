using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Text;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace Book_Author
{
    class Author
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public DateTime DOB { get; set; }
    }

    class Book
    {
        public int ID { get; set; }
        public string title { get; set; }
        public int Year { get; set; }
        public string Description { get; set; }

        public List<Author> authors { get; set; }
    }

    class DAO
    {
        //Khai bao doi tuong ket noi 
        SqlConnection connection;

        //Khai bao doi tuong thuc thi cac truy van
        SqlCommand command;

        //Phuong thuc lay chuoi ket noi tu appsettings.json
        string GetConnectionString()
        {
            //Khai bao va lay chuoi ket noi tu appsettings.json
            IConfiguration config = new ConfigurationBuilder()
                                    .SetBasePath(Directory.GetCurrentDirectory())
                                    .AddJsonFile("appsettings.json", true, true).Build();

            return config["ConnectionString:MyBookDB"];
        }

        public int deleteAuthor(int bookID, int authorID)
        {
            int result = 0;

            connection = new SqlConnection(GetConnectionString());
            command = new SqlCommand("delete from Author_Book where BookID = @bookID and AuthorID = @authorID", connection);
            command.Parameters.AddWithValue("@bookID", bookID);
            command.Parameters.AddWithValue("@authorID", authorID);

            try
            {
                connection.Open();
                result = command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                connection.Close();
            }

            return result;
        }

        public List<Book> GetBooks()
        {

            List<Book> books = new List<Book>();
            connection = new SqlConnection(GetConnectionString());
            command = new SqlCommand("select * from Books", connection);
            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader(CommandBehavior.CloseConnection);
                if (reader.HasRows == true)
                {
                    while (reader.Read())
                    {
                        books.Add(new Book
                        {
                            ID = reader.GetInt32("ID"),
                            title = reader.GetString("Title"),
                            Year = reader.GetInt32("Year"),
                            Description = reader.GetString("Description"),
                            authors = getAuthor(reader.GetInt32("ID"))
                        });
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                connection.Close();
            }
            return books;
        }

        public List<Author> getAuthor(int bookID)
        {
            List<Author> authors = new List<Author>();
            connection = new SqlConnection(GetConnectionString());
            command = new SqlCommand("select * from Authors inner join Author_Book on Authors.ID = Author_Book.AuthorID where BookID = @ID", connection);
            command.Parameters.AddWithValue("@ID", bookID);
            
            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader(CommandBehavior.CloseConnection);
                if (reader.HasRows == true)
                {
                    while (reader.Read())
                    {
                        authors.Add(new Author
                        {
                            ID = reader.GetInt32("ID"),
                            Name = reader.GetString("Name"),
                            DOB = reader.GetDateTime("DOB")
                        }); ;
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                connection.Close();
            }
            return authors;
        }
    }
}
