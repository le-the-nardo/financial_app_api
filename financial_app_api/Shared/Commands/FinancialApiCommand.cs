using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using financial_app_api.Shared.Models;

namespace financial_app_api.Shared.Commands
{
    public abstract class FinancialApiCommand<T, R>
    {
        protected T parameters;

        public FinancialApiCommand(T parameters)
        {
            this.parameters = parameters;
        }

        public abstract CommandResult<R> Execute();


        protected CommandResult<R> MakeResult(R data)
        {
            return new CommandResult<R> { result = data };
        }
    }
}