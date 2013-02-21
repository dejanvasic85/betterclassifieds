namespace Paramount.ApplicationBlock.Logging.DataAccess
{
    using System;
    using System.Collections.ObjectModel;
    using System.Runtime.Serialization;
    using System.Security.Permissions;

    [Serializable]
    public class DataProxyFactoryException : Exception
    {
        private readonly Collection<Parameter> parameters;
        private readonly string procName;

        public DataProxyFactoryException() { }

        public DataProxyFactoryException(string message) : base(message) { }

        public DataProxyFactoryException(string message, Exception ex) : base(message, ex) { }

        public DataProxyFactoryException(string message, string procName, Collection<Parameter> parameters, Exception ex)
            : base(message, ex)
        {
            this.procName = procName;
            this.parameters = parameters;
        }

        protected DataProxyFactoryException(SerializationInfo serializationInfo, StreamingContext streamingContext)
            : base(serializationInfo, streamingContext) { }

        public string ProcName
        {
            get { return this.procName; }
        }

        public Collection<string> Parameters
        {
            get
            {
                var paramCollection = new Collection<string>();
                foreach (var param in this.parameters)
                {
                    paramCollection.Add(
                        param.Name + "=" + param.Value != null ? param.Value.ToString() : "null" + " (" + param.SqlDBType + ")");
                }

                return paramCollection;
            }
        }

        [SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.SerializationFormatter)]
        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            base.GetObjectData(info, context);
        }
    }
}
