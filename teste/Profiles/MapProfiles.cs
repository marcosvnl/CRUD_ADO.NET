using AutoMapper;
using teste.DTO;
using teste.Entity;

namespace teste.Profiles;

internal class MapProfiles
{
    internal IMapper ConfigMapper
    {
        get
        {
            var config = new MapperConfiguration(c =>
            {
                c.CreateMap<Users, UsersDTO>()
                    .ForMember(user => user.Nome, dto => dto.MapFrom(u => u.Name))
                    .ForMember(user => user.SobreNome, dto => dto.MapFrom(u => u.LastName))
                    .ForMember(user => user.Idade, dto => dto.MapFrom(u => u.YearsOld))
                    .ForMember(user => user.MaiorIdade, dto => dto.MapFrom(u => u.LegalAge))
                    .ReverseMap();
            });

            return config.CreateMapper();
        }
    }
    
}
