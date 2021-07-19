using Livraria.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Livraria.Infra.Data
{
    /// <summary>
    /// Classe parcial para guardar os DbSets do contexto.
    /// </summary>
    public partial class LivrariaDbContext
    {
        public DbSet<Livro> Livros { get; set; }
    }
}