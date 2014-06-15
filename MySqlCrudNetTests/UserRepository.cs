using CrudNet;
using MySqlCrudNet;

namespace MySqlCrudNetTests
{
	public class UserRepository : LongIdentifiableMySqlRepository<User>, ICreatable<User>
	{
		public UserRepository(
			IMySqlDbContext db,
			IMySqlCommandProvider<User> commandProvider,
			IMySqlObjectBuilder<User> objectBuilder)
			: base(db, commandProvider, objectBuilder)
		{
		}
	}
}