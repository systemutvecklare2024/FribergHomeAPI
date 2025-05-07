using AutoMapper;
using FribergHomeAPI.Data;
using FribergHomeAPI.Data.Repositories;
using FribergHomeAPI.DTOs;
using FribergHomeAPI.Results;

namespace FribergHomeAPI.Services
{
	public class PropertyService : IPropertyService
	{
		private readonly IPropertyRepository _propertyRepository;
		private readonly IMapper _mapper;

		public PropertyService(ApplicationDbContext appDbContext, IPropertyRepository propertyRepository, IMapper mapper)
		{
			_propertyRepository = propertyRepository;
			_mapper = mapper;
		}
		public async Task<ServiceResult<PropertyDTO>> UpdatePropertyAsync(int id, PropertyDTO dto)
		{
			var existingProperty = await _propertyRepository.GetWithAddressAndImages(id);

			if (existingProperty == null)
			{
				return ServiceResult<PropertyDTO>.Failure("Felaktigt Id på bostaden. Xtreme error.");
			}
			var imagesToDelete = existingProperty.Images
				.Where(img => !dto.ImageUrls.Any(updatedImg => updatedImg.Id == img.Id))
				.ToList();

			_mapper.Map(dto, existingProperty);
			await _propertyRepository.UpdateAsync(existingProperty, imagesToDelete);
			var mapped = _mapper.Map(existingProperty, dto);
			return ServiceResult<PropertyDTO>.SuccessResult(mapped);
		}
	}
}
