namespace PetPolicyObjectSchema
{
    public interface IOwner
    {
        int CountryId { get; }
        int OwnerId { get; }
        string OwnerName { get; }
    }
}