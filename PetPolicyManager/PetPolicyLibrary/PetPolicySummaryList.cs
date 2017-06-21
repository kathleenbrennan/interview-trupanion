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

        public static List<IPetPolicySummary> GetPetPolicySummaryList(int ownerId)
        {
            var provider = DataProviderFactory.GetDataProvider();
            var dtoList = provider.GetPetPolicySummaryList(ownerId);

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