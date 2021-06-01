using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using financial_app_api.Services.Security.models;
using financial_app_api.Services.User;

namespace financial_app_api.Services.Security.repository
{
    public class CreateUserRepository : UserRepository
    {
        internal void CreateUser(CreateUser newUser)
        {
            var select = "INSERT INTO user_login (cpf, name, password, email) " +
                        " VALUES (@cpf, @name, @password, @email);";

            using (var cmd = GetCmd(select))
            {
                cmd.Parameters.AddWithValue("cpf", newUser.cpf);
                cmd.Parameters.AddWithValue("name", newUser.name);
                cmd.Parameters.AddWithValue("password", newUser.password);
                cmd.Parameters.AddWithValue("email", newUser.email);

                var query = cmd.ExecuteScalar();
            }
        }
    }
}