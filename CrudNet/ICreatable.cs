namespace CrudNet
{
    public interface ICreatable<T>
    {
        /// <summary>
        /// This version of Create is useful if you implement CRUD with an ORM framework
        /// that must created the objects that get stored into the database(such as
        /// Entity).  Because it also calls InitializeItem, it is recommended to create
        /// you database objects with this method, even if your database doesn't require
        /// it.
        /// </summary>
        /// <returns></returns>
        T Create();

        /// <summary>
        /// This is the standard Create method that takes an object and inserts it into
        /// the database.  Depending on how CRUD is implemented, this object may need
        /// to be created with the other Create method.
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        T Create(T item);
    }
}
