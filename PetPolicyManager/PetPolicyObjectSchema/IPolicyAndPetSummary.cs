using System;

namespace PetPolicyObjectSchema
{
    public interface IPolicyAndPetSummary
    {
        DateTime AddToPolicyDate { get; set; }
        int BreedId { get; set; }
        string BreedName { get; set; }
        DateTime PetDateOfBirth { get; set; }
        int PetId { get; set; }
        string PetName { get; set; }
        int PolicyId { get; set; }
        string PolicyNumber { get; set; }
        DateTime? RemoveFromPolicyDate { get; set; }
        int SpeciesId { get; set; }
        string SpeciesName { get; set; }
    }
}