using Livraria.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Livraria.Infra.Data
{
    public partial class LivrariaDbContext
    {
        public DbSet<Livro> Livros { get; set; }
    }
}