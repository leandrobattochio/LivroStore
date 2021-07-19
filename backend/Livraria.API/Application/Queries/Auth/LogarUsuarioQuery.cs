using FluentValidation;
using Livraria.Infra.Messages;
using Microsoft.AspNetCore.Mvc;

namespace Livraria.API.Application.Queries
{
    public class LogarUsuarioQuery : Query<QueryResponseMessage<LogarUsuarioQueryResult>>
    {
        [FromBody]
        public LogarUsuarioQueryBody Body { get; set; }

        public LogarUsuarioQuery(LogarUsuarioQueryBody body)
        {
            Body = body;
        }

        public LogarUsuarioQuery()
        {

        }

        public class LogarUsuarioQueryBody
        {
            public LogarUsuarioQueryBody(string usuario, string senha)
            {
                Usuario = usuario;
                Senha = senha;
            }

            public string Usuario { get; set; }
            public string Senha { get; set; }
        }

        public override bool IsValid()
        {
            ValidationResult = new LogarUsuarioQueryValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }


    public class LogarUsuarioQueryValidation : AbstractValidator<LogarUsuarioQuery>
    {
        public LogarUsuarioQueryValidation()
        {
            RuleFor(c => c.Body.Usuario)
                .NotEmpty()
                .NotNull()
                .MinimumLength(4)
                .MaximumLength(20);

            RuleFor(c => c.Body.Senha)
                .NotEmpty()
                .NotNull()
                .MinimumLength(4)
                .MaximumLength(20);
        }
    }


    public class LogarUsuarioQueryResult
    {
        public string Token { get; set; }

        public LogarUsuarioQueryResult(string token)
        {
            Token = token;
        }
    }
}
