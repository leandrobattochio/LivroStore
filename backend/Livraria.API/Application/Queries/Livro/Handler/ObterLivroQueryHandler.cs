using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Livraria.Infra.Messages;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Livraria.API.Application.Queries.Handler
{
    public partial class LivroQueryHandler :
           IRequestHandler<ObterLivroQuery, QueryResponseMessage<ObterLivroQueryResult>>
    {
        public async Task<QueryResponseMessage<ObterLivroQueryResult>> Handle(ObterLivroQuery request, CancellationToken cancellationToken)
        {
            if (!request.IsValid()) return new QueryResponseMessage<ObterLivroQueryResult>(request.GetValidationResult());

            var livro = await _context.Livros
                .Where(c => c.Id.Equals(request.LivroId))
                .AsNoTracking()
                .SingleOrDefaultAsync();

            if (livro == null)
            {
                AdicionarErro("Livro não encontrado!");
                return new QueryResponseMessage<ObterLivroQueryResult>(ValidationResult);
            }

            var retorno = new ObterLivroQueryResult(livro.ImagemCapa, livro.Titulo, livro.ISBN, livro.Editora, livro.Autor,
                livro.Sinopse, livro.DataPublicacao);

            return new QueryResponseMessage<ObterLivroQueryResult>(ValidationResult, retorno);
        }
    }
}