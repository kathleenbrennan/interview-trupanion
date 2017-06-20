using System;

namespace PetPolicyObjectSchema
{
    public class PetDto
    {
        public int OwnerId { get; set; }
        public string PetName { get; set; }
        public string BreedName { get; set; }
        public int SpeciesId { get; set; }
        public DateTime PetDateOfBirth { get; set; }
        public int PetId { get; set; }
    }
}