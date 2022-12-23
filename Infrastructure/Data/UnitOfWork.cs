using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Entities;
using Core.Interfaces;

namespace Infrastructure.Data
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext context;
        private Hashtable repositories;

        public UnitOfWork(AppDbContext context)
        {
            this.context = context;
        }
        public async Task<int> Complete()
        {
            return await context.SaveChangesAsync();
        }

        public void Dispose()
        {
            context.Dispose();
        }

        public IGenericRepository<TEntity> Repository<TEntity>() where TEntity : BaseEntity
        {
            //We check if we have anything in our HashTable or if we have one created at the moment
            if (repositories == null) repositories = new Hashtable();

            //We want to get the type of the entity, the name of it
            var type = typeof(TEntity).Name;

            //We see if our hashtable contains an entity with the specific name
            if (!repositories.ContainsKey(type))
            {
                var repositoryType = typeof(GenericRepository<>);

                var repositoryInstance = Activator.CreateInstance(repositoryType.MakeGenericType(typeof(TEntity)), context);

                repositories.Add(type, repositoryInstance);
            }

            return (IGenericRepository<TEntity>)repositories[type];
        }
    }
}
