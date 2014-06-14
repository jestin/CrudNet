using System;
using MySql.Data.MySqlClient;

namespace MySqlCrudNet
{
	public class MySqlDbContext : IMySqlDbContext
	{
		#region IMySqlDbContext implementation

		public MySqlConnection Connection { get; set; }

		public bool Initialized { get; set; }

		public bool Connect(string connectionString)
		{
			Initialized = false;

			try
			{
				Connection = new MySqlConnection(connectionString);
				Connection.Open();
				Initialized = true;
			}
			catch(Exception)
			{
				return false;
			}

			return Initialized;
		}

		public void Disconnect()
		{
			Connection.Close();
		}

		#endregion
	}
}