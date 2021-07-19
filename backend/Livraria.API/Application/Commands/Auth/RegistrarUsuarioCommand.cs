using FluentValidation;
using Livraria.Infra.Messages;
using Microsoft.AspNetCore.Mvc;

namespace Livraria.API.Application.Commands
{
    /// <summary>
    /// Command para registrar um usuario
    /// </summary>
    public class RegistrarUsuarioCommand : Command
    {

        [FromBody]
        public RegistrarUsuarioCommandBody Body { get; set; }

        public RegistrarUsuarioCommand(RegistrarUsuarioCommandBody body)
        {
            Body = body;
        }

        public RegistrarUsuarioCommand()
        {

        }

        public override bool IsValid()
        {
            ValidationResult = new RegistrarUsuarioCommandValidation().Validate(this);
            return ValidationResult.IsValid;
        }

        public class RegistrarUsuarioCommandBody
        {
            public RegistrarUsuarioCommandBody(string usuario, string senha, string email, enTipoUsuario tipoUsuario)
            {
                Usuario = usuario;
                Senha = senha;
                Email = email;
                TipoUsuario = tipoUsuario;
            }

            public string Usuario { get; set; }
            public string Senha { get; set; }
            public string Email { get; set; }
            public enTipoUsuario TipoUsuario { get; set; }
        }
    }

    public enum enTipoUsuario
    {
        Normal = 1,
        Administrador = 2
    }

    /// <summary>
    /// Validação para o command de registrar usuario
    /// </summary>
    public class RegistrarUsuarioCommandValidation : AbstractValidator<RegistrarUsuarioCommand>
    {
        public RegistrarUsuarioCommandValidation()
        {
            RuleFor(c => c.Body.Usuario)
                .NotEmpty()
                    .WithMessage("Nome de usuario em branco!")
                .MinimumLength(4)
                    .WithMessage("Nome de usuario com no mínimo 4 caracteres!")
                .MaximumLength(12)
                    .WithMessage("Nome de usuário com no máximo 12 caracteres!");

            RuleFor(c => c.Body.Senha)
                .NotEmpty()
                    .WithMessage("Senha em branco!")
                .MinimumLength(6)
                    .WithMessage("Senha com no mínimo 6 caracteres!");

            RuleFor(c => c.Body.Email)
                .EmailAddress()
                    .WithMessage("E-mail inválido!");
        }
    }
}