using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PetPolicyObjectSchema;
using System.Configuration;

namespace PetPolicyLibrary
{
    public static class PetPolicyFactory
    {

        public static IPetPolicy Enroll(string countryCode, int? ownerId)
        {
            if (string.IsNullOrWhiteSpace(countryCode) || countryCode.Length < 3 || countryCode.Length > 3)
            {
                throw new UnableToCreatePolicyException("A three-letter country code is required to enroll a policy.");
            }
            if (!ownerId.HasValue)
            {
                throw new UnableToCreatePolicyException("A non-null owner ID is required to enroll a policy.");
            }
            return new PetPolicy(countryCode, ownerId.Value);

        }

        public static List<IPetPolicySummary> GetPolicySummaryList(int ownerId)
        {
            

            return PetPolicySummaryList.GetPetPolicySummaryList(ownerId);
        }

        public class PetPolicy : IPetPolicy
        {

            public string PolicyNumber { get; set; }

            private PetPolicy()
            {
                //hide constructor so unable to create without initialization 
            }



            internal PetPolicy(string countryCode, int ownerId)
            {
                var provider = DataProviderFactory.GetDataProvider();
                var dto = new PetPolicyDto();

                dto.PolicyNumber = provider.GeneratePolicyNumber(countryCode, ownerId);

                PolicyNumber = dto.PolicyNumber;
            }
        }
    }
}
