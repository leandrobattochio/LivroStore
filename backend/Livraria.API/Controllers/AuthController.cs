using System.Threading.Tasks;
using Livraria.API.Application.Commands;
using Livraria.Infra.Messages;
using Microsoft.AspNetCore.Mvc;

namespace Livraria.API.Controllers
{
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

        // /// <summary>
        // /// Faz o login do usuario
        // /// </summary>
        // /// <returns></returns>
        // [HttpPost("login")]
        // public async Task<IActionResult> Login()
        // {

        // }
    }
}