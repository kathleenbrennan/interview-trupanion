using System;
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

        public static List<IPolicyAndOwnerSummary> GetPolicySummaryList()
        {
            return PolicyAndOwnerSummaryList.GetPolicyAndOwnerSummaryList();
        }

        public static List<IPolicyAndOwnerSummary> GetPolicySummaryListByOwner(int ownerId)
        {
            return PolicyAndOwnerSummaryList.GetPolicyAndOwnerSummaryListByOwner(ownerId);
        }

        public static List<IPolicyAndOwnerSummary> GetPolicySummaryListById(int policyId)
        {
            return PolicyAndOwnerSummaryList.GetPetPolicySummaryListById(policyId);
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
                //todo: in a real system, you'd probably want 
                //  to get the id of the policy in case you wanted to do other things with it
            }
        }

        public static DateTime? AddPetToPolicy(int petId, int policyId)
        {
            //todo: check if pet already has a policy and if it does 
            //  return the policy add date
            try
            {
                var provider = DataProviderFactory.GetDataProvider();
                DateTime? addDate = provider.AddPetToPolicy(petId, policyId);
                //todo: return the policy add date
                return addDate;
            }
            catch (Exception ex)
            {
                throw ex;
                //todo: would have better error handling here in a real system
            }
        }

        public static DateTime? RemovePetFromPolicy(int petId, int policyId)
        {
            //todo: return error if this pet is not on this policy
            //todo: return policy remove date if successful
            try
            {
                var provider = DataProviderFactory.GetDataProvider();
                DateTime? removeDate = provider.RemovePetFromPolicy(petId, policyId);
                return removeDate;
            }
            catch (Exception ex)
            {
                throw ex;
                //todo: would have better error handling here in a real system
            }
        }
    }
}
