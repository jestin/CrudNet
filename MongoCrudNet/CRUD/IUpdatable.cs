namespace MongoCrudNet.CRUD
{
    public interface IUpdatable<T>
    {
        /// <summary>
        /// This method updates the object in the database.  Be
        /// careful, because some databases and ORMs require that
        /// the object being updated was the actual one that was
        /// retrieved from the database.
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        T Update(T item);
    }
}
