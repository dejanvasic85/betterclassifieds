namespace Paramount.Betterclassifieds.Business
{
    using System;
    public class SetupException : Exception
    {
        public SetupException(string msg)
            : base(msg)
        { }
    }

    public class OperationOnDisabledValueException : Exception
    {
        public OperationOnDisabledValueException(string msg) : base(msg)
        {

        }
    }
}
