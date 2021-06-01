using financial_app_api.Services.Security.models;
using financial_app_api.Services.Security.repository;
using financial_app_api.Shared.Commands;
using financial_app_api.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace financial_app_api.Services.Security.commands
{
    public class CreateUserCommand : FinancialApiCommand<CreateUser, DataResponse>
    {
        public CreateUserCommand(CreateUser parameters) : base(parameters)
        {
        }

        public override CommandResult<DataResponse> Execute()
        {
            DataResponse result = new DataResponse();
            using var repo = new CreateUserRepository();

            var hashPassword = PasswordUtil.CalcHash(parameters.password);
            parameters.password = hashPassword;

            parameters.cpf = parameters.cpf.Replace("-", "").Replace(".", "");

            repo.CreateUser(parameters);

            result.payload_data = "";
            result.error = false;
            result.info = "Usuário cadastrado com sucesso!";

            return MakeResult(result);
        }
    }
}