using System.Threading;
using System.Threading.Tasks;
using Livraria.Infra.Messages;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Livraria.API.Application.Commands
{
    /// <summary>
    /// 
    /// </summary>
    public partial class AuthCommandHandler : IRequestHandler<RegistrarUsuarioCommand, CommonCommandResult>
    {
        public async Task<CommonCommandResult> Handle(RegistrarUsuarioCommand request, CancellationToken cancellationToken)
        {
            if (!request.IsValid()) return new CommonCommandResult(request.GetValidationResult());

            // Registrar o usuario
            var usuario = new IdentityUser(request.Body.Usuario);
            usuario.Email = request.Body.Email;

            var resultado = await _userManager.CreateAsync(usuario, request.Body.Senha);

            // Adiciona erros se houver algum
            if (resultado.Succeeded == false)
                foreach (var p in resultado.Errors)
                    AdicionarErro(p.Description);

            // Adiciona o usuario na Role
            var user = await _userManager.FindByNameAsync(request.Body.Usuario);

            if (request.Body.TipoUsuario == enTipoUsuario.Administrador)
            {
                await _userManager.AddToRoleAsync(user, "Administrador");
            }
            else
            {
                await _userManager.AddToRoleAsync(user, "Normal");
            }

            // Retorna OK
            return new CommonCommandResult(ValidationResult);
        }
    }
}