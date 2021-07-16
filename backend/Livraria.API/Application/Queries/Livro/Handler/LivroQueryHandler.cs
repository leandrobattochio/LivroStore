using Livraria.Infra.Data;
using Livraria.Infra.Messages;

namespace Livraria.API.Application.Queries.Handler
{
    public partial class LivroQueryHandler : CommandHandler
    {
        private readonly LivrariaDbContext _context;

        public LivroQueryHandler(LivrariaDbContext context)
        {
            _context = context;
        }
    }
}