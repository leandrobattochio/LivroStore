using System;
using System.Text.Json.Serialization;
using FluentValidation.Results;
using MediatR;

namespace Livraria.Infra.Messages
{
    /// <summary>
    /// Classe base de uma query do CQRS
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class Query<T> : IRequest<T>
    {
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