using AutoMapper;
using FribergHomeAPI.Data;
using FribergHomeAPI.Data.Repositories;
using FribergHomeAPI.DTOs;

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
		public async Task UpdatePropertyAsync(int id, PropertyDTO dto)
		{
			var existingProperty = await _propertyRepository.GetWithAddressAndImages(id);

			if (existingProperty == null)
			{
				throw new KeyNotFoundException($"Property with ID {id} not found.");
			}
			var imagesToDelete = existingProperty.Images
				.Where(img => !dto.ImageUrls.Any(updatedImg => updatedImg.Id == img.Id))
				.ToList();

			_mapper.Map(dto, existingProperty);
			await _propertyRepository.UpdateAsync(existingProperty, imagesToDelete);
		}
	}
}
