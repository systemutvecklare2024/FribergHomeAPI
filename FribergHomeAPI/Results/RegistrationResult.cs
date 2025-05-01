using FribergHomeAPI.Models;
using Microsoft.AspNetCore.Identity;

namespace FribergHomeAPI.Results
{
    // Author: Christoffer
    public record RegistrationResult(bool Success, RealEstateAgent? Agent, IEnumerable<IdentityError> Errors)
    {
        public static RegistrationResult Failed(IEnumerable<IdentityError> errors) => new(false, null, errors);
        public static RegistrationResult SuccessResult(RealEstateAgent agent) => new(true, agent, Enumerable.Empty<IdentityError>());
    }
}
