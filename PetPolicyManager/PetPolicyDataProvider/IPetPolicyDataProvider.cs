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
        List<PolicyAndOwnerSummaryDto> GetPolicyAndOwnerSummaryListById(int policyId);
        List<PolicyAndOwnerSummaryDto> GetPolicyAndOwnerSummaryListByOwner(int ownerId);
        DateTime? AddPetToPolicy(int petId, int policyId);
        DateTime? RemovePetFromPolicy(int petId, int policyId);
        List<PolicyAndPetSummaryDto> GetPolicyAndPetSummaryList();
        List<PolicyAndPetSummaryDto> GetPolicyAndPetSummaryListByPolicyId(int policyId);
        List<PolicyAndPetSummaryDto> GetPolicyAndPetSummaryListByPolicyIdAndPetId(int policyId, int petId);
    }


}