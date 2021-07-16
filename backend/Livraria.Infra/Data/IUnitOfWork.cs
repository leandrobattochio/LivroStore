using System.Collections.Generic;
using System.Threading.Tasks;
using FluentValidation.Results;

namespace Livraria.Infra.Data
{
    public interface IUnitOfWork
    {
        Task<IEnumerable<ValidationFailure>> Commit();
    }
}