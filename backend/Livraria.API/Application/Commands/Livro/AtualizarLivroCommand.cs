using System.Security.Cryptography;
using System;
using FluentValidation;
using Livraria.Infra.Messages;
using Microsoft.AspNetCore.Mvc;

namespace Livraria.API.Application.Commands
{
    public class AtualizarLivroCommand : Command
    {

        [FromRoute]
        public Guid LivroId { get; set; }

        [FromBody]
        public AtualizarLivroBody Body { get; set; }

        public AtualizarLivroCommand(AtualizarLivroBody body)
        {
            Body = body;
        }

        public AtualizarLivroCommand()
        {

        }

        public class AtualizarLivroBody
        {
            public AtualizarLivroBody(string imagemCapa, string titulo, string iSBN, string editora, string autor, string sinopse, DateTime dataPublicacao)
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
            ValidationResult = new AtualizarLivroCommandValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }

    public class AtualizarLivroCommandValidation : AbstractValidator<AtualizarLivroCommand>
    {
        public AtualizarLivroCommandValidation()
        {
            RuleFor(c => c.LivroId)
                .NotEmpty()
                .NotNull();

            RuleFor(c => c.Body.ImagemCapa)
                .NotEmpty()
                    .WithMessage("ImagemCapa n??o pode estar em branco!")
                .MaximumLength(200)
                    .WithMessage("ImagemCapa m??ximo de 200 caracteres!");

            RuleFor(c => c.Body.Titulo)
                .NotEmpty()
                    .WithMessage("Titulo n??o pode estar em branco!")
                .MaximumLength(100)
                    .WithMessage("Titulo m??ximo de 100 caracteres!");

            RuleFor(c => c.Body.ISBN)
                .NotEmpty()
                    .WithMessage("ISBN n??o pode estar em branco!")
                .MaximumLength(100)
                    .WithMessage("ISBN m??ximo de 100 caracteres!");

            RuleFor(c => c.Body.Editora)
                .NotEmpty()
                    .WithMessage("Editora n??o pode estar em branco!")
                .MaximumLength(100)
                    .WithMessage("Editora m??ximo de 100 caracteres!");

            RuleFor(c => c.Body.Autor)
                .NotEmpty()
                    .WithMessage("Autor n??o pode estar em branco!")
                .MaximumLength(200)
                    .WithMessage("Autor m??ximo de 200 caracteres!");

            RuleFor(c => c.Body.Sinopse)
                .NotEmpty()
                    .WithMessage("Sinopse n??o pode estar em branco!")
                .MinimumLength(50)
                    .WithMessage("A sinopse deve conter no m??nimo 50 caracteres.")
                .MaximumLength(500)
                    .WithMessage("A sinopse deve conter no m??ximo 500 caracteres.");

            RuleFor(c => c.Body.DataPublicacao)
                .NotEmpty()
                    .WithMessage("DataPublicacao n??o pode estar em branco!");
        }
    }
}