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
        List<PolicyAndOwnerSummaryDto> GetPolicyAndOwnerSummaryList();
        List<PolicyAndOwnerSummaryDto> GetPetPolicySummaryListById(int policyId);
        List<PolicyAndOwnerSummaryDto> GetPolicyAndOwnerSummaryListByOwner(int ownerId);
        DateTime? AddPetToPolicy(int petId, int policyId);
        DateTime? RemovePetFromPolicy(int petId, int policyId);
    }


}