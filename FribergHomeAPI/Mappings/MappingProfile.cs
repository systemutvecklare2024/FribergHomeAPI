using AutoMapper;
using FribergHomeAPI.DTOs;
using FribergHomeAPI.Models;

namespace FribergHomeAPI.Mappings
{
	public class MappingProfile : Profile
	{
		public MappingProfile()
		{
			CreateMap<Property, PropertyDTO>()
				.ForMember(d => d.Street, opt => opt.MapFrom(src => src.Address.Street))
				.ForMember(d => d.PostalCode, opt => opt.MapFrom(src => src.Address.PostalCode))
				.ForMember(d => d.City, opt => opt.MapFrom(src => src.Address.City))
				.ForMember(d => d.MuncipalityId, opt => opt.MapFrom(src => src.MuncipalityId))
				.ForMember(d=>d.ImageUrls, opt => opt.MapFrom(src=>src.Images))
				.ReverseMap();

            CreateMap<RealEstateAgency, RealEstateAgencyDTO>()
                .MaxDepth(1)
                .PreserveReferences()
                .ForMember(d => d.Agents, opt => opt.MapFrom(src => src.Agents))
                .ReverseMap();
			CreateMap<RealEstateAgency, RealEstateAgencyPageDTO>()
				.ForMember(d => d.Agents, opt => opt.MapFrom(src => src.Agents));
			
			CreateMap<RealEstateAgent, RealEstateAgentDTO>()
                .MaxDepth(1)
                .PreserveReferences()
                .ForMember(d => d.Agency, opt => opt.MapFrom(src=>src.Agency))
				.ForMember(d => d.Properties, opt => opt.MapFrom(src=>src.Properties))
                .ReverseMap();
			CreateMap<RealEstateAgent, RealEstateAgentsFromAgencyDTO>()
				.ReverseMap();

			CreateMap<PropertyImage, PropertyImageDTO>()
				.ReverseMap();
			CreateMap<Muncipality, MuncipalityDTO>()
				.ForMember(d => d.Id, opt => opt.MapFrom(src => src.Id))
				.ForMember(d=>d.Name, opt=> opt.MapFrom(src => src.Name))
				.ReverseMap();
		}
	} 
}
