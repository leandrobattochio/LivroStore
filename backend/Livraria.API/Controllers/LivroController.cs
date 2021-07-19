using System.Threading.Tasks;
using Livraria.API.Application.Commands;
using Livraria.API.Application.Queries;
using Livraria.Infra.Messages;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Livraria.API.Controllers
{
    
    /// <summary>
    /// Controller dos livros.
    /// </summary>
    [Route("api/v1/livro")]
    public class LivroController : MainController
    {
        private readonly IMediatorHandler _mediator;

        public LivroController(IMediatorHandler mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Cria um livro
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpPost]
        [Authorize(Roles = "Administrador")]
        public async Task<IActionResult> Create(AdicionarLivroCommand command)
        {
            // Validação do comando
            if (!command.IsValid()) return CustomResponse(command.GetValidationResult());

            // Envia e retorna
            return CustomResponse(await _mediator.EnviarComando(command));
        }

        /// <summary>
        /// Atualiza o livro
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpPut("{LivroId}")]
        [Authorize(Roles = "Administrador")]
        public async Task<IActionResult> Update(AtualizarLivroCommand command)
        {
            // Validação do comando
            if (!command.IsValid()) return CustomResponse(command.GetValidationResult());

            // Envia o comando e retorna
            return CustomResponse(await _mediator.EnviarComando(command));
        }

        /// <summary>
        /// Exclui o livro
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpDelete("{LivroId}")]
        [Authorize(Roles = "Administrador")]
        public async Task<IActionResult> Excluir(ExcluirLivroCommand command)
        {
            // Validação do Comando
            if (!command.IsValid()) return CustomResponse(command.GetValidationResult());

            // Envia o comando e retorna
            return CustomResponse(await _mediator.EnviarComando(command));
        }


        /// <summary>
        /// Obter todos os livros cadastrados.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Authorize(Roles = "Administrador")]
        public async Task<IActionResult> ObterTodos(ObterLivrosQuery query)
        {
            // Validação da query
            if (!query.IsValid()) return CustomResponse(query);

            // Envia a query
            var resultado = await _mediator.EnviarQuery(query);

            // Verifica o resultado para dar o retorno adequado
            if (resultado.ValidationResult.IsValid)
            {
                if (resultado.Data.Itens.Count > 0)
                    return CustomResponse(resultado.Data);
                else
                    return NoContent();
            }
            else
            {
                return CustomResponse(resultado.ValidationResult);
            }
        }

        /// <summary>
        /// Obtem um livro por ID.
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>

        [HttpGet("{LivroId}")]
        public async Task<IActionResult> ObterLivroPorId(ObterLivroQuery query)
        {
            // Validação da query
            if (!query.IsValid()) return CustomResponse(query.GetValidationResult());

            // Envia a query
            var resultado = await _mediator.EnviarQuery(query);

            // Verifica o resultado para dar o retorno adequado
            if (resultado.ValidationResult.IsValid)
            {
                return CustomResponse(resultado.Data);
            }
            else
            {
                return CustomResponse(resultado.ValidationResult);
            }
        }
    }
}