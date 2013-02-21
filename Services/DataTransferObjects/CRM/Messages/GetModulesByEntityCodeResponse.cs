namespace Paramount.Common.DataTransferObjects.CRM.Messages
{
    using System.Collections.ObjectModel;

    public class GetModulesByEntityCodeResponse
    {
        private readonly Collection<ParamountModule> _moduleList = new Collection<ParamountModule>();
        public Collection<ParamountModule> Modules
        {
            get { return _moduleList; }
        }
    }
}
