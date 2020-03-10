using System.Linq;
using System;
using System.Collections.Generic;
using testEvolution.Data;
using testEvolution.Interfaces;
using testEvolution.Models.Entities;
using testEvolution.Services;

namespace evolutionPrueba.Services
{
    public class PersonService : BaseData, IData<Person>
    {
        public readonly UserService _userService;
        public PersonService(){
            _userService = new UserService();
        }
        public Person Add(Person person)
        {
            if(person == null) return null;
            //INSERT INTO users OUTPUT INSERTED.* VALUES ('aaaa', 'asdsdad', 1)
            User user = _userService.Add(person.user);
            Console.WriteLine(user.Username);
            if(user == null) return null;
            person.UserId = user.Id;
            person.user = user;
            Console.WriteLine("person " + person.name);
            Console.WriteLine("usuario : "+user.Username);
            sqlCommand.CommandText = "INSERT INTO persons VALUES(@first_name, @second_name, @first_last_name, @second_last_name, @user_id)";
            sqlCommand.Parameters.AddWithValue("@first_name", person.FirstName);
            sqlCommand.Parameters.AddWithValue("@second_name", person.SecondName);// new PasswordHasher().HashPassword(model.Password));
            sqlCommand.Parameters.AddWithValue("@first_last_name", person.FirstLastName);
            sqlCommand.Parameters.AddWithValue("@second_last_name", person.SecondLastName);
            sqlCommand.Parameters.AddWithValue("@user_id", user.Id);
            try
            {
                Connection.Open();
                int result = sqlCommand.ExecuteNonQuery();
                Connection.Close();
                return result > 0? person:null;
            }
            catch (Exception e)
            {
                Console.WriteLine("genero una exception : " + e.Message.ToString());
                Connection.Close();
                return null;
            }
        }

        public Person Edit(int id, Person person)
        {
            if(person == null) return null;
            //INSERT INTO users OUTPUT INSERTED.* VALUES ('aaaa', 'asdsdad', 1)
            User user = _userService.Edit(person.UserId,person.user);
            if(user == null) return null;
            person.UserId = user.Id;
            person.user = user;
            sqlCommand.CommandText = $"UPDATE INTO persons VALUES(@first_name, @second_name, @first_last_name, @second_last_name, @user_id) WHERE id = {person.Id}";
            sqlCommand.Parameters.AddWithValue("@first_name", person.FirstName);
            sqlCommand.Parameters.AddWithValue("@second_name", person.SecondName);// new PasswordHasher().HashPassword(model.Password));
            sqlCommand.Parameters.AddWithValue("@first_last_name", person.FirstLastName);
            sqlCommand.Parameters.AddWithValue("@second_last_name", person.SecondLastName);
            sqlCommand.Parameters.AddWithValue("@user_id", user.Id);
            try
            {
                Connection.Open();
                int result = sqlCommand.ExecuteNonQuery();
                Connection.Close();
                return result > 0? person:null;
            }
            catch (Exception e)
            {
                Console.WriteLine("genero una exception : " + e.Message.ToString());
                Connection.Close();
                return null;
            }
        }

        public Person Find(int id)
        {
            if(id == null && Convert.ToInt32(id) < 0) return null;
            sqlCommand.CommandText = $"SELECT * FROM persons where id={id}";
            try
            {
                Connection.Open();
                reader = sqlCommand.ExecuteReader();
                Person person = reader.Read()? new Person(reader,_userService.Find(Convert.ToInt32(reader["user_id"]))):null;
                Connection.Close();
                return person;
            }
            catch (Exception)
            {
                Connection.Close();
                return null;
            }
        }
        public Person FindUser(int userId)
        {
            if(userId == null && Convert.ToInt32(userId) < 0) return null;
            sqlCommand.CommandText = $"SELECT * FROM persons where user_id={userId}";
            try
            {
                Connection.Open();
                reader = sqlCommand.ExecuteReader();
                Person person = reader.Read()? new Person(reader,_userService.Find(Convert.ToInt32(reader["user_id"]))):null;
                Connection.Close();
                return person;
            }
            catch (Exception)
            {
                Connection.Close();
                return null;
            }
        }

        public IList<Person> GetAll()
        {
            IList<Person> persons = new List<Person>();
            sqlCommand.CommandText = "SELECT * FROM persons";
            try
            {
                Connection.Open();
                reader = sqlCommand.ExecuteReader();
                while (reader.Read())
                {
                    Person p = new Person(reader, _userService.Find(Convert.ToInt32(reader["user_id"])));
                    if(p==null) return null;
                    Console.WriteLine(p.FirstLastName);
                    persons.Add(p);
                }
                Connection.Close();
                return persons;
            }
            catch (Exception e)
            {
                Console.WriteLine("genero una exception : " + e.Message.ToString());
                Connection.Close();
                return null;
            }
        }
    }
}