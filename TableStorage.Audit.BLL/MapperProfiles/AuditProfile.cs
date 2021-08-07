using AutoMapper;
using TableStorage.Audit.BLL.Responses;
using Z.EntityFramework.Plus;

namespace TableStorage.Audit.BLL.MapperProfiles
{
    public class AuditProfile : Profile
    {
        public AuditProfile()
        {
            CreateMap<AuditEntry, AuditEntryResponse>();
            CreateMap<AuditEntryProperty, AuditEntryPropertyResponse>();
        }
    }
}