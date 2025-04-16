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
				.ReverseMap();
		}
	} 
}
