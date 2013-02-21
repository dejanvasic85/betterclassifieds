namespace Paramount.ApplicationBlock.Membership.Exceptions
{
    using System;

    public sealed class ApplicationNameNotSupportedException:Exception
    {
        public ApplicationNameNotSupportedException(string message):base(message)
        {
            Data.Add("DateTime", DateTime.Now);
        }

        public ApplicationNameNotSupportedException():this("Application Name is not supported"){}
    }
}
