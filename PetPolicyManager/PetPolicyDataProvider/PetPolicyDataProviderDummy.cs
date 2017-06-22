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

        public override List<PetPolicySummaryDto> GetPetPolicySummaryList()
        {
            var list = new List<PetPolicySummaryDto> { new PetPolicySummaryDto { OwnerId = 1 } };
            return list;
        }

        public override List<PetPolicySummaryDto> GetPetPolicySummaryListById(int policyId)
        {
            var list = new List<PetPolicySummaryDto> { new PetPolicySummaryDto { PolicyId = policyId } };
            return list;
        }


        public override List<PetPolicySummaryDto> GetPetPolicySummaryListByOwner(int ownerId)
        {
            var list = new List<PetPolicySummaryDto> {new PetPolicySummaryDto {OwnerId = ownerId}};
            return list;
        }

        public override DateTime? AddPetToPolicy(int petId, int policyId)
        {
            //this doesn't really test anything since it is always successful
            return DateTime.Now;
        }

        public override DateTime? RemovePetFromPolicy(int petId, int policyId)
        {
            //this doesn't really test anything since it is always successful
            return DateTime.Now;
        }
    }
}