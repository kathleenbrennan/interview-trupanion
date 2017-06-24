using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetPolicyClientConsoleApp
{
    public class PolicyAndOwnerSummary
    {
        public int PolicyId { get; set; }
        public string PolicyNumber { get; set; }
        public DateTime PolicyEnrollmentDate { get; set; }
        public DateTime? PolicyCancellationDate { get; set; }
        public int OwnerId { get; set; }
        public string OwnerName { get; set; }
        public int CountryId { get; set; }
        public string CountryIso3LetterCode { get; set; }
    }

    public class PolicyAndPetSummary
    {
        public DateTime AddToPolicyDate { get; set; }
        public int BreedId { get; set; }
        public string BreedName { get; set; }
        public DateTime PetDateOfBirth { get; set; }
        public int PetId { get; set; }
        public string PetName { get; set; }
        public int PolicyId { get; set; }
        public string PolicyNumber { get; set; }
        public DateTime? RemoveFromPolicyDate { get; set; }
        public int SpeciesId { get; set; }
        public string SpeciesName { get; set; }
    }

    public class OwnerModel
    {
        public int OwnerId { get; set; }
        public string OwnerName { get; set; }
        public string CountryIso3LetterCode { get; set; }
    }

    public class PolicyModel
    {
        public int PolicyId { get; set; }
        public string PolicyNumber { get; set; }
    }

    public class PetModel
    {
        public int PetId { get; set; }
        public int OwnerId { get; set; }
        public string PetName { get; set; }
        public string Species { get; set; }
        public string BreedName { get; set; }
        public DateTime PetDateOfBirth { get; set; }
    }
}
