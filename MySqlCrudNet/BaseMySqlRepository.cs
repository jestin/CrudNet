using CrudNet;

namespace MySqlCrudNet
{
	public abstract class BaseMySqlRepository<T> : ICreatable<T>, IRetrievable<T>, IUpdatable<T>, IDeletable<T>, IBulkRetrievable<T> where T : new()
	{
		#region Dependencies

		protected readonly IMySqlDbContext Db;
		private readonly IMySqlCommandProvider<T> _commandProvider;
		private readonly IMySqlObjectBuilder<T> _objectBuilder;

		#endregion

		protected BaseMySqlRepository(
			IMySqlDbContext dbContext,
			IMySqlCommandProvider<T> commandProvider,
			IMySqlObjectBuilder<T> objectBuilder)
		{
			Db = dbContext;
			_commandProvider = commandProvider;
			_objectBuilder = objectBuilder;
		}

		#region ICreatable implementation

		public T Create()
		{
			return new T();
		}

		public T Create(T item)
		{
			var cmd = _commandProvider.Create(item);
			cmd.Connection = Db.Connection;
			if(cmd.ExecuteNonQuery() > 0)
			{
				// TODO: Figure out how to set any auto-generated properties like auto-increment id fields
				return item;
			}

			return default(T);
		}

		#endregion

		#region IRetrievable implementation

		public T Retrieve(object key)
		{
			var cmd = _commandProvider.Retrieve(key);
			cmd.Connection = Db.Connection;
			return _objectBuilder.BuildObject(cmd.ExecuteReader());
		}

		public T Retrieve(object[] keys)
		{
			var cmd = _commandProvider.Retrieve(keys);
			cmd.Connection = Db.Connection;
			return _objectBuilder.BuildObject(cmd.ExecuteReader());
		}

		#endregion

		#region IUpdatable implementation

		public T Update(T item)
		{
			var cmd = _commandProvider.Update(item);
			cmd.Connection = Db.Connection;

			if(cmd.ExecuteNonQuery() > 0)
			{
				return item;
			}

			return default(T);
		}

		#endregion

		#region IDeletable implementation

		public bool Delete(object[] keys)
		{
			var cmd = _commandProvider.Delete(keys);
			cmd.Connection = Db.Connection;

			return cmd.ExecuteNonQuery() > 0 ? true : false;
		}

		public bool Delete(T item)
		{
			var cmd = _commandProvider.Delete(item);
			cmd.Connection = Db.Connection;

			return cmd.ExecuteNonQuery() > 0 ? true : false;
		}

		public bool DeleteAll()
		{
			var cmd = _commandProvider.DeleteAll();
			cmd.Connection = Db.Connection;

			return cmd.ExecuteNonQuery() > 0 ? true : false;
		}

		public bool DeleteAll(object[] keys)
		{
			var cmd = _commandProvider.DeleteAll(keys);
			cmd.Connection = Db.Connection;

			return cmd.ExecuteNonQuery() > 0 ? true : false;
		}

		#endregion

		#region IBulkRetrievable implementation

		public System.Collections.Generic.IEnumerable<T> RetrieveAll()
		{
			var cmd = _commandProvider.RetrieveAll();
			cmd.Connection = Db.Connection;
			return _objectBuilder.BuildObjects(cmd.ExecuteReader());
		}

		public System.Collections.Generic.IEnumerable<T> RetrieveAll(object key)
		{
			var cmd = _commandProvider.RetrieveAll(key);
			cmd.Connection = Db.Connection;
			return _objectBuilder.BuildObjects(cmd.ExecuteReader());
		}

		public System.Collections.Generic.IEnumerable<T> RetrieveAll(object[] keys)
		{
			var cmd = _commandProvider.RetrieveAll(keys);
			cmd.Connection = Db.Connection;
			return _objectBuilder.BuildObjects(cmd.ExecuteReader());
		}

		#endregion
	}
}