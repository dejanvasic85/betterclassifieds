namespace Paramount.Services.Proxy
{
    using System;
    using System.Configuration;
    using System.Reflection;
    using System.ServiceModel;
    using System.ServiceModel.Channels;
    using Paramount.ApplicationBlock.Configuration;
    using Paramount.Common.DataTransferObjects;

    public abstract class BaseProxy
    {
        public static T CreateChannel<T>(Binding binding, string uri)
        {
            EndpointAddress address = null;
            object reflectOb = CreateGenericInstance<T>(binding, uri, ref address);
            return (T)Convert.ChangeType(reflectOb, typeof(T));
        }

        private static object CreateGenericInstance<T>(Binding binding, string uri, ref EndpointAddress address)
        {
            address = new EndpointAddress(uri);
            Type genericType = typeof(T);

            ConstructorInfo[] constructorInfo = genericType.GetConstructors();
            int constructorCounter;

            for (constructorCounter = 0; constructorCounter < constructorInfo.Length; constructorCounter++)
            {
                ParameterInfo[] paramInfo = constructorInfo[constructorCounter].GetParameters();
                if (paramInfo.Length == 2)
                    break;
            }

            if (constructorCounter == constructorInfo.Length)
                throw new Exception();

            object[] constructorArgs = new object[2];

            constructorArgs[0] = binding;

            constructorArgs[1] = address;
            return constructorInfo[constructorCounter].Invoke(constructorArgs);
        }

        /// <summary>
        /// Initialises the request header
        /// </summary>
        public static T SetRequestHeader<T>(BaseRequest baseRequest)
        {
            baseRequest.ApplicationName = ConfigSettingReader.ApplicationName;
            baseRequest.ClientCode = ConfigSettingReader.ClientCode;
            baseRequest.Domain = ConfigSettingReader.Domain;

            return (T)Convert.ChangeType(baseRequest, typeof(T));
        }
    }
}
