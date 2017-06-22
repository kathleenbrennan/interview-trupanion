using System;

namespace PetPolicyObjectSchema
{
    public class PolicyAndPetSummaryDto
    {
        public int PolicyId { get; set; }
        public string PolicyNumber { get; set; }
        public int PetId { get; set; }
        public string PetName { get; set; }
        public DateTime PetDateOfBirth { get; set; }
        public int SpeciesId { get; set; }
        public string SpeciesName { get; set; }
        public int BreedId { get; set; }
        public string BreedName { get; set; }
        public DateTime AddToPolicyDate { get; set; }
        public DateTime? RemoveFromPolicyDate { get; set; }
    }
}