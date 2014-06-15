namespace Crud
{
	/// <summary>
	/// This is an interface to be implemented by domain objects or database objects (depending on
	/// your case, you may not want domain objects to implement interfaces).  It ensures that a
	/// class has a long Id field on it.  This interface allows a base implementation of
	/// LongIdentifiable*Repository to be provided.
	/// </summary>
	public interface ILongIdentifiable
	{
		long Id { get; set; }
	}
}