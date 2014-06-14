using CrudNet;

namespace MySqlCrudNet
{
	public abstract class BaseMySqlRepository<T> : ICreatable<T>, IRetrievable<T>, IUpdatable<T>, IDeletable<T>, IBulkRetrievable<T> where T : new()
	{
		#region Dependencies

		protected readonly IMySqlDbContext Db;

		#endregion

		protected BaseMySqlRepository(IMySqlDbContext dbContext)
		{
			Db = dbContext;
		}

		#region ICreatable implementation

		public T Create()
		{
			throw new System.NotImplementedException();
		}

		public T Create(T item)
		{
			throw new System.NotImplementedException();
		}

		#endregion

		#region IRetrievable implementation

		public T Retrieve(object key)
		{
			throw new System.NotImplementedException();
		}

		public T Retrieve(object[] keys)
		{
			throw new System.NotImplementedException();
		}

		#endregion

		#region IUpdatable implementation

		public T Update(T item)
		{
			throw new System.NotImplementedException();
		}

		#endregion

		#region IDeletable implementation

		public bool Delete(object[] keys)
		{
			throw new System.NotImplementedException();
		}

		public bool Delete(T item)
		{
			throw new System.NotImplementedException();
		}

		public bool DeleteAll()
		{
			throw new System.NotImplementedException();
		}

		public bool DeleteAll(object[] keys)
		{
			throw new System.NotImplementedException();
		}

		#endregion

		#region IBulkRetrievable implementation

		public System.Collections.Generic.IEnumerable<T> RetrieveAll()
		{
			throw new System.NotImplementedException();
		}

		public System.Collections.Generic.IEnumerable<T> RetrieveAll(object key)
		{
			throw new System.NotImplementedException();
		}

		public System.Collections.Generic.IEnumerable<T> RetrieveAll(object[] keys)
		{
			throw new System.NotImplementedException();
		}

		#endregion
	}
}