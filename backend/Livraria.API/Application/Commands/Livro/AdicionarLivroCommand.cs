using System.Security.Cryptography;
using System;
using FluentValidation;
using Livraria.Infra.Messages;
using Microsoft.AspNetCore.Mvc;

namespace Livraria.API.Application.Commands
{
    /// <summary>
    /// Command para criação de um livro
    /// </summary>
    public class AdicionarLivroCommand : Command
    {
        [FromBody]
        public AdicionarLivroCommandBody Body { get; set; }

        public AdicionarLivroCommand(AdicionarLivroCommandBody body)
        {
            Body = body;
        }

        public AdicionarLivroCommand()
        {

        }

        public class AdicionarLivroCommandBody
        {
            public AdicionarLivroCommandBody(string imagemCapa, string titulo, string iSBN, string editora, string autor, string sinopse, DateTime dataPublicacao)
            {
                ImagemCapa = imagemCapa;
                Titulo = titulo;
                ISBN = iSBN;
                Editora = editora;
                Autor = autor;
                Sinopse = sinopse;
                DataPublicacao = dataPublicacao;
            }

            public string ImagemCapa { get; set; }
            public string Titulo { get; set; }
            public string ISBN { get; set; }
            public string Editora { get; set; }
            public string Autor { get; set; }
            public string Sinopse { get; set; }
            public DateTime DataPublicacao { get; set; }
        }


        /// <summary>
        /// Valida o comando. Adiciona os erros no ValidationResult em caso de falha.
        /// </summary>
        /// <returns></returns>
        public override bool IsValid()
        {
            ValidationResult = new AdicionarLivroCommandValidation().Validate(this);
            return ValidationResult.IsValid;
        }

    }


    /// <summary>
    /// Classe para validar o comand usando FluentValidation
    /// </summary>
    public class AdicionarLivroCommandValidation : AbstractValidator<AdicionarLivroCommand>
    {
        public AdicionarLivroCommandValidation()
        {
            RuleFor(c => c.Body.ImagemCapa)
                .NotEmpty()
                    .WithMessage("ImagemCapa não pode estar em branco!")
                .MaximumLength(200)
                    .WithMessage("ImagemCapa máximo de 200 caracteres!");

            RuleFor(c => c.Body.Titulo)
                .NotEmpty()
                    .WithMessage("Titulo não pode estar em branco!")
                .MaximumLength(100)
                    .WithMessage("Titulo máximo de 100 caracteres!");

            RuleFor(c => c.Body.ISBN)
                .NotEmpty()
                    .WithMessage("ISBN não pode estar em branco!")
                .MaximumLength(100)
                    .WithMessage("ISBN máximo de 100 caracteres!");

            RuleFor(c => c.Body.Editora)
                .NotEmpty()
                    .WithMessage("Editora não pode estar em branco!")
                .MaximumLength(100)
                    .WithMessage("Editora máximo de 100 caracteres!");

            RuleFor(c => c.Body.Autor)
                .NotEmpty()
                    .WithMessage("Autor não pode estar em branco!")
                .MaximumLength(200)
                    .WithMessage("Autor máximo de 200 caracteres!");

            RuleFor(c => c.Body.Sinopse)
                .NotEmpty()
                    .WithMessage("Sinopse não pode estar em branco!")
                .MinimumLength(50)
                    .WithMessage("A sinopse deve conter no mínimo 50 caracteres.")
                .MaximumLength(500)
                    .WithMessage("A sinopse deve conter no máximo 500 caracteres.");

            RuleFor(c => c.Body.DataPublicacao)
                .NotEmpty()
                    .WithMessage("DataPublicacao não pode estar em branco!");
        }
    }
}