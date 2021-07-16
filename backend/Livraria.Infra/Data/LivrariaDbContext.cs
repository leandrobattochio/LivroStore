using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FluentValidation.Results;
using Livraria.Domain.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Livraria.Infra.Data
{
    /// <summary>
    /// Contexto do banco de dados, juntamente com Identity Core
    /// </summary>
    public partial class LivrariaDbContext : IdentityDbContext<IdentityUser>, IUnitOfWork
    {
        public LivrariaDbContext(DbContextOptions<LivrariaDbContext> options) : base(options)
        {

        }

        public async Task<IEnumerable<ValidationFailure>> Commit()
        {
            var errors = new List<ValidationFailure>();
            var sucesso = false;

            try
            {
                sucesso = await base.SaveChangesAsync() > 0;
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return new List<ValidationFailure>();
        }
    }
}