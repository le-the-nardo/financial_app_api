using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using financial_app_api.Services.Security.models;
using financial_app_api.Shared.DataBase;

namespace financial_app_api.Services.User
{
    public class UserRepository : FinancialAppDataBase
    {

        internal AppUser GetUser(String email)
        {
            AppUser appUser = new AppUser();

            var select = "SELECT id, cpf, name, password, email FROM user_login WHERE email = @email";

            using (var cmd = GetCmd(select))
            {
                cmd.Parameters.AddWithValue("email", email);
                using var reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    appUser.user_id = Convert.ToInt32(reader["id"]);
                    appUser.cpf = reader["cpf"].ToString();
                    appUser.name = reader["name"].ToString();
                    appUser.password = reader["password"].ToString();
                    appUser.email = reader["email"].ToString();
                }
                else
                {
                    return null;
                }
            }

            return appUser;
        }

    }
}