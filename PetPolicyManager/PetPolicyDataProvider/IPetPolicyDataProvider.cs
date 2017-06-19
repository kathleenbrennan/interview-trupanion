using PetPolicyObjectSchema;

namespace PetPolicyDataProvider
{
    public interface IPetPolicyDataProvider
    {
        string GeneratePolicyNumber(string countryCode, int ownerId);
        PetOwnerDto RegisterOwner(string countryCode, string ownerName);
    }


}