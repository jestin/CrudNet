namespace CrudNet
{
    public interface IRetrievable<T>
    {
        /// <summary>
        /// This Retrieve method is for the simple case where a
        /// database object has only a single key.  It will be
        /// the one that gets used in most cases.
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        T Retrieve(object key);

        /// <summary>
        /// This Retrieve method can be useful for when the
        /// database object has a complex key.
        /// </summary>
        /// <param name="keys"></param>
        /// <returns></returns>
        T Retrieve(object[] keys);
    }
}
