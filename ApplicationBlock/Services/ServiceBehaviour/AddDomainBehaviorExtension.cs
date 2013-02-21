namespace Paramount.ApplicationBlock.Services.ServiceBehaviour
{
    using System;
    using System.ServiceModel.Configuration;

    public class AddDomainBehaviorExtension : BehaviorExtensionElement
    {
        protected override object CreateBehavior()
        {
            return new AddDomainEndpointBehavior();
        }

        public override Type BehaviorType
        {
            get
            {
                var t = typeof (AddDomainEndpointBehavior);
                return t;
            }
        }
    }
}