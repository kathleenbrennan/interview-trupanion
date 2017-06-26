using System;

namespace PetPolicyObjectSchema
{
    public interface IPet
    {
        int OwnerId { get; }
        int PetId { get; }
        string PetName { get; }
        int SpeciesId { get; }
        string BreedName { get; }
        DateTime PetDateOfBirth { get; }
    }
}