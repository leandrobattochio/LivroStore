using System.Threading.Tasks;
using Livraria.API.Application.Commands;
using Livraria.API.Application.Queries;
using Livraria.Infra.Messages;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Livraria.API.Controllers
{
    /// <summary>
    /// Controlador das rotas de login e registro de contas
    /// </summary>
    [Route("api/v1/auth")]
    public class AuthController : MainController
    {
        private readonly IMediatorHandler _mediator;

        public AuthController(IMediatorHandler mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Registra um usuario no sistema
        /// </summary>
        /// <returns></returns>
        [HttpPost("registrar")]
        public async Task<IActionResult> Registrar(RegistrarUsuarioCommand command)
        {
            if (!command.IsValid()) return CustomResponse(command.GetValidationResult());

            return CustomResponse(await _mediator.EnviarComando(command));
        }

        /// <summary>
        /// Faz o login do usuario.
        /// Tratei o login como uma QUERY no CQRS, pois em teoria um login n�o modifica o banco de dados. Por�m, n�o se passa
        /// senhas e informa��es sensiveis do usuario atraves de um m�todo GET, portanto foi usado POST mas mantendo toda a estrutura
        /// interna da l�gica como QUERY.
        /// </summary>
        /// <returns></returns>
        [HttpPost("login")]
        public async Task<IActionResult> LogarUsuario(LogarUsuarioQuery query)
        {
            if (!query.IsValid()) return CustomResponse(query.GetValidationResult());

            return CustomResponse(await _mediator.EnviarQuery(query));
        }

        /// <summary>
        /// Endpoint apenas para verificar se o usuario esta logado no sistema.
        /// </summary>
        /// <returns></returns>
        [Authorize]
        [HttpGet("loggedin")]
        public async Task<IActionResult> IsLoggedIn() { await Task.Yield(); return Ok(); }

        /// <summary>
        /// Endpoint apenas para verificar se o usuario possui a role de Administrador
        /// </summary>
        /// <returns></returns>
        [Authorize(Roles = "Administrador")]
        [HttpGet("adminrole")]
        public async Task<IActionResult> IsAdminRole() { await Task.Yield(); return Ok(); }
    }
}