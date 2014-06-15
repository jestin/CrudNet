using System;
using MySql.Data.MySqlClient;
using MySqlCrudNet;

namespace MySqlCrudNetTests
{
	public class UserCommandProvider : IMySqlCommandProvider<User>
	{
		#region IMySqlCommandProvider implementation

		public MySqlCommand Create(User item)
		{
			var cmd = new MySqlCommand(@"INSERT INTO users (first_name, last_name, date_created, gender) " +
			                           "VALUES(@firstName, @lastName, @dateCreated, @gender)");
			cmd.Parameters.AddWithValue("@firstName", item.FirstName);
			cmd.Parameters.AddWithValue("@lastName", item.LastName);
			cmd.Parameters.AddWithValue("@dateCreated", DateTime.Now);
			cmd.Parameters.AddWithValue("@gender", item.Gender);

			return cmd;
		}

		public MySqlCommand Retrieve(object key)
		{
			var cmd = new MySqlCommand(@"SELECT * FROM users WHERE id=@id");
			cmd.Parameters.AddWithValue("@id", (long) key);

			return cmd;
		}

		public MySqlCommand Retrieve(object[] keys)
		{
			var cmd = new MySqlCommand(@"SELECT * FROM users WHERE id=@id");
			cmd.Parameters.AddWithValue("@id", (long) keys[0]); // ignoring complex keys for this table

			return cmd;
		}

		public MySqlCommand Update(User item)
		{
			var cmd = new MySqlCommand(@"UPDATE users SET first_name=@firstName, last_name=@lastName, gender=@gender WHERE id=@id");
			cmd.Parameters.AddWithValue("@firstName", item.FirstName);
			cmd.Parameters.AddWithValue("@lastName", item.LastName);
			cmd.Parameters.AddWithValue("@gender", item.Gender);
			cmd.Parameters.AddWithValue("@id", item.Id);

			return cmd;
		}

		public MySqlCommand Delete(object[] keys)
		{
			var cmd = new MySqlCommand(@"DELETE FROM users WHERE id=@id");
			cmd.Parameters.AddWithValue("@id", (long) keys[0]);

			return cmd;
		}

		public MySqlCommand Delete(User item)
		{
			var cmd = new MySqlCommand(@"DELETE FROM users WHERE id=@id");
			cmd.Parameters.AddWithValue("@id", item.Id);

			return cmd;
		}

		public MySqlCommand DeleteAll()
		{
			return new MySqlCommand(@"DELETE FROM users");
		}

		public MySqlCommand DeleteAll(object[] keys)
		{
			throw new System.NotImplementedException();
		}

		public MySqlCommand RetrieveAll()
		{
			return new MySqlCommand(@"SELECT * FROM users");
		}

		public MySqlCommand RetrieveAll(object key)
		{
			throw new System.NotImplementedException();
		}

		public MySqlCommand RetrieveAll(object[] keys)
		{
			throw new System.NotImplementedException();
		}

		#endregion
	}
}