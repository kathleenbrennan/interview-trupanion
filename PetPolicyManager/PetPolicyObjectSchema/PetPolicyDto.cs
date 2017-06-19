

namespace PetPolicyObjectSchema
{
    public class PetPolicyDto
    {
        public string PolicyNumber { get; set; }
    }

    public class PetOwnerDto
    {
        public int OwnerId { get; set; }
        public string OwnerName { get; set; }
        public int CountryId { get; set; }
    }
}
