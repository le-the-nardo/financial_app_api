using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using financial_app_api.Security.Commands;
using financial_app_api.Security.models;
using financial_app_api.Services.Security.commands;
using financial_app_api.Services.Security.models;
using financial_app_api.Shared.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace financial_app_api.Security
{
    [Route("api/[controller]")]
    public class SecurityController : Controller
    {
        [HttpPost("[action]")]
        [AllowAnonymous]
        public AuthToken Login([FromBody] Credentials credentials)
        {
            try
            {
                var login = new LoginCommand(credentials);

                var cmdResult = login.Execute();

                return cmdResult.result;

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                AuthToken token = new AuthToken();

                token.message = ex.Message;

                return token;
            }
        }

        [HttpPost("[action]")]
        public DataResponse CreateUser([FromBody] CreateUser createUser)
        {
            try
            {
                DataResponse response = new DataResponse();
                
                var newUser = new CreateUserCommand(createUser);

                var cmdResult = newUser.Execute();

                return cmdResult.result;

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                DataResponse result = new DataResponse();

                result.payload_data = "";
                result.error = true;
                result.info = ex.Message;

                Response.StatusCode = 505;

                return result;
            }
        }
    }
}
