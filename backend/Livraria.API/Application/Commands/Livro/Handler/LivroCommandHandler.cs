using Livraria.Infra.Data;
using Livraria.Infra.Messages;

namespace Livraria.API.Application.Commands.Handler
{
    /// <summary>
    /// Construtor do Handler dos comandos "Livro".
    /// Adicionar as dependências aqui.
    /// </summary>
    public partial class LivroCommandHandler : CommandHandler
    {
        private readonly LivrariaDbContext _context;

        public LivroCommandHandler(LivrariaDbContext context)
        {
            _context = context;
        }
    }
}