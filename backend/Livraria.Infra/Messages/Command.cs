using System;
using System.Text.Json.Serialization;
using FluentValidation.Results;
using MediatR;

namespace Livraria.Infra.Messages
{
    /// <summary>
    /// Classe base do comando do CQRS
    /// </summary>
    public abstract class Command : IRequest<CommonCommandResult>
    {
        /// <summary>
        /// Guarda o resultado da execução do Comando. Protected para o swagger nao serializar.
        /// </summary>
        /// <value></value>
        protected ValidationResult ValidationResult { get; set; }


        /// <summary>
        /// Como a propriedade ValidationResult é protected, precisa de um método para acessa-la.
        /// </summary>
        /// <returns></returns>
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
}