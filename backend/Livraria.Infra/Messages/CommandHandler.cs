using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation.Results;
using Livraria.Infra.Data;

namespace Livraria.Infra.Messages
{
    /// <summary>
    /// Classe base do CommandHandler que contém métodos para adicionar erros
    /// e persistencia de dados no contexto do banco de dados.
    /// </summary>
    public abstract class CommandHandler
    {
        protected ValidationResult ValidationResult;

        protected CommandHandler()
        {
            ValidationResult = new ValidationResult();
        }

        protected void AdicionarErro(string mensagem)
        {
            ValidationResult.Errors.Add(new ValidationFailure(string.Empty, mensagem));
        }

        protected void AdicionarErro(ValidationFailure validationFailure)
        {
            ValidationResult.Errors.Add(validationFailure);
        }

        protected void AdicionarErros(IEnumerable<ValidationFailure> validationFailures)
        {
            ValidationResult.Errors.AddRange(validationFailures);
        }

        protected async Task<ValidationResult> PersistirDados(IUnitOfWork uow)
        {
            var errors = await uow.Commit();
            if (errors.Count() > 0)
                foreach (var error in errors)
                    AdicionarErro(error);

            return ValidationResult;
        }
    }
}