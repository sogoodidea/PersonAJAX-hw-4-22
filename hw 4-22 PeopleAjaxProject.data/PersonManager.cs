using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace hw_4_22_PeopleAjaxProject.data
{
    public class PersonManager
    {
        private string _conStr = @"Data Source=.\sqlexpress;Initial Catalog=MySecondDb;Integrated Security=True;";

        public List<Person> GetPeople()
        {
            using (SqlConnection conn = new SqlConnection(_conStr))
            using (SqlCommand cmd = conn.CreateCommand())
            {
                cmd.CommandText = "SELECT * FROM People";
                conn.Open();
                List<Person> people = new List<Person>();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    people.Add(new Person
                    {
                        Id = (int)reader["Id"],
                        FirstName = (string)reader["FirstName"],
                        LastName = (string)reader["LastName"],
                        Age = (int)reader["Age"]
                    });
                }
                return people;
            }
        }
        public void AddPerson(Person person)
        {
            using (SqlConnection conn = new SqlConnection(_conStr))
            using (SqlCommand cmd = conn.CreateCommand())
            {
                cmd.CommandText = @"INSERT INTO People (FirstName, LastName, Age)
                                VALUES (@firstName, @lastName, @age)";
                cmd.Parameters.AddWithValue("@firstName", person.FirstName);
                cmd.Parameters.AddWithValue("@lastName", person.LastName);
                cmd.Parameters.AddWithValue("@age", person.Age);
                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }
        public void DeletePerson(int id)
        {
            using (SqlConnection conn = new SqlConnection(_conStr))
            using (SqlCommand cmd = conn.CreateCommand())
            {
                cmd.CommandText = @"DELETE FROM People
                                WHERE Id = @id";
                cmd.Parameters.AddWithValue("@id", id);
                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }
        public void UpdatePerson(Person person)
        {
            using (SqlConnection conn = new SqlConnection(_conStr))
            using (SqlCommand cmd = conn.CreateCommand())
            {
                cmd.CommandText = @"UPDATE People 
                                SET FirstName = @firstName, LastName = @lastName, Age = @age
                                WHERE Id = @id";
                cmd.Parameters.AddWithValue("@firstName", person.FirstName);
                cmd.Parameters.AddWithValue("@lastName", person.LastName);
                cmd.Parameters.AddWithValue("@age", person.Age);
                cmd.Parameters.AddWithValue("@id", person.Id);
                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }
    }
}
