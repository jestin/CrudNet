using MySql.Data.MySqlClient;

namespace MySqlCrudNet
{
	/// <summary>
	/// This is an interface that provides the commands sent to a MySql database
	/// in order to create a CRUD repository.
	/// </summary>
	public interface IMySqlCommandProvider<T>
	{
		/// <summary>
		/// Provides the command to create an object
		/// </summary>
		/// <param name="item">Item.</param>
		MySqlCommand Create(T item);

		/// <summary>
		/// Provides the command to retrieve an object
		/// </summary>
		/// <param name="key">Key.</param>
		MySqlCommand Retrieve(object key);

		/// <summary>
		/// Provides the command to retrieve an object from a complex key
		/// </summary>
		/// <param name="keys">Keys.</param>
		MySqlCommand Retrieve(object[] keys);

		/// <summary>
		/// Provides the command to update the specified item.
		/// </summary>
		/// <param name="item">Item.</param>
		MySqlCommand Update(T item);

		/// <summary>
		/// Provides the command to delete an item from a complex key.
		/// </summary>
		/// <param name="keys">Keys.</param>
		MySqlCommand Delete(object[] keys);

		/// <summary>
		/// Provides the command to delete the specified item.
		/// </summary>
		/// <param name="item">Item.</param>
		MySqlCommand Delete(T item);

		/// <summary>
		/// Provides the command to delete all.
		/// </summary>
		/// <returns>The all.</returns>
		MySqlCommand DeleteAll();

		/// <summary>
		/// Provides the command to delete all items from a complex key.
		/// </summary>
		/// <returns>The all.</returns>
		/// <param name="keys">Keys.</param>
		MySqlCommand DeleteAll(object[] keys);

		/// <summary>
		/// Provides the command to retrieve all.
		/// </summary>
		/// <returns>The all.</returns>
		MySqlCommand RetrieveAll();

		/// <summary>
		/// Provides the command to retrieve all from a key.
		/// </summary>
		/// <returns>The all.</returns>
		/// <param name="key">Key.</param>
		MySqlCommand RetrieveAll(object key);

		/// <summary>
		/// Provides the command to retrieves all from a complex key.
		/// </summary>
		/// <returns>The all.</returns>
		/// <param name="keys">Keys.</param>
		MySqlCommand RetrieveAll(object[] keys);
	}
}