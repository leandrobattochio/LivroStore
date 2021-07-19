using Livraria.Infra;
using Livraria.Infra.Data;
using Livraria.Infra.Messages;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;

namespace Livraria.API.Application.Queries
{
    public partial class AuthQueryHandler : CommandHandler
    {
        private readonly LivrariaDbContext _context;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly JwtSettings _jwtSettings;

        public AuthQueryHandler(LivrariaDbContext context, SignInManager<IdentityUser> signInManager, UserManager<IdentityUser> userManager, 
            IOptionsMonitor<JwtSettings> jwtSettings)
        {
            _context = context;
            _signInManager = signInManager;
            _userManager = userManager;
            _jwtSettings = jwtSettings.CurrentValue;
        }
    }
}
