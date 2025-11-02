namespace Timelines.Core.Exceptions
{
	public class MissingChildException : Exception
	{
		public MissingChildException() { }
		public MissingChildException(string message) : base(message) { }
		public MissingChildException(string message, Exception inner) : base(message, inner) { }
	}
}
