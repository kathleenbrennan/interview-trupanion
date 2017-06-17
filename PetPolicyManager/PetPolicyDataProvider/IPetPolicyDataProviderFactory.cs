namespace PetPolicyDataProvider
{
    public static class PetPolicyDataProviderFactory
    {
        public static IPetPolicyDataProvider GetProvider(bool useDatabase)
        {
            if (useDatabase)
            {
                return new PetPolicyDataProviderDatabase();
            }
            return new PetPolicyDataProviderDummy();
        }
    }
}