using System;
using System.Text.Json.Serialization;
using FluentValidation.Results;
using MediatR;

namespace Livraria.Infra.Messages
{
    public abstract class Query<T> : IRequest<T>
    {
        [JsonIgnore]
        protected ValidationResult ValidationResult { get; set; }

        public ValidationResult GetValidationResult()
        {
            return ValidationResult;
        }

        protected Query()
        {

        }

        public virtual bool IsValid()
        {
            throw new NotImplementedException();
        }
    }
}