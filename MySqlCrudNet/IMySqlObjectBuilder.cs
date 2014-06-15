using MySql.Data.MySqlClient;
using System.Collections.Generic;

namespace MySqlCrudNet
{
	/// <summary>
	/// This interface builds objects from the row(s) returned by a MySql query
	/// </summary>
	public interface IMySqlObjectBuilder<T>
	{
		/// <summary>
		/// Builds the object.
		/// </summary>
		/// <returns>The object.</returns>
		/// <param name="reader">Reader.</param>
		T BuildObject(MySqlDataReader reader);

		/// <summary>
		/// Builds the objects.
		/// </summary>
		/// <returns>The objects.</returns>
		/// <param name="reader">Reader.</param>
		IEnumerable<T> BuildObjects(MySqlDataReader reader);
	}
}