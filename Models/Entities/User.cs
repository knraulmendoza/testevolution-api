using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using testEvolution.Models.Base;
using testEvolution.Models.Enums;
using testEvolution.Helpers;

namespace testEvolution.Models.Entities
{
    public class User : Model<User>
    {
        // [Required]
        // [StringLength(30)]
        public string Username { get; set; }
        // [Required]
        // [StringLength(30)]
        public string Password { get; set; }
        // [Required]
        public State State { get; set; }
        public string token { get; set;}
        public int roleId { get; set;}

        public User(){}
        public User(SqlDataReader reader){
            Id = Convert.ToInt32(reader["id"]);
            Username = reader["user_name"].ToString();
            Password = reader["password"].ToString();
            State = (State)Convert.ToInt32(reader["state"]);
            roleId = Convert.ToInt32(reader["role_id"]);
        }
    }
}
