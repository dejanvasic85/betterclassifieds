using System;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Description;

namespace Paramount.Services.Proxy
{
    public class ServiceChannelFactory<T> : ChannelFactory<T>
    {
        public ServiceChannelFactory(Binding binding, EndpointAddress remoteAddress) : base(binding, remoteAddress)
        {
        }

        public ServiceChannelFactory()
        {
        }

        public ServiceChannelFactory(ServiceEndpoint endpoint) : base(endpoint)
        {
        }
    }
}