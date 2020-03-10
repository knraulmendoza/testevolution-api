using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using testEvolution.Models.Base;

namespace testEvolution.Models.Entities
{
    public class RoleUser : Model<RoleUser>
    {
        
        public int Role_id { get; set; }
        public int User_id { get; set; }
        public RoleUser(){}

        public RoleUser(int role_id, int user_id)
        {
            this.Role_id = role_id;
            this.User_id = user_id;

        }
    }
}
