using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Livraria.Infra.Messages;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Livraria.API.Application.Queries.Handler
{
    public partial class LivroQueryHandler :
        IRequestHandler<ObterLivrosQuery, QueryResponseMessage<ObterLivrosQueryRetorno>>
    {
        public async Task<QueryResponseMessage<ObterLivrosQueryRetorno>> Handle(ObterLivrosQuery request, CancellationToken cancellationToken)
        {
            if (!request.IsValid())
                return new QueryResponseMessage<ObterLivrosQueryRetorno>(request.GetValidationResult());

            var livrosQuery = _context.Livros
                .AsNoTracking()
                .AsQueryable();


            // Aplicar filtros.
            // Tenta filtar pelo nome.
            if (string.IsNullOrEmpty(request.PalavraChave) == false)
            {
                livrosQuery = livrosQuery
                    .Where(c => EF.Functions.Like(c.Titulo, $"%{request.PalavraChave}%"))
                    .AsQueryable();
            }

            // Tenta filtrar por data
            if (request.DataInicio != default(DateTime) && request.DataFim != default(DateTime))
            {
                livrosQuery = livrosQuery
                    .Where(c => c.DataPublicacao >= request.DataInicio && c.DataPublicacao <= request.DataFim)
                    .AsQueryable();
            }

            // Filtra por A-Z ou Z-A
            if (request.Order != null)
            {
                if (request.Order.Equals("asc", StringComparison.CurrentCultureIgnoreCase))
                {
                    livrosQuery = livrosQuery
                        .OrderBy(c => c.Titulo)
                        .AsQueryable();
                }
                else
                {
                    livrosQuery = livrosQuery
                        .OrderByDescending(c => c.Titulo)
                        .AsQueryable();
                }
            }

            // Executa a Query
            var livrosMapped = await livrosQuery
                .Select(c => new ObterLivrosQueryRetorno.Item(c.Id, c.ImagemCapa, c.Titulo, c.ISBN, c.Editora, c.Autor, c.Sinopse,
                    c.DataPublicacao))
                .ToListAsync();

            var retorno = new ObterLivrosQueryRetorno(livrosMapped);

            return new QueryResponseMessage<ObterLivrosQueryRetorno>(ValidationResult, retorno);
        }
    }
}