using System;
using System.Collections.Generic;
using testEvolution.Data;
using testEvolution.Interfaces;
using testEvolution.Models.Entities;

namespace evolutionPrueba.Services
{
    public class UserRoleService : BaseData, IData<RoleUser>
    {

        public RoleUser Add(RoleUser roleUser)
        {
            if(roleUser == null) return null;
            //INSERT INTO users OUTPUT INSERTED.* VALUES ('aaaa', 'asdsdad', 1)
            sqlCommand.CommandText = "INSERT INTO users_roles VALUES(@user_id, @role_id)";
            sqlCommand.Parameters.AddWithValue("@user_id", roleUser.User_id);
            sqlCommand.Parameters.AddWithValue("@role_id", roleUser.Role_id);
            try{
                Connection.Open();
                int result  = sqlCommand.ExecuteNonQuery();
                Connection.Close();
                return result > 0 ?roleUser : null;
            }catch(Exception e){
                Connection.Close();
                return null;
            }
        }

        public RoleUser Edit(int id, RoleUser model)
        {
            throw new System.NotImplementedException();
        }

        public RoleUser Find(int id)
        {
            throw new System.NotImplementedException();
        }

        public IList<RoleUser> GetAll()
        {
            throw new System.NotImplementedException();
        }
    }
}