using Crud;

namespace MySqlCrudNet
{
	public class LongIdentifiableMySqlRepository<T> : BaseMySqlRepository<T> where T : ILongIdentifiable, new()
	{
		protected LongIdentifiableMySqlRepository(
			IMySqlDbContext dbContext,
			IMySqlCommandProvider<T> commandProvider,
			IMySqlObjectBuilder<T> objectBuilder)
			: base(dbContext, commandProvider, objectBuilder)
		{
			Db = dbContext;
			CommandProvider = commandProvider;
			ObjectBuilder = objectBuilder;
		}

		public override T Create(T item)
		{
			var cmd = CommandProvider.Create(item);
			cmd.Connection = Db.Connection;

			if(cmd.ExecuteNonQuery() > 0)
			{
				item.Id = cmd.LastInsertedId;
				return item;
			}

			return default(T);
		}
	}
}

