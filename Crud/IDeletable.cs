namespace CrudNet
{
    public interface IDeletable<T>
    {
        /// <summary>
        /// This Delete method will delete an object that has a
        /// specific complex key.
        /// </summary>
        /// <param name="keys"></param>
        /// <returns></returns>
        bool Delete(object[] keys);

        /// <summary>
        /// This Delete method will delete a given object from
        /// the database.
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        bool Delete(T item);

        /// <summary>
        /// This method will remove all objects of this type from
        /// the database.
        /// </summary>
        /// <returns></returns>
        bool DeleteAll();

        /// <summary>
        /// This DeleteAll method will delete all objects that have a
        /// specific complex key.
        /// </summary>
        /// <param name="keys"></param>
        /// <returns></returns>
        bool DeleteAll(object[] keys);
    }
}
