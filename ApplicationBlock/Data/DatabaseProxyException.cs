namespace Paramount.ApplicationBlock.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Collections.ObjectModel;
    using System.Runtime.Serialization;
    using System.Security.Permissions;

    [Serializable]
    public class DatabaseProxyException : Exception
    {
        private readonly Collection<Parameter> _parameters;
        private readonly string _procName;

        public DatabaseProxyException() { }

        public DatabaseProxyException(string message) : base(message) { }

        public DatabaseProxyException(string message, Exception ex) : base(message, ex) { }

        public DatabaseProxyException(string message, string procName, Collection<Parameter> parameters, Exception ex)
            : base(message, ex)
        {
            this._procName = procName;
            this._parameters = parameters;
        }

        protected DatabaseProxyException(SerializationInfo serializationInfo, StreamingContext streamingContext)
            : base(serializationInfo, streamingContext) { }

        public string ProcName
        {
            get { return this._procName; }
        }

        public Collection<string> Parameters
        {
            get
            {
                var paramCollection = new Collection<string>();
                foreach (var param in this._parameters)
                {
                    paramCollection.Add(
                        param.Name + "=" + param.Value != null ? param.Value.ToString() : "null" + " (" + param.SqlDbType + ")");
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
