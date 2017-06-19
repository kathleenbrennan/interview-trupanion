using System;
using PetPolicyObjectSchema;

namespace PetPolicyDataProvider
{
    public class PetPolicyDataProviderDummy : PetPolicyDataProviderAbstract
    {
        public override string GeneratePolicyNumber(string countryCode, int ownerId)
        {
            return countryCode + "1234567890";
        }

        public override PetOwnerDto RegisterOwner(string countryCode, string ownerName)
        {
            return new PetOwnerDto
            {
                CountryId = 3,
                OwnerId = 3,
                OwnerName = ownerName
            };

        }
    }
}