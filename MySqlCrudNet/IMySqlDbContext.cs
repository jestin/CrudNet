using MySql.Data.MySqlClient;

namespace MySqlCrudNet
{
	public interface IMySqlDbContext
	{
		MySqlConnection Connection { get; set; }
		bool Initialized { get; }

		/// <summary>
		/// Connects to the server and the database
		/// </summary>
		/// <param name="connectionString"></param>
		/// <param name="databaseName"></param>
		/// <returns></returns>
		bool Connect(string connectionString);

		/// <summary>
		/// Disconnects from the server and closes the connection
		/// </summary>
		void Disconnect();
	}
}