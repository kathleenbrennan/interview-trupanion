using System;
using System.Collections.Generic;
using PetPolicyObjectSchema;

namespace PetPolicyLibrary
{

    public class PolicyAndOwnerAndOwnerSummary : IPolicyAndOwnerSummary
    {
        public int OwnerId { get; set; }
        public int PolicyId { get; set; }
        public string PolicyNumber { get; set; }
        public DateTime PolicyEnrollmentDate { get; set; }
        public DateTime? PolicyCancellationDate { get; set; }
        public string OwnerName { get; set; }
        public int CountryId { get; set; }
        public string CountryIso3LetterCode { get; set; }

        public PolicyAndOwnerAndOwnerSummary()
        {
        }
    }
}