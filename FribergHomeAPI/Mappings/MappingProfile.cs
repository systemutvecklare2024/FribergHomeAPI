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
				.ReverseMap();
			
			CreateMap<RealEstateAgent, RealEstateAgentDTO>()
				.ForMember(d => d.Agency, opt => opt.MapFrom(src=>src.Agency))
				.ForMember(d => d.Properties, opt => opt.MapFrom(src=>src.Properties))
				.ReverseMap();

			CreateMap<PropertyImage, PropertyImageDTO>()
				.ReverseMap();
		}
	} 
}
