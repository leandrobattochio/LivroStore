using System;
using Livraria.Domain.Core;

namespace Livraria.Domain.Models
{
    /// <summary>
    /// Entidade dos livros.
    /// </summary>
    public class Livro : BaseEntity, IAggregatRoot
    {
        public Livro(string imagemCapa, string titulo, string iSBN, string editora, string autor, string sinopse, DateTime dataPublicacao)
        {
            ImagemCapa = imagemCapa;
            Titulo = titulo;
            ISBN = iSBN;
            Editora = editora;
            Autor = autor;
            Sinopse = sinopse;
            DataPublicacao = dataPublicacao;
        }

        public Livro(Guid id, string imagemCapa, string titulo, string iSBN, string editora, string autor, string sinopse,
            DateTime dataPublicacao) : this(imagemCapa, titulo, iSBN, editora, autor, sinopse, dataPublicacao)
        {
            Id = id;
        }

        protected Livro()
        {

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