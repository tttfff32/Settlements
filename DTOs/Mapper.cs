using AutoMapper;
using Settlements.DTOs;
using Settlements.Models; 

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Settlement, SettlementDTO>()
            .ReverseMap();
    }
}