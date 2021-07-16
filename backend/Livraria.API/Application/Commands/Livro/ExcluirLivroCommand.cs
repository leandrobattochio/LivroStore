using System;
using FluentValidation;
using Livraria.Infra.Messages;
using Microsoft.AspNetCore.Mvc;

namespace Livraria.API.Application.Commands
{
    public class ExcluirLivroCommand : Command
    {

        [FromRoute]
        public Guid LivroId { get; set; }

        public ExcluirLivroCommand(Guid livroId)
        {
            LivroId = livroId;
        }

        public ExcluirLivroCommand()
        {

        }



        public override bool IsValid()
        {
            ValidationResult = new ExcluirLivroCommandValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }

    public class ExcluirLivroCommandValidation : AbstractValidator<ExcluirLivroCommand>
    {
        public ExcluirLivroCommandValidation()
        {
            RuleFor(c => c.LivroId)
                .NotEmpty()
                .NotNull();
        }
    }
}