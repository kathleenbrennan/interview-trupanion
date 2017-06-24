using System.Collections.Generic;
using System.Linq;
using PetPolicyObjectSchema;

namespace PetPolicyLibrary
{
    public class PolicyAndPetSummaryList : List<IPolicyAndPetSummary>
    {
        internal static List<IPolicyAndPetSummary> GetPolicyAndPetSummaryList(int policyId)
        {
            var provider = DataProviderFactory.GetDataProvider();
            var dtoList = provider.GetPolicyAndPetSummaryListByPolicyId(policyId);

            var summaryList = PopulatePolicyAndPetSummaries(dtoList);
            return summaryList;
        }

        internal static List<IPolicyAndPetSummary> GetPolicyAndPetSummaryList(int policyId, int petId)
        {
            var provider = DataProviderFactory.GetDataProvider();
            var dtoList = provider.GetPolicyAndPetSummaryListByPolicyIdAndPetId(policyId, petId);

            var summaryList = PopulatePolicyAndPetSummaries(dtoList);
            return summaryList;
        }

        internal static List<IPolicyAndPetSummary> GetPolicyAndPetSummaryListByOwnerId(int ownerId)
        {
            var provider = DataProviderFactory.GetDataProvider();
            var dtoList = provider.GetPolicyAndPetSummaryListByOwnerId(ownerId);
            var summaryList = PopulatePolicyAndPetSummaries(dtoList);
            return summaryList;
        }

        private static List<IPolicyAndPetSummary> PopulatePolicyAndPetSummaries(List<PolicyAndPetSummaryDto> dtoList)
        {
            var summaryList = dtoList.Select
            (
                dto =>
                    new PolicyAndPetSummary
                    {
                        PolicyId = dto.PolicyId,
                        PolicyNumber = dto.PolicyNumber,
                        PetId = dto.PetId,
                        PetName = dto.PetName,
                        PetDateOfBirth = dto.PetDateOfBirth,
                        SpeciesId = dto.SpeciesId,
                        SpeciesName = dto.SpeciesName,
                        BreedId = dto.BreedId,
                        BreedName = dto.BreedName,
                        AddToPolicyDate = dto.AddToPolicyDate,
                        RemoveFromPolicyDate = dto.RemoveFromPolicyDate
                    }
            ).ToList<IPolicyAndPetSummary>();
            return summaryList;
        }
    }
}