using System;
using System.Collections.Generic;
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

        public override PetDto AddPet(int ownerId, string petName, int speciesId, string breedName, DateTime petDateOfBirth)
        {
            return new PetDto
            {
                OwnerId = ownerId,
                PetName = petName,
                SpeciesId = speciesId,
                BreedName = breedName,
                PetDateOfBirth = petDateOfBirth
            };
        }

        public override List<PetPolicySummaryDto> GetPetPolicySummaryList(int ownerId)
        {
            var list = new List<PetPolicySummaryDto>();
            list.Add(new PetPolicySummaryDto { OwnerId = ownerId});
            return list;
        }


    }
}