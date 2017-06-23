using System;
using System.Collections.Generic;
using PetPolicyObjectSchema;

namespace PetPolicyDataProvider
{
    public class PetPolicyDataProviderDummy : PetPolicyDataProviderAbstract
    {
        public override PetPolicyDto EnrollPolicy(string countryCode, int ownerId)
        {
            return new PetPolicyDto
            {
                PolicyNumber = countryCode + "1234567890",
                PolicyId = 999
            };
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

        public override PetOwnerDto GetOwnerById(int ownerId)
        {
            return new PetOwnerDto
            {
                CountryId = 3,
                OwnerId = ownerId,
                OwnerName = "Felix Mendelssohn"
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

        public override List<PolicyAndOwnerSummaryDto> GetPolicyAndOwnerSummaryList()
        {
            var list = new List<PolicyAndOwnerSummaryDto> { new PolicyAndOwnerSummaryDto { OwnerId = 1 } };
            return list;
        }

        public override List<PolicyAndOwnerSummaryDto> GetPolicyAndOwnerSummaryListById(int policyId)
        {
            var list = new List<PolicyAndOwnerSummaryDto> { new PolicyAndOwnerSummaryDto { PolicyId = policyId } };
            return list;
        }


        public override List<PolicyAndOwnerSummaryDto> GetPolicyAndOwnerSummaryListByOwner(int ownerId)
        {
            var list = new List<PolicyAndOwnerSummaryDto> {new PolicyAndOwnerSummaryDto {OwnerId = ownerId}};
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

        public override List<PolicyAndPetSummaryDto> GetPolicyAndPetSummaryList()
        {
            var list = new List<PolicyAndPetSummaryDto> { new PolicyAndPetSummaryDto { PetId = 1 } };
            return list;
        }

        public override List<PolicyAndPetSummaryDto> GetPolicyAndPetSummaryListByPolicyId(int policyId)
        {
            var list = new List<PolicyAndPetSummaryDto> { new PolicyAndPetSummaryDto { PetId = 1 } };
            return list;
        }

        public override List<PolicyAndPetSummaryDto> GetPolicyAndPetSummaryListByPolicyIdAndPetId(int policyId, int petId)
        {
            var list = new List<PolicyAndPetSummaryDto> { new PolicyAndPetSummaryDto { PetId = 1 } };
            return list;
        }
    }
}