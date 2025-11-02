namespace Timelines.Core.Exceptions
{
	public class ChildAlreadyExistsException : Exception
	{
		public ChildAlreadyExistsException() { }
		public ChildAlreadyExistsException(string message) : base(message) { }
		public ChildAlreadyExistsException(string message, Exception inner) : base(message, inner) { }
	}
}
