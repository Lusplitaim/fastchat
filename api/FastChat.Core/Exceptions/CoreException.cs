namespace FastChat.Core.Exceptions
{
    public class CoreException : Exception
    {
        public CoreException() { }

        public CoreException(string? message, Exception? innerException) : base(message, innerException)
        {

        }

        public CoreException(string? message) : base(message, null)
        {

        }
    }
}
