namespace Crud
{
	/// <summary>
	/// This is an interface to be implemented by domain objects or database objects (depending on
	/// your case, you may not want domain objects to implement interfaces).  It ensures that a
	/// class has an int Id field on it.  This interface allows a base implementation of
	/// IntIdentifiableMongoRepository to be provided.
	/// </summary>
	public interface IIntIdentifiable
	{
		int Id { get; set; }
	}
}