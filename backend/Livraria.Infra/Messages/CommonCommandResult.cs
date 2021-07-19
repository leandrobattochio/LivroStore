using System;
using FluentValidation.Results;

namespace Livraria.Infra.Messages
{
    /// <summary>
    /// Classe de retorno padrão dos Commands do CQRS
    /// </summary>
    public class CommonCommandResult
    {
        public CommonCommandResult(ValidationResult validationResult, Guid entityId)
        {
            ValidationResult = validationResult;
            EntityId = entityId;
        }

        public ValidationResult ValidationResult { get; set; }

        public CommonCommandResult(ValidationResult validationResult)
        {
            ValidationResult = validationResult;
        }

        public Guid EntityId { get; set; }

    }
}