using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Paramount.Common.ServiceContracts;
using System.ServiceModel;
using Paramount.Common.DataTransferObjects.LoggingService.Messages;
using System.ServiceModel.Channels;

namespace Paramount.Services.Proxy
{
    public partial class LogServiceClient : ClientBase<ILogService>
    {
        public LogServiceClient() { }

        public LogServiceClient(string endpointConfigurationName)
            : base(endpointConfigurationName) { }

        public LogServiceClient(Binding binding, EndpointAddress endpointAddress)
            : base(binding, endpointAddress) { }

        public LogAddResponse AddLogAudit(LogAddRequest request)
        {
            return Channel.Add(request);
        }
    }
}
