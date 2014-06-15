using MySqlCrudNet;

namespace MySqlCrudNetTests
{
	public class UserObjectBuilder : IMySqlObjectBuilder<User>
	{
		#region IMySqlObjectBuilder implementation

		public User BuildObject(MySql.Data.MySqlClient.MySqlDataReader reader)
		{
			throw new System.NotImplementedException();
		}

		public System.Collections.Generic.IEnumerable<User> BuildObjects(MySql.Data.MySqlClient.MySqlDataReader reader)
		{
			throw new System.NotImplementedException();
		}

		#endregion
	}
}