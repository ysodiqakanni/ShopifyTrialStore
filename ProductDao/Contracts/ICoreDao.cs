using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ProductDao.Contracts
{
    public interface ICoreDao<TEntity> where TEntity : class
    {
        /// <summary>
        /// Add single entity to the store
        /// </summary>
        /// <param name="entity"></param>
        void Add(TEntity entity);

        /// <summary>
        /// Add a collection of entities to the store
        /// </summary>
        /// <param name="entities"></param>
        void AddRange(IEnumerable<TEntity> entities);

        /// <summary>
        /// Get single entity from the store
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        TEntity Get(object id);

        /// <summary>
        /// Gets all entities from the store
        /// </summary>
        /// <returns></returns>
        IEnumerable<TEntity> GetAll();

        /// <summary>
        /// Find entity or group of entities matching a given expression
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> predicate);

        //IEnumerable<TEntity> FindUsingDictionary(Dictionary<string, object> dictionary);

        /// <summary>
        /// Remove single entity from the collection
        /// </summary>
        /// <param name="entity"></param>
        void Remove(TEntity entity);

        /// <summary>
        /// Removes a group of entities from the collection
        /// </summary>
        /// <param name="predicate"></param>
        void RemoveRange(IEnumerable<TEntity> entities);

        void Update(TEntity entity);

        void Attach(TEntity entity);
        void Include(string entityName);
    }
}
