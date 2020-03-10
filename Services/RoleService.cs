using System;
using System.Collections.Generic;
using testEvolution.Data;
using testEvolution.Interfaces;
using testEvolution.Models.Entities;
using testEvolution.Models.Enums;

namespace evolutionPrueba.Services
{
    public class RoleService : BaseData, IData<Role>
    {
        public Role Add(Role role)
        {
            if(role == null) return null;
            //INSERT INTO users OUTPUT INSERTED.* VALUES ('aaaa', 'asdsdad', 1)
            sqlCommand.CommandText = "INSERT INTO roles VALUES(@name, @description, @state)";
            sqlCommand.Parameters.AddWithValue("@name", role.Name);
            sqlCommand.Parameters.AddWithValue("@description", role.Description);
            sqlCommand.Parameters.AddWithValue("@state", State.ACTIVO);
            try{
                Connection.Open();
                int result  = sqlCommand.ExecuteNonQuery();
                Connection.Close();
                return result > 0 ? role: null;
            }catch(Exception e){
                Console.WriteLine(e.Message);
                Connection.Close();
                return null;
            }
        }

        public Role Edit(int id, Role role)
        {
            if(role == null) return null;
            //INSERT INTO users OUTPUT INSERTED.* VALUES ('aaaa', 'asdsdad', 1)
            sqlCommand.CommandText = $"UPDATE roles SET name = @name, description = @description, state = @state WHERE id = {id}";
            sqlCommand.Parameters.AddWithValue("@name", role.Name);
            sqlCommand.Parameters.AddWithValue("@description", role.Description);// new PasswordHasher().HashPassword(model.Password));
            sqlCommand.Parameters.AddWithValue("@state", role.State);
            try{
                Connection.Open();
                int result  = sqlCommand.ExecuteNonQuery();
                return result > 0?role:null;
            }catch(Exception e){
                Console.WriteLine(e.Message);
                Connection.Close();
                return null;
            }
        }

        public Role Find(int id)
        {
            sqlCommand.CommandText = $"SELECT * FROM roles where id={id}";
            try
            {
                Connection.Open();
                reader = sqlCommand.ExecuteReader();
                Role role = reader.Read()? new Role(reader): null;
                Connection.Close();
                return role;
            }
            catch (Exception e)
            {
                Connection.Close();
                return null;
            }
        }

        public IList<Role> GetAll()
        {
            IList<Role> roles = new List<Role>();
            sqlCommand.CommandText = "SELECT * FROM roles";
            try
            {
                Connection.Open();
                reader = sqlCommand.ExecuteReader();
                while (reader.Read())
                {
                    Role r = new Role(reader);
                    if(r==null) return null;
                    roles.Add(r);
                }
                Connection.Close();
                return roles;
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