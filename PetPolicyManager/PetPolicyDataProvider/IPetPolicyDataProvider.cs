using System;
using System.Collections.Generic;
using PetPolicyObjectSchema;

namespace PetPolicyDataProvider
{
    public interface IPetPolicyDataProvider
    {
        string GeneratePolicyNumber(string countryCode, int ownerId);
        PetOwnerDto RegisterOwner(string countryCode, string ownerName);
        PetDto AddPet(int ownerId, string petName, int speciesId, string breedName, DateTime petDateOfBirth);
        List<PetPolicySummaryDto> GetPetPolicySummaryList();
        List<PetPolicySummaryDto> GetPetPolicySummaryListById(int policyId);
        List<PetPolicySummaryDto> GetPetPolicySummaryListByOwner(int ownerId);
        DateTime? AddPetToPolicy(int petId, int policyId);
        DateTime? RemovePetFromPolicy(int petId, int policyId);
    }


}