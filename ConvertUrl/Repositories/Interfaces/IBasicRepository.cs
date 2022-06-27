using System;
using ConvertUrlRepository.Domain;

namespace ConvertUrlRepository.Repositories.Interfaces
{
    public interface IBasicRepository<TEntity> : IDisposable where TEntity : BaseUrlEntity
    {
        TEntity Insert(TEntity entity);
        bool SaveChange();


    }
}
