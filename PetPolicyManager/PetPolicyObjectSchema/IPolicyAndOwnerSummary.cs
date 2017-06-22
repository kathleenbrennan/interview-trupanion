using System;

namespace PetPolicyObjectSchema
{
    public interface IPolicyAndOwnerSummary
    {
        int PolicyId { get; set; }
        string PolicyNumber { get; set; }
        DateTime PolicyEnrollmentDate { get; set; }
        DateTime? PolicyCancellationDate { get; set; }
        int OwnerId { get; set; }
        string OwnerName { get; set; }
        int CountryId { get; set; }
        string CountryIso3LetterCode { get; set; }

    }
}