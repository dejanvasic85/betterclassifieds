namespace Paramount.Common.DataTransferObjects.CRM.Messages
{
    using System.Collections.ObjectModel;
    using CRM;

    public class GetEntityListResponse
    {
        public int TotalPopulationSize { get; set; }
        private Collection<ParamountEntity> _entities = new Collection<ParamountEntity>();
        public Collection<ParamountEntity> Entities
        {
            get
            {
                return _entities;
            }
            set
            {
                _entities = value;
            }
        }
    }
}
