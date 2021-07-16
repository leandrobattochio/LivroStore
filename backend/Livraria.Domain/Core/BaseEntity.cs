using System;

namespace Livraria.Domain.Core
{
    public abstract class BaseEntity
    {
        public Guid Id { get; protected set; }

        protected BaseEntity()
        {

        }
    }

    public interface IAggregatRoot
    {

    }
}