using financial_app_api.Security.models;
using financial_app_api.Services.User;
using financial_app_api.Shared.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace financial_app_api.Services.Security.repository
{
    public class LoginRepository : UserRepository
    {
        internal long CreateSession(Credentials credentials, string rt, int userID)
        {
            var select = "INSERT INTO app_session (session_start, active_until, refresh_token, user_id) " +
                        " VALUES (@session_start, @active_until, @rt, @user_id); " +
                        " SELECT MAX(session_id) FROM app_session; ";

            using (var cmd = GetCmd(select))
            {
                cmd.Parameters.AddWithValue("session_start", DateTime.Now);
                cmd.Parameters.AddWithValue("rt", rt);
                cmd.Parameters.AddWithValue("active_until", DateTime.Now.Add(JwtTokenDefinitions.RefreshTokenExpirationTime));
                cmd.Parameters.AddWithValue("user_id", userID);

                var query = cmd.ExecuteScalar();

                return Convert.ToInt64(query);
            }
        }
        internal void UpdateSession(AuthToken token)
        {
            var select = "UPDATE app_session SET active_until = @active_until, refresh_token = @refresh_token " +
                "WHERE session_id = @session_id";

            using (var cmd = GetCmd(select))
            {
                cmd.Parameters.AddWithValue("session_id", token.session_id);
                cmd.Parameters.AddWithValue("refresh_token", token.refreshToken);
                cmd.Parameters.AddWithValue("active_until", DateTime.Now.Add(JwtTokenDefinitions.RefreshTokenExpirationTime));

                var query = cmd.ExecuteScalar();
            }

        }
    }
}
