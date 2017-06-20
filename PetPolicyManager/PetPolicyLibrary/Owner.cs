using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization.Formatters;
using System.Text;
using System.Threading.Tasks;
using PetPolicyDataProvider;
using PetPolicyObjectSchema;

namespace PetPolicyLibrary
{

    public static class OwnerFactory
    {
        public static IOwner RegisterOwner(string countryCode, string ownerName)
        {
            return new Owner(countryCode, ownerName);
        }
    }
    public class Owner : IOwner
    {
        public int OwnerId { get; }
        public string OwnerName { get;  }
        public int CountryId { get; }

        private Owner()
        {
            //hide constructor so unable to create without initialization 
        }

        internal Owner(string countryCode, string ownerName)
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
            var dto = new PetOwnerDto();
            dto = provider.RegisterOwner(countryCode, ownerName);

            OwnerId = dto.OwnerId;
            OwnerName = dto.OwnerName;
            CountryId = dto.CountryId;
        }


    }
}
