using MySqlCrudNet;
using System.Collections.ObjectModel;
using System.Collections.Generic;

namespace MySqlCrudNetTests
{
	public class UserObjectBuilder : IMySqlObjectBuilder<User>
	{
		#region IMySqlObjectBuilder implementation

		public User BuildObject(MySql.Data.MySqlClient.MySqlDataReader reader)
		{
			// we only expect a single row, so there's no need to read in a loop
			reader.Read();
			var user = new User
			{
				Id = reader.GetInt64(0),
				FirstName = reader.GetString(1),
				LastName = reader.GetString(2),
				DateCreated = reader.GetDateTime(3),
				Gender = reader.GetChar(4)
			};

			reader.Close();

			return user;
		}

		public IEnumerable<User> BuildObjects(MySql.Data.MySqlClient.MySqlDataReader reader)
		{
			var users = new Collection<User>();

			while(reader.Read())
			{
				users.Add(new User
				{
					Id = reader.GetInt64(0),
					FirstName = reader.GetString(1),
					LastName = reader.GetString(2),
					DateCreated = reader.GetDateTime(3),
					Gender = reader.GetChar(4)
				});
			}

			reader.Close();

			return users;
		}

		#endregion
	}
}