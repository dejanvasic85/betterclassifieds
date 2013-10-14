using Paramount.Common.ServiceContracts;

namespace Paramount.Services.Proxy
{
    using System;
    using System.Configuration;
    using System.ServiceModel;
    using ApplicationBlock.Configuration;

    public static class WebServiceHostManager
    {
        private const string SectionName = "paramount/services";
        
        public static BroadcastServiceClient BroadcastServiceHost
        {
            get
            {
                return new BroadcastServiceClient(BasicHttpBinding, CreateEndpointAddress(ServiceDefinition.Broadcast.Address));
            }
        }

        public static IBannerService BannerServiceClient
        {
            get
            {
                return CreateClient<IBannerService>(ServiceDefinition.Banner.SectionKey);
            }
        }

        public static IBetterclassifiedService BetterclassifiedServiceClient
        {
            get
            {
                return CreateClient<IBetterclassifiedService>(ServiceDefinition.Betterclassifieds.SectionKey);
            }
        }

        public static IBillingService BillingServiceClient
        {
            get
            {
                return CreateClient<IBillingService>(ServiceDefinition.Billing.SectionKey);
            }
        }

        public static IMembershipService MembershipServiceClient
        {
            get
            {
                return CreateClient<IMembershipService>(ServiceDefinition.Membership.SectionKey);
            }
        }
        
        private static EndpointAddress CreateEndpointAddress(string serviceName)
        {
            var config = ConfigurationManager.GetSection(SectionName) as ConfigurationSectionHandler;

            if (config == null)
            {
                throw new ApplicationException("Invalid Configuration Section");
            }
            var configurationItem = config[serviceName];
            var address = new EndpointAddress(configurationItem.Value);
            return address;
        }
        
        private static BasicHttpBinding BasicHttpBinding
        {
            get
            {
                // Create a BasicHttpBinding instance
                var binding = new BasicHttpBinding();

                binding.Security.Mode = BasicHttpSecurityMode.None;
                binding.Security.Transport.ClientCredentialType = HttpClientCredentialType.None;
                binding.Security.Transport.ProxyCredentialType = HttpProxyCredentialType.None;

                binding.ReaderQuotas.MaxArrayLength = 2147483647;
                binding.ReaderQuotas.MaxDepth = 2147483647;
                binding.ReaderQuotas.MaxBytesPerRead = 2147483647;
                binding.ReaderQuotas.MaxStringContentLength = 2147483647;
                binding.ReaderQuotas.MaxNameTableCharCount = 2147483647;
                return binding;
            }
        }

        private static T CreateClient<T>(string serviceAddress)
        {
            ConfigurationSectionHandler config = ConfigurationManager.GetSection(SectionName) as ConfigurationSectionHandler;

            if (config == null)
            {
                throw new ApplicationException("Invalid Configuration Section");
            }

            ConfigurationItem configurationItem = config[serviceAddress];
            EndpointAddress address = new EndpointAddress(configurationItem.Value);
            ServiceChannelFactory<T> factory = new ServiceChannelFactory<T>(BasicHttpBinding, address);

            return factory.CreateChannel();
        }
    }
}
