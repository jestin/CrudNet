using MySql.Data.MySqlClient;

namespace MySqlCrudNet
{
	/// <summary>
	/// Manages the connection to a MySql database
	/// </summary>
	public interface IMySqlDbContext
	{
		/// <summary>
		/// The current connection to a database
		/// </summary>
		/// <value>The connection.</value>
		MySqlConnection Connection { get; set; }

		/// <summary>
		/// A flag that indicates whether this context has been initialized
		/// </summary>
		/// <value><c>true</c> if initialized; otherwise, <c>false</c>.</value>
		bool Initialized { get; }

		/// <summary>
		/// Connects to the server and the database
		/// </summary>
		/// <param name="connectionString"></param>
		/// <returns>True when the connection succeeds</returns>
		bool Connect(string connectionString);

		/// <summary>
		/// Disconnects from the server and closes the connection
		/// </summary>
		void Disconnect();
	}
}