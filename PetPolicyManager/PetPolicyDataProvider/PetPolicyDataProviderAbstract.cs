using System;

namespace PetPolicyDataProvider
{
    public abstract class PetPolicyDataProviderAbstract : IPetPolicyDataProvider
    {
        public abstract string GeneratePolicyNumber(string countryCode, int ownerId);
    }
}
