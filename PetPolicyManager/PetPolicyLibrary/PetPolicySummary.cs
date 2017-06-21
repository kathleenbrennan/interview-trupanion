using System;
using System.Collections.Generic;
using PetPolicyObjectSchema;

namespace PetPolicyLibrary
{

    public class PetPolicySummary : IPetPolicySummary
    {
        public int OwnerId { get; set; }
        public int PolicyId { get; set; }
        public string PolicyNumber { get; set; }
        public DateTime PolicyEnrollmentDate { get; set; }
        public DateTime? PolicyCancellationDate { get; set; }
        public string OwnerName { get; set; }
        public int CountryId { get; set; }
        public string CountryIso3LetterCode { get; set; }

        public PetPolicySummary()
        {
            //OwnerId = ownerId;
            //PolicyId = policyId;
            //PolicyNumber = policyNumber;
            //PolicyEnrollmentDate = policyEnrollmentDate;
            //PolicyCancellationDate = policyCancellationDate;
            //OwnerName = ownerName;
            //CountryId = countryId;
            //CountryIso3LetterCode = countryIso3LetterCode;
        }
    }
}