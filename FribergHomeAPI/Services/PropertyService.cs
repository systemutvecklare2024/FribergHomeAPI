using AutoMapper;
using FribergHomeAPI.Data;
using FribergHomeAPI.Data.Repositories;
using FribergHomeAPI.DTOs;
using FribergHomeAPI.Results;
using System.Security.Claims;

namespace FribergHomeAPI.Services
{
    public class PropertyService : IPropertyService
    {
        private readonly IPropertyRepository propertyRepository;
        private readonly IMapper mapper;
        private readonly IAccountService accountService;

        public PropertyService(ApplicationDbContext appDbContext, IPropertyRepository propertyRepository, IMapper mapper, IAccountService accountService)
        {
            this.propertyRepository = propertyRepository;
            this.mapper = mapper;
            this.accountService = accountService;
        }
        public async Task<ServiceResult<PropertyDTO>> UpdatePropertyAsync(ClaimsPrincipal user, int id, PropertyDTO dto)
        {
            var existingProperty = await propertyRepository.GetWithAddressImagesAndMuncipality(id);
            if (existingProperty == null)
            {
                return ServiceResult<PropertyDTO>.Failure(new ServiceResultError { Code = "404", Description = "Fastighet saknas." });
            }

            var isAllowed = await accountService.OwnedBy(user, existingProperty.RealEstateAgentId);
            if (!isAllowed)
            {
                return ServiceResult<PropertyDTO>.Failure(new ServiceResultError { Code = "403", Description = "Ni saknar rättigheter att ändra denna fastighet." });
            }

            try
            {
                var imagesToDelete = existingProperty.Images
                    .Where(img => !dto.ImageUrls.Any(updatedImg => updatedImg.Id == img.Id))
                    .ToList();

                mapper.Map(dto, existingProperty);
                await propertyRepository.UpdateAsync(existingProperty, imagesToDelete);
                var mapped = mapper.Map(existingProperty, dto);

                return ServiceResult<PropertyDTO>.SuccessResult(mapped);

            }
            catch (Exception ex)
            {
                return ServiceResult<PropertyDTO>.Failure(ex.Message);
            }
        }

        public async Task<ServiceResult> DeleteAsync(ClaimsPrincipal user, int id)
        {
            var prop = await propertyRepository.GetAsync(id);
            if (prop == null) return ServiceResult.Failure(new ServiceResultError { Code = "404", Description = "Fastighet saknas." });

            var allowed = await accountService.OwnedBy(user, prop.RealEstateAgentId);
            if (!allowed) return ServiceResult.Failure(new ServiceResultError { Code = "403", Description = "Ni saknar rättigheter att ta bort denna fastighet." });

            await propertyRepository.RemoveAsync(prop);
            return ServiceResult.SuccessResult();
        }
    }
}