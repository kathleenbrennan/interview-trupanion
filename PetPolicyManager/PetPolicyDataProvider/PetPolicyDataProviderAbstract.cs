using System;
using PetPolicyObjectSchema;

namespace PetPolicyDataProvider
{
    public abstract class PetPolicyDataProviderAbstract : IPetPolicyDataProvider
    {
        public abstract string GeneratePolicyNumber(string countryCode, int ownerId);
        public abstract PetOwnerDto RegisterOwner(string countryCode, string ownerName);
        public abstract PetDto AddPet(int ownerId, string petName, int speciesId, string breedName, DateTime petDateOfBirth);
    }
}
