using System;
using PetPolicyObjectSchema;

namespace PetPolicyDataProvider
{
    public class PetPolicyDataProviderDummy : PetPolicyDataProviderAbstract
    {
        public override string GeneratePolicyNumber(string countryCode)
        {
            return countryCode + "1234567890";
        }
    }
}