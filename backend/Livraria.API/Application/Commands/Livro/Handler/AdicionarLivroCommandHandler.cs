using System.Threading;
using System.Threading.Tasks;
using FluentValidation.Results;
using MediatR;
using Livraria.Domain.Models;
using Livraria.Infra.Messages;

namespace Livraria.API.Application.Commands.Handler
{
    public partial class LivroCommandHandler :
        IRequestHandler<AdicionarLivroCommand, CommonCommandResult>
    {
        /// <summary>
        /// Comando responsável por adicionar um livro no banco de dados.
        /// </summary>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<CommonCommandResult> Handle(AdicionarLivroCommand request, CancellationToken cancellationToken)
        {
            if (!request.IsValid()) return new CommonCommandResult(request.GetValidationResult());

            var livro = new Livro(request.Body.ImagemCapa, request.Body.Titulo, request.Body.ISBN, request.Body.Editora,
                request.Body.Autor, request.Body.Sinopse, request.Body.DataPublicacao);

            await _context.Livros.AddAsync(livro);

            ValidationResult = await PersistirDados(_context);
            var retorno = new CommonCommandResult(ValidationResult, livro.Id);
            return retorno;
        }
    }
}