using System.Dynamic;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using testEvolution.Models.Base;
using testEvolution.Models.Enums;

namespace testEvolution.Models.Entities
{
    public class Role : Model<Role>
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public State State { get; set; }

        public Role(){}
        public Role(SqlDataReader reader){
            Id = Convert.ToInt32(reader["id"]);
            Name = reader["name"].ToString();
            Description = reader["description"].ToString()??"";
            State = (State)Convert.ToInt32(reader["state"]);
        }
    }
}
