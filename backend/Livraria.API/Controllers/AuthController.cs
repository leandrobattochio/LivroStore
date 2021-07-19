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
        /// Tratei o login como uma QUERY no CQRS, pois em teoria um login não modifica o banco de dados. Porém, não se passa
        /// senhas e informações sensiveis do usuario atraves de um método GET, portanto foi usado POST mas mantendo toda a estrutura
        /// interna da lógica como QUERY.
        /// </summary>
        /// <returns></returns>
        [HttpPost("login")]
        public async Task<IActionResult> LogarUsuario(LogarUsuarioQuery query)
        {
            if (!query.IsValid()) return CustomResponse(query.GetValidationResult());

            return CustomResponse(await _mediator.EnviarQuery(query));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [Authorize]
        [HttpGet("loggedin")]
        public async Task<IActionResult> IsLoggedIn()
        {
            await Task.Yield();
            return Ok();
        }
    }
}