using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using financial_app_api.Security.models;
using financial_app_api.Services.Security.repository;
using financial_app_api.Shared.Models;
using financial_app_api.Shared.Commands;
using financial_app_api.Shared.Utils;
using Microsoft.IdentityModel.Tokens;

using Scrypt;
using financial_app_api.Services.Security;

namespace financial_app_api.Security.Commands
{
    public class LoginCommand : FinancialApiCommand<Credentials, AuthToken>
    {

        public LoginCommand(Credentials parameters) : base(parameters)
        {
        }

        public override CommandResult<AuthToken> Execute()
        {

            var repo = new LoginRepository();

            var user = repo.GetUser(parameters.user);

            if (user == null)
            {
                return MakeResult(new AuthToken { message = "Usuário ou senha inválidos." });
            }
            else if (!PasswordUtil.VerifyPassword(parameters.password, user.password))
            {
                return MakeResult(new AuthToken { message = "Usuário ou senha inválidos." });
            }

            var rt = Guid.NewGuid().ToString("n");
            var session_id = repo.CreateSession(parameters, rt, user.user_id).ToString();

            var token = GenToken(session_id, user.name, parameters.user, user.cpf, rt, user.user_id);

            return MakeResult(token);

            throw new ArgumentException("Invalid parameters.type");
        }
        private AuthToken GenToken(string session_id, string name, string email, string cpf, string refreshToken, int userID)
        {
            //https://ckeditor.com/docs/cs/latest/examples/token-endpoints/dotnet.html

            var handler = new JwtSecurityTokenHandler();
            ClaimsIdentity identity = new ClaimsIdentity(
            new[] {
                new Claim("id", session_id),
                new Claim("id_user", userID.ToString()),
                new Claim("email", email.ToString()),
                new Claim("cpf", cpf)
            }
            );

            var requestAt = DateTime.Now;
            var expiresIn = JwtTokenDefinitions.TokenExpirationTime;
            var expires = requestAt.Add(expiresIn);

            var securityToken = handler.CreateToken(new SecurityTokenDescriptor
            {
                Issuer = JwtTokenDefinitions.Issuer,
                Audience = JwtTokenDefinitions.Audience,
                SigningCredentials = JwtTokenDefinitions.SigningCredentials,
                Subject = identity,
                Expires = expires,
                NotBefore = requestAt.AddMinutes(-5),
                IssuedAt = requestAt
            });

            var token = handler.WriteToken(securityToken);

            return new AuthToken
            {
                authenticated = true,
                created = requestAt,
                expiration = expires,
                expiresIn = expiresIn.TotalSeconds,
                requestAt = DateTime.Now,
                accessToken = token,
                refreshToken = refreshToken,
                session_id = session_id,
                name = name,
                change_password = false
            };
        }


    }
}