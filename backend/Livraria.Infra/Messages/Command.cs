using System;
using System.Text.Json.Serialization;
using FluentValidation.Results;
using MediatR;

namespace Livraria.Infra.Messages
{
    [AttributeUsage(AttributeTargets.Property)]
    public class SwaggerExcludeAttribute : Attribute
    {
    }

    /// <summary>
    /// Classe base do comando do CQRS
    /// </summary>
    public abstract class Command : IRequest<CommonCommandResult>
    {
        /// <summary>
        /// Guarda o resultado da execução do Comando
        /// </summary>
        /// <value></value>
        [SwaggerExclude]
        protected ValidationResult ValidationResult { get; set; }


        public ValidationResult GetValidationResult()
        {
            return ValidationResult;
        }


        protected Command()
        {

        }

        public virtual bool IsValid()
        {
            throw new NotImplementedException();
        }
    }

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