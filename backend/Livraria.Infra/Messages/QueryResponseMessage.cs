using FluentValidation.Results;

namespace Livraria.Infra.Messages
{
    /// <summary>
    /// Resposta de uma query do CQRS
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class QueryResponseMessage<T>
        where T : class
    {
        public ValidationResult ValidationResult { get; private set; }

        public T Data { get; private set; }

        public QueryResponseMessage(ValidationResult validationResult, T data = null)
        {
            ValidationResult = validationResult;
            Data = data;
        }
    }
}