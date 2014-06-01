using Crud;
using MongoCrudNet.Mongo;
using MongoDB.Driver.Builders;
using System;

namespace MongoCrudNet
{
	/// <summary>
	/// This abstract child of BaseExampleRepository is used to supply additional functionality for handling
	/// database id's.  It adds a constraint on the template type to ensure that it has an 'Id' field.
	/// Not all repositories in this application necessarily be derived from this class, only the ones
	/// for a domain object with an 'Id' field.
	/// </summary>
	/// <remarks>
	/// When using IntIdentifiableMongoRepository, the caller must always set the Id field before creating,
	/// otherwise a zero will be attempted, most likely causing id conflicts.
	/// </remarks>
	/// <typeparam name="T"></typeparam>
	public abstract class IntIdentifiableMongoRepository<T> : BaseMongoRepository<T> where T : IIntIdentifiable, new()
	{
		protected IntIdentifiableMongoRepository(IMongoDbContext context)
			: base(context)
		{
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
				var result = Collection.Remove(Query.EQ("_id", item.Id));
				return !result.HasLastErrorMessage;
			}
			catch (Exception)
			{
				return false;
			}
		}
	}
}