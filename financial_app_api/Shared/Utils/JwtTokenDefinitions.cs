using System;
using System.Collections.Generic;
using System.Text;

using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;


namespace financial_app_api.Shared.Utils
{
    public class JwtTokenDefinitions
    {
        public static void LoadFromConfiguration(IConfiguration configuration)
        {
            var config = configuration.GetSection("JwtConfiguration");
            Key = config.GetValue<string>("Key");
            Audience = config.GetValue<string>("Audience");
            Issuer = config.GetValue<string>("Issuer");

            try
            {
                if (!string.IsNullOrWhiteSpace(Environment.GetEnvironmentVariable("JWT_TOKEN_EXPIRATION_TIME")))
                {
                    TokenExpirationTime = TimeSpan.FromMinutes(int.Parse(Environment.GetEnvironmentVariable("JWT_TOKEN_EXPIRATION_TIME")));
                }
                else
                    TokenExpirationTime = TimeSpan.FromMinutes(config.GetValue<int>("TokenExpirationTime"));
            }
            catch
            {
                TokenExpirationTime = TimeSpan.FromMinutes(config.GetValue<int>("TokenExpirationTime"));
            }
            Console.WriteLine($"TokenExpirationTime: {TokenExpirationTime}");
            try
            {
                if (!string.IsNullOrWhiteSpace(Environment.GetEnvironmentVariable("JWT_REFRESH_EXPIRATION_TIME")))
                {
                    RefreshTokenExpirationTime = TimeSpan.FromMinutes(int.Parse(Environment.GetEnvironmentVariable("JWT_REFRESH_EXPIRATION_TIME")));
                }
                else
                    RefreshTokenExpirationTime = TimeSpan.FromMinutes(config.GetValue<int>("RefreshTokenExpirationTime"));
            }
            catch
            {
                RefreshTokenExpirationTime = TimeSpan.FromMinutes(config.GetValue<int>("RefreshTokenExpirationTime"));
            }
            Console.WriteLine($"RefreshTokenExpirationTime: {RefreshTokenExpirationTime}");

            ValidateIssuerSigningKey = true;
            ValidateLifetime = true;
            var min = config.GetValue<int>("ClockSkew");
            if (min == 0)
                ClockSkew = TimeSpan.Zero;
            else
                ClockSkew = TimeSpan.FromMinutes(min);
        }

        public static string Key { get; set; }

        public static SecurityKey IssuerSigningKey
        {
            get
            {

                //var nk = SHA.GenerateSHA256String(Key);

                return new SymmetricSecurityKey(Encoding.ASCII.GetBytes(Key));
            }
        }

        public static SigningCredentials SigningCredentials
        {

            get => new SigningCredentials(IssuerSigningKey, SecurityAlgorithms.HmacSha512Signature, SecurityAlgorithms.Sha512Digest);
        }

        public static TimeSpan TokenExpirationTime { get; set; }
        public static TimeSpan RefreshTokenExpirationTime { get; set; }

        public static TimeSpan ClockSkew { get; set; } = TimeSpan.Zero;

        public static string Issuer { get; set; }

        public static string Audience { get; set; } = "all";

        public static bool ValidateIssuerSigningKey { get; set; } = true;

        public static bool ValidateLifetime { get; set; } = true;


    }
}
