using Livraria.Infra.Data;
using Livraria.Infra.Messages;
using Microsoft.AspNetCore.Identity;

namespace Livraria.API.Application.Commands
{
    public partial class AuthCommandHandler : CommandHandler
    {
        private readonly LivrariaDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        public AuthCommandHandler(LivrariaDbContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }
    }
}