using System;
using System.Collections.Generic;
using PetPolicyObjectSchema;

namespace PetPolicyDataProvider
{
    public abstract class PetPolicyDataProviderAbstract : IPetPolicyDataProvider
    {
        public abstract PetPolicyDto EnrollPolicy(string countryCode, int ownerId);
        public abstract PetOwnerDto RegisterOwner(string countryCode, string ownerName);
        public abstract PetOwnerDto GetOwnerById(int ownerId);
        public abstract PetDto AddPet(int ownerId, string petName, int speciesId, string breedName, DateTime petDateOfBirth);
        public abstract List<PolicyAndOwnerSummaryDto> GetPolicyAndOwnerSummaryList();
        public abstract List<PolicyAndOwnerSummaryDto> GetPolicyAndOwnerSummaryListById(int policyId);
        public abstract List<PolicyAndOwnerSummaryDto> GetPolicyAndOwnerSummaryListByOwner(int ownerId);
        public abstract DateTime? AddPetToPolicy(int petId, int policyId);
        public abstract DateTime? RemovePetFromPolicy(int petId, int policyId);
        public abstract List<PolicyAndPetSummaryDto> GetPolicyAndPetSummaryList();
        public abstract List<PolicyAndPetSummaryDto> GetPolicyAndPetSummaryListByPolicyId(int policyId);
        public abstract List<PolicyAndPetSummaryDto> GetPolicyAndPetSummaryListByPolicyIdAndPetId(int policyId, int petId);
    }
}
