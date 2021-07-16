using System.Threading;
using System.Threading.Tasks;
using FluentValidation.Results;
using MediatR;
using Livraria.Domain.Models;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Livraria.Infra.Messages;

namespace Livraria.API.Application.Commands.Handler
{
    public partial class LivroCommandHandler :
        IRequestHandler<ExcluirLivroCommand, CommonCommandResult>
    {
        /// <summary>
        /// Comando responsável por excluir um livro no banco de dados.
        /// </summary>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<CommonCommandResult> Handle(ExcluirLivroCommand request, CancellationToken cancellationToken)
        {
            if (!request.IsValid()) return new CommonCommandResult(request.GetValidationResult());

            var entity = await _context.Livros
                .Where(c => c.Id.Equals(request.LivroId))
                .SingleOrDefaultAsync();

            if (entity != null)
            {
                _context.Entry(entity).State = EntityState.Deleted;

                ValidationResult = await PersistirDados(_context);
                return new CommonCommandResult(ValidationResult);
            }
            else
            {
                AdicionarErro("Livro inválido ou não existente!");
                return new CommonCommandResult(ValidationResult);
            }
        }
    }
}