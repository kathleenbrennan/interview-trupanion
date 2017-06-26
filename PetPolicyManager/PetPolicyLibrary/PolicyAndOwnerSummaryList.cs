using System;
using System.Collections.Generic;
using System.Linq;
using PetPolicyObjectSchema;

namespace PetPolicyLibrary
{
    public class PolicyAndOwnerSummaryList : List<IPolicyAndOwnerSummary>
    {
        public static List<IPolicyAndOwnerSummary> GetPolicyAndOwnerSummaryList()
        {
            var provider = DataProviderFactory.GetDataProvider();
            var dtoList = provider.GetPolicyAndOwnerSummaryList();

            var summaryList = PopulatePolicyAndOwnerSummaries(dtoList);
            return summaryList;
        }

        public static List<IPolicyAndOwnerSummary> GetPolicyAndOwnerSummaryListByOwner(int ownerId)
        {
            var provider = DataProviderFactory.GetDataProvider();
            var dtoList = provider.GetPolicyAndOwnerSummaryListByOwner(ownerId);

            var summaryList = PopulatePolicyAndOwnerSummaries(dtoList);
            return summaryList;
        }

        public static List<IPolicyAndOwnerSummary> GetPolicyAndOwnerSummaryListById(int policyId)
        {
            var provider = DataProviderFactory.GetDataProvider();
            var dtoList = provider.GetPolicyAndOwnerSummaryListById(policyId);

            var summaryList = PopulatePolicyAndOwnerSummaries(dtoList);
            return summaryList;
        }

        private static List<IPolicyAndOwnerSummary> PopulatePolicyAndOwnerSummaries(List<PolicyAndOwnerSummaryDto> dtoList)
        {
            var summaryList = dtoList.Select
            (
                dto =>
                    new PolicyAndOwnerSummary
                    {
                        PolicyId = dto.PolicyId,
                        PolicyNumber = dto.PolicyNumber,
                        PolicyEnrollmentDate = dto.PolicyEnrollmentDate,
                        PolicyCancellationDate = dto.PolicyCancellationDate,
                        CountryId = dto.CountryId,
                        CountryIso3LetterCode = dto.CountryIso3LetterCode,
                        OwnerId = dto.OwnerId,
                        OwnerName = dto.OwnerName
                    }
            ).ToList<IPolicyAndOwnerSummary>();
            return summaryList;
        }
    }
}