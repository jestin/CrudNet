using System.Collections.Generic;

namespace MongoCrudNet.CRUD
{
    public interface IBulkRetrievable<T>
    {
        /// <summary>
        /// This method retrieves all instances of a database object
        /// that are in the database.  This usually means every row
        /// in a table, for databases that are implemented with
        /// rows and tables.
        /// </summary>
        /// <returns></returns>
        IEnumerable<T> RetrieveAll();

        /// <summary>
        /// This method retrieves all the objects that have a specific
        /// key.
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        IEnumerable<T> RetrieveAll(object key);

        /// <summary>
        /// This method retrieves all the objects that have a specific
        /// complex key.
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        IEnumerable<T> RetrieveAll(object[] keys);
    }
}
