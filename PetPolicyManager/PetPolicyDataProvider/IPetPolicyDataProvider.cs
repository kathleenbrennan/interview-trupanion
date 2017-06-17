using PetPolicyObjectSchema;

namespace PetPolicyDataProvider
{
    public interface IPetPolicyDataProvider
    {
        string GeneratePolicyNumber(string countryCode);
    }


}