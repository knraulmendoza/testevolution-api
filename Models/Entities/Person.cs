using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using testEvolution.Models.Base;
using testEvolution.Models.Enums;

namespace testEvolution.Models.Entities
{
    public class Person : Model<Person>
    {
        // [Required]
        // [StringLength(30)]
        public string FirstName { get; set; }
        // [StringLength(30)]
        public string SecondName { get;set; }
        // [Required]
        // [StringLength(30)]
        public string FirstLastName { get; set; }
        // [Required]
        // [StringLength(30)]
        public string SecondLastName { get; set; }
        // [Required]
        // [StringLength(30)]
        public int UserId { get; set;}
        public User user { get; set;}

        public Person(){}
        public Person(SqlDataReader reader, User _user)
        {
            Id = Convert.ToInt32(reader["id"]);
            FirstName = reader["first_name"].ToString();
            SecondName = reader["second_name"]?.ToString()??"";
            FirstLastName = reader["first_last_name"].ToString();
            SecondLastName = reader["second_last_name"].ToString();
            UserId = Convert.ToInt32(reader["user_id"]);
            user = _user;
        }

        public string name {
            get => FirstName + (SecondName==""?" ":$" {SecondName} ")+FirstLastName+ $" {SecondLastName}";
        }
        public string names {
            get => FirstName + (SecondName==""?" ":$" {SecondName}");
        }
        public string lasnames {
            get => FirstName + $" {FirstLastName}"+ $" {SecondLastName}";
        }
    }
}
