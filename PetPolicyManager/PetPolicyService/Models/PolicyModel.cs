using PetPolicyObjectSchema;

namespace PetPolicyService.Models
{
    public class PolicyModel : IPetPolicy
    {
        public int PolicyId { get; set; }
        public string PolicyNumber { get; set; }
    }
}