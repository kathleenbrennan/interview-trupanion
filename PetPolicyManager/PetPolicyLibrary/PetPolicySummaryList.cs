using System;
using System.Collections.Generic;
using System.Linq;
using PetPolicyObjectSchema;

namespace PetPolicyLibrary
{
    public class PetPolicySummaryList : List<IPetPolicySummary>
    {
        public int PolicyId { get; set; }
        public string PolicyNumber { get; set; }
        public DateTime PolicyEnrollmentDate { get; set; }
        public DateTime PolicyCancellationDate { get; set; }
        public int OwnerId { get; set; }
        public string OwnerName { get; set; }
        public int CountryId { get; set; }
        public string CountryIso3LetterCode { get; set; }


        public static List<IPetPolicySummary> GetPetPolicySummaryList()
        {
            var provider = DataProviderFactory.GetDataProvider();
            var dtoList = provider.GetPetPolicySummaryList();

            var summaryList = PopulatePetPolicySummaries(dtoList);
            return summaryList;
        }

        public static List<IPetPolicySummary> GetPetPolicySummaryListByOwner(int ownerId)
        {
            var provider = DataProviderFactory.GetDataProvider();
            var dtoList = provider.GetPetPolicySummaryListByOwner(ownerId);

            var summaryList = PopulatePetPolicySummaries(dtoList);
            return summaryList;
        }

        public static List<IPetPolicySummary> GetPetPolicySummaryListById(int policyId)
        {
            var provider = DataProviderFactory.GetDataProvider();
            var dtoList = provider.GetPetPolicySummaryListById(policyId);

            var summaryList = PopulatePetPolicySummaries(dtoList);
            return summaryList;
        }

        private static List<IPetPolicySummary> PopulatePetPolicySummaries(List<PetPolicySummaryDto> dtoList)
        {
            var summaryList = dtoList.Select
            (
                dto =>
                    new PetPolicySummary
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
            ).ToList<IPetPolicySummary>();
            return summaryList;
        }
    }
}