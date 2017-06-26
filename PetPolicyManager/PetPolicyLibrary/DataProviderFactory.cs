using System;
using PetPolicyDataProvider;

namespace PetPolicyLibrary
{
    internal static class DataProviderFactory
    {
        internal static IPetPolicyDataProvider GetDataProvider()
        {
            //use dependency injection to determine 
            //  whether or not to use database
            var useDatabaseConfigSetting =
                System.Configuration.ConfigurationManager.AppSettings["useDatabase"];

            bool result;
            var useDatabase =
                Boolean.TryParse(useDatabaseConfigSetting, out result)
                && result;

            IPetPolicyDataProvider provider =
                PetPolicyDataProviderFactory.GetProvider(useDatabase: useDatabase);
            return provider;
        }
    }
}