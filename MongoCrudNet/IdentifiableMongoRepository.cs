using System;
using CrudNet;
using MongoDB.Bson;
using MongoDB.Driver.Builders;

namespace MongoCrudNet.Mongo
{
    /// <summary>
    /// This abstract child of BaseExampleRepository is used to supply additional functionality for handling
    /// database id's.  It adds a constraint on the template type to ensure that it has an 'Id' field.
    /// Not all repositories in this application necessarily be derived from this class, only the ones
    /// for a domain object with an 'Id' field.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class IdentifiableMongoRepository<T> : BaseMongoRepository<T> where T : IIdentifiable, new()
    {
        protected IdentifiableMongoRepository(IMongoDbContext dbContext) : base(dbContext)
        {
        }

        /// <summary>
        /// Generate database id's manually because the domain object includes an 'Id' field itself.
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public override T Create(T item)
        {
            item.Id = ObjectId.GenerateNewId().ToString();
            return base.Create(item);
        }

        /// <summary>
        /// This method was impossible to implement without knowing how to query the object, but now
        /// that we know that T contains a unique Id field, we can implement it here.
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public override bool Delete(T item)
        {
            try
            {
                var result = Collection.Remove(Query.EQ("_id", item.Id.ToString()));
                return !result.HasLastErrorMessage;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
