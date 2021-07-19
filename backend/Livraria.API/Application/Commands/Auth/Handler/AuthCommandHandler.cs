using Livraria.Infra.Data;
using Livraria.Infra.Messages;
using Microsoft.AspNetCore.Identity;

namespace Livraria.API.Application.Commands
{
    /// <summary>
    /// Contrutor do command handler Auth, centralizando as injeções de dependência.
    /// </summary>
    public partial class AuthCommandHandler : CommandHandler
    {
        private readonly LivrariaDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public AuthCommandHandler(LivrariaDbContext context, UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _context = context;
            _userManager = userManager;
            _roleManager = roleManager;
        }
    }
}