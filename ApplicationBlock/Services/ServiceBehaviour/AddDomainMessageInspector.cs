namespace Paramount.ApplicationBlock.Services.ServiceBehaviour
{
    using System;
    using System.Net;
    using System.ServiceModel;
    using System.ServiceModel.Channels;
    using System.ServiceModel.Dispatcher;

    public class AddDomainMessageInspector : IClientMessageInspector
    {
        public object BeforeSendRequest(ref Message request, IClientChannel channel)
        {
            HttpRequestMessageProperty httpRequest = GetHttpRequestProp(request);
            if (httpRequest != null)
            {
                httpRequest.Headers.Add("Domain", "ParamountIt");
                return  httpRequest;
            }

            return null;

        }

        private static HttpRequestMessageProperty GetHttpRequestProp(Message request)
        {
            if (!request.Properties.ContainsKey(HttpRequestMessageProperty.Name))
            {
                request.Properties.Add(HttpRequestMessageProperty.Name, new HttpRequestMessageProperty());
            }

            return request.Properties[HttpRequestMessageProperty.Name] as HttpRequestMessageProperty;
        }


        public void AfterReceiveReply(ref Message reply, object correlationState)
        {
            return;
        }
    }
}