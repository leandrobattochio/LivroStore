using System;
using FluentValidation;
using Livraria.Infra.Messages;
using Microsoft.AspNetCore.Mvc;

namespace Livraria.API.Application.Queries
{
    /// <summary>
    /// Representa uma Query para obter um livro a partir do ID.
    /// </summary>
    public class ObterLivroQuery : Query<QueryResponseMessage<ObterLivroQueryResult>>
    {
        [FromRoute]
        public Guid LivroId { get; set; }

        public ObterLivroQuery(Guid livroId)
        {
            LivroId = livroId;
        }

        public ObterLivroQuery()
        {

        }

        public override bool IsValid()
        {
            ValidationResult = new ObterLivroQueryValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }

    public class ObterLivroQueryValidation : AbstractValidator<ObterLivroQuery>
    {
        public ObterLivroQueryValidation()
        {
            RuleFor(c => c.LivroId)
                .NotNull()
                .NotEmpty();
        }
    }

    public class ObterLivroQueryResult
    {
        public ObterLivroQueryResult(string imagemCapa, string titulo, string iSBN, string editora, string autor, string sinopse, DateTime dataPublicacao)
        {
            ImagemCapa = imagemCapa;
            Titulo = titulo;
            ISBN = iSBN;
            Editora = editora;
            Autor = autor;
            Sinopse = sinopse;
            DataPublicacao = dataPublicacao;
        }

        public string ImagemCapa { get; private set; }
        public string Titulo { get; private set; }
        public string ISBN { get; private set; }
        public string Editora { get; private set; }
        public string Autor { get; private set; }
        public string Sinopse { get; private set; }
        public DateTime DataPublicacao { get; private set; }
    }
}