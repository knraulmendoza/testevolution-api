using evolutionPrueba.Services;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using testEvolution.Data;
using testEvolution.Helpers;
using testEvolution.Interfaces;
using testEvolution.Models.Entities;
using testEvolution.Models.Enums;

namespace testEvolution.Services
{
    public class UserService : BaseData, IData<User>
    {
        public readonly RoleService _roleService;
        public readonly UserRoleService _roleUserService;
        public UserService(){
            _roleService = new RoleService();
            _roleUserService = new UserRoleService();
        }
        public User Add(User user)
        {
            if (user != null)
            {
                //INSERT INTO users OUTPUT INSERTED.* VALUES ('aaaa', 'asdsdad', 1)
                sqlCommand.CommandText = "INSERT INTO users VALUES(@user_name, @password, @state)";
                sqlCommand.Parameters.AddWithValue("@user_name", user.Username);
                sqlCommand.Parameters.AddWithValue("@password", new PasswordHasher().HashPassword(user.Password));// new PasswordHasher().HashPassword(model.Password));
                sqlCommand.Parameters.AddWithValue("@state", State.ACTIVO);
                try
                {
                    Connection.Open();
                    if (_roleService.Find(user.roleId) == null) return null;
                    int result = sqlCommand.ExecuteNonQuery();
                    if (result > 0)
                    {
                        user.Id = GetInsert();
                        Console.WriteLine(user.Id);
                        if (_roleUserService.Add(new RoleUser(user.roleId, user.Id)) == null) return null;
                        return user;
                    }
                    return null;
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                    Connection.Close();
                    return null;
                }
            }

            return null;
        }

        public int GetInsert(){
            sqlCommand.CommandText = "SELECT MAX(id) as id FROM users";
            reader = sqlCommand.ExecuteReader();
            return reader.Read()?Convert.ToInt32(reader["id"]):0;
        }

        public User Edit(int id, User user)
        {
            if(user == null) return null;
            //INSERT INTO users OUTPUT INSERTED.* VALUES ('aaaa', 'asdsdad', 1)
            sqlCommand.CommandText = $"UPDATE users SET user_name = @user_name, password = @password, state = @state WHERE id = {id}";
            sqlCommand.Parameters.AddWithValue("@user_name", user.Username);
            sqlCommand.Parameters.AddWithValue("@password", user.Password);// new PasswordHasher().HashPassword(model.Password));
            sqlCommand.Parameters.AddWithValue("@state", user.State);
            try{
                Connection.Open();
                int result  = sqlCommand.ExecuteNonQuery();
                return result > 0?user:null;
            }catch(Exception e){
                Console.WriteLine(e.Message);
                Connection.Close();
                return null;
            }
        }

        public User Find(User model)
        {
            sqlCommand.CommandText = $"SELECT * FROM users INNER JOIN users_roles ON users_roles.user_id = dbo.users.id INNER JOIN roles ON users_roles.role_id = roles.id WHERE users.user_name='{model.Username}' AND users.state = {Convert.ToInt32(State.ACTIVO)}";
            Connection.Open();
            reader = sqlCommand.ExecuteReader();
            User user = reader.Read()? new User(reader): null;
            Connection.Close();
            return (GetPasswordHasher(user.Password, model.Password) == PasswordVerificationResult.Success)? user: null;
        }
        public User Find(int id)
        {
            sqlCommand.CommandText = $"SELECT * FROM users INNER JOIN users_roles ON users_roles.user_id = dbo.users.id INNER JOIN roles ON users_roles.role_id = roles.id WHERE users.id = {id}";
            try
            {
                Connection.Open();
                reader = sqlCommand.ExecuteReader();
                User user = reader.Read()? new User(reader): null;
                Console.WriteLine(user);
                Connection.Close();
                return user;
            }
            catch (Exception e)
            {
                Connection.Close();
                return null;
            }
        }

        public PasswordVerificationResult GetPasswordHasher(string passwordHasher, string password){
            return new PasswordHasher().VerifyHashedPassword(passwordHasher, password);
        }
        public IList<User> GetAll()
        {
            throw new NotImplementedException();
        }
    }
}
