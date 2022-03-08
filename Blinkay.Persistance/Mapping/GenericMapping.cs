using Blinkay.Domain;
using FluentNHibernate.Mapping;

namespace Blinkay.Persistance.Mapping
{
    public class GenericMapping : ClassMap<Generic>
    {
        public GenericMapping()
        {
            Id(x => x.Id).GeneratedBy.Identity();
            Map(x => x.Texto);
        }
    }
}
