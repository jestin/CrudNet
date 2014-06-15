using System;
using MySql.Data.MySqlClient;

namespace MySqlCrudNet
{
	/// <summary>
	/// Manages the connection to a MySql database
	/// </summary>
	public class MySqlDbContext : IMySqlDbContext
	{
		#region IMySqlDbContext implementation

		/// <summary>
		/// The current connection to a database
		/// </summary>
		/// <value>The connection.</value>
		public MySqlConnection Connection { get; set; }

		/// <summary>
		/// A flag that indicates whether this context has been initialized
		/// </summary>
		/// <value><c>true</c> if initialized; otherwise, <c>false</c>.</value>
		public bool Initialized { get; set; }

		/// <summary>
		/// Connects to the server and the database
		/// </summary>
		/// <param name="connectionString"></param>
		/// <returns>True when the connection succeeds</returns>
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

		/// <summary>
		/// Disconnects from the server and closes the connection
		/// </summary>
		public void Disconnect()
		{
			Connection.Close();
		}

		#endregion
	}
}