using Livraria.Infra.Messages;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Livraria.API.Application.Queries
{
    public partial class AuthQueryHandler :
        IRequestHandler<LogarUsuarioQuery, QueryResponseMessage<LogarUsuarioQueryResult>>
    {
        public async Task<QueryResponseMessage<LogarUsuarioQueryResult>> Handle(LogarUsuarioQuery request, CancellationToken cancellationToken)
        {
            if (!request.IsValid()) return new QueryResponseMessage<LogarUsuarioQueryResult>(request.GetValidationResult());

            // Tenta fazer o login com o usuario e a senha.
            var signInResult = await _signInManager.PasswordSignInAsync(request.Body.Usuario, request.Body.Senha, false, true);

            if (signInResult.Succeeded)
            {
                return new QueryResponseMessage<LogarUsuarioQueryResult>(ValidationResult, await GerarTokenJWT(request.Body.Usuario));
            }

            // Da uma mensagem de erro generico.
            // A pessoa não pode saber se ela acertou o usuario mas errou a senha, por exemplo.
            AdicionarErro("Nome de usuario ou senha invalidos!");

            return new QueryResponseMessage<LogarUsuarioQueryResult>(ValidationResult);
        }


        private async Task<LogarUsuarioQueryResult> GerarTokenJWT(string userName)
        {
            var user = await _userManager.FindByNameAsync(userName);
            var claims = await _userManager.GetClaimsAsync(user);

            var identityClaims = await ObterClaimsUsuario(claims, user);
            var encodedToken = CodificarToken(identityClaims);

            var result = new LogarUsuarioQueryResult(encodedToken);
            return result;
        }

        private string CodificarToken(ClaimsIdentity identityClaims)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_jwtSettings.Key);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = identityClaims,
                Expires = DateTime.UtcNow.AddHours(2),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        private async Task<ClaimsIdentity> ObterClaimsUsuario(ICollection<Claim> claims, IdentityUser user)
        {
            var userRoles = await _userManager.GetRolesAsync(user);

            claims.Add(new Claim(JwtRegisteredClaimNames.Sub, user.Id));
            claims.Add(new Claim(JwtRegisteredClaimNames.Email, user.Email));
            claims.Add(new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()));
            claims.Add(new Claim(JwtRegisteredClaimNames.Nbf, ToUnixEpochDate(DateTime.UtcNow).ToString()));
            claims.Add(new Claim(JwtRegisteredClaimNames.Iat, ToUnixEpochDate(DateTime.UtcNow).ToString(), ClaimValueTypes.Integer64));

            foreach (var userRole in userRoles)
            {
                claims.Add(new Claim("role", userRole));
            }

            var identityClaims = new ClaimsIdentity();
            identityClaims.AddClaims(claims);

            return identityClaims;
        }

        private static long ToUnixEpochDate(DateTime date)
            => (long)Math.Round((date.ToUniversalTime() - new DateTimeOffset(1970, 1, 1, 0, 0, 0, TimeSpan.Zero))
            .TotalSeconds);

        public enum enTipoUsuario
        {
            None = 0,
            Cliente = 1,
            Escritorio = 2
        }
    }
}
