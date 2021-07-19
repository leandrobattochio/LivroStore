using System;
using System.Collections.Generic;
using FluentValidation;
using Livraria.Infra.Messages;
using Microsoft.AspNetCore.Mvc;

namespace Livraria.API.Application.Queries
{
    /// <summary>
    /// Representa uma query para obter livros com base em filtros determinados
    /// </summary>
    public class ObterLivrosQuery : Query<QueryResponseMessage<ObterLivrosQueryRetorno>>
    {
        [FromQuery]
        public string PalavraChave { get; set; }

        [FromQuery]
        public DateTime DataInicio { get; set; }

        [FromQuery]
        public DateTime DataFim { get; set; }

        [FromQuery]
        public string Order { get; set; }

        public ObterLivrosQuery()
        {

        }

        public override bool IsValid()
        {
            ValidationResult = new ObterLivrosQueryValidation().Validate(this);
            return ValidationResult.IsValid;
        }

    }

    public class ObterLivrosQueryRetorno
    {
        public ObterLivrosQueryRetorno(ICollection<Item> itens)
        {
            Itens = itens;
        }

        public ICollection<Item> Itens { get; private set; }

        public class Item
        {
            public Item(Guid id, string imagemCapa, string titulo, string iSBN, string editora, string autor, string sinopse, DateTime dataPublicacao)
            {
                Id = id;
                ImagemCapa = imagemCapa;
                Titulo = titulo;
                ISBN = iSBN;
                Editora = editora;
                Autor = autor;
                Sinopse = sinopse;
                DataPublicacao = dataPublicacao;
            }

            public Guid Id { get; private set; }
            public string ImagemCapa { get; private set; }
            public string Titulo { get; private set; }
            public string ISBN { get; private set; }
            public string Editora { get; private set; }
            public string Autor { get; private set; }
            public string Sinopse { get; private set; }
            public DateTime DataPublicacao { get; private set; }
        }
    }

    /// <summary>
    /// Validação. Em branco pois não é obrigatorio mandar nenhum filtro.
    /// </summary>
    public class ObterLivrosQueryValidation : AbstractValidator<ObterLivrosQuery>
    {
        public ObterLivrosQueryValidation()
        {

        }
    }
}