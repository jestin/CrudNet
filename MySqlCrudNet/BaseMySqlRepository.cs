using CrudNet;
using System.Collections.Generic;

namespace MySqlCrudNet
{
	public abstract class BaseMySqlRepository<T> : ICreatable<T>, IRetrievable<T>, IUpdatable<T>, IDeletable<T>, IBulkRetrievable<T> where T : new()
	{
		#region Dependencies

		protected IMySqlDbContext Db;
		protected IMySqlCommandProvider<T> CommandProvider;
		protected IMySqlObjectBuilder<T> ObjectBuilder;

		#endregion

		protected BaseMySqlRepository(
			IMySqlDbContext dbContext,
			IMySqlCommandProvider<T> commandProvider,
			IMySqlObjectBuilder<T> objectBuilder)
		{
			Db = dbContext;
			CommandProvider = commandProvider;
			ObjectBuilder = objectBuilder;
		}

		#region ICreatable implementation

		public virtual T Create()
		{
			return new T();
		}

		public virtual T Create(T item)
		{
			var cmd = CommandProvider.Create(item);
			cmd.Connection = Db.Connection;
			if(cmd.ExecuteNonQuery() > 0)
			{
				return item;
			}

			return default(T);
		}

		#endregion

		#region IRetrievable implementation

		public virtual T Retrieve(object key)
		{
			var cmd = CommandProvider.Retrieve(key);
			cmd.Connection = Db.Connection;
			return ObjectBuilder.BuildObject(cmd.ExecuteReader());
		}

		public virtual T Retrieve(object[] keys)
		{
			var cmd = CommandProvider.Retrieve(keys);
			cmd.Connection = Db.Connection;
			return ObjectBuilder.BuildObject(cmd.ExecuteReader());
		}

		#endregion

		#region IUpdatable implementation

		public virtual T Update(T item)
		{
			var cmd = CommandProvider.Update(item);
			cmd.Connection = Db.Connection;

			if(cmd.ExecuteNonQuery() > 0)
			{
				return item;
			}

			return default(T);
		}

		#endregion

		#region IDeletable implementation

		public virtual bool Delete(object[] keys)
		{
			var cmd = CommandProvider.Delete(keys);
			cmd.Connection = Db.Connection;

			return cmd.ExecuteNonQuery() > 0 ? true : false;
		}

		public virtual bool Delete(T item)
		{
			var cmd = CommandProvider.Delete(item);
			cmd.Connection = Db.Connection;

			return cmd.ExecuteNonQuery() > 0 ? true : false;
		}

		public virtual bool DeleteAll()
		{
			var cmd = CommandProvider.DeleteAll();
			cmd.Connection = Db.Connection;

			return cmd.ExecuteNonQuery() > 0 ? true : false;
		}

		public virtual bool DeleteAll(object[] keys)
		{
			var cmd = CommandProvider.DeleteAll(keys);
			cmd.Connection = Db.Connection;

			return cmd.ExecuteNonQuery() > 0 ? true : false;
		}

		#endregion

		#region IBulkRetrievable implementation

		public virtual IEnumerable<T> RetrieveAll()
		{
			var cmd = CommandProvider.RetrieveAll();
			cmd.Connection = Db.Connection;
			return ObjectBuilder.BuildObjects(cmd.ExecuteReader());
		}

		public virtual IEnumerable<T> RetrieveAll(object key)
		{
			var cmd = CommandProvider.RetrieveAll(key);
			cmd.Connection = Db.Connection;
			return ObjectBuilder.BuildObjects(cmd.ExecuteReader());
		}

		public virtual IEnumerable<T> RetrieveAll(object[] keys)
		{
			var cmd = CommandProvider.RetrieveAll(keys);
			cmd.Connection = Db.Connection;
			return ObjectBuilder.BuildObjects(cmd.ExecuteReader());
		}

		#endregion
	}
}